
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.MObject;
using CRB.TPM.Mod.Admin.Core.Domain.MOrg;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Defaults;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MMarketingSetup.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MMarketingSetup.Vo;
using CRB.TPM.Mod.MainData.Core.Domain.MMarketingSetup;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MMarketingSetupRepository : RepositoryAbstract<MMarketingSetupEntity>, IMMarketingSetupRepository
{
    private IQueryable<MMarketingSetupEntity, MObjectEntity> queryBuilder;

    ///// <summary>
    ///// 根据组织权限生成Sql条件字符串
    ///// </summary>
    //public BuildSqlWhereStrByOrgAuthFunc BuildSqlWhereStrAuthFunc { get; set; }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<PagingQueryResultModel<MMarketingSetupQueryVo>> QueryPage(MMarketingSetupQueryDto dto)
    {
        await this.QueryBuild().QueryWhereBuild(dto);
        return await queryBuilder.ToPaginationResultVo<MMarketingSetupQueryVo>(dto.Paging);
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<IList<MDistributorQueryVo>> Query(MMarketingSetupExportDto dto)
    {
        this.QueryBuild().QueryExportWhereBuild(dto);
        var rows = await queryBuilder.ToList<MDistributorQueryVo>();
        return rows;
    }

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
    /// 构建查询
    /// </summary>
    /// <returns></returns>
    private MMarketingSetupRepository QueryBuild()
    {
        queryBuilder = Find()
        .InnerJoin<MObjectEntity>(j => j.T1.MarketingId == j.T2.MarketingId)
        .Select(s => new
        {
            Id = s.T1.Id,
            OrgCode = s.T2.MarketingCode,
            OrgName = s.T2.MarketingName,
            IsReal = s.T1.IsReal,
            IsSynchronizeCrm = s.T1.IsSynchronizeCRM,
            IsSynchronizeCrmDistributorStation = s.T1.IsSynchronizeCRMDistributorStation,
            Creator = s.T1.Creator,
            CreatedTime = s.T1.CreatedTime,
            Modifier = s.T1.Modifier,
            ModifierTime = s.T1.ModifiedTime
        });
        return this;
    }

    /// <summary>
    /// 构建条件查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private async Task<MMarketingSetupRepository> QueryWhereBuild(MMarketingSetupQueryDto dto)
    {
        queryBuilder = queryBuilder
            .WhereNotNull(dto.Name, w => w.T2.MarketingCode.Contains(dto.Name) || w.T2.MarketingName.Contains(dto.Name));

        dto.MarketingIds = dto.MarketingIds;
        var filter = await dto.BuildFilter(SP, OrgEnumType.MarketingCenter, "T2");
        if (!string.IsNullOrEmpty(filter))
        {
            queryBuilder = queryBuilder.Where(filter);
        }
        else
        {
            queryBuilder = queryBuilder.WhereIf(dto.MarketingIds.NotNullAndEmpty(), w => w.T2.Type == (int)OrgEnumType.MarketingCenter && dto.MarketingIds.Contains(w.T2.Id));
        }

        //if (this.BuildSqlWhereStrAuthFunc != null)
        //{
        //    var input = IOrgIevelIdsDtoFactory.CreateInstance();
        //    input.MarketingIds = dto.MarketingIds;
        //    queryBuilder = queryBuilder
        //   .Where(BuildSqlWhereStrAuthFunc(input, OrgEnumType.MarketingCenter, "T2").Result);
        //}
        //else
        //{
        //    queryBuilder = queryBuilder.WhereIf(dto.MarketingIds.NotNullAndEmpty(), w => w.T2.Type == (int)OrgEnumType.MarketingCenter && dto.MarketingIds.Contains(w.T2.Id));
        //}

        return this;
    }

    /// <summary>
    /// 构建条件查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private MMarketingSetupRepository QueryExportWhereBuild(MMarketingSetupExportDto dto)
    {
        queryBuilder = queryBuilder
            .WhereNotNull(dto.Name, w => w.T2.MarketingCode.Contains(dto.Name) || w.T2.MarketingName.Contains(dto.Name));
        return this;
    }
}