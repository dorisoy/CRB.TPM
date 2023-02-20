using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Core.Extensions;
using CRB.TPM.Data.Sharding;
using CRB.TPM.Mod.Logging.Core.Application.CrmData.Dto;
using CRB.TPM.Mod.Logging.Core.Domain.CrmData;
using CRB.TPM.Mod.Logging.Core.Infrastructure;
using CRB.TPM.Utils.Map;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Logging.Core.Application.CrmData
{
    public class CrmDataService : ICrmDataService
    {
        private readonly IMapper _mapper;
        private readonly LoggingDbContext _dbContext;
        private readonly ICrmDataRepository _repository;
        public CrmDataService(IMapper mapper,
            LoggingDbContext dbContext,
            ICrmDataRepository repository)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _repository = repository;
        }

        public Task<PagingQueryResultModel<CrmDataEntity>> Query(CrmDataQueryDto dto)
        {
            var query = _repository.Find();
            return query.ToPaginationResult(dto.Paging);
        }

        [Transaction]
        public async Task<IResultModel> Add(CrmDataAddDto dto)
        {
            var entity = _mapper.Map<CrmDataEntity>(dto);
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

            var dto = _mapper.Map<CrmDataUpdateDto>(entity);
            return ResultModel.Success(dto);
        }

        [Transaction]
        public async Task<IResultModel> Update(CrmDataUpdateDto dto)
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
        /// 批量处理（存在则更新，不存在则新增）
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public async Task<IResultModel> AddOrUpdate(List<CrmDataSyncDto> dtos)
        {
            if (dtos != null && dtos.Count > 0)
            {
                #region 写临时表
                string connectionString = _dbContext.NewConnection().ConnectionString;
                string tmpTableName = $"tmp_crmdata_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                string tmpTableSql = $@"CREATE TABLE [{tmpTableName}] (
                                                            [Id] uniqueidentifier NOT NULL,
                                                            [DataType] int  NOT NULL,
                                                            [Code] nvarchar(100)  NULL,
                                                            [JsonString] text NULL,
                                                            [ZDATE] datetime  NOT NULL,
                                                            PRIMARY KEY ([Id])
                                                )";
                await _repository.Execute(tmpTableSql);
                DataTable tmpDataTable = dtos.ToDataTable();
                tmpDataTable.TableName = tmpTableName;

                //BulkInsertExtensions.SqlBulkCopy(connectionString, tmpDataTable);
                _dbContext.SqlBulkCopy(tmpDataTable);

                #endregion

                #region 存在时更新，不存在则新增
                //存在时更新，不存在则新增
                string crmDataSaveSql = $@"
                                        MERGE INTO CRM_Data AS T
                                        USING {tmpTableName} AS S
                                        ON T.Id = S.Id
                                        WHEN MATCHED THEN
                                            UPDATE SET T.DataType = S.DataType ,
                                                        T.Code = S.Code ,
                                                        T.JsonString = S.JsonString ,
                                                        T.ZDATE = S.ZDATE
                                        WHEN NOT MATCHED THEN
                                            INSERT ( Id ,
                                                        DataType ,
                                                        Code ,
                                                        JsonString ,
                                                        ZDATE
                                                    )
                                            VALUES ( S.Id ,
                                                        S.DataType ,
                                                        S.Code ,
                                                        S.JsonString ,
                                                        S.ZDATE
                                                    );";
                await _repository.Execute(crmDataSaveSql);
                #endregion

                #region 删除临时表
                string delTmpTableSql = string.Empty;
                if (tmpTableName.IsNull() == false)
                {
                    delTmpTableSql = $"DROP TABLE {tmpTableName};";
                    try
                    {
                        await _repository.Execute(delTmpTableSql);
                    }
                    catch
                    {
                    }
                }
                #endregion
            }

            return ResultModel.Success();
        }
    }
}
