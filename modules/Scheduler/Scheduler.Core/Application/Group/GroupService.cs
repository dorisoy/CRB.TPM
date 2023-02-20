using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Scheduler.Core.Application.Group.Dto;
using CRB.TPM.Mod.Scheduler.Core.Domain.Group;
using CRB.TPM.Mod.Scheduler.Core.Domain.Job;
using CRB.TPM.Utils.Map;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace CRB.TPM.Mod.Scheduler.Core.Application.Group;

public class GroupService : IGroupService
{
    private readonly IMapper _mapper;
    private readonly IGroupRepository _repository;
    private readonly IJobRepository _jobRepository;

    public GroupService(IMapper mapper, IGroupRepository repository, IJobRepository jobRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _jobRepository = jobRepository;
    }

    public async Task<PagingQueryResultModel<GroupEntity>> Query(GroupQueryDto dto)
    {
        return await _repository.Query(dto);
    }

    public async Task<IResultModel> Add(GroupAddDto dto)
    {
        var entity = _mapper.Map<GroupEntity>(dto);
        if (await _repository.Exists(entity))
        {
            return ResultModel.Failed("编码已存在");
        }

        var result = await _repository.Add(entity);
        return ResultModel.Result(result);
    }

    public async Task<IResultModel> Delete(Guid id)
    {
        var entity = await _repository.Get(id);
        if (entity == null)
        {
            return ResultModel.NotExists;
        }

        if (await _jobRepository.ExistsByGroup(entity.Code))
        {
            return ResultModel.Failed("有任务绑定了该分组，请先删除任务");
        }

        var result = await _repository.Delete(id);

        return ResultModel.Result(result);
    }

    public async Task<IResultModel> Select()
    {
        var list = await _repository.Find().ToList();
        var select = list.Select(m => new OptionResultModel
        {
            Label = m.Name,
            Value = m.Code
        });

        return ResultModel.Success(select);
    }
}
