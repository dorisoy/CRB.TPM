using CRB.TPM.Cache.Abstractions;
using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Config.Core;
using CRB.TPM.Mod.Admin.Core.Application.Authorize.Vo;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Vo;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Mod.Admin.Core.Domain.AccountRole;
using CRB.TPM.Mod.Admin.Core.Domain.AccountRoleOrg;
using CRB.TPM.Mod.Admin.Core.Domain.AccountSkin;
using CRB.TPM.Mod.Admin.Core.Domain.Menu;
using CRB.TPM.Mod.Admin.Core.Domain.MObject;
using CRB.TPM.Mod.Admin.Core.Domain.MOrg;
using CRB.TPM.Mod.Admin.Core.Domain.Role;
using CRB.TPM.Mod.Admin.Core.Domain.RoleButton;
using CRB.TPM.Mod.Admin.Core.Domain.RoleMenu;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Json;
using CRB.TPM.Utils.Map;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IAccount = CRB.TPM.Auth.Abstractions.IAccount;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure.Defaults;

/// <summary>
/// 默认账户资料解析器
/// </summary>
[ScopedInject]
public class DefaultAccountProfileResolver : IAccountResolver
{
    private readonly IMapper _mapper;
    private readonly IMenuRepository _menuRepository;
    private readonly IRoleMenuRepository _roleMenuRepository;
    private readonly IRoleButtonRepository _roleButtonRepository;
    private readonly IAccountSkinRepository _accountSkinRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly JsonHelper _jsonHelper;
    private readonly IRoleRepository _roleRepository;
    private readonly IAccountRoleOrgRepository _accountRoleOrgRepository;
    private readonly IAccount _account;
    private readonly IMOrgRepository _mOrgRepository;
    private readonly IMObjectRepository _mObjectRepository;
    private readonly ICacheProvider _cacheHandler;
    private readonly AdminCacheKeys _cacheKeys;


    public DefaultAccountProfileResolver(IRoleMenuRepository roleMenuRepository,
        IMenuRepository menuRepository,
        IMapper mapper,
        IAccount account,
        IRoleButtonRepository roleButtonRepository,
        IAccountSkinRepository accountSkinRepository,
        IAccountRoleRepository accountRoleRepository,
        IRoleRepository roleRepository,
        IAccountRoleOrgRepository accountRoleOrgRepository,
        JsonHelper jsonHelper,
        IMOrgRepository mOrgRepository,
        IMObjectRepository mObjectRepository,
        ICacheProvider cacheHandler,
        AdminCacheKeys cacheKeys)
    {
        _menuRepository = menuRepository;
        _roleMenuRepository = roleMenuRepository;
        _mapper = mapper;
        _account = account;
        _roleButtonRepository = roleButtonRepository;
        _accountSkinRepository = accountSkinRepository;
        _accountRoleRepository = accountRoleRepository;
        _roleRepository = roleRepository;
        _accountRoleRepository = accountRoleRepository;
        _accountRoleOrgRepository = accountRoleOrgRepository;
        _jsonHelper = jsonHelper;
        _mOrgRepository = mOrgRepository;
        _mObjectRepository = mObjectRepository;
        _cacheHandler = cacheHandler;
        _cacheKeys = cacheKeys;
    }

