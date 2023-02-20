using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Cache.Abstractions;
using CRB.TPM.Config.Core;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Vo;
using CRB.TPM.Mod.Admin.Core.Domain.MObject;
using CRB.TPM.Mod.Admin.Core.Domain.MOrg;
using CRB.TPM.Mod.Admin.Core.Infrastructure;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using CRB.TPM.Utils.ClayObject;
using CRB.TPM.Utils.Map;
using Dapper;
using Microsoft.AspNetCore.Http.Extensions;
using Pipelines.Sockets.Unofficial.Arenas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;
//using System.Linq.Async;

namespace CRB.TPM.Mod.Admin.Core.Application.MOrg;

public abstract class MOrgServiceAbstract : BaseService, IMOrgService
{
    protected readonly AdminCacheKeys _cacheKeys;
    protected readonly IMObjectRepository _mObjectRepository;

    public MOrgServiceAbstract(IMapper mapper,
        ICacheProvider cacheHandler,
        IServiceProvider serviceProvider,
        IMOrgRepository mOrgRepository,
        IAccountResolver accountResolver,
        AdminCacheKeys cacheKeys,
        IMObjectRepository mObjectRepository) : base(mapper, cacheHandler, serviceProvider, mOrgRepository, accountResolver)
    {
        _cacheKeys = cacheKeys;
        _mObjectRepository = mObjectRepository;
    }

    public Task<PagingQueryResultModel<MOrgEntity>> Query(MOrgQueryDto dto)
    {
        var query = _mOrgRepository.Find(f => f.Type > 0);
        return query.ToPaginationResult(dto.Paging);
    }

    [Transaction]
    public async Task<IResultModel> Add(MOrgAddDto dto)
    {
        var entity = _mapper.Map<MOrgEntity>(dto);
        var checkRes = await CheckParm(entity);
        if (checkRes.res)
        {
            return ResultModel.Failed(checkRes.errmsg);
        }
        var result = await _mOrgRepository.Add(entity);
        if (result)
        {
            await ClearCacheAll();
        }
        return ResultModel.Result(result);
    }

    public async Task<IResultModel> Delete(Guid id)
    {
        if (await IsDelete(id))
        {
            var result = await _mOrgRepository.Delete(id);
            if (result)
            {
                await ClearCacheAll();
            }
            return ResultModel.Result(result);
        }
        return ResultModel.Failed("删除失败，当前组织已有下级组织！");
    }

    /// <summary>
    /// 批量删除终端消息
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<IResultModel> DeleteSelected(IEnumerable<Guid> ids)
    {
        IList<Guid> delList = new List<Guid>();
        foreach (var id in ids)
        {
            if (await IsDelete(id))
            {
                delList.Add(id);
            }
        }
        if (delList.Any())
        {
            var delRes = await _mOrgRepository.Delete(delList);
            if (delRes > 0)
            {
                await ClearCacheAll();
            }
        }
        if (delList.Count == ids.Count())
        {
            return ResultModel.Result(true);
        }
        var notDelete = ids.Except(delList);
        var notDeleteMsgList =
            (await _mOrgRepository.Find(f => notDelete.Contains(f.Id)).ToList())
            .Select(s => $"【{s.OrgName}】删除失败，当前组织已有下级组织！");
        string resmsg = string.Join("<br/>", notDeleteMsgList);
        return ResultModel.Failed(resmsg);
    }

    /// <summary>
    /// 是否可以根据节点id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns>返回true可以删除</returns>
    public async Task<bool> IsDelete(Guid id)
    {
        var res = false;
        //删除的时候要判断一下 有没有下级，如果有下级 不允许删除
        res = await _mOrgRepository.Find(f => f.ParentId == id).ToFirst() == null;

        //TODO 政策表里面是否用到了 这个组织，如果用到了 不允许删除

        return res;
    }

    public async Task<IResultModel> Edit(Guid id)
    {
        var entity = await _mOrgRepository.Get(id);
        if (entity == null)
            return ResultModel.NotExists;
        var dto = _mapper.Map<MOrgUpdateDto>(entity);

        return ResultModel.Success(dto);
    }

