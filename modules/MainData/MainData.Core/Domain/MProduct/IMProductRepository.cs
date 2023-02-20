
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MProduct;
using CRB.TPM.Mod.MainData.Core.Application.MProduct.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MProduct.Vo;
using System.Collections.Generic;
using System;
using CRB.TPM.Mod.MainData.Core.Application.MProductProperty.Vo;

namespace CRB.TPM.Mod.MainData.Core.Domain.MProduct
{
    /// <summary>
    /// 仓储
    /// </summary>
    public interface IMProductRepository : IRepository<MProductEntity>
    {
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<int> Delete(IEnumerable<Guid> ids);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MProductQueryVo>> QueryPage(MProductQueryDto dto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IList<MProductPropertyQueryVo>> Query(MProductQueryDto dto);
        /// <summary>
        /// 是否可以变更产品编码
        /// </summary>
        /// <returns>true=可以更新，false=不能更新</returns>
        Task<(bool res, string msg)> IsChangeProductId(Guid id);
        /// <summary>
        /// Select下拉接口
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<ProductSelectVo>> Select(ProductSelectDto dto);
    }
}


