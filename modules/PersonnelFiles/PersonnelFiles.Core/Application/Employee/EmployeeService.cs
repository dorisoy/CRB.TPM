using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Cache.Abstractions;
using CRB.TPM.Config.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.Account;
using CRB.TPM.Mod.Admin.Core.Application.Account.Dto;
using CRB.TPM.Mod.Admin.Core.Application.Account.Vo;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Mod.Admin.Core.Domain.AccountRole;
using CRB.TPM.Mod.Admin.Core.Infrastructure;
using CRB.TPM.Mod.PS.Core.Application.Employee.Dto;
using CRB.TPM.Mod.PS.Core.Application.Employee.Vo;
using CRB.TPM.Mod.PS.Core.Application.EmployeeLatestSelect.Dto;
using CRB.TPM.Mod.PS.Core.Application.EmployeeLeaveInfo.Vo;
using CRB.TPM.Mod.PS.Core.Domain.Department;
using CRB.TPM.Mod.PS.Core.Domain.Employee;
using CRB.TPM.Mod.PS.Core.Domain.EmployeeLatestSelect;
using CRB.TPM.Mod.PS.Core.Domain.EmployeeLeaveInfo;
using CRB.TPM.Mod.PS.Core.Infrastructure;
using CRB.TPM.Utils.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.PS.Core.Application.Employee;

