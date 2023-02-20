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

namespace CRB.TPM.Mod.MainData.Core.Application;

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
    /// 获取当前用户AROS(账户角色组织关系：数据权限)
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
    /// 解析当前访问用户的角色组织树（合并用户角色）
    /// </summary>
    /// <returns></returns>
    public async Task<List<TreeResultModel<Guid, MOrgTreeVo>>> CurrentAccountAROSTree()
    {
        List<string> aros = new();
        this.CurrentAccountAROS.ForEach(s =>
        {
            aros.AddRange(s.Orgs);
        });
        var arost = new HashSet<string>(aros).Select(s => s.ToGuid()).ToList();
        var list = await _mOrgRepository.Find(s => arost.Contains(s.Id)).ToList();
        return ResolveAROrgTree(list, Guid.Empty, "", new());
    }


    /// <summary>
    /// 解析角色组织树(带节点路径)
    /// </summary>
    /// <param name="all"></param>
    /// <param name="parentId"></param>
    /// <param name="name"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    private List<TreeResultModel<Guid, MOrgTreeVo>> ResolveAROrgTree(IList<MOrgEntity> all, Guid parentId, string name, List<string> path)
    {
        var temp = new List<string>();

        var ret = all.Where(m => m.ParentId == parentId)
            .OrderBy(m => m.Id)
            .Select(m =>
            {
                path.ForEach(s =>
                {
                    if (!temp.Contains(s))
                        temp.Add(s);
                });

                if (!temp.Contains(m.OrgName))
                    temp.Add(m.OrgName);

                return new TreeResultModel<Guid, MOrgTreeVo>
                {
                    Id = m.Id,
                    Label = m.OrgName,
                    ParentId = m.ParentId,
                    Level = m.Type,
                    Path = temp,
                    Children = ResolveAROrgTree(all, m.Id, m.OrgName, temp)
                };

            }).ToList();


        if (!string.IsNullOrEmpty(name) && !temp.Contains(name))
            temp.Add(name);

        return ret;
    }

}
