using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Mod.Admin.Core.Application.AuditInfo.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.AuditInfo;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Application.AuditInfo;

public interface IAuditInfoService
{
    Task<IResultModel> Add(AuditInfoEntity info);
    Task<IResultModel> Details(int id);
    Task<IResultModel<ExcelModel>> Export(AuditInfoQueryDto dto);
    Task<PagingQueryResultModel<AuditInfoEntity>> Query(AuditInfoQueryDto dto);
    Task<IResultModel> QueryLatestWeekPv();
}