/// <summary>
/// 用于表示员工服务
/// </summary>
public class EmployeeService : IEmployeeService
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _repository;
    private readonly IAccountService _accountService;
    private readonly IAccountRepository _accountRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IEmployeeLeaveInfoRepository _leaveInfoRepository;
    private readonly IEmployeeLatestSelectRepository _latestSelectRepository;
    private readonly AdminDbContext _adminDbContext;
    private readonly PSDbContext _dbContext;
    private readonly ICacheProvider _cacheHandler;
    private readonly IConfigProvider _configProvider;
    private readonly PSCacheKeys _cacheKeys;


    public EmployeeService(IMapper mapper,
        IEmployeeRepository repository,
        IAccountService accountService,
        IAccountRepository accountRepository,
        AdminDbContext adminDbContext,
        PSDbContext dbContext,
        IEmployeeLeaveInfoRepository leaveInfoRepository,
        IEmployeeLatestSelectRepository latestSelectRepository,
        ICacheProvider cacheHandler,
        IDepartmentRepository departmentRepository,
        IAccountRoleRepository accountRoleRepository,
        IConfigProvider configProvider,
        PSCacheKeys cacheKeys)
    {
        _mapper = mapper;
        _repository = repository;
        _accountService = accountService;
        _accountRepository = accountRepository;
        _adminDbContext = adminDbContext;
        _dbContext = dbContext;
        _leaveInfoRepository = leaveInfoRepository;
        _latestSelectRepository = latestSelectRepository;
        _cacheHandler = cacheHandler;
        _departmentRepository = departmentRepository;
        _accountRoleRepository = accountRoleRepository;
        _configProvider = configProvider;
        _cacheKeys = cacheKeys;
    }

    #region ==基本信息==

    /// <summary>
    /// 分页查询员工
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<PagingQueryResultModel<EmployeeEntity>> Query(EmployeeQueryDto dto)
    {
        if (dto.JobNo != null)
        {
            //dto.JobNo = dto.JobNo;
        }
        var accounts = await _accountRepository.Find().Select(m => new { m, Creator = m.Name }).ToList();
        return await _repository.Query(dto, accounts);
    }

    /// <summary>
    /// 添加员工
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<IResultModel> Add(EmployeeAddDto dto)
    {
        try
        {
            if (dto.Password.NotNull() && !dto.Password.Equals(dto.ConfirmPassword))
                return ResultModel.Failed("两次输入的密码不同");

            var entity = _mapper.Map<EmployeeEntity>(dto);
            entity.JoinDate = entity.JoinDate.Date;

            //var account = new EmployeeAddDto
            //{
            //    //默认员工账户
            //    Type = AccountType.User,
            //    Name = dto.Name,
            //    Username = dto.UserName,
            //    Password = dto.Password,
            //    IsLock = true,
            //    Roles = dto.Roles,
            //    Status = AccountStatus.Active
            //};

            //var result = await _accountService.CreateAccount(account);
            //if (result.Successful)
            //{
            //    entity.AccountId = result.Data;
            //    if (await _repository.Add(entity))
            //    {
            //        var key = _cacheKeys.EmployeeTree();
            //        await _cacheHandler.Remove(key);
            //        return ResultModel.Success();
            //    }
            //}

            return ResultModel.Success();


        }
        catch (Exception ex)
        {
            throw;
        }
    }

    /// <summary>
    /// 删除员工
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResultModel> Delete(Guid id)
    {
        try
        {
            var employee = await _repository.Find(w => w.Id == id)
                .Select(s => new { id = s.Id, AccountId = s.AccountId })
                .ToFirst();

            if (employee == null)
                return ResultModel.Failed("员工信息不存在");

            var account = await _accountRepository.Get(employee.AccountId);
            if (account == null)
                return ResultModel.Failed("账户信息不存在");

            using var uow = _dbContext.NewUnitOfWork();
            using var adminUow = _adminDbContext.NewUnitOfWork();

            var result = await _repository.SoftDelete(id, uow);
            if (result)
            {
                result = await _accountRepository.SoftDelete(account.Id, adminUow);
                if (result)
                {
                    uow.SaveChanges();
                    adminUow.SaveChanges();

                    //清除缓存
                    var key1 = _cacheKeys.EmployeeTree();
                    var key2 = _cacheKeys.EmployeeBaseInfo(employee.Id);

                    await _cacheHandler.Remove(key1);
                    await _cacheHandler.Remove(key2);

                    return ResultModel.Success();
                }
            }
            return ResultModel.Failed();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    /// <summary>
    /// 编辑员工
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResultModel> Edit(Guid id)
    {
        var entity = await _repository.Get(id);
        if (entity == null)
            return ResultModel.NotExists;

        var model = _mapper.Map<EmployeeUpdateDto>(entity);
        return ResultModel.Success(model);
    }

    /// <summary>
    /// 更新员工
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<IResultModel> Update(EmployeeUpdateDto model)
    {
        var entity = await _repository.Get(model.Id);
        if (entity == null)
            return ResultModel.NotExists;

        _mapper.Map(model, entity);
        entity.JoinDate = entity.JoinDate.Date;

        var result = await _repository.Update(entity);
        if (result)
        {
            var account = await _accountRepository.Get(entity.AccountId);
            if (account != null)
            {
                var syncModel = new AccountSyncVo
                {
                    Id = account.Id,
                    Name = entity.Name,
                    Email = account.Email,
                    Phone = account.Phone,
                    Roles = null,
                    UserName = account.Username
                };

                await _accountService.Sync(syncModel);
            }

            //清除缓存
            await _cacheHandler.Remove(_cacheKeys.EmployeeTree());
            await _cacheHandler.Remove(_cacheKeys.EmployeeBaseInfo(entity.Id));
        }
        return ResultModel.Result(result);
    }


    /// <summary>
    /// 离职
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<IResultModel> Leave(EmployeeLeaveVo model)
    {
        var entity = await _repository.Get(model.EmployeeId);
        if (entity == null)
            return ResultModel.NotExists;
        if (entity.Status == EmployeeStatus.Leave)
            return ResultModel.Failed("该人员已办理过离职");

        using var uow = _dbContext.NewUnitOfWork();
        var result = await _repository.UpdateLeaveStatus(model.EmployeeId, uow);
        if (result)
        {
            var leaveInfo = _mapper.Map<EmployeeLeaveInfoEntity>(model);
            leaveInfo.EmployeeId = model.EmployeeId;
            result = await _leaveInfoRepository.Add(leaveInfo, uow);
            if (result)
            {
                uow.SaveChanges();

                //清除缓存
                await _cacheHandler.Remove(_cacheKeys.EmployeeTree());
                await _cacheHandler.Remove(_cacheKeys.EmployeeBaseInfo(entity.Id));

                return ResultModel.Success();
            }
        }
        return ResultModel.Failed();
    }

    /// <summary>
    /// 获取离职信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResultModel> GetLeaveInfo(Guid id)
    {
        var info = await _leaveInfoRepository.GetByEmployeeId(id);
        return ResultModel.Success(info);
    }

    #endregion

    #region ==账户更新==

    /// <summary>
    /// 账户编辑
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResultModel> EditAccount(Guid id)
    {
        var employee = await _repository.Get(id);
        if (employee == null)
            return ResultModel.NotExists;

        var accountTask = _accountRepository.Get(employee.AccountId);
        var rolesTask = _accountRoleRepository.QueryRole(employee.AccountId);

        var account = await accountTask;
        var roles = await rolesTask;

        var model = new EmployeeUpdateDto
        {
            Id = id,
            UserName = account.Username,
            Roles = roles.Select(m => m.Id).ToList()
        };

        return ResultModel.Success(model);
    }

    /// <summary>
    /// 账户更新
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<IResultModel> UpdateAccount(EmployeeUpdateDto dto)
    {
        var employee = await _repository.Get(dto.Id);
        if (employee == null)
            return ResultModel.NotExists;

        if (await _accountRepository.ExistsUsername(dto.UserName, employee.AccountId))
            return ResultModel.Failed("用户名已存在");

        var account = await _accountRepository.Get(employee.AccountId);
        if (account != null)
        {
            var syncModel = new AccountSyncVo
            {
                Id = account.Id,
                Name = account.Name,
                Email = account.Email,
                Phone = account.Phone,
                Roles = dto.Roles,
                UserName = dto.UserName,
                NewPassword = dto.Password
            };

            var result = await _accountService.Sync(syncModel);
            return ResultModel.Result(result.Successful);
        }

        return ResultModel.Failed();
    }

    #endregion

    #region ==人员选择==

    public async Task<PagingQueryResultModel<EmployeeEntity>> QueryWithSameDepartment(Guid accountId, EmployeeQueryDto dto)
    {
        var employee = await _repository.GetByAccountId(accountId);
        if (employee == null)
            return new PagingQueryResultModel<EmployeeEntity>();

        dto.DepartmentId = employee.DepartmentId;

        var accounts = await _accountRepository.Find().Select(m => new { m, Creator = m.Name }).ToList();

        var result = await _repository.Query(dto, accounts);

        result.Data.Rows.Remove(result.Data.Rows.FirstOrDefault(m => m.Id == employee.Id));
        return result;
    }

    public async Task<PagingQueryResultModel<EmployeeLatestSelectEntity>> QueryLatestSelect(Guid accountId, EmployeeLatestSelectQueryDto dto)
    {
        var employee = await _repository.GetByAccountId(accountId);
        if (employee != null)
            return await _latestSelectRepository.Query(employee.Id, dto);
        else
            return new PagingQueryResultModel<EmployeeLatestSelectEntity>();
    }

    public async Task<IResultModel> SaveLatestSelect(Guid accountId, List<Guid> ids)
    {
        var employee = await _repository.GetByAccountId(accountId);
        if (employee == null)
            return ResultModel.Success();

        if (ids != null && ids.Any())
        {
            var uow = _dbContext.NewUnitOfWork();
            foreach (var relationId in ids)
            {
                var entity = await _latestSelectRepository.Get(employee.Id, relationId, uow);
                if (entity == null)
                {
                    entity = new EmployeeLatestSelectEntity
                    {
                        EmployeeId = employee.Id,
                        RelationId = relationId,
                        Timestamp = DateTime.Now.ToTimestamp()
                    };

                    await _latestSelectRepository.Add(entity, uow);
                }
                else
                {
                    entity.Timestamp = DateTime.Now.ToTimestamp();
                    await _latestSelectRepository.Update(entity, uow);
                }
            }

            uow.SaveChanges();
        }

        return ResultModel.Success();
    }

    public async Task<IResultModel> GetTree()
    {
        var key = _cacheKeys.EmployeeTree();

        var root = await _cacheHandler.Get<TreeResultModel<Guid, EmployeeTreeVo>>(key);

        if (root != null)
            return ResultModel.Success(root);

        var allDepart = await _departmentRepository.Find().ToList();
        var config = _configProvider.Get<PSConfig>();
        root = new TreeResultModel<Guid, EmployeeTreeVo>
        {
            Label = config.CompanyName,
            Item = new EmployeeTreeVo
            {
                SourceId = "",
                Name = config.CompanyName
            }
        };

        root.Children = ResolveTree(allDepart, Guid.Empty, root);

        root.Id = Guid.Empty;

        if (root.Children.Any())
        {
            await _cacheHandler.Set(_cacheKeys.EmployeeTree(), root);
        }

        return ResultModel.Success(root);
    }

    private List<TreeResultModel<Guid, EmployeeTreeVo>> ResolveTree(IList<DepartmentEntity> all, Guid parentId, TreeResultModel<Guid, EmployeeTreeVo> root)
    {
        return all.Where(m => m.ParentId == parentId).OrderBy(m => m.Sort)
            .Select(m =>
            {
                var node = new TreeResultModel<Guid, EmployeeTreeVo>
                {
                    //Id = ++root.Id,
                    Label = m.Name,
                    Item = new EmployeeTreeVo
                    {
                        SourceId = m.Id.ToString(),
                        Name = m.Name
                    },
                    Children = new List<TreeResultModel<Guid, EmployeeTreeVo>>()
                };

                node.Children.AddRange(ResolveTree(all, m.Id, root));

                var employeeList = _repository.QueryByDepartment(m.Id).Result;
                if (employeeList.Any())
                {
                    foreach (var employeeEntity in employeeList)
                    {
                        //排除已离职人员
                        if (employeeEntity.Status == EmployeeStatus.Leave)
                            continue;

                        node.Children.Add(new TreeResultModel<EmployeeTreeVo>
                        {
                            //Id = ++root.Id,
                            Label = employeeEntity.Name,
                            Item = new EmployeeTreeVo
                            {
                                Type = 1,
                                SourceId = employeeEntity.Id.ToString(),
                                Name = employeeEntity.Name,
                                PostName = employeeEntity.PostName,
                                Sex = employeeEntity.Sex,
                                DepartmentPath = employeeEntity.DepartmentPath
                            }
                        });
                    }
                }

                return node;
            }).ToList();
    }

    public async Task<IResultModel> GetBaseInfoList(List<Guid> ids)
    {
        var list = new List<EmployeeEntity>();
        if (ids != null && ids.Any())
        {
            foreach (var id in ids)
            {

                var key = _cacheKeys.EmployeeBaseInfo(id);
                var employee = await _cacheHandler.Get<EmployeeEntity>(key);
                if (employee == null)
                {
                    employee = await _repository.GetWidthExtend(id);
                    if (employee != null)
                    {
                        await _cacheHandler.Set(key, employee);
                    }
                }

                if (employee != null)
                {
                    list.Add(employee);
                }
            }
        }

        return ResultModel.Success(list);
    }

    #endregion
}
