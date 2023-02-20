
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MEntity;
using CRB.TPM.Mod.MainData.Core.Application.MEntity.Dto;
using System.Collections.Generic;
using System;

namespace CRB.TPM.Mod.MainData.Core.Domain.MEntity
{
    /// <summary>
    /// 业务实体仓储
    /// </summary>
    public interface IMEntityRepository : IRepository<MEntityEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MEntityEntity>> Query(MEntityQueryDto dto);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<int> Delete(IEnumerable<Guid> ids);
    }
}