    /// <summary>
    /// 检查新增/更新参数校验
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<(bool res, string errmsg)> CheckParm(MOrgEntity entity)
    {
        IList<string> errList = new List<string>();
        if (entity.StartTime != null && entity.EndTime != null && entity.StartTime.Value.CompareTo(entity.EndTime.Value) >= 0)
        {
            errList.Add("生效时间不能大于等于失效时间");
        }
        var parentOrg = await _mOrgRepository.Get(entity.ParentId);
        if (parentOrg == null)
        {
            errList.Add("父级组织不存在");
        }
        if (parentOrg != null)
        {
            if (parentOrg.Deleted)
            {
                errList.Add("父级组织已被删除");
            }
            if (parentOrg.Enabled == 0)
            {
                errList.Add("父级组织未启用");
            }
            if (parentOrg.Type == (int)OrgEnumType.Station)
            {
                errList.Add("父级组织不能为工作站");
            }
        }
        if (parentOrg.Type <= (int)OrgEnumType.HeadOffice || parentOrg.Type > (int)OrgEnumType.Station)
        {
            errList.Add("组织层级不在范围内");
        }
        var resByTDID = await _mOrgRepository.Find(f => f.OrgCode == entity.OrgCode).WhereNotEmpty(entity.Id, w => w.Id != entity.Id).Where(w => w.Deleted == false).ToFirst();
        if (resByTDID != null)
        {
            errList.Add("当前组织编码已存在");
        }
        string errmsg = errList.Any() ? string.Join("<br/>", errList) : string.Empty;
        return (errmsg.Length > 0, errmsg);
    }

    [Transaction]
    public async Task<IResultModel> Update(MOrgUpdateDto dto)
    {
        var id = dto.Id;
        var entity = await _mOrgRepository.Get(dto.Id);
        if (entity == null)
            return ResultModel.NotExists;

        _mapper.Map(dto, entity);
        entity.Id = id;
        var checkRes = await CheckParm(entity);
        if (checkRes.res)
        {
            return ResultModel.Failed(checkRes.errmsg);
        }
        //TODO mobject同步更新
        var result = await _mOrgRepository.Update(entity);
        if (result)
        {
            await ClearCacheAll();
        }

        return ResultModel.Result(result);
    }


    /// <summary>
    /// 获取指定层级组织
    /// </summary>
    /// <param name="level"></param>
    /// <param name="ignore">是否忽略aros</param>
    /// <returns></returns>
    public async Task<IResultModel> GetOrgLevel(int? level = 10, bool ignore = false)
    {
        var aros = CurrentAROS;
        var cache = await GetForCache();
        var account = _accountResolver.CurrentAccount;

        var query = cache.AsEnumerable();
        if (account.IsAdmin() || ignore)
            query = query.Where(s => s.Type == level.Value);
        else
            query = query.Where(s => s.Type == level.Value && aros.Contains(s.Id));

        var list = query.Select(s => new
        {
            id = s.Id,
            name = s.OrgName,
            parentId = s.ParentId,
            type = s.Type,
            orgCode = s.OrgCode,
        }).ToList<dynamic>();

        return ResultModel.Success(list);
    }


    /// <summary>
    /// 根据父ID获取组织节点
    /// </summary>
    /// <param name="level"></param>
    /// <param name="parentId"></param>
    /// <param name="ignore"></param>
    /// <returns></returns>
    public async Task<IResultModel> GetNodeByParentId(int? level = 10, Guid? parentId = null, bool ignore = false)
    {
        var aros = CurrentAROS;
        var account = _accountResolver.CurrentAccount;

        var cache = await GetForCache();
        var query = cache.AsEnumerable();

        if (ignore)
            query = query.Where(s => s.Type == level.Value);
        else
            query = query.Where(s => s.Type == level.Value && aros.Contains(s.Id));

        if (parentId.HasValue && parentId != Guid.Empty)
            query = query.Where(s => s.ParentId == parentId);

        var list = query.Select(s => new
        {
            id = s.Id,
            name = s.OrgName,
            parentId = s.ParentId,
            type = s.Type,
            orgCode = s.OrgCode,
        }).ToList<dynamic>();

        return ResultModel.Success(list);
    }



    /// <summary>
    /// 获取组织树
    /// </summary>
    /// <param name="level">表示指定获取几个层级的数据,默认1层</param>
    /// <param name="metadata"></param>
    /// <returns></returns>
    public async Task<IResultModel> GetTree(int? level = 10, bool metadata = false)
    {
        var key = _cacheKeys.MORGTree(level);
        var list = await _cacheHandler.Get<List<TreeResultModel<Guid, MOrgTreeVo>>>(key);
        if (list != null && list.Any())
        {
            return ResultModel.Success(list);
        }

        var cache = await GetForCache();
        var query = cache.AsEnumerable();

        if (level.HasValue && level > 0)
        {
            query = query.Where(s => s.Type <= level && s.Type > 0);
        }

        var all = query.ToList();

        if (all != null && all.Any())
            list = ResolveTree(all, Guid.Empty, metadata);

        if (list != null && list.Any())
            await _cacheHandler.Set(key, list);
        else
            await _cacheHandler.Set(key, list, new TimeSpan(0, 0, 5));

        return ResultModel.Success(list);
    }


