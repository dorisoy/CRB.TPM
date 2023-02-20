using System;
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Module.Core.Application.Account.Dto;
using CRB.TPM.Mod.Module.Core.Domain.Account;


namespace CRB.TPM.Mod.Module.Core.Infrastructure.Repositories;

public class AccountRepository : RepositoryAbstract<AccountEntity>, IAccountRepository
{
    public Task<PagingQueryResultModel<AccountEntity>> Query(AccountQueryDto dto)
    {
        var query = QueryBuilder(dto);
        return query.ToPaginationResult(dto.Paging);
    }

    public Task<bool> ExistsUsername(string username, Guid? id = null)
    {
        return Find(m => m.Username == username).WhereNotNull(id, m => m.Id != id).ToExists();
    }

    public Task<bool> ExistsPhone(string phone, Guid? id = null)
    {
        return Find(m => m.Phone == phone).WhereNotNull(id, m => m.Id != id).ToExists();
    }

    public Task<bool> ExistsEmail(string email, Guid? id = null)
    {
        return Find(m => m.Email == email).WhereNotNull(id, m => m.Id != id).ToExists();
    }

    public Task<AccountEntity> GetByUserName(string username, Guid? tenantId = null)
    {
        return Find(m => m.Username == username).ToFirst();
    }

    private IQueryable<AccountEntity> QueryBuilder(AccountQueryDto dto)
    {
        return Find()
            .WhereNotNull(dto.Username, m => m.Username == dto.Username)
            .WhereNotNull(dto.Name, m => m.Name.Contains(dto.Name))
            .WhereNotNull(dto.Phone, m => m.Phone.Contains(dto.Phone))
            .Select(m => m)
            .OrderByDescending(m => m.Id);
    }
}