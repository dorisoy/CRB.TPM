
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MProductProperty;
using CRB.TPM.Mod.MainData.Core.Application.MProductProperty.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MProductProperty.Vo;
using System.Collections.Generic;
using System;

namespace CRB.TPM.Mod.MainData.Core.Domain.MProductProperty
{
    /// <summary>
    /// 产品属性仓储
    /// </summary>
    public interface IMProductPropertyRepository : IRepository<MProductPropertyEntity>
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
        Task<PagingQueryResultModel<MProductPropertyQueryVo>> QueryPage(MProductPropertyQueryDto dto);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IList<MProductPropertyQueryVo>> Query(MProductPropertyQueryDto dto);
    }
}


