using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Excel.Abstractions.Export;
using CRB.TPM.Mod.AuditInfo.Core.Application.AuditInfo.Dto;
using CRB.TPM.Mod.AuditInfo.Core.Domain.AuditInfo;
using CRB.TPM.Module.Abstractions;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.AuditInfo.Core.Application.AuditInfo;

public class AuditInfoService : IAuditInfoService
{
    private readonly IAuditInfoRepository _repository;
    private readonly IExcelProvider _excelProvider;
    private readonly IModuleCollection _moduleCollection;
    private readonly IPlatformProvider _platformProvider;

    public AuditInfoService(IAuditInfoRepository repository, IExcelProvider excelProvider, IModuleCollection moduleCollection, IPlatformProvider platformProvider)
    {
        _repository = repository;
        _excelProvider = excelProvider;
        _moduleCollection = moduleCollection;
        _platformProvider = platformProvider;
    }

    public async Task<IResultModel> Add(AuditInfoEntity info)
    {
        if (info == null)
            return ResultModel.Failed();

        var result = await _repository.Add(info);
        return ResultModel.Result(result);
    }

    public Task<PagingQueryResultModel<AuditInfoEntity>> Query(AuditInfoQueryDto dto)
    {
        var query = _repository.Find();
        return query.ToPaginationResult(dto.Paging);
    }

    public async Task<IResultModel> Details(int id)
    {
        var modules = _moduleCollection.ToList();
        var entity = await _repository.Details(id, modules);
        if (entity == null)
            return ResultModel.NotExists;

        entity.PlatformName = _platformProvider.ToDescription(entity?.Platform ?? -1);

        return ResultModel.Success(entity);
    }

    public async Task<IResultModel> QueryLatestWeekPv()
    {
        var list = await _repository.QueryLatestWeekPv();
        return ResultModel.Success(list);
    }

    public async Task<IResultModel<ExcelModel>> Export(AuditInfoQueryDto dto)
    {
        var query = _repository.Find();
        query.WhereNotNull(dto.Platform, m => m.Platform.Equals(dto.Platform));
        var list = await query.ToList();

        dto.ExportModel.Entities = list;
        var result = await _excelProvider.Export(dto.ExportModel);
        return result;
    }
}
