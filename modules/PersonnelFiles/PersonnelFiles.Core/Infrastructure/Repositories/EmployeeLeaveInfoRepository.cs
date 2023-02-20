using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.PS.Core.Application.EmployeeLeaveInfo.Dto;
using CRB.TPM.Mod.PS.Core.Domain.EmployeeLeaveInfo;
using System;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.PS.Core.Infrastructure.Repositories;

public class EmployeeLeaveInfoRepository : RepositoryAbstract<EmployeeLeaveInfoEntity>, IEmployeeLeaveInfoRepository
{

    public Task<PagingQueryResultModel<EmployeeLeaveInfoEntity>> Query(EmployeeLeaveInfoQueryDto dto)
    {
        var query = Find();
        query.WhereNotNull(dto.EmployeeId, m => m.EmployeeId == dto.EmployeeId);
        query.WhereNotNull(dto.Type, m => m.Type == dto.Type);
        query.WhereNotNull(dto.ApplyDate, m => m.ApplyDate == dto.ApplyDate);
        query.WhereNotNull(dto.LeaveDate, m => m.LeaveDate == dto.LeaveDate);
        return query.ToPaginationResult(dto.Paging);
    }

    public Task<EmployeeLeaveInfoEntity> GetByEmployeeId(Guid employeeId)
    {
        return Get(m => m.EmployeeId == employeeId);
    }

}
