
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Data.Core.SqlBuilder;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Defaults;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalUser.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalUser.Vo;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminalUser;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MTerminalUserRepository : RepositoryAbstract<MTerminalUserEntity>, IMTerminalUserRepository
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
    public async Task<PagingQueryResultModel<MTerminalUserQueryVo>> QueryPage(MTerminalUserQueryDto dto)
    {
        var build = await QueryBuild(dto);
        string sqlCount = build.BuildTotalCount();
        dto.Paging.TotalCount = await base.QueryFirstOrDefault<long>(sqlCount, dto);
        string sqlPage = build.BuildQueryPage(dto.Paging.Skip, dto.Paging.Size);
        var rows = await base.Query<MTerminalUserQueryVo>(sqlPage, dto);
        var resultBody = new PagingQueryResultBody<MTerminalUserQueryVo>(rows?.ToList(), dto.Paging);
        var res = new PagingQueryResultModel<MTerminalUserQueryVo>().Success(resultBody);
        return res;
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<IList<MTerminalUserQueryVo>> Query(MTerminalUserQueryDto dto)
    {
        var build = await QueryBuild(dto);
        string sql = build.BuildQuery();
        var rows = await base.Query<MTerminalUserQueryVo>(sql, dto);
        return rows.ToList();
    }

    public async Task<SqlStrJoinBuilder> QueryBuild(MTerminalUserQueryDto dto)

    {
        var joinBuilder = SqlStrJoinBuilder.CreateInstance();
        joinBuilder.SelectStr = @$"
            tmt.Id,
            tmt.TerminalId,
            tmt.TerminalCode,
            tmt.AccountId,
            mt.TerminalName,
            tmt.UserBP,
            ac.Name AS AccountName";

        joinBuilder.FromStr = $@"
            FROM 
            M_Terminal_User tmt 
            LEFT JOIN SYS_Account ac ON ac.Id=tmt.AccountId
            JOIN M_Terminal mt ON mt.Id=tmt.TerminalId
            JOIN M_Object mobj ON mobj.StationId=mt.StationId AND mobj.Type={(int)OrgEnumType.Station}";

        List<string> rootWhere = new List<string>();
        dto.TerminalId.NotEmptyIf(() => rootWhere.Add("tmt.TerminalId=@TerminalId"));
        dto.AccountId.NotEmptyIf(() => rootWhere.Add("tmt.AccountId=@AccountId"));

        var filter = await dto.BuildFilter(SP);
        if (!string.IsNullOrEmpty(filter))
        {
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
        joinBuilder.OrderStr = "ORDER BY [Id] ASC";
        return joinBuilder;
    }
}