    /// <summary>
    /// 默认账户资料解析
    /// </summary>
    /// <param name="account"></param>
    /// <param name="platform"></param>
    /// <returns></returns>
    public async Task<ProfileVo> Resolve(AccountEntity account, int platform)
    {
        var sw = new Stopwatch();
        sw.Start();

        var vo = new ProfileVo
        {
            AccountId = account.Id,
            Platform = platform,
            Username = account.Username,
            Name = account.Name,
            Phone = account.Phone,
            Email = account.Email,
        };

        //读取账户皮肤配置信息
        var accountSkin = await _accountSkinRepository.Find(m => m.AccountId == account.Id).ToFirst();
        if (accountSkin != null)
        {
            vo.Skin = new ProfileSkinVo
            {
                Name = accountSkin.Name,
                Code = accountSkin.Code,
                Theme = accountSkin.Theme,
                Size = accountSkin.Size
            };
        }
        else
        {
            vo.Skin = new ProfileSkinVo();
        }

        sw.Stop();
        Debug.WriteLine($"耗时: {sw.Elapsed.ToString(@"hh\:mm\:s\.fff")}");


        //标签导航组件配置信息
        vo.Tabnav = new TabnavVo
        {
            Enabled = true,
            HomeUrl = "",
            ShowHome = true,
            MaxOpenCount = 20,
            ShowIcon = true,
        };

        //根据账户查询角色，然后找到菜单
        //var accountRoles = await _accountRoleRepository.QueryRole(account.Id);
        //var roles = accountRoles.Select(s => s.Id).ToList();

        sw.Restart();
        var roles = await _accountRoleRepository.QueryRole(account.Id);
        sw.Stop();
        Debug.WriteLine($"耗时: {sw.Elapsed.ToString(@"hh\:mm\:s\.fff")}");

        sw.Restart();
        var accountRoles = await _accountRoleRepository.QueryByAccount(account.Id);
        sw.Stop();
        Debug.WriteLine($"耗时: {sw.Elapsed.ToString(@"hh\:mm\:s\.fff")}");

        sw.Restart();
        var accountRoleOrgs = await _accountRoleOrgRepository.QueryByAccountRole(accountRoles?.Select(s => s.Id)?.ToList());
        sw.Stop();
        Debug.WriteLine($"耗时: {sw.Elapsed.ToString(@"hh\:mm\:s\.fff")}");


        sw.Restart();
        //关联角色
        vo.Roles = roles?.Select(r => new OptionResultModel { Label = r.Name, Value = r.Id })?.ToList();

        //用户角色
        vo.AccountRoles = accountRoles?
               .Select(r => (new { roleId = r.RoleId, id = r.Id }).ToExpando())?
               .ToList();

        //用户角色组织
        vo.AccountRoleOrgs = accountRoleOrgs?
            .Select(r => (new { orgId = r.OrgId, id = r.Id }).ToExpando())?
            .ToList();

        //用户角色组织关系（角色对应组织）
        vo.AROS = ResolveAROS(account.Id, roles, accountRoles, accountRoleOrgs);

        //当前访问用户的角色组织树（合并用户角色）
        //vo.AROSTree = await CurrentAccountAROSTree();
        vo.MOrgs = await CurrentMOrg();

#if DEBUG
        //var rn = _account.RouterName;
        //var test = await CurrentMOrgs();
        //var json = Newtonsoft.Json.JsonConvert.SerializeObject(test);
        //Debug.WriteLine(json);
#endif

        sw.Stop();
        Debug.WriteLine($"耗时: {sw.Elapsed.ToString(@"hh\:mm\:s\.fff")}");

        //菜单
        //var menus = await menusQuery.ToList<MenuEntity>();
        var menus = (await _menuRepository.QueryByAccount(account.Id)).Distinct(new MenuComparer()).ToList();

        //按钮
        var buttons = await _roleButtonRepository.Find(m => roles.Select(s => s.Id).Contains(m.RoleId)).ToList();
        var buttons2 = await _roleButtonRepository.QueryButtonCodesByAccount(account.Id);

        var rootMenu = new ProfileMenuVo { Id = Guid.Empty };

        //解析菜单
        ResolveMenu(menus, buttons, rootMenu);

        //菜单列表
        vo.Menus = rootMenu.Children;

        return vo;
    }

    /// <summary>
    /// 当前账户
    /// </summary>
    public IAccount CurrentAccount
    {
        get
        {
            return _account;
        }
    }

    /// <summary>
    /// 获取当前用户AROS(账户角色组织关系：数据权限)
    /// </summary>
    public List<AROVo> AROS
    {
        get
        {
            return ResolveAROS(_account.Id).Result;
        }
    }

    /// <summary>
    ///  获取当前访问用户的角色拥有的组织（合并用户角色和组织）
    /// </summary>
    /// <returns></returns>
    public List<Guid> CurrentAROS
    {
        get
        {
            var aros = new List<string>();
            this.AROS.ForEach(s =>
            {
                aros.AddRange(s.Orgs);
            });

            if (aros.Count == 0)
                return new();

            var merges = new HashSet<string>(aros).Select(s => s.ToGuid()).ToList();

            return merges;
        }
    }

    /// <summary>
    /// 获取当前访问用户的角色拥有的组织层级（合并用户角色和组织）
    /// </summary>
    /// <returns></returns>
    public async Task<MOrgLevelVo> CurrentMOrg(List<string> maros = null)
    {
        List<string> aros = new();
        if (maros != null && maros.Any())
        {
            aros.AddRange(maros);
        }
        else
        {
            this.AROS.ForEach(s => { aros.AddRange(s.Orgs); });
        }

        if (aros.Count == 0)
            return new();

        var differents = new HashSet<string>(aros).Select(s => s.ToGuid()).ToList();

        var alls = await GetForCache();
        var orgs = alls.Where(s => differents.Contains(s.Id)).ToList();

        var molv = new MOrgLevelVo()
        {
            HeadOffice = ToMOrgVo(orgs, OrgEnumType.HeadOffice),
            Dbs = ToMOrgVo(orgs, OrgEnumType.BD),
            MarketingCenters = ToMOrgVo(orgs, OrgEnumType.MarketingCenter),
            SaleRegions = ToMOrgVo(orgs, OrgEnumType.SaleRegion),
            Departments = ToMOrgVo(orgs, OrgEnumType.Department),
            Stations = ToMOrgVo(orgs, OrgEnumType.Station),
        };

        return molv;
    }