    /// <summary>
    /// 根据父级ID获取组织树
    /// </summary>
    /// <param name="level">表示指定获取几个层级的数据,默认1层</param>
    /// <param name="parentId">组织父级ID</param>
    /// <param name="metadata"></param>
    /// <returns></returns>
    public async Task<IResultModel> GetTreeByParentId(int? level = 10, Guid? parentId = null, bool metadata = false)
    {
        var key = _cacheKeys.MORGTree(level, parentId);
        var list = await _cacheHandler.Get<List<TreeResultModel<Guid, MOrgTreeVo>>>(key);
        if (list != null)
        {
            return ResultModel.Success(list);
        }

        var cache = await GetForCache();
        var query = cache.AsEnumerable();
        if (level.HasValue && level > 0)
        {
            query = query.Where(s => s.Type <= level);
        }

        if (parentId.HasValue && parentId != Guid.Empty)
        {
            query = query.Where(s => s.ParentId == parentId);
        }

        var all = query.ToList();

        if (all != null && all.Any())
            list = ResolveTree(all, (parentId.HasValue ? parentId.Value : Guid.Empty), metadata);

        if (list != null && list.Any())
            await _cacheHandler.Set(key, list);
        else
            await _cacheHandler.Set(key, list, new TimeSpan(0, 0, 5));

        return ResultModel.Success(list);
    }

