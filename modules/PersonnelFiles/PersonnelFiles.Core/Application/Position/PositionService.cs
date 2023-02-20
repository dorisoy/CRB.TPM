using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.PS.Core.Application.Position.Dto;
using CRB.TPM.Mod.PS.Core.Domain.Position;
using CRB.TPM.Mod.PS.Core.Domain.Post;
using CRB.TPM.Utils.Map;
using System.Threading.Tasks;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using System;

namespace CRB.TPM.Mod.PS.Core.Application.Position;

public class PositionService : IPositionService
{
    private readonly IMapper _mapper;
    private readonly IPositionRepository _repository;
    private readonly IPostRepository _postRepository;
    private readonly IAccountRepository _accountRepository;

    public PositionService(IMapper mapper, IPositionRepository repository,
         IAccountRepository accountRepository,
        IPostRepository postRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _postRepository = postRepository;
        _accountRepository = accountRepository;
    }

    public async Task<PagingQueryResultModel<PositionEntity>> Query(PositionQueryDto dto)
    {
        var accounts = await _accountRepository.Find().Select(m => new { m, Creator = m.Name }).ToList();
        return await _repository.Query(dto, accounts);
    }

    public async Task<IResultModel> Add(PositionAddDto dto)
    {
        var entity = _mapper.Map<PositionEntity>(dto);
        if (await _repository.ExistsName(entity.Name))
            return ResultModel.Failed("名称已存在");

        if (entity.Code.NotNull() && await _repository.ExistsCode(entity.Code))
            return ResultModel.Failed("名称已存在");

        var result = await _repository.Add(entity);
        return ResultModel.Result(result);
    }

    public async Task<IResultModel> Delete(Guid id)
    {
        if (!await _repository.Exists(id))
        {
            return ResultModel.Failed("数据不存在");
        }

        if (await _postRepository.ExistsPosition(id))
        {
            return ResultModel.Failed("有岗位关联了该职位，请先删除岗位");
        }

        var result = await _repository.Delete(id);
        return ResultModel.Result(result);
    }

    public async Task<IResultModel> Edit(Guid id)
    {
        var entity = await _repository.Get(id);
        if (entity == null)
            return ResultModel.NotExists;

        var model = _mapper.Map<PositionUpdateDto>(entity);
        return ResultModel.Success(model);
    }

    public async Task<IResultModel> Update(PositionUpdateDto dto)
    {
        if (await _repository.ExistsName(dto.Name, dto.Id))
            return ResultModel.Failed("名称已存在");

        if (dto.Code.NotNull() && await _repository.ExistsCode(dto.Code, dto.Id))
            return ResultModel.Failed("名称已存在");

        var entity = await _repository.Get(dto.Id);
        if (entity == null)
            return ResultModel.NotExists;

        _mapper.Map(dto, entity);

        var result = await _repository.Update(entity);

        return ResultModel.Result(result);
    }

    public async Task<IResultModel> Get(Guid id)
    {
        var entity = await _repository.Get(id);
        if (entity != null)
        {
            return ResultModel.Success(entity);
        }

        return ResultModel.Failed();
    }
}
