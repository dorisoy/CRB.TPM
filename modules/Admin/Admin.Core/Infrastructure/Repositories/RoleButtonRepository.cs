using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Admin.Core.Domain.AccountRole;
using CRB.TPM.Mod.Admin.Core.Domain.RoleButton;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure.Repositories;

public class RoleButtonRepository : RepositoryAbstract<RoleButtonEntity>, IRoleButtonRepository
{
    public async Task<IList<RoleButtonEntity>> QueryButtonCodes(Guid roleId)
    {
        return await Find(m => m.RoleId == roleId).ToList();
    }

    public Task<IList<string>> QueryButtonCodes(Guid roleId, string pageCode)
    {
        return Find(m => m.RoleId == roleId).Select(m => m.ButtonCode).ToList<string>();
    }

    public async Task<IList<string>> QueryButtonCodesByAccount(Guid accountId)
    {
        return await Find()
            .InnerJoin<AccountRoleEntity>(m => m.T1.RoleId == m.T2.RoleId && m.T2.AccountId == accountId)
            .Select(m => m.T1.ButtonCode)
            .ToList<string>();
    }

    public async Task<bool> DeleteByRole(Guid roleId, IUnitOfWork uow = null)
    {
        return await Find(m => m.RoleId == roleId).UseUow(uow).ToDelete();
    }
}