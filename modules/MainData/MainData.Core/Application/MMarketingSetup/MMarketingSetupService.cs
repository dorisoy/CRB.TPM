using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Mod.Admin.Core.Domain.MOrg;
using CRB.TPM.Mod.Admin.Core.Infrastructure;
using CRB.TPM.Mod.MainData.Core.Application.MMarketingSetup.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MMarketingSetup.Vo;
using CRB.TPM.Mod.MainData.Core.Domain.MMarketingSetup;
using CRB.TPM.Utils.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.MMarketingSetup
{
    /// <summary>
    /// 营销中心配置
    /// </summary>
    public class MMarketingSetupService : IMMarketingSetupService
    {
        private readonly IMapper _mapper;
        private readonly IExcelProvider _excelProvider;
        private readonly IMMarketingSetupRepository _repository;
        private readonly IMOrgRepository _mOrgRepository;
        public MMarketingSetupService(IMapper mapper, IMMarketingSetupRepository repository, IExcelProvider excelProvider, IMOrgRepository mOrgRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _excelProvider = excelProvider;
            _mOrgRepository = mOrgRepository;
        }
        /// <summary>
        /// 营销中心配置列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<PagingQueryResultModel<MMarketingSetupQueryVo>> Query(MMarketingSetupQueryDto dto)
        {
            return _repository.QueryPage(dto);
        }
        /// <summary>
        /// 导出客户信息列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel<ExcelModel>> Export(MMarketingSetupExportDto dto)
        {
            var list = await _repository.Query(dto);
            //TODO 目前不支持自定义model导出
            dto.ExportModel.Entities = (IList<MMarketingSetupEntity>)list;
            var result = await _excelProvider.Export(dto.ExportModel);
            return result;
        }
        /// <summary>
        /// 新增营销中心配置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Transaction]
        public async Task<IResultModel> Add(MMarketingSetupAddDto dto)
        {
            var entity = _mapper.Map<MMarketingSetupEntity>(dto);
            var checkRes = await CheckParm(entity);
            if (checkRes.res)
            {
                return ResultModel.Failed(checkRes.errmsg);
            }

            var result = await _repository.Add(entity);
            return ResultModel.Result(result);
        }
        /// <summary>
        /// 新增营销中心配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResultModel> Delete(Guid id)
        {
            var result = await _repository.Delete(id);
            return ResultModel.Result(result);
        }
        /// <summary>
        /// 批量删除营销中心配置
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IResultModel> DeleteSelected(IEnumerable<Guid> ids)
        {
            await _repository.Delete(ids);
            return ResultModel.Success();
        }
        /// <summary>
        /// 编辑营销中心配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _repository.Get(id);
            if (entity == null)
                return ResultModel.NotExists;

            var dto = _mapper.Map<MMarketingSetupUpdateDto>(entity);
            return ResultModel.Success(dto);
        }
        /// <summary>
        /// 更新营销中心配置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Transaction]
        public async Task<IResultModel> Update(MMarketingSetupUpdateDto dto)
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
        public async Task<(bool res, string errmsg)> CheckParm(MMarketingSetupEntity entity)
        {
            IList<string> errList = new List<string>();
            var resByStationId = await _mOrgRepository.Get(f => f.Id == entity.MarketingId);
            if (resByStationId == null)
            {
                errList.Add("配置的营销中心不存");
            }
            else if (resByStationId != null && resByStationId.Deleted)
            {
                errList.Add("配置的营销中心已被删除");
            }
            else if(resByStationId.Enabled == 0)
            {
                errList.Add("配置的营销中心已失效");
            }
            var resByMarketingId = await _repository.Find(f => f.MarketingId == entity.MarketingId).WhereNotEmpty(entity.Id, w => w.Id != entity.Id).ToFirst();
            if (resByMarketingId != null)
            {
                errList.Add("当前营销中心已配置");
            }
            
            string errmsg = errList.Any() ? string.Join("<br/>", errList) : string.Empty;
            return (errmsg.Length > 0, errmsg);
        }
    }
}
