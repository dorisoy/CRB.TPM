
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MdKaBigSysNameConf;
using CRB.TPM.Mod.MainData.Core.Application.MdKaBigSysNameConf.Dto;

namespace CRB.TPM.Mod.MainData.Core.Domain.MdKaBigSysNameConf
{
    /// <summary>
    /// KA大系统 M_KABigSysNameConf仓储
    /// </summary>
    public interface IMdKaBigSysNameConfRepository : IRepository<MdKaBigSysNameConfEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MdKaBigSysNameConfEntity>> Query(MdKaBigSysNameConfQueryDto dto);
    }
}


