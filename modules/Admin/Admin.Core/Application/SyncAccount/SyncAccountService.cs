using CRB.TPM.Config.Abstractions;
using CRB.TPM.Data.Core.Extensions;
using CRB.TPM.Data.Sharding;
using CRB.TPM.Mod.Admin.Core.Application.SyncAccount.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Mod.Admin.Core.Infrastructure;
using CRB.TPM.Utils.Map;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Application.SyncAccount
{
    public class SyncAccountService : ISyncAccountService
    {
        private readonly IMapper _mapper;
        private readonly AdminDbContext _dbContext;
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHandler _passwordHandler;
        private readonly IConfigProvider _configProvider;

        public SyncAccountService(IMapper mapper,
                                  AdminDbContext dbContext,
                                  IAccountRepository accountRepository,
                                  IPasswordHandler passwordHandler,
                                   IConfigProvider configProvider
                                   )
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _accountRepository = accountRepository;
            _passwordHandler = passwordHandler;
            _configProvider = configProvider;
        }

        public async Task<IResultModel> SyncData(List<ET_ORG> userData, List<ET_BPREL> userRelData)
        {
            List<AccountSyncDto> syncAccounts = new List<AccountSyncDto>();
            AccountSyncDto syncAccount = null;

            List<AccountRoleSyncDto> roleSyncAccounts = new List<AccountRoleSyncDto>();
            AccountRoleSyncDto accountRoleSync = null;


            foreach (var item in userData)
            {
                var account = syncAccounts.FirstOrDefault(x => x.UserBP == item.ZUSER_BP);
                if (account == null)
                {
                    syncAccount = new AccountSyncDto();
                    syncAccount.Id = Guid.NewGuid();
                    syncAccount.OrgId = Guid.Empty;
                    syncAccount.OrgCode = item.MINI_ORG;
                    syncAccount.UserBP = item.ZUSER_BP;
                    syncAccount.LDAPName = item.ZUSER_NAME;
                    syncAccount.Name = item.ZUSER_NAME_TXT;
                    syncAccount.CreatedBy = Guid.Empty;
                    syncAccount.Creator = "系统同步";
                    syncAccount.CreatedTime = DateTime.Now;

                    syncAccounts.Add(syncAccount);

                    account = syncAccount;
                }

                accountRoleSync = new AccountRoleSyncDto();
                accountRoleSync.Id = Guid.NewGuid();
                accountRoleSync.UserBP = item.ZUSER_BP;
                accountRoleSync.AccountId = account.Id;
                accountRoleSync.PostAttrCode = item.SHORT;
                accountRoleSync.RoleId = Guid.Empty;
                accountRoleSync.OrgId = Guid.Empty;
                if (!string.IsNullOrWhiteSpace(item.ZORG3))
                {
                    accountRoleSync.OrgCode = item.ZORG3;
                }
                else if (!string.IsNullOrWhiteSpace(item.ZORG2))
                {
                    accountRoleSync.OrgCode = item.ZORG2;
                }
                else if (!string.IsNullOrWhiteSpace(item.ZORG1))
                {
                    accountRoleSync.OrgCode = item.ZORG1;
                }

                var userRel = userRelData.Where(x => x.EMPLOYEE == accountRoleSync.UserBP && x.OBJID == item.ZPOSITION).OrderBy(x => x.STATUS).FirstOrDefault();
                if (userRel != null)
                {
                    accountRoleSync.Status = userRel.STATUS;
                }
                accountRoleSync.MinOrgId = Guid.Empty;
                accountRoleSync.MinOrgCode = item.MINI_ORG;

                roleSyncAccounts.Add(accountRoleSync);
            }


            var updateAccountResult = await UpdateSysAccount(syncAccounts);
            var updateAccountRoleResult = await UpdateAccountRole(roleSyncAccounts);
            return null;
        }

        private async Task<bool> UpdateSysAccount(List<AccountSyncDto> accountData)
        {
            try
            {
                string defaultPassword = _passwordHandler.Encrypt(_configProvider.Get<AdminConfig>().DefaultPassword);

                string tmpTableName = $"Tmp_SYS_Account_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                string tmpCreateTableSql = $@"CREATE TABLE [{tmpTableName}] (
                                                [Id] uniqueidentifier NOT NULL,
                                                [OrgId] uniqueidentifier NOT NULL,
                                                [OrgCode] nvarchar(10) NULL,
                                                [UserBP] varchar(10) NOT NULL,
                                                [LDAPName] nvarchar(50) NULL,
                                                [Name] nvarchar(250) NOT NULL,
                                                [CreatedBy] uniqueidentifier NOT NULL,
                                                [Creator] nvarchar(50) NOT NULL,
                                                [CreatedTime] datetime NOT NULL
                                                )";
                await _accountRepository.Execute(tmpCreateTableSql);
                DataTable tmpTable = accountData.ToDataTable();
                tmpTable.TableName = tmpTableName;

                _dbContext.SqlBulkCopy(tmpTable);


                string updateTmpTableSql = $@"update t set t.Id = a.Id from {tmpTableName} t
                                                inner join SYS_Account a on t.UserBP = a.UserBP;
                                              update t set t.OrgId = o.Id from {tmpTableName} t
                                                inner join M_Org o on t.OrgCode = o.OrgCode;";

                string updateSql = $@"MERGE INTO SYS_Account AS T
                                            USING {tmpTableName} AS S
                                            ON S.UserBP = T.UserBP 
                                            WHEN MATCHED THEN
                                                UPDATE SET T.LDAPName = S.LDAPName,T.Name = S.Name,T.ModifiedBy = S.CreatedBy,T.Modifier = S.Creator,T.ModifiedTime = S.CreatedTime
                                            WHEN NOT MATCHED THEN
                                                INSERT (Id,OrgId,Type,UserName,Password,Name,Status,UserBP,LDAPName,ActivatedTime,IsLock,Deleted,CreatedBy,Creator,CreatedTime)
                                                VALUES (S.Id,S.OrgId,1,(case when S.LDAPName is null then S.UserBP else S.LDAPName end),'{defaultPassword}',S.Name,1,S.UserBP,S.LDAPName,GETDATE(),0,0,S.CreatedBy,S.Creator,S.CreatedTime);";

                string deleteTableSql = $@"DROP TABLE {tmpTableName}";

                await _accountRepository.Execute(updateTmpTableSql);
                await _accountRepository.Execute(updateSql);
                await _accountRepository.Execute(deleteTableSql);

                return true;
            }
            catch (Exception ex)
            {
                //todo 写日志
                return false;
            }
        }

        private async Task<bool> UpdateAccountRole(List<AccountRoleSyncDto> accountRoleData)
        {
            try
            {
                string tmpTableName = $"Tmp_SYS_Account_Role_{DateTime.Now.ToString("yyyMMddhhmmss")}";
                string tmpCreateTableSql = $@"CREATE TABLE [{tmpTableName}] (
                                                [Id] uniqueidentifier NOT NULL,
                                                [UserBP] varchar(10) NOT NULL,
                                                [AccountId] uniqueidentifier NOT NULL,
                                                [PostAttrCode] nvarchar(50) NOT NULL,
                                                [RoleId] uniqueidentifier NOT NULL,
                                                [OrgId] uniqueidentifier NOT NULL,
                                                [OrgCode] nvarchar(10) NOT NULL,
                                                [MinOrgId] uniqueidentifier NOT NULL,
                                                [MinOrgCode] nvarchar(10) NOT NULL,
                                                [Status] varchar(20) NULL
                                                )";

                await _accountRepository.Execute(tmpCreateTableSql);
                DataTable tmpTable = accountRoleData.ToDataTable();
                tmpTable.TableName = tmpTableName;

                _dbContext.SqlBulkCopy(tmpTable);

                string updateTmpTableSql = $@"update t set t.AccountId = a.Id from {tmpTableName} t
                                            inner join SYS_Account a on t.UserBP = a.UserBP;
                                            update t set t.OrgId = o.Id from {tmpTableName} t
                                            inner join M_Org o on t.OrgCode = o.OrgCode;
                                            update t set t.MinOrgId = o.Id from {tmpTableName} t
                                            inner join M_Org o on t.MinOrgCode = o.OrgCode;
                                            update t set t.RoleId = r.Id from {tmpTableName} t
                                            inner join SYS_Role r on t.PostAttrCode = r.CRMCode and t.OrgId = r.OrgId;
                                            update t set t.Id = ar.Id from {tmpTableName} t
                                            inner join SYS_Account_Role ar on t.AccountId = ar.AccountId and t.RoleId = ar.RoleId";

                string updateSql = $@"insert into SYS_Account_Role(Id,AccountId,RoleId) 
                                        select Id,AccountId,RoleId from {tmpTableName} t
                                        where t.RoleId != '00000000-0000-0000-0000-000000000000' and
                                        not exists (select 1 from SYS_Account_Role ar where t.AccountId = ar.AccountId and t.RoleId = ar.RoleId) and t.Status = 'E0001';
                                      insert into SYS_Account_Role_Org(Id,Account_RoleId,OrgId)
                                        select NEWID(),Id,MinOrgId from {tmpTableName} t
                                        where t.RoleId != '00000000-0000-0000-0000-000000000000' 
                                              and exists (select 1 from SYS_Account_Role ar where t.AccountId = ar.AccountId and t.RoleId = ar.RoleId) 
                                              and not exists (select 1 from SYS_Account_Role_Org aro where t.Id = aro.Account_RoleId and t.MinOrgId = aro.OrgId) 
                                              and t.Status = 'E0001';";

                string delSql = $@"delete ar from SYS_Account_Role ar 
                                    where not exists (select 1 from {tmpTableName} t where ar.Id = t.Id and t.Status = 'E0001');
                                    delete aro from SYS_Account_Role_Org aro
                                    where not exists (select 1 from {tmpTableName} t where aro.Account_RoleId = t.Id and aro.OrgId = t.MinOrgId and t.Status = 'E0001')";

                //string updateStatusSql = $@"UPDATE T set T.Deleted = 1,T.DeletedBy = '00000000-0000-0000-0000-000000000000',T.Deleter = '系统同步',T.DeletedTime = getdate() from M_Org T
                //                                WHERE NOT EXISTS (SELECT 1 FROM {tmpTableName} S WHERE S.OrgCode = T.OrgCode)";

                string deleteTableSql = $@"DROP TABLE {tmpTableName}";

                await _accountRepository.Execute(updateTmpTableSql);
                await _accountRepository.Execute(updateSql);
                await _accountRepository.Execute(delSql);
                await _accountRepository.Execute(deleteTableSql);

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
