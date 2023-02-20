using CRB.TPM.Cache.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.PS.Core.Infrastructure;
using CRB.TPM.Mod.PS.Core.Application.Department.Dto;
using CRB.TPM.Mod.PS.Core.Application.Department.Vo;
using CRB.TPM.Mod.PS.Core.Domain.Department;
using CRB.TPM.Mod.PS.Core.Domain.Employee;
using CRB.TPM.Utils.Map;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Mod.Admin.Core.Domain.Menu;

namespace CRB.TPM.Mod.PS.Core.Application.Department;

/// <summary>
/// 部门服务
/// </summary>
public class DepartmentService : IDepartmentService
{
    private readonly IMapper _mapper;
    private readonly IDepartmentRepository _repository;
    private readonly IEmployeeRepository _userRepository;
    private readonly ICacheProvider _cacheHandler;
    private readonly PSCacheKeys _cacheKeys;
    private readonly IAccountRepository _accountRepository;

    public DepartmentService(IMapper mapper, IDepartmentRepository repository,
        IEmployeeRepository userRepository,
        IAccountRepository accountRepository,
        ICacheProvider cacheHandler, 
        PSCacheKeys cacheKeys)
    {
        _mapper = mapper;
        _repository = repository;
        _userRepository = userRepository;
        _cacheHandler = cacheHandler;
        _cacheKeys = cacheKeys;
        _accountRepository = accountRepository;
    }

    /// <summary>
    /// 获取部门树
    /// </summary>
    /// <returns></returns>
    public async Task<IResultModel> GetTree()
    {
        var key = _cacheKeys.DepartmentTree();
        var list = await _cacheHandler.Get<List<TreeResultModel<Guid, DepartmentTreeVo>>>(key);
        if (list != null)
        {
            return ResultModel.Success(list);
        }

        var all = await _repository.Find().ToList();
        list = ResolveTree(all, Guid.Empty);

        if (list.Any())
            await _cacheHandler.Set(key, list);
        else
            await _cacheHandler.Set(key, list, new TimeSpan(0, 0, 5));

        return ResultModel.Success(list);
    }

    /// <summary>
    /// 解析部门树
    /// </summary>
    /// <param name="all"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    private List<TreeResultModel<Guid, DepartmentTreeVo>> ResolveTree(IList<DepartmentEntity> all, Guid parentId)
    {
        return all.Where(m => m.ParentId == parentId).OrderBy(m => m.Sort)
            .Select(m => new TreeResultModel<Guid, DepartmentTreeVo>
            {
                Id = m.Id,
                Label = m.Name,
                Item = _mapper.Map<DepartmentTreeVo>(m),
                Children = ResolveTree(all, m.Id)
            }).ToList();
    }

    /// <summary>
    /// 分页查询部门
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<PagingQueryResultModel<DepartmentEntity>> Query(DepartmentQueryDto dto)
    {
        var accounts = await _accountRepository.Find().Select(m => new { m, Creator = m.Name }).ToList();
        return await _repository.Query(dto, accounts);
    }

    /// <summary>
    /// 添加部门
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<IResultModel> Add(DepartmentAddDto dto)
    {
        if (await _repository.ExistsName(dto.Name, dto.ParentId ?? Guid.Empty))
        {
            return ResultModel.Failed("所属部门中已存在名称相同的部门");
        }

        if (dto.Code.NotNull() && await _repository.ExistsCode(dto.Code))
        {
            return ResultModel.Failed("编码已存在");
        }

        var entity = _mapper.Map<DepartmentEntity>(dto);
        entity.FullPath = $"/{entity.Name}";
        //查询父级
        if (!dto.ParentId.HasValue || dto.ParentId.HasValue)
        {
            var parent = await _repository.Get(dto.ParentId);
            if (parent != null)
            {
                //设置等级
                entity.Level = parent.Level++;
                //设置完整路径
                entity.FullPath = $"{parent.FullPath}/{entity.Name}";
            }
        }

        var result = await _repository.Add(entity);
        if (result)
        {
            await ClearCache();
            return ResultModel.Success(entity);
        }

        return ResultModel.Failed();
    }

    /// <summary>
    /// 删除部门
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResultModel> Delete(Guid id)
    {
        if (await _repository.ExistsChildren(id))
        {
            return ResultModel.Failed("当前部门包含子部门，请先删除子部门");
        }

        if (await _userRepository.ExistsBindDept(id))
        {
            return ResultModel.Failed("当前部门包含用户，请先删除用户");
        }

        var result = await _repository.Delete(id);
        if (result)
        {
            await ClearCache();
        }
        return ResultModel.Result(result);
    }

    /// <summary>
    /// 编辑部门
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResultModel> Edit(Guid id)
    {
        var entity = await _repository.Get(id);
        if (entity == null)
            return ResultModel.NotExists;

        var model = _mapper.Map<DepartmentUpdateDto>(entity);
        return ResultModel.Success(model);
    }

    /// <summary>
    /// 更新部门
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<IResultModel> Update(DepartmentUpdateDto model)
    {
        var entity = await _repository.Get(model.Id);
        if (entity == null)
            return ResultModel.NotExists;

        _mapper.Map(model, entity);

        if (await _repository.ExistsName(entity.Name, entity.ParentId, entity.Id))
        {
            return ResultModel.Failed("所属部门中已存在名称相同的部门");
        }
        if (model.Code.NotNull() && await _repository.ExistsCode(entity.Code, entity.Id))
        {
            return ResultModel.Failed("编码已存在");
        }

        entity.FullPath = entity.Name;
        //查询父级
        if (!model.ParentId.HasValue)
        {
            var parent = await _repository.Get(model.ParentId);
            if (parent != null)
            {
                //设置完整路径
                entity.FullPath = $"{parent.FullPath}/{entity.Name}";
            }
        }
        var result = await _repository.Update(entity);
        if (result)
        {
            await ClearCache();
        }

        return ResultModel.Result(result);
    }

    /// <summary>
    /// 清除缓存
    /// </summary>
    /// <returns></returns>
    private async Task<bool[]> ClearCache()
    {
        var key = _cacheKeys.DepartmentTree();
        var key2 = _cacheKeys.EmployeeTree();

        var task1 = _cacheHandler.Remove(key);
        var task2 = _cacheHandler.Remove(key2);

        return await Task.WhenAll(task1, task2);
    }



    [Transaction]
    public async Task<IResultModel> UpdateSort(IList<DepartmentEntity> departments)
    {
        if (!departments.Any())
            return ResultModel.Success();

        foreach (var department in departments)
        {
            await _repository.Find(m => m.Id == department.Id).ToUpdate(m => new DepartmentEntity
            {
                ParentId = department.ParentId,
                Sort = department.Sort
            });
        }

        await ClearCache();
        return ResultModel.Success();
    }
}
