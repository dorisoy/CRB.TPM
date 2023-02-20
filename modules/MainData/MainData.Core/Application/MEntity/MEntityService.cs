using System;
using System.Threading.Tasks;
//using AutoMapper;
using System.Collections.Generic;

using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;

using CRB.TPM.Utils.Json;
using CRB.TPM.Utils.Map;
using CRB.TPM.Mod.MainData.Core.Infrastructure;
using CRB.TPM.Mod.MainData.Core.Application.MEntity.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MEntity;
using CRB.TPM.Mod.MainData.Core.Domain.MEntity;
using CRB.TPM.Excel.Abstractions.Export;
using CRB.TPM.Mod.Logging.Core.Application.LoginLog.Dto;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Abstractions.Entities;

namespace CRB.TPM.Mod.MainData.Core.Application.MEntity
{
    /// <summary>
    /// 法人主体
    /// </summary>
    public class MEntityService : IMEntityService
    {
        private readonly IMapper _mapper;
        private readonly IMEntityRepository _repository;
        private readonly IExcelProvider _excelProvider;

        public MEntityService(IMapper mapper, IMEntityRepository repository, IExcelProvider excelProvider)
        {
            _mapper = mapper;
            _repository = repository;
            _excelProvider = excelProvider;
        }

        public Task<PagingQueryResultModel<MEntityEntity>> Query(MEntityQueryDto dto)
        {
            var query = _repository.Find();
            query = WhereIfQuery(query, dto);
            return query.ToPaginationResult(dto.Paging);
        }

        private IQueryable<MEntityEntity> WhereIfQuery(IQueryable<MEntityEntity> query, MEntityQueryDto dto)
        {
            return query.WhereIf(dto.Name.NotNull(), m => m.EntityName.Contains(dto.Name) || m.EntityCode.Contains(dto.Name));
        }

        public async Task<(bool res, string errmsg)> CheckParm(MEntityEntity entity)
        {
            string errmsg = "";
            bool res = false;
            var resByEntityCode = await _repository.Find(f => f.EntityCode == entity.EntityCode).WhereNotEmpty(entity.Id, w => w.Id != entity.Id).ToFirst();
            if (resByEntityCode != null)
            {
                errmsg = "法人主体编码已存在";
                res = true;
            }
            return (res, errmsg);
        }

        public async Task<IResultModel<ExcelModel>> Export(MEntityQueryDto dto)
        {
            var query = _repository.Find();
            query = WhereIfQuery(query, dto);
            var list = await query.ToList<MEntityExportDto>();
            dto.ExportModel.Entities = list; 
            var result = await _excelProvider.Export(dto.ExportModel);
            return result;
        }

        [Transaction]
        public async Task<IResultModel> Add(MEntityAddDto dto)
        {
            var entity = _mapper.Map<MEntityEntity>(dto);
            var checkRes = await CheckParm(entity);
            if (checkRes.res)
            {
                return ResultModel.Failed(checkRes.errmsg);
            }
            var result = await _repository.Add(entity);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(Guid id)
        {
            var result = await _repository.Delete(id);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> DeleteSelected(IEnumerable<Guid> ids)
        {
            await _repository.Delete(ids);
            return ResultModel.Success();
        }

        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _repository.Get(id);
            if (entity == null)
                return ResultModel.NotExists;

            var dto = _mapper.Map<MEntityUpdateDto>(entity);
            return ResultModel.Success(dto);
        }

        [Transaction]
        public async Task<IResultModel> Update(MEntityUpdateDto dto)
        {
            var entity = await _repository.Get(dto.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(dto, entity);

            var checkRes = await CheckParm(entity);
            if (checkRes.res)
            {
                return ResultModel.Failed(checkRes.errmsg);
            }

            var result = await _repository.Update(entity);

            return ResultModel.Result(result);
        }
    }
}
