using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.Menu.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Domain.Menu;

/// <summary>
/// 菜单仓储
/// </summary>
public interface IMenuRepository : IRepository<MenuEntity>
{
    Task<PagingQueryResultModel<MenuEntity>> Query(MenuQueryDto dto);
    Task<IList<MenuEntity>> QueryChildren(Guid parentId);
    Task<bool> ExistsChild(Guid id);
    Task<MenuEntity> GetAsync(Guid id);
    Task<IList<MenuEntity>> QueryByAccount(Guid accountId);
    Task<IList<MenuEntity>> QueryByRole(Guid roleId);
}