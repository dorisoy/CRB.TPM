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
using CRB.TPM.Mod.Admin.Core.Domain.Role;
using CRB.TPM.Mod.Admin.Core.Domain.RoleMenu;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure.Repositories;

public class AccountRoleRepository : RepositoryAbstract<AccountRoleEntity>, IAccountRoleRepository
{
    public async Task<bool> Delete(Guid accountId, Guid roleId)
    {
        return await Find(m => m.AccountId == accountId && m.RoleId == roleId).ToDelete();
    }

    public async Task<bool> DeleteByAccount(Guid accountId, IUnitOfWork uow)
    {
        return await Find(m => m.AccountId == accountId).UseUow(uow).ToDelete();
    }

    public async Task<bool> Exists(Guid accountId, Guid roleId)
    {
        return await Find(m => m.AccountId == accountId && m.RoleId == roleId).ToExists();
    }

    public async Task<IList<RoleEntity>> QueryRole(Guid accountId)
    {
        var result = await Find(m => m.AccountId == accountId)
            .InnerJoin<RoleEntity>(m => m.T1.RoleId == m.T2.Id)
            .Select(m => new { m.T2 })
            .ToList<RoleEntity>();
        return result;
    }

    public async Task<IList<AccountRoleEntity>> QueryByRole(Guid roleId)
    {
        return await  Find(m => m.RoleId == roleId).ToList();
    }

    public async Task<IList<AccountRoleEntity>> QueryByAccount(Guid accountId)
    {
        return await Find(m => m.AccountId == accountId).ToList();
    }

    public async Task<IList<AccountRoleEntity>> QueryByAccount(Guid accountId, Guid roleId)
    {
        return await Find(m => m.AccountId == accountId && m.RoleId == roleId).ToList();
    }

    public async Task<IList<AccountRoleEntity>> QueryByAccount(Guid accountId, IList<Guid> roleIds)
    {
        return await Find(m => m.AccountId == accountId && roleIds.Contains(m.RoleId)).ToList();
    }

    public async Task<IList<AccountRoleEntity>> QueryByMenu(Guid menuId)
    {
        return await Find()
            .InnerJoin<RoleMenuEntity>(m => m.T1.RoleId == m.T2.RoleId)
            .Where(m => m.T2.MenuId == menuId)
            .ToList();
    }

    public async Task<bool> ExistsByRole(Guid roleId)
    {
        return await Find(m => m.RoleId == roleId)
            .InnerJoin<AccountEntity>(m => m.T1.AccountId == m.T2.Id && m.T2.Deleted == false)
            .ToExists();
    }
}
