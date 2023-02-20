using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Mod.Admin.Core.Domain.MOrg;
using CRB.TPM.Mod.Admin.Core.Infrastructure;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Defaults;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Vo;
using CRB.TPM.Mod.MainData.Core.Application.MTerminal.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MTerminal.Vo;
using CRB.TPM.Mod.MainData.Core.Domain.MdSaleLine;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminal;
using CRB.TPM.Utils.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.MTerminal
{
    /// <summary>
    /// 终端信息
    /// </summary>
    public class MTerminalService : IMTerminalService
    {
        private readonly IMapper _mapper;
        private readonly IMTerminalRepository _repository;
        private readonly IExcelProvider _excelProvider;
        private readonly IMOrgRepository _mOrgRepository;
        private readonly IMdSaleLineRepository _mMdSaleLineRepository;
        private readonly IAccountResolver _accountProfileResolver;

        public MTerminalService(IMapper mapper, 
            IMTerminalRepository repository, 
            IExcelProvider excelProvider, 
            IMOrgRepository mOrgRepository, 
            IMdSaleLineRepository mMdSaleLineRepository, 
            IAccountResolver accountProfileResolver)
        {
            _mapper = mapper;
            _repository = repository;
            _excelProvider = excelProvider;
            _mOrgRepository = mOrgRepository;
            _mMdSaleLineRepository = mMdSaleLineRepository;
            _accountProfileResolver = accountProfileResolver;
        }
        /// <summary>
        /// 终端信息列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagingQueryResultModel<MTerminalQueryVo>> Query(MTerminalQueryDto dto)
        {
            return await _repository.QueryPage(dto);
        }
        /// <summary>
        /// 导出终端消息列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel<ExcelModel>> Export(MTerminalQueryExportDto dto)
        {
            var list = await _repository.Query(dto);
            //TODO 目前不支持自定义model导出
            dto.ExportModel.Entities = (IList<MTerminalEntity>)list;
            var result = await _excelProvider.Export(dto.ExportModel);
            return result;
        }
        /// <summary>
        /// 新增终端信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Transaction]
        public async Task<IResultModel> Add(MTerminalAddDto dto)
        {
            var entity = _mapper.Map<MTerminalEntity>(dto);
            var checkRes = await CheckParm(entity);
            if (checkRes.res)
            {
                return ResultModel.Failed(checkRes.errmsg);
            }
            var result = await _repository.Add(entity);
            return ResultModel.Result(result);
        }
        /// <summary>
        /// 删除终端信息
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
            var isUpdateRes = await _repository.IsChangeMTerminalCode(entity.TerminalCode);
            if (!isUpdateRes.res)
            {
                return ResultModel.Failed("删除失败，" + isUpdateRes.msg);
            }
            var result = await _repository.Delete(id);
            return ResultModel.Result(result);
        }
        /// <summary>
        /// 批量删除终端消息
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
                var isUpdateRes = await _repository.IsChangeMTerminalCode(entity.TerminalCode);
                if (!isUpdateRes.res)
                {
                    msgList.Add((entity.TerminalCode + "删除失败，" + isUpdateRes.msg));
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
        /// <summary>
        /// 编辑终端信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _repository.Get(id);
            if (entity == null)
                return ResultModel.NotExists;

            var dto = _mapper.Map<MTerminalUpdateDto>(entity);
            return ResultModel.Success(dto);
        }
        /// <summary>
        /// 更新终端信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Transaction]
        public async Task<IResultModel> Update(MTerminalUpdateDto dto)
        {
            var entity = await _repository.Get(dto.Id);
            if (entity == null)
                return ResultModel.NotExists;
            //终端编码存在被使用，就不能编辑终端编码
            var isUpdateRes = await _repository.IsChangeMTerminalCode(entity.TerminalCode);
            if (!isUpdateRes.res)
            {
                return ResultModel.Failed("更新失败，" + isUpdateRes.msg);
            }
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
        public async Task<(bool res, string errmsg)> CheckParm(MTerminalEntity entity)
        {
            IList<string> errList = new List<string>();
            var station = await _mOrgRepository.Find(f => f.Id == entity.StationId && f.Type == (int)OrgEnumType.Station).ToFirst();
            if (station == null)
            {
                errList.Add("工作站不存在");
            }
            if (station != null && station.Enabled == 0)
            {
                errList.Add("工作站已失效");
            }
            var resByTerminalCode = await _repository.Find(f => f.TerminalCode == entity.TerminalCode).WhereNotEmpty(entity.Id, w => w.Id != entity.Id).ToFirst();
            if (resByTerminalCode != null)
            {
                errList.Add("终端编码已存在");
            }
            //业务线需做校验，需要判断状态
            var saleLine = await _mMdSaleLineRepository.Find(f => f.LineCD == entity.SaleLine).ToFirst();
            if (saleLine == null)
            {
                errList.Add("业务线不存在");
            }
            if (saleLine != null && saleLine.Status == 0)
            {
                errList.Add("业务线未启用");
            }
            string errmsg = errList.Any() ? string.Join("<br/>", errList) : string.Empty;
            return (errmsg.Length > 0, errmsg);
        }
        /// <summary>
        /// 终端Select下拉接口
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagingQueryResultModel<MTerminalSelectVo>> Select(MTerminalSelectDto dto)
        {
            var res = await _repository.Select(dto);
            return res;
        }
    }
}
