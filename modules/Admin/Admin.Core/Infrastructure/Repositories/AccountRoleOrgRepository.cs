using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Admin.Core.Application.Account.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Mod.Admin.Core.Domain.AccountRole;
using CRB.TPM.Mod.Admin.Core.Domain.AccountRoleOrg;
using CRB.TPM.Mod.Admin.Core.Domain.Role;
using CRB.TPM.Mod.Admin.Core.Domain.RoleMenu;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure.Repositories;

public class AccountRoleOrgRepository : RepositoryAbstract<AccountRoleOrgEntity>, IAccountRoleOrgRepository
{

    public async Task<bool> DeleteOrgByAccountRole(Guid accountRoleId, IUnitOfWork uow)
    {
        return await Find(m => m.Account_RoleId == accountRoleId).UseUow(uow).ToDelete();
    }

    public async Task<bool> DeleteOrgByAccountRole(IList<Guid> accountRoleIds, IUnitOfWork uow)
    {
        return await Find(m => accountRoleIds.Contains( m.Account_RoleId)).UseUow(uow).ToDelete();
    }

    public async Task<IList<AccountRoleOrgEntity>> QueryByAccountRole(IList<Guid> accountRoleIds)
    {
        return await Find(m => accountRoleIds.Contains(m.Account_RoleId)).ToList();
    }

}
