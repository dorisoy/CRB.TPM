using CRB.TPM.Config.Abstractions;
using CRB.TPM.Data.Core.Extensions;
using CRB.TPM.Data.Sharding;
using CRB.TPM.Mod.Admin.Core.Application.MObject;
using CRB.TPM.Mod.Admin.Core.Application.MObject.Dto;
using CRB.TPM.Mod.Admin.Core.Application.WarningInfo;
using CRB.TPM.Mod.Admin.Core.Application.WarningInfo.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.MObject;
using CRB.TPM.Mod.Admin.Core.Domain.MOrg;
using CRB.TPM.Mod.Admin.Core.Domain.WarningInfo;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using CRB.TPM.Mod.Logging.Core.Application.CrmData;
using CRB.TPM.Mod.Logging.Core.Application.CrmData.Dto;
using CRB.TPM.Mod.Logging.Core.Application.CrmRelation;
using CRB.TPM.Mod.Logging.Core.Application.CrmRewrite;
using CRB.TPM.Mod.Logging.Core.Domain.CrmData;
using CRB.TPM.Mod.Logging.Core.Domain.CrmRelation;
using CRB.TPM.Mod.Logging.Core.Domain.CrmRewrite;
using CRB.TPM.Mod.Logging.Core.Infrastructure;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MDistributorRelation.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MTerminal.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalDistributor.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalUser.Dto;
using CRB.TPM.Mod.MainData.Core.Application.SyncDtAndTmn.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributor;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributorRelation;
using CRB.TPM.Mod.MainData.Core.Domain.MEntity;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminal;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminalDetail;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminalDistributor;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminalUser;
using CRB.TPM.Mod.MainData.Core.Infrastructure;
using CRB.TPM.Utils.Map;
using Microsoft.Extensions.Logging;
using SapNwRfc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.SyncDtAndTmn
{
    public class SyncDtAndTmnService : ISyncDtAndTmnService
    {
        private readonly IMapper _mapper;
        private readonly MainDataDbContext _dbContext;
        private readonly IConfigProvider _configProvider;
        private readonly ILogger<SyncDtAndTmnService> _logger;
        private readonly IMDistributorRepository _mDistributorRepository;
        private readonly IMTerminalRepository _mTerminalRepository;
        private readonly IMTerminalDistributorRepository _mTerminalDistributorRepository;
        private readonly IMTerminalUserRepository _mTerminalUserRepository;
        private readonly IMDistributorRelationRepository _mDistributorRelationRepository;
        private readonly IMObjectService _mObjectService;
        private readonly ICrmRewriteService _crmRewriteService;
        private readonly ICrmRelationService _crmRelationService;
        private readonly ICrmDataService _crmDataService;
        private readonly IWarningInfoService _warningInfoService;
        private readonly IMEntityRepository _mEntityRepository;

        public SyncDtAndTmnService(IMapper mapper,
            MainDataDbContext dbContext,
            LoggingDbContext loggingDbContext,
            IConfigProvider configProvider,
            ILogger<SyncDtAndTmnService> logger,
            IMDistributorRepository mDistributorRepository,
            IMTerminalRepository mTerminalRepository,
            IMTerminalDistributorRepository mTerminalDistributorRepository,
            IMTerminalUserRepository mTerminalUserRepository,
            IMDistributorRelationRepository mDistributorRelationRepository,
            IMObjectService mObjectService,
            ICrmRewriteService crmRewriteService,
            ICrmRelationService crmRelationService,
            ICrmDataService crmDataService,
            IWarningInfoService warningInfoService,
            IMEntityRepository mEntityRepository
            )
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _logger = logger;
            _configProvider = configProvider;
            _mDistributorRepository = mDistributorRepository;
            _mTerminalRepository = mTerminalRepository;
            _mTerminalDistributorRepository = mTerminalDistributorRepository;
            _mTerminalUserRepository = mTerminalUserRepository;
            _mDistributorRelationRepository = mDistributorRelationRepository;
            _mObjectService = mObjectService;
            _crmRewriteService = crmRewriteService;
            _crmRelationService = crmRelationService;
            _crmDataService = crmDataService;
            _warningInfoService = warningInfoService;
            _mEntityRepository = mEntityRepository;
        }

        #region 同步CRM经销商、终端主数据
        /// <summary>
        /// 同步CRM经销商、终端主数据
        /// </summary>
        /// <param name="dto">请求模型</param>
        /// <returns></returns>
        public async Task<IResultModel> SyncData(SyncDtAndTmnDto dto)
        {
            List<SyncDtAndTmnErrorLogDto> syncDtAndTmnErrorLogs = new List<SyncDtAndTmnErrorLogDto>();
            try
            {
                string marketOrgCD = dto.MarketOrgCD;
                MObjectEntity curMarketOrg;

                var config = _configProvider.Get<MainDataConfig>();

                SapConnectionParameters sapConnectionParameters = SapNwRfc.SapConnectionParameters.Parse(config.SapConnection);
                while (true)
                {
                    using (SapNwRfc.SapConnection connection = new SapNwRfc.SapConnection(sapConnectionParameters))
                    {
                        connection.Connect();
                        using (var someFunction = connection.CreateFunction(config.SyncCRMDtAndTmnFunctionName))
                        {

                            var data = new { IV_QUYU = "O " + marketOrgCD };

                            _logger.LogInformation("开始取数");
                            var result = someFunction.Invoke<Result_ZSNFX_RFC_GET_MTMX_TPM>(data);
                            if (result.ET_CHANNELS.Length > 0 || result.ET_TERMINAL.Length > 0 || result.ET_RELATION.Length > 0)
                            {

                                List<IT_RETURN> returnList = new List<IT_RETURN>();

                                //获取连接字符串
                                string connectionString = _dbContext.NewConnection().ConnectionString;
                                IList<MObjectEntity> mObjects = await _mObjectService.QueryMObjectByType(OrgEnumType.Station);
                                curMarketOrg = await _mObjectService.GetMObjectByCode(marketOrgCD);
                                //取现有业务实体
                                IList<MEntityEntity> mEntities = await _mEntityRepository.Find().ToList();
                                List<MDistributorSyncDto> distributorList = new List<MDistributorSyncDto>();
                                List<MTerminalSyncDto> terminalList = new List<MTerminalSyncDto>();
                                List<MTerminalDetailEntity> terminalDetailList = new List<MTerminalDetailEntity>();
                                List<MTerminalDistributorSyncDto> terminalDistributorList = new List<MTerminalDistributorSyncDto>();
                                List<MTerminalUserSyncDto> terminalUserList = new List<MTerminalUserSyncDto>();
                                List<MDistributorRelationSyncDto> distributorRelationList = new List<MDistributorRelationSyncDto>();
                                List<MDistributorManageAccountDto> distributorManageAccountList = new List<MDistributorManageAccountDto>();
                                List<MObjectSyncDto> mObjectList = new List<MObjectSyncDto>();
                                List<CrmRelationEntity> crmRelationList = new List<CrmRelationEntity>();
                                List<CrmRewriteEntity> crmRewriteList = new List<CrmRewriteEntity>();
                                List<CrmDataEntity> crmDataList = new List<CrmDataEntity>();
                                string tmpDistributorTableName = string.Empty;
                                string tmpTerminalTableName = string.Empty;
                                string tmpTerminalDistributorTableName = string.Empty;
                                string tmpTerminalUserTableName = string.Empty;
                                string tmpDistributorRelationTableName = string.Empty;
                                string tmpDistributorManageAccountTableName = string.Empty;
                                string tmpMObjectTableName = string.Empty;

                                #region 读取数据

                                _logger.LogInformation("开始读取数据");

                                #region 经销商信息
                                string crmInitCustomercode = "";
                                foreach (var tmp in result.ET_CHANNELS)
                                {
                                    crmInitCustomercode = tmp.PARTNER;

                                    if (tmp.ZZGZZ_ID.IsNull() == false && tmp.ZZGZZ_ID.StartsWith("O ") == true)
                                    {
                                        tmp.ZZGZZ_ID = tmp.ZZGZZ_ID.Substring(2);
                                    }
                                    var stationOrg = mObjects.FirstOrDefault(f => f.ObjectCode == tmp.ZZGZZ_ID && f.Type == (int)OrgEnumType.Station);
                                    if (stationOrg == null)
                                    {
                                        if (!syncDtAndTmnErrorLogs.Any(a => a.Type == WarningInfoType.Distributor && a.Code1 == crmInitCustomercode))
                                        {
                                            syncDtAndTmnErrorLogs.Add(this.CreateErrorLog(marketOrgCD, WarningInfoType.Distributor, crmInitCustomercode, null, $"经销商：{crmInitCustomercode}，工作站：{tmp.ZZGZZ_ID } 不存在 "));
                                        }
                                        //TODO：经销商工作站未找到，是否保存数据
                                        //continue;
                                    }

                                    Guid mEntityId = Guid.Empty;
                                    if (tmp.ZZENTITY.IsNull())
                                    {
                                        if (!syncDtAndTmnErrorLogs.Any(a => a.Type == WarningInfoType.DistributorEntity && a.Code1 == crmInitCustomercode))
                                        {
                                            syncDtAndTmnErrorLogs.Add(this.CreateErrorLog(marketOrgCD, WarningInfoType.DistributorEntity, crmInitCustomercode, null, $"经销商：{crmInitCustomercode}，业务实体为空 "));
                                        }
                                        //continue;
                                    }
                                    else
                                    {
                                        var mEntity = mEntities.FirstOrDefault(f => f.EntityCode == tmp.ZZENTITY);
                                        if (mEntity == null)
                                        {
                                            if (!syncDtAndTmnErrorLogs.Any(a => a.Type == WarningInfoType.DistributorEntity && a.Code1 == crmInitCustomercode))
                                            {
                                                syncDtAndTmnErrorLogs.Add(this.CreateErrorLog(marketOrgCD, WarningInfoType.DistributorEntity, crmInitCustomercode, null, $"经销商：{crmInitCustomercode}，业务实体 {tmp.ZZENTITY} 不存在 "));
                                            }
                                            //continue;
                                        }
                                        else
                                        {
                                            mEntityId = mEntity.Id;
                                        }
                                    }


                                    returnList.Add(this.CreateCRMReturnObjectForDistributor(crmInitCustomercode, tmp.ZDATE));
                                    //非一批商、二批商直接返写
                                    if (tmp.ZZCLIENT_TYPE != "01" && tmp.ZZCLIENT_TYPE != "02")
                                    {
                                        continue;
                                    }

                                    Guid distributorId = Guid.NewGuid();
                                    distributorList.Add(new MDistributorSyncDto
                                    {
                                        Id = distributorId,
                                        DistributorCode = crmInitCustomercode,
                                        DistributorName = tmp.NAME_ORG1,
                                        DistributorType = tmp.ZZCLIENT_TYPE == "01" ? 1 : 2,
                                        StationId = stationOrg == null ? Guid.Empty : stationOrg.Id,
                                        Status = 1, //TODO：暂未返回   tmp.ZZSTATUS1 == "Z01" ? 1 : tmp.ZZSTATUS1 == "Z02" ? 2 : 0,
                                        CrmCode = crmInitCustomercode,
                                        CustomerCode = crmInitCustomercode,
                                        DetailType = 1, //1表示主户；2管理开户的子户；3TPM虚拟子户
                                        CreatedBy = Guid.Empty,
                                        CreatedTime = DateTime.Now,
                                        Creator = String.Empty,
                                        JsonString = Newtonsoft.Json.JsonConvert.SerializeObject(tmp),
                                        ZDATE = GetDateTime(tmp.ZDATE),
                                        EntityId = mEntityId
                                    });
                                    if (stationOrg != null)
                                    {
                                        mObjectList.Add(new MObjectSyncDto
                                        {
                                            Id = distributorId,
                                            Type = (int)OrgEnumType.Distributor,
                                            ObjectCode = crmInitCustomercode,
                                            ObjectName = tmp.NAME_ORG1,
                                            HeadOfficeId = stationOrg.HeadOfficeId,
                                            HeadOfficeCode = stationOrg.HeadOfficeCode,
                                            HeadOfficeName = stationOrg.HeadOfficeName,
                                            DivisionId = stationOrg.DivisionId,
                                            DivisionCode = stationOrg.DivisionCode,
                                            DivisionName = stationOrg.DivisionName,
                                            MarketingId = stationOrg.MarketingId,
                                            MarketingCode = stationOrg.MarketingCode,
                                            MarketingName = stationOrg.MarketingName,
                                            BigAreaId = stationOrg.BigAreaId,
                                            BigAreaCode = stationOrg.BigAreaCode,
                                            BigAreaName = stationOrg.BigAreaName,
                                            OfficeId = stationOrg.OfficeId,
                                            OfficeCode = stationOrg.OfficeCode,
                                            OfficeName = stationOrg.OfficeName,
                                            StationId = stationOrg.Id,
                                            StationCode = stationOrg.StationCode,
                                            StationName = stationOrg.StationName,
                                            DistributorId = distributorId,
                                            DistributorCode = crmInitCustomercode,
                                            DistributorName = tmp.NAME_ORG1,
                                            Enabled = 1
                                        });
                                    }
                                }
                                #endregion

                                #region 终端信息
                                foreach (var tmp in result.ET_TERMINAL)
                                {

                                    Guid terminalId = Guid.NewGuid();
                                    if (tmp.ZZGZZ_ID.IsNull() == false && tmp.ZZGZZ_ID.StartsWith("O ") == true)
                                    {
                                        tmp.ZZGZZ_ID = tmp.ZZGZZ_ID.Substring(2);
                                    }
                                    var org = mObjects.FirstOrDefault(f => f.ObjectCode == tmp.ZZGZZ_ID && f.Type == (int)OrgEnumType.Station);
                                    if (org == null)
                                    {
                                        if (!syncDtAndTmnErrorLogs.Any(a => a.Type == WarningInfoType.Terminal && a.Code1 == crmInitCustomercode))
                                        {
                                            syncDtAndTmnErrorLogs.Add(this.CreateErrorLog(marketOrgCD, WarningInfoType.Terminal, crmInitCustomercode, null, $"终端：{tmp.PARTNER}，工作站：{tmp.ZZGZZ_ID } 不存在 "));
                                        }
                                        //TODO：终端工作站未找到，是否保存数据
                                        //continue;
                                    }
                                    terminalList.Add(this.CreateTerminal(terminalId, org == null ? Guid.Empty : org.Id, tmp));

                                    returnList.Add(this.CreateCRMReturnObjectForTerminal(tmp.PARTNER, tmp.ZDATE));

                                }
                                #endregion

                                #region 关系
                                //关于关系的处理（如终端-客户），要先按crm时间排序，避免在同一批数据同一关系有新增和删除操作的，不按顺序可能会处理错了。
                                //ZUPDMODE是D的删除：如果系统存在删除
                                //ZUPDMODE不是D的：系统存在不处理（返写掉），不存在的新增进系统
                                result.ET_RELATION = result.ET_RELATION.OrderBy(r => r.ZDATE).ToArray();

                                foreach (var tmp in result.ET_RELATION)
                                {
                                    #region 终端-客户关系

                                    if (tmp.RELTYP == "ZS007")
                                    {
                                        terminalDistributorList.Add(new MTerminalDistributorSyncDto
                                        {
                                            Id = Guid.NewGuid(),
                                            DistributorCode = tmp.PARTNER2,
                                            TerminalCode = tmp.PARTNER1,
                                            UpdateMode = tmp.ZUPDMODE ?? String.Empty
                                        });

                                        returnList.Add(this.CreateCRMReturnObjectForRelation(tmp));

                                        crmRelationList.Add(new CrmRelationEntity
                                        {
                                            Id = Guid.NewGuid(),
                                            Code1 = tmp.PARTNER1,
                                            Code2 = tmp.PARTNER2,
                                            RELTYP = tmp.RELTYP,
                                            ZUPDMODE = tmp.ZUPDMODE,
                                            ZDATE = tmp.ZDATE
                                        });
                                    }
                                    #endregion

                                    #region 终端-人员关系
                                    else if (tmp.RELTYP == "ZS003")
                                    {
                                        terminalUserList.Add(new MTerminalUserSyncDto
                                        {
                                            Id = Guid.NewGuid(),
                                            TerminalCode = tmp.PARTNER1,
                                            UserBP = tmp.PARTNER2,
                                            UpdateMode = tmp.ZUPDMODE ?? String.Empty
                                        });

                                        returnList.Add(this.CreateCRMReturnObjectForRelation(tmp));

                                        crmRelationList.Add(new CrmRelationEntity
                                        {
                                            Id = Guid.NewGuid(),
                                            Code1 = tmp.PARTNER1,
                                            Code2 = tmp.PARTNER2,
                                            RELTYP = tmp.RELTYP,
                                            ZUPDMODE = tmp.ZUPDMODE,
                                            ZDATE = tmp.ZDATE
                                        });
                                    }
                                    #endregion

                                    #region 一、二批商关系
                                    else if (tmp.RELTYP == "ZS005")
                                    {
                                        distributorRelationList.Add(new MDistributorRelationSyncDto
                                        {
                                            Id = Guid.NewGuid(),
                                            DistributorCode1 = tmp.PARTNER2,
                                            DistributorCode2 = tmp.PARTNER1,
                                            UpdateMode = tmp.ZUPDMODE ?? String.Empty
                                        });

                                        returnList.Add(this.CreateCRMReturnObjectForRelation(tmp));

                                        crmRelationList.Add(new CrmRelationEntity
                                        {
                                            Id = Guid.NewGuid(),
                                            Code1 = tmp.PARTNER1,
                                            Code2 = tmp.PARTNER2,
                                            RELTYP = tmp.RELTYP,
                                            ZUPDMODE = tmp.ZUPDMODE,
                                            ZDATE = tmp.ZDATE
                                        });
                                    }
                                    #endregion

                                    #region 本渠道商主户头
                                    else if (tmp.RELTYP == "ZS004")
                                    {
                                        distributorManageAccountList.Add(new MDistributorManageAccountDto
                                        {
                                            DistributorSubAccount = tmp.PARTNER1,
                                            DistributorMainAccount = tmp.PARTNER2
                                        });

                                        returnList.Add(this.CreateCRMReturnObjectForRelation(tmp));

                                        crmRelationList.Add(new CrmRelationEntity
                                        {
                                            Id = Guid.NewGuid(),
                                            Code1 = tmp.PARTNER1,
                                            Code2 = tmp.PARTNER2,
                                            RELTYP = tmp.RELTYP,
                                            ZUPDMODE = tmp.ZUPDMODE,
                                            ZDATE = tmp.ZDATE
                                        });
                                    }
                                    #endregion

                                    #region 直接回写
                                    else
                                    {
                                        returnList.Add(this.CreateCRMReturnObjectForRelation(tmp));
                                    }
                                    #endregion
                                }
                                #endregion
                                #endregion

                                #region 写临时表处理
                                _logger.LogInformation("开始写临时表");


                                #region 经销商信息
                                if (distributorList.Count > 0)
                                {
                                    tmpDistributorTableName = $"tmp_{marketOrgCD}_dt_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                                    string tmpDistributorTableSql = this.CreateDistributorTmpTable(tmpDistributorTableName);
                                    await _mDistributorRepository.Execute(tmpDistributorTableSql);
                                    DataTable tmpDistributorDataTable = distributorList.ToDataTable();
                                    tmpDistributorDataTable.TableName = tmpDistributorTableName;

                                    //BulkInsertExtensions.SqlBulkCopy(connectionString, tmpDistributorDataTable);
                                    _dbContext.SqlBulkCopy(tmpDistributorDataTable);
                                }
                                #endregion

                                #region 终端信息
                                if (terminalList.Count > 0)
                                {
                                    tmpTerminalTableName = $"tmp_{marketOrgCD}_tmn_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                                    string tmpTerminalTableSql = this.CreateTerminalTmpTable(tmpTerminalTableName);
                                    await _mTerminalRepository.Execute(tmpTerminalTableSql);
                                    DataTable tmpTerminalDataTable = terminalList.ToDataTable();
                                    tmpTerminalDataTable.TableName = tmpTerminalTableName;

                                    //BulkInsertExtensions.SqlBulkCopy(connectionString, tmpTerminalDataTable);
                                    _dbContext.SqlBulkCopy(tmpTerminalDataTable);
                                }
                                #endregion

                                #region 经销商-终端信息
                                if (terminalDistributorList.Count > 0)
                                {
                                    tmpTerminalDistributorTableName = $"tmp_{marketOrgCD}_tmndt_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                                    string tmpTerminalDistributorTableSql = this.CreateTerminalDistributorTmpTable(tmpTerminalDistributorTableName);
                                    await _mTerminalRepository.Execute(tmpTerminalDistributorTableSql);
                                    DataTable tmpTerminalDistributorDataTable = terminalDistributorList.ToDataTable();
                                    tmpTerminalDistributorDataTable.TableName = tmpTerminalDistributorTableName;

                                    //BulkInsertExtensions.SqlBulkCopy(connectionString, tmpTerminalDistributorDataTable);

                                    _dbContext.SqlBulkCopy(tmpTerminalDistributorDataTable);
                                }
                                #endregion

                                #region 人员-终端信息
                                if (terminalUserList.Count > 0)
                                {
                                    tmpTerminalUserTableName = $"tmp_{marketOrgCD}_tmnuser_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                                    string tmpTerminalUserTableSql = this.CreateTerminalUserTmpTable(tmpTerminalUserTableName);
                                    await _mTerminalRepository.Execute(tmpTerminalUserTableSql);
                                    DataTable tmpTerminalUserDataTable = terminalUserList.ToDataTable();
                                    tmpTerminalUserDataTable.TableName = tmpTerminalUserTableName;

                                    //BulkInsertExtensions.SqlBulkCopy(connectionString, tmpTerminalUserDataTable);

                                    _dbContext.SqlBulkCopy(tmpTerminalUserDataTable);
                                }
                                #endregion

                                #region 一、二批商关系
                                if (distributorRelationList.Count > 0)
                                {
                                    tmpDistributorRelationTableName = $"tmp_{marketOrgCD}_DistributorRelation_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                                    string tmpDistributorRelationTableSql = this.CreateDistributorRelationTmpTable(tmpDistributorRelationTableName);
                                    await _mTerminalRepository.Execute(tmpDistributorRelationTableSql);
                                    DataTable tmpDistributorRelationDataTable = distributorRelationList.ToDataTable();
                                    tmpDistributorRelationDataTable.TableName = tmpDistributorRelationTableName;

                                    //BulkInsertExtensions.SqlBulkCopy(connectionString, tmpDistributorRelationDataTable);

                                    _dbContext.SqlBulkCopy(tmpDistributorRelationDataTable);
                                }
                                #endregion

                                #region 本渠道商主户头
                                if (distributorManageAccountList.Count > 0)
                                {
                                    tmpDistributorManageAccountTableName = $"tmp_{marketOrgCD}_DistributorManageAccount_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                                    string tmpDistributorManageAccountTableSql = this.CreateDistributorManageAccountTmpTable(tmpDistributorManageAccountTableName);
                                    await _mTerminalRepository.Execute(tmpDistributorManageAccountTableSql);
                                    DataTable tmpDistributorManageAccountDataTable = distributorManageAccountList.ToDataTable();
                                    tmpDistributorManageAccountDataTable.TableName = tmpDistributorManageAccountTableName;

                                    //BulkInsertExtensions.SqlBulkCopy(connectionString, tmpDistributorManageAccountDataTable);
                                    _dbContext.SqlBulkCopy(tmpDistributorManageAccountDataTable);

                                }
                                #endregion

                                #region MObject
                                if (mObjectList.Count > 0)
                                {
                                    tmpMObjectTableName = await _mObjectService.CreateSyncTmpTable(mObjectList);
                                }
                                #endregion

                                #endregion

                                #region 保存数据
                                using (var uow = _dbContext.NewUnitOfWork())
                                {
                                    _logger.LogInformation("开始保存数据");

                                    #region 经销商
                                    if (tmpDistributorTableName.IsNull() == false)
                                    {
                                        //存在时更新，不存在则新增
                                        string distributorSaveSql = this.MergeIntoMDistributor(tmpDistributorTableName);
                                        await _mDistributorRepository.Execute(distributorSaveSql, uow: uow);
                                        await _mDistributorRepository.Execute($"UPDATE A SET A.Id = D.Id FROM {tmpDistributorTableName} A INNER JOIN M_Distributor D ON D.DistributorCode = A.DistributorCode", uow: uow);
                                    }
                                    #endregion

                                    #region 终端

                                    //存在时更新，不存在则新增
                                    if (tmpTerminalTableName.IsNull() == false)
                                    {
                                        string terminalSaveSql = this.MergeIntoMTerminal(tmpTerminalTableName);
                                        await _mTerminalRepository.Execute(terminalSaveSql, uow: uow);
                                        //更新临时表终端Id
                                        await _mTerminalRepository.Execute($"UPDATE A SET A.Id = D.Id FROM {tmpTerminalTableName} A INNER JOIN M_Terminal D ON D.TerminalCode = A.TerminalCode", uow: uow);

                                        //处理终端其他明细信息
                                        terminalSaveSql = this.MergeIntoMTerminalDetail(tmpTerminalTableName);
                                        await _mTerminalRepository.Execute(terminalSaveSql, uow: uow);
                                    }
                                    #endregion

                                    #region 经销商与终端关系
                                    if (tmpTerminalDistributorTableName.IsNull() == false)
                                    {
                                        //更新临时表Id字段
                                        string updateIdSql = $@"UPDATE  A
                                                                SET     A.DistributorId = D.Id ,
                                                                        A.TerminalId = T.Id
                                                                FROM    {tmpTerminalDistributorTableName} A
                                                                        LEFT JOIN M_Distributor D ON D.DistributorCode = A.DistributorCode
                                                                        LEFT JOIN M_Terminal T ON T.TerminalCode = A.TerminalCode;";

                                        await _mTerminalDistributorRepository.Execute(updateIdSql, uow: uow);

                                        //查出关系中未匹配到经销商或终端的数据
                                        string notMatchSql = $"SELECT * FROM {tmpTerminalDistributorTableName} S WHERE S.DistributorId IS NULL OR S.TerminalId IS NULL;";
                                        var notMatchItems = await _mTerminalDistributorRepository.Query<MTerminalDistributorSyncDto>(notMatchSql, uow: uow);
                                        foreach (var item in notMatchItems)
                                        {
                                            if (!syncDtAndTmnErrorLogs.Any(a => a.Type == WarningInfoType.DistributorTerminal && a.Code1 == item.TerminalCode && a.Code2 == item.DistributorCode))
                                            {
                                                syncDtAndTmnErrorLogs.Add(this.CreateErrorLog(marketOrgCD,
                                                                                            WarningInfoType.DistributorTerminal,
                                                                                            item.TerminalCode,
                                                                                            item.DistributorCode,
                                                                                            $"【{item.DistributorCode}-{item.TerminalCode}】" +
                                                                                            this.GetErrorMessage("终端", item.TerminalCode, item.TerminalId) +
                                                                                            this.GetErrorMessage("经销商", item.DistributorCode, item.DistributorId)));
                                            }
                                            var returnItem = returnList.FirstOrDefault(f => f.ZTYPE_1 == "ZS007" && f.PARTNER1 == item.TerminalCode && f.PARTNER2 == item.DistributorCode);
                                            if (returnItem != null)
                                            {
                                                returnList.Remove(returnItem);
                                            }
                                        }

                                        //删除关系中未匹配到经销商或终端的数据
                                        string delNotMatchSql = $"DELETE S FROM {tmpTerminalDistributorTableName} S WHERE S.DistributorId IS NULL OR S.TerminalId IS NULL;";
                                        await _mTerminalDistributorRepository.Execute(delNotMatchSql, uow: uow);

                                        //不存在则新增
                                        string terminalDistributorSaveSql = $@"
                                                        MERGE INTO M_Terminal_Distributor AS T
                                                        USING {tmpTerminalDistributorTableName} AS S
                                                        ON T.TerminalCode = S.TerminalCode 
                                                           AND T.DistributorCode = S.DistributorCode
                                                           AND S.UpdateMode != 'D'
                                                        WHEN NOT MATCHED THEN
                                                            INSERT ( Id ,
                                                                     TerminalCode ,
                                                                     DistributorCode ,
                                                                     TerminalId,
                                                                     DistributorId
                                                                   )
                                                            VALUES ( S.Id ,
                                                                     S.TerminalCode ,
                                                                     S.DistributorCode ,
                                                                     S.TerminalId,
                                                                     S.DistributorId
                                                                   );";
                                        await _mTerminalDistributorRepository.Execute(terminalDistributorSaveSql, uow: uow);

                                        //ZUPDMODE是D的删除
                                        string terminalDistributorDelSql = $@"DELETE  A
                                                                FROM    M_Terminal_Distributor A
                                                                WHERE   EXISTS ( SELECT 1
                                                                                 FROM   {tmpTerminalDistributorTableName} B
                                                                                 WHERE  B.TerminalCode = A.TerminalCode
                                                                                        AND B.DistributorCode = A.DistributorCode 
						                                                                AND B.UpdateMode = 'D');";
                                        await _mTerminalDistributorRepository.Execute(terminalDistributorDelSql, uow: uow);
                                    }

                                    #endregion

                                    #region 人员终端关系
                                    if (tmpTerminalUserTableName.IsNull() == false)
                                    {
                                        //更新临时表Id字段
                                        string updateIdSql = $@"UPDATE  A
                                                                SET     A.AccountId = D.Id ,
                                                                        A.TerminalId = T.Id
                                                                FROM    {tmpTerminalUserTableName} A
                                                                        LEFT JOIN SYS_Account D ON D.UserBP = A.UserBP
                                                                        LEFT JOIN M_Terminal T ON T.TerminalCode = A.TerminalCode;";

                                        await _mTerminalUserRepository.Execute(updateIdSql, uow: uow);

                                        //查出关系中未匹配到终端或人员的数据
                                        string notMatchSql = $"SELECT * FROM {tmpTerminalUserTableName} S WHERE S.AccountId IS NULL OR S.TerminalId IS NULL;";
                                        var notMatchItems = await _mTerminalUserRepository.Query<MTerminalUserSyncDto>(notMatchSql, uow: uow);
                                        foreach (var item in notMatchItems)
                                        {
                                            if (!syncDtAndTmnErrorLogs.Any(a => a.Type == WarningInfoType.UserTerminal && a.Code1 == item.TerminalCode && a.Code2 == item.UserBP))
                                            {
                                                syncDtAndTmnErrorLogs.Add(this.CreateErrorLog(marketOrgCD,
                                                                                            WarningInfoType.UserTerminal,
                                                                                            item.TerminalCode,
                                                                                            item.UserBP,
                                                                                            $"【{item.UserBP}-{item.TerminalCode}】" +
                                                                                            this.GetErrorMessage("终端", item.TerminalCode, item.TerminalId) +
                                                                                            this.GetErrorMessage("人员", item.UserBP, item.AccountId)));
                                            }
                                            var returnItem = returnList.FirstOrDefault(f => f.ZTYPE_1 == "ZS003" && f.PARTNER1 == item.TerminalCode && f.PARTNER2 == item.UserBP);
                                            if (returnItem != null)
                                            {
                                                returnList.Remove(returnItem);
                                            }
                                        }

                                        //删除关系中未匹配到终端或人员的数据
                                        string delNotMatchSql = $"DELETE S FROM {tmpTerminalUserTableName} S WHERE S.AccountId IS NULL OR S.TerminalId IS NULL;";
                                        await _mTerminalUserRepository.Execute(delNotMatchSql, uow: uow);

                                        //不存在则新增
                                        string terminalUserSaveSql = $@"
                                                        MERGE INTO M_Terminal_User AS T
                                                        USING {tmpTerminalUserTableName} AS S
                                                        ON T.TerminalCode = S.TerminalCode 
                                                           AND T.UserBP = S.UserBP
                                                           AND S.UpdateMode != 'D'
                                                        WHEN NOT MATCHED THEN
                                                            INSERT ( Id ,
                                                                     TerminalCode ,
                                                                     UserBP,
                                                                     AccountId,
                                                                     TerminalId
                                                                   )
                                                            VALUES ( S.Id ,
                                                                     S.TerminalCode ,
                                                                     S.UserBP,
                                                                     CASE WHEN S.AccountId IS NULL THEN '{Guid.Empty}' ELSE S.AccountId END ,
                                                                     CASE WHEN S.TerminalId IS NULL THEN '{Guid.Empty}' ELSE S.TerminalId END 
                                                                   );";
                                        await _mTerminalUserRepository.Execute(terminalUserSaveSql, uow: uow);

                                        //ZUPDMODE是D的删除
                                        string terminalUserDelSql = $@"DELETE  A
                                                                FROM    M_Terminal_User A
                                                                WHERE   EXISTS ( SELECT 1
                                                                                 FROM   {tmpTerminalUserTableName} B
                                                                                 WHERE  B.TerminalCode = A.TerminalCode
                                                                                        AND B.UserBP = A.UserBP 
						                                                                AND B.UpdateMode = 'D');";
                                        await _mTerminalUserRepository.Execute(terminalUserDelSql, uow: uow);
                                    }

                                    #endregion

                                    #region 一、二批商关系
                                    if (tmpDistributorRelationTableName.IsNull() == false)
                                    {
                                        //更新临时表Id字段
                                        string updateIdSql = $@"UPDATE  A
                                                                SET     A.DistributorId1 = D.Id ,
                                                                        A.DistributorId2 = T.Id
                                                                FROM    {tmpDistributorRelationTableName} A
                                                                        LEFT JOIN M_Distributor D ON D.DistributorCode = A.DistributorCode1
                                                                        LEFT JOIN M_Distributor T ON T.DistributorCode = A.DistributorCode2;";

                                        await _mDistributorRelationRepository.Execute(updateIdSql, uow: uow);

                                        //查出关系中未匹配到一批商或二批商的数据
                                        string notMatchSql = $"SELECT * FROM {tmpDistributorRelationTableName} S WHERE S.DistributorId1 IS NULL OR S.DistributorId2 IS NULL;";
                                        var notMatchItems = await _mDistributorRelationRepository.Query<MDistributorRelationSyncDto>(notMatchSql, uow: uow);
                                        foreach (var item in notMatchItems)
                                        {
                                            if (!syncDtAndTmnErrorLogs.Any(a => a.Type == WarningInfoType.DistributorRelation && a.Code1 == item.DistributorCode1 && a.Code2 == item.DistributorCode2))
                                            {
                                                syncDtAndTmnErrorLogs.Add(this.CreateErrorLog(marketOrgCD,
                                                                                            WarningInfoType.DistributorRelation,
                                                                                            item.DistributorCode1,
                                                                                            item.DistributorCode2,
                                                                                            $"【{item.DistributorCode1}-{item.DistributorCode2}】" +
                                                                                            this.GetErrorMessage("一批商", item.DistributorCode1, item.DistributorId1) +
                                                                                            this.GetErrorMessage("二批商", item.DistributorCode2, item.DistributorId2)));
                                            }
                                            var returnItem = returnList.FirstOrDefault(f => f.ZTYPE_1 == "ZS005" && f.PARTNER1 == item.DistributorCode2 && f.PARTNER2 == item.DistributorCode1);
                                            if (returnItem != null)
                                            {
                                                returnList.Remove(returnItem);
                                            }
                                        }

                                        //删除关系中未匹配到一批商或二批商的数据
                                        string delNotMatchSql = $"DELETE S FROM {tmpDistributorRelationTableName} S WHERE S.DistributorId1 IS NULL OR S.DistributorId2 IS NULL;";
                                        await _mDistributorRelationRepository.Execute(delNotMatchSql, uow: uow);

                                        //不存在则新增
                                        string distributorRelationSaveSql = $@"
                                                        MERGE INTO M_DistributorRelation AS T
                                                        USING {tmpDistributorRelationTableName} AS S
                                                        ON T.DistributorCode1 = S.DistributorCode1 
                                                           AND T.DistributorCode2 = S.DistributorCode2
                                                           AND S.UpdateMode != 'D'
                                                        WHEN NOT MATCHED THEN
                                                            INSERT ( Id ,
                                                                     DistributorCode1 ,
                                                                     DistributorCode2 ,
                                                                     DistributorId1 ,
                                                                     DistributorId2
                                                                   )
                                                            VALUES ( S.Id ,
                                                                     S.DistributorCode1 ,
                                                                     S.DistributorCode2 ,
                                                                     S.DistributorId1 ,
                                                                     S.DistributorId2
                                                                   );";
                                        await _mDistributorRelationRepository.Execute(distributorRelationSaveSql, uow: uow);

                                        //ZUPDMODE是D的删除
                                        string distributorRelationDelSql = $@"DELETE  A
                                                                FROM    M_DistributorRelation A
                                                                WHERE   EXISTS ( SELECT 1
                                                                                 FROM   {tmpDistributorRelationTableName} B
                                                                                 WHERE  B.DistributorCode1 = A.DistributorCode1
                                                                                        AND B.DistributorCode2 = A.DistributorCode2 
						                                                                AND B.UpdateMode = 'D');";
                                        await _mDistributorRelationRepository.Execute(distributorRelationDelSql, uow: uow);
                                    }

                                    #endregion

                                    #region 本渠道商主户头
                                    if (tmpDistributorManageAccountTableName.IsNull() == false)
                                    {
                                        //更新临时表Id字段
                                        string updateIdSql = $@"UPDATE  A
                                                                SET     A.DistributorMainAccountId = T.Id 
                                                                FROM    {tmpDistributorRelationTableName} A
                                                                        INNER JOIN M_Distributor T ON T.DistributorCode = A.DistributorMainAccount;";

                                        await _mDistributorRelationRepository.Execute(updateIdSql, uow: uow);

                                        //查出关系中未匹配到主户头的数据
                                        string notMatchSql = $"SELECT * FROM {tmpDistributorRelationTableName} S WHERE S.DistributorMainAccountId IS NULL;";
                                        var notMatchItems = await _mDistributorRelationRepository.Query<MDistributorManageAccountDto>(notMatchSql, uow: uow);
                                        foreach (var item in notMatchItems)
                                        {
                                            if (!syncDtAndTmnErrorLogs.Any(a => a.Type == WarningInfoType.DistributorMainAccount && a.Code1 == item.DistributorSubAccount && a.Code2 == item.DistributorMainAccount))
                                            {
                                                syncDtAndTmnErrorLogs.Add(this.CreateErrorLog(marketOrgCD,
                                                                                            WarningInfoType.DistributorMainAccount,
                                                                                            item.DistributorSubAccount,
                                                                                            item.DistributorMainAccount,
                                                                                            $"【{item.DistributorSubAccount}-{item.DistributorMainAccount}】" +
                                                                                            this.GetErrorMessage("本渠道商主户头", item.DistributorMainAccount, item.DistributorMainAccountId)));
                                            }
                                            var returnItem = returnList.FirstOrDefault(f => f.ZTYPE_1 == "ZS004" && f.PARTNER1 == item.DistributorSubAccount && f.PARTNER2 == item.DistributorMainAccount);
                                            if (returnItem != null)
                                            {
                                                returnList.Remove(returnItem);
                                            }
                                        }


                                        //删除关系中未匹配到主户头的数据
                                        string delNotMatchSql = $"DELETE S FROM {tmpDistributorRelationTableName} S WHERE S.DistributorMainAccountId IS NULL;";
                                        await _mDistributorRelationRepository.Execute(delNotMatchSql, uow: uow);

                                        string distributorManageAccountUpdateSql = $@"
                                                                UPDATE  A
                                                                SET A.Relation = 2, A.CustomerCode = B.DistributorMainAccount, ParentId = B.DistributorMainAccountId
                                                                FROM    M_Distributor A
                                                                INNER JOIN  {tmpDistributorRelationTableName} B ON A.DistributorCode = B.DistributorSubAccount
                                                                 ;";
                                        await _mDistributorRepository.Execute(distributorManageAccountUpdateSql, uow: uow);
                                    }

                                    #endregion

                                    #region MObject对象表
                                    if (mObjectList.Count > 0)
                                    {
                                        await _mObjectService.BatchProcess(tmpMObjectTableName, uow: uow);
                                    }
                                    #endregion

                                    uow.SaveChanges();
                                }
                                #endregion

                                #region 写数据库日志

                                #region 预警信息表
                                if (syncDtAndTmnErrorLogs.Any(a => a.IsSave == false))
                                {
                                    List<WarningInfoAddDto> logs = new List<WarningInfoAddDto>();
                                    var notSaveLogs = syncDtAndTmnErrorLogs.Where(w => w.IsSave == false).ToList();
                                    foreach (var item in notSaveLogs)
                                    {
                                        logs.Add(new WarningInfoAddDto
                                        {
                                            OrgId = curMarketOrg?.Id.ToString(),
                                            OrgCodeName = curMarketOrg?.ObjectName,
                                            Type = item.Type,
                                            Mess1 = item.Message,
                                            CreatedTime = DateTime.Now,
                                        });
                                        item.IsSave = true;
                                    }
                                    await _warningInfoService.BulkAdd(logs);
                                }
                                #endregion

                                #region 记录返写CRM记录
                                if (returnList.Count > 0)
                                {
                                    foreach (var item in returnList)
                                    {
                                        crmRewriteList.Add(new CrmRewriteEntity
                                        {
                                            Id = Guid.NewGuid(),
                                            PARTNER1 = item.PARTNER1,
                                            PARTNER2 = item.PARTNER2,
                                            ZUSER = item.ZUSER,
                                            ZTYPE = item.ZTYPE,
                                            ZTYPE_1 = item.ZTYPE_1,
                                            ZDATE = item.ZDATE,
                                            ZBAIOS = item.ZBAIOS,
                                            CreateTime = DateTime.Now
                                        });
                                    }
                                    await _crmRewriteService.BulkAdd(crmRewriteList);
                                }
                                #endregion

                                #region 记录CRM的关系变动记录表
                                if (crmRelationList.Count > 0)
                                {
                                    await _crmRelationService.BulkAdd(crmRelationList);
                                }
                                #endregion

                                #region 记录CRM数据
                                List<CrmDataSyncDto> crmDatas = new List<CrmDataSyncDto>();
                                if (tmpDistributorTableName.IsNull() == false)
                                {
                                    string sql = $@"select Id, 7 AS DataType,DistributorCode AS Code, JsonString, ZDATE
                                                        from {tmpDistributorTableName};";
                                    crmDatas = (await _mDistributorRepository.Query<CrmDataSyncDto>(sql)).ToList();
                                }
                                if (tmpTerminalTableName.IsNull() == false)
                                {
                                    string sql = $@"select Id, 8 AS DataType, TerminalCode AS Code, JsonString, ZDATE
                                                        from {tmpTerminalTableName};";
                                    crmDatas.AddRange((await _mDistributorRepository.Query<CrmDataSyncDto>(sql)).ToList());
                                }
                                if (crmDatas.Count > 0)
                                {
                                    await _crmDataService.AddOrUpdate(crmDatas);
                                }
                                #endregion

                                #endregion

                                #region 删除临时表
                                _logger.LogInformation("开始删除临时表");
                                await this.DeleteTmpTable(tmpDistributorTableName);
                                await this.DeleteTmpTable(tmpTerminalTableName);
                                await this.DeleteTmpTable(tmpTerminalDistributorTableName);
                                await this.DeleteTmpTable(tmpTerminalUserTableName);
                                await this.DeleteTmpTable(tmpDistributorRelationTableName);
                                await this.DeleteTmpTable(tmpDistributorManageAccountTableName);
                                await this.DeleteTmpTable(tmpMObjectTableName);
                                #endregion

                                #region 返写  
                                _logger.LogInformation("开始返写数据");

                                if (returnList.Count == 0)
                                {
                                    break;
                                }
                                while (returnList.Count > 0)
                                {
                                    //每次返写5000条，分批返写。
                                    var returndata = new
                                    {
                                        IT_RETURN = returnList.Take(5000).ToArray()
                                    };
                                    if (returndata.IT_RETURN.Length > 0)
                                    {
                                        returnList.RemoveRange(0, returndata.IT_RETURN.Length);
                                    }
                                    someFunction.Invoke(returndata);
                                }

                                #endregion

                            }
                            else
                            {
                                break;
                            }
                            await Task.Delay(100);
                        }
                    }
                }
                return ResultModel.Success("同步成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return ResultModel.Failed("同步失败：" + ex.ToString());
            }
            finally
            {
                _logger.LogError(String.Join("\r\n", Newtonsoft.Json.JsonConvert.SerializeObject(syncDtAndTmnErrorLogs)));
            }
        }
        #endregion

        #region 公共方法

        #region 创建临时表
        /// <summary>
        /// 创建经销商临时表
        /// </summary>
        /// <param name="tmpDistributorTableName">临时表名</param>
        /// <returns></returns>
        private string CreateDistributorTmpTable(string tmpDistributorTableName)
        {
            string tmpDistributorTableSql = $@"CREATE TABLE [{tmpDistributorTableName}] (
                                                            [Id] uniqueidentifier NOT NULL,
                                                            [DistributorCode] nvarchar(30) NOT NULL,
                                                            [DistributorName] nvarchar(60) NULL,
                                                            [DistributorType] int NULL,
                                                            [StationId] uniqueidentifier NULL,
                                                            [Status] int  NOT NULL,
                                                            [CrmCode] nvarchar(10) NULL,
                                                            [DetailType] int NOT NULL,
                                                            [CustomerCode] nvarchar(30) NULL,
                                                            [CreatedBy] uniqueidentifier NULL,
                                                            [Creator] nvarchar(50) NULL,
                                                            [CreatedTime] datetime NULL,
                                                            [JsonString] text NULL,
                                                            [ZDATE] datetime NULL,
                                                            [EntityId] uniqueidentifier NULL,
                                                            PRIMARY KEY ([Id])
                                                )";
            return tmpDistributorTableSql;
        }

        /// <summary>
        /// 创建终端临时表
        /// </summary>
        /// <param name="tmpTerminalTableName">临时表名</param>
        /// <returns></returns>
        private string CreateTerminalTmpTable(string tmpTerminalTableName)
        {
            string tmpTerminalTableSql = $@"
                                CREATE TABLE [{tmpTerminalTableName}] (
                                    [Id] uniqueidentifier  NOT NULL,
                                    [TerminalCode] char(10) NOT NULL,
                                    [TerminalName] nvarchar(40) NOT NULL,
                                    [StationId] uniqueidentifier NULL,
                                    [SaleLine] varchar(10) NULL,
                                    [Lvl1Type] varchar(10) NULL,
                                    [Lvl2Type] varchar(10) NULL,
                                    [Lvl3Type] varchar(10) NULL,
                                    [Status] int  NOT NULL,
                                    [Address] nvarchar(500) NULL,
                                    [JsonString] text NULL,
                                    [ZDATE] datetime NULL,
                                    [Tel] nvarchar(30) NULL,
                                    [Prov] varchar(3) NULL,
                                    [City] nvarchar(10) NULL,
                                    [Country] nvarchar(20) NULL,
                                    [Street] nvarchar(60) NULL,
                                    [Village] nvarchar(20) NULL,
                                    [AddDetail] nvarchar(60) NULL,
                                    [TmnOwner] nvarchar(12) NULL,
                                    [TmnPhone] nvarchar(20) NULL,
                                    [Per1Nm] nvarchar(20) NULL,
                                    [Per1Post] nvarchar(20) NULL,
                                    [Per1Bir] char(8) NULL,
                                    [Per1Tel] nvarchar(20) NULL,
                                    [Per2Nm] nvarchar(20) NULL,
                                    [Per2Post] varchar(20) NULL,
                                    [Per2Bir] char(8) NULL,
                                    [Per2Tel] nvarchar(20) NULL,
                                    [Per3Nm] nvarchar(20) NULL,
                                    [Per3Post] varchar(20) NULL,
                                    [Per3Bir] char(8) NULL,
                                    [Per3Tel] nvarchar(20) NULL,
                                    [Geo] char(2) NULL,
                                    [CoopNature] char(2) NULL,
                                    [SysNum] nvarchar(40) NULL,
                                    [SysNm] nvarchar(40) NULL,
                                    [SaleChannel] varchar(2) NULL,
                                    [IsProtocol] bit  NOT NULL,
                                    [RL] numeric(10)  NULL,
                                    [XYLY] char(2) NULL,
                                    [ZGDFL] char(4) NULL,
                                    [FaxNumber] nvarchar(30) NULL,
                                    [NamCountry] nvarchar(3) NULL,
                                    [ZZKASystem1] nvarchar(10) NULL,
                                    [ZZFMS_MUM] nvarchar(40) NULL,
                                    [ZZTable] nvarchar(3) NULL,
                                    [ZZSeat] nvarchar(5) NULL,
                                    [ZZWEIXIN_NUM] nvarchar(20) NULL,
                                    [ZZAge] nvarchar(10) NULL,
                                    [ZZInner_Area] nvarchar(10) NULL,
                                    [ZZOut_Area] nvarchar(10) NULL,
                                    [ZZBEER] nvarchar(10) NULL,
                                    [ZZCHAIN_NAME] nvarchar(40) NULL,
                                    [ZZCHAIN_TEL] nvarchar(20) NULL,
                                    [ZZCHAIN_TYPE] nvarchar(2) NULL,
                                    [ZZCHAIN_QUA] nvarchar(2) NULL,
                                    [ZZCHAIN_NUM] nvarchar(3) NULL,
                                    [ZZCUISINE] nvarchar(2) NULL,
                                    [ZZCHARACTERISTIC] nvarchar(2) NULL,
                                    [ZZPERCONSUME] nvarchar(15) NULL,
                                    [ZZOPEN_TIME] nvarchar(20) NULL,
                                    [ZZFREEZER] nvarchar(3) NULL,
                                    [ZZFLD0000CG] nvarchar(20) NULL,
                                    [ZZVIRTUAL] nvarchar(2) NULL,
                                    [ZZVISIT] decimal(12,2)  NULL,
                                    [ZZCHARACTER] nvarchar(2) NULL,
                                    [ZZSTORAGE] nvarchar(10) NULL,
                                    [ZZFLD000052] nvarchar(10) NULL,
                                    [ZZSMALLBOX_NUM] nvarchar(3) NULL,
                                    [ZZPRO_NUM2] nvarchar(40) NULL,
                                    [ZZPRO_NAME2] nvarchar(40) NULL,
                                    [ZZALCO] nvarchar(10) NULL,
                                    [ZZBEST_TIME] nvarchar(20) NULL,
                                    [ZZWHET_CHAIN] nvarchar(2) NULL,
                                    [ZZBIGBOX_NUM] nvarchar(3) NULL,
                                    [ZZMIDBOX_NUM] nvarchar(3) NULL,
                                    [ZZPORN_PRICE] nvarchar(10) NULL,
                                    [ZZPRO_RANK] nvarchar(2) NULL,
                                    [ZZDAY_REVENUE] nvarchar(10) NULL,
                                    [ZZCASHIER_NUM] nvarchar(3) NULL,
                                    [ZZDISTRI_WAY] nvarchar(2) NULL,
                                    [ZZFLD00005D] decimal(13,2)  NULL,
                                    [ZZRECONCILIATION] nvarchar(10) NULL,
                                    [ZZACCOUNT_WAY] nvarchar(10) NULL,
                                    [ZZACCCOUNT_TIME] nvarchar(10) NULL,
                                    [ZZFIPERSON] nvarchar(20) NULL,
                                    [ZZFIPERSON_TEL] nvarchar(20) NULL,
                                    [E_MAILSMT] nvarchar(50) NULL,
                                    [URIURI] nvarchar(100) NULL,
                                    [BZ] nvarchar(200) NULL,
                                    [ZZDELIVER_NOTE] nvarchar(2) NULL,
                                    [ZZCARLIMIT_DESC] nvarchar(40) NULL,
                                    [ZZACCOUNT_PERIOD] nvarchar(5) NULL,
                                    [ZZKABEER_NUM] nvarchar(3) NULL,
                                    [ZZKABEER_PILE] nvarchar(3) NULL,
                                    [ZZKANONBEER_PILE] nvarchar(3) NULL,
                                    [ZZKAICE_NUM] nvarchar(3) NULL,
                                    [ZZKACOLD_NUM] nvarchar(3) NULL,
                                    [ZZKASHELF_NUM] nvarchar(3) NULL,
                                    [ZZKALEVEL_NUM] nvarchar(3) NULL,
                                    [ZZKAWHOLEBOX_NUM] nvarchar(2) NULL,
                                    [ZZKAPACKAGE_NUM] nvarchar(2) NULL,
                                    [ZZKAPILE_USE] nvarchar(2) NULL,
                                    [ZZKANONPILE_USE] nvarchar(2) NULL,
                                    [ZZKAPRO_USE] nvarchar(2) NULL,
                                    [ZZKASHELF_USE] nvarchar(2) NULL,
                                    [ZZKAICE_USE] nvarchar(2) NULL,
                                    [ZZKACOLD_USE] nvarchar(2) NULL,
                                    [ZZKACASHER_USE] nvarchar(2) NULL,
                                    [ZZKAMULTI_USE] nvarchar(2) NULL,
                                    [ZZKADISPLAY_USE] nvarchar(20) NULL,
                                    [ZZKAPILEOUT_USE] nvarchar(2) NULL,
                                    [ZZFLD0000G2] nvarchar(2) NULL,
                                    [ZZKAFLAG_USE] nvarchar(2) NULL,
                                    [ZZKAPOST_USE] nvarchar(2) NULL,
                                    [ZZKAAB_USE] nvarchar(2) NULL,
                                    [ZZFLD0000G6] nvarchar(2) NULL,
                                    [ZZKALADDER_USE] nvarchar(2) NULL,
                                    [ZZKASERVICE_USE] nvarchar(2) NULL,
                                    [ZZKAPOP_USE] nvarchar(2) NULL,
                                    [ZZKALIVELY_USE] nvarchar(20) NULL,
                                    [ZZBOX] int  NULL,
                                    [ZZDECK_NAME] nvarchar(6) NULL,
                                    [ZbnType] nvarchar(10) NULL,
                                    [ZZGSYYZZH] nvarchar(30) NULL,
                                    [ZZGSZZMC] nvarchar(80) NULL,
                                    PRIMARY KEY ([Id]))";
            return tmpTerminalTableSql;
        }

        /// <summary>
        /// 创建经销商、终端关系临时表
        /// </summary>
        /// <param name="tmpTerminalDistributorTableName">临时表名</param>
        /// <returns></returns>
        private string CreateTerminalDistributorTmpTable(string tmpTerminalDistributorTableName)
        {
            string tmpTerminalDistributorTableSql = $@"CREATE TABLE [{tmpTerminalDistributorTableName}] (
                                                                    [Id] uniqueidentifier  NOT NULL,
                                                                    [TerminalCode] char(10) NOT NULL,
                                                                    [DistributorCode] varchar(10) NOT NULL,
                                                                    [TerminalId] uniqueidentifier NULL,
                                                                    [DistributorId] uniqueidentifier NULL,
                                                                    [UpdateMode] varchar(10) NOT NULL,
                                                                    PRIMARY KEY ([Id])
                                                )";
            return tmpTerminalDistributorTableSql;
        }

        /// <summary>
        /// 创建人员、终端关系临时表
        /// </summary>
        /// <param name="tmpTerminalUserTableName">临时表名</param>
        /// <returns></returns>
        private string CreateTerminalUserTmpTable(string tmpTerminalUserTableName)
        {
            string tmpTerminalUserTableSql = $@"CREATE TABLE [{tmpTerminalUserTableName}] (
                                                                    [Id] uniqueidentifier  NOT NULL,
                                                                    [TerminalCode] char(10) NOT NULL,
                                                                    [UserBP] varchar(10) NOT NULL,
                                                                    [AccountId] uniqueidentifier NULL,
                                                                    [TerminalId] uniqueidentifier NULL,
                                                                    [UpdateMode] varchar(10) NOT NULL,
                                                                    PRIMARY KEY ([Id])
                                                )";
            return tmpTerminalUserTableSql;
        }

        /// <summary>
        /// 创建一、二批商关系临时表
        /// </summary>
        /// <param name="tmpDistributorRelationTableName">临时表名</param>
        /// <returns></returns>
        private string CreateDistributorRelationTmpTable(string tmpDistributorRelationTableName)
        {
            string tmpDistributorRelationTableSql = $@"CREATE TABLE [{tmpDistributorRelationTableName}] (
                                                                                [Id] uniqueidentifier  NOT NULL,
                                                                                [DistributorCode1] nvarchar(30) NOT NULL,
                                                                                [DistributorCode2] nvarchar(30) NOT NULL,
                                                                                [DistributorId1] uniqueidentifier NULL,
                                                                                [DistributorId2] uniqueidentifier NULL,
                                                                                [UpdateMode] varchar(10) NOT NULL,
                                                                                PRIMARY KEY ([Id])
                                                )";
            return tmpDistributorRelationTableSql;
        }

        /// <summary>
        /// 创建经销商管理开户关系临时表
        /// </summary>
        /// <param name="tmpDistributorManageAccountTableName">临时表名</param>
        /// <returns></returns>
        private string CreateDistributorManageAccountTmpTable(string tmpDistributorManageAccountTableName)
        {
            string tmpDistributorManageAccountTableSql = $@"CREATE TABLE [{tmpDistributorManageAccountTableName}] (
                                                                                [Id] uniqueidentifier  NOT NULL,
                                                                                [DistributorMainAccount] nvarchar(30) NOT NULL,
                                                                                [DistributorSubAccount] nvarchar(30) NOT NULL,
                                                                                [DistributorMainAccountId] uniqueidentifier NULL,
                                                                                [UpdateMode] varchar(10) NOT NULL,
                                                                                PRIMARY KEY ([Id])
                                                )";
            return tmpDistributorManageAccountTableSql;
        }
        #endregion

        #region 保存数据SQL
        /// <summary>
        /// 经销商数据保存SQL
        /// </summary>
        /// <param name="tmpDistributorTableName">临时表名</param>
        /// <returns></returns>
        private string MergeIntoMDistributor(string tmpDistributorTableName)
        {
            //存在时更新，不存在则新增
            string distributorSaveSql = $@"
                                MERGE INTO M_Distributor AS T
                                USING {tmpDistributorTableName} AS S
                                ON T.DistributorCode = S.DistributorCode
                                WHEN MATCHED THEN
                                    UPDATE SET T.DistributorName = S.DistributorName ,
                                                T.DistributorType = S.DistributorType ,
                                                T.StationId = CASE WHEN T.IsSynchronizeCRMStation = 1 THEN S.StationId ELSE T.StationId END ,
                                                T.Status = S.Status ,
                                                T.CrmCode = S.CrmCode ,
                                                T.EntityId = S.EntityId ,
                                                T.ModifiedTime = GETDATE()
                                WHEN NOT MATCHED THEN
                                    INSERT ( Id ,
                                                DistributorCode ,
                                                DistributorName ,
                                                DistributorType ,
                                                EntityId ,
                                                StationId ,
                                                Status ,
                                                CrmCode ,
                                                DetailType ,
                                                CustomerCode ,
                                                ParentId ,
                                                IsSynchronizeCRMStation ,
                                                Deleted ,
                                                CreatedBy ,
                                                Creator ,
                                                CreatedTime
                                            )
                                    VALUES ( S.Id ,
                                                S.DistributorCode ,
                                                S.DistributorName ,
                                                S.DistributorType ,
                                                S.EntityId ,
                                                S.StationId ,
                                                S.Status ,
                                                S.CrmCode ,
                                                S.DetailType ,
                                                S.CustomerCode ,
                                                '{Guid.Empty}' ,
                                                1 ,
                                                0 ,
                                                S.CreatedBy ,
                                                S.Creator ,
                                                S.CreatedTime
                                            );";
            return distributorSaveSql;
        }

        /// <summary>
        /// 终端主表数据保存SQL
        /// </summary>
        /// <param name="tmpTerminalTableName">临时表名</param>
        /// <returns></returns>
        private string MergeIntoMTerminal(string tmpTerminalTableName)
        {
            //存在时更新，不存在则新增
            string terminalSaveSql = $@"
                                    MERGE INTO M_Terminal AS T
                                    USING {tmpTerminalTableName} AS S
                                    ON T.TerminalCode = S.TerminalCode
                                    WHEN MATCHED THEN
                                        UPDATE SET T.TerminalName = S.TerminalName ,
                                                    T.StationId = S.StationId ,
                                                    T.SaleLine = S.SaleLine ,
                                                    T.Lvl1Type = S.Lvl1Type ,
                                                    T.Lvl2Type = S.Lvl2Type ,
                                                    T.Lvl3Type = S.Lvl3Type ,
                                                    T.Status = S.Status ,
                                                    T.Address = S.Address
                                    WHEN NOT MATCHED THEN
                                        INSERT ( Id ,
                                                    TerminalCode ,
                                                    TerminalName ,
                                                    StationId ,
                                                    SaleLine ,
                                                    Lvl1Type ,
                                                    Lvl2Type ,
                                                    Lvl3Type ,
                                                    Status ,
                                                    Address
                                                )
                                        VALUES ( S.Id ,
                                                    S.TerminalCode ,
                                                    S.TerminalName ,
                                                    S.StationId ,
                                                    S.SaleLine ,
                                                    S.Lvl1Type ,
                                                    S.Lvl2Type ,
                                                    S.Lvl3Type ,
                                                    S.Status ,
                                                    S.Address
                                                );";
            return terminalSaveSql;
        }

        /// <summary>
        /// 终端明细表数据保存SQL
        /// </summary>
        /// <param name="tmpTerminalTableName">临时表名</param>
        /// <returns></returns>
        private string MergeIntoMTerminalDetail(string tmpTerminalTableName)
        {
            //存在时更新，不存在则新增
            string terminalSaveSql = $@"
                                MERGE INTO M_TerminalDetail AS T
                                USING {tmpTerminalTableName} AS S
                                ON T.Id = S.Id
                                WHEN MATCHED THEN
                                    UPDATE SET   T.Id = S.Id, 
                                                    T.Tel = S.Tel, 
                                                    T.Prov = S.Prov, 
                                                    T.City = S.City, 
                                                    T.Country = S.Country, 
                                                    T.Street = S.Street, 
                                                    T.Village = S.Village, 
                                                    T.AddDetail = S.AddDetail, 
                                                    T.TmnOwner = S.TmnOwner, 
                                                    T.TmnPhone = S.TmnPhone, 
                                                    T.Per1Nm = S.Per1Nm, 
                                                    T.Per1Post = S.Per1Post, 
                                                    T.Per1Bir = S.Per1Bir, 
                                                    T.Per1Tel = S.Per1Tel, 
                                                    T.Per2Nm = S.Per2Nm, 
                                                    T.Per2Post = S.Per2Post, 
                                                    T.Per2Bir = S.Per2Bir, 
                                                    T.Per2Tel = S.Per2Tel, 
                                                    T.Per3Nm = S.Per3Nm, 
                                                    T.Per3Post = S.Per3Post, 
                                                    T.Per3Bir = S.Per3Bir, 
                                                    T.Per3Tel = S.Per3Tel, 
                                                    T.Geo = S.Geo, 
                                                    T.CoopNature = S.CoopNature, 
                                                    T.SysNum = S.SysNum, 
                                                    T.SysNm = S.SysNm, 
                                                    T.SaleChannel = S.SaleChannel, 
                                                    T.IsProtocol = S.IsProtocol, 
                                                    T.RL = S.RL, 
                                                    T.XYLY = S.XYLY, 
                                                    T.ZGDFL = S.ZGDFL, 
                                                    T.FaxNumber = S.FaxNumber, 
                                                    T.NamCountry = S.NamCountry, 
                                                    T.ZZKASystem1 = S.ZZKASystem1, 
                                                    T.ZZFMS_MUM = S.ZZFMS_MUM, 
                                                    T.ZZTable = S.ZZTable, 
                                                    T.ZZSeat = S.ZZSeat, 
                                                    T.ZZWEIXIN_NUM = S.ZZWEIXIN_NUM, 
                                                    T.ZZAge = S.ZZAge, 
                                                    T.ZZInner_Area = S.ZZInner_Area, 
                                                    T.ZZOut_Area = S.ZZOut_Area, 
                                                    T.ZZBEER = S.ZZBEER, 
                                                    T.ZZCHAIN_NAME = S.ZZCHAIN_NAME, 
                                                    T.ZZCHAIN_TEL = S.ZZCHAIN_TEL, 
                                                    T.ZZCHAIN_TYPE = S.ZZCHAIN_TYPE, 
                                                    T.ZZCHAIN_QUA = S.ZZCHAIN_QUA, 
                                                    T.ZZCHAIN_NUM = S.ZZCHAIN_NUM, 
                                                    T.ZZCUISINE = S.ZZCUISINE, 
                                                    T.ZZCHARACTERISTIC = S.ZZCHARACTERISTIC, 
                                                    T.ZZPERCONSUME = S.ZZPERCONSUME, 
                                                    T.ZZOPEN_TIME = S.ZZOPEN_TIME, 
                                                    T.ZZFREEZER = S.ZZFREEZER, 
                                                    T.ZZFLD0000CG = S.ZZFLD0000CG, 
                                                    T.ZZVIRTUAL = S.ZZVIRTUAL, 
                                                    T.ZZVISIT = S.ZZVISIT, 
                                                    T.ZZCHARACTER = S.ZZCHARACTER, 
                                                    T.ZZSTORAGE = S.ZZSTORAGE, 
                                                    T.ZZFLD000052 = S.ZZFLD000052, 
                                                    T.ZZSMALLBOX_NUM = S.ZZSMALLBOX_NUM, 
                                                    T.ZZPRO_NUM2 = S.ZZPRO_NUM2, 
                                                    T.ZZPRO_NAME2 = S.ZZPRO_NAME2, 
                                                    T.ZZALCO = S.ZZALCO, 
                                                    T.ZZBEST_TIME = S.ZZBEST_TIME, 
                                                    T.ZZWHET_CHAIN = S.ZZWHET_CHAIN, 
                                                    T.ZZBIGBOX_NUM = S.ZZBIGBOX_NUM, 
                                                    T.ZZMIDBOX_NUM = S.ZZMIDBOX_NUM, 
                                                    T.ZZPORN_PRICE = S.ZZPORN_PRICE, 
                                                    T.ZZPRO_RANK = S.ZZPRO_RANK, 
                                                    T.ZZDAY_REVENUE = S.ZZDAY_REVENUE, 
                                                    T.ZZCASHIER_NUM = S.ZZCASHIER_NUM, 
                                                    T.ZZDISTRI_WAY = S.ZZDISTRI_WAY, 
                                                    T.ZZFLD00005D = S.ZZFLD00005D, 
                                                    T.ZZRECONCILIATION = S.ZZRECONCILIATION, 
                                                    T.ZZACCOUNT_WAY = S.ZZACCOUNT_WAY, 
                                                    T.ZZACCCOUNT_TIME = S.ZZACCCOUNT_TIME, 
                                                    T.ZZFIPERSON = S.ZZFIPERSON, 
                                                    T.ZZFIPERSON_TEL = S.ZZFIPERSON_TEL, 
                                                    T.E_MAILSMT = S.E_MAILSMT, 
                                                    T.URIURI = S.URIURI, 
                                                    T.BZ = S.BZ, 
                                                    T.ZZDELIVER_NOTE = S.ZZDELIVER_NOTE, 
                                                    T.ZZCARLIMIT_DESC = S.ZZCARLIMIT_DESC, 
                                                    T.ZZACCOUNT_PERIOD = S.ZZACCOUNT_PERIOD, 
                                                    T.ZZKABEER_NUM = S.ZZKABEER_NUM, 
                                                    T.ZZKABEER_PILE = S.ZZKABEER_PILE, 
                                                    T.ZZKANONBEER_PILE = S.ZZKANONBEER_PILE, 
                                                    T.ZZKAICE_NUM = S.ZZKAICE_NUM, 
                                                    T.ZZKACOLD_NUM = S.ZZKACOLD_NUM, 
                                                    T.ZZKASHELF_NUM = S.ZZKASHELF_NUM, 
                                                    T.ZZKALEVEL_NUM = S.ZZKALEVEL_NUM, 
                                                    T.ZZKAWHOLEBOX_NUM = S.ZZKAWHOLEBOX_NUM, 
                                                    T.ZZKAPACKAGE_NUM = S.ZZKAPACKAGE_NUM, 
                                                    T.ZZKAPILE_USE = S.ZZKAPILE_USE, 
                                                    T.ZZKANONPILE_USE = S.ZZKANONPILE_USE, 
                                                    T.ZZKAPRO_USE = S.ZZKAPRO_USE, 
                                                    T.ZZKASHELF_USE = S.ZZKASHELF_USE, 
                                                    T.ZZKAICE_USE = S.ZZKAICE_USE, 
                                                    T.ZZKACOLD_USE = S.ZZKACOLD_USE, 
                                                    T.ZZKACASHER_USE = S.ZZKACASHER_USE, 
                                                    T.ZZKAMULTI_USE = S.ZZKAMULTI_USE, 
                                                    T.ZZKADISPLAY_USE = S.ZZKADISPLAY_USE, 
                                                    T.ZZKAPILEOUT_USE = S.ZZKAPILEOUT_USE, 
                                                    T.ZZFLD0000G2 = S.ZZFLD0000G2, 
                                                    T.ZZKAFLAG_USE = S.ZZKAFLAG_USE, 
                                                    T.ZZKAPOST_USE = S.ZZKAPOST_USE, 
                                                    T.ZZKAAB_USE = S.ZZKAAB_USE, 
                                                    T.ZZFLD0000G6 = S.ZZFLD0000G6, 
                                                    T.ZZKALADDER_USE = S.ZZKALADDER_USE, 
                                                    T.ZZKASERVICE_USE = S.ZZKASERVICE_USE, 
                                                    T.ZZKAPOP_USE = S.ZZKAPOP_USE, 
                                                    T.ZZKALIVELY_USE = S.ZZKALIVELY_USE, 
                                                    T.ZZBOX = S.ZZBOX, 
                                                    T.ZZDECK_NAME = S.ZZDECK_NAME, 
                                                    T.ZbnType = S.ZbnType, 
                                                    T.ZZGSYYZZH = S.ZZGSYYZZH, 
                                                    T.ZZGSZZMC = S.ZZGSZZMC
                                WHEN NOT MATCHED THEN
                                    INSERT (  Id ,
                                                Tel ,
                                                Prov ,
                                                City ,
                                                Country ,
                                                Street ,
                                                Village ,
                                                AddDetail ,
                                                TmnOwner ,
                                                TmnPhone ,
                                                Per1Nm ,
                                                Per1Post ,
                                                Per1Bir ,
                                                Per1Tel ,
                                                Per2Nm ,
                                                Per2Post ,
                                                Per2Bir ,
                                                Per2Tel ,
                                                Per3Nm ,
                                                Per3Post ,
                                                Per3Bir ,
                                                Per3Tel ,
                                                Geo ,
                                                CoopNature ,
                                                SysNum ,
                                                SysNm ,
                                                SaleChannel ,
                                                IsProtocol ,
                                                RL ,
                                                XYLY ,
                                                ZGDFL ,
                                                FaxNumber ,
                                                NamCountry ,
                                                ZZKASystem1 ,
                                                ZZFMS_MUM ,
                                                ZZTable ,
                                                ZZSeat ,
                                                ZZWEIXIN_NUM ,
                                                ZZAge ,
                                                ZZInner_Area ,
                                                ZZOut_Area ,
                                                ZZBEER ,
                                                ZZCHAIN_NAME ,
                                                ZZCHAIN_TEL ,
                                                ZZCHAIN_TYPE ,
                                                ZZCHAIN_QUA ,
                                                ZZCHAIN_NUM ,
                                                ZZCUISINE ,
                                                ZZCHARACTERISTIC ,
                                                ZZPERCONSUME ,
                                                ZZOPEN_TIME ,
                                                ZZFREEZER ,
                                                ZZFLD0000CG ,
                                                ZZVIRTUAL ,
                                                ZZVISIT ,
                                                ZZCHARACTER ,
                                                ZZSTORAGE ,
                                                ZZFLD000052 ,
                                                ZZSMALLBOX_NUM ,
                                                ZZPRO_NUM2 ,
                                                ZZPRO_NAME2 ,
                                                ZZALCO ,
                                                ZZBEST_TIME ,
                                                ZZWHET_CHAIN ,
                                                ZZBIGBOX_NUM ,
                                                ZZMIDBOX_NUM ,
                                                ZZPORN_PRICE ,
                                                ZZPRO_RANK ,
                                                ZZDAY_REVENUE ,
                                                ZZCASHIER_NUM ,
                                                ZZDISTRI_WAY ,
                                                ZZFLD00005D ,
                                                ZZRECONCILIATION ,
                                                ZZACCOUNT_WAY ,
                                                ZZACCCOUNT_TIME ,
                                                ZZFIPERSON ,
                                                ZZFIPERSON_TEL ,
                                                E_MAILSMT ,
                                                URIURI ,
                                                BZ ,
                                                ZZDELIVER_NOTE ,
                                                ZZCARLIMIT_DESC ,
                                                ZZACCOUNT_PERIOD ,
                                                ZZKABEER_NUM ,
                                                ZZKABEER_PILE ,
                                                ZZKANONBEER_PILE ,
                                                ZZKAICE_NUM ,
                                                ZZKACOLD_NUM ,
                                                ZZKASHELF_NUM ,
                                                ZZKALEVEL_NUM ,
                                                ZZKAWHOLEBOX_NUM ,
                                                ZZKAPACKAGE_NUM ,
                                                ZZKAPILE_USE ,
                                                ZZKANONPILE_USE ,
                                                ZZKAPRO_USE ,
                                                ZZKASHELF_USE ,
                                                ZZKAICE_USE ,
                                                ZZKACOLD_USE ,
                                                ZZKACASHER_USE ,
                                                ZZKAMULTI_USE ,
                                                ZZKADISPLAY_USE ,
                                                ZZKAPILEOUT_USE ,
                                                ZZFLD0000G2 ,
                                                ZZKAFLAG_USE ,
                                                ZZKAPOST_USE ,
                                                ZZKAAB_USE ,
                                                ZZFLD0000G6 ,
                                                ZZKALADDER_USE ,
                                                ZZKASERVICE_USE ,
                                                ZZKAPOP_USE ,
                                                ZZKALIVELY_USE ,
                                                ZZBOX ,
                                                ZZDECK_NAME ,
                                                ZbnType ,
                                                ZZGSYYZZH ,
                                                ZZGSZZMC ,
                                                Deleted ,
                                                CreatedBy ,
                                                Creator ,
                                                CreatedTime
                                            )  
                                    VALUES (  S.Id ,
                                                S.Tel ,
                                                S.Prov ,
                                                S.City ,
                                                S.Country ,
                                                S.Street ,
                                                S.Village ,
                                                S.AddDetail ,
                                                S.TmnOwner ,
                                                S.TmnPhone ,
                                                S.Per1Nm ,
                                                S.Per1Post ,
                                                S.Per1Bir ,
                                                S.Per1Tel ,
                                                S.Per2Nm ,
                                                S.Per2Post ,
                                                S.Per2Bir ,
                                                S.Per2Tel ,
                                                S.Per3Nm ,
                                                S.Per3Post ,
                                                S.Per3Bir ,
                                                S.Per3Tel ,
                                                S.Geo ,
                                                S.CoopNature ,
                                                S.SysNum ,
                                                S.SysNm ,
                                                S.SaleChannel ,
                                                S.IsProtocol ,
                                                S.RL ,
                                                S.XYLY ,
                                                S.ZGDFL ,
                                                S.FaxNumber ,
                                                S.NamCountry ,
                                                S.ZZKASystem1 ,
                                                S.ZZFMS_MUM ,
                                                S.ZZTable ,
                                                S.ZZSeat ,
                                                S.ZZWEIXIN_NUM ,
                                                S.ZZAge ,
                                                S.ZZInner_Area ,
                                                S.ZZOut_Area ,
                                                S.ZZBEER ,
                                                S.ZZCHAIN_NAME ,
                                                S.ZZCHAIN_TEL ,
                                                S.ZZCHAIN_TYPE ,
                                                S.ZZCHAIN_QUA ,
                                                S.ZZCHAIN_NUM ,
                                                S.ZZCUISINE ,
                                                S.ZZCHARACTERISTIC ,
                                                S.ZZPERCONSUME ,
                                                S.ZZOPEN_TIME ,
                                                S.ZZFREEZER ,
                                                S.ZZFLD0000CG ,
                                                S.ZZVIRTUAL ,
                                                S.ZZVISIT ,
                                                S.ZZCHARACTER ,
                                                S.ZZSTORAGE ,
                                                S.ZZFLD000052 ,
                                                S.ZZSMALLBOX_NUM ,
                                                S.ZZPRO_NUM2 ,
                                                S.ZZPRO_NAME2 ,
                                                S.ZZALCO ,
                                                S.ZZBEST_TIME ,
                                                S.ZZWHET_CHAIN ,
                                                S.ZZBIGBOX_NUM ,
                                                S.ZZMIDBOX_NUM ,
                                                S.ZZPORN_PRICE ,
                                                S.ZZPRO_RANK ,
                                                S.ZZDAY_REVENUE ,
                                                S.ZZCASHIER_NUM ,
                                                S.ZZDISTRI_WAY ,
                                                S.ZZFLD00005D ,
                                                S.ZZRECONCILIATION ,
                                                S.ZZACCOUNT_WAY ,
                                                S.ZZACCCOUNT_TIME ,
                                                S.ZZFIPERSON ,
                                                S.ZZFIPERSON_TEL ,
                                                S.E_MAILSMT ,
                                                S.URIURI ,
                                                S.BZ ,
                                                S.ZZDELIVER_NOTE ,
                                                S.ZZCARLIMIT_DESC ,
                                                S.ZZACCOUNT_PERIOD ,
                                                S.ZZKABEER_NUM ,
                                                S.ZZKABEER_PILE ,
                                                S.ZZKANONBEER_PILE ,
                                                S.ZZKAICE_NUM ,
                                                S.ZZKACOLD_NUM ,
                                                S.ZZKASHELF_NUM ,
                                                S.ZZKALEVEL_NUM ,
                                                S.ZZKAWHOLEBOX_NUM ,
                                                S.ZZKAPACKAGE_NUM ,
                                                S.ZZKAPILE_USE ,
                                                S.ZZKANONPILE_USE ,
                                                S.ZZKAPRO_USE ,
                                                S.ZZKASHELF_USE ,
                                                S.ZZKAICE_USE ,
                                                S.ZZKACOLD_USE ,
                                                S.ZZKACASHER_USE ,
                                                S.ZZKAMULTI_USE ,
                                                S.ZZKADISPLAY_USE ,
                                                S.ZZKAPILEOUT_USE ,
                                                S.ZZFLD0000G2 ,
                                                S.ZZKAFLAG_USE ,
                                                S.ZZKAPOST_USE ,
                                                S.ZZKAAB_USE ,
                                                S.ZZFLD0000G6 ,
                                                S.ZZKALADDER_USE ,
                                                S.ZZKASERVICE_USE ,
                                                S.ZZKAPOP_USE ,
                                                S.ZZKALIVELY_USE ,
                                                S.ZZBOX ,
                                                S.ZZDECK_NAME ,
                                                S.ZbnType ,
                                                S.ZZGSYYZZH ,
                                                S.ZZGSZZMC ,
                                                0,
                                                '{Guid.Empty}' ,
                                                '' ,
                                                getdate()
                                            );";
            return terminalSaveSql;
        }
        #endregion

        #region 删除临时表
        /// <summary>
        /// 删除临时表
        /// </summary>
        /// <param name="tableName">临时表名</param>
        /// <returns></returns>
        private async Task DeleteTmpTable(string tableName)
        {
            if (tableName.IsNull() == false)
            {
                try
                {
                    await _mDistributorRepository.Execute($"DROP TABLE {tableName};");
                }
                catch
                {
                }
            }
        }
        #endregion

        #region 创建对象
        /// <summary>
        /// 创建终端对象
        /// </summary>
        /// <param name="terminalId">终端id</param>
        /// <param name="stationId">工作站id</param>
        /// <param name="terminal">CRM终端对象</param>
        /// <returns>终端对象</returns>
        private MTerminalSyncDto CreateTerminal(Guid terminalId, Guid stationId, ET_TERMINAL terminal)
        {
            MTerminalSyncDto dto = new MTerminalSyncDto
            {
                Id = terminalId,
                TerminalCode = terminal.PARTNER,
                TerminalName = terminal.MC_NAME1,
                StationId = stationId,
                SaleLine = terminal.ZZSTORE_TYPE1,
                Lvl1Type = terminal.ZZSTORE_TYPE2,
                Lvl2Type = terminal.ZZSTORE_TYPE3,
                Lvl3Type = terminal.ZZSTORE_TYP4,
                Address = terminal.ZZADD_DETAIL,
                Status = terminal.ZZSTATUS1 == "01" ? 2 : terminal.ZZSTATUS1 == "02" ? 1 : 0, //0关闭，1生效，2停业
                JsonString = Newtonsoft.Json.JsonConvert.SerializeObject(terminal),
                ZDATE = GetDateTime(terminal.ZDATE),
                Tel = terminal.TEL_NUMBER,
                Prov = terminal.REGION,
                City = terminal.ZZCITY,
                Country = terminal.ZZCOUNTY,
                Street = terminal.ZZSTREET_NUM,
                Village = terminal.ZZLILLAGE_NUM,
                AddDetail = terminal.ZZADD_DETAIL,
                TmnOwner = terminal.ZZPERSON,
                TmnPhone = terminal.ZZTELPHONE,
                Per1Nm = terminal.ZZPER1_NAME,
                Per1Post = terminal.ZZPER1_PO,
                Per1Bir = terminal.ZZPER1_BIR,
                Per1Tel = terminal.ZZPER1_TEL,
                Per2Nm = terminal.ZZPER2_NAME,
                Per2Post = terminal.ZZPER2_PO,
                Per2Bir = terminal.ZZPER2_BIR,
                Per2Tel = terminal.ZZPER2_TEL,
                Per3Nm = terminal.ZZPER3_NAME,
                Per3Post = terminal.ZZPER3_PO,
                Per3Bir = terminal.ZZPER3_BIR,
                Per3Tel = terminal.ZZPER3_TEL,
                Geo = terminal.ZZGEO,
                //CoopNature=,
                //SysNum = ,
                //SysNm = tmp.ZZKASYSTEM,
                SaleChannel = terminal.ZZSALES_CHANNEL,
                IsProtocol = terminal.ZZPROTOCOL == "01" ? 1 : 0, //TODO：值
                RL = terminal.ZZRL.ToDecimal(),
                //XYLY = ,
                ZGDFL = terminal.ZZGDFL,
                FaxNumber = terminal.FAX_NUMBER,
                //NamCountry = ,
                ZZKASystem1 = terminal.ZZKASYSTEM,
                //ZZFMS_MUM = ,
                ZZTable = terminal.ZZTABLE,
                ZZSeat = terminal.ZZSEAT,
                ZZWEIXIN_NUM = terminal.ZZWEIXIN_NUM,
                ZZAge = terminal.ZZAGE,
                ZZInner_Area = terminal.ZZINNER_AREA,
                ZZOut_Area = terminal.ZZOUT_AREA,
                ZZBEER = terminal.ZZBEER,
                ZZCHAIN_NAME = terminal.ZZCHAIN_NAME,
                ZZCHAIN_TEL = terminal.ZZCHAIN_TEL,
                ZZCHAIN_TYPE = terminal.ZZCHAIN_TYPE,
                ZZCHAIN_QUA = terminal.ZZCHAIN_QUA,
                ZZCHAIN_NUM = terminal.ZZCHAIN_NUM,
                ZZCUISINE = terminal.ZZCUISINE,
                ZZCHARACTERISTIC = terminal.ZZCHARACTERISTIC,
                ZZPERCONSUME = terminal.ZZPERCONSUME,
                ZZOPEN_TIME = terminal.ZZOPEN_TIME,
                ZZFREEZER = terminal.ZZFREEZER,
                ZZFLD0000CG = terminal.ZZFLD0000CG,
                ZZVIRTUAL = terminal.ZZVIRTUAL,
                ZZVISIT = terminal.ZZVISIT.ToDecimal(),
                ZZCHARACTER = terminal.ZZCHARACTER,
                ZZSTORAGE = terminal.ZZSTORAGE,
                ZZFLD000052 = terminal.ZZFLD000052,
                ZZSMALLBOX_NUM = terminal.ZZSMALLBOX_NUM,
                ZZPRO_NUM2 = terminal.ZZPRO_NUM2,
                ZZPRO_NAME2 = terminal.ZZPRO_NAME2,
                ZZALCO = terminal.ZZALCO,
                ZZBEST_TIME = terminal.ZZBEST_TIME,
                ZZWHET_CHAIN = terminal.ZZWHET_CHAIN,
                ZZBIGBOX_NUM = terminal.ZZBIGBOX_NUM,
                ZZMIDBOX_NUM = terminal.ZZMIDBOX_NUM,
                ZZPORN_PRICE = terminal.ZZPORN_PRICE,
                ZZPRO_RANK = terminal.ZZPRO_RANK,
                ZZDAY_REVENUE = terminal.ZZDAY_REVENUE,
                ZZCASHIER_NUM = terminal.ZZCASHIER_NUM,
                ZZDISTRI_WAY = terminal.ZZDISTRI_WAY,
                ZZFLD00005D = terminal.ZZFLD00005D.ToDecimal(),
                ZZRECONCILIATION = terminal.ZZRECONCILIATION,
                ZZACCOUNT_WAY = terminal.ZZACCOUNT_WAY,
                ZZACCCOUNT_TIME = terminal.ZZACCCOUNT_TIME,
                ZZFIPERSON = terminal.ZZFIPERSON,
                ZZFIPERSON_TEL = terminal.ZZFIPERSON_TEL,
                //E_MAILSMT = tmp.E_MAILSMT,
                URIURI = terminal.URI_ADDR,
                //BZ = tmp.BZ,
                ZZDELIVER_NOTE = terminal.ZZDELIVER_NOTE,
                ZZCARLIMIT_DESC = terminal.ZZCARLIMIT_DESC,
                ZZACCOUNT_PERIOD = terminal.ZZACCOUNT_PERIOD,
                //ZZKABEER_NUM = tmp.ZZKABEER_NUM,
                //ZZKABEER_PILE = tmp.ZZKABEER_PILE,
                //ZZKANONBEER_PILE = tmp.ZZKANONBEER_PILE,
                //ZZKAICE_NUM = tmp.ZZKAICE_NUM,
                //ZZKACOLD_NUM = tmp.ZZKACOLD_NUM,
                //ZZKASHELF_NUM = tmp.ZZKASHELF_NUM,
                //ZZKALEVEL_NUM = tmp.ZZKALEVEL_NUM,
                //ZZKAWHOLEBOX_NUM = tmp.ZZKAWHOLEBOX_NUM,
                //ZZKAPACKAGE_NUM = tmp.ZZKAPACKAGE_NUM,
                //ZZKAPILE_USE = tmp.ZZKAPILE_USE,
                //ZZKANONPILE_USE = tmp.ZZKANONPILE_USE,
                //ZZKAPRO_USE = tmp.ZZKAPRO_USE,
                //ZZKASHELF_USE = tmp.ZZKASHELF_USE,
                //ZZKAICE_USE = tmp.ZZKAICE_USE,
                //ZZKACOLD_USE = tmp.ZZKACOLD_USE,
                //ZZKACASHER_USE = tmp.ZZKACASHER_USE,
                //ZZKAMULTI_USE = tmp.ZZKAMULTI_USE,
                //ZZKADISPLAY_USE = tmp.ZZKADISPLAY_USE,
                //ZZKAPILEOUT_USE = tmp.ZZKAPILEOUT_USE,
                //ZZFLD0000G2 = tmp.ZZFLD0000G2,
                //ZZKAFLAG_USE = tmp.ZZKAFLAG_USE,
                //ZZKAPOST_USE = tmp.ZZKAPOST_USE,
                //ZZKAAB_USE = tmp.ZZKAAB_USE,
                //ZZFLD0000G6 = tmp.ZZFLD0000G6,
                //ZZKALADDER_USE = tmp.ZZKALADDER_USE,
                //ZZKASERVICE_USE = tmp.ZZKASERVICE_USE,
                //ZZKAPOP_USE = tmp.ZZKAPOP_USE,
                //ZZKALIVELY_USE = tmp.ZZKALIVELY_USE,
                ZZBOX = terminal.ZZBOX.ToInt(),
                ZZDECK_NAME = terminal.ZZDECK_NAME,
                //ZbnType = tmp.ZbnType,
                //ZZGSYYZZH = tmp.ZZGSYYZZH,
                //ZZGSZZMC = tmp.ZZGSZZMC
            };
            if (dto.TmnOwner.IsNull() == false && dto.TmnOwner.Length > 12)
            {
                dto.TmnOwner = dto.TmnOwner.Substring(0, 12);
            }
            return dto;
        }

        /// <summary>
        /// 经销商返写对象
        /// </summary>
        /// <param name="distributorCode">经销商编码</param>
        /// <param name="zdate">日期</param>
        /// <returns>返写对象</returns>
        private IT_RETURN CreateCRMReturnObjectForDistributor(string distributorCode, string zdate)
        {
            return (new IT_RETURN
            {
                PARTNER1 = distributorCode,
                PARTNER2 = "0000000000",
                ZUSER = "",
                ZTYPE = "06",
                ZTYPE_1 = "Z003",
                ZDATE = zdate,
                ZBAIOS = "0000000000"
            });
        }

        /// <summary>
        /// 终端返写对象
        /// </summary>
        /// <param name="terminalCode">经销商编码</param>
        /// <param name="zdate">日期</param>
        /// <returns>返写对象</returns>
        private IT_RETURN CreateCRMReturnObjectForTerminal(string terminalCode, string zdate)
        {
            return (new IT_RETURN
            {
                PARTNER1 = terminalCode,
                PARTNER2 = "0000000000",
                ZUSER = "",
                ZTYPE = "01",
                ZTYPE_1 = "Z002",
                ZDATE = zdate,
                ZBAIOS = "0000000000"
            });
        }

        /// <summary>
        /// 关系返写对象
        /// </summary>
        /// <param name="rel">关系</param>
        /// <returns>返写对象</returns>
        private IT_RETURN CreateCRMReturnObjectForRelation(ET_RELATION rel)
        {
            return (new IT_RETURN
            {
                PARTNER1 = rel.PARTNER1,
                PARTNER2 = rel.PARTNER2,
                ZUSER = "",
                ZTYPE = "03",
                ZTYPE_1 = rel.RELTYP,
                ZDATE = rel.ZDATE,
                ZBAIOS = "0000000000"
            });
        }

        /// <summary>
        /// 创建错误日志
        /// </summary>
        /// <param name="marketOrgCD">营销中心编码</param>
        /// <param name="type">类型</param>
        /// <param name="code1">编码1</param>
        /// <param name="code2">编码2</param>
        /// <param name="message">错误信息</param>
        /// <returns></returns>
        private SyncDtAndTmnErrorLogDto CreateErrorLog(string marketOrgCD, WarningInfoType type, string code1, string code2, string message)
        {
            SyncDtAndTmnErrorLogDto dto = new SyncDtAndTmnErrorLogDto
            {
                MarketOrgCD = marketOrgCD,
                Code1 = code1,
                Code2 = code2,
                Type = type,
                Message = message
            };
            return dto;
        }
        #endregion

        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <param name="type"> </param>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetErrorMessage(string type, string code, Guid? id)
        {
            if (id.HasValue == false || id.Value.IsEmpty())
            {
                return $"{type}：{code} 不存在 ";
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取CRM日期值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private DateTime GetDateTime(string str)
        {
            if (string.IsNullOrEmpty(str))
                return new DateTime(2000, 1, 1);
            else
                return new DateTime(Convert.ToInt32(str.Substring(0, 4)), Convert.ToInt32(str.Substring(4, 2)), Convert.ToInt32(str.Substring(6, 2)), Convert.ToInt32(str.Substring(8, 2)), Convert.ToInt32(str.Substring(10, 2)), Convert.ToInt32(str.Substring(12, 2)));
        }
        #endregion
    }
}
