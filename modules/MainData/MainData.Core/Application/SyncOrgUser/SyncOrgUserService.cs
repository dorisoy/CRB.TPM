using CRB.TPM.Config.Abstractions;
using CRB.TPM.Data.Core.Extensions;
using CRB.TPM.Data.Sharding;
using CRB.TPM.Mod.Admin.Core.Application.MObject;
using CRB.TPM.Mod.Admin.Core.Application.MObject.Dto;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;
using CRB.TPM.Mod.Admin.Core.Application.SyncAccount;
using CRB.TPM.Mod.Admin.Core.Domain.MOrg;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using CRB.TPM.Mod.MainData.Core.Application.SyncOrgUser.Dto;
using CRB.TPM.Mod.MainData.Core.Infrastructure;
using CRB.TPM.Utils.Helpers;
using CRB.TPM.Utils.Map;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace CRB.TPM.Mod.MainData.Core.Application.SyncOrgUser
{
    public class SyncOrgUserService : ISyncOrgUserService
    {
        private readonly IMapper _mapper;
        private readonly MainDataDbContext _dbContext;
        private readonly IMOrgRepository _mOrgRepository;
        private readonly IMObjectService _mObjectService;
        private readonly ISyncAccountService _syncAccountService;
        private readonly IConfigProvider _configProvider;
        public SyncOrgUserService(IMapper mapper,
                                  MainDataDbContext dbContext,
                                  IMOrgRepository mOrgRepository,
                                  IMObjectService mObjectService,
                                  ISyncAccountService syncAccountService,
                                  IConfigProvider configProvider)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _mOrgRepository = mOrgRepository;
            _mObjectService = mObjectService;
            _syncAccountService = syncAccountService;
            _configProvider = configProvider;
        }

        public async Task<IResultModel> SyncData(string date)
        {
            var config = _configProvider.Get<MainDataConfig>();

            string inType = "IF0005";
            int mode = 1;//0全量 1增量

            if (string.IsNullOrWhiteSpace(date))
            {
                mode = 0;
            }
            else
            {
                mode = 1;
                date = string.Format("{0}000000", date);
            }

            string busParaModule = "\"INTYP\":\"{0}\",\"OSPLATFM\":\"NET\",\"MODE\":\"{1}\",\"DATE\":\"{2}\"";
            string busPara = "{" + string.Format(busParaModule, inType, mode, date) + "}";

            IResultModel<string> result = SapWebServiceHelper.GetWebServiceResult(busPara, config.SapWsUrl);

            if (!result.Successful)
                return result;

            string tmpOrgTableName = "";
            string tmpMObjectTableName = "";

            try
            {
                var r = JsonConvert.DeserializeObject<Result_OrgUser>(result.Data);

                #region 更新组织主数据
                List<MOrgAddExtendDto> mOrgAdds = new List<MOrgAddExtendDto>();
                MOrgAddExtendDto mOrgAdd = null;

                foreach (var item in r.ET_ORGREL)
                {
                    mOrgAdd = new MOrgAddExtendDto();
                    mOrgAdd.Id = Guid.NewGuid();
                    mOrgAdd.OrgCode = item.ORG_ID;
                    mOrgAdd.OrgName = item.ORG_TEXT;
                    mOrgAdd.ParentOrgCode = item.ORG_UP;
                    mOrgAdd.ParentOrgName = item.ORG_UP_TEXT;
                    mOrgAdd.Type = item.ORG_LVL;
                    mOrgAdd.Deleted = false;
                    mOrgAdd.CreatedBy = Guid.Empty;
                    mOrgAdd.Creator = "系统同步";
                    mOrgAdd.CreatedTime = DateTime.Now;
                    mOrgAdd.Enabled = true;

                    var orgAttr = r.ET_ORGATR.FirstOrDefault(x => x.ZORG == item.ORG_ID);
                    if (orgAttr == null)
                    {
                        mOrgAdd.Attribute = (int)OrgEnumAttr.ZSORG;
                    }
                    else
                    {
                        mOrgAdd.Attribute = orgAttr.ORG_ATTR;
                    }

                    mOrgAdds.Add(mOrgAdd);
                }

                int levelOneType = (int)OrgEnumType.HeadOffice;
                int levelTwoType = (int)OrgEnumType.BD;
                int levelThreeType = (int)OrgEnumType.MarketingCenter;
                int levelFourType = (int)OrgEnumType.SaleRegion;
                int levelFiveType = (int)OrgEnumType.Department;
                int levelSixType = (int)OrgEnumType.Station;

                //雪花总部
                var levelOneOrg = mOrgAdds.FirstOrDefault(x => x.Type == levelOneType);
                //事业部\区域
                var levelTwoOrgs = mOrgAdds.Where(x => x.Type == levelTwoType).ToList();
                //营销中心
                var levelThreeOrgs = mOrgAdds.Where(x => x.Type == levelThreeType).ToList();
                //大区
                var levelFourOrgs = mOrgAdds.Where(x => x.Type == levelFourType).ToList();
                //业务部
                var levelFiveOrgs = mOrgAdds.Where(x => x.Type == levelFiveType).ToList();
                //工作站
                var levelSixOrgs = mOrgAdds.Where(x => x.Type == levelSixType).ToList();
                //虚拟销分
                var levelVirtualOrgs = mOrgAdds.Where(x => x.Type == 0).ToList();

                List<MObjectSyncDto> mObjectSyncs = new List<MObjectSyncDto>();
                MObjectSyncDto mObjectSync = null;

                if (levelOneOrg != null)
                {
                    mObjectSync = new MObjectSyncDto();
                    mObjectSync.Id = levelOneOrg.Id;
                    mObjectSync.Type = levelOneOrg.Type;
                    mObjectSync.ObjectCode = levelOneOrg.OrgCode;
                    mObjectSync.ObjectName = levelOneOrg.OrgName;
                    mObjectSync.HeadOfficeId = levelOneOrg.Id;
                    mObjectSync.HeadOfficeCode = levelOneOrg.OrgCode;
                    mObjectSync.HeadOfficeName = levelOneOrg.OrgName;
                    mObjectSync.DivisionId = Guid.Empty;
                    mObjectSync.DivisionCode = "";
                    mObjectSync.DivisionName = "";
                    mObjectSync.MarketingId = Guid.Empty;
                    mObjectSync.MarketingCode = "";
                    mObjectSync.MarketingName = "";
                    mObjectSync.BigAreaId = Guid.Empty;
                    mObjectSync.BigAreaCode = "";
                    mObjectSync.BigAreaName = "";
                    mObjectSync.OfficeId = Guid.Empty;
                    mObjectSync.OfficeCode = "";
                    mObjectSync.OfficeName = "";
                    mObjectSync.StationId = Guid.Empty;
                    mObjectSync.StationCode = "";
                    mObjectSync.StationName = "";
                    mObjectSync.DistributorId = Guid.Empty;
                    mObjectSync.DistributorCode = "";
                    mObjectSync.DistributorName = "";
                    mObjectSync.Enabled = 1;

                    mObjectSyncs.Add(mObjectSync);
                }

                //更新事业部\区域组织的父级组织Id
                foreach (var item in levelTwoOrgs)
                {
                    item.ParentId = levelOneOrg.Id;
                    item.ParentOrgCode = levelOneOrg.OrgCode;
                    item.ParentOrgName = levelOneOrg.OrgName;

                    mObjectSync = new MObjectSyncDto();
                    mObjectSync.Id = item.Id;
                    mObjectSync.Type = item.Type;
                    mObjectSync.ObjectCode = item.OrgCode;
                    mObjectSync.ObjectName = item.OrgName;
                    mObjectSync.HeadOfficeId = levelOneOrg.Id;
                    mObjectSync.HeadOfficeCode = levelOneOrg.OrgCode;
                    mObjectSync.HeadOfficeName = levelOneOrg.OrgName;
                    mObjectSync.DivisionId = item.Id;
                    mObjectSync.DivisionCode = item.OrgCode;
                    mObjectSync.DivisionName = item.OrgName;
                    mObjectSync.MarketingId = Guid.Empty;
                    mObjectSync.MarketingCode = "";
                    mObjectSync.MarketingName = "";
                    mObjectSync.BigAreaId = Guid.Empty;
                    mObjectSync.BigAreaCode = "";
                    mObjectSync.BigAreaName = "";
                    mObjectSync.OfficeId = Guid.Empty;
                    mObjectSync.OfficeCode = "";
                    mObjectSync.OfficeName = "";
                    mObjectSync.StationId = Guid.Empty;
                    mObjectSync.StationCode = "";
                    mObjectSync.StationName = "";
                    mObjectSync.DistributorId = Guid.Empty;
                    mObjectSync.DistributorCode = "";
                    mObjectSync.DistributorName = "";
                    mObjectSync.Enabled = 1;

                    mObjectSyncs.Add(mObjectSync);
                }
                //更新营销中心组织的父级组织Id
                foreach (var item in levelThreeOrgs)
                {
                    var parentOrg = levelTwoOrgs.FirstOrDefault(x => x.OrgCode == item.ParentOrgCode);
                    if (parentOrg != null)
                    {
                        item.ParentId = parentOrg.Id;
                        item.ParentOrgCode = parentOrg.OrgCode;
                        item.ParentOrgName = parentOrg.OrgName;

                        mObjectSync = new MObjectSyncDto();
                        mObjectSync.Id = item.Id;
                        mObjectSync.Type = item.Type;
                        mObjectSync.ObjectCode = item.OrgCode;
                        mObjectSync.ObjectName = item.OrgName;
                        mObjectSync.HeadOfficeId = levelOneOrg.Id;
                        mObjectSync.HeadOfficeCode = levelOneOrg.OrgCode;
                        mObjectSync.HeadOfficeName = levelOneOrg.OrgName;
                        mObjectSync.DivisionId = parentOrg.Id;
                        mObjectSync.DivisionCode = parentOrg.OrgCode;
                        mObjectSync.DivisionName = parentOrg.OrgName;
                        mObjectSync.MarketingId = item.Id;
                        mObjectSync.MarketingCode = item.OrgCode;
                        mObjectSync.MarketingName = item.OrgName;
                        mObjectSync.BigAreaId = Guid.Empty;
                        mObjectSync.BigAreaCode = "";
                        mObjectSync.BigAreaName = "";
                        mObjectSync.OfficeId = Guid.Empty;
                        mObjectSync.OfficeCode = "";
                        mObjectSync.OfficeName = "";
                        mObjectSync.StationId = Guid.Empty;
                        mObjectSync.StationCode = "";
                        mObjectSync.StationName = "";
                        mObjectSync.DistributorId = Guid.Empty;
                        mObjectSync.DistributorCode = "";
                        mObjectSync.DistributorName = "";
                        mObjectSync.Enabled = 1;

                        mObjectSyncs.Add(mObjectSync);
                    }
                }
                //更新虚拟销分组织的父级组织Id
                foreach (var item in levelVirtualOrgs)
                {
                    var parentOrg = levelThreeOrgs.FirstOrDefault(x => x.OrgCode == item.ParentOrgCode);
                    if (parentOrg != null)
                    {
                        item.ParentId = parentOrg.Id;
                        item.ParentOrgCode = parentOrg.OrgCode;
                        item.ParentOrgName = parentOrg.OrgName;

                    }
                }
                //更新大区组织的父级组织Id
                foreach (var item in levelFourOrgs)
                {
                    var parentOrg = levelVirtualOrgs.FirstOrDefault(x => x.OrgCode == item.ParentOrgCode);
                    if (parentOrg != null)
                    {
                        item.ParentId = parentOrg.ParentId;
                        item.ParentOrgCode = parentOrg.ParentOrgCode;
                        item.ParentOrgName = parentOrg.ParentOrgName;

                        var parentObjectSync = mObjectSyncs.FirstOrDefault(x => x.ObjectCode == parentOrg.ParentOrgCode);
                        if (parentObjectSync != null)
                        {

                            mObjectSync = new MObjectSyncDto();
                            mObjectSync.Id = item.Id;
                            mObjectSync.Type = item.Type;
                            mObjectSync.ObjectCode = item.OrgCode;
                            mObjectSync.ObjectName = item.OrgName;
                            mObjectSync.HeadOfficeId = parentObjectSync.HeadOfficeId;
                            mObjectSync.HeadOfficeCode = parentObjectSync.HeadOfficeCode;
                            mObjectSync.HeadOfficeName = parentObjectSync.HeadOfficeName;
                            mObjectSync.DivisionId = parentObjectSync.DivisionId;
                            mObjectSync.DivisionCode = parentObjectSync.DivisionCode;
                            mObjectSync.DivisionName = parentObjectSync.DivisionName;
                            mObjectSync.MarketingId = parentOrg.ParentId;
                            mObjectSync.MarketingCode = parentOrg.ParentOrgCode;
                            mObjectSync.MarketingName = parentOrg.ParentOrgName;
                            mObjectSync.BigAreaId = item.Id;
                            mObjectSync.BigAreaCode = item.OrgCode;
                            mObjectSync.BigAreaName = item.OrgName;
                            mObjectSync.OfficeId = Guid.Empty;
                            mObjectSync.OfficeCode = "";
                            mObjectSync.OfficeName = "";
                            mObjectSync.StationId = Guid.Empty;
                            mObjectSync.StationCode = "";
                            mObjectSync.StationName = "";
                            mObjectSync.DistributorId = Guid.Empty;
                            mObjectSync.DistributorCode = "";
                            mObjectSync.DistributorName = "";
                            mObjectSync.Enabled = 1;

                            mObjectSyncs.Add(mObjectSync);
                        }
                    }
                }
                //更新业务部组织的父级组织Id
                foreach (var item in levelFiveOrgs)
                {
                    var parentOrg = levelFourOrgs.FirstOrDefault(x => x.OrgCode == item.ParentOrgCode);
                    if (parentOrg != null)
                    {
                        item.ParentId = parentOrg.Id;
                        item.ParentOrgCode = parentOrg.OrgCode;
                        item.ParentOrgName = parentOrg.OrgName;


                        var parentObjectSync = mObjectSyncs.FirstOrDefault(x => x.ObjectCode == parentOrg.OrgCode);
                        if (parentObjectSync != null)
                        {
                            mObjectSync = new MObjectSyncDto();
                            mObjectSync.Id = item.Id;
                            mObjectSync.Type = item.Type;
                            mObjectSync.ObjectCode = item.OrgCode;
                            mObjectSync.ObjectName = item.OrgName;
                            mObjectSync.HeadOfficeId = parentObjectSync.HeadOfficeId;
                            mObjectSync.HeadOfficeCode = parentObjectSync.HeadOfficeCode;
                            mObjectSync.HeadOfficeName = parentObjectSync.HeadOfficeName;
                            mObjectSync.DivisionId = parentObjectSync.DivisionId;
                            mObjectSync.DivisionCode = parentObjectSync.DivisionCode;
                            mObjectSync.DivisionName = parentObjectSync.DivisionName;
                            mObjectSync.MarketingId = parentObjectSync.MarketingId;
                            mObjectSync.MarketingCode = parentObjectSync.MarketingCode;
                            mObjectSync.MarketingName = parentObjectSync.MarketingName;
                            mObjectSync.BigAreaId = parentOrg.Id;
                            mObjectSync.BigAreaCode = parentOrg.OrgCode;
                            mObjectSync.BigAreaName = parentOrg.OrgName;
                            mObjectSync.OfficeId = item.Id;
                            mObjectSync.OfficeCode = item.OrgCode;
                            mObjectSync.OfficeName = item.OrgName;
                            mObjectSync.StationId = Guid.Empty;
                            mObjectSync.StationCode = "";
                            mObjectSync.StationName = "";
                            mObjectSync.DistributorId = Guid.Empty;
                            mObjectSync.DistributorCode = "";
                            mObjectSync.DistributorName = "";
                            mObjectSync.Enabled = 1;

                            mObjectSyncs.Add(mObjectSync);
                        }
                    }
                }
                //更新工作站组织的父级组织Id
                foreach (var item in levelSixOrgs)
                {
                    var parentOrg = levelFiveOrgs.FirstOrDefault(x => x.OrgCode == item.ParentOrgCode);
                    if (parentOrg != null)
                    {
                        item.ParentId = parentOrg.Id;
                        item.ParentOrgCode = parentOrg.OrgCode;
                        item.ParentOrgName = parentOrg.OrgName;

                        var parentObjectSync = mObjectSyncs.FirstOrDefault(x => x.ObjectCode == parentOrg.OrgCode);

                        if (parentObjectSync != null)
                        {
                            mObjectSync = new MObjectSyncDto();
                            mObjectSync.Id = item.Id;
                            mObjectSync.Type = item.Type;
                            mObjectSync.ObjectCode = item.OrgCode;
                            mObjectSync.ObjectName = item.OrgName;
                            mObjectSync.HeadOfficeId = parentObjectSync.HeadOfficeId;
                            mObjectSync.HeadOfficeCode = parentObjectSync.HeadOfficeCode;
                            mObjectSync.HeadOfficeName = parentObjectSync.HeadOfficeName;
                            mObjectSync.DivisionId = parentObjectSync.DivisionId;
                            mObjectSync.DivisionCode = parentObjectSync.DivisionCode;
                            mObjectSync.DivisionName = parentObjectSync.DivisionName;
                            mObjectSync.MarketingId = parentObjectSync.MarketingId;
                            mObjectSync.MarketingCode = parentObjectSync.MarketingCode;
                            mObjectSync.MarketingName = parentObjectSync.MarketingName;
                            mObjectSync.BigAreaId = parentObjectSync.BigAreaId;
                            mObjectSync.BigAreaCode = parentObjectSync.BigAreaCode;
                            mObjectSync.BigAreaName = parentObjectSync.BigAreaName;
                            mObjectSync.OfficeId = parentOrg.Id;
                            mObjectSync.OfficeCode = parentOrg.OrgCode;
                            mObjectSync.OfficeName = parentOrg.OrgName;
                            mObjectSync.StationId = item.Id;
                            mObjectSync.StationCode = item.OrgCode;
                            mObjectSync.StationName = item.OrgName;
                            mObjectSync.DistributorId = Guid.Empty;
                            mObjectSync.DistributorCode = "";
                            mObjectSync.DistributorName = "";
                            mObjectSync.Enabled = 1;

                            mObjectSyncs.Add(mObjectSync);
                        }
                    }
                }

                if (mOrgAdds.Count > 0)
                {
                    tmpOrgTableName = $"Tmp_M_Org_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                    string tmpTableSql = CreateOrgTmpTable(tmpOrgTableName);
                    await _mOrgRepository.Execute(tmpTableSql);
                    DataTable tmpTable = mOrgAdds.ToDataTable();
                    tmpTable.TableName = tmpOrgTableName;

                    _dbContext.SqlBulkCopy(tmpTable);
                }

                if (mObjectSyncs.Count > 0)
                {
                    tmpMObjectTableName = await _mObjectService.CreateSyncTmpTable(mObjectSyncs);
                }

                using (var uow = _dbContext.NewUnitOfWork())
                {
                    #region 组织关系
                    if (tmpOrgTableName.IsNull() == false)
                    {
                        string updateTmpTableSql = $@"update t set t.Id = o.Id 
                                                           from {tmpOrgTableName} t
                                                           inner join M_Org o on t.OrgCode = o.OrgCode;
                                                      update t set t.ParentId = o.Id 
                                                           from {tmpOrgTableName} t
                                                           inner join M_Org o on t.ParentOrgCode = o.OrgCode;
                                                      delete from {tmpOrgTableName} where ParentId = '00000000-0000-0000-0000-000000000000' and Type != 10;
                                                      delete from {tmpOrgTableName} where Type = 0;";

                        string updateSql = $@"MERGE INTO M_Org AS T
                                            USING {tmpOrgTableName} AS S ON S.OrgCode = T.OrgCode 
                                            WHEN MATCHED THEN
                                                UPDATE SET T.OrgName = S.OrgName,T.Type = S.Type,T.ParentId = S.ParentId,T.ModifiedBy = S.CreatedBy,T.Modifier = S.Creator,T.ModifiedTime = S.CreatedTime,T.Enabled = S.Enabled,T.Attribute = S.Attribute
                                            WHEN NOT MATCHED THEN
                                                INSERT (Id,OrgCode,OrgName,Type,ParentId,Deleted,CreatedBy,Creator,CreatedTime,Enabled,Attribute)
                                                VALUES (S.Id,S.OrgCode,S.OrgName,S.Type,S.ParentId,S.Deleted,S.CreatedBy,S.Creator,S.CreatedTime,S.Enabled,S.Attribute);";

                        string updateStatusSql = $@"UPDATE T set T.Deleted = 1,T.DeletedBy = '00000000-0000-0000-0000-000000000000',T.Deleter = '系统同步',T.DeletedTime = getdate() 
                                                         from M_Org T
                                                         WHERE NOT EXISTS (SELECT 1 FROM {tmpOrgTableName} S WHERE S.OrgCode = T.OrgCode)";

                        await _mOrgRepository.Execute(updateTmpTableSql, uow: uow);
                        await _mOrgRepository.Execute(updateSql, uow: uow);
                        await _mOrgRepository.Execute(updateStatusSql, uow: uow);
                    }
                    #endregion

                    #region MObject对象表
                    if (mObjectSyncs.Count > 0)
                    {
                        await _mObjectService.BatchProcess(tmpMObjectTableName, uow: uow);
                    }
                    #endregion

                    uow.SaveChanges();
                }
                #endregion

                var accountSyncResult = await _syncAccountService.SyncData(r.ET_ORG, r.ET_BPREL);

                return ResultModel.Success("同步成功");
            }
            catch (Exception ex)
            {
                return ResultModel.Failed("同步失败：" + ex.ToString());
            }
            finally
            {
                #region 删除临时表
                if (tmpOrgTableName.IsNull() == false)
                {
                    await this.DeleteTmpTable(tmpOrgTableName);
                }
                if (tmpMObjectTableName.IsNull() == false)
                {
                    await this.DeleteTmpTable(tmpMObjectTableName);
                }
                #endregion
            }


        }

        private string CreateOrgTmpTable(string tmpTableName)
        {
            string tmpTableSql = $@"CREATE TABLE [{tmpTableName}] (
                                                 [Id] uniqueidentifier NOT NULL,
                                                 [OrgCode] nvarchar(10) NOT NULL,
                                                 [OrgName] nvarchar(60) NOT NULL,
                                                 [Type] int NOT NULL,
                                                 [ParentId] uniqueidentifier NOT NULL,
                                                 [ParentOrgCode] nvarchar(10) NOT NULL,
                                                 [Deleted] bit NOT NULL,
                                                 [CreatedBy] uniqueidentifier NOT NULL,
                                                 [Creator] nvarchar(50) NOT NULL,
                                                 [CreatedTime] datetime NOT NULL,
                                                 [Enabled] bit NOT NULL,
                                                 [Attribute] int NOT NULL
                                                 )";
            return tmpTableSql;
        }

        /// <summary>
        /// 删除临时表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private async Task DeleteTmpTable(string tableName)
        {
            if (tableName.IsNull() == false)
            {
                try
                {
                    await _mOrgRepository.Execute($"DROP TABLE {tableName};");
                }
                catch
                {
                }
            }
        }
    }
}
