
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Data.Core.SqlBuilder;
using CRB.TPM.Mod.MainData.Core.Application.MDistributorRelation.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MDistributorRelation.Vo;
using CRB.TPM.Mod.MainData.Core.Application.MProductProperty.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MProductProperty;
using CRB.TPM.Mod.Admin.Core.Domain.MObject;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributor;
using CRB.TPM.Mod.MainData.Core.Domain.MEntity;
using CRB.TPM.Mod.MainData.Core.Application.MProductProperty.Vo;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MProductPropertyRepository : RepositoryAbstract<MProductPropertyEntity>, IMProductPropertyRepository
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
    public async Task<PagingQueryResultModel<MProductPropertyQueryVo>> QueryPage(MProductPropertyQueryDto dto)
    {
        var query = this.QueryBuild(dto);
        return await query.ToPaginationResultVo<MProductPropertyQueryVo>(dto.Paging);
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<IList<MProductPropertyQueryVo>> Query(MProductPropertyQueryDto dto)
    {
        var query = this.QueryBuild(dto);
        return await query.ToList<MProductPropertyQueryVo>();
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <returns></returns>
    private IQueryable<MProductPropertyEntity> QueryBuild(MProductPropertyQueryDto dto)
    {
        var query = Find()
            .WhereIf(dto.ProductPropertiesType > 0, w => w.ProductPropertiesType == dto.ProductPropertiesType)
            .WhereNotNull(dto.Name, w => w.ProductPropertiesName.Contains(dto.Name))
            .OrderByDescending(o => o.Sort)
            .Select(s => new
            {
                Id = s.Id,
                ProductPropertiesName = s.ProductPropertiesName,
                ProductPropertiesCode = s.ProductPropertiesCode,
                ProductPropertiesType = s.ProductPropertiesType,
                Sort = s.Sort
            });
        return query;
    }
}