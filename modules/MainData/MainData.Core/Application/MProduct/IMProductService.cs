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
using CRB.TPM.Mod.MainData.Core.Application.MProduct.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MProduct;
using CRB.TPM.Mod.MainData.Core.Domain.MProduct;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Mod.MainData.Core.Application.MProduct.Vo;

namespace CRB.TPM.Mod.MainData.Core.Application.MProduct
{
    /// <summary>
    /// 服务
    /// </summary>
    public interface IMProductService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MProductQueryVo>> Query(MProductQueryDto dto);

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel<ExcelModel>> Export(MProductQueryDtoExportDto dto);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel> Add(MProductAddDto dto);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        Task<IResultModel> Delete(Guid id);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IResultModel> DeleteSelected(IEnumerable<Guid> ids);

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
        Task<IResultModel> Update(MProductUpdateDto dto);
        /// <summary>
        /// 产品Select下拉接口
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<ProductSelectVo>> Select(ProductSelectDto dto);
    }
}
