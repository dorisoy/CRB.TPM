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
using CRB.TPM.Mod.MainData.Core.Application.MTerminalUser.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalUser;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminalUser;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalUser.Vo;
using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalDistributor.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminalDistributor;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminal;
using CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;
using CRB.TPM.Mod.Admin.Core.Infrastructure;

namespace CRB.TPM.Mod.MainData.Core.Application.MTerminalUser
{
    /// <summary>
    /// 终端业务员
    /// </summary>
    public class MTerminalUserService : IMTerminalUserService
    {
        private readonly IMapper _mapper;
        private readonly IExcelProvider _excelProvider;
        private readonly IMTerminalUserRepository _repository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMTerminalRepository _mTerminalRepository;
        private readonly IAccountResolver _accountProfileResolver;

        public MTerminalUserService(IMapper mapper, IMTerminalUserRepository repository, IExcelProvider excelProvider, IAccount account, IAccountRepository accountRepository, IMTerminalRepository mTerminalRepository, IAccountResolver accountProfileResolver)
        {
            _mapper = mapper;
            _repository = repository;
            _excelProvider = excelProvider;
            _accountRepository = accountRepository;
            _mTerminalRepository = mTerminalRepository;
            _accountProfileResolver = accountProfileResolver;
        }
        /// <summary>
        /// 终端业务员列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagingQueryResultModel<MTerminalUserQueryVo>> Query(MTerminalUserQueryDto dto)
        {
            return await _repository.QueryPage(dto);
        }
        /// <summary>
        /// 导出终端业务员
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel<ExcelModel>> Export(MTerminalUserQueryDto dto)
        {
            var list = await _repository.Query(dto);
            //TODO 目前不支持自定义model导出
            dto.ExportModel.Entities = (IList<MTerminalUserEntity>)list;
            var result = await _excelProvider.Export(dto.ExportModel);
            return result;
        }
        /// <summary>
        /// 添加终端业务员
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Transaction]
        public async Task<IResultModel> Add(MTerminalUserAddDto dto)
        {
            var entity = _mapper.Map<MTerminalUserEntity>(dto);
            entity.UserBP = "";
            var checkRes = await CheckParm(entity);
            if (checkRes.res)
            {
                return ResultModel.Failed(checkRes.errmsg);
            }
            var result = await _repository.Add(entity);
            return ResultModel.Result(result);
        }
        /// <summary>
        /// 删除终端业务员
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
        /// 编辑终端业务员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _repository.Get(id);
            if (entity == null)
                return ResultModel.NotExists;
            var dto = _mapper.Map<MTerminalUserUpdateDto>(entity);
            return ResultModel.Success(dto);
        }
        /// <summary>
        /// 跟新终端业务员
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Transaction]
        public async Task<IResultModel> Update(MTerminalUserUpdateDto dto)
        {
            var entity = await _repository.Get(dto.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(dto, entity);
            entity.UserBP = (await _accountRepository.Get(entity.AccountId))?.UserBP;
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
        public async Task<(bool res, string errmsg)> CheckParm(MTerminalUserEntity entity)
        {
            IList<string> errList = new List<string>();
            var account = await _accountRepository.Get(entity.AccountId);
            if(account == null)
            {
                errList.Add("业务人员不存在");
            }
            if (account != null && account.Deleted)
            {
                errList.Add("业务人员已被删除");
            }
            var terminal = await _mTerminalRepository.Get(entity.TerminalId);
            if (terminal == null)
            {
                errList.Add("终端不存在或已被删除");
            }
            string errmsg = errList.Any() ? string.Join("<br/>", errList) : string.Empty;
            return (errmsg.Length > 0, errmsg);
        }
    }
}
