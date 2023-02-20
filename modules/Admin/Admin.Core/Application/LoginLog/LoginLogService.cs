using CRB.TPM.Cache.Abstractions;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.LoginLog.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.LoginLog;
using CRB.TPM.Mod.Admin.Core.Infrastructure;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Repositories;
using CRB.TPM.Utils.Map;
using System.Threading.Tasks;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Excel.Abstractions.Export;
using CRB.TPM.Mod.Admin.Core.Application.DictGroup.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.DictGroup;
using System.Collections.Generic;

namespace CRB.TPM.Mod.Admin.Core.Application.LoginLog;

public class LoginLogService : ILoginLogService
{
    private readonly IMapper _mapper;
    private readonly ILoginLogRepository _repository;
    private readonly ICacheProvider _cacheHandler;
    private readonly AdminCacheKeys _cacheKeys;
    private readonly AdminLocalizer _localizer;
    private readonly IExcelProvider _excelProvider;

    public LoginLogService(IMapper mapper, ILoginLogRepository repository, ICacheProvider cacheHandler, AdminCacheKeys cacheKeys, AdminLocalizer localizer, IExcelProvider excelProvider)
    {
        _mapper = mapper;
        _repository = repository;
        _cacheHandler = cacheHandler;
        _cacheKeys = cacheKeys;
        _localizer = localizer;
        _excelProvider = excelProvider;
    }

    public Task<PagingQueryResultModel<LoginLogEntity>> Query(LoginLogQueryDto dto)
    {
        var query = _repository.Find();
        return query.ToPaginationResult(dto.Paging);
    }

    [Transaction]
    public async Task<IResultModel> Add(LoginLogAddDto dto)
    {
        var entity = _mapper.Map<LoginLogEntity>(dto);
        var result = await _repository.Add(entity);
        return ResultModel.Result(result);
    }

    public async Task<IResultModel> Delete(int id)
    {
        var entity = await _repository.Get(id);
        if (entity == null)
            return ResultModel.NotExists;

        var result = await _repository.Delete(id);

        return ResultModel.Result(result);
    }


    public async Task<IResultModel<ExcelModel>> ExportLogin(LoginLogQueryDto dto)
    {
        var query = _repository.Find();
        query.WhereNotNull(dto.Platform, m => m.Platform.Equals(dto.Platform));
        var list = await query.ToList();

        dto.ExportModel.Entities = list;
  
        var result = await _excelProvider.Export(dto.ExportModel);
        return result;
    }


}