    /// <summary>
    /// 获取当前访问用户的角色拥有的组织层级（合并用户角色和组织）
    /// </summary>
    /// <returns></returns>
    public async IAsyncEnumerable<MOrgLevelTreeVo> CurrentMOrgs()
    {
        //var molvs = new List<MOrgLevelTreeVo>();
        var aros = new List<string>();

        #region 路由处理
        //获取当前请求路由菜单
        var routerName = _account.RouterName;
        //如果路由存在时，则从路由取组织(这里路由ID=菜单ID)
        var isGuid = Guid.TryParse(routerName, out Guid menuId);
        if (isGuid && !menuId.IsEmpty())
        {
            //根据路菜单由获取对应的角色
            var mroles = await _roleRepository.QueryByMenuId(menuId);
            //当前用户的角色
            var roles = await _accountRoleRepository.QueryRole(_account.Id);
            var troles = new List<RoleEntity>();
            if (mroles != null && mroles.Any())
            {
                foreach (var mr in mroles)
                {
                    var role = roles.Select(s => s.Id).Contains(mr.Id);
                    if (role)
                        troles.Add(mr);
                }
            }
            //获取当前用户账户和角色关系
            var accountRoles = await _accountRoleRepository.QueryByAccount(_account.Id);
            //获取当前用户账户角色和组织关系
            var accountRoleOrgs = await _accountRoleOrgRepository.QueryByAccountRole(accountRoles?.Select(s => s.Id)?.ToList());
            //当前对应的组织
            var curorgs = ResolveAROS(_account.Id, troles, accountRoles, accountRoleOrgs);
            curorgs.ForEach(s => { aros.AddRange(s.Orgs); });
        }
        //如果路由存在时，则从普通角色取组织
        else
        {
            this.AROS.ForEach(s => { aros.AddRange(s.Orgs); });
        }
        #endregion

        if (aros.Count == 0)
            yield break;

        var differents = new HashSet<string>(aros).Select(s => s.ToGuid()).ToList();
        var alls = await GetForCache();
        var orgs = alls.Where(s => differents.Contains(s.Id)).ToList();
        var mobjects = await GetMObjectForCache();

        if (orgs == null || mobjects == null)
            yield break;

        if (orgs.Any())
        {
            foreach (var item in orgs)
            {
                var m = mobjects.Where(s => s.ObjectCode == item.OrgCode).FirstOrDefault();

                var molv = new MOrgLevelTreeVo() { Level = (OrgEnumType)item.Type };

                molv.HeadOfficeID = new MOrgBaseVo
                {
                    Id = m.HeadOfficeId,
                    OrgCode = m.HeadOfficeCode,
                    Name = m.HeadOfficeName,
                };

                molv.DbID = new MOrgBaseVo
                {
                    Id = m.DivisionId,
                    OrgCode = m.DivisionCode,
                    Name = m.DivisionName,
                };

                molv.MarketingCenterID = new MOrgBaseVo
                {
                    Id = m.MarketingId,
                    OrgCode = m.MarketingCode,
                    Name = m.MarketingName,
                };

                molv.SaleRegionID = new MOrgBaseVo
                {
                    Id = m.BigAreaId,
                    OrgCode = m.BigAreaCode,
                    Name = m.BigAreaName,
                };

                molv.DepartmentID = new MOrgBaseVo
                {
                    Id = m.OfficeId,
                    OrgCode = m.OfficeCode,
                    Name = m.ObjectName,
                };

                molv.StationID = new MOrgBaseVo
                {
                    Id = m.StationId,
                    OrgCode = m.StationCode,
                    Name = m.StationName,
                };

               // molvs.Add(molv);
               yield return molv;
            }
        }

        //return molvs;
        yield break;
    }

