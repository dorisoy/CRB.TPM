using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRB.TPM.Cache.Abstractions;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Mod.Admin.Core.Domain.RolePermission;
using CRB.TPM.Utils.Annotations;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure.Defaults;

/// <summary>
/// 默认账户权限解析器
/// </summary>
[ScopedInject]
internal class DefaultAccountPermissionResolver : IAccountPermissionResolver
{
    private readonly IAccountRepository _accountRepository;
    private readonly IRolePermissionRepository _rolePermissionRepository;
    private readonly AdminCacheKeys _cacheKeys;
    private readonly ICacheProvider _cacheHandler;

    public DefaultAccountPermissionResolver(IAccountRepository accountRepository, IRolePermissionRepository rolePermissionRepository, AdminCacheKeys cacheKeys, ICacheProvider cacheHandler)
    {
        _accountRepository = accountRepository;
        _rolePermissionRepository = rolePermissionRepository;
        _cacheKeys = cacheKeys;
        _cacheHandler = cacheHandler;
    }

    /// <summary>
    /// 默认账户权限解析
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="platform"></param>
    /// <returns></returns>
    public async Task<IList<string>> Resolve(Guid accountId, int platform)
    {
        if (accountId.IsEmpty())
            return new List<string>();

        var key = _cacheKeys.AccountPermissions(accountId, platform);
        var list = await _cacheHandler.Get<IList<string>>(key);
        if (list == null)
        {
            var account = await _accountRepository.Get(accountId);
            if (account == null)
                return new List<string>();

            //list = await _rolePermissionRepository
            //    .Find(m => m.RoleId == account.RoleId)
            //    .Select(m => m.PermissionCode)
            //    .ToList<string>();

            list = await _rolePermissionRepository.QueryByAccount(accountId, platform);

            await _cacheHandler.Set(key, list);
        }

        return list;
    }
}