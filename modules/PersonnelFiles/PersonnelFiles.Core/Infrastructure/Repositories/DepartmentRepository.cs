using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Mod.PS.Core.Application.Department.Dto;
using CRB.TPM.Mod.PS.Core.Domain.Department;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace CRB.TPM.Mod.PS.Core.Infrastructure.Repositories;

public class DepartmentRepository : RepositoryAbstract<DepartmentEntity>, IDepartmentRepository
{
    public Task<PagingQueryResultModel<DepartmentEntity>> Query(DepartmentQueryDto dto, IList<AccountEntity> accounts)
    {
        var result = new PagingQueryResultModel<DepartmentEntity>();

        try
        {
            var query = Find();

            query.WhereNotNull(dto.ParentId, m => m.ParentId == dto.ParentId);
            query.WhereNotNull(dto.Name, m => m.Name.Contains(dto.Name));
            query.WhereNotNull(dto.Code, m => m.Code == dto.Code);

            if (accounts != null && accounts.Any())
                query.Where(m => accounts.Select(s => s.Id).Contains(m.CreatedBy));

            if (!dto.Paging.OrderBy.Any())
                query.OrderByDescending(m => m.Id);

            return query.ToPaginationResult(dto.Paging);

        }
        catch (Exception ex)
        {
            return Task.FromResult(result.Failed(ex.Message));
        }
    }

    public async Task<bool> ExistsName(string name, Guid parentId, Guid? id = null)
    {
        var query = Find(m => m.Name == name && m.ParentId == parentId);
        query.WhereNotNull(id, m => m.Id != id.Value);
        return await query.ToExists();
    }

    public async Task<bool> ExistsCode(string code, Guid? id = null)
    {
        var query = Find(m => m.Code == code);
        query.WhereNotNull(id, m => m.Id != id.Value);
        return await query.ToExists();
    }

    public async Task<bool> ExistsChildren(Guid parentId)
    {
        return await Find(m => m.ParentId == parentId).ToExists();
    }
}
