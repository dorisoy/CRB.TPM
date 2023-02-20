using CRB.TPM.Cache.Abstractions;
using CRB.TPM.Mod.Admin.Core.Application.Authorize.Vo;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Vo;
using CRB.TPM.Mod.Admin.Core.Domain.MOrg;
using CRB.TPM.Mod.Admin.Core.Infrastructure;
using CRB.TPM.Utils.Map;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRB.TPM.Auth.Abstractions;

namespace CRB.TPM.Mod.Admin.Core.Application;

/// <summary>
/// 用于表示服务基类
/// </summary>
public abstract class BaseService
{
    protected readonly IMapper _mapper;
    protected readonly ICacheProvider _cacheHandler;
    protected readonly IServiceProvider _serviceProvider;
    protected readonly IAccountResolver _accountResolver;
    protected readonly IMOrgRepository  _mOrgRepository;

    public BaseService(IMapper mapper,
    ICacheProvider cacheHandler,
    IServiceProvider serviceProvider,
    IMOrgRepository mOrgRepository,
    IAccountResolver accountResolver)
    {
        _mapper = mapper;
        _cacheHandler = cacheHandler;
        _serviceProvider = serviceProvider;
        _accountResolver = accountResolver;
        _mOrgRepository = mOrgRepository;
    }


    /// <summary>
    /// 获取当前用户AROS(账户角色组织关系)
    /// </summary>
    public List<AROVo> CurrentAccountAROS
    {
        get
        {
            var profileResolver = _serviceProvider.GetService<IAccountResolver>();
            if (profileResolver == null)
                profileResolver = _accountResolver;

            return profileResolver?.AROS ?? new();
        }
    }

    /// <summary>
    /// 获取当前访问用户的角色拥有的组织（合并用户角色和组织）
    /// </summary>
    public List<Guid> CurrentAROS
    {
        get
        {
            var profileResolver = _serviceProvider.GetService<IAccountResolver>();
            if (profileResolver == null)
                profileResolver = _accountResolver;

            return profileResolver?.CurrentAROS ?? new();
        }
    }


    /// <summary>
    /// 获取当前访问用户的角色拥有的组织层级（合并用户角色和组织）
    /// </summary>
    /// <returns></returns>
    public async Task<MOrgLevelVo> CurrentMOrg()
    {
        var profileResolver = _serviceProvider.GetService<IAccountResolver>();
        if (profileResolver == null)
            profileResolver = _accountResolver;

        return await profileResolver.CurrentMOrg();
    }


    ///// <summary>
    ///// 获取当前访问用户的角色拥有的组织层级（合并用户角色和组织）
    ///// </summary>
    ///// <returns></returns>
    //public async Task<List<MOrgLevelTreeVo>> CurrentMOrgs()
    //{
    //    var profileResolver = _serviceProvider.GetService<IAccountProfileResolver>();
    //    if (profileResolver == null)
    //        profileResolver = _accountResolver;

    //    return await profileResolver.CurrentMOrgs();
    //}
}
