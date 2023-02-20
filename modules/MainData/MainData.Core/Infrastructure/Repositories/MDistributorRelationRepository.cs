
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Data.Core.SqlBuilder;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Defaults;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using CRB.TPM.Mod.MainData.Core.Application.MDistributorRelation.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MDistributorRelation.Vo;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributorRelation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MDistributorRelationRepository : RepositoryAbstract<MDistributorRelationEntity>, IMDistributorRelationRepository
{
    ///// <summary>
    ///// 根据组织权限生成Sql条件字符串
    ///// </summary>
    //public BuildSqlWhereStrByOrgAuthFunc BuildSqlWhereStrAuthFunc { get; set; }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<PagingQueryResultModel<MDistributorRelationQueryVo>> QueryPage(MDistributorRelationQueryDto dto)
    {
        var build = await QueryBuild(dto);
        string sqlCount = build.BuildTotalCount();
        dto.Paging.TotalCount = await base.QueryFirstOrDefault<long>(sqlCount, dto);

        string sqlPage = build.BuildQueryPage(dto.Paging.Skip, dto.Paging.Size);
        var rows = await base.Query<MDistributorRelationQueryVo>(sqlPage, dto);
        var resultBody = new PagingQueryResultBody<MDistributorRelationQueryVo>(rows?.ToList(), dto.Paging);
        var res = new PagingQueryResultModel<MDistributorRelationQueryVo>().Success(resultBody);
        return res;
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<IList<MDistributorRelationQueryVo>> Query(MDistributorRelationQueryDto dto)
    {
        var qb = await QueryBuild(dto);
        string sql = qb.BuildQuery();
        var rows = await base.Query<MDistributorRelationQueryVo>(sql, dto);
        return rows.ToList();
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

    private async Task<SqlStrJoinBuilder> QueryBuild(MDistributorRelationQueryDto dto)
    {
        var joinBuilder = SqlStrJoinBuilder.CreateInstance();
        joinBuilder.SelectStr = @$"
            dr.[Id] AS [Id],
        	dr.[DistributorCode1] AS [DistributorCode1],
        	dr.[DistributorCode2] AS [DistributorCode2],
        	d1.[DistributorName] AS [DistributorName1],
        	d2.[DistributorName] AS [DistributorName2],
        	obj1.[StationName] AS [DistributorStationName1],
        	obj2.[StationName] AS [DistributorStationName2]";

        joinBuilder.FromStr = @$"FROM
        	[M_DistributorRelation] AS dr WITH ( NOLOCK )
        	LEFT JOIN [M_Distributor] AS d1 WITH ( NOLOCK ) ON dr.[DistributorId1] = d1.[Id]
        	JOIN [M_Object] AS obj1 WITH ( NOLOCK ) ON dr.[DistributorId1] = obj1.[DistributorId]
        	LEFT JOIN [M_Distributor] AS d2 WITH ( NOLOCK ) ON dr.[DistributorId2] = d2.[Id]
        	LEFT JOIN [M_Object] AS obj2 WITH ( NOLOCK ) ON dr.[DistributorId2] = obj2.[DistributorId]";

        List<string> rootWhere = new List<string>();

        var filter = await dto.BuildFilter(SP, OrgEnumType.Distributor, "obj1");
        if (!string.IsNullOrEmpty(filter))
        {
            rootWhere.Add(filter);
            //rootWhere.Add(this.BuildSqlWhereStrAuthFunc(dto, OrgEnumType.Distributor, "obj1").Result);
        }
        else
        {
            dto.MarketingIds.NotNullAndEmptyIf(() => rootWhere.Add("obj1.MarketingId IN @MarketingIds"));
            dto.DutyregionIds.NotNullAndEmptyIf(() => rootWhere.Add("obj1.BigAreaId IN @DutyregionIds"));
            dto.DepartmentIds.NotNullAndEmptyIf(() => rootWhere.Add("obj1.OfficeId IN @DepartmentIds"));
            dto.StationIds.NotNullAndEmptyIf(() => rootWhere.Add("obj1.StationId IN @StationIds"));
        }
        dto.DistributorIds.RemoveGuidEmptyIf(_ => rootWhere.Add("dr.DistributorId1 IN @DistributorIds"));
        dto.DistributorId2.NotEmptyIf(() => rootWhere.Add("dr.DistributorId2=@DistributorId2"));

        joinBuilder.WhereStr = rootWhere.AndBuildStr().WhereRootBuildStr();
        joinBuilder.OrderStr = "ORDER BY [Id] ASC";
        return joinBuilder;
    }
}