    /// <summary>
    /// 组织联动组件接口
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<PagingQueryResultModel<OrgSelectVo>> Select(OrgSelectDto dto)
    {
        // 如果传了ids数组，那么返回根据level+ids所筛选出来的全部数据，不要分页，用来显示已选数据
        if (dto.Ids.NotNullAndEmpty())
        {
            dto.Page.Size = int.MaxValue;
            return await _mOrgRepository.Find(f => f.Type == dto.Level && dto.Ids.Contains(f.Id))
                    .WhereNotNull(dto.Name, w => w.OrgName.Contains(dto.Name))
                    .Select(s => new { Label = s.OrgName, Value = s.Id })
                    .ToPaginationResultVo<OrgSelectVo>(dto.Paging);
        }

        #region 设置层级对应组织IDs
        if (dto.Level == (int)OrgEnumType.Distributor || !Enum.IsDefined((OrgEnumType)dto.Level))
        {
            return PagingQueryResultModel<OrgSelectVo>.SuccessEmpty();
        }

        var authAllOrg = await _accountResolver.CurrentMOrgs().ToListAsync();
        //如果配置了一级组织，直接取一级组织
        if (authAllOrg.Any(a => a.Level == OrgEnumType.HeadOffice))
        {
            var headIds = authAllOrg.Where(w => w.Level == OrgEnumType.HeadOffice).Select(s => s.HeadOfficeID.Id).ToList();
            dto.Level1Ids = dto.Ids == null ? headIds : headIds.Intersect(dto.Level1Ids).ToList();
            if (!dto.Level1Ids.Any())
            {
                dto.Level1Ids = headIds;
            }
        }
        else
        {
            Dictionary<int, (string authV, string objV)> map = new Dictionary<int, (string authV, string objV)>()
            {
                { 
                    (int)OrgEnumType.HeadOffice, 
                    (nameof(MOrgLevelTreeVo.HeadOfficeID),nameof(MObjectEntity.HeadOfficeId)) 
                },
                { 
                    (int)OrgEnumType.BD, 
                    (nameof(MOrgLevelTreeVo.DbID), nameof(MObjectEntity.DivisionId)) 
                },
                { 
                    (int)OrgEnumType.MarketingCenter, 
                    (nameof(MOrgLevelTreeVo.MarketingCenterID),nameof(MObjectEntity.MarketingId))
                },
                { 
                    (int)OrgEnumType.SaleRegion, 
                    (nameof(MOrgLevelTreeVo.SaleRegionID),nameof(MObjectEntity.BigAreaId)) 
                },
                { 
                    (int)OrgEnumType.Department, 
                    (nameof(MOrgLevelTreeVo.DepartmentID), nameof(MObjectEntity.OfficeId)) 
                },
                { 
                    (int)OrgEnumType.Station, 
                    (nameof(MOrgLevelTreeVo.StationID),nameof(MObjectEntity.StationId)) 
                },
            };

            //没有挂组织直接返回空
            if (!authAllOrg.NotNullAndEmpty() || !map.ContainsKey(dto.Level))
            {
                return PagingQueryResultModel<OrgSelectVo>.SuccessEmpty();
            }

            var mOrgLevelType = typeof(MOrgLevelTreeVo);
            var dtoType = typeof(OrgSelectDto);
            var objType = typeof(MObjectEntity);
            bool isFindMin = false;
            var key = _cacheKeys.MObject();
            var mobjectsCache = await _cacheHandler.Get<IList<MObjectEntity>>(key);
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

            //找出对应层级拥有组织权限
            foreach (var item in map.Where(w => w.Key <= (int)OrgEnumType.Station).OrderByDescending(o => o.Key))
            {
                var minLevelIdsProp = dtoType.GetProperty($"Level{item.Key / 10}Ids");
                var minObjIds = minLevelIdsProp.GetValue(dto);
                var minLevelIds = minObjIds != null ? minObjIds as IList<Guid> : new List<Guid>();
                if ((item.Key == (int)OrgEnumType.HeadOffice || minLevelIds.Any()) && !isFindMin)
                {
                    isFindMin = true;
                    foreach (var m in map.Where(w => w.Key >= item.Key && w.Key <= (int)OrgEnumType.Station).OrderBy(o => o.Key))
                    {
                        //优先查下级组织绑定
                        var upAuthOrg = authAllOrg.Where(w =>
                        {
                            var idObj = mOrgLevelType.GetProperty(map[item.Key].authV).GetValue(w) as MOrgBaseVo;
                            return idObj != null && m.Key == (int)w.Level && (!minLevelIds.Any() || minLevelIds.Contains(idObj.Id));
                        });
                        var queryLevelIdsProp = dtoType.GetProperty($"Level{m.Key / 10}Ids");
                        //查到下级组织有绑定
                        if (upAuthOrg.Any())
                        {
                            var setOrgIds = new List<Guid>();
                            foreach (var authOrg in upAuthOrg)
                            {
                                //查的是下级层级，挂的是上级组织
                                if ((int)authOrg.Level <= dto.Level)
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
                                    queryLevelIdsProp = dtoType.GetProperty($"Level{dto.Level / 10}Ids");
                                    var objIds = queryLevelIdsProp.GetValue(dto);
                                    var queryLevelIds = objIds != null ? objIds as IList<Guid> : new List<Guid>();
                                    var orgIds = upAuthOrg.Select(s => (mOrgLevelType.GetProperty(map[dto.Level].authV).GetValue(s) as MOrgBaseVo).Id).Distinct().ToList();
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
                else if (minLevelIds.NotNullAndEmpty())
                {
                    minLevelIdsProp.SetValue(dto, null);
                }
            }
        }

        //一个组织都没查到返回空行
        if (!dto.AnyInputOrg())
        {
            return PagingQueryResultModel<OrgSelectVo>.SuccessEmpty();
        }

        #endregion

        // 根据不同层级id来分页显示
        return await _mObjectRepository.QueryByLevel(dto);
    }


    /// <summary>
    /// 从获取组织数据
    /// </summary>
    /// <returns></returns>
    public async Task<IList<MOrgEntity>> GetForCache()
    {
        var key = _cacheKeys.MORGTree();
        var list = await _cacheHandler.Get<IList<MOrgEntity>>(key);
        if (list != null && list.Any())
            return list;

        list = await _mOrgRepository.Find().ToList();

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


    /// <summary>
    /// 递归获取最底层org
    /// </summary>
    /// <param name="all"></param>
    /// <param name="parentId"></param>
    /// <param name="res"></param>
    private void ResolveTree(IList<MOrgEntity> all, Guid parentId, ref List<OrgSelectVo> res)
    {
        var list = all.Where(m => m.ParentId == parentId).OrderBy(m => m.Id);
        if (list.Any())
        {
            res.AddRange(list.Select(s => new OrgSelectVo() { Label = s.OrgName, Value = s.Id }));
            foreach (var item in list)
            {
                ResolveTree(all, item.Id, ref res);
            }
        }
    }


    /// <summary>
    /// 解析组织树
    /// </summary>
    /// <param name="all"></param>
    /// <param name="parentId"></param>
    /// <param name="metadata"></param>
    /// <returns></returns>
    private List<TreeResultModel<Guid, MOrgTreeVo>> ResolveTree(IList<MOrgEntity> all, Guid parentId, bool metadata = false)
    {
        return all.Where(m => m.ParentId == parentId).OrderBy(m => m.Id)
            .Select(m => new TreeResultModel<Guid, MOrgTreeVo>
            {
                Id = m.Id,
                Label = m.OrgName,
                Item = metadata ? _mapper.Map<MOrgTreeVo>(m) : null,
                ParentId = m.ParentId,
                Level = m.Type,
                Children = ResolveTree(all, m.Id, metadata)
            }).ToList();
    }


    /// <summary>
    /// 清除缓存
    /// </summary>
    /// <returns></returns>
    private async Task<bool[]> ClearCache(int? type)
    {
        var key = _cacheKeys.MORGTree(type);
        var task = _cacheHandler.Remove(key);
        return await Task.WhenAll(task);
    }

    /// <summary>
    /// 清除所有缓存
    /// </summary>
    /// <returns></returns>
    private async Task ClearCacheAll()
    {
        var key = _cacheKeys.MORGTree();
        await _cacheHandler.RemoveByPrefix(key);
    }
}
