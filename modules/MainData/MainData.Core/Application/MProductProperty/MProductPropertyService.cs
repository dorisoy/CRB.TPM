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
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MProductProperty.Vo;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Mod.MainData.Core.Application.MEntity.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributor;
using CRB.TPM.Mod.MainData.Core.Domain.MProduct;
using CRB.TPM.Mod.MainData.Core.Infrastructure.Enums;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Vo;
using CRB.TPM.Data.Abstractions.Pagination;
using CRB.TPM.Mod.MainData.Core.Application.MMarketingProduct.Vo;
using CRB.TPM.Mod.Admin.Core.Infrastructure;

namespace CRB.TPM.Mod.MainData.Core.Application.MProductProperty
{
    /// <summary>
    /// 产品属性
    /// </summary>
    public class MProductPropertyService : IMProductPropertyService
    {
        private readonly IExcelProvider _excelProvider;
        private readonly IMapper _mapper;
        private readonly IMProductPropertyRepository _repository;

        public MProductPropertyService(IMapper mapper, IMProductPropertyRepository repository, IExcelProvider excelProvider)
        {
            _mapper = mapper;
            _repository = repository;
            _excelProvider = excelProvider;
        }
        /// <summary>
        /// 产品属性列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<PagingQueryResultModel<MProductPropertyQueryVo>> Query(MProductPropertyQueryDto dto)
        {
            return _repository.QueryPage(dto);
        }
        /// <summary>
        /// 产品属性列表
        /// </summary>
        /// <returns></returns>
        public PagingQueryResultModel<ProductPropertiesTypeSelectVo> TypeSelect()
        {
            var rows = ProductPropertiesTypeEnum.Grade.ToResult().Select(s => new ProductPropertiesTypeSelectVo()
            {
                Label = s.Label,
                Value = (int)s.Value
            }).ToList();
            var resultBody = new PagingQueryResultBody<ProductPropertiesTypeSelectVo>(rows, rows.Count);
            return new PagingQueryResultModel<ProductPropertiesTypeSelectVo>().Success(resultBody);
        }
        /// <summary>
        /// 导出产品属性列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel<ExcelModel>> Export(MProductPropertyQueryExportDto dto)
        {
            var list = await _repository.Query(dto);
            //TODO 目前不支持自定义model导出
            dto.ExportModel.Entities = _mapper.Map<List<MProductPropertyEntity>>(list);
            var result = await _excelProvider.Export(dto.ExportModel);
            return result;
        }
        /// <summary>
        /// 新增产品属性
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Transaction]
        public async Task<IResultModel> Add(MProductPropertyAddDto dto)
        {
            var entity = _mapper.Map<MProductPropertyEntity>(dto);
            var checkRes = await CheckParm(entity);
            if (checkRes.res)
            {
                return ResultModel.Failed(checkRes.errmsg);
            }
            var result = await _repository.Add(entity);
            return ResultModel.Result(result);
        }
        /// <summary>
        /// 删除产品属性
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResultModel> Delete(Guid id)
        {
            var result = await _repository.Delete(id);
            return ResultModel.Result(result);
        }
        /// <summary>
        /// 编辑产品属性
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _repository.Get(id);
            if (entity == null)
                return ResultModel.NotExists;

            var dto = _mapper.Map<MProductPropertyUpdateDto>(entity);
            return ResultModel.Success(dto);
        }
        /// <summary>
        /// 批量删除产品属性信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IResultModel> DeleteSelected(IEnumerable<Guid> ids)
        {
            await _repository.Delete(ids);
            return ResultModel.Success();
        }
        /// <summary>
        /// 更新产品属性
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Transaction]
        public async Task<IResultModel> Update(MProductPropertyUpdateDto dto)
        {
            var entity = await _repository.Get(dto.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(dto, entity);

            var checkRes = await CheckParm(entity);
            if (checkRes.res)
            {
                return ResultModel.Failed(checkRes.errmsg);
            }

            var result = await _repository.Update(entity);

            return ResultModel.Result(result);
        }
        /// <summary>
        /// 检查新增/更新参数校验
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<(bool res, string errmsg)> CheckParm(MProductPropertyEntity entity)
        {
            IList<string> errList = new List<string>();
            var resByDistributorCode = (await _repository.Find(f => f.ProductPropertiesCode.Equals(entity.ProductPropertiesCode, StringComparison.OrdinalIgnoreCase)).WhereNotEmpty(entity.Id, w => w.Id != entity.Id).ToFirst());
            if (resByDistributorCode != null)
            {
                errList.Add("产品属性编码已存在");
            }
            if (!Enum.IsDefined(typeof(ProductPropertiesTypeEnum), entity.ProductPropertiesType))
            {
                errList.Add("产品属性类型不存在");
            }
            string errmsg = errList.Any() ? string.Join("<br/>", errList) : string.Empty;
            return (errmsg.Length > 0, errmsg);
        }
    }
}
