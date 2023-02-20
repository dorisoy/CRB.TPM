using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRB.TPM.Config.Abstractions;
using CRB.TPM.Data.Abstractions.Query;

using CRB.TPM.Mod.Module.Core.Application.Account.Dto;
using CRB.TPM.Mod.Module.Core.Domain.Account;
using CRB.TPM.Utils.Map;

//引入
using CRB.TPM.Data.Core;
using CRB.TPM.Data.Sharding;
using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.Module.Core.Application.Account;

public class AccountClientService : IAccountClientService
{
    private readonly IMapper _mapper;
    private readonly IConfigProvider _configProvider;
    private readonly IClientDbContext _clientDbContext;

    public AccountClientService(IMapper mapper,
        IConfigProvider configProvider,
        IClientDbContext clientDbContext)
    {
        _mapper = mapper;
        _configProvider = configProvider;
        _clientDbContext = clientDbContext;
    }

    public async Task<IList<AccountEntity>> Query()
    {
        var table = _clientDbContext.Db.GetTable<AccountEntity>("AccountEntity");
        var query = await table.GetAllAsync();
        return query.ToList();
    }

    public async Task<IResultModel> Add(AccountAddDto dto)
    {
        return await ResultModel.Result(null);
    }

    public async Task<IResultModel> Edit(Guid id)
    {
        return await ResultModel.Result(null);
    }

    public async Task<IResultModel> Update(AccountUpdateDto dto)
    {
        return await ResultModel.Result(null);
    }

    public async Task<IResultModel> Delete(Guid id)
    {
        return await ResultModel.Result(null);
    }

}