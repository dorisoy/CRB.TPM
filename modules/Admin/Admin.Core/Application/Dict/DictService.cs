using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRB.TPM.Cache.Abstractions;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;


using CRB.TPM.Mod.Admin.Core.Application.Dict.Dto;
using CRB.TPM.Mod.Admin.Core.Application.Dict.Vo;
using CRB.TPM.Mod.Admin.Core.Domain.Dict;
using CRB.TPM.Mod.Admin.Core.Domain.DictGroup;
using CRB.TPM.Mod.Admin.Core.Domain.DictItem;
using CRB.TPM.Mod.Admin.Core.Infrastructure;
using CRB.TPM.Utils.Map;

namespace CRB.TPM.Mod.Admin.Core.Application.Dict;

public class DictService : IDictService
{
    private readonly IMapper _mapper;
    private readonly IDictRepository _repository;
    private readonly IDictGroupRepository _groupRepository;
    private readonly IDictItemRepository _itemRepository;
    private readonly ICacheProvider _cacheHandler;
    private readonly AdminCacheKeys _cacheKeys;
    private readonly AdminLocalizer _localizer;

    public DictService(IMapper mapper, IDictRepository repository, IDictGroupRepository groupRepository, IDictItemRepository itemRepository, ICacheProvider cacheHandler, AdminCacheKeys cacheKeys, AdminLocalizer localizer)
    {
        _mapper = mapper;
        _repository = repository;
        _groupRepository = groupRepository;
        _itemRepository = itemRepository;
        _cacheHandler = cacheHandler;
        _cacheKeys = cacheKeys;
        _localizer = localizer;
    }

    public Task<PagingQueryResultModel<DictEntity>> Query(DictQueryDto dto)
    {
        return _repository.Query(dto);
    }

    public async Task<IResultModel> Add(DictAddDto dto)
    {
        if (await _repository.Find(m => m.GroupCode == dto.GroupCode && m.Code == dto.Code).ToExists())
            return ResultModel.Failed(_localizer["当前分组下字典编码已存在"]);

        if (!await _groupRepository.Find(m => m.Code == dto.GroupCode).ToExists())
            return ResultModel.Failed(_localizer["当前分组不存在"]);

        var entity = _mapper.Map<DictEntity>(dto);

        var result = await _repository.Add(entity);

        return ResultModel.Result(result);
    }

    public async Task<IResultModel> Edit(int id)
    {
        var entity = await _repository.Get(id);
        if (entity == null)
            return ResultModel.NotExists;

        var model = _mapper.Map<DictUpdateDto>(entity);
        return ResultModel.Success(model);
    }

    [Transaction]
    public async Task<IResultModel> Update(DictUpdateDto dto)
    {
        var entity = await _repository.Get(dto.Id);
        if (entity == null)
            return ResultModel.NotExists;

        if (await _repository.Find(m => m.GroupCode == dto.GroupCode && m.Code == dto.Code && m.Id != dto.Id).ToExists())
            return ResultModel.Failed("当前分组下字典编码已存在");

        if (!await _groupRepository.Find(m => m.Code == dto.GroupCode).ToExists())
            return ResultModel.Failed("当前分组不存在");

        _mapper.Map(dto, entity);

        var result = await _repository.Update(entity);
        return ResultModel.Result(result);
    }

    public async Task<IResultModel> Delete(int id)
    {
        var entity = await _repository.Get(id);
        if (entity == null)
            return ResultModel.NotExists;

        if (await _itemRepository.Find(m => m.GroupCode == entity.GroupCode && m.DictCode == entity.Code)
            .ToExists())
            return ResultModel.Failed(_localizer["该字典包含数据项，请先删除数据项"]);

        var result = await _repository.SoftDelete(id);
        return ResultModel.Result(result);
    }

