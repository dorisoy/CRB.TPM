using System;
using System.Linq;
using System.Threading.Tasks;
//using AutoMapper;

using System.Collections.Generic;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;

using CRB.TPM.Utils.Json;
using CRB.TPM.Utils.Map;
using CRB.TPM.Mod.Logging.Core.Infrastructure;
using CRB.TPM.Mod.Logging.Core.Application.CrmData.Dto;
using CRB.TPM.Mod.Logging.Core.Application.CrmData;
using CRB.TPM.Mod.Logging.Core.Domain.CrmData;

namespace CRB.TPM.Mod.Logging.Core.Application.CrmData
{
    /// <summary>
    /// CRM客户、终端记录服务
    /// </summary>
    public interface ICrmDataService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
       Task<PagingQueryResultModel<CrmDataEntity>> Query(CrmDataQueryDto dto);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel> Add(CrmDataAddDto dto);

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
        Task<IResultModel> Update(CrmDataUpdateDto dto);

        /// <summary>
        /// 批量处理（存在则更新，不存在则新增）
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        Task<IResultModel> AddOrUpdate(List<CrmDataSyncDto> dtos);
    }
}
