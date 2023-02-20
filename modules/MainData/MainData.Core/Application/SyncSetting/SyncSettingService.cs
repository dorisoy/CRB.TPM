using CRB.TPM.Config.Abstractions;
using CRB.TPM.Data.Core.Extensions;
using CRB.TPM.Data.Sharding;
using CRB.TPM.Mod.MainData.Core.Application.MdCityDistrict.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MdCountryProvince.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MdDistrictStreet.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MdHeightConf.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MdKaBigSysNameConf.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MdProvinceCity.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MdReTmnBTyteConfig.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MdStreetVillage.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MdTmnType.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MEntity.Dto;
using CRB.TPM.Mod.MainData.Core.Application.SyncSetting.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MdCityDistrict;
using CRB.TPM.Mod.MainData.Core.Domain.MdCountryProvince;
using CRB.TPM.Mod.MainData.Core.Domain.MdDistrictStreet;
using CRB.TPM.Mod.MainData.Core.Domain.MdHeightConf;
using CRB.TPM.Mod.MainData.Core.Domain.MdKaBigSysNameConf;
using CRB.TPM.Mod.MainData.Core.Domain.MdProvinceCity;
using CRB.TPM.Mod.MainData.Core.Domain.MdReTmnBTyteConfig;
using CRB.TPM.Mod.MainData.Core.Domain.MdStreetVillage;
using CRB.TPM.Mod.MainData.Core.Domain.MdTmnType;
using CRB.TPM.Mod.MainData.Core.Domain.MEntity;
using CRB.TPM.Mod.MainData.Core.Infrastructure;
using CRB.TPM.Utils.Helpers;
using CRB.TPM.Utils.Map;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.SyncSetting
{
    public class SyncSettingService : ISyncSettingService
    {
        private readonly IMapper _mapper;
        private readonly IMEntityRepository _mEntityRepository;
        private readonly IMdTmnTypeRepository _mdTmnTypeRepository;
        private readonly IMdReTmnBTyteConfigRepository _mdReTmnBTyteConfigRepository;
        private readonly IMdHeightConfRepository _mdHeightConfRepository;
        private readonly IMdKaBigSysNameConfRepository _mdKaBigSysNameConfRepository;
        private readonly IMdCountryProvinceRepository _mdCountryProvinceRepository;
        private readonly IMdProvinceCityRepository _mdProvinceCityRepository;
        private readonly IMdCityDistrictRepository _mdCityDistrictRepository;
        private readonly IMdDistrictStreetRepository _mdDistrictStreetRepository;
        private readonly IMdStreetVillageRepository _mdStreetVillageRepository;
        private readonly MainDataDbContext _dbContext;
        private readonly IConfigProvider _configProvider;
        public SyncSettingService(IMapper mapper,
                                    MainDataDbContext dbContext,
                                    IMEntityRepository mEntityRepository,
                                    IMdTmnTypeRepository mdTmnTypeRepository,
                                    IMdReTmnBTyteConfigRepository mdReTmnBTyteConfigRepository,
                                    IMdHeightConfRepository mdHeightConfRepository,
                                    IMdKaBigSysNameConfRepository mdKaBigSysNameConfRepository,
                                    IMdCountryProvinceRepository mdCountryProvinceRepository,
                                    IMdProvinceCityRepository mdProvinceCityRepository,
                                    IMdCityDistrictRepository mdCityDistrictRepository,
                                    IMdDistrictStreetRepository mdDistrictStreetRepository,
                                    IMdStreetVillageRepository mdStreetVillageRepository,
                                    IConfigProvider configProvider
                                    )
        {
            _mapper = mapper;
            _mEntityRepository = mEntityRepository;
            _dbContext = dbContext;
            _mdTmnTypeRepository = mdTmnTypeRepository;
            _mdReTmnBTyteConfigRepository = mdReTmnBTyteConfigRepository;
            _mdHeightConfRepository = mdHeightConfRepository;
            _mdKaBigSysNameConfRepository = mdKaBigSysNameConfRepository;
            _mdCountryProvinceRepository = mdCountryProvinceRepository;
            _mdProvinceCityRepository = mdProvinceCityRepository;
            _mdCityDistrictRepository = mdCityDistrictRepository;
            _mdDistrictStreetRepository = mdDistrictStreetRepository;
            _mdStreetVillageRepository = mdStreetVillageRepository;
            _configProvider = configProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<IResultModel> SyncData(string date)
        {
            var config = _configProvider.Get<MainDataConfig>();

            string inType = "IF0006";
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

            var r = JsonConvert.DeserializeObject<Result_Setting>(result.Data);

            #region 业务实体
            List<MEntityAddDto> mEentities = new List<MEntityAddDto>();
            MEntityAddDto mEntity = null;
            foreach (var item in r.ET_ZSNTM0024)
            {
                if (mEentities.Any(x => x.EntityCode == item.ZSND_NUM.Trim()))
                    continue;

                mEntity = new MEntityAddDto();
                mEntity.EntityCode = item.ZSND_NUM.Trim();
                mEntity.EntityName = item.ZSND_BUSINESS.Trim();
                mEntity.Enabled = 1.ToBool();
                mEntity.ERPCode = "";

                mEentities.Add(mEntity);
            }

            var updateEntityResult = await UpdateEntity(mEentities);
            #endregion

            #region 业务线/一级终端类型/二级终端类型/三级终端类型
            List<MdTmnTypeAddDto> mdTmnTypeAdds = new List<MdTmnTypeAddDto>();
            MdTmnTypeAddDto mdTmnTypeAdd = null;

            foreach (var item in r.ET_ZSNTM0040)
            {
                if (string.IsNullOrWhiteSpace(item.SALES_ORG) || item.ZTYPE0_NAME.Contains("废") || item.ZTYPE1_NAME.Contains("废") || item.ZTYPE2_NAME.Contains("废") || item.ZTYPE3_NAME.Contains("废"))
                    continue;

                mdTmnTypeAdd = new MdTmnTypeAddDto();
                mdTmnTypeAdd.RegionCD = item.SALES_ORG;
                mdTmnTypeAdd.MarketOrgCD = item.ZAREA_NUM;
                mdTmnTypeAdd.LineCD = item.ZTYPE0_NUM;
                mdTmnTypeAdd.LineNm = item.ZTYPE0_NAME;
                mdTmnTypeAdd.Level1TypeCD = item.ZTYPE1_NUM;
                mdTmnTypeAdd.Level1TypeNm = item.ZTYPE1_NAME;
                mdTmnTypeAdd.Level2TypeCD = item.ZTYPE2_NUM;
                mdTmnTypeAdd.Level2TypeNm = item.ZTYPE2_NAME;
                mdTmnTypeAdd.Level3TypeCD = item.ZTYPE3_NUM;
                mdTmnTypeAdd.Level3TypeNm = item.ZTYPE3_NAME;
                mdTmnTypeAdd.Status = 1;

                mdTmnTypeAdds.Add(mdTmnTypeAdd);
            }

            var updateTmnTypeResult = await UpdateTmnType(mdTmnTypeAdds);
            #endregion

            #region 终端业态配置表
            List<MdReTmnBTyteConfigAddDto> mdReTmnBTyteConfigAdds = new List<MdReTmnBTyteConfigAddDto>();
            MdReTmnBTyteConfigAddDto mdReTmnBTyteConfigAdd = null;

            foreach (var item in r.ET_BYTE)
            {
                mdReTmnBTyteConfigAdd = new MdReTmnBTyteConfigAddDto();
                mdReTmnBTyteConfigAdd.TmnStoreType1 = item.ZZSTORE_TYPE1;
                mdReTmnBTyteConfigAdd.ZbnType = item.ZBN_TYPE;
                mdReTmnBTyteConfigAdd.ZbnTypeTxt = item.ZBN_TYPE_TXT;
                mdReTmnBTyteConfigAdd.Status = 1;

                mdReTmnBTyteConfigAdds.Add(mdReTmnBTyteConfigAdd);
            }

            var updateMdReTmnBTyteConfigResult = await UpdateMdReTmnBTyteConfig(mdReTmnBTyteConfigAdds);
            #endregion

            #region CRM字典数据

            //foreach (var item in r.ET_DATA)
            //{

            //}
            #endregion

            #region 制高点配置
            List<MdHeightConfAddDto> mdHeightConfAdds = new List<MdHeightConfAddDto>();
            MdHeightConfAddDto mdHeightConfAdd = null;

            foreach (var item in r.ET_HEIGHT_CONF)
            {
                mdHeightConfAdd = new MdHeightConfAddDto();
                mdHeightConfAdd.SaleOrg = item.SALEORG;
                mdHeightConfAdd.Height = item.HEIGHT;
                mdHeightConfAdd.Text = item.TEXT;

                mdHeightConfAdds.Add(mdHeightConfAdd);
            }

            var updateMdHeightConfResult = await UpdateMdHeightConf(mdHeightConfAdds);
            #endregion

            #region KA大系统
            List<MdKaBigSysNameConfAddDto> mdKaBigSysNameConfAdds = new List<MdKaBigSysNameConfAddDto>();
            MdKaBigSysNameConfAddDto mdKaBigSysNameConfAdd = null;

            foreach (var item in r.ET_093)
            {
                mdKaBigSysNameConfAdd = new MdKaBigSysNameConfAddDto();
                mdKaBigSysNameConfAdd.SaleOrg = item.SALEORG;
                mdKaBigSysNameConfAdd.KASystemNum = item.KASYSTEM_NUM;
                mdKaBigSysNameConfAdd.SaleOrgNm = item.ZSALEORG_DES;
                mdKaBigSysNameConfAdd.KASystemName = item.KASYSTEM_NAME;
                mdKaBigSysNameConfAdd.KALx = item.ZKALX;

                mdKaBigSysNameConfAdds.Add(mdKaBigSysNameConfAdd);
            }

            var updateMdKaBigSysNameConfResult = await UpdateMdKaBigSysNameConf(mdKaBigSysNameConfAdds);
            #endregion

            #region 国家省份
            List<MdCountryProvinceAddDto> mdCountryProvinceAdds = new List<MdCountryProvinceAddDto>();
            MdCountryProvinceAddDto mdCountryProvinceAdd = null;

            foreach (var item in r.ET_ZSNS_COUNTRY_PROVINCE)
            {
                mdCountryProvinceAdd = new MdCountryProvinceAddDto();
                mdCountryProvinceAdd.CountryCD = item.COUNTRY;
                mdCountryProvinceAdd.CountryNm = item.LANDX;
                mdCountryProvinceAdd.ProvinceCD = item.ZAREA_NUM;
                mdCountryProvinceAdd.ProvinceNm = item.ZAREA_NAME;

                mdCountryProvinceAdds.Add(mdCountryProvinceAdd);
            }

            var UpdateMdCountryProvinceResult = await UpdateMdCountryProvince(mdCountryProvinceAdds);
            #endregion

            #region 省份城市
            List<MdProvinceCityAddDto> mdProvinceCityAdds = new List<MdProvinceCityAddDto>();
            MdProvinceCityAddDto mdProvinceCityAdd = null;

            foreach (var item in r.ET_ZSNTM0015)
            {
                mdProvinceCityAdd = new MdProvinceCityAddDto();
                mdProvinceCityAdd.ProvinceCD = item.ZPROVINCE_NUM;
                mdProvinceCityAdd.ProvinceNm = item.ZPROVINCE_NAME;
                mdProvinceCityAdd.CityCD = item.ZCITY_NUM;
                mdProvinceCityAdd.CityNm = item.ZCITY_NAME;

                mdProvinceCityAdds.Add(mdProvinceCityAdd);
            }

            var updateMdProvinceCityResult = await UpdateMdProvinceCity(mdProvinceCityAdds);
            #endregion

            #region 城市区县
            List<MdCityDistrictAddDto> mdCityDistrictAdds = new List<MdCityDistrictAddDto>();
            MdCityDistrictAddDto mdCityDistrictAdd = null;

            foreach (var item in r.ET_ZSNTM0016)
            {
                mdCityDistrictAdd = new MdCityDistrictAddDto();
                mdCityDistrictAdd.CityCD = item.ZCITY_NUM;
                mdCityDistrictAdd.CityNm = item.ZCITY_NAME;
                mdCityDistrictAdd.DistrictCD = item.ZCOUNTY_NUM;
                mdCityDistrictAdd.DistrictNm = item.ZCOUNTY_NAME;

                mdCityDistrictAdds.Add(mdCityDistrictAdd);
            }

            var updateMdCityDistrictResult = await UpdateMdCityDistrict(mdCityDistrictAdds);
            #endregion

            #region 区县街道
            List<MdDistrictStreetAddDto> mdDistrictStreetAdds = new List<MdDistrictStreetAddDto>();
            MdDistrictStreetAddDto mdDistrictStreetAdd = null;

            foreach (var item in r.ET_ZSNTM0078)
            {
                mdDistrictStreetAdd = new MdDistrictStreetAddDto();
                mdDistrictStreetAdd.DistrictCD = item.ZCOUNTY_NUM;
                mdDistrictStreetAdd.DistrictNm = item.ZCOUNTY_NAME;
                mdDistrictStreetAdd.StreetCD = item.ZSTREET_NUM;
                mdDistrictStreetAdd.StreetNm = item.ZSTREET_NAME;

                mdDistrictStreetAdds.Add(mdDistrictStreetAdd);
            }

            var updateMdDistrictStreetResult = await UpdateMdDistrictStreet(mdDistrictStreetAdds);
            #endregion

            #region 街道村
            List<MdStreetVillageAddDto> mdStreetVillageAdds = new List<MdStreetVillageAddDto>();
            MdStreetVillageAddDto mdStreetVillageAdd = null;

            foreach (var item in r.ET_ZSNTM0079)
            {
                mdStreetVillageAdd = new MdStreetVillageAddDto();
                mdStreetVillageAdd.StreetCD = item.ZSTREET_NUM;
                mdStreetVillageAdd.StreetNm = item.ZSTREET_NAME;
                mdStreetVillageAdd.VillageCD = item.ZVILLAGE_NUM;
                mdStreetVillageAdd.VillageNm = item.ZVILLAGE_NAME;

                mdStreetVillageAdds.Add(mdStreetVillageAdd);
            }

            var updateMdStreetVillageResult = await UpdateMdStreetVillage(mdStreetVillageAdds);
            #endregion

            #region 职位优先级(暂不接入)
            //foreach (var item in r.ET_PRIORTY)
            //{

            //}
            #endregion

            #region KA系统名称(暂不接入)
            //foreach (var item in r.ET_ZSNTM0011)
            //{

            //}
            #endregion

            return ResultModel.Success("同步配置数据完毕：" + $"同步业务实体{(updateEntityResult ? "成功" : "失败")}"
                                                            + $"同步业务线/一级终端类型/二级终端类型/三级终端类型{(updateTmnTypeResult ? "成功" : "失败")}"
                                                            + $"同步终端业态配置表{(updateMdReTmnBTyteConfigResult ? "成功" : "失败")}"
                                                            + $"同步制高点配置{(updateMdHeightConfResult ? "成功" : "失败")}"
                                                            + $"同步KA大系统{(updateMdKaBigSysNameConfResult ? "成功" : "失败")}"
                                                            + $"同步国家省份{(UpdateMdCountryProvinceResult ? "成功" : "失败")}"
                                                            + $"同步省份城市{(updateMdProvinceCityResult ? "成功" : "失败")}"
                                                            + $"同步城市区县{(updateMdCityDistrictResult ? "成功" : "失败")}"
                                                            + $"同步区县街道{(updateMdDistrictStreetResult ? "成功" : "失败")}"
                                                            + $"同步街道村{(updateMdStreetVillageResult ? "成功" : "失败")}"
                                                            );
        }

        /// <summary>
        /// 更新业务实体
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<bool> UpdateEntity(List<MEntityAddDto> data)
        {
            try
            {
                string tmpMEntityTableName = $"Tmp_M_Entity_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                string tmpMEntityTableSql = $@"CREATE TABLE [{tmpMEntityTableName}] (
                                                [EntityCode] varchar(30) NOT NULL,
                                                [EntityName] nvarchar(40) NOT NULL,
                                                [Enabled] bit NOT NULL,
                                                [ERPCode] varchar(30) NOT NULL
                                                )";
                await _mEntityRepository.Execute(tmpMEntityTableSql);
                DataTable tmpMEntityTable = data.ToDataTable();
                tmpMEntityTable.TableName = tmpMEntityTableName;

                _dbContext.SqlBulkCopy(tmpMEntityTable);


                string updateMEntitySql = $@"MERGE INTO M_Entity AS T
                                            USING {tmpMEntityTableName} AS S
                                            ON S.EntityCode = T.EntityCode
                                            WHEN MATCHED THEN
                                                UPDATE SET T.EntityName = S.EntityName,T.[Enabled] = S.[Enabled]
                                            WHEN NOT MATCHED THEN
                                                INSERT (Id,EntityCode,EntityName,ERPCode,[Enabled])
                                                VALUES (NEWID(),S.EntityCode,S.EntityName,S.ERPCode,S.[Enabled]);";

                string updateMEntityStatusSql = $@"UPDATE T set T.[Enabled] = 0 from M_Entity T
                                                WHERE NOT EXISTS (SELECT 1 FROM {tmpMEntityTableName} S WHERE T.EntityCode = S.EntityCode)";

                string deleteTmpMEntityTableSql = $@"DROP TABLE {tmpMEntityTableName}";

                await _mEntityRepository.Execute(updateMEntitySql);
                await _mEntityRepository.Execute(updateMEntityStatusSql);
                await _mEntityRepository.Execute(deleteTmpMEntityTableSql);

                return true;
            }
            catch (Exception ex)
            {
                //todo 写日志
                return false;
            }
        }

        /// <summary>
        /// 更新终端 业务线/一级终端类型/二级终端类型/三级终端类型
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<bool> UpdateTmnType(List<MdTmnTypeAddDto> data)
        {
            try
            {
                string tmpTableName = $"Tmp_MD_TmnType_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                string tmpCreateTableSql = $@"CREATE TABLE [{tmpTableName}] (
                                                [RegionCD] char(8) NOT NULL,
                                                [MarketOrgCD] char(8) NOT NULL,
                                                [LineCD] varchar(10) NOT NULL,
                                                [LineNm] nvarchar(40) NOT NULL,
                                                [Level1TypeCD] varchar(10) NOT NULL,
                                                [Level1TypeNm] nvarchar(40) NULL,
                                                [Level2TypeCD] varchar(10) NOT NULL,
                                                [Level2TypeNm] nvarchar(40) NULL,
                                                [Level3TypeCD] varchar(10) NOT NULL,
                                                [Level3TypeNm] nvarchar(40) NULL,
                                                [Status] int NOT NULL
                                                )";
                await _mdTmnTypeRepository.Execute(tmpCreateTableSql);
                DataTable tmpTable = data.ToDataTable();
                tmpTable.TableName = tmpTableName;

                //BulkInsertExtensions.SqlBulkCopy(connectionString, tmpTable);
                _dbContext.SqlBulkCopy(tmpTable);


                string updateSql = $@"MERGE INTO MD_TmnType AS T
                                            USING {tmpTableName} AS S
                                            ON S.RegionCD = T.RegionCD and S.MarketOrgCD = T.MarketOrgCD AND S.LineCD = T.LineCD AND S.Level1TypeCD = T.Level1TypeCD AND S.Level2TypeCD = T.Level2TypeCD AND S.Level3TypeCD = T.Level3TypeCD
                                            WHEN MATCHED THEN
                                                UPDATE SET T.LineNm = S.LineNm,T.Level1TypeNm = S.Level1TypeNm,T.Level2TypeNm = S.Level2TypeNm,T.Level3TypeNm = S.Level3TypeNm,T.Status = S.Status
                                            WHEN NOT MATCHED THEN
                                                INSERT (Id,RegionCD,MarketOrgCD,LineCD,LineNm,Level1TypeCD,Level1TypeNm,Level2TypeCD,Level2TypeNm,Level3TypeCD,Level3TypeNm,Status)
                                                VALUES (NEWID(),S.RegionCD,S.MarketOrgCD,S.LineCD,S.LineNm,S.Level1TypeCD,S.Level1TypeNm,S.Level2TypeCD,S.Level2TypeNm,S.Level3TypeCD,S.Level3TypeNm,S.Status);";

                string updateStatusSql = $@"UPDATE T set T.Status = 0 from MD_TmnType T
                                                WHERE NOT EXISTS (SELECT 1 FROM {tmpTableName} S WHERE S.RegionCD = T.RegionCD and S.MarketOrgCD = T.MarketOrgCD AND S.LineCD = T.LineCD AND S.Level1TypeCD = T.Level1TypeCD AND S.Level2TypeCD = T.Level2TypeCD AND S.Level3TypeCD = T.Level3TypeCD)";

                string updateSaleLineSql = $@"insert into MD_SaleLine (Id,LineCD,LineNm,[Status])
                                                select NEWID(),z.LineCD,z.LineNm,1 from (select distinct LineCD,LineNm from {tmpTableName}) z 
                                                where not exists (select 1 from MD_SaleLine sl where z.LineCD = sl.LineCD);
                                              update sl set sl.LineNm = z.LineNm from MD_SaleLine sl 
                                                inner join (select distinct LineCD,LineNm from {tmpTableName}) z on sl.LineCD = z.LineCD;
                                              update sl set sl.[Status] = 0 from MD_SaleLine sl where not exists (select 1 from {tmpTableName} t where sl.LineCD = t.LineCD);";

                string deleteTableSql = $@"DROP TABLE {tmpTableName}";

                await _mdTmnTypeRepository.Execute(updateSql);
                await _mdTmnTypeRepository.Execute(updateStatusSql);
                await _mdTmnTypeRepository.Execute(updateSaleLineSql);
                await _mdTmnTypeRepository.Execute(deleteTableSql);

                return true;
            }
            catch (Exception ex)
            {
                //todo 写日志
                return false;
            }
        }

        /// <summary>
        /// 更新终端业态配置表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<bool> UpdateMdReTmnBTyteConfig(List<MdReTmnBTyteConfigAddDto> data)
        {
            try
            {
                string tmpTableName = $"Tmp_MD_Re_Tmn_BTyte_Config_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                string tmpCreateTableSql = $@"CREATE TABLE [{tmpTableName}] (
                                                [TmnStoreType1] nvarchar(10) NOT NULL,
                                                [ZbnType] nvarchar(10) NOT NULL,
                                                [ZbnTypeTxt] nvarchar(40) NOT NULL,
                                                [Status] int NOT NULL
                                                )";
                await _mdReTmnBTyteConfigRepository.Execute(tmpCreateTableSql);
                DataTable tmpTable = data.ToDataTable();
                tmpTable.TableName = tmpTableName;

                _dbContext.SqlBulkCopy(tmpTable);


                string updateSql = $@"MERGE INTO MD_Re_Tmn_BTyte_Config AS T
                                            USING {tmpTableName} AS S
                                            ON S.TmnStoreType1 = T.TmnStoreType1 and S.ZbnType = T.ZbnType
                                            WHEN MATCHED THEN
                                                UPDATE SET T.ZbnTypeTxt = S.ZbnTypeTxt,T.Status = S.Status
                                            WHEN NOT MATCHED THEN
                                                INSERT (Id,TmnStoreType1,ZbnType,ZbnTypeTxt,Status)
                                                VALUES (NEWID(),S.TmnStoreType1,S.ZbnType,S.ZbnTypeTxt,S.Status);";

                string updateStatusSql = $@"UPDATE T set T.Status = 0 from MD_Re_Tmn_BTyte_Config T
                                                WHERE NOT EXISTS (SELECT 1 FROM {tmpTableName} S WHERE S.TmnStoreType1 = T.TmnStoreType1 and S.ZbnType = T.ZbnType)";

                string deleteTableSql = $@"DROP TABLE {tmpTableName}";

                await _mdReTmnBTyteConfigRepository.Execute(updateSql);
                await _mdReTmnBTyteConfigRepository.Execute(updateStatusSql);
                await _mdReTmnBTyteConfigRepository.Execute(deleteTableSql);

                return true;
            }
            catch (Exception ex)
            {
                //todo 写日志
                return false;
            }
        }

        /// <summary>
        /// 更新 制高点配置
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<bool> UpdateMdHeightConf(List<MdHeightConfAddDto> data)
        {
            try
            {
                string tmpTableName = $"Tmp_MD_HeightConf_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                string tmpCreateTableSql = $@"CREATE TABLE [{tmpTableName}] (
                                                [SaleOrg] char(14) NOT NULL,
                                                [Height] char(10) NOT NULL,
                                                [Text] nvarchar(40) NULL
                                                )";
                await _mdHeightConfRepository.Execute(tmpCreateTableSql);
                DataTable tmpTable = data.ToDataTable();
                tmpTable.TableName = tmpTableName;

                _dbContext.SqlBulkCopy(tmpTable);

                string updateSql = $@"MERGE INTO MD_HeightConf AS T
                                            USING {tmpTableName} AS S
                                            ON S.SaleOrg = T.SaleOrg and S.Height = T.Height
                                            WHEN MATCHED THEN
                                                UPDATE SET T.Text = S.Text
                                            WHEN NOT MATCHED THEN
                                                INSERT (Id,SaleOrg,Height,Text)
                                                VALUES (NEWID(),S.SaleOrg,S.Height,S.Text);";

                string deleteSql = $@"delete from MD_HeightConf T where not exists (SELECT 1 FROM {tmpTableName} S WHERE S.SaleOrg = T.SaleOrg and S.Height = T.Height)";

                string deleteTableSql = $@"DROP TABLE {tmpTableName}";

                await _mdHeightConfRepository.Execute(updateSql);
                await _mdHeightConfRepository.Execute(deleteSql);
                await _mdHeightConfRepository.Execute(deleteTableSql);

                return true;
            }
            catch (Exception ex)
            {
                //todo 写日志
                return false;
            }
        }

        /// <summary>
        /// 更新 KA大系统
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<bool> UpdateMdKaBigSysNameConf(List<MdKaBigSysNameConfAddDto> data)
        {
            try
            {
                string tmpTableName = $"Tmp_MD_KABigSysNameConf_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                string tmpCreateTableSql = $@"CREATE TABLE [{tmpTableName}] (
                                                [SaleOrg] char(20) NOT NULL,
                                                [KASystemNum] char(20) NOT NULL,
                                                [SaleOrgNm] nvarchar(40) NULL,
                                                [KASystemName] nvarchar(60) NULL,
                                                [KALx] char(6) NULL
                                                )";
                await _mdKaBigSysNameConfRepository.Execute(tmpCreateTableSql);
                DataTable tmpTable = data.ToDataTable();
                tmpTable.TableName = tmpTableName;

                _dbContext.SqlBulkCopy(tmpTable);

                string updateSql = $@"MERGE INTO MD_KABigSysNameConf AS T
                                            USING {tmpTableName} AS S
                                            ON S.SaleOrg = T.SaleOrg and S.KASystemNum = T.KASystemNum
                                            WHEN MATCHED THEN
                                                UPDATE SET T.SaleOrgNm = S.SaleOrgNm,T.KASystemName = S.KASystemName,T.KALx = S.KALx
                                            WHEN NOT MATCHED THEN
                                                INSERT (Id,SaleOrg,KASystemNum,SaleOrgNm,KASystemName,KALx)
                                                VALUES (NEWID(),S.SaleOrg,S.KASystemNum,S.SaleOrgNm,S.KASystemName,S.KALx);";

                string deleteSql = $@"delete from MD_KABigSysNameConf T
                                                WHERE NOT EXISTS (SELECT 1 FROM {tmpTableName} S WHERE S.SaleOrg = T.SaleOrg and S.KASystemNum = T.KASystemNum)";

                string deleteTableSql = $@"DROP TABLE {tmpTableName}";

                await _mdKaBigSysNameConfRepository.Execute(updateSql);
                await _mdKaBigSysNameConfRepository.Execute(deleteSql);
                await _mdKaBigSysNameConfRepository.Execute(deleteTableSql);

                return true;
            }
            catch (Exception ex)
            {
                //todo 写日志
                return false;
            }
        }

        /// <summary>
        /// 更新 国家省份
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<bool> UpdateMdCountryProvince(List<MdCountryProvinceAddDto> data)
        {
            try
            {
                string tmpTableName = $"Tmp_MD_CountryProvince_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                string tmpCreateTableSql = $@"CREATE TABLE [{tmpTableName}] (
                                                [CountryCD] varchar(3) NOT NULL,
                                                [CountryNm] nvarchar(15) NOT NULL,
                                                [ProvinceCD] varchar(3) NULL,
                                                [ProvinceNm] nvarchar(20) NULL
                                                )";
                await _mdCountryProvinceRepository.Execute(tmpCreateTableSql);
                DataTable tmpTable = data.ToDataTable();
                tmpTable.TableName = tmpTableName;

                _dbContext.SqlBulkCopy(tmpTable);

                string updateSql = $@"MERGE INTO MD_CountryProvince AS T
                                            USING {tmpTableName} AS S
                                            ON S.CountryCD = T.CountryCD and S.ProvinceCD = T.ProvinceCD
                                            WHEN MATCHED THEN
                                                UPDATE SET T.CountryNm = S.CountryNm,T.ProvinceNm = S.ProvinceNm
                                            WHEN NOT MATCHED THEN
                                                INSERT (Id,CountryCD,CountryNm,ProvinceCD,ProvinceNm)
                                                VALUES (NEWID(),S.CountryCD,S.CountryNm,S.ProvinceCD,S.ProvinceNm);";

                string deleteSql = $@"delete from MD_CountryProvince T
                                                WHERE NOT EXISTS (SELECT 1 FROM {tmpTableName} S WHERE S.CountryCD = T.CountryCD and S.ProvinceCD = T.ProvinceCD)";

                string deleteTableSql = $@"DROP TABLE {tmpTableName}";

                await _mdCountryProvinceRepository.Execute(updateSql);
                await _mdCountryProvinceRepository.Execute(deleteSql);
                await _mdCountryProvinceRepository.Execute(deleteTableSql);

                return true;
            }
            catch (Exception ex)
            {
                //todo 写日志
                return false;
            }
        }

        /// <summary>
        /// 更新 省份城市
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<bool> UpdateMdProvinceCity(List<MdProvinceCityAddDto> data)
        {
            try
            {
                string tmpTableName = $"Tmp_MD_ProvinceCity_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                string tmpCreateTableSql = $@"CREATE TABLE [{tmpTableName}] (
                                                [ProvinceCD] varchar(3) NOT NULL,
                                                [ProvinceNm] nvarchar(20) NULL,
                                                [CityCD] varchar(10) NOT NULL,
                                                [CityNm] nvarchar(20) NULL
                                                )";
                await _mdProvinceCityRepository.Execute(tmpCreateTableSql);
                DataTable tmpTable = data.ToDataTable();
                tmpTable.TableName = tmpTableName;

                _dbContext.SqlBulkCopy(tmpTable);

                string updateSql = $@"MERGE INTO MD_ProvinceCity AS T
                                            USING {tmpTableName} AS S
                                            ON S.ProvinceCD = T.ProvinceCD and S.CityCD = T.CityCD
                                            WHEN MATCHED THEN
                                                UPDATE SET T.ProvinceNm = S.ProvinceNm,T.CityNm = S.CityNm
                                            WHEN NOT MATCHED THEN
                                                INSERT (Id,ProvinceCD,ProvinceNm,CityCD,CityNm)
                                                VALUES (NEWID(),S.ProvinceCD,S.ProvinceNm,S.CityCD,S.CityNm);";

                string deleteSql = $@"delete from MD_ProvinceCity T
                                                WHERE NOT EXISTS (SELECT 1 FROM {tmpTableName} S WHERE S.ProvinceCD = T.ProvinceCD and S.CityCD = T.CityCD)";

                string deleteTableSql = $@"DROP TABLE {tmpTableName}";

                await _mdProvinceCityRepository.Execute(updateSql);
                await _mdProvinceCityRepository.Execute(deleteSql);
                await _mdProvinceCityRepository.Execute(deleteTableSql);

                return true;
            }
            catch (Exception ex)
            {
                //todo 写日志
                return false;
            }
        }

        /// <summary>
        /// 更新 城市区县
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<bool> UpdateMdCityDistrict(List<MdCityDistrictAddDto> data)
        {
            try
            {
                string tmpTableName = $"Tmp_MD_CityDistrict_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                string tmpCreateTableSql = $@"CREATE TABLE [{tmpTableName}] (
                                                [CityCD] varchar(10) NOT NULL,
                                                [CityNm] nvarchar(20) NOT NULL,
                                                [DistrictCD] varchar(20) NOT NULL,
                                                [DistrictNm] nvarchar(20) NOT NULL
                                                )";
                await _mdCityDistrictRepository.Execute(tmpCreateTableSql);
                DataTable tmpTable = data.ToDataTable();
                tmpTable.TableName = tmpTableName;

                _dbContext.SqlBulkCopy(tmpTable);

                string updateSql = $@"MERGE INTO MD_CityDistrict AS T
                                            USING {tmpTableName} AS S
                                            ON S.CityCD = T.CityCD and S.DistrictCD = T.DistrictCD
                                            WHEN MATCHED THEN
                                                UPDATE SET T.CityNm = S.CityNm,T.DistrictNm = S.DistrictNm
                                            WHEN NOT MATCHED THEN
                                                INSERT (Id,CityCD,CityNm,DistrictCD,DistrictNm)
                                                VALUES (NEWID(),S.CityCD,S.CityNm,S.DistrictCD,S.DistrictNm);";

                string deleteSql = $@"delete from MD_CityDistrict T
                                                WHERE NOT EXISTS (SELECT 1 FROM {tmpTableName} S WHERE S.CityCD = T.CityCD and S.DistrictCD = T.DistrictCD)";

                string deleteTableSql = $@"DROP TABLE {tmpTableName}";

                await _mdCityDistrictRepository.Execute(updateSql);
                await _mdCityDistrictRepository.Execute(deleteSql);
                await _mdCityDistrictRepository.Execute(deleteTableSql);

                return true;
            }
            catch (Exception ex)
            {
                //todo 写日志
                return false;
            }
        }

        /// <summary>
        /// 更新 区县街道
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<bool> UpdateMdDistrictStreet(List<MdDistrictStreetAddDto> data)
        {
            try
            {
                string tmpTableName = $"Tmp_MD_DistrictStreet_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                string tmpCreateTableSql = $@"CREATE TABLE [{tmpTableName}] (
                                                [DistrictCD] varchar(20) NOT NULL,
                                                [DistrictNm] nvarchar(20) NOT NULL,
                                                [StreetCD] varchar(20) NOT NULL,
                                                [StreetNm] nvarchar(20) NOT NULL
                                                )";
                await _mdDistrictStreetRepository.Execute(tmpCreateTableSql);
                DataTable tmpTable = data.ToDataTable();
                tmpTable.TableName = tmpTableName;

                _dbContext.SqlBulkCopy(tmpTable);

                string updateSql = $@"MERGE INTO MD_DistrictStreet AS T
                                            USING {tmpTableName} AS S
                                            ON S.DistrictCD = T.DistrictCD and S.StreetCD = T.StreetCD
                                            WHEN MATCHED THEN
                                                UPDATE SET T.DistrictNm = S.DistrictNm,T.StreetNm = S.StreetNm
                                            WHEN NOT MATCHED THEN
                                                INSERT (Id,DistrictCD,DistrictNm,StreetCD,StreetNm)
                                                VALUES (NEWID(),S.DistrictCD,S.DistrictNm,S.StreetCD,S.StreetNm);";

                string deleteSql = $@"delete from MD_DistrictStreet T
                                                WHERE NOT EXISTS (SELECT 1 FROM {tmpTableName} S WHERE S.DistrictCD = T.DistrictCD and S.StreetCD = T.StreetCD)";

                string deleteTableSql = $@"DROP TABLE {tmpTableName}";

                await _mdDistrictStreetRepository.Execute(updateSql);
                await _mdDistrictStreetRepository.Execute(deleteSql);
                await _mdDistrictStreetRepository.Execute(deleteTableSql);

                return true;
            }
            catch (Exception ex)
            {
                //todo 写日志
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<bool> UpdateMdStreetVillage(List<MdStreetVillageAddDto> data)
        {
            try
            {
                string tmpTableName = $"Tmp_MD_StreetVillage_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                string tmpCreateTableSql = $@"CREATE TABLE [{tmpTableName}] (
                                                [StreetCD] varchar(20) NOT NULL,
                                                [StreetNm] nvarchar(20) NOT NULL,
                                                [VillageCD] varchar(20) NOT NULL,
                                                [VillageNm] nvarchar(20) NOT NULL
                                                )";
                await _mdStreetVillageRepository.Execute(tmpCreateTableSql);
                DataTable tmpTable = data.ToDataTable();
                tmpTable.TableName = tmpTableName;

                _dbContext.SqlBulkCopy(tmpTable);

                string updateSql = $@"MERGE INTO MD_StreetVillage AS T
                                            USING {tmpTableName} AS S
                                            ON S.StreetCD = T.StreetCD and S.VillageCD = T.VillageCD
                                            WHEN MATCHED THEN
                                                UPDATE SET T.StreetNm = S.StreetNm,T.VillageNm = S.VillageNm
                                            WHEN NOT MATCHED THEN
                                                INSERT (Id,StreetCD,StreetNm,VillageCD,VillageNm)
                                                VALUES (NEWID(),S.StreetCD,S.StreetNm,S.VillageCD,S.VillageNm);";

                string deleteSql = $@"delete from MD_StreetVillage T
                                                WHERE NOT EXISTS (SELECT 1 FROM {tmpTableName} S WHERE S.StreetCD = T.StreetCD and S.VillageCD = T.VillageCD)";

                string deleteTableSql = $@"DROP TABLE {tmpTableName}";

                await _mdStreetVillageRepository.Execute(updateSql);
                await _mdStreetVillageRepository.Execute(deleteSql);
                await _mdStreetVillageRepository.Execute(deleteTableSql);

                return true;
            }
            catch (Exception ex)
            {
                //todo 写日志
                return false;
            }
        }
    }
}
