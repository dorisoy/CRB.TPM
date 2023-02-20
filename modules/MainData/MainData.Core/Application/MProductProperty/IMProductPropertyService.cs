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
using CRB.TPM.Mod.MainData.Core.Application.MProductProperty.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MProductProperty;
using CRB.TPM.Mod.MainData.Core.Domain.MProductProperty;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Mod.MainData.Core.Application.MProductProperty.Vo;

namespace CRB.TPM.Mod.MainData.Core.Application.MProductProperty
{
    /// <summary>
    /// 产品属性服务
    /// </summary>
    public interface IMProductPropertyService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MProductPropertyQueryVo>> Query(MProductPropertyQueryDto dto);

        /// <summary>
        /// 产品属性列表
        /// </summary>
        /// <returns></returns>
        public PagingQueryResultModel<ProductPropertiesTypeSelectVo> TypeSelect();

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel> Add(MProductPropertyAddDto dto);

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
        Task<IResultModel> Update(MProductPropertyUpdateDto dto);

        /// <summary>
        /// 导出产品属性列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel<ExcelModel>> Export(MProductPropertyQueryExportDto dto);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IResultModel> DeleteSelected(IEnumerable<Guid> ids);
    }
}