    public async Task<IResultModel> Select(string groupCode, string dictCode)
    {
        var key = _cacheKeys.DictSelect(groupCode, dictCode);
        var list = await _cacheHandler.Get<List<OptionResultModel>>(key);
        if (list != null)
        {
            return ResultModel.Success(list);
        }

        var all = await _itemRepository.Find(m => m.GroupCode == groupCode && m.DictCode == dictCode && m.ParentId == null)
            .OrderBy(m => m.Sort)
            .ToList();

        list = all.Select(m => new OptionResultModel
        {
            Label = m.Name,
            Value = m.Value,
            Data = new
            {
                m.Id,
                m.Name,
                m.Value,
                m.Extend,
                m.Icon,
                m.Level
            }
        }).ToList();

        if (list.Any())
            await _cacheHandler.Set(key, list);
        else
            await _cacheHandler.Set(key, list, new TimeSpan(0, 0, 5));

        return ResultModel.Success(list);
    }

    public async Task<IResultModel> Tree(string groupCode, string dictCode)
    {
        var key = _cacheKeys.DictTree(groupCode, dictCode);
        var tree = await _cacheHandler.Get<List<TreeResultModel<Guid, DictItemTreeVo>>>(key);
        if (tree != null)
        {
            return ResultModel.Success(tree);
        }

        var dict = await _repository.Find(m => m.GroupCode == groupCode && m.Code == dictCode).ToFirst();
        if (dict == null)
            return ResultModel.Failed(_localizer["字典不存在"]);

        tree = new List<TreeResultModel<Guid, DictItemTreeVo>>();
        var root = new TreeResultModel<Guid, DictItemTreeVo>
        {
            Id = Guid.Empty,
            Label = dict.Name,
            Item = new DictItemTreeVo
            {
                Name = dict.Name
            }
        };

        var all = await _itemRepository.Find(m => m.GroupCode == groupCode && m.DictCode == dictCode)
            .OrderBy(m => m.Sort)
            .ToList();

        root.Children = ResolveTree(all);

        tree.Add(root);

        if (tree.Any())
            await _cacheHandler.Set(key, tree, new TimeSpan(0, 0, 5));

        return ResultModel.Success(tree);
    }

    private List<TreeResultModel<Guid, DictItemTreeVo>> ResolveTree(IList<DictItemEntity> all, Guid? parentId = null)
    {
        return all.Where(m => m.ParentId == parentId).OrderBy(m => m.Sort).Select(m =>
        {
            var node = new TreeResultModel<Guid, DictItemTreeVo>
            {
                Id = m.Id,
                Label = m.Name,
                Value = m.Value,
                Item = _mapper.Map<DictItemTreeVo>(m),
                Children = ResolveTree(all, m.Id)
            };

            return node;
        }).ToList();
    }

    public async Task<IResultModel> Cascader(string groupCode, string dictCode)
    {
        var key = _cacheKeys.DictCascader(groupCode, dictCode);
        var list = await _cacheHandler.Get<List<OptionResultModel>>(key);
        if (list != null)
        {
            return ResultModel.Success(list);
        }

        var all = await _itemRepository.Find(m => m.GroupCode == groupCode && m.DictCode == dictCode)
            .OrderBy(m => m.Sort)
            .ToList();

        list = ResolveCascader(all);

        if (list.Any())
            await _cacheHandler.Set(key, list, new TimeSpan(0, 0, 5));

        return ResultModel.Success(list);
    }

    private List<OptionResultModel> ResolveCascader(IList<DictItemEntity> all, Guid? parentId = null)
    {
        return all.Where(m => m.ParentId == parentId).OrderBy(m => m.Sort).Select(m =>
        {
            var node = new OptionResultModel
            {
                Label = m.Name,
                Value = m.Value,
                Data = new
                {
                    m.Id,
                    m.Name,
                    m.Value,
                    m.Extend,
                    m.Icon,
                    m.Level
                },
                Children = ResolveCascader(all, m.Id)
            };

            if (!node.Children.Any())
            {
                node.Children = null;
            }
            return node;
        }).ToList();
    }

}