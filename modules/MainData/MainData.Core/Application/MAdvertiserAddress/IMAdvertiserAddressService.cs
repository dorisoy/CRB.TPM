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
using CRB.TPM.Mod.MainData.Core.Application.MAdvertiserAddress.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MAdvertiserAddress;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAddress;

namespace CRB.TPM.Mod.MainData.Core.Application.MAdvertiserAddress
{
    /// <summary>
    /// 广告商地点表 M_ADAddress服务
    /// </summary>
    public interface IMAdvertiserAddressService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
       Task<PagingQueryResultModel<MAdvertiserAddressEntity>> Query(MAdvertiserAddressQueryDto dto);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel> Add(MAdvertiserAddressAddDto dto);

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
        Task<IResultModel> Update(MAdvertiserAddressUpdateDto dto);

    }
}
