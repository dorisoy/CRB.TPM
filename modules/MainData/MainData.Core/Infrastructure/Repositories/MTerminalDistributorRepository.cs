
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Admin.Core.Domain.MObject;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalDistributor.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalDistributor.Vo;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributor;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminal;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminalDistributor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MTerminalDistributorRepository : RepositoryAbstract<MTerminalDistributorEntity>, IMTerminalDistributorRepository
{
    private IQueryable<MTerminalDistributorEntity, MDistributorEntity, MObjectEntity, MTerminalEntity> queryBuilder;

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<int> Delete(IEnumerable<Guid> ids)
    {
        int result = 0;
        foreach (var id in ids)
        {
            if (await Delete(id)) result++;
        }
        return result;
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<PagingQueryResultModel<MTerminalDistributorQueryVo>> QueryPage(MTerminalDistributorQueryDto dto)
    {
        QueryBuild().QueryWhereBuild(dto);
        return await queryBuilder.ToPaginationResultVo<MTerminalDistributorQueryVo>(dto.Paging);
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<IList<MTerminalDistributorQueryVo>> Query(MTerminalDistributorQueryDto dto)
    {
        this.QueryBuild().QueryWhereBuild(dto);
        var rows = await queryBuilder.ToList<MTerminalDistributorQueryVo>();
        return rows;
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <returns></returns>
    private MTerminalDistributorRepository QueryBuild()
    {
        queryBuilder =
            Find()
            .InnerJoin<MDistributorEntity>(i => i.T1.DistributorId == i.T2.Id)
            .LeftJoin<MObjectEntity>(j => j.T2.Id == j.T3.DistributorId)
            .InnerJoin<MTerminalEntity>(j => j.T1.TerminalId == j.T4.Id)
            .Select(s => new
            {
                Id = s.T1.Id,
                DistributorId = s.T1.DistributorId,
                TerminalId = s.T1.TerminalId,
                DistributorCode = s.T2.DistributorCode,
                TerminalCode = s.T4.TerminalCode,
                DistributorName = s.T2.DistributorName,
                TerminalName = s.T4.TerminalName
            });
        return this;
    }

    /// <summary>
    /// 构建条件查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private MTerminalDistributorRepository QueryWhereBuild(MTerminalDistributorQueryDto dto)
    {
        queryBuilder = queryBuilder
            .WhereIf(dto.MarketingIds.NotNullAndEmpty(), w => dto.MarketingIds.Contains(w.T3.MarketingId))
            .WhereIf(dto.DutyregionIds.NotNullAndEmpty(), w => dto.DutyregionIds.Contains(w.T3.BigAreaId))
            .WhereIf(dto.DepartmentIds.NotNullAndEmpty(), w => dto.DepartmentIds.Contains(w.T3.OfficeId))
            .WhereIf(dto.StationIds.NotNullAndEmpty(), w => dto.StationIds.Contains(w.T3.StationId))
            .WhereIf(dto.DistributorIds.NotNullAndEmpty(), w => dto.DistributorIds.Contains(w.T2.Id))
            .WhereNotEmpty(dto.TerminalId, w => w.T4.Id == dto.TerminalId);
        return this;
    }
}