using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Cache.Abstractions;
using CRB.TPM.Config.Abstractions;
using CRB.TPM.Data.Abstractions.Entities;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.Account.Dto;
using CRB.TPM.Mod.Admin.Core.Application.Account.Vo;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Mod.Admin.Core.Domain.AccountRole;
using CRB.TPM.Mod.Admin.Core.Domain.AccountSkin;
using CRB.TPM.Mod.Admin.Core.Domain.Role;
using CRB.TPM.Mod.Admin.Core.Infrastructure;
using CRB.TPM.Utils.Map;
using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using CRB.TPM.Mod.Admin.Core.Domain.AccountRoleOrg;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Repositories;
using Microsoft.Identity.Client;
using IAccount = CRB.TPM.Auth.Abstractions.IAccount;

namespace CRB.TPM.Mod.Admin.Core.Application.Account;

public class AccountService : IAccountService
{
    private readonly IMapper _mapper;

    private readonly IAccount _account;
    private readonly IPasswordHandler _passwordHandler;
    private readonly IAccountSkinRepository _skinRepository;
    private readonly IConfigProvider _configProvider;

    private readonly IAccountRepository _repository;
    private readonly IRoleRepository _roleRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IAccountRoleOrgRepository _accountRoleOrgRepository;

    private readonly AdminDbContext _dbContext;
    private readonly IPlatformProvider _platformProvider;
    private readonly ICacheProvider _cacheHandler;
    private readonly AdminCacheKeys _cacheKeys;
    private readonly IAccountResolver _accountResolver;

    public AccountService(IMapper mapper, 
        IAccountRepository repository, 
        IPasswordHandler passwordHandler, 
        IRoleRepository roleRepository, 
        IAccountSkinRepository skinRepository,
        IConfigProvider configProvider,
        IAccountRoleRepository accountRoleRepository,
        IAccountRoleOrgRepository accountRoleOrgRepository,
        IAccountResolver accountResolver,
        IPlatformProvider platformProvider,
        ICacheProvider cacheHandler,
        AdminCacheKeys cacheKeys,
        IAccount account,
        AdminDbContext dbContext)
    {
        _mapper = mapper;
        _repository = repository;
        _passwordHandler = passwordHandler;
        _roleRepository = roleRepository;
        _skinRepository = skinRepository;
        _configProvider = configProvider;
        _accountRoleRepository = accountRoleRepository;
        _accountRoleOrgRepository = accountRoleOrgRepository;
        _accountResolver = accountResolver;
        _dbContext = dbContext;
        _platformProvider = platformProvider;
        _cacheHandler = cacheHandler;
        _cacheKeys = cacheKeys;
        _account = account;
    }

    /// <summary>
    /// 查询账户角色
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<PagingQueryResultModel<AccountEntity>> Query(AccountQueryDto dto)
    {
        var result = await  _repository.Query(dto);
        if (!_account.IsAdmin())
            result.Data.Rows.RemoveWhere(s => s.IsAdmin);

        foreach (var item in result.Data.Rows)
        {
            var roles = await _accountRoleRepository.QueryRole(item.Id);
            var accountRoles = await _accountRoleRepository.QueryByAccount(item.Id);
            var accountRoleOrgs = await _accountRoleOrgRepository.QueryByAccountRole(accountRoles?.Select(s => s.Id)?.ToList());
            var aros = new List<string>();
            var orms = new List<OptionResultModel>();
            if (roles != null && roles.Any())
            {
                //角色
                foreach (var s in roles)
                {
                    //当前用户角色对应的组织
                    var query = from r in roles
                                join ar in accountRoles on r.Id equals ar.RoleId
                                join aro in accountRoleOrgs on ar.Id equals aro.Account_RoleId
                                where r.Locked == false && ar.AccountId == item.Id && r.Id == s.Id
                                select aro.OrgId;

                    aros.AddRange(query);

                    //当前角色的组织
                    //var arost = new HashSet<string>(query).Select(s => new { orgId = s.ToGuid() }.ToExpando()).ToList();
                    //var arost = await _accountResolver.CurrentMOrg(aros.Select(s => s).ToList());
                    var diffs = new HashSet<string>(query).Select(s => s).ToList();
                    var arost = await _accountResolver.CurrentMOrg(diffs);
                    orms.Add(new OptionResultModel
                    {
                        Label = s.Name,
                        Value = s.Id,
                        Extends = arost
                    });
                }
            }

            item.Roles = orms;
            item.AROS = new HashSet<string>(aros).Select(s => new { orgId = s.ToGuid() }.ToExpando()).ToList();
            item.MOrgs = await _accountResolver.CurrentMOrg(aros.Select(s=> s).ToList());

            item.AccountRoles = accountRoles?
                .Select(r => (new { roleId = r.RoleId, id = r.Id }).ToExpando())?
                .ToList();

            item.AccountRoleOrgs = accountRoleOrgs?
                .Select(r => (new { orgId = r.OrgId, id = r.Id }).ToExpando())?
                .ToList();

        }

        return result;
    }




