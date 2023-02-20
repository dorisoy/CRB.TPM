using System;
using System.Linq;
using System.Threading.Tasks;
//using AutoMapper;

using System.Collections.Generic;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;

using CRB.TPM.Utils.Json;
using CRB.TPM.Utils.Map;
using CRB.TPM.Mod.MainData.Core.Infrastructure;
using CRB.TPM.Mod.MainData.Core.Application.MdReTmnBTyteConfig.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MdReTmnBTyteConfig;
using CRB.TPM.Mod.MainData.Core.Domain.MdReTmnBTyteConfig;

namespace CRB.TPM.Mod.MainData.Core.Application.MdReTmnBTyteConfig
{
    /// <summary>
    ///  终端业态关系表 M_Re_Tmn_BTyte_Config服务
    /// </summary>
    public interface IMdReTmnBTyteConfigService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
       Task<PagingQueryResultModel<MdReTmnBTyteConfigEntity>> Query(MdReTmnBTyteConfigQueryDto dto);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel> Add(MdReTmnBTyteConfigAddDto dto);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        Task<IResultModel> Delete(Guid id);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Edit(Guid id);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel> Update(MdReTmnBTyteConfigUpdateDto dto);

    }
}
