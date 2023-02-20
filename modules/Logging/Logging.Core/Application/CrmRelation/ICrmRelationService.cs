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
using CRB.TPM.Mod.Logging.Core.Application.CrmRelation.Dto;
using CRB.TPM.Mod.Logging.Core.Application.CrmRelation;
using CRB.TPM.Mod.Logging.Core.Domain.CrmRelation;

namespace CRB.TPM.Mod.Logging.Core.Application.CrmRelation
{
    /// <summary>
    /// CRM的关系变动记录表服务
    /// </summary>
    public interface ICrmRelationService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
       Task<PagingQueryResultModel<CrmRelationEntity>> Query(CrmRelationQueryDto dto);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel> Add(CrmRelationAddDto dto);

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
        Task<IResultModel> Update(CrmRelationUpdateDto dto);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        Task<IResultModel> BulkAdd(List<CrmRelationEntity> dtos);
    }
}
