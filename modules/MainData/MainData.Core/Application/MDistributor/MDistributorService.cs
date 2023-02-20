using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;
using CRB.TPM.Data.Abstractions.Logger;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Mod.Admin.Core.Domain.MObject;
using CRB.TPM.Mod.Admin.Core.Domain.MOrg;
using CRB.TPM.Mod.Admin.Core.Infrastructure;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Vo;
using CRB.TPM.Mod.MainData.Core.Application.MEntity.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributor;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributorRelation;
using CRB.TPM.Mod.MainData.Core.Domain.MEntity;
using CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;
using CRB.TPM.Utils.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace CRB.TPM.Mod.MainData.Core.Application.MDistributor
{
    /// <summary>
    /// 客户信息
    /// </summary>
    public class MDistributorService : IMDistributorService
    {
        private readonly IExcelProvider _excelProvider;
        private readonly IMapper _mapper;
        private readonly IMDistributorRepository _repository;
        private readonly IMObjectRepository _mObjectRepository;
        private readonly IMOrgRepository _mOrgRepository;
        private readonly IMEntityRepository _mEntityRepository;
        private readonly IMDistributorRelationRepository _mRelationRepository;

        public MDistributorService(
            IMapper mapper,
            IMDistributorRepository repository,
            IExcelProvider excelProvider,
            IMObjectRepository mObjectRepository,
            IMOrgRepository mOrgRepository,
            IMEntityRepository mEntityRepository,
            IMDistributorRelationRepository mRelationRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _excelProvider = excelProvider;
            _mObjectRepository = mObjectRepository;
            _mOrgRepository = mOrgRepository;
            _mEntityRepository = mEntityRepository;
            _mRelationRepository = mRelationRepository;
        }

        /// <summary>
        /// 客户信息分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagingQueryResultModel<MDistributorQueryVo>> Query(MDistributorQueryDto dto)
        {
            return await _repository.QueryPage(dto);
        }
        /// <summary>
        /// 导出客户信息列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResultModel<ExcelModel>> Export(MDistributorQueryDto dto)
        {
            var list = await _repository.Query(dto);
            //TODO 目前不支持自定义model导出
            dto.ExportModel.Entities = (IList<MEntityExportDto>)list;
            var result = await _excelProvider.Export(dto.ExportModel);
            return result;
        }
        /// <summary>
        /// 新增客户信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Transaction]
        public async Task<IResultModel> Add(MDistributorAddDto dto)
        {
            var entity = _mapper.Map<MDistributorEntity>(dto);
            var checkRes = await CheckParm(entity);
            if (checkRes.res)
            {
                return ResultModel.Failed(checkRes.errmsg);
            }
            var result = await _repository.Add(entity);
            //工作站发生变化需要维护对象表
            await _mObjectRepository.Delete(entity.Id);
            await AddMObjeByDistributor(dto.StationId, entity);
            return ResultModel.Result(result);
        }
        /// <summary>
        /// 删除客户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResultModel> Delete(Guid id)
        {
            var entity = await _repository.Get(id);
            if(entity == null)
            {
                return ResultModel.Success();
            }
            var isUpdateRes = await _repository.IsChangeDistributorCode(entity.DistributorCode);
            if (!isUpdateRes.res)
            {
                return ResultModel.Failed("删除失败，" + isUpdateRes.msg);
            }
            var mobj = await _mObjectRepository.Find(f => f.DistributorId == id).ToFirst();
            if(mobj != null)
            {
                await _mObjectRepository.Delete(mobj.Id);
            }
            var result = await _repository.Delete(id);
            return ResultModel.Result(result);
        }
        /// <summary>
        /// 批量删除客户信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IResultModel> DeleteSelected(IEnumerable<Guid> ids)
        {
            var entityList = await _repository.Find(f => ids.Contains(f.Id)).ToList();
            List<string> msgList = new List<string>();
            List<Guid> delList = new List<Guid>();
            foreach (var entity in entityList)
            {
                var isUpdateRes = await _repository.IsChangeDistributorCode(entity.DistributorCode);
                if (!isUpdateRes.res)
                {
                    msgList.Add((entity.DistributorCode + "删除失败，" + isUpdateRes.msg));
                }
                else
                {
                    delList.Add(entity.Id);
                }
            }
            var delObjList = (await _mObjectRepository.Find(f => delList.Contains(f.DistributorId)).ToList()).Select(s => s.Id);
            if(delObjList.Any())
            {
                await _mObjectRepository.Deletes(delObjList);
            }
            await _repository.Delete(delList);
            if (msgList.Count > 0)
            {
                return ResultModel.Failed(String.Join("<br/>", msgList));
            }
            return ResultModel.Success();
        }
        /// <summary>
        /// 编辑客户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _repository.Get(id);
            if (entity == null)
                return ResultModel.NotExists;
            var dto = _mapper.Map<MDistributorEditVo>(entity);
            var mobj = await _mObjectRepository.Find(f => f.DistributorId == entity.Id).ToFirst();
            if (mobj != null)
            {
                dto.DepartmentId = mobj.OfficeId;
                dto.DutyregionId = mobj.BigAreaId;
                dto.MarketingId = mobj.MarketingId;
            }
            return ResultModel.Success(dto);
        }
        /// <summary>
        /// 更新编辑客户信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Transaction]
        public async Task<IResultModel> Update(MDistributorUpdateDto dto)
        {
            var entity = await _repository.Get(dto.Id);
            if (entity == null)
                return ResultModel.NotExists;

            var isUpdateRes = await _repository.IsChangeDistributorCode(entity.DistributorCode);
            if (!isUpdateRes.res)
            {
                return ResultModel.Failed("更新失败，" + isUpdateRes.msg);
            }
            _mapper.Map(dto, entity);
            var checkRes = await CheckParm(entity);
            if (checkRes.res)
            {
                return ResultModel.Failed(checkRes.errmsg);
            }

            //工作站发生变化需要维护对象表
            await _mObjectRepository.Delete(entity.Id);
            await _mObjectRepository.Find(f => f.DistributorCode == entity.DistributorCode).ToDelete();
            await AddMObjeByDistributor(dto.StationId, entity);
            var result = await _repository.Update(entity);

            return ResultModel.Result(result);
        }
        /// <summary>
        /// 添加客户组织维护
        /// </summary>
        /// <param name="stationId"></param>
        /// <param name="distributor"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task AddMObjeByDistributor(Guid stationId, MDistributorEntity distributor)
        {
            var station = await _mObjectRepository.Find(f => f.Type == (int)OrgEnumType.Station && f.StationId == stationId).ToFirst();
            if (station == null)
            {
                throw new ArgumentException("未找到工作站");
            }
            var mobj = new MObjectEntity()
            {
                Type = (int)OrgEnumType.Distributor,
                ObjectCode = distributor.DistributorCode,
                ObjectName = distributor.DistributorName,
                HeadOfficeId = station.HeadOfficeId,
                HeadOfficeCode = station.HeadOfficeCode,
                HeadOfficeName = station.HeadOfficeName,
                DivisionId = station.DivisionId,
                DivisionCode = station.DivisionCode,
                DivisionName = station.DivisionName,
                MarketingId = station.MarketingId,
                MarketingCode = station.MarketingCode,
                MarketingName = station.MarketingName,
                BigAreaId = station.BigAreaId,
                BigAreaCode = station.BigAreaCode,
                BigAreaName = station.BigAreaName,
                OfficeId = station.OfficeId,
                OfficeCode = station.OfficeCode,
                OfficeName = station.OfficeName,
                StationId = station.StationId,
                StationCode = station.StationCode,
                StationName = station.StationName,
                DistributorId = distributor.Id,
                DistributorCode = distributor.DistributorCode,
                DistributorName = distributor.DistributorName,
                Enabled = 1
            };
            await _mObjectRepository.Add(mobj);
        }
        /// <summary>
        /// 检查新增/更新参数校验
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<(bool res, string errmsg)> CheckParm(MDistributorEntity entity)
        {
            IList<string> errList = new List<string>();
            var resByDistributorCode = (await _repository.Find(f => f.DistributorCode.Equals(entity.DistributorCode, StringComparison.OrdinalIgnoreCase)).WhereNotEmpty(entity.Id, w => w.Id != entity.Id).Where(w => w.Deleted == false).ToFirst());
            if (resByDistributorCode != null)
            {
                errList.Add("客户编码已存在");
            }
            var resByStationId = _mObjectRepository.Find(f => f.Type == (int)OrgEnumType.Station && f.StationId == entity.StationId);
            if (resByStationId == null)
            {
                errList.Add("工作站不存在");
            }
            var mEntity = await _mEntityRepository.Get(entity.EntityId);
            if (mEntity == null)
            {
                errList.Add("销售组织不存在");
            }
            string errmsg = errList.Any() ? string.Join("<br/>", errList) : string.Empty;
            return (errmsg.Length > 0, errmsg);
        }
        /// <summary>
        /// 客户信息分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagingQueryResultModel<DistributorSelectVo>> Select(DistributorSelectDto dto)
        {
            var res = await _repository.Select(dto);
            return res;
        }
    }
}
