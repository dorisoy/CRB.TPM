
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.MainData.Core.Application.MProduct.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MProductProperty.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MProductProperty.Vo;
using CRB.TPM.Mod.MainData.Core.Domain.MProduct;
using CRB.TPM.Mod.MainData.Core.Domain.MProductProperty;
using CRB.TPM.Mod.MainData.Core.Application.MProduct.Vo;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Vo;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributorRelation;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MProductRepository : RepositoryAbstract<MProductEntity>, IMProductRepository
{
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
    public async Task<PagingQueryResultModel<MProductQueryVo>> QueryPage(MProductQueryDto dto)
    {
        var query = this.QueryBuild(dto);
        return await query.ToPaginationResultVo<MProductQueryVo>(dto.Paging);
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<IList<MProductPropertyQueryVo>> Query(MProductQueryDto dto)
    {
        var query = this.QueryBuild(dto);
        return await query.ToList<MProductPropertyQueryVo>();
    }

    /// <summary>
    /// Select下拉接口
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<PagingQueryResultModel<ProductSelectVo>> Select(ProductSelectDto dto)
    {
        var query = Find(f => f.Enabled == 1);
        query = query.WhereNotNull(dto.Name, w => w.ProductName.Contains(dto.Name) || w.ProductCode.Contains(dto.Name))
                     .WhereIf(dto.ProductType > 0 ,w => w.ProductType == dto.ProductType);
        if (dto.Ids.NotNullAndEmpty())
        {
            dto.Page.Size = int.MaxValue;
            var res = query.Where(w => dto.Ids.Contains(w.Id)).Select(s => new { Label = s.ProductName, Value = s.Id }).ToPaginationResultVo<ProductSelectVo>(dto.Paging);

        }
        return await query.Select(s => new { Label = s.ProductName, Value = s.Id }).ToPaginationResultVo<ProductSelectVo>(dto.Paging);
    }

    /// <summary>
    /// 是否可以变更产品编码
    /// </summary>
    /// <returns>true=可以更新，false=不能更新</returns>
    public async Task<(bool res, string msg)> IsChangeProductId(Guid id)
    {
        //经销分销绑定，就不能修改编码
        string sql = @$"SELECT COUNT(1) FROM M_Marketing_Product WHERE ProductId='{id}'";
        var res = await base.QueryFirstOrDefault<long>(sql);
        return (res == 0, "营销产品关系已绑定");
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <returns></returns>
    private IQueryable<MProductEntity> QueryBuild(MProductQueryDto dto)
    {
        var query = Find()
            .WhereIf(dto.ProductType > 0, w => w.ProductType == dto.ProductType)
            .WhereNotNull(dto.Name, w => w.ProductCode.Contains(dto.Name) || w.ProductName.Contains(dto.Name))
            .OrderByDescending(o => o.Sort);
        return query;
    }
}