    /// <summary>
    /// 根据组织权限生成Sql条件字符串
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="defaultGetLevel"></param>
    /// <param name="objAlias"></param>
    /// <returns></returns>
    public async Task<string> BuildSqlWhereStrByOrgAuth(GlobalOrgFilterDto dto, OrgEnumType defaultGetLevel = OrgEnumType.Station, string objAlias = "mobj")
    {
        //dto = dto ?? IOrgIevelIdsDtoFactory.CreateInstance();
        int getLevel = (int)defaultGetLevel;
        //表示没有权限
        string noAnyAuthSqlStr = "0=1";
        if (dto == null)
        {
            return noAnyAuthSqlStr;
        }
        /* 获取当前登陆用户所有权限 */
        //IList<MOrgLevelTreeVo> authAllOrg = await CurrentMOrgs();
        var authAllOrg = await CurrentMOrgs().ToListAsync();
        /* 没有挂组织直接返回空 */
        if (!authAllOrg.NotNullAndEmpty())
        {
            return noAnyAuthSqlStr;
        }
        var dtoType = typeof(GlobalOrgFilterDto);
        bool isAndJoin = false; //是否使用And拼接
        Dictionary<int, (string dtoV, string authV, string objV)> map = new Dictionary<int, (string dtoV, string authV, string objV)>()
            {
                {
                    (int)OrgEnumType.HeadOffice,
                    (nameof(dto.HeadOfficeIds),
                    nameof(MOrgLevelTreeVo.HeadOfficeID),
                    nameof(MObjectEntity.HeadOfficeId))
                },
                {
                    (int)OrgEnumType.BD,
                    (nameof(dto.DivisionIds),
                    nameof(MOrgLevelTreeVo.DbID),
                    nameof(MObjectEntity.DivisionId))
                },
                {
                    (int)OrgEnumType.MarketingCenter,
                    (nameof(dto.MarketingIds),
                    nameof(MOrgLevelTreeVo.MarketingCenterID),
                    nameof(MObjectEntity.MarketingId))
                },
                {
                    (int)OrgEnumType.SaleRegion,
                    (nameof(dto.DutyregionIds),
                    nameof(MOrgLevelTreeVo.SaleRegionID),
                    nameof(MObjectEntity.BigAreaId))
                },
                {
                    (int)OrgEnumType.Department,
                    (nameof(dto.DepartmentIds),
                    nameof(MOrgLevelTreeVo.DepartmentID),
                    nameof(MObjectEntity.OfficeId))
                },
                {
                    (int)OrgEnumType.Station,
                    (nameof(dto.StationIds),
                    nameof(MOrgLevelTreeVo.StationID),
                    nameof(MObjectEntity.StationId))
                },
                {
                    (int)OrgEnumType.Distributor,
                    (nameof(dto.DistributorIds),
                    nameof(MOrgLevelTreeVo.StationID),
                    nameof(MObjectEntity.DistributorId))
                }
            };
        if (!map.ContainsKey(getLevel))
        {
            return noAnyAuthSqlStr;
        }

        //如果配置了一级组织，直接取一级组织
        if (authAllOrg.Any(a => a.Level == OrgEnumType.HeadOffice))
        {
            var headIds = authAllOrg.Where(w => w.Level == OrgEnumType.HeadOffice).Select(s => s.HeadOfficeID.Id).ToList();
            dto.HeadOfficeIds = dto.HeadOfficeIds == null ? headIds : headIds.Intersect(dto.HeadOfficeIds).ToList();
            if (!dto.HeadOfficeIds.Any())
            {
                dto.HeadOfficeIds = headIds;
            }
            isAndJoin = true;
        }
        else
        {
            #region 过滤掉查询没有权限的组织，包含虚拟提级
            /* 找出最小层级及以下有权限的组织 */
            var mOrgLevelType = typeof(MOrgLevelTreeVo);
            var objType = typeof(MObjectEntity);
            bool isFindMin = false;
            var mobjectsCache = await GetMObjectForCache();
            if (!mobjectsCache.NotNullAndEmpty())
            {
                return noAnyAuthSqlStr;
            }
            var mobjects = new List<MObjectEntity>();
            //过滤出已配置权限的组织
            foreach (var m in map.Where(w => w.Key < (int)OrgEnumType.Distributor).OrderByDescending(o => o.Key))
            {
                var ids = authAllOrg.Select(s =>
                {
                    var obj = mOrgLevelType.GetProperty(map[m.Key].authV).GetValue(s) as MOrgBaseVo;
                    return obj.Id;
                }).Distinct().ToList().RemoveGuidEmpty();
                var levelMobjs = mobjectsCache.Where(w =>
                {
                    object obj = (Guid)objType.GetProperty(map[m.Key].objV).GetValue(w);
                    return w.Type == m.Key && ids.Any(a => a == (Guid)obj);
                }).ToList();
                if (levelMobjs.Any())
                {
                    mobjects.AddRange(levelMobjs);
                }
            }

            foreach (var item in map.OrderByDescending(o => o.Key))
            {
                /* 获取最小查询组织 */
                var minLevelIdsProp = dtoType.GetProperty(map[item.Key].dtoV);
                var minObjIds = minLevelIdsProp.GetValue(dto);
                var minLevelIds = minObjIds != null ? minObjIds as IList<Guid> : new List<Guid>();

                //根据(查询的最小组织)从上级或从下级找权限
                if ((item.Key == (int)OrgEnumType.HeadOffice || minLevelIds.Any()) && !isFindMin)
                {
                    isFindMin = true;
                    foreach (var m in map.Where(w => w.Key >= item.Key).OrderBy(o => o.Key))
                    {
                        //优先查下级组织绑定
                        var upAuthOrg = authAllOrg.Where(w =>
                        {
                            var idObj = mOrgLevelType.GetProperty(map[item.Key].authV).GetValue(w) as MOrgBaseVo;
                            return idObj != null && m.Key == (int)w.Level && (!minLevelIds.Any() || minLevelIds.Contains(idObj.Id));
                        }).ToList();
                        var queryLevelIdsProp = dtoType.GetProperty(map[m.Key].dtoV);
                        //查到下级组织有绑定
                        if (upAuthOrg.Any())
                        {
                            var setOrgIds = new List<Guid>();
                            foreach (var authOrg in upAuthOrg)
                            {
                                //查的是下级层级，挂的是上级组织
                                if ((int)authOrg.Level <= getLevel)
                                {
                                    var orgIds = upAuthOrg.Select(s => (mOrgLevelType.GetProperty(map[m.Key].authV).GetValue(s) as MOrgBaseVo).Id).Distinct().ToList();
                                    var objIds = queryLevelIdsProp.GetValue(dto);
                                    var queryLevelIds = objIds != null ? objIds as IList<Guid> : new List<Guid>();
                                    //有权限就添加，没权限就移除
                                    setOrgIds = queryLevelIds.Any() ? orgIds.Intersect(queryLevelIds).ToList() : orgIds;
                                }
                                //查的是上级层级，挂的是下级组织，返回的需要虚拟提级
                                else
                                {
                                    queryLevelIdsProp = dtoType.GetProperty(map[getLevel].dtoV);
                                    var objIds = queryLevelIdsProp.GetValue(dto);
                                    var queryLevelIds = objIds != null ? objIds as IList<Guid> : new List<Guid>();
                                    var orgIds = upAuthOrg.Select(s => (mOrgLevelType.GetProperty(map[getLevel].authV).GetValue(s) as MOrgBaseVo).Id).Distinct().ToList();
                                    setOrgIds = queryLevelIds.Union(orgIds).ToList();
                                }
                                queryLevelIdsProp.SetValue(dto, setOrgIds);
                            }
                        }
                        //下级组织绑定没有找到，则去查上级组织绑定
                        else
                        {
                            var nowLevel = mobjects.Where(w =>
                            {
                                return w.Type == m.Key && minLevelIds.Contains((Guid)objType.GetProperty(map[m.Key].objV).GetValue(w));
                            }).ToList();
                            IList<Guid> upIds = new List<Guid>();
                            foreach (var level in nowLevel)
                            {
                                var up = authAllOrg.FirstOrDefault(aw =>
                                {
                                    foreach (var itemUp in map.Where(w => w.Key < (int)item.Key).OrderByDescending(o => o.Key))
                                    {
                                        var isUp = (mOrgLevelType.GetProperty(map[itemUp.Key].authV).GetValue(aw) as MOrgBaseVo).Id == (Guid)objType.GetProperty(map[itemUp.Key].objV).GetValue(level) && (int)aw.Level == itemUp.Key;
                                        if (isUp) return true;
                                    }
                                    return false;
                                });
                                if (up != null)
                                {
                                    upIds.Add((Guid)objType.GetProperty(map[m.Key].objV).GetValue(level));
                                }
                            }
                            queryLevelIdsProp.SetValue(dto, upIds);
                        }
                    }
                }
                //清空(查询的最小组织)以上组织ids
                else if (minLevelIds.NotNullAndEmpty())
                {
                    minLevelIdsProp.SetValue(dto, null);
                }
            }
            //如果一个组织都没查到
            if (map.All(a => { var obj = dtoType.GetProperty(a.Value.dtoV).GetValue(dto);
                return obj == null || !(obj as IList<Guid>).Any();}))
            {
                return noAnyAuthSqlStr;
            }
            #endregion
        }

        //过滤后的组织生成查询条件Sql语句
        List<string> whereList = new List<string>();
        foreach (var item in map)
        {
            var levelObjIds = dtoType.GetProperty(map[item.Key].dtoV).GetValue(dto);
            var levelIds = levelObjIds != null ? (IList<Guid>)levelObjIds : new List<Guid>();
            if (levelIds.Any())
            {
                levelIds.NotNullAndEmptyIf(() => whereList.Add($"{objAlias}.{map[item.Key].objV} IN ({string.Join(',', levelIds.Select(s => $"'{s}'"))})"));
            }
        }
        var orgWhereStr = isAndJoin ? whereList.AndBuildStr(true) : whereList.OrBuildStr(true);
        var res = $"{objAlias}.Type={getLevel} AND {orgWhereStr}";
        return res;
    }

