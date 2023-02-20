using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Core.Extensions;
using CRB.TPM.Data.Sharding;
using CRB.TPM.Mod.Admin.Core.Application.MObject.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.MObject;
using CRB.TPM.Mod.Admin.Core.Infrastructure;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using CRB.TPM.Utils.Map;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Application.MObject;

public abstract class MObjectServiceAbstract : IMObjectService
{
    protected readonly IMapper _mapper;
    protected readonly AdminDbContext _dbContext;
    protected readonly IMObjectRepository _repository;
    public MObjectServiceAbstract(IMapper mapper, AdminDbContext dbContext, IMObjectRepository repository)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _repository = repository;
    }

    public Task<PagingQueryResultModel<MObjectEntity>> Query(MObjectQueryDto dto)
    {
        var query = _repository.Find();
        return query.ToPaginationResult(dto.Paging);
    }

    [Transaction]
    public async Task<IResultModel> Add(MObjectAddDto dto)
    {
        var entity = _mapper.Map<MObjectEntity>(dto);
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

        var dto = _mapper.Map<MObjectUpdateDto>(entity);
        return ResultModel.Success(dto);
    }

    [Transaction]
    public async Task<IResultModel> Update(MObjectUpdateDto dto)
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
    /// 创建数据同步批量处理临时表，并写入临时数据
    /// </summary>
    /// <param name="dtos">临时数据</param>
    /// <returns></returns>
    public async Task<string> CreateSyncTmpTable(List<MObjectSyncDto> dtos)
    {
        string tmpMObjectTableName = String.Empty;
        if (dtos != null && dtos.Count > 0)
        {
            #region 写临时表
            string connectionString = _dbContext.NewConnection().ConnectionString;
            tmpMObjectTableName = $"tmp_mobject_{DateTime.Now.ToString("yyyMMddhhmmssfff")}";
            string tmpMObjectTableSql = $@"CREATE TABLE [{tmpMObjectTableName}] (
                                                            [Id] uniqueidentifier NOT NULL,
                                                            [Type] int  NOT NULL,
                                                            [ObjectCode] nvarchar(10) NOT NULL,
                                                            [ObjectName] nvarchar(60) NOT NULL,
                                                            [HeadOfficeId] uniqueidentifier  NOT NULL,
                                                            [HeadOfficeCode] nvarchar(10) NOT NULL,
                                                            [HeadOfficeName] nvarchar(60) NOT NULL,
                                                            [DivisionId] uniqueidentifier  NOT NULL,
                                                            [DivisionCode] nvarchar(10) NOT NULL,
                                                            [DivisionName] nvarchar(60) NOT NULL,
                                                            [MarketingId] uniqueidentifier  NOT NULL,
                                                            [MarketingCode] nvarchar(10) NOT NULL,
                                                            [MarketingName] nvarchar(60) NOT NULL,
                                                            [BigAreaId] uniqueidentifier  NOT NULL,
                                                            [BigAreaCode] nvarchar(10) NOT NULL,
                                                            [BigAreaName] nvarchar(60) NOT NULL,
                                                            [OfficeId] uniqueidentifier  NOT NULL,
                                                            [OfficeCode] nvarchar(10) NOT NULL,
                                                            [OfficeName] nvarchar(60) NOT NULL,
                                                            [StationId] uniqueidentifier  NOT NULL,
                                                            [StationCode] nvarchar(10) NOT NULL,
                                                            [StationName] nvarchar(60) NOT NULL,
                                                            [DistributorId] uniqueidentifier  NOT NULL,
                                                            [DistributorCode] nvarchar(10) NOT NULL,
                                                            [DistributorName] nvarchar(100) NOT NULL,
                                                            [Enabled] bit NOT NULL,
                                                            PRIMARY KEY ([Id])
                                                )";
            await _repository.Execute(tmpMObjectTableSql);
            DataTable tmpMObjectDataTable = dtos.ToDataTable();
            tmpMObjectDataTable.TableName = tmpMObjectTableName;

            //BulkInsertExtensions.SqlBulkCopy(connectionString, tmpMObjectDataTable);
            _dbContext.SqlBulkCopy(tmpMObjectDataTable);
            #endregion
        }

        return tmpMObjectTableName;
    }

    /// <summary>
    /// 批量处理（存在则更新，不存在则新增）
    /// </summary>
    /// <param name="tmpMObjectTableName">临时表</param>
    /// <param name="uow">工作单元</param>
    /// <returns></returns>
    public async Task<IResultModel> BatchProcess(string tmpMObjectTableName, IUnitOfWork uow = null)
    {
        if (tmpMObjectTableName.IsNull() == false)
        {
            #region 更新组织/经销商Id
            //更新数据Id
            string updateTmpDataSql = $@"
                                            UPDATE  A
                                            SET     A.Id = ( CASE A.Type
                                                               WHEN {(int)OrgEnumType.HeadOffice} THEN H.Id
                                                               WHEN {(int)OrgEnumType.BD} THEN BD.Id
                                                               WHEN {(int)OrgEnumType.MarketingCenter} THEN M.Id
                                                               WHEN {(int)OrgEnumType.SaleRegion} THEN B.Id
                                                               WHEN {(int)OrgEnumType.Department} THEN O.Id
                                                               WHEN {(int)OrgEnumType.Station} THEN S.Id
                                                               WHEN {(int)OrgEnumType.Distributor} THEN D.Id
                                                               ELSE A.Id
                                                             END ) ,
		                                            A.HeadOfficeId = CASE WHEN H.Id IS NULL THEN A.HeadOfficeId ELSE H.ID END ,
		                                            A.DivisionId = CASE WHEN BD.Id IS NULL THEN A.DivisionId ELSE BD.ID END ,
		                                            A.MarketingId = CASE WHEN M.Id IS NULL THEN A.MarketingId ELSE M.ID END ,
                                                    A.BigAreaId = CASE WHEN B.Id IS NULL THEN A.BigAreaId ELSE B.ID END ,
                                                    A.OfficeId =  CASE WHEN O.Id IS NULL THEN A.OfficeId ELSE O.ID END,
                                                    A.StationId = CASE WHEN S.Id IS NULL THEN A.Id ELSE S.ID END ,
                                                    A.DistributorId = CASE WHEN D.Id IS NULL THEN A.DistributorId ELSE D.ID END
                                            FROM    {tmpMObjectTableName} A
                                                    LEFT JOIN M_Org H ON H.OrgCode = A.HeadOfficeCode
                                                    LEFT JOIN M_Org BD ON BD.OrgCode = A.DivisionCode
                                                    LEFT JOIN M_Org M ON M.OrgCode = A.MarketingCode
                                                    LEFT JOIN M_Org B ON B.OrgCode = A.BigAreaCode
                                                    LEFT JOIN M_Org O ON O.OrgCode = A.OfficeCode
                                                    LEFT JOIN M_Org S ON S.OrgCode = A.StationCode
                                                    LEFT JOIN M_Distributor D ON D.DistributorCode = A.DistributorCode;";

            await _repository.Execute(updateTmpDataSql, uow: uow);
            #endregion

            #region 删除不需要更新工作站的经销商
            string delSql = $@"DELETE S FROM {tmpMObjectTableName}  S
                                                        INNER JOIN M_Distributor D ON S.DistributorCode = D.DistributorCode
                                                        WHERE D.IsSynchronizeCRMStation = 0";
            await _repository.Execute(delSql, uow: uow);
            #endregion

            #region 存在时更新，不存在则新增
            //存在时更新，不存在则新增
            string mObjectSaveSql = $@"
                                        MERGE INTO M_Object AS T
                                        USING {tmpMObjectTableName} AS S
                                        ON T.ObjectCode = S.ObjectCode
                                        WHEN MATCHED THEN
                                            UPDATE SET T.Id = S.Id,
                                                       T.Type = S.Type,
                                                       T.ObjectName = S.ObjectName,
                                                       T.HeadOfficeId = S.HeadOfficeId ,
                                                       T.HeadOfficeCode = S.HeadOfficeCode ,
                                                       T.HeadOfficeName = S.HeadOfficeName ,
                                                       T.DivisionId = S.DivisionId ,
                                                       T.DivisionCode = S.DivisionCode ,
                                                       T.DivisionName = S.DivisionName ,
                                                       T.MarketingId = S.MarketingId ,
                                                       T.MarketingCode = S.MarketingCode ,
                                                       T.MarketingName = S.MarketingName ,
                                                       T.BigAreaId = S.BigAreaId ,
                                                       T.BigAreaCode = S.BigAreaCode ,
                                                       T.BigAreaName = S.BigAreaName ,
                                                       T.OfficeId = S.OfficeId ,
                                                       T.OfficeCode = S.OfficeCode ,
                                                       T.OfficeName = S.OfficeName ,
                                                       T.StationId = S.StationId ,
                                                       T.StationCode = S.StationCode ,
                                                       T.StationName = S.StationName ,
                                                       T.DistributorId = S.DistributorId ,
                                                       T.DistributorCode = S.DistributorCode ,
                                                       T.DistributorName = S.DistributorName ,
                                                       T.Enabled = S.Enabled 
                                        WHEN NOT MATCHED THEN
                                            INSERT ( Id ,
                                                        Type ,
                                                        ObjectCode ,
                                                        ObjectName ,
                                                        HeadOfficeId,
                                                        HeadOfficeCode,
                                                        HeadOfficeName,
                                                        DivisionId,
                                                        DivisionCode,
                                                        DivisionName,
                                                        MarketingId ,
                                                        MarketingCode ,
                                                        MarketingName ,
                                                        BigAreaId ,
                                                        BigAreaCode ,
                                                        BigAreaName ,
                                                        OfficeId ,
                                                        OfficeCode ,
                                                        OfficeName ,
                                                        StationId ,
                                                        StationCode ,
                                                        StationName ,
                                                        DistributorId ,
                                                        DistributorCode ,
                                                        DistributorName ,
                                                        Enabled
                                                    )
                                            VALUES ( S.Id ,
                                                        S.Type ,
                                                        S.ObjectCode ,
                                                        S.ObjectName ,
                                                        S.HeadOfficeId,
                                                        S.HeadOfficeCode,
                                                        S.HeadOfficeName,
                                                        S.DivisionId,
                                                        S.DivisionCode,
                                                        S.DivisionName,
                                                        S.MarketingId ,
                                                        S.MarketingCode ,
                                                        S.MarketingName ,
                                                        S.BigAreaId ,
                                                        S.BigAreaCode ,
                                                        S.BigAreaName ,
                                                        S.OfficeId ,
                                                        S.OfficeCode ,
                                                        S.OfficeName ,
                                                        S.StationId ,
                                                        S.StationCode ,
                                                        S.StationName ,
                                                        S.DistributorId ,
                                                        S.DistributorCode ,
                                                        S.DistributorName ,
                                                        S.Enabled
                                                    );";
            await _repository.Execute(mObjectSaveSql, uow: uow);
            #endregion
        }

        return ResultModel.Success();
    }

    /// <summary>
    /// 根据层级获取政策对象
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public async Task<IList<MObjectEntity>> QueryMObjectByType(OrgEnumType type)
    {
        var query = await _repository.Find().Where(w => w.Type == (int)type).ToList();
        return query;
    }

    /// <summary>
    /// 根据对象编码获取政策对象
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public async Task<MObjectEntity> GetMObjectByCode(string code)
    {
        var mObject = await _repository.Find().Where(w => w.ObjectCode == code).ToFirst();
        return mObject;
    }
}
