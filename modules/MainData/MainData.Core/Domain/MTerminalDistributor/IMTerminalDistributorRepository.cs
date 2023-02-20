
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminalDistributor;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalDistributor.Dto;
using System.Collections;
using System.Collections.Generic;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalDistributor.Vo;
using System;

namespace CRB.TPM.Mod.MainData.Core.Domain.MTerminalDistributor
{
    /// <summary>
    /// 终端与经销商的关系信息仓储
    /// </summary>
    public interface IMTerminalDistributorRepository : IRepository<MTerminalDistributorEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MTerminalDistributorQueryVo>> QueryPage(MTerminalDistributorQueryDto dto);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IList<MTerminalDistributorQueryVo>> Query(MTerminalDistributorQueryDto dto);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<int> Delete(IEnumerable<Guid> ids);
    }
}


