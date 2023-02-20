using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Mod.Admin.Core.Domain.MObject;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalDistributor.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalDistributor.Vo;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributor;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminal;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminalDistributor;
using CRB.TPM.Utils.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.MTerminalDistributor
{
    /// <summary>
    /// 终端经销商
    /// </summary>
    public class MTerminalDistributorService : IMTerminalDistributorService
    {
        private readonly IMapper _mapper;
        private readonly IExcelProvider _excelProvider;
        private readonly IMTerminalDistributorRepository _repository;
        private readonly IMObjectRepository _mObjectRepository;
        private readonly IMTerminalRepository _mTerminalRepository;
        private readonly IMDistributorRepository _mdistributorRepository;

        public MTerminalDistributorService(IMapper mapper, IMTerminalDistributorRepository repository, IMObjectRepository mObjectRepository, IExcelProvider excelProvider, IMTerminalRepository mTerminalRepository, IMDistributorRepository mdistributorRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _mObjectRepository = mObjectRepository;
            _excelProvider = excelProvider;
            _mTerminalRepository = mTerminalRepository;
            _mdistributorRepository = mdistributorRepository;
        }
        /// <summary>
        /// 终端经销商列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<PagingQueryResultModel<MTerminalDistributorQueryVo>> Query(MTerminalDistributorQueryDto dto)
        {
            return _repository.QueryPage(dto);
        }
        /// <summary>
        /// 导出客户信息列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel<ExcelModel>> Export(MTerminalDistributorQueryDto dto)
        {
            var list = await _repository.Query(dto);
            //TODO 目前不支持自定义model导出
            dto.ExportModel.Entities = (IList<MTerminalDistributorEntity>)list;
            var result = await _excelProvider.Export(dto.ExportModel);
            return result;
        }
        /// <summary>
        /// 新增终端经销商
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Transaction]
        public async Task<IResultModel> Add(MTerminalDistributorAddDto dto)
        {
            var entity = _mapper.Map<MTerminalDistributorEntity>(dto);
            var checkRes = await CheckParm(entity);
            if (checkRes.res)
            {
                return ResultModel.Failed(checkRes.errmsg);
            }
            var result = await _repository.Add(entity);
            return ResultModel.Result(result);
        }
        /// <summary>
        /// 删除终端经销商
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResultModel> Delete(Guid id)
        {
            var result = await _repository.Delete(id);
            return ResultModel.Result(result);
        }
        /// <summary>
        /// 批量删除终端经销商
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IResultModel> DeleteSelected(IEnumerable<Guid> ids)
        {
            await _repository.Delete(ids);
            return ResultModel.Success();
        }
        /// <summary>
        /// 编辑终端经销商
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _repository.Get(id);
            if (entity == null)
                return ResultModel.NotExists;

            var dto = _mapper.Map<MTerminalDistributorUpdateDto>(entity);
            return ResultModel.Success(dto);
        }
        /// <summary>
        /// 更新终端经销商
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Transaction]
        public async Task<IResultModel> Update(MTerminalDistributorUpdateDto dto)
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
            var terminal = await _mTerminalRepository.Get(entity.TerminalId);
            entity.TerminalCode = terminal?.TerminalCode;
            var result = await _repository.Update(entity);

            return ResultModel.Result(result);
        }
        /// <summary>
        /// 检查新增/更新参数校验
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<(bool res, string errmsg)> CheckParm(MTerminalDistributorEntity entity)
        {
            IList<string> errList = new List<string>();
            var terminal = await _mTerminalRepository.Get(entity.TerminalId);
            if (terminal == null)
            {
                errList.Add("终端不存在或已被删除");
            }
            var distributor = await _mdistributorRepository.Get(entity.DistributorId);
            if (distributor == null)
            {
                errList.Add("客户不存在或已被删除");
            }
            if (!errList.Any())
            {
                var resByTDID = await _repository.Find(f => f.TerminalId == entity.TerminalId && f.DistributorId == entity.DistributorId).WhereNotEmpty(entity.Id, w => w.Id != entity.Id).ToFirst();
                if (resByTDID != null)
                {
                    errList.Add("当前经销商与终端已绑定");
                }
            }
            string errmsg = errList.Any() ? string.Join("<br/>", errList) : string.Empty;
            return (errmsg.Length > 0, errmsg);
        }
    }
}
