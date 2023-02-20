using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Admin.Core.Domain.AccountRole;
using CRB.TPM.Mod.Admin.Core.Domain.Menu;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Mod.Admin.Core.Domain.RoleMenu;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using CRB.TPM.Mod.Admin.Core.Application.Menu.Dto;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Domain.Role;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure.Repositories;

public class MenuRepository : RepositoryAbstract<MenuEntity>, IMenuRepository
{
    public async Task<PagingQueryResultModel<MenuEntity>> Query(MenuQueryDto dto)
    {
        var query = Find();

        query.WhereNotNull(dto.Name, m => m.LocalesConfig.Contains(dto.Name));
        query.Where(m => m.ParentId == dto.ParentId);

        var joinQuery = query.LeftJoin<AccountEntity>(m => m.T1.CreatedBy == m.T2.Id);

        if (!dto.Paging.OrderBy.Any())
        {
            joinQuery.OrderBy(m => m.T1.Sort);
        }

        joinQuery.Select(m => new { m.T1, CreatorName = m.T2.Name });

        return await joinQuery.ToPaginationResult(dto.Paging);
    }

    public async Task<IList<MenuEntity>> QueryChildren(Guid parentId)
    {
        return await Find(m => m.ParentId == parentId).OrderBy(m => m.Sort).ToList();
    }

    public async Task<bool> ExistsChild(Guid id)
    {
        return await Find(e => e.ParentId == id).ToCount() > 0;
    }

    public async Task<MenuEntity> GetAsync(Guid id)
    {
        var query = Find().LeftJoin<MenuEntity>(m => m.T1.ParentId == m.T2.Id)
            .LeftJoin<AccountEntity>(m => m.T1.CreatedBy == m.T3.Id)
            .Where(m => m.T1.Id == id)
            .Select(m => new { m.T1, ParentName = m.T3.Name, });

        return await query.ToFirst<MenuEntity>();
    }

    public async Task<IList<MenuEntity>> QueryByAccount(Guid accountId)
    {
        return await Find().InnerJoin<RoleMenuEntity>(m => m.T1.Id == m.T2.MenuId)
             .InnerJoin<AccountRoleEntity>(m => m.T2.RoleId == m.T3.RoleId && m.T3.AccountId == accountId)
             .Select(m => new { m.T1 })
             .ToList();
    }

    public async Task<IList<MenuEntity>> QueryByRole(Guid roleId)
    {
        return await Find().InnerJoin<RoleMenuEntity>(m => m.T1.Id == m.T2.MenuId && m.T2.RoleId == roleId)
            .Select(m => new { m.T1 })
            .ToList();
    }

}