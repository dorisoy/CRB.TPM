using CRB.TPM.Cache.Abstractions;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Mod.Logging.Core.Application.LoginLog.Dto;
using CRB.TPM.Mod.Logging.Core.Domain.LoginLog;
using CRB.TPM.Mod.Logging.Core.Infrastructure;
using CRB.TPM.Utils.Map;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Logging.Core.Application.LoginLog;

public class LoginLogService : ILoginLogService
{
    private readonly IMapper _mapper;
    private readonly ILoginLogRepository _repository;
    private readonly ICacheProvider _cacheHandler;
    private readonly LoggingCacheKeys _cacheKeys;
    private readonly IExcelProvider _excelProvider;

    public LoginLogService(IMapper mapper, ILoginLogRepository repository, ICacheProvider cacheHandler, LoggingCacheKeys cacheKeys,  IExcelProvider excelProvider)
    {
        _mapper = mapper;
        _repository = repository;
        _cacheHandler = cacheHandler;
        _cacheKeys = cacheKeys;
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