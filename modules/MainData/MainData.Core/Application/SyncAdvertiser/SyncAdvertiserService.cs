using CRB.TPM.Config.Abstractions;
using CRB.TPM.Data.Core.Extensions;
using CRB.TPM.Data.Sharding;
using CRB.TPM.Mod.MainData.Core.Application.MAdvertiser.Dto;
using CRB.TPM.Mod.MainData.Core.Application.SyncAdvertiser.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiser;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAccount;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAccountAddress;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAddress;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserOrg;
using CRB.TPM.Mod.MainData.Core.Infrastructure;
using CRB.TPM.Utils.Helpers;
using CRB.TPM.Utils.Map;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.SyncAdvertiser
{
    public class SyncAdvertiserService : ISyncAdvertiserService
    {
        private readonly IMapper _mapper;
        private readonly MainDataDbContext _dbContext;
        private readonly IMAdvertiserRepository _mAdvertiserRepository;
        private readonly IMAdvertiserOrgRepository _mAdvertiserOrgRepository;
        private readonly IMAdvertiserAccountRepository _mAdvertiserAccountRepository;
        private readonly IMAdvertiserAccountAddressRepository _mAdvertiserAccountAddressRepository;
        private readonly IMAdvertiserAddressRepository _mAdvertiserAddressRepository;
        private readonly IConfigProvider _configProvider;
        public SyncAdvertiserService(IMapper mapper,
                                     MainDataDbContext dbContext,
                                     IMAdvertiserRepository mAdvertiserRepository,
                                     IMAdvertiserOrgRepository mAdvertiserOrgRepository,
                                     IMAdvertiserAccountRepository mAdvertiserAccountRepository,
                                     IMAdvertiserAccountAddressRepository mAdvertiserAccountAddressRepository,
                                     IMAdvertiserAddressRepository mAdvertiserAddressRepository,
                                     IConfigProvider configProvider)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _mAdvertiserRepository = mAdvertiserRepository;
            _mAdvertiserOrgRepository = mAdvertiserOrgRepository;
            _mAdvertiserAccountRepository = mAdvertiserAccountRepository;
            _mAdvertiserAccountAddressRepository = mAdvertiserAccountAddressRepository;
            _mAdvertiserAddressRepository = mAdvertiserAddressRepository;
            _configProvider = configProvider;
        }

        public async Task<IResultModel> SyncData(string date, string marketOrgCode)
        {
            var config = _configProvider.Get<MainDataConfig>();

            string inType = "IF0004";
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

            string busParaModule = "\"INTYP\":\"{0}\",\"OSPLATFM\":\"NET\",\"MODE\":\"{1}\",\"DATE\":\"{2}\",\"ORG\":\"{3}\",\"IMEI\":\"1\"";
            string busPara = "{" + string.Format(busParaModule, inType, mode, date, marketOrgCode) + "}";

            IResultModel<string> result = SapWebServiceHelper.GetWebServiceResult(busPara, config.SapWsUrl);

            if (!result.Successful)
                return result;

            var r = JsonConvert.DeserializeObject<Result_Advertiser>(result.Data);

            #region 广告商主数据
            List<MAdvertiserAddDto> mAdvertiserAdds = new List<MAdvertiserAddDto>();
            MAdvertiserAddDto mAdvertiserAdd = null;

            foreach (var item in r.ET_ADVERTIER)
            {
                mAdvertiserAdd = new MAdvertiserAddDto();
                mAdvertiserAdd.Id = Guid.NewGuid();
                mAdvertiserAdd.AdvertiserCode = item.VENDOR_CODE;
                mAdvertiserAdd.AdvertiserName = item.NAME_ORG1;
                mAdvertiserAdd.RegionCD = item.ORG;
                mAdvertiserAdd.Status = item.STATUS.Equals("Y") ? 1 : 0;
                mAdvertiserAdd.ADJC = item.JC;

                mAdvertiserAdds.Add(mAdvertiserAdd);
            }

            var updateAvertiserResult = await UpdateAdvertiser(mAdvertiserAdds);
            #endregion

            #region 广告商营销组织关系表
            List<MAdvertiserOrgAddExtendDto> mAdvertiserOrgAdds = new List<MAdvertiserOrgAddExtendDto>();
            MAdvertiserOrgAddExtendDto mAdvertiserOrgAdd = null;

            foreach (var item in r.ET_ORG)
            {
                var advertiser = mAdvertiserAdds.FirstOrDefault(x => x.AdvertiserCode == item.ZZQD_NUM);

                if (advertiser != null)
                {
                    mAdvertiserOrgAdd = new MAdvertiserOrgAddExtendDto();
                    mAdvertiserOrgAdd.MarketOrg = item.ZZMAKET_ID;
                    mAdvertiserOrgAdd.BigAreaOrg = item.ZZOFFICE_ID;
                    mAdvertiserOrgAdd.OfficeOrg = item.ZZGROUP_ID;
                    mAdvertiserOrgAdd.StationOrg = item.ZZGZZ_ID;
                    mAdvertiserOrgAdd.AdvertiserId = advertiser.Id;
                    mAdvertiserOrgAdd.AdvertiserCode = item.ZZQD_NUM;
                    mAdvertiserOrgAdd.Status = 1;// int.Parse(item.ZZJXSTATUS);

                    mAdvertiserOrgAdds.Add(mAdvertiserOrgAdd);
                }
            }

            var updateAdvertiserOrgResult = await UpdateAdvertiserOrg(mAdvertiserOrgAdds);
            #endregion

            #region 广告商银行账号表
            List<MAdvertiserAccountAddExtendDto> mAdvertiserAccountAdds = new List<MAdvertiserAccountAddExtendDto>();
            MAdvertiserAccountAddExtendDto mAdvertiserAccountAdd = null;

            foreach (var item in r.ET_ADVERT_BANK)
            {
                var advertiser = mAdvertiserAdds.FirstOrDefault(x => x.AdvertiserCode == item.VENDOR_CODE);
                if (advertiser != null)
                {
                    mAdvertiserAccountAdd = new MAdvertiserAccountAddExtendDto();
                    mAdvertiserAccountAdd.Id = Guid.NewGuid();
                    mAdvertiserAccountAdd.AdvertiserId = advertiser.Id;
                    mAdvertiserAccountAdd.AdvertiserCode = item.VENDOR_CODE;
                    mAdvertiserAccountAdd.BankAccount = item.ACOUNTNO;
                    mAdvertiserAccountAdd.BankActNm = item.BANKNO;
                    mAdvertiserAccountAdd.BankNm = item.BANKNAME;
                    mAdvertiserAccountAdd.CustomerNm = item.ACOUNTNAME;
                    mAdvertiserAccountAdd.CurrencyCD = item.CURRCODE;
                    //mAdvertiserAccountAdd.DateStart = item.;
                    //mAdvertiserAccountAdd.DateEnd = item.;
                    mAdvertiserAccountAdd.Status = string.IsNullOrWhiteSpace(item.STATUS) ? 0 : int.Parse(item.STATUS);
                    mAdvertiserAccountAdd.AccountTYP = item.ACOUNTTYP;
                    mAdvertiserAccountAdd.IsMainFLG = item.MAINFLG;
                    mAdvertiserAccountAdd.BankInfoNUM = item.BANKINFONUM;
                    mAdvertiserAccountAdd.RBKNUM = item.RBKNUM;
                    mAdvertiserAccountAdd.IsValid = item.VALID;
                    mAdvertiserAccountAdd.AreaCode = item.AREA_CODE;
                    mAdvertiserAccountAdd.CommercialBankDocumentNUM = item.ARCHNUM;
                    mAdvertiserAccountAdd.ModelCode = item.CODE;
                    mAdvertiserAccountAdd.MainCode = item.MAINCD;

                    mAdvertiserAccountAdds.Add(mAdvertiserAccountAdd);
                }
            }

            var updateAdvertiserAccountResult = await UpdateAdvertiserAccount(mAdvertiserAccountAdds);
            #endregion

            #region 广告商地点分配表
            List<MAdvertiserAccountAddressAddExtendDto> mAdvertiserAccountAddressAdds = new List<MAdvertiserAccountAddressAddExtendDto>();
            MAdvertiserAccountAddressAddExtendDto mAdvertiserAccountAddressAdd = null;

            foreach (var item in r.ET_ADVERT_ASSIGN)
            {
                var advertiserAccount = mAdvertiserAccountAdds.FirstOrDefault(x => x.BankAccount == item.ACOUNTNO);
                if (advertiserAccount != null)
                {
                    mAdvertiserAccountAddressAdd = new MAdvertiserAccountAddressAddExtendDto();
                    mAdvertiserAccountAddressAdd.AdvertiserId = advertiserAccount.AdvertiserId;
                    mAdvertiserAccountAddressAdd.AdvertiserCode = item.VENDOR_CODE;
                    mAdvertiserAccountAddressAdd.ADDRNO = item.ADDRNO;
                    mAdvertiserAccountAddressAdd.AdvertiserAccountId = advertiserAccount.Id;
                    mAdvertiserAccountAddressAdd.AdvertiserAccount = item.ACOUNTNO;
                    mAdvertiserAccountAddressAdd.ASSIGNSTAU = item.ASSIGNSTAU;
                    mAdvertiserAccountAddressAdd.UUID = item.UUID;
                    mAdvertiserAccountAddressAdd.STDATE = item.STDATE;
                    mAdvertiserAccountAddressAdd.ENDATE = item.ENDATE;
                    mAdvertiserAccountAddressAdd.CODE = item.CODE;
                    mAdvertiserAccountAddressAdd.MAINCD = item.MAINCD;
                    mAdvertiserAccountAddressAdd.BUCODE = item.BUCODE;

                    mAdvertiserAccountAddressAdds.Add(mAdvertiserAccountAddressAdd);
                }
            }

            var updateAdvertiserAccountAddressResult = await UpdateAdvertiserAccountAddress(mAdvertiserAccountAddressAdds);
            #endregion

            #region 广告商地点表
            List<MAdvertiserAddressAddExtendDto> mAdvertiserAddressAdds = new List<MAdvertiserAddressAddExtendDto>();
            MAdvertiserAddressAddExtendDto mAdvertiserAddressAdd = null;

            foreach (var item in r.ET_ADVERT_ADDR)
            {
                var advertiser = mAdvertiserAdds.FirstOrDefault(x => x.AdvertiserCode == item.VENDOR_CODE);

                if (advertiser != null)
                {
                    mAdvertiserAddressAdd = new MAdvertiserAddressAddExtendDto();
                    mAdvertiserAddressAdd.AdvertiserId = advertiser.Id;
                    mAdvertiserAddressAdd.AdvertiserCode = item.VENDOR_CODE;
                    mAdvertiserAddressAdd.ADDRNO = item.ADDRNO;
                    mAdvertiserAddressAdd.ADDRDESC = item.ADDRDESC;
                    mAdvertiserAddressAdd.ZDESC = item.ZDESC;
                    mAdvertiserAddressAdd.MAINPLACE = item.MAINPLACE;
                    mAdvertiserAddressAdd.ORGID = item.ORGID;
                    mAdvertiserAddressAdd.ORGCODE = item.ORGCODE;
                    mAdvertiserAddressAdd.ORGNAME = item.ORGNAME;
                    mAdvertiserAddressAdd.LOCSTATU = item.LOCSTATU;
                    mAdvertiserAddressAdd.INVDATE = item.INVDATE;
                    mAdvertiserAddressAdd.FKTJ = item.FKTJ;
                    mAdvertiserAddressAdd.COA = item.COA;
                    mAdvertiserAddressAdd.EXPACOUNT = item.EXPACOUNT;
                    mAdvertiserAddressAdd.ADVPACOUNT = item.ADVPACOUNT;
                    mAdvertiserAddressAdd.FUACOUNT = item.FUACOUNT;
                    mAdvertiserAddressAdd.CODE = item.CODE;
                    mAdvertiserAddressAdd.MAINCD = item.MAINCD;

                    mAdvertiserAddressAdds.Add(mAdvertiserAddressAdd);
                }
            }
            var updateAdvertiserAddressResult = await UpdateAdvertiserAddress(mAdvertiserAddressAdds);
            #endregion

            return ResultModel.Success("同步完毕：" + $"同步广告商主数据{(updateAvertiserResult ? "成功" : "失败")}"
                                                    + $"同步广告商营销组织关系表{(updateAdvertiserOrgResult ? "成功" : "失败")}"
                                                    + $"同步广告商银行账号表{(updateAdvertiserAccountResult ? "成功" : "失败")}"
                                                    + $"同步广告商地点分配表{(updateAdvertiserAccountAddressResult ? "成功" : "失败")}"
                                                    + $"同步广告商地点表{(updateAdvertiserAddressResult ? "成功" : "失败")}");
        }

        /// <summary>
        /// 更新广告商
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<bool> UpdateAdvertiser(List<MAdvertiserAddDto> data)
        {
            try
            {
                string tmpTableName = $"Tmp_M_Advertiser_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                string tmpCreateTableSql = $@"CREATE TABLE [{tmpTableName}] (
                                                [Id] uniqueidentifier NOT NULL,
                                                [AdvertiserCode] nvarchar(10) NOT NULL,
                                                [AdvertiserName] nvarchar(40) NOT NULL,
                                                [RegionCD] char(8) NOT NULL,
                                                [Status] int NOT NULL,
                                                [ADJC] nvarchar(50) NOT NULL
                                                )";
                await _mAdvertiserRepository.Execute(tmpCreateTableSql);
                DataTable tmpTable = data.ToDataTable();
                tmpTable.TableName = tmpTableName;

                _dbContext.SqlBulkCopy(tmpTable);

                string updateSql = $@"MERGE INTO M_Advertiser AS T
                                            USING {tmpTableName} AS S
                                            ON S.AdvertiserCode = T.AdvertiserCode
                                            WHEN MATCHED THEN
                                                UPDATE SET T.AdvertiserName = S.AdvertiserName,T.RegionCD = S.RegionCD,T.Status = S.Status,T.ADJC = S.ADJC
                                            WHEN NOT MATCHED THEN
                                                INSERT (Id,AdvertiserCode,AdvertiserName,RegionCD,Status,ADJC)
                                                VALUES (NEWID(),S.AdvertiserCode,S.AdvertiserName,S.RegionCD,S.Status,S.ADJC);";

                //string updateStatusSql = $@"UPDATE T set T.[Enabled] = 0 from M_Advertiser T
                //                                WHERE NOT EXISTS (SELECT 1 FROM {tmpTableName} S WHERE T.EntityCode = S.EntityCode)";

                string deleteTmpMEntityTableSql = $@"DROP TABLE {tmpTableName}";

                await _mAdvertiserRepository.Execute(updateSql);
                //await _mAdvertiserRepository.Execute(updateStatusSql);
                await _mAdvertiserRepository.Execute(deleteTmpMEntityTableSql);

                return true;
            }
            catch (Exception ex)
            {
                //todo 写日志
                return false;
            }
        }

        /// <summary>
        /// 广告商营销组织关系表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<bool> UpdateAdvertiserOrg(List<MAdvertiserOrgAddExtendDto> data)
        {
            try
            {
                string tmpTableName = $"Tmp_M_Advertiser_Org_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                string tmpCreateTableSql = $@"CREATE TABLE [{tmpTableName}] (
                                                [MarketOrg] char(8) NOT NULL,
                                                [BigAreaOrg] char(8) NOT NULL,
                                                [OfficeOrg] char(8) NOT NULL,
                                                [StationOrg] char(8) NOT NULL,
                                                [AdvertiserId] uniqueidentifier NOT NULL,
                                                [AdvertiserCode] nvarchar(10) NOT NULL,
                                                [Status] int NOT NULL
                                                )";
                await _mAdvertiserOrgRepository.Execute(tmpCreateTableSql);
                DataTable tmpTable = data.ToDataTable();
                tmpTable.TableName = tmpTableName;

                _dbContext.SqlBulkCopy(tmpTable);

                string updateIdSql = $@"update t set t.AdvertiserId = a.Id from {tmpTableName} t
                                            inner join M_Advertiser a on t.AdvertiserCode = a.AdvertiserCode;";

                string updateSql = $@"MERGE INTO M_Advertiser_Org AS T
                                            USING {tmpTableName} AS S
                                            ON S.MarketOrg = T.MarketOrg and S.BigAreaOrg = T.BigAreaOrg and S.OfficeOrg = T.OfficeOrg and S.StationOrg = T.StationOrg and S.AdvertiserId = T.AdvertiserId
                                            WHEN MATCHED THEN
                                                UPDATE SET T.Status = S.Status
                                            WHEN NOT MATCHED THEN
                                                INSERT (Id,MarketOrg,BigAreaOrg,OfficeOrg,StationOrg,AdvertiserId,Status)
                                                VALUES (NEWID(),S.MarketOrg,S.BigAreaOrg,S.OfficeOrg,S.StationOrg,S.AdvertiserId,S.Status);";

                //string updateStatusSql = $@"DELETE from M_Advertiser_Org T
                //                                WHERE NOT EXISTS (SELECT 1 FROM {tmpTableName} S WHERE S.MarketOrg = T.MarketOrg and S.BigAreaOrg = T.BigAreaOrg and S.OfficeOrg = T.OfficeOrg and S.StationOrg = T.StationOrg and S.AdvertiserId = T.AdvertiserId)";

                string deleteTmpMEntityTableSql = $@"DROP TABLE {tmpTableName}";

                await _mAdvertiserOrgRepository.Execute(updateIdSql);
                await _mAdvertiserOrgRepository.Execute(updateSql);
                //await _mAdvertiserOrgRepository.Execute(updateStatusSql);
                await _mAdvertiserOrgRepository.Execute(deleteTmpMEntityTableSql);

                return true;
            }
            catch (Exception ex)
            {
                //todo 写日志
                return false;
            }
        }

        /// <summary>
        /// 广告商银行账号表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<bool> UpdateAdvertiserAccount(List<MAdvertiserAccountAddExtendDto> data)
        {
            try
            {
                string tmpTableName = $"Tmp_M_AdvertiserAccount_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                string tmpCreateTableSql = $@"CREATE TABLE [{tmpTableName}] (
                                                [AdvertiserId] uniqueidentifier NOT NULL,
                                                [AdvertiserCode] nvarchar(10) NOT NULL,
                                                [BankAccount] nvarchar(35) NOT NULL,
                                                [BankActNm] nvarchar(18) NOT NULL,
                                                [BankNm] nvarchar(100) NOT NULL,
                                                [CustomerNm] nvarchar(100) NOT NULL,
                                                [CurrencyCD] nvarchar(10) NULL,
                                                [Status] int NOT NULL,
                                                [AccountTYP] nvarchar(5) NULL,
                                                [IsMainFLG] bit NULL,
                                                [BankInfoNUM] nvarchar(10) NULL,
                                                [RBKNUM] nvarchar(20) NULL,
                                                [IsValid] bit NULL,
                                                [AreaCode] nvarchar(10) NULL,
                                                [CommercialBankDocumentNUM] nvarchar(20) NULL,
                                                [ModelCode] nvarchar(10) NULL,
                                                [MainCode] nvarchar(10) NULL
                                                )";
                await _mAdvertiserAccountRepository.Execute(tmpCreateTableSql);
                DataTable tmpTable = data.ToDataTable();
                tmpTable.TableName = tmpTableName;

                _dbContext.SqlBulkCopy(tmpTable);

                string updateIdSql = $@"update t set t.AdvertiserId = a.Id from {tmpTableName} t
                                            inner join M_Advertiser a on t.AdvertiserCode = a.AdvertiserCode;";

                string updateSql = $@"MERGE INTO M_AdvertiserAccount AS T
                                            USING {tmpTableName} AS S
                                            ON S.AdvertiserCode = T.AdvertiserCode and S.BankAccount = T.BankAccount
                                            WHEN MATCHED THEN
                                                UPDATE SET T.AdvertiserId = S.AdvertiserId,T.BankActNm = S.BankActNm,T.BankNm = S.BankNm,T.CustomerNm = S.CustomerNm,T.CurrencyCD = S.CurrencyCD,
                                                           T.CurrencyCD = S.CurrencyCD,T.Status = S.Status,T.AccountTYP = S.AccountTYP,T.IsMainFLG = S.IsMainFLG,T.BankInfoNUM = S.BankInfoNUM,
                                                           T.RBKNUM = S.RBKNUM,T.IsValid = S.IsValid,T.AreaCode = S.AreaCode,T.CommercialBankDocumentNUM = S.CommercialBankDocumentNUM,
                                                           T.ModelCode = S.ModelCode,T.MainCode = S.MainCode
                                            WHEN NOT MATCHED THEN
                                                INSERT (Id,AdvertiserId,BankAccount,BankActNm,BankNm,CustomerNm,CurrencyCD,Status,AccountTYP,IsMainFLG,BankInfoNUM,RBKNUM,IsValid,AreaCode,CommercialBankDocumentNUM,ModelCode,MainCode)
                                                VALUES (NEWID(),S.AdvertiserId,S.BankAccount,S.BankActNm,S.BankNm,S.CustomerNm,S.CurrencyCD,S.Status,S.AccountTYP,S.IsMainFLG,S.BankInfoNUM,S.RBKNUM,S.IsValid,S.AreaCode,S.CommercialBankDocumentNUM,S.ModelCode,S.MainCode);";

                //string updateStatusSql = $@"DELETE from M_Advertiser_Org T
                //                                WHERE NOT EXISTS (SELECT 1 FROM {tmpTableName} S WHERE S.MarketOrg = T.MarketOrg and S.BigAreaOrg = T.BigAreaOrg and S.OfficeOrg = T.OfficeOrg and S.StationOrg = T.StationOrg and S.AdvertiserId = T.AdvertiserId)";

                string deleteTmpMEntityTableSql = $@"DROP TABLE {tmpTableName}";

                await _mAdvertiserOrgRepository.Execute(updateIdSql);
                await _mAdvertiserAccountRepository.Execute(updateSql);
                //await _mAdvertiserOrgRepository.Execute(updateStatusSql);
                await _mAdvertiserAccountRepository.Execute(deleteTmpMEntityTableSql);

                return true;
            }
            catch (Exception ex)
            {
                //todo 写日志
                return false;
            }
        }

        /// <summary>
        /// 广告商地点分配表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<bool> UpdateAdvertiserAccountAddress(List<MAdvertiserAccountAddressAddExtendDto> data)
        {
            try
            {
                string tmpTableName = $"Tmp_M_AdvertiserAccount_Address_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                string tmpCreateTableSql = $@"CREATE TABLE [{tmpTableName}] (
                                                [AdvertiserId] uniqueidentifier NOT NULL,
                                                [AdvertiserCode] nvarchar(10) NOT NULL,
                                                [ADDRNO] nvarchar(40) NOT NULL,
                                                [AdvertiserAccountId] uniqueidentifier NOT NULL,
                                                [AdvertiserAccount] nvarchar(35) NOT NULL,
                                                [ASSIGNSTAU] nvarchar(2) NOT NULL,
                                                [UUID] nvarchar(20) NULL,
                                                [STDATE] nvarchar(20) NULL,
                                                [ENDATE] nvarchar(20) NULL,
                                                [CODE] nvarchar(20) NULL,
                                                [MAINCD] nvarchar(20) NULL,
                                                [BUCODE] nvarchar(20) NULL
                                                )";
                await _mAdvertiserAccountRepository.Execute(tmpCreateTableSql);
                DataTable tmpTable = data.ToDataTable();
                tmpTable.TableName = tmpTableName;

                _dbContext.SqlBulkCopy(tmpTable);

                string updateIdSql = $@"update t set t.AdvertiserId = a.Id from {tmpTableName} t
                                            inner join M_Advertiser a on t.AdvertiserCode = a.AdvertiserCode;
                                        update t set t.AdvertiserAccountId = a.Id from {tmpTableName} t
                                            inner join M_AdvertiserAccount a on t.BankAccount = a.AdvertiserAccount;";

                string updateSql = $@"MERGE INTO M_AdvertiserAccount_Address AS T
                                            USING {tmpTableName} AS S
                                            ON S.AdvertiserId = T.AdvertiserId and S.AdvertiserAccountId = T.AdvertiserAccountId
                                            WHEN MATCHED THEN
                                                UPDATE SET T.ADDRNO = S.ADDRNO,T.ASSIGNSTAU = S.ASSIGNSTAU,T.UUID = S.UUID,T.STDATE = S.STDATE,T.ENDATE = S.ENDATE,T.CODE = S.CODE,T.MAINCD = S.MAINCD,T.BUCODE = S.BUCODE
                                            WHEN NOT MATCHED THEN
                                                INSERT (Id,AdvertiserId,ADDRNO,AdvertiserAccountId,ASSIGNSTAU,UUID,STDATE,ENDATE,CODE,MAINCD,BUCODE)
                                                VALUES (NEWID(),S.AdvertiserId,S.ADDRNO,S.AdvertiserAccountId,S.ASSIGNSTAU,S.UUID,S.STDATE,S.ENDATE,S.CODE,S.MAINCD,S.BUCODE);";

                //string updateStatusSql = $@"DELETE from M_Advertiser_Org T
                //                                WHERE NOT EXISTS (SELECT 1 FROM {tmpTableName} S WHERE S.MarketOrg = T.MarketOrg and S.BigAreaOrg = T.BigAreaOrg and S.OfficeOrg = T.OfficeOrg and S.StationOrg = T.StationOrg and S.AdvertiserId = T.AdvertiserId)";

                string deleteTmpMEntityTableSql = $@"DROP TABLE {tmpTableName}";

                await _mAdvertiserOrgRepository.Execute(updateIdSql);
                await _mAdvertiserAccountRepository.Execute(updateSql);
                //await _mAdvertiserOrgRepository.Execute(updateStatusSql);
                await _mAdvertiserAccountRepository.Execute(deleteTmpMEntityTableSql);

                return true;
            }
            catch (Exception ex)
            {
                //todo 写日志
                return false;
            }
        }

        /// <summary>
        /// 广告商地点表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<bool> UpdateAdvertiserAddress(List<MAdvertiserAddressAddExtendDto> data)
        {
            try
            {
                string tmpTableName = $"Tmp_M_AdvertiserAddress_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                string tmpCreateTableSql = $@"CREATE TABLE [{tmpTableName}] (
                                                [AdvertiserId] uniqueidentifier NOT NULL,
                                                [AdvertiserCode] nvarchar(10) NOT NULL,
                                                [ADDRNO] nvarchar(40) NOT NULL,
                                                [ADDRDESC] nvarchar(80) NULL,
                                                [ZDESC] nvarchar(80) NOT NULL,
                                                [MAINPLACE] nvarchar(20) NOT NULL,
                                                [ORGID] nvarchar(20) NULL,
                                                [ORGCODE] nvarchar(20) NULL,
                                                [ORGNAME] nvarchar(80) NULL,
                                                [LOCSTATU] nvarchar(2) NULL,
                                                [INVDATE] nvarchar(10) NULL,
                                                [FKTJ] nvarchar(40) NULL,
                                                [COA] nvarchar(80) NULL,
                                                [EXPACOUNT] nvarchar(80) NULL,
                                                [ADVPACOUNT] nvarchar(80) NULL,
                                                [FUACOUNT] nvarchar(80) NULL,
                                                [CODE] nvarchar(20) NULL,
                                                [MAINCD] nvarchar(20) NULL
                                                )";
                await _mAdvertiserAddressRepository.Execute(tmpCreateTableSql);
                DataTable tmpTable = data.ToDataTable();
                tmpTable.TableName = tmpTableName;

                _dbContext.SqlBulkCopy(tmpTable);

                string updateIdSql = $@"update t set t.AdvertiserId = a.Id from {tmpTableName} t
                                            inner join M_Advertiser a on t.AdvertiserCode = a.AdvertiserCode;";

                string updateSql = $@"MERGE INTO M_AdvertiserAddress AS T
                                            USING {tmpTableName} AS S
                                            ON S.AdvertiserId = T.AdvertiserId and S.ADDRNO = T.ADDRNO
                                            WHEN MATCHED THEN
                                                UPDATE SET T.ADDRDESC = S.ADDRDESC,T.ZDESC = S.ZDESC,T.MAINPLACE = S.MAINPLACE,T.ORGID = S.ORGID,T.ORGCODE = S.ORGCODE,T.ORGNAME = S.ORGNAME,T.LOCSTATU = S.LOCSTATU,T.INVDATE = S.INVDATE,
                                                           T.FKTJ = S.FKTJ,T.COA = S.COA,T.EXPACOUNT = S.EXPACOUNT,T.ADVPACOUNT = S.ADVPACOUNT,T.FUACOUNT = S.FUACOUNT,T.CODE = S.CODE,T.MAINCD = S.MAINCD
                                            WHEN NOT MATCHED THEN
                                                INSERT (Id,AdvertiserId,ADDRNO,ADDRDESC,ZDESC,MAINPLACE,ORGID,ORGCODE,ORGNAME,LOCSTATU,INVDATE,FKTJ,COA,EXPACOUNT,ADVPACOUNT,FUACOUNT,CODE,MAINCD)
                                                VALUES (NEWID(),S.AdvertiserId,S.ADDRNO,S.ADDRDESC,S.ZDESC,S.MAINPLACE,S.ORGID,S.ORGCODE,S.ORGNAME,S.LOCSTATU,S.INVDATE,S.FKTJ,S.COA,S.EXPACOUNT,S.ADVPACOUNT,S.FUACOUNT,S.CODE,S.MAINCD);";

                //string updateStatusSql = $@"DELETE from M_Advertiser_Org T
                //                                WHERE NOT EXISTS (SELECT 1 FROM {tmpTableName} S WHERE S.MarketOrg = T.MarketOrg and S.BigAreaOrg = T.BigAreaOrg and S.OfficeOrg = T.OfficeOrg and S.StationOrg = T.StationOrg and S.AdvertiserId = T.AdvertiserId)";

                string deleteTableSql = $@"DROP TABLE {tmpTableName}";

                await _mAdvertiserAddressRepository.Execute(updateIdSql);
                await _mAdvertiserAddressRepository.Execute(updateSql);
                //await _mAdvertiserOrgRepository.Execute(updateStatusSql);
                await _mAdvertiserAddressRepository.Execute(deleteTableSql);

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
