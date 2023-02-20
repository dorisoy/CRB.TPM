using System;
using System.Dynamic;
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.Account.Dto;
using CRB.TPM.Mod.Admin.Core.Application.Account.Vo;
using CRB.TPM.Mod.Admin.Core.Domain.Account;

namespace CRB.TPM.Mod.Admin.Core.Application.Account;

public interface IAccountService
{
    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<PagingQueryResultModel<AccountEntity>> Query(AccountQueryDto dto);
    Task<PagingQueryResultModel<dynamic>> Query(AccountSelectQueryDto dto);
    //Task<PagingQueryResultModel<AccountSelectVo>> Query2(AccountSelectQueryDto dto);

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<IResultModel> Add(AccountAddDto dto);

    /// <summary>
    /// 添加账户
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<IResultModel<Guid>> CreateAccount(AccountAddDto dto);

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IResultModel> Edit(Guid id);

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<IResultModel> Update(AccountUpdateDto dto);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IResultModel> Delete(Guid id);

    /// <summary>
    /// 更新皮肤配置
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<IResultModel> UpdateSkin(AccountSkinUpdateDto dto);

    /// <summary>
    /// 激活账户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> Activate(Guid id);

    /// <summary>
    /// 同步账户
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<IResultModel> Sync(AccountSyncVo model);

    /// <summary>
    /// 清除权限缓存
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task ClearPermissionListCache(Guid id);

    /// <summary>
    /// 更新账户角色组织
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<IResultModel> UpdateAccountRoleOrg(AccountRoleOrgUpdateDto dto);
}