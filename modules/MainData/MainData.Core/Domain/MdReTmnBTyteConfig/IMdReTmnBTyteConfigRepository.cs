
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MdReTmnBTyteConfig;
using CRB.TPM.Mod.MainData.Core.Application.MdReTmnBTyteConfig.Dto;

namespace CRB.TPM.Mod.MainData.Core.Domain.MdReTmnBTyteConfig
{
    /// <summary>
    ///  终端业态关系表 M_Re_Tmn_BTyte_Config仓储
    /// </summary>
    public interface IMdReTmnBTyteConfigRepository : IRepository<MdReTmnBTyteConfigEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MdReTmnBTyteConfigEntity>> Query(MdReTmnBTyteConfigQueryDto dto);
    }
}


