using System;
using System.Linq;
using System.Threading.Tasks;
//using AutoMapper;
using System.Collections.Generic;

using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;

using CRB.TPM.Utils.Json;
using CRB.TPM.Utils.Map;
using CRB.TPM.Mod.MainData.Core.Infrastructure;
using CRB.TPM.Mod.MainData.Core.Application.MdKaBigSysNameConf.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MdKaBigSysNameConf;
using CRB.TPM.Mod.MainData.Core.Domain.MdKaBigSysNameConf;

namespace CRB.TPM.Mod.MainData.Core.Application.MdKaBigSysNameConf
{
    public class MdKaBigSysNameConfService : IMdKaBigSysNameConfService
    {
        private readonly IMapper _mapper;
        private readonly IMdKaBigSysNameConfRepository _repository;
        public MdKaBigSysNameConfService(IMapper mapper, IMdKaBigSysNameConfRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public Task<PagingQueryResultModel<MdKaBigSysNameConfEntity>> Query(MdKaBigSysNameConfQueryDto dto)
        {
            var query = _repository.Find();
            return query.ToPaginationResult(dto.Paging);
        }

        [Transaction]
        public async Task<IResultModel> Add(MdKaBigSysNameConfAddDto dto)
        {
            var entity = _mapper.Map<MdKaBigSysNameConfEntity>(dto);
            //if (await _repository.Exists(entity))
            //{
                //return ResultModel.HasExists;
            //}

            var result = await _repository.Add(entity);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(Guid id)
        {
            var result = await _repository.Delete(id);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _repository.Get(id);
            if (entity == null)
                return ResultModel.NotExists;

            var dto = _mapper.Map<MdKaBigSysNameConfUpdateDto>(entity);
            return ResultModel.Success(dto);
        }

        [Transaction]
        public async Task<IResultModel> Update(MdKaBigSysNameConfUpdateDto dto)
        {
            var entity = await _repository.Get(dto.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(dto, entity);

            //if (await _repository.Exists(entity))
            //{
                //return ResultModel.HasExists;
            //}

            var result = await _repository.Update(entity);

            return ResultModel.Result(result);
        }
    }
}
