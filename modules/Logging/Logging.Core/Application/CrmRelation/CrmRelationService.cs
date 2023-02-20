using System;
using System.Linq;
using System.Threading.Tasks;
//using AutoMapper;
using System.Collections.Generic;

using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;

using CRB.TPM.Utils.Json;
using CRB.TPM.Utils.Map;
using CRB.TPM.Mod.Logging.Core.Infrastructure;
using CRB.TPM.Mod.Logging.Core.Application.CrmRelation.Dto;
using CRB.TPM.Mod.Logging.Core.Application.CrmRelation;
using CRB.TPM.Mod.Logging.Core.Domain.CrmRelation;

namespace CRB.TPM.Mod.Logging.Core.Application.CrmRelation
{
    public class CrmRelationService : ICrmRelationService
    {
        private readonly IMapper _mapper;
        private readonly ICrmRelationRepository _repository;
        public CrmRelationService(IMapper mapper, ICrmRelationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public Task<PagingQueryResultModel<CrmRelationEntity>> Query(CrmRelationQueryDto dto)
        {
            var query = _repository.Find();
            return query.ToPaginationResult(dto.Paging);
        }

        [Transaction]
        public async Task<IResultModel> Add(CrmRelationAddDto dto)
        {
            var entity = _mapper.Map<CrmRelationEntity>(dto);
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

            var dto = _mapper.Map<CrmRelationUpdateDto>(entity);
            return ResultModel.Success(dto);
        }

        [Transaction]
        public async Task<IResultModel> Update(CrmRelationUpdateDto dto)
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

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public async Task<IResultModel> BulkAdd(List<CrmRelationEntity> dtos)
        {
            if (dtos != null && dtos.Count > 0)
            {
                if (await _repository.BulkAdd(dtos))
                {
                    return ResultModel.Success();
                }
                else
                {
                    return ResultModel.Failed("添加失败");
                }
            }
            return ResultModel.Success();
        }
    }
}