    /// <summary>
    /// 查询账户角色
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<PagingQueryResultModel<dynamic>> Query(AccountSelectQueryDto dto)
    {
        var result = await _repository.Query(dto);
        return result;
    }

    //public async Task<PagingQueryResultModel<AccountSelectVo>> Query2(AccountSelectQueryDto dto)
    //{
    //    var result = await _repository.Query2(dto);
    //    return result;
    //}


    /// <summary>
    /// 添加账户
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<IResultModel> Add(AccountAddDto dto)
    {
        //if (await _repository.ExistsUsername(dto.Username))
        //    return ResultModel.Failed("用户名已存在");
        //if (dto.Phone.NotNull() && await _repository.ExistsPhone(dto.Phone))
        //    return ResultModel.Failed("手机号已存在");
        //if (dto.Email.NotNull() && await _repository.ExistsUsername(dto.Email))
        //    return ResultModel.Failed("邮箱已存在");

        //if (!await _roleRepository.Exists(dto.RoleId))
        //    return ResultModel.Failed("绑定角色不存在");


        if (!dto.Roles?.Any() ?? false)
            return ResultModel.Failed("请指定角色");

        var account = _mapper.Map<AccountEntity>(dto);

        var exists = await Exists(account);
        if (!exists.Successful)
            return exists;

        if (account.Password.IsNull())
        {
            var config = _configProvider.Get<AdminConfig>();
            account.Password = config.DefaultPassword;
        }

        account.Password = _passwordHandler.Encrypt(account.Password);
        var result = await _repository.Add(account);
        return ResultModel.Result(result);
    }

    /// <summary>
    /// 添加账户
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<IResultModel<Guid>> CreateAccount(AccountAddDto dto)
    {
        var result = new ResultModel<Guid>();

        //if (await _repository.ExistsUsername(dto.Username))
        //    return result.Failed("用户名已存在");
        //if (dto.Phone.NotNull() && await _repository.ExistsPhone(dto.Phone))
        //    return result.Failed("手机号已存在");
        //if (dto.Email.NotNull() && await _repository.ExistsUsername(dto.Email))
        //    return result.Failed("邮箱已存在");

        if (!dto.Roles?.Any() ?? false)
            return result.Failed("请指定角色");

        var account = _mapper.Map<AccountEntity>(dto);

        var exists = await Exists(account);
        if (!exists.Successful)
            return exists;

        if (account.Password.IsNull())
        {
            var config = _configProvider.Get<AdminConfig>();
            account.Password = config.DefaultPassword;
        }

        account.Password = _passwordHandler.Encrypt(account.Password);
        var success = await _repository.Add(account);
        if (success)
        {
            if (dto.Roles != null && dto.Roles.Any())
            {
                var accountRoleList = dto.Roles.Select(m => new AccountRoleEntity { AccountId = account.Id, RoleId = m }).ToList();
                if (await _accountRoleRepository.BulkAdd(accountRoleList))
                {
                    return result.Success(account.Id);
                }
                else 
                {
                    return result.Failed("添加失败");
                }
            }
            else
            {
                return result.Success(account.Id);
            }
        }
        else
        {
            return result.Failed("添加失败");
        }
    }

    /// <summary>
    /// 编辑账户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResultModel> Edit(Guid id)
    {
        var account = await _repository.Get(id);
        if (account == null)
            return ResultModel.NotExists;

        var dto = _mapper.Map<AccountUpdateDto>(account);
        dto.Password = "";

        var roles = await _accountRoleRepository.QueryRole(id);
        dto.Roles = roles.Select(m => m.Id).ToList();

        return ResultModel.Success(dto);
    }