    /// <summary>
    /// 检查传入的组织id是否有权限
    /// </summary>
    /// <param name="orgType"></param>
    /// <param name="orgIds"></param>
    /// <returns></returns>
    public async Task<(bool isAuth, IList<string> noAuthCode)> CheckOrgIdsAuth(OrgEnumType orgType, IList<Guid> orgIds)
    {
        var whereAuthSqlStr = await BuildSqlWhereStrByOrgAuth(null, orgType);
        return await _mObjectRepository.CheckOrgIdsAuth(orgIds, orgType, whereAuthSqlStr);
    }

    ///// <summary>
    ///// 给指定对象绑定组织权限方法
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="obj"></param>
    //public void BindOrgAuthPropFunc<T>(T obj)
    //{
    //    var type = typeof(T);
    //    var prop = type.GetProperty("BuildSqlWhereStrAuthFunc");
    //    if (prop != null)
    //    {
    //        prop.SetValue(obj, new BuildSqlWhereStrByOrgAuthFunc(this.BuildSqlWhereStrByOrgAuth));
    //    }
    //}

    ///// <summary>
    ///// 获取当前访问用户的角色拥有的组织层级（合并用户角色和组织）
    ///// </summary>
    ///// <returns></returns>
    //[Obsolete("该方法已经弃用")]
    //public async Task<List<MOrgLevelTreeVo>> CurrentMOrgs2()
    //{
    //    var molvs = new List<MOrgLevelTreeVo>();
    //    var aros = new List<string>();

