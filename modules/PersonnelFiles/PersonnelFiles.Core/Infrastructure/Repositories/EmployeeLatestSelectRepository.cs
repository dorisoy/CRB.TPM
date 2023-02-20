using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.PS.Core.Application.EmployeeLatestSelect.Dto;
using CRB.TPM.Mod.PS.Core.Domain.Department;
using CRB.TPM.Mod.PS.Core.Domain.Employee;
using CRB.TPM.Mod.PS.Core.Domain.EmployeeLatestSelect;
using CRB.TPM.Mod.PS.Core.Domain.Post;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CRB.TPM.Data.Core.Queryable;

namespace CRB.TPM.Mod.PS.Core.Infrastructure.Repositories;

public class EmployeeLatestSelectRepository : RepositoryAbstract<EmployeeLatestSelectEntity>, IEmployeeLatestSelectRepository
{
    public Task<PagingQueryResultModel<EmployeeLatestSelectEntity>> Query(Guid employeeId, EmployeeLatestSelectQueryDto dto)
    {
        var query = Find(m => m.EmployeeId == employeeId);
        var joinQuery = query.LeftJoin<EmployeeEntity>(m => m.T1.RelationId == m.T2.Id && m.T2.Status == EmployeeStatus.Work)
            .LeftJoin<DepartmentEntity>(m => m.T2.DepartmentId == m.T3.Id)
            .LeftJoin<PostEntity>(m => m.T2.PostId == m.T4.Id);

        joinQuery.WhereNotNull(dto.Name, m => m.T2.Name.Contains(dto.Name));
        joinQuery.OrderByDescending(m => m.T1.Timestamp);
        joinQuery.Select(m => new { m.T2, DepartmentPath = m.T3.FullPath, PostName = m.T4.Name });

        return joinQuery.ToPaginationResult(dto.Paging);
    }

    public Task<EmployeeLatestSelectEntity> Get(Guid employeeId, Guid relationId, IUnitOfWork uow)
    {
        return Get(m => m.EmployeeId == employeeId && m.RelationId == relationId, uow);
    }

    public async Task<PagingQueryResultModel<EmployeeLatestSelectVo>> QueryView(Guid employeeId, EmployeeLatestSelectQueryDto dto)
    {
        var result = new PagingQueryResultModel<EmployeeLatestSelectVo>();
        try
        {

            var query = Find(m => m.EmployeeId == employeeId);

            var joinQuery = query
                .LeftJoin<EmployeeEntity>(m => m.T1.RelationId == m.T2.Id && m.T2.Status == EmployeeStatus.Work)
                .LeftJoin<DepartmentEntity>(m => m.T2.DepartmentId == m.T3.Id)
                .LeftJoin<PostEntity>(m => m.T2.PostId == m.T4.Id);

            joinQuery.WhereNotNull(dto.Name, m => m.T2.Name.Contains(dto.Name));
            joinQuery.OrderByDescending(m => m.T1.Timestamp);
            joinQuery.Select(m => new { RelationId = m.T1.Id });
  
            var rows = await joinQuery.ToPagination<EmployeeLatestSelectVo>(dto.Paging);
            var data = new PagingQueryResultBody<EmployeeLatestSelectVo>(rows, dto.Paging);

            return result.Success(data);
        }
        catch (Exception)
        {
            return result.Success(null);
        }
    }

}