    /// <summary>
    /// 更新账户
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<IResultModel> Update(AccountUpdateDto dto)
    {
        var account = await _repository.Get(dto.Id);
        if (account == null)
            return ResultModel.Failed("账户不存在！");

        if (account.IsLock)
            return ResultModel.Failed("账户锁定，不允许修改");

        //if (dto.Phone.NotNull() && await _repository.ExistsPhone(dto.Phone, dto.Id))
        //    return ResultModel.Failed("手机号已存在");

        //if (dto.Email.NotNull() && await _repository.ExistsUsername(dto.Email, dto.Id))
        //    return ResultModel.Failed("邮箱已存在");

        if (!dto.Roles?.Any() ?? false)
            return ResultModel.Failed("请指定角色");

        var exists = await Exists(account);
        if (!exists.Successful)
            return exists;

        var username = account.Username;
        var password = account.Password;
        _mapper.Map(dto, account);

        //用户名和密码不允许修改
        account.Username = username;
        account.Password = password;

        //if (dto.NewPassword.NotNull())
        //{
        //    account.Password = _passwordHandler.Encrypt(account.UserName, dto.NewPassword);
        //}
       // var result = await _repository.Update(account);

        using (var uow = _dbContext.NewUnitOfWork())
        {
            var result = await _repository.Update(account, uow);
            if (result)
            {
                if (dto.Roles != null)
                {
                    result = await _accountRoleRepository.DeleteByAccount(account.Id, uow);
                }
                if (dto.Roles != null && dto.Roles.Any())
                {
                    var accountRoleList = dto.Roles.Select(m => new AccountRoleEntity { AccountId = account.Id, RoleId = m }).ToList();
                    result = await _accountRoleRepository.BulkAdd(accountRoleList, 50, uow);
                }

                if (result)
                {
                    uow.SaveChanges();

                    await ClearPermissionListCache(account.Id);

                    await ClearCache(true, account.Id);

                    return ResultModel.Result(result);
                }
            }
        }

        return ResultModel.Failed();

    }

    /// <summary>
    /// 删除账户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResultModel> Delete(Guid id)
    {
        var account = await _repository.Get(id);
        if (account == null)
            return ResultModel.NotExists;

        var result = await _repository.SoftDelete(id);

        return ResultModel.Result(result);
    }

    /// <summary>
    /// 更新用户皮肤（用于自定义主题）
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<IResultModel> UpdateSkin(AccountSkinUpdateDto dto)
    {
        var config = await _skinRepository.Find(m => m.AccountId == dto.AccountId).ToFirst();

        if (config == null)
        {
            config = new AccountSkinEntity
            {
                AccountId = dto.AccountId
            };
        }

        config.Code = dto.Code;
        config.Name = dto.Name;
        config.Theme = dto.Theme;
        config.Size = dto.Size;

        if (config.Id == Guid.Empty)
        {
            return ResultModel.Result(await _skinRepository.Add(config));
        }

        return ResultModel.Result(await _skinRepository.Update(config));
    }

    /// <summary>
    /// 用于账户激活
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<bool> Activate(Guid id)
    {
        return _repository
            .Find(m => m.Id == id)
            .ToUpdate(m => new AccountEntity
            {
                Status = AccountStatus.Active
            });
    }


    /// <summary>
    /// 判断账户是否存在
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    private async Task<IResultModel<Guid>> Exists(AccountEntity entity)
    {
        var result = new ResultModel<Guid>();

        if (await _repository.ExistsUsername(entity.Username, entity.Id))
            return result.Failed("用户名已存在");
        if (entity.Phone.NotNull() && await _repository.ExistsPhone(entity.Phone, entity.Id))
            return result.Failed("手机号已存在");
        if (entity.Email.NotNull() && await _repository.ExistsEmail(entity.Email, entity.Id))
            return result.Failed("邮箱已存在");

        return result.Success(Guid.Empty);
    }

    /// <summary>
    /// 同步账户
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<IResultModel> Sync(AccountSyncVo dto)
    {
        var account = await _repository.Get(dto.Id);
        if (account == null)
            return ResultModel.Failed("账户不存在！");

       _mapper.Map(dto, account);

        var exists = await Exists(account);
        if (!exists.Successful)
            return exists;

        if (dto.NewPassword.NotNull())
        {
            account.Password = _passwordHandler.Encrypt(dto.NewPassword);
        }

        using (var uow = _dbContext.NewUnitOfWork())
        {
            var result = await _repository.Update(account, uow);
            if (result)
            {
                if (dto.Roles != null)
                {
                    result = await _accountRoleRepository.DeleteByAccount(account.Id, uow);
                }
                if (dto.Roles != null && dto.Roles.Any())
                {
                    var accountRoleList = dto.Roles.Select(m => new AccountRoleEntity { AccountId = account.Id, RoleId = m }).ToList();
                    result = await _accountRoleRepository.BulkAdd(accountRoleList, 50, uow);
                }

                if (result)
                {
                    uow.SaveChanges();

                    await ClearPermissionListCache(account.Id);

                    await ClearCache(true, account.Id);

                    return ResultModel.Success();
                }
            }
        }

        return ResultModel.Failed();
    }


