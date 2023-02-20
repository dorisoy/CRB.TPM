using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Mod.PS.Core.Application.Employee.Dto;
using CRB.TPM.Mod.PS.Core.Domain.Department;
using CRB.TPM.Mod.PS.Core.Domain.Employee;
using CRB.TPM.Mod.PS.Core.Domain.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.PS.Core.Infrastructure.Repositories;

public class EmployeeRepository : RepositoryAbstract<EmployeeEntity>, IEmployeeRepository
{
    public async Task<PagingQueryResultModel<EmployeeEntity>> Query(EmployeeQueryDto dto, IList<AccountEntity> accounts)
    {
        try
        {
            var query = Find();
            query.WhereIf(dto.DepartmentId != null && !dto.DepartmentId.HasValue, m => m.DepartmentId == dto.DepartmentId);
            query.WhereNotNull(dto.Name, m => m.Name.Contains(dto.Name));
            query.WhereNotNull(dto.JobNo, m => m.Id == dto.JobNo.Value);

            if (accounts != null && accounts.Any())
                query.Where(m => accounts.Select(s => s.Id).Contains(m.CreatedBy));

            //var joinQuery = query.LeftJoin<AccountEntity>(m => m.T1.CreatedBy == m.T2.Id)
            //    .LeftJoin<DepartmentEntity>(m => m.T1.DepartmentId == m.T3.Id)
            //    .LeftJoin<PostEntity>(m => m.T1.PostId == m.T4.Id);

            var joinQuery = query.LeftJoin<DepartmentEntity>(m => m.T1.DepartmentId == m.T2.Id)
               .LeftJoin<PostEntity>(m => m.T1.PostId == m.T3.Id);

            if (!dto.Paging.OrderBy.Any())
            {
                joinQuery.OrderByDescending(m => m.T1.Id);
            }

            joinQuery.Select(m => new
            {
                m.T1.Id,
                m.T1.AccountId,
                m.T1.DepartmentId,
                m.T1.Name,
                m.T1.Nature,
                m.T1.Sex,
                m.T1.Picture,
                m.T1.PostId,
                m.T1.Status,
                m.T1.JoinDate,
                m.T1.LDAP,
                m.T1.Sort,
                m.T1.CreatedTime,
                m.T1.CreatedBy,
                m.T1.ModifiedTime,
                m.T1.ModifiedBy,
                m.T1.Deleted,
                m.T1.DeletedTime,
                DepartmentPath = m.T2.FullPath,
                PostName = m.T3.Name
            });

            return await joinQuery.ToPaginationResult(dto.Paging);

        }
        catch (Exception ex)
        {
            throw;
        }

    }

    public async Task<bool> ExistsBindDept(Guid departmentId)
    {
        return await Find(m => m.DepartmentId == departmentId).ToExists();
    }

    public async Task<bool> ExistsBindPost(Guid postId)
    {
        return await Find(m => m.PostId == postId).ToExists();
    }

    public async Task<bool> UpdateLeaveStatus(Guid id, IUnitOfWork uow)
    {
        return await Find(m => m.Id == id).UseUow(uow).ToUpdate(m => new EmployeeEntity
        {
            Status = EmployeeStatus.Leave
        });
    }

    public async Task<EmployeeEntity> GetByAccountId(Guid accountId)
    {
        return await Get(m => m.AccountId == accountId);
    }

    public async Task<IList<EmployeeEntity>> QueryByDepartment(Guid departmentId)
    {
        return await Find(m => m.DepartmentId == departmentId)
            .LeftJoin<PostEntity>(m => m.T1.PostId == m.T2.Id)
            .LeftJoin<DepartmentEntity>(m => m.T1.DepartmentId == m.T3.Id)
            .OrderBy(m => m.T1.Sort)
            .Select(m => new { m.T1, PostName = m.T2.Name, DepartmentPath = m.T3.FullPath })
            .ToList();
    }

    public async Task<EmployeeEntity> GetWidthExtend(Guid id)
    {

        return await Find(m => m.Id == id)
            .LeftJoin<PostEntity>(m => m.T1.PostId == m.T2.Id)
            .LeftJoin<DepartmentEntity>(m => m.T1.DepartmentId == m.T3.Id)
            .OrderBy(m => m.T1.Sort)
            .Select(m => new { m.T1, PostName = m.T2.Name, DepartmentPath = m.T3.FullPath })
            .ToFirst();
    }
}
