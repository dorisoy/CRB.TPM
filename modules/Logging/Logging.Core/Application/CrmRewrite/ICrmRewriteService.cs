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
using CRB.TPM.Mod.Logging.Core.Application.CrmRewrite.Dto;
using CRB.TPM.Mod.Logging.Core.Application.CrmRewrite;
using CRB.TPM.Mod.Logging.Core.Domain.CrmRewrite;

namespace CRB.TPM.Mod.Logging.Core.Application.CrmRewrite
{
    /// <summary>
    /// 返写CRM记录服务
    /// </summary>
    public interface ICrmRewriteService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
       Task<PagingQueryResultModel<CrmRewriteEntity>> Query(CrmRewriteQueryDto dto);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel> Add(CrmRewriteAddDto dto);

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
        Task<IResultModel> Update(CrmRewriteUpdateDto dto);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        Task<IResultModel> BulkAdd(List<CrmRewriteEntity> dtos);
    }
}
