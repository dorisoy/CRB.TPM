using CRB.TPM.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Domain.RoleButton;

/// <summary>
/// 角色按钮绑定关系仓储
/// </summary>
public interface IRoleButtonRepository : IRepository<RoleButtonEntity>
{
    Task<IList<RoleButtonEntity>> QueryButtonCodes(Guid roleId);
    Task<IList<string>> QueryButtonCodes(Guid roleId, string pageCode);
    Task<IList<string>> QueryButtonCodesByAccount(Guid accountId);
    Task<bool> DeleteByRole(Guid roleId, IUnitOfWork uow = null);
}