    /// <summary>
    /// 更新账户角色组织
    /// </summary>
    /// <returns></returns>
    public async Task<IResultModel> UpdateAccountRoleOrg(AccountRoleOrgUpdateDto dto)
    {
        var account = await _repository.Get(dto.Id);
        if (account == null)
            return ResultModel.Failed("账户不存在！");

        if (account.IsLock)
            return ResultModel.Failed("账户锁定，不允许修改");

        if (!dto.Datas?.Any() ?? false)
            return ResultModel.Failed("请指定角色组织");


        using (var uow = _dbContext.NewUnitOfWork())
        {
            bool result = false;

            var datas = dto.Datas;
            if (datas != null && datas.Any())
            {
                //获取提交的所有角色
                var roles = datas.Select(s => s.RoleId).ToList();
                //获取用户角色映射
                var accountRoles = await _accountRoleRepository.QueryByAccount(dto.Id, roles);
                //删除组织关系
                if (accountRoles != null && accountRoles.Any())
                {
                    result = await _accountRoleOrgRepository.DeleteOrgByAccountRole(accountRoles.Select(s => s.Id).ToList(), uow);
                }

                var accountRoleOrgs = new List<AccountRoleOrgEntity>();
                //遍历提交的当前角色
                foreach (var item in datas)
                {
                    var ar = accountRoles.Where(s => s.RoleId == item.RoleId).FirstOrDefault();
                    if (ar == null)
                        break;

                    //雪花
                    if (item.HeadOffice != null && item.HeadOffice.Any())
                    {
                        item.HeadOffice.ForEach(s =>
                        {
                            var aro = new AccountRoleOrgEntity
                            {
                                Account_RoleId = ar.Id,
                                OrgId = s.ToString()
                            };
                            accountRoleOrgs.Add(aro);
                        });
                    }

                    //事业部
                    if (item.Dbs != null && item.Dbs.Any())
                    {
                        item.Dbs.ForEach(s =>
                        {
                            var aro = new AccountRoleOrgEntity
                            {
                                Account_RoleId = ar.Id,
                                OrgId = s.ToString()
                            };
                            accountRoleOrgs.Add(aro);
                        });
                    }

                    //营销中心
                    if (item.MarketingCenters != null && item.MarketingCenters.Any())
                    {
                        item.MarketingCenters.ForEach(s =>
                        {
                            var aro = new AccountRoleOrgEntity
                            {
                                Account_RoleId = ar.Id,
                                OrgId = s.ToString()
                            };
                            accountRoleOrgs.Add(aro);
                        });
                    }

                    //大区
                    if (item.SaleRegions != null && item.SaleRegions.Any())
                    {
                        item.SaleRegions.ForEach(s =>
                        {
                            var aro = new AccountRoleOrgEntity
                            {
                                Account_RoleId = ar.Id,
                                OrgId = s.ToString()
                            };
                            accountRoleOrgs.Add(aro);
                        });
                    }

                    //业务部
                    if (item.Departments != null && item.Departments.Any())
                    {
                        item.Departments.ForEach(s =>
                        {
                            var aro = new AccountRoleOrgEntity
                            {
                                Account_RoleId = ar.Id,
                                OrgId = s.ToString()
                            };
                            accountRoleOrgs.Add(aro);
                        });
                    }

                    //工作站
                    if (item.Stations != null && item.Stations.Any())
                    {
                        item.Stations.ForEach(s =>
                        {
                            var aro = new AccountRoleOrgEntity
                            {
                                Account_RoleId = ar.Id,
                                OrgId = s.ToString()
                            };
                            accountRoleOrgs.Add(aro);
                        });
                    }
                }

                //批量添加
                if (accountRoleOrgs.Any())
                {
                    result = await _accountRoleOrgRepository.BulkAdd(accountRoleOrgs, 50, uow);
                }
            }


            if (result)
            {
                //更新数据库
                uow.SaveChanges();

                //清除权限缓存
                await ClearPermissionListCache(account.Id);
           
                //清除缓存
                await ClearCache(true, account.Id);

                return ResultModel.Result(result);
            }
        }

        return ResultModel.Success();
    }


    /// <summary>
    /// 清除权限缓存
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task ClearPermissionListCache(Guid id)
    {
        var list= _platformProvider.SelectOptions();
        var tasks = new List<Task>();

        foreach (var t in list)
        {
            var key1 = _cacheKeys.AccountPermissions(id, t.Value.ToInt());
            var key2 = _cacheKeys.AccountPages(id, t.Value.ToInt());
            var key3 = _cacheKeys.AccountButtons(id, t.Value.ToInt());
            var key4 = _cacheKeys.AccountMenus(id, t.Value.ToInt());
            var key5 = _cacheKeys.CurrentAccountAROSTree(id);

            tasks.Add(_cacheHandler.Remove(key1));
            tasks.Add(_cacheHandler.Remove(key2));
            tasks.Add(_cacheHandler.Remove(key3));
            tasks.Add(_cacheHandler.Remove(key4));
            tasks.Add(_cacheHandler.Remove(key5));
        }

        return Task.WhenAll(tasks);
    }


    /// <summary>
    /// 清除缓存
    /// </summary>
    /// <param name="result"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    private Task ClearCache(bool result, Guid id)
    {
        if (result)
        {
            var key = _cacheKeys.Account(id);
            return _cacheHandler.Remove(key);
        }

        return Task.CompletedTask;
    }

}