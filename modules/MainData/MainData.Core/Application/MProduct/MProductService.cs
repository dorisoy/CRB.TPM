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
using CRB.TPM.Mod.MainData.Core.Application.MProduct.Vo;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Mod.MainData.Core.Application.MProductProperty.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MProductProperty;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributor;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Vo;

namespace CRB.TPM.Mod.MainData.Core.Application.MProduct
{
    /// <summary>
    /// 产品
    /// </summary>
    public class MProductService : IMProductService
    {
        private readonly IExcelProvider _excelProvider;
        private readonly IMapper _mapper;
        private readonly IMProductRepository _repository;
        public MProductService(IMapper mapper, IMProductRepository repository, IExcelProvider excelProvider)
        {
            _mapper = mapper;
            _repository = repository;
            _excelProvider = excelProvider;
        }
        /// <summary>
        /// 产品列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<PagingQueryResultModel<MProductQueryVo>> Query(MProductQueryDto dto)
        {
            return _repository.QueryPage(dto);
        }
        /// <summary>
        /// 导出产品列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel<ExcelModel>> Export(MProductQueryDtoExportDto dto)
        {
            var list = await _repository.Query(dto);
            //TODO 目前不支持自定义model导出
            dto.ExportModel.Entities = _mapper.Map<List<MProductEntity>>(list);
            var result = await _excelProvider.Export(dto.ExportModel);
            return result;
        }
        /// <summary>
        /// 新增产品
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Transaction]
        public async Task<IResultModel> Add(MProductAddDto dto)
        {
            var entity = _mapper.Map<MProductEntity>(dto);
            var checkRes = await CheckParm(entity);
            if (checkRes.res)
            {
                return ResultModel.Failed(checkRes.errmsg);
            }
            var result = await _repository.Add(entity);
            return ResultModel.Result(result);
        }
        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResultModel> Delete(Guid id)
        {
            var entity = await _repository.Get(id);
            if (entity == null)
            {
                return ResultModel.Success();
            }
            var isUpdateRes = await _repository.IsChangeProductId(id);
            if (!isUpdateRes.res)
            {
                return ResultModel.Failed("删除失败，" + isUpdateRes.msg);
            }
            var result = await _repository.Delete(id);
            return ResultModel.Result(result);
        }
        /// <summary>
        /// 批量删除产品信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IResultModel> DeleteSelected(IEnumerable<Guid> ids)
        {
            var entityList = await _repository.Find(f => ids.Contains(f.Id)).ToList();
            List<string> msgList = new List<string>();
            List<Guid> delList = new List<Guid>();
            foreach (var entity in entityList)
            {
                var isChangeRes = await _repository.IsChangeProductId(entity.Id);
                if (!isChangeRes.res)
                {
                    msgList.Add((entity.ProductCode + "删除失败，" + isChangeRes.msg));
                }
                else
                {
                    delList.Add(entity.Id);
                }
            }
            await _repository.Delete(delList);
            if (msgList.Count > 0)
            {
                return ResultModel.Failed(String.Join("<br/>", msgList));
            }
            return ResultModel.Success();
        }
        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _repository.Get(id);
            if (entity == null)
                return ResultModel.NotExists;

            var dto = _mapper.Map<MProductUpdateDto>(entity);
            return ResultModel.Success(dto);
        }
        /// <summary>
        /// 跟新产品
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Transaction]
        public async Task<IResultModel> Update(MProductUpdateDto dto)
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
        public async Task<(bool res, string errmsg)> CheckParm(MProductEntity entity)
        {
            IList<string> errList = new List<string>();
            var resByDistributorCode = (await _repository.Find(f => f.ProductCode.Equals(entity.ProductCode, StringComparison.OrdinalIgnoreCase) && f.CharacterCode.Equals(entity.CharacterCode, StringComparison.OrdinalIgnoreCase)).WhereNotEmpty(entity.Id, w => w.Id != entity.Id).ToFirst());
            if (resByDistributorCode != null)
            {
                errList.Add("产品已存在");
            }
            string errmsg = errList.Any() ? string.Join("<br/>", errList) : string.Empty;
            return (errmsg.Length > 0, errmsg);
        }
        /// <summary>
        /// 客户信息分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagingQueryResultModel<ProductSelectVo>> Select(ProductSelectDto dto)
        {
            var res = await _repository.Select(dto);
            return res;
        }
    }
}
