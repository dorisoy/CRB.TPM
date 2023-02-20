using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Admin.Core.Application.Account.Dto;
using CRB.TPM.Mod.Admin.Core.Application.Account.Vo;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure.Repositories;

public class AccountRepository : RepositoryAbstract<AccountEntity>, IAccountRepository
{

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<PagingQueryResultModel<AccountEntity>> Query(AccountQueryDto dto)
    {
        var query = QueryBuilder(dto);
        return query.ToPaginationResult(dto.Paging);
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<PagingQueryResultModel<dynamic>> Query(AccountSelectQueryDto dto)
    {
        var result = new PagingQueryResultModel<dynamic>();

        var query = Find(m => m.IsLock == false);

        if (dto.Type != null)
        {
            query.Where(s => s.Type == dto.Type);
        }

        if (dto.Ids != null && dto.Ids.Length > 0)
        {
            query.Where(s => dto.Ids.Contains(s.Id));
        }

        query.Select(m => new
        {
            label = m.Name,
            value = m.Id,
        });

        var data = await query.ToPaginationDynamicResult(dto.Paging);

        return result.Success(data);
    }

    #region
    //public async Task<PagingQueryResultModel<AccountSelectVo>> Query2(AccountSelectQueryDto dto)
    //{
    //    var result = new PagingQueryResultModel<AccountSelectVo>();

    //    var query = Find(m => m.Type == dto.Type && m.IsLock == false);

    //    if (dto.Ids != null && dto.Ids.Length > 0)
    //    {
    //        query.Where(s => dto.Ids.Contains(s.Id));
    //    }

    //    query.Select(m => new
    //    {
    //        lable = m.Name,
    //        value = m.Id,
    //    });

    //    return await query.ToPaginationResultVo<AccountSelectVo>(dto.Paging);
    //}
    #endregion

    public Task<bool> ExistsUsername(string username, Guid? id = null)
    {
        return Find(m => m.Username == username).WhereNotNull(id, m => m.Id != id).ToExists();
    }

    public Task<bool> ExistsPhone(string phone, Guid? id = null)
    {
        return Find(m => m.Phone == phone).WhereNotNull(id, m => m.Id != id).ToExists();
    }

    public Task<bool> ExistsEmail(string email, Guid? id = null)
    {
        return Find(m => m.Email == email).WhereNotNull(id, m => m.Id != id).ToExists();
    }

    public Task<AccountEntity> GetByUserName(string username, Guid? tenantId = null)
    {
        return Find(m => m.Username == username).WhereNotNull(tenantId, m => m.TenantId == tenantId.Value).ToFirst();
    }


    private Data.Abstractions.Queryable.IQueryable<AccountEntity> QueryBuilder(AccountQueryDto dto)
    {
        var query = Find()
            .WhereNotNull(dto.Keys, m => m.Username == dto.Keys || m.Name.Contains(dto.Keys) || m.Phone.Contains(dto.Keys));

        if (dto.HeadOffice.Length > 0)
            query.Where(m=> dto.HeadOffice.Contains(m.OrgId));

        if (dto.Dbs.Length > 0)
            query.Where(m => dto.Dbs.Contains(m.OrgId));

        if (dto.MarketingCenters.Length > 0)
            query.Where(m => dto.MarketingCenters.Contains(m.OrgId));

        if (dto.SaleRegions.Length > 0)
            query.Where(m => dto.SaleRegions.Contains(m.OrgId));

        if (dto.SaleRegions.Length > 0)
            query.Where(m => dto.SaleRegions.Contains(m.OrgId));

        if (dto.Departments.Length > 0)
            query.Where(m => dto.Departments.Contains(m.OrgId));

        if (dto.Type != Auth.Abstractions.AccountType.UnKnown)
            query.WhereNotNull(dto.Type, m => m.Type == dto.Type);

        if (dto.Status != AccountStatus.UnKnown)
            query.WhereNotNull(dto.Status, m => m.Status == dto.Status);

        query.Select(m => m).OrderByDescending(m => m.Id);

        return query;
    }
}