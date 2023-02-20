using CRB.TPM.Cache.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.PS.Core.Application.Post.Dto;
using CRB.TPM.Mod.PS.Core.Domain.Employee;
using CRB.TPM.Mod.PS.Core.Domain.Post;
using CRB.TPM.Mod.PS.Core.Infrastructure;
using CRB.TPM.Utils.Map;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using System;

namespace CRB.TPM.Mod.PS.Core.Application.Post;

public class PostService : IPostService
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _repository;
    private readonly IEmployeeRepository _userRepository;
    private readonly ICacheProvider _cacheHandler;
    private readonly PSCacheKeys _cacheKeys;
    private readonly IAccountRepository _accountRepository;


    public PostService(IMapper mapper, IPostRepository repository,
        IEmployeeRepository userRepository,
        PSCacheKeys cacheKeys,
        IAccountRepository accountRepository,
        ICacheProvider cacheHandler)
    {
        _mapper = mapper;
        _repository = repository;
        _userRepository = userRepository;
        _cacheHandler = cacheHandler;
        _cacheKeys = cacheKeys;
        _accountRepository = accountRepository;
    }

    public async Task<PagingQueryResultModel<PostEntity>> Query(PostQueryDto dto)
    {
        var accounts = await _accountRepository.Find().Select(m => new { m, Creator = m.Name }).ToList();
        return await _repository.Query(dto, accounts);
    }

    public async Task<IResultModel> Add(PostAddDto dto)
    {
        if (await _repository.ExistsName(dto.Name))
            return ResultModel.Failed("名称已存在");
        var entity = _mapper.Map<PostEntity>(dto);

        var result = await _repository.Add(entity);
        if (result)
        {
            await _cacheHandler.Remove(_cacheKeys.PostSelect());
        }
        return ResultModel.Result(result);
    }

    public async Task<IResultModel> Delete(Guid id)
    {
        if (!await _repository.Exists(id))
            return ResultModel.NotExists;

        if (await _userRepository.ExistsBindPost(id))
            return ResultModel.Failed("有用户绑定了该职位，请先删除绑定关系");

        var result = await _repository.Delete(id);
        if (result)
        {
            await _cacheHandler.Remove(_cacheKeys.PostSelect());
        }
        return ResultModel.Result(result);
    }

    public async Task<IResultModel> Edit(Guid id)
    {
        var entity = await _repository.Get(id);
        if (entity == null)
            return ResultModel.NotExists;

        var model = _mapper.Map<PostUpdateDto>(entity);
        return ResultModel.Success(model);
    }

    public async Task<IResultModel> Update(PostUpdateDto model)
    {
        if (await _repository.ExistsName(model.Name, model.Id))
            return ResultModel.Failed("名称已存在");

        var entity = await _repository.Get(model.Id);
        if (entity == null)
            return ResultModel.NotExists;
        _mapper.Map(model, entity);

        var result = await _repository.Update(entity);
        if (result)
        {
            await _cacheHandler.Remove(_cacheKeys.PostSelect());
        }
        return ResultModel.Result(result);
    }

    public async Task<IResultModel> Select()
    {
        var list = await _cacheHandler.Get<List<OptionResultModel>>(_cacheKeys.PostSelect());
        if (list == null)
        {
            var all = await _repository.Find().ToList();
            list = all.Select(m => new OptionResultModel
            {
                Label = m.Name,
                Value = m.Id
            }).ToList();

            await _cacheHandler.Set(_cacheKeys.PostSelect(), list);
        }

        return ResultModel.Success(list);
    }
}
