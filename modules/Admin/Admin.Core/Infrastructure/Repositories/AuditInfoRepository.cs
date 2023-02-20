using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Admin.Core.Application.AuditInfo.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Mod.Admin.Core.Domain.AuditInfo;
using CRB.TPM.Module.Abstractions;
using CRB.TPM.Utils.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CRB.TPM.Mod.Admin.Core.Infrastructure.Repositories;

public class AuditInfoRepository : RepositoryAbstract<AuditInfoEntity>, IAuditInfoRepository
{

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<PagingQueryResultModel<AuditInfoEntity>> Query(AuditInfoQueryDto dto)
    {
        return QueryBuilder(dto).ToPaginationResult(dto.Paging);
    }

    private Data.Abstractions.Queryable.IQueryable<AuditInfoEntity> QueryBuilder(AuditInfoQueryDto dto)
    {
        var query = Find();

        query.WhereNotNull(dto.Platform, m => m.Platform == dto.Platform.Value);
        query.WhereNotNull(dto.ModuleCode, m => m.Area == dto.ModuleCode);
        query.WhereNotNull(dto.Controller, m => m.Controller.Contains(dto.Controller) || m.ControllerDesc.Contains(dto.Controller));
        query.WhereNotNull(dto.Action, m => m.ActionDesc.Contains(dto.Action) || m.Action.Contains(dto.Action));
        query.WhereNotNull(dto.StartDate, m => m.ExecutionTime >= dto.StartDate.Value.Date);
        query.WhereNotNull(dto.EndDate, m => m.ExecutionTime < dto.EndDate.Value.AddDays(1).Date);

        query.OrderByDescending(x => x.Id);

        return query;
    }

    /// <summary>
    /// 按照模块查询访问量
    /// </summary>
    /// <returns></returns>
    public Task<IList<OptionResultModel>> QueryCountByModule()
    {
        return Find().GroupBy(m => new { m.Area }).OrderByDescending(m => m.Count())
            .Select(m => new { Label = m.Key.Area, Value = m.Count() }).ToList<OptionResultModel>();
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"></param>
    /// <param name="modules"></param>
    /// <returns></returns>
    public async Task<AuditInfoEntity> Details(int id, IList<ModuleDescriptor> modules = null)
    {
        return await Find(m => m.Id == id)
               .LeftJoin<AccountEntity>(m => m.T1.AccountId == m.T2.Id)
               //.LeftJoin<ModuleEntity>((x, y, z) => x.Area == z.Code)
               .Select(m => new
               {
                   m.T1,
                   Creator = m.T2.Name,
                   ModuleName = (modules.Where(s => s.Code == m.T1.Area).FirstOrDefault()).Name ?? ""
               }).ToFirst<AuditInfoEntity>();
    }

    /// <summary>
    /// 查询最近一周访问量
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<ChartDataResultModel>> QueryLatestWeekPv()
    {
        return QueryLatestWeekPvAsync();
    }
}
