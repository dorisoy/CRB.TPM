using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Admin.Core.Domain.AccountRole;
using CRB.TPM.Mod.Admin.Core.Domain.RolePermission;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using CRB.TPM.Data.Abstractions;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure.Repositories;

public class RolePermissionRepository : RepositoryAbstract<RolePermissionEntity>, IRolePermissionRepository
{
    public Task<IList<string>> QueryByRole(Guid roleId, int platform)
    {
        return Find(m => m.RoleId == roleId && m.Platform == platform).Select(m => m.PermissionCode).ToList<string>();
    }

    public Task<IList<string>> QueryByAccount(Guid accountId, int platform)
    {
        return Find(m => m.Platform == platform)
            .InnerJoin<AccountRoleEntity>(m => m.T1.RoleId == m.T2.RoleId && m.T2.AccountId == accountId)
            .Select(m => m.T1.PermissionCode)
            .ToList<string>();
    }

    public Task<bool> DeleteByRole(Guid roleId, int platform, IUnitOfWork uow = null)
    {
        return Find(m => m.RoleId == roleId && m.Platform == platform).UseUow(uow).ToDelete();
    }
}