    //    this.AROS.ForEach(s => { aros.AddRange(s.Orgs); });

    //    if (aros.Count == 0)
    //        return new();

    //    var differents = new HashSet<string>(aros).Select(s => s.ToGuid()).ToList();
    //    var alls = await GetForCache();
    //    var orgs = alls.Where(s => differents.Contains(s.Id)).ToList();

    //    var headOffices = ToMOrgVoKey(orgs, OrgEnumType.HeadOffice); 
    //    var dbs = ToMOrgVoKey(orgs, OrgEnumType.BD);
    //    var marketingCenters = ToMOrgVoKey(orgs, OrgEnumType.MarketingCenter);
    //    var saleRegions = ToMOrgVoKey(orgs, OrgEnumType.SaleRegion);
    //    var departments = ToMOrgVoKey(orgs, OrgEnumType.Department);
    //    var stations = ToMOrgVoKey(orgs, OrgEnumType.Station);

    //    int max = CommonHelper.GetMax(new int[]
    //    {
    //        headOffices.Count,
    //        dbs.Count,
    //        marketingCenters.Count,
    //        saleRegions.Count,
    //        departments.Count,
    //        stations.Count
    //    });

    //    //空站位补充
    //    AppendEmpt(max, headOffices.Count, ref headOffices);
    //    AppendEmpt(max, dbs.Count, ref dbs);
    //    AppendEmpt(max, marketingCenters.Count, ref marketingCenters);
    //    AppendEmpt(max, saleRegions.Count, ref saleRegions);
    //    AppendEmpt(max, departments.Count, ref departments);
    //    AppendEmpt(max, stations.Count, ref stations);

    //    //示例：转化前
    //    /*
    //    [1,2]
    //    [4,5]
    //    [6,7,8]
    //    [9]
    //    [3]
    //    [10]
    //    */

    //    //转化后
    //    /*
    //      1,4,6,9,3,10
    //      2,5,7,0,0,0
    //      0,0,8,0,0,0
    //     */

    //    //构建一个转化矩阵
    //    var matrix = new Guid[][]
    //    {
    //       headOffices.ToArray(),
    //       dbs.ToArray(),
    //       marketingCenters.ToArray(),
    //       saleRegions.ToArray(),
    //       departments.ToArray(),
    //       stations.ToArray()
    //    };

    //    //转换为可分层数据
    //    var nodes = alls.Single(x => x.ParentId == Guid.Empty)
    //        .AsHierarchical(x => alls.Where(n => n.ParentId == x.Id), true);
    //    var ables = nodes.AsEnumerable();


    //    //to MOrgVo
    //    orgs.ForEach(s =>
    //    {
    //        //获取当前节点
    //        var curnode = ables.Where(m => m.Current.Id == s.Id).Single();
    //        //获取从根到节点的路径
    //        var paths = curnode.GetPathFromRoot().Select(m => m.Current);
    //        s.Paths = paths.ToList();
    //    });

    //    //转化矩阵到
    //    molvs = ConvertMatrix(orgs, MatrixTranspose(matrix));

    //    return molvs;
    //}

    /// <summary>求一个矩阵的转置矩阵</summary>
    /// <param name="matrix">矩阵</param>
    /// <returns>转置矩阵</returns>
    private Guid[][] MatrixTranspose(Guid[][] matrix)
    {
        //合法性检查
        if (!IsMatrix(matrix))
        {
            throw new Exception("matrix 不是一个矩阵");
        }

        //矩阵中没有元素的情况
        if (matrix.Length == 0)
        {
            return new Guid[][] { };
        }

        Guid[][] result = new Guid[matrix[0].Length][];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = new Guid[matrix.Length];
        }

