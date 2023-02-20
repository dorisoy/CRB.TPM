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
using CRB.TPM.Mod.MainData.Core.Application.MDistributorRelation.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MDistributorRelation;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributorRelation;
using CRB.TPM.Mod.MainData.Core.Application.MDistributorRelation.Vo;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MEntity.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributor;
using CRB.TPM.Mod.Admin.Core.Infrastructure;

namespace CRB.TPM.Mod.MainData.Core.Application.MDistributorRelation
{
    /// <summary>
    /// 经销分销关系
    /// </summary>
    public class MDistributorRelationService : IMDistributorRelationService
    {
        private readonly IMapper _mapper;
        private readonly IExcelProvider _excelProvider;
        private readonly IMDistributorRepository _mDistributorRepository;
        private readonly IMDistributorRelationRepository _repository;

        public MDistributorRelationService(
            IMapper mapper, 
            IMDistributorRelationRepository repository, 
            IExcelProvider excelProvider, 
            IMDistributorRepository mDistributorRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _excelProvider = excelProvider;
            _mDistributorRepository = mDistributorRepository;
        }

        /// <summary>
        /// 经销分销关系列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagingQueryResultModel<MDistributorRelationQueryVo>> Query(MDistributorRelationQueryDto dto)
        {
            return await _repository.QueryPage(dto);
        }
        /// <summary>
        /// 导出经销分销关系列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel<ExcelModel>> Export(MDistributorRelationQueryDto dto)
        {
            var list = await _repository.Query(dto);
            //TODO 目前不支持自定义model导出
            dto.ExportModel.Entities = (IList<MDistributorRelationEntity>)list;
            var result = await _excelProvider.Export(dto.ExportModel);
            return result;
        }
        /// <summary>
        /// 新增经销分销关系
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Transaction]
        public async Task<IResultModel> Add(MDistributorRelationAddDto dto)
        {
            var entity = _mapper.Map<MDistributorRelationEntity>(dto);
            var checkRes = await CheckParm(entity);
            if (checkRes.res)
            {
                return ResultModel.Failed(checkRes.errmsg);
            }
            await UpdateEntityDistributorCode(entity);
            var result = await _repository.Add(entity);
            return ResultModel.Result(result);
        }
        /// <summary>
        /// 删除经销分销关系
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResultModel> Delete(Guid id)
        {
            var result = await _repository.Delete(id);
            return ResultModel.Result(result);
        }
        /// <summary>
        /// 批量删除经销分销关系
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IResultModel> DeleteSelected(IEnumerable<Guid> ids)
        {
            await _repository.Delete(ids);
            return ResultModel.Success();
        }
        /// <summary>
        /// 编辑经销分销关系
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _repository.Get(id);
            if (entity == null)
                return ResultModel.NotExists;

            var dto = _mapper.Map<MDistributorRelationEditVo>(entity);
            return ResultModel.Success(dto);
        }
        /// <summary>
        /// 更新编辑经销分销关系
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Transaction]
        public async Task<IResultModel> Update(MDistributorRelationUpdateDto dto)
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
            //绑定发生变更，需要同步Code
            var resDistributorTuple = await _mDistributorRepository.GetByDistributorId(entity.DistributorId1, entity.DistributorId2);
            entity.DistributorCode1 = resDistributorTuple.mdb1.CustomerCode;
            entity.DistributorCode2 = resDistributorTuple.mdb1.CustomerCode;

            await UpdateEntityDistributorCode(entity);
            var result = await _repository.Update(entity);
            return ResultModel.Result(result);
        }
        /// <summary>
        /// 检查新增/更新参数校验
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<(bool res, string errmsg)> CheckParm(MDistributorRelationEntity entity)
        {
            IList<string> errList = new List<string>();
            var resDistributorTuple = await _mDistributorRepository.GetByDistributorId(entity.DistributorId1, entity.DistributorId2);
            if (resDistributorTuple.mdb1 == null)
            {
                errList.Add("经销商不存在");
            }
            if (resDistributorTuple.mdb2 == null)
            {
                errList.Add("分销商不存在");
            }
            // 新增/编辑 校验重复
            var resDistributorRelation = await _repository.Find(f => f.DistributorId1 == entity.DistributorId1 && f.DistributorId2 == entity.DistributorId2).WhereNotEmpty(entity.Id, w => w.Id != entity.Id).ToFirst();
            if(resDistributorRelation != null)
            {
                errList.Add("经销商已绑定分销商");
            }
            string errmsg = errList.Any() ? string.Join("<br/>", errList) : string.Empty;
            return (errmsg.Length > 0, errmsg);
        }
        /// <summary>
        /// 更新Entity上(经销商和分销商)编码
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private async Task UpdateEntityDistributorCode(MDistributorRelationEntity entity)
        {
            var resDistributorTuple = await _mDistributorRepository.GetByDistributorId(entity.DistributorId1, entity.DistributorId2);
            entity.DistributorCode1 = resDistributorTuple.mdb1.DistributorCode;
            entity.DistributorCode2 = resDistributorTuple.mdb2.DistributorCode;
        }
    }
}
