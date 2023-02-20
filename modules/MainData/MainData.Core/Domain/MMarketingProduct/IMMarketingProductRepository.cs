
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MMarketingProduct;
using CRB.TPM.Mod.MainData.Core.Application.MMarketingProduct.Dto;
using System.Collections.Generic;
using System;
using CRB.TPM.Mod.MainData.Core.Application.MMarketingProduct.Vo;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Defaults;

namespace CRB.TPM.Mod.MainData.Core.Domain.MMarketingProduct
{
    /// <summary>
    /// 营销中心产品仓储
    /// </summary>
    public interface IMMarketingProductRepository : IRepository<MMarketingProductEntity>
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
        Task<PagingQueryResultModel<MMarketingProductQueryVo>> QueryPage(MMarketingProductQueryDto dto);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IList<MMarketingProductQueryVo>> Query(MMarketingProductQueryExportDto dto);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<int> Delete(IEnumerable<Guid> ids);
    }
}


