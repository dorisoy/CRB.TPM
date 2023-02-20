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
using CRB.TPM.Mod.MainData.Core.Application.MMarketingProduct.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MMarketingProduct;
using CRB.TPM.Mod.MainData.Core.Domain.MMarketingProduct;
using CRB.TPM.Mod.MainData.Core.Application.MMarketingProduct.Vo;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalUser.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminalUser;
using CRB.TPM.Mod.MainData.Core.Domain.MProduct;
using CRB.TPM.Mod.Admin.Core.Domain.MObject;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using CRB.TPM.Mod.Admin.Core.Infrastructure;

namespace CRB.TPM.Mod.MainData.Core.Application.MMarketingProduct
{
    /// <summary>
    /// 营销产品分配
    /// </summary>
    public class MMarketingProductService : IMMarketingProductService
    {
        private readonly IExcelProvider _excelProvider;
        private readonly IMapper _mapper;
        private readonly IMMarketingProductRepository _repository;
        private readonly IMProductRepository _mproductRepository;
        private readonly IMObjectRepository _mObjectRepository;
        public MMarketingProductService(IMapper mapper, 
            IMMarketingProductRepository repository, 
            IExcelProvider excelProvider, 
            IMProductRepository mproductRepository,
            IMObjectRepository mObjectRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _excelProvider = excelProvider;
            _mproductRepository = mproductRepository;
            _mObjectRepository = mObjectRepository;
        }

        /// <summary>
        /// 营销产品分配列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagingQueryResultModel<MMarketingProductQueryVo>> Query(MMarketingProductQueryDto dto)
        {
            return await _repository.QueryPage(dto);
        }
        /// <summary>
        /// 导出营销产品分配
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel<ExcelModel>> Export(MMarketingProductQueryExportDto dto)
        {
            var list = await _repository.Query(dto);
            //TODO 目前不支持自定义model导出
            dto.ExportModel.Entities = _mapper.Map<List<MMarketingProductEntity>>(list); ;
            var result = await _excelProvider.Export(dto.ExportModel);
            return result;
        }
        /// <summary>
        /// 新增营销产品分配
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Transaction]
        public async Task<IResultModel> Add(MMarketingProductAddDto dto)
        {
            var entity = _mapper.Map<MMarketingProductEntity>(dto);
            var checkRes = await CheckParm(entity);
            if (checkRes.res)
            {
                return ResultModel.Failed(checkRes.errmsg);
            }
            var result = await _repository.Add(entity);
            return ResultModel.Result(result);
        }
        /// <summary>
        /// 删除营销产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResultModel> Delete(Guid id)
        {
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
            await _repository.Delete(ids);
            return ResultModel.Success();
        }
        /// <summary>
        /// 编辑营销产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _repository.Get(id);
            if (entity == null)
                return ResultModel.NotExists;

            var dto = _mapper.Map<MMarketingProductUpdateDto>(entity);
            return ResultModel.Success(dto);
        }
        /// <summary>
        /// 更新营销产品
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Transaction]
        public async Task<IResultModel> Update(MMarketingProductUpdateDto dto)
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
        public async Task<(bool res, string errmsg)> CheckParm(MMarketingProductEntity entity)
        {
            IList<string> errList = new List<string>();
            var product = await _mproductRepository.Get(entity.ProductId);
            if (product == null)
            {
                errList.Add("产品不存在");
            }
            var marketing = await _mObjectRepository.Find(f => f.MarketingId == entity.MarketingId && f.Type == (int)OrgEnumType.MarketingCenter).ToFirst();
            if (marketing == null)
            {
                errList.Add("营销中心不存在");
            }
            var bindEntity = await _repository.Find(f => f.ProductId == entity.ProductId && f.MarketingId == entity.MarketingId).WhereNotEmpty(entity.Id, w => w.Id != entity.Id).ToFirst();
            if(bindEntity != null)
            {
                errList.Add("新增营销中心产品已绑定");
            }
            string errmsg = errList.Any() ? string.Join("<br/>", errList) : string.Empty;
            return (errmsg.Length > 0, errmsg);
        }
    }
}
