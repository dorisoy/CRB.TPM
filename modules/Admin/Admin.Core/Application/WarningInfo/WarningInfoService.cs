using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Cache.Abstractions;
using CRB.TPM.Config.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Core.Extensions;
using CRB.TPM.Mod.Admin.Core.Application.WarningInfo.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Mod.Admin.Core.Domain.AccountRole;
using CRB.TPM.Mod.Admin.Core.Domain.AccountSkin;
using CRB.TPM.Mod.Admin.Core.Domain.Role;
using CRB.TPM.Mod.Admin.Core.Domain.WarningInfo;
using CRB.TPM.Mod.Admin.Core.Infrastructure;
using CRB.TPM.Utils.Map;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace CRB.TPM.Mod.Admin.Core.Application.WarningInfo;

public class WarningInfoService : IWarningInfoService
{
    private readonly IMapper _mapper;
    private readonly IWarningInfoRepository _repository;
    private readonly IConfigProvider _configProvider;
    private readonly AdminDbContext _dbContext;
    private readonly IPlatformProvider _platformProvider;
    private readonly ICacheProvider _cacheHandler;
    private readonly AdminCacheKeys _cacheKeys;

    public WarningInfoService(IMapper mapper,
        IWarningInfoRepository repository,
        IConfigProvider configProvider,
        IPlatformProvider platformProvider,
        ICacheProvider cacheHandler,
         AdminCacheKeys cacheKeys,
        AdminDbContext dbContext)
    {
        _mapper = mapper;
        _repository = repository;
        _configProvider = configProvider;
        _dbContext = dbContext;
        _platformProvider = platformProvider;
        _cacheHandler = cacheHandler;
        _cacheKeys = cacheKeys;
    }

    public async Task<PagingQueryResultModel<WarningInfoEntity>> Query(WarningInfoQueryDto dto)
    {
        var result = await _repository.Query(dto);
        return result;
    }

    /// <summary>
    /// 批量添加
    /// </summary>
    /// <param name="dtos"></param>
    /// <returns></returns>
    public async Task<IResultModel> BulkAdd(List<WarningInfoAddDto> dtos)
    {
        var list = _mapper.Map<List<WarningInfoEntity>>(dtos);
        await _repository.BulkAdd(list);
        return ResultModel.Success();
    }

}