        //新矩阵生成规则
        for (int i = 0; i < result.Length; i++)
        {
            for (int j = 0; j < result[0].Length; j++)
            {
                result[i][j] = matrix[j][i];
            }
        }
        return result;
    }

    /// <summary>判断一个二维数组是否为矩阵</summary>
    /// <param name="matrix">二维数组</param>
    /// <returns>true:是矩阵 false:不是矩阵</returns>
    private bool IsMatrix(Guid[][] matrix)
    {
        //空矩阵是矩阵
        if (matrix.Length < 1) return true;
        //不同行列数如果不相等，则不是矩阵
        int count = matrix[0].Length; for (int i = 1; i < matrix.Length; i++)
        {
            if (matrix[i].Length != count)
            {
                return false;
            }
        }
        //各行列数相等，则是矩阵
        return true;
    }

    /// <summary>
    /// 转化矩阵到List
    /// </summary>
    /// <param name="orgs"></param>
    /// <param name="matrix"></param>
    private List<MOrgLevelTreeVo> ConvertMatrix(List<MOrgVo> orgs, Guid[][] matrix)
    {
        var molvs = new List<MOrgLevelTreeVo>();
        for (int i = 0; i < matrix.Length; i++)
        {
            var molv = new MOrgLevelTreeVo();
            for (int j = 0; j < matrix[i].Length; j++)
            {
                var key = matrix[i][j];
                var org = orgs.Where(m => m.Id == key).FirstOrDefault();
                switch (j)
                {
                    case 0:
                        molv.HeadOfficeID = org;
                        break;
                    case 1:
                        molv.DbID = org;
                        break;
                    case 2:
                        molv.MarketingCenterID = org;
                        break;
                    case 3:
                        molv.SaleRegionID = org;
                        break;
                    case 4:
                        molv.DepartmentID = org;
                        break;
                    case 5:
                        molv.StationID = org;
                        break;
                }
            }

            if (molv.HeadOfficeID == null
                && molv.DbID == null
                && molv.MarketingCenterID == null
                && molv.SaleRegionID == null
                && molv.DepartmentID == null
                && molv.StationID == null) { }
            else
                molvs.Add(molv);
        }
        return molvs;
    }

    /// <summary>
    /// 空补位
    /// </summary>
    /// <param name="max"></param>
    /// <param name="length"></param>
    /// <param name="guids"></param>
    private void AppendEmpt(int max, int length, ref List<Guid> guids)
    {
        if (guids == null || !guids.Any())
            guids.Add(Guid.Empty);

        var step = (max <= 1) ? 1 : max - length;
        if (step > 0)
        {
            for (var i = 0; i < step; i++)
            {
                guids.Add(Guid.Empty);
            }
        }
    }

    /// <summary>
    /// 筛选指定Type的组织
    /// </summary>
    /// <param name="orgs"></param>
    /// <param name="enumType"></param>
    /// <returns></returns>
    private List<MOrgVo> ToMOrgVo(IList<MOrgVo> orgs, OrgEnumType enumType)
    {
        return orgs.Where(s => s.Type == (int)enumType)
            .OrderBy(s => s.Type)
            .Select(s => s)
            .ToList();
    }

    /// <summary>
    /// 筛选指定Type的组织
    /// </summary>
    /// <param name="orgs"></param>
    /// <param name="enumType"></param>
    /// <returns></returns>
    private List<Guid> ToMOrgVoKey(IList<MOrgVo> orgs, OrgEnumType enumType)
    {
        return orgs.Where(s => s.Type == (int)enumType)
            .OrderBy(s => s.Type)
            .Select(s => s.Id)
            .ToList();
    }

    /// <summary>
    /// 解析当前访问用户的角色组织树（合并用户角色）
    /// </summary>
    /// <returns></returns>
    [Obsolete("该方法已经弃用，请使用 CurrentMOrg 替代")]
    public async Task<List<TreeResultModel<Guid, MOrgTreeVo>>> CurrentAccountAROSTree()
    {
        var key = _cacheKeys.CurrentAccountAROSTree(_account.Id);
        var list = await _cacheHandler.Get<List<TreeResultModel<Guid, MOrgTreeVo>>>(key);
        if (list != null && list.Any())
        {
            return list;
        }

        List<string> aros = new();

        this.AROS.ForEach(s => { aros.AddRange(s.Orgs); });

        if (aros.Count == 0)
            return new();

        var alls = await GetForCache();
        var differents = new HashSet<string>(aros).Select(s => s.ToGuid()).ToList();
        var orgs = alls.Where(s => differents.Contains(s.Id)).ToList();
        var retTree = ResolveAROrgTree(orgs, Guid.Empty, "", new());

        if (retTree.Any())
            await _cacheHandler.Set(key, retTree);
        else
            await _cacheHandler.Set(key, retTree, new TimeSpan(0, 60, 0));

        return retTree;
    }

    /// <summary>
    /// 获取组织查询缓存
    /// </summary>
    /// <returns></returns>
    private async Task<IList<MOrgVo>> GetForCache()
    {
        var key = _cacheKeys.MORGTree();
        var list = await _cacheHandler.Get<IList<MOrgVo>>(key);
        if (list != null && list.Any())
        {
            return list;
        }

        //取关键字段
        var query = await _mOrgRepository
             .Find()
             .Select(s => new
             {
                 Id = s.Id,
                 ParentId = s.ParentId,
                 OrgCode = s.OrgCode,
                 OrgName = s.OrgName,
                 Type = s.Type,
                 Attribute = s.Attribute
             }).ToList();

        //to MOrgVo
        list = query.Select(s => new MOrgVo
        {
            Id = s.Id,
            ParentId = s.ParentId,
            OrgCode = s.OrgCode,
            Name = s.OrgName,
            Type = s.Type,
            Attribute = s.Attribute
        }).ToList();

        if (list != null && list.Any())
            await _cacheHandler.Set(key, list);
        else
            await _cacheHandler.Set(key, list, new TimeSpan(0, 60, 0));

        return list;
    }

    /// <summary>
    /// 获取对象表查询缓存
    /// </summary>
    /// <returns></returns>
    private async Task<IList<MObjectEntity>> GetMObjectForCache()
    {
        var key = _cacheKeys.MObject();
        var list = await _cacheHandler.Get<IList<MObjectEntity>>(key);
        if (list != null && list.Any())
            return list;

        list = await _mObjectRepository.Find().ToList();

        if (list != null && list.Any())
            await _cacheHandler.Set(key, list);
        else
            await _cacheHandler.Set(key, list, new TimeSpan(0, 60, 0));

        return list;
    }

    /// <summary>
    /// 解析角色组织树(带节点路径)
    /// </summary>
    /// <param name="all"></param>
    /// <param name="parentId"></param>
    /// <param name="name"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    private List<TreeResultModel<Guid, MOrgTreeVo>> ResolveAROrgTree(IList<MOrgVo> all, Guid parentId, string name, List<string> path)
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

                if (!temp.Contains(m.Name))
                    temp.Add(m.Name);

                return new TreeResultModel<Guid, MOrgTreeVo>
                {
                    Id = m.Id,
                    Label = m.Name,
                    ParentId = m.ParentId,
                    Level = m.Type,
                    Path = temp,
                    Children = ResolveAROrgTree(all, m.Id, m.Name, temp)
                };

            }).ToList();


        if (!string.IsNullOrEmpty(name) && !temp.Contains(name))
            temp.Add(name);

        return ret;
    }

    /// <summary>
    /// 解析当前用户角色对应的(组织关系)
    /// </summary>
    private List<AROVo> ResolveAROS(Guid accountId, IList<RoleEntity> roles, IList<AccountRoleEntity> accountRoles, IList<AccountRoleOrgEntity> accountRoleOrgs)
    {
        //当前用户角色组织
        var aros = roles?.Select(s =>
        {
            //当前用户角色组织
            var query = from r in roles
                        join ar in accountRoles on r.Id equals ar.RoleId
                        join aro in accountRoleOrgs on ar.Id equals aro.Account_RoleId
                        where r.Locked == false && ar.AccountId == accountId && r.Id == s.Id
                        select aro.OrgId;

            return new AROVo
            {
                RoleId = s.Id,
                RoleName = s.Name,
                Orgs = query?.ToList()
            };

        })?.ToList();

        return aros;
    }

    /// <summary>
    /// 解析当前用户角色对应的(组织关系)
    /// </summary>
    private async Task<List<AROVo>> ResolveAROS(Guid id)
    {
        var accountId = id.IsEmpty() ? _account.Id : id;
        if (accountId.IsEmpty())
            return new();

        var roles = await _accountRoleRepository.QueryRole(accountId);
        var accountRoles = await _accountRoleRepository.QueryByAccount(accountId);
        var accountRoleOrgs = await _accountRoleOrgRepository.QueryByAccountRole(accountRoles?.Select(s => s.Id)?.ToList());

        return ResolveAROS(accountId, roles, accountRoles, accountRoleOrgs);
    }

    /// <summary>
    /// 解析菜单
    /// </summary>
    /// <param name="menus"></param>
    /// <param name="buttons"></param>
    /// <param name="parent"></param>
    private void ResolveMenu(IList<MenuEntity> menus, IList<RoleButtonEntity> buttons, ProfileMenuVo parent)
    {
        parent.Children = new List<ProfileMenuVo>();
        var children = menus.Where(m => m.ParentId == parent.Id).ToList();
        foreach (var child in children)
        {
            child.MenuId = child.Id;

            var menuVo = _mapper.Map<ProfileMenuVo>(child);

            menuVo.MenuId = child.Id;

            if (child.LocalesConfig.NotNull())
            {
                menuVo.Locales = _jsonHelper.Deserialize<MenuLocales>(child.LocalesConfig);
            }

            menuVo.Buttons = buttons.Where(m => m.MenuId == child.Id).Select(m => m.ButtonCode.ToLower()).ToList();

            parent.Children.Add(menuVo);

            ResolveMenu(menus, buttons, menuVo);
        }
    }

}

//public delegate Task<string> BuildSqlWhereStrByOrgAuthFunc(IMOrgIevelIdsDto dto, OrgEnumType defaultGetLevel = OrgEnumType.Station, string objAlias = "mobj");

