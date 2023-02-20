using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRB.TPM.Config.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Excel.Abstractions.Export;
using CRB.TPM.Mod.Module.Core.Application.Account.Dto;
using CRB.TPM.Mod.Module.Core.Domain.Account;
using CRB.TPM.Utils.Map;

namespace CRB.TPM.Mod.Module.Core.Application.Account;

public class AccountService : IAccountService
{
    private readonly IMapper _mapper;
    private readonly IAccountRepository _repository;
    private readonly IConfigProvider _configProvider;
    private readonly IExcelProvider _excelProvider;


    public AccountService(IMapper mapper, IAccountRepository repository,
        IExcelProvider excelProvider,
        IConfigProvider configProvider)
    {
        _mapper = mapper;
        _repository = repository;
        _configProvider = configProvider;
        _excelProvider = excelProvider;
    }

    public Task<IList<AccountEntity>> Query()
    {
        return _repository.Find()?.ToList();
    }

    public async Task<IResultModel> Add(AccountAddDto dto)
    {
        var account = _mapper.Map<AccountEntity>(dto);
        if (account.Password.IsNull())
        {
            account.Password = "123456";
        }
        account.Password = "42343dfsfsdfasdfasdf";
        var result = await _repository.Add(account);
        return ResultModel.Result(result);
    }

    public async Task<IResultModel> Edit(Guid id)
    {
        var account = await _repository.Get(id);
        if (account == null)
            return ResultModel.NotExists;

        var model = _mapper.Map<AccountUpdateDto>(account);
        model.Password = "";

        return ResultModel.Success(model);
    }

    public async Task<IResultModel> Update(AccountUpdateDto dto)
    {
        var account = await _repository.Get(dto.Id);
        if (account == null)
            return ResultModel.NotExists;

        var username = account.Username;
        var password = account.Password;

        _mapper.Map(dto, account);

        account.Username = username;
        account.Password = password;

        var result = await _repository.Update(account);
        return ResultModel.Result(result);
    }

    public async Task<IResultModel> Delete(Guid id)
    {
        var account = await _repository.Get(id);
        if (account == null)
            return ResultModel.NotExists;

        var result = await _repository.SoftDelete(id);

        return ResultModel.Result(result);
    }

    public async Task<IResultModel<ExcelModel>> Export(AccountQueryDto dto)
    {
        var query = _repository.Find();
        var list = await query.ToList();
        dto.ExportModel.Entities = list;
        var result = await _excelProvider.Export(dto.ExportModel);
        return result;
    }
}