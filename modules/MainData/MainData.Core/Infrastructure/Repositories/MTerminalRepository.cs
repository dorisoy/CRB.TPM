
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.MainData.Core.Application.MTerminal.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminal;
using CRB.TPM.Mod.MainData.Core.Application.MTerminal.Vo;
using CRB.TPM.Mod.MainData.Core.Application.MDistributorRelation.Dto;
using System.Text;
using CRB.TPM.Mod.MainData.Core.Application.MDistributorRelation.Vo;
using System.Linq;
using CRB.TPM.Utils.ClayObject;
using CRB.TPM.Data.Core.SqlBuilder;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Vo;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributorRelation;
using CRB.TPM.Mod.Admin.Core.Domain.MObject;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using CRB.TPM.Mod.Admin.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Defaults;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MTerminalRepository : RepositoryAbstract<MTerminalEntity>, IMTerminalRepository
{
    ///// <summary>
    ///// 根据组织权限生成Sql条件字符串
    ///// </summary>
    //public BuildSqlWhereStrByOrgAuthFunc BuildSqlWhereStrAuthFunc { get; set; }

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
    public async Task<PagingQueryResultModel<MTerminalQueryVo>> QueryPage(MTerminalQueryDto dto)
    {
        var build = await QueryBuild(dto);
        string sqlCount = build.BuildTotalCount();
        dto.Paging.TotalCount = await QueryFirstOrDefault<long>(sqlCount, dto);

        string sqlPage = build.BuildQueryPage(dto.Paging.Skip, dto.Paging.Size);
        var rows = await Query<MTerminalQueryVo>(sqlPage.ToString(), dto);
        var resultBody = new PagingQueryResultBody<MTerminalQueryVo>(rows?.ToList(), dto.Paging);
        var res = new PagingQueryResultModel<MTerminalQueryVo>().Success(resultBody);
        return res;
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<IList<MTerminalQueryVo>> Query(MTerminalQueryDto dto)
    {
        var build = await QueryBuild(dto);
        string sql = build.BuildQuery();
        var rows = await base.Query<MTerminalQueryVo>(sql, dto);
        return rows.ToList();
    }

    /// <summary>
    /// 是否可以更新终端编码
    /// </summary>
    /// <returns>true=可以更新，false=不能更新</returns>
    public async Task<(bool res, string msg)> IsChangeMTerminalCode(string code)
    {
        //经销分销绑定，就不能修改编码
        string sql = @$"SELECT COUNT(1) FROM M_Terminal_Distributor WHERE TerminalCode='{code}'";
        var res = await base.QueryFirstOrDefault<long>(sql);
        return (res == 0, "终端经销商关系已绑定");
    }

    private async Task<SqlStrJoinBuilder> QueryBuild(MTerminalQueryDto dto)
    {
        var joinBuilder = SqlStrJoinBuilder.CreateInstance();
        joinBuilder.SelectStr = @$"
            mt.[Id],
            mt.TerminalCode,
	        mt.TerminalName,
	        mt.Lvl1Type,
	        mt.Lvl2Type,
	        mt.Lvl3Type,
	        mt.[Status],
	        mt.[Address],
            mt.SaleLine,
	        mobj.MarketingId MarketingId,
	        mobj.MarketingName MarketingName,
	        mobj.BigAreaId DutyregionId,
	        mobj.BigAreaName DutyregionName,
	        mobj.OfficeId DepartmentId,
	        mobj.OfficeName DepartmentName,
	        mobj.StationId StationId,
	        mobj.StationName StationName";

        joinBuilder.FromStr = @$"
            FROM
            M_Terminal mt
            JOIN M_Object mobj ON mobj.StationId=mt.StationId AND mobj.Type={(int)OrgEnumType.Station}";
        dto.DistributorId.NotEmptyIf(() => joinBuilder.FromStr += "LEFT JOIN M_Terminal_Distributor mtd ON mtd.TerminalId=mt.Id");

        List<string> rootWhere = new List<string>();
        dto.Name.NotNullIf(() => rootWhere.Add("(mt.TerminalName like CONCAT('%',@NAME,'%') OR mt.TerminalCode like CONCAT('%',@NAME,'%'))"));
        dto.DistributorId.NotEmptyIf(() => rootWhere.Add("mtd.Id=@DistributorId"));

        var filter = await dto.BuildFilter(SP);
        if (!string.IsNullOrEmpty(filter))
        {
            //rootWhere.Add(this.BuildSqlWhereStrAuthFunc(dto).Result);
            rootWhere.Add(filter);
        }
        else
        {
            dto.MarketingIds.NotNullAndEmptyIf(() => rootWhere.Add("mobj.MarketingId IN @MarketingIds"));
            dto.DutyregionIds.NotNullAndEmptyIf(() => rootWhere.Add("mobj.BigAreaId IN @DutyregionIds"));
            dto.DepartmentIds.NotNullAndEmptyIf(() => rootWhere.Add("mobj.OfficeId IN @DepartmentIds"));
            dto.StationIds.NotNullAndEmptyIf(() => rootWhere.Add("mt.StationId IN @StationIds"));
        }
        joinBuilder.WhereStr = rootWhere.AndBuildStr().WhereRootBuildStr();
        joinBuilder.OrderStr = "ORDER BY mt.[Id] ASC";
        return joinBuilder;
    }

    /// <summary>
    /// 终端Select下拉接口
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<PagingQueryResultModel<MTerminalSelectVo>> Select(MTerminalSelectDto dto)
    {
        var query = Find();
        if (dto.Ids.NotNullAndEmpty())
        {
            dto.Page.Size = int.MaxValue;
            return await query.Where(w => dto.Ids.Contains(w.Id)).Select(s => new { Label = s.TerminalName, Value = s.Id }).ToPaginationResultVo<MTerminalSelectVo>(dto.Paging);
        }

        var queryPage = query
            .LeftJoin<MObjectEntity>(j => j.T1.StationId == j.T2.StationId);

        var filter = await dto.BuildFilter(SP, OrgEnumType.Station, "T2");
        if (!string.IsNullOrEmpty(filter))
        {
            //query = query
            //.Where(BuildSqlWhereStrAuthFunc(dto, OrgEnumType.Station, "T2").Result);
            query = query.Where(filter);
        }
        else
        {
            queryPage = queryPage
            .WhereIf(dto.StationIds.NotNullAndEmpty(), w => dto.StationIds.Contains(w.T2.StationId))
            .WhereIf(dto.DepartmentIds.NotNullAndEmpty(), w => dto.DepartmentIds.Contains(w.T2.OfficeId))
            .WhereIf(dto.DutyregionIds.NotNullAndEmpty(), w => dto.DutyregionIds.Contains(w.T2.BigAreaId))
            .WhereIf(dto.MarketingIds.NotNullAndEmpty(), w => dto.MarketingIds.Contains(w.T2.MarketingId));
        }
        queryPage = queryPage.WhereNotNull(dto.Name, w => w.T1.TerminalName.Contains(dto.Name))
                             .OrderByDescending(o => o.T1.Id);

        return await queryPage.Select(s => new { Label = s.T1.TerminalName, Value = s.T1.Id }).ToPaginationResultVo<MTerminalSelectVo>(dto.Paging);
    }
}