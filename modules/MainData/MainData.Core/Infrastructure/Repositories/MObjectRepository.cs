
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Data.Core.SqlBuilder;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Vo;
using CRB.TPM.Mod.Admin.Core.Domain.MObject;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Defaults;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributor;
using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MObjectRepository : RepositoryAbstract<MObjectEntity>, IMObjectRepository
{
    private readonly static Dictionary<OrgEnumType, string> map;
    static MObjectRepository()
    {
        map = new Dictionary<OrgEnumType, string>()
        {
            { OrgEnumType.HeadOffice, "obj.HeadOffice" },
            { OrgEnumType.BD, "obj.Division" },
            { OrgEnumType.MarketingCenter, "obj.Marketing" },
            { OrgEnumType.SaleRegion, "obj.BigArea" },
            { OrgEnumType.Department, "obj.Office" },
            { OrgEnumType.Station, "obj.Station" },
        };
    }

    /// <summary>
    /// 根据各层级id获取指定层级数据
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<PagingQueryResultModel<OrgSelectVo>> QueryByLevel(OrgSelectDto dto)
    {
        if (!map.ContainsKey((OrgEnumType)dto.Level))
        {
            return PagingQueryResultModel<OrgSelectVo>.SuccessEmpty();
        }

        var joinBuilder = SqlStrJoinBuilder.CreateInstance();
        joinBuilder.SelectStr = $"{map[(OrgEnumType)dto.Level]}Name [Label],{map[(OrgEnumType)dto.Level]}Id [Value]";
        joinBuilder.FromStr = @$"FROM M_Object obj";

        IList<string> rootWhere = new List<string>();
        IList<string> orgWhere = new List<string>();

        rootWhere.Add($"obj.Enabled=1");
        rootWhere.Add($"obj.Type={dto.Level}");
        dto.Level1Ids.NotNullAndEmptyIf(() => orgWhere.Add($"obj.HeadOfficeId IN @Level1Ids"));
        dto.Level2Ids.NotNullAndEmptyIf(() => orgWhere.Add($"obj.DivisionId IN @Level2Ids"));
        dto.Level3Ids.NotNullAndEmptyIf(() => orgWhere.Add($"obj.MarketingId IN @Level3Ids"));
        dto.Level4Ids.NotNullAndEmptyIf(() => orgWhere.Add($"obj.BigAreaId IN @Level4Ids"));
        dto.Level5Ids.NotNullAndEmptyIf(() => orgWhere.Add($"obj.OfficeId IN @Level5Ids"));
        dto.Level6Ids.NotNullAndEmptyIf(() => orgWhere.Add($"obj.StationId IN @Level6Ids"));
        rootWhere.Add(orgWhere.OrBuildStr(true));
        dto.Name.NotNullIf(() =>
        {
            var likeSql = $"{map[(OrgEnumType)dto.Level]}Name LIKE '%{dto.Name}%'";
            rootWhere.Add(likeSql);
        });

        joinBuilder.WhereStr = rootWhere.AndBuildStr().WhereRootBuildStr();
        joinBuilder.OrderStr = "ORDER BY [Value] ASC";

        string sqlCount = joinBuilder.BuildTotalCount();
        dto.Paging.TotalCount = await base.QueryFirstOrDefault<long>(sqlCount, dto);

        var sql = joinBuilder.BuildQueryPage(dto.Paging.Skip, dto.Paging.Size);
        var rows = await base.Query<OrgSelectVo>(sql, dto);
        var resultBody = new PagingQueryResultBody<OrgSelectVo>(rows?.ToList(), dto.Paging);
        return new PagingQueryResultModel<OrgSelectVo>().Success(resultBody);
    }
    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<int> Deletes(IEnumerable<Guid> ids)
    {
        int result = 0;
        foreach (var id in ids)
        {
            if (await Delete(id)) result++;
        }
        return result;
    }
    /// <summary>
    /// 校验指定层级组织ID是否有权限
    /// </summary>
    /// <param name="orgIds"></param>
    /// <param name="orgType"></param>
    /// <param name="whereAuthSqlStr"></param>
    /// <returns></returns>
    public async Task<(bool isAuth, IList<string> noAuthCode)> CheckOrgIdsAuth(IList<Guid> orgIds, OrgEnumType orgType, string whereAuthSqlStr)
    {
        if (!orgIds.NotNullAndEmpty() || !map.ContainsKey(orgType))
        {
            return (false, null);
        }
        string filed = map[orgType].Replace("obj.", "mobj.");
        StringBuilder sql = new StringBuilder();
        sql.Append($"SELECT * FROM dbo.M_Object mobj WHERE ");
        if (orgIds.Count == 1)
        {
            sql.Append($"{filed}Id='{orgIds[0]}'");
        }
        else
        {
            sql.Append($"{filed}Id IN ({string.Join(',', orgIds.Select(s => $"'{s}'"))})");
        }
        sql.Append(" AND " + whereAuthSqlStr);
        var rows = await Query<MObjectEntity>(sql.ToString());
        if(rows.Count() == orgIds.Count)
        {
            return (true, null);
        }
        var mobjType = typeof(MObjectEntity);
        var authOrgIds = rows.Select(s => s.Id).ToList();
        var notAuthOrgIds = orgIds.Except(authOrgIds).ToList();
        var notAuthOrg = await Find(w => notAuthOrgIds.Contains(w.Id)).ToList();
        var propName = map[orgType].Replace("obj.", "");
        var noAuthOrgCode = notAuthOrg.Select(s => mobjType.GetProperty($"{propName}Code").GetValue(s).ToString()).ToList();
        return (false, noAuthOrgCode);
    }
}