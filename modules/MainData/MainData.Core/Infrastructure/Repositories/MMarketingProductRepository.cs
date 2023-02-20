
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Data.Core.SqlBuilder;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using CRB.TPM.Mod.MainData.Core.Application.MMarketingProduct.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalUser.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalUser.Vo;
using CRB.TPM.Mod.MainData.Core.Domain.MMarketingProduct;
using System.Linq;
using CRB.TPM.Mod.MainData.Core.Application.MMarketingProduct.Vo;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Defaults;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MMarketingProductRepository : RepositoryAbstract<MMarketingProductEntity>, IMMarketingProductRepository
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
    public async Task<PagingQueryResultModel<MMarketingProductQueryVo>> QueryPage(MMarketingProductQueryDto dto)
    {
        var build =  await QueryBuild(dto);
        string sqlCount = build.BuildTotalCount();
        dto.Paging.TotalCount = await base.QueryFirstOrDefault<long>(sqlCount, dto);
        string sqlPage = build.BuildQueryPage(dto.Paging.Skip, dto.Paging.Size);
        var rows = await base.Query<MMarketingProductQueryVo>(sqlPage, dto);
        var resultBody = new PagingQueryResultBody<MMarketingProductQueryVo>(rows?.ToList(), dto.Paging);
        var res = new PagingQueryResultModel<MMarketingProductQueryVo>().Success(resultBody);
        return res;
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<IList<MMarketingProductQueryVo>> Query(MMarketingProductQueryExportDto dto)
    {
        var builder = await QueryBuild(dto);
        string sql = builder.BuildQuery();
        var rows = await base.Query<MMarketingProductQueryVo>(sql, dto);
        return rows.ToList();
    }

    public async Task<SqlStrJoinBuilder> QueryBuild(MMarketingProductQueryDto dto)
    {
        var joinBuilder = SqlStrJoinBuilder.CreateInstance();
        joinBuilder.SelectStr = @$"
	        mmp.Id,
	        mmp.MarketingId,
	        mobj.MarketingCode,
	        mobj.MarketingName,
	        mmp.ProductId,
	        mp.ProductName,
	        mp.BrandName";

        joinBuilder.FromStr = $@"
            FROM M_Marketing_Product mmp
	        LEFT JOIN M_Object mobj ON mobj.MarketingId=mmp.MarketingId AND mobj.Type={(int)OrgEnumType.MarketingCenter}
	        LEFT JOIN M_Product mp ON mp.Id=mmp.ProductId";

        List<string> rootWhere = new List<string>();

        dto.MarketingIds = new List<Guid>() { dto.MarketingId };
        var filter = await dto.BuildFilter(SP, OrgEnumType.MarketingCenter, "mobj");
        if (!string.IsNullOrEmpty(filter))
        {
            //var input = IOrgIevelIdsDtoFactory.CreateInstance();
            //input.MarketingIds = new List<Guid>() { dto.MarketingId };
            //rootWhere.Add(this.BuildSqlWhereStrAuthFunc(input, OrgEnumType.MarketingCenter, "mobj").Result);
            rootWhere.Add(filter);
        }
        else
        {
            dto.MarketingId.NotEmptyIf(() => rootWhere.Add($"mmp.MarketingId='{dto.MarketingId}'"));
        }
        dto.MarketingId.NotEmptyIf(() => rootWhere.Add($"mmp.MarketingId='{dto.MarketingId}'"));
        dto.ProductId.NotEmptyIf(() => rootWhere.Add($"mmp.ProductId={dto.ProductId}"));

        joinBuilder.WhereStr = rootWhere.AndBuildStr().WhereRootBuildStr();
        joinBuilder.OrderStr = "ORDER BY [Id] ASC";
        return joinBuilder;
    }
}