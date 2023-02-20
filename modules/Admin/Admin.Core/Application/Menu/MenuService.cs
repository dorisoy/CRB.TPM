using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.Menu.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.Menu;
using CRB.TPM.Mod.Admin.Core.Domain.RoleButton;
using CRB.TPM.Mod.Admin.Core.Domain.RoleMenu;
using CRB.TPM.Mod.Admin.Core.Domain.RolePermission;
using CRB.TPM.Mod.Admin.Core.Infrastructure;
using CRB.TPM.Utils.Json;
using CRB.TPM.Utils.Map;

namespace CRB.TPM.Mod.Admin.Core.Application.Menu;

public class MenuService : IMenuService
{
    private readonly IMapper _mapper;
    private readonly IMenuRepository _repository;
    private readonly IRoleMenuRepository _roleMenuRepository;
    private readonly IRoleButtonRepository _roleButtonRepository;
    private readonly IRolePermissionRepository _rolePermissionRepository;
    private readonly JsonHelper _jsonHelper;
    private readonly AdminLocalizer _localizer;

    public MenuService(IMapper mapper, IMenuRepository repository, IRoleMenuRepository roleMenuRepository, IRoleButtonRepository roleButtonRepository, IRolePermissionRepository rolePermissionRepository, JsonHelper jsonHelper, AdminLocalizer localizer)
    {
        _mapper = mapper;
        _repository = repository;
        _roleMenuRepository = roleMenuRepository;
        _roleButtonRepository = roleButtonRepository;
        _rolePermissionRepository = rolePermissionRepository;
        _jsonHelper = jsonHelper;
        _localizer = localizer;
    }

    public Task<PagingQueryResultModel<MenuEntity>> Query(MenuQueryDto dto)
    {

        var query = _repository.Find(m => m.GroupId == dto.GroupId && m.ParentId == dto.ParentId);
        query.OrderBy(m => m.Sort);

        return query.ToPaginationResult(dto.Paging);
    }

    [Transaction]
    public async Task<IResultModel> Add(MenuAddDto dto)
    {
        var menu = _mapper.Map<MenuEntity>(dto);
        menu.Level = 1;

        if (dto.Type == MenuType.Route)
        {
            if (dto.Module.IsNull())
                return ResultModel.Failed(_localizer["模块编码不能为空"]);

            if (dto.RouteName.IsNull())
                return ResultModel.Failed(_localizer["路由名称不能为空"]);

            //var exist = await _repository.Find(s => s.RouteName.Equals(dto.RouteName.ToLower())).ToExists();
            //if (exist)
            //    return ResultModel.Failed(_localizer["路由名称已经添加"]);
        }
        else if (dto.Type == MenuType.Link)
        {
            if (dto.Url.IsNull())
                return ResultModel.Failed(_localizer["Url不能为空"]);
        }
        else if (dto.Type == MenuType.CustomJs)
        {
            if (dto.CustomJs.IsNull())
                return ResultModel.Failed(_localizer["自定义Javascript不能为空"]);
        }

        if (dto.Locales != null)
        {
            menu.LocalesConfig = _jsonHelper.Serialize(dto.Locales);
        }

        if (dto.ParentId != Guid.Empty)
        {
            var parent = await _repository.Get(dto.ParentId);
            if (parent == null)
            {
                return ResultModel.Failed(_localizer["父节点菜单不存在"]);
            }

            menu.Level = parent.Level + 1;
        }

        var result = await _repository.Add(menu);
        return ResultModel.Result(result);
    }

    public async Task<IResultModel> Edit(Guid id)
    {
        var menu = await _repository.Get(id);
        if (menu == null)
            return ResultModel.NotExists;

        var model = _mapper.Map<MenuUpdateDto>(menu);

        if (menu.LocalesConfig.NotNull())
        {
            model.Locales = menu.Locales;
        }

        return ResultModel.Success(model);
    }

    public async Task<IResultModel> Update(MenuUpdateDto dto)
    {
        var menu = await _repository.Get(dto.Id);
        if (menu == null)
            return ResultModel.NotExists;

        _mapper.Map(dto, menu);

        if (dto.Locales != null)
        {
            menu.LocalesConfig = _jsonHelper.Serialize(dto.Locales);
        }

        var result = await _repository.Update(menu);

        return ResultModel.Result(result);
    }

    [Transaction]
    public async Task<IResultModel> Delete(Guid id)
    {
        var result = await _repository.Delete(id);
        if (result)
        {
            //删除角色菜单绑定数据
            await _roleMenuRepository.Find(m => m.MenuId == id).ToDelete();
            //删除角色按钮绑定数据
            await _roleButtonRepository.Find(m => m.MenuId == id).ToDelete();
            //删除角色权限绑定数据
            await _rolePermissionRepository.Find(m => m.MenuId == id).ToDelete();
        }

        return ResultModel.Result(result);
    }

    public async Task<IResultModel> GetTree(Guid groupId)
    {
        var root = new TreeResultModel<MenuEntity>();
        var all = await _repository.Find(m => m.GroupId == groupId).ToList();

        ResolveTree(all, root);

        return ResultModel.Success(root.Children);
    }

    private void ResolveTree(IList<MenuEntity> all, TreeResultModel<MenuEntity> parent)
    {
        foreach (var menu in all.Where(m => m.ParentId == parent.Id).OrderBy(m => m.Sort))
        {
            var child = new TreeResultModel<MenuEntity>
            {
                Id = menu.Id,
                Item = menu
            };

            //只有节点菜单才有子级
            if (menu.Type == MenuType.Node)
            {
                ResolveTree(all, child);
            }

            parent.Children.Add(child);
        }
    }

    [Transaction]
    public async Task<IResultModel> UpdateSort(IList<MenuEntity> menus)
    {
        if (!menus.Any())
            return ResultModel.Success();

        foreach (var menu in menus)
        {
            await _repository.Find(m => m.Id == menu.Id).ToUpdate(m => new MenuEntity
            {
                ParentId = menu.ParentId,
                Sort = menu.Sort
            });
        }

        return ResultModel.Success();
    }

}