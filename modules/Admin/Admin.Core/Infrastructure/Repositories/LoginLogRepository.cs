using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Admin.Core.Application.LoginLog.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.LoginLog;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure.Repositories;

public class LoginLogRepository : RepositoryAbstract<LoginLogEntity>, ILoginLogRepository
{
    public Task<PagingQueryResultModel<LoginLogEntity>> Query(LoginLogQueryDto dto)
    {
        return QueryBuilder(dto).ToPaginationResult(dto.Paging);
    }

    private IQueryable<LoginLogEntity> QueryBuilder(LoginLogQueryDto dto)
    {
        var query = Find();
        return query;
    }

    //ExcelExportEntityModel
}