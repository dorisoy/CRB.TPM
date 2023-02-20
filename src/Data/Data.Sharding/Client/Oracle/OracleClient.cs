using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Dapper;

namespace CRB.TPM.Data.Sharding
{

    /// <summary>
    /// 用于表示Oracle数据库访问客户端
    /// </summary>
    internal class OracleClient : IClient
    {

        public OracleClient(DataBaseConfig config, string connectionString = "") : base(DataBaseType.Oracle, config)
        {
            if (config.Database_Size_Mb <= 0)
                config.Database_Size_Mb = 1;
            if (config.Database_SizeGrowth_Mb <= 0)
                config.Database_SizeGrowth_Mb = 1;
            if (!string.IsNullOrEmpty(config.Database_Path))
            {
                if (!Directory.Exists(config.Database_Path))
                {
                    Directory.CreateDirectory(config.Database_Path);
                }
            }
   
            ConnectionString = string.IsNullOrEmpty(connectionString) ? ConnectionStringBuilder.BuilderOracleSysdba(config) : connectionString;
        }

        public override string ConnectionString { get; }


        #region protected method

        protected override IDatabase CreateIDatabase(string name)
        {
            return new OracleDatabase(name, this);
        }

        #endregion

        public override string GetDatabaseScript(string name, bool useGis = false, string ext = null)
        {
            throw new NotImplementedException();
        }

        public override void CreateDatabase(string name, bool useGis = false, string ext = null)
        {
            var upName = name.ToUpper();
            string dbpath;
            if (!string.IsNullOrEmpty(Config.Database_Path))
            {
                dbpath = Path.Combine(Config.Database_Path, upName + ".DBF");
            }
            else
            {
                dbpath = upName + ".DBF";
            }

            var sql1 = $"create user {upName} identified by {Config.Password}";
            var sql2 = $"create tablespace {upName} datafile '{dbpath}' size {Config.Database_Size_Mb}m autoextend on next {Config.Database_SizeGrowth_Mb}m";
            var sql3 = $"alter user {upName} default tablespace {upName}";
            var sql4 = $"grant create session,create table,unlimited tablespace to {upName}";
            var sql5 = $"alter user {upName} account unlock";
            var sql6 = $"grant connect,resource,dba to {upName}";
            using (var conn = GetConn())
            {
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        conn.Execute(sql1, null, tran);
                        conn.Execute(sql2, null, tran);
                        conn.Execute(sql3, null, tran);
                        conn.Execute(sql4, null, tran);
                        conn.Execute(sql5, null, tran);
                        conn.Execute(sql6, null, tran);
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }

                }

            }
        }

        public override void DropDatabase(string name)
        {
            var sql = $@"DROP USER {name.ToUpper()} CASCADE";
            var sql2 = $"DROP TABLESPACE {name.ToUpper()} INCLUDING CONTENTS AND DATAFILES";
            Execute(sql);
            Execute(sql2);
            DataBaseCache.TryRemove(name, out _);
        }

        public override bool ExistsDatabase(string name)
        {
            return ExecuteScalar<long>($"SELECT COUNT(1) FROM dba_users WHERE USERNAME='{name.ToUpper()}'") > 0;
        }

        public override IDbConnection GetConn()
        {
            var conn = new OracleConnection(ConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    conn.Dispose();
                    throw ex;
                }
            }
            return conn;
        }

        public override async Task<IDbConnection> GetConnAsync()
        {
            var conn = new OracleConnection(ConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    await conn.OpenAsync();
                }
                catch (Exception ex)
                {
#if CORE
                    await conn.DisposeAsync();
#else
                    conn.Dispose();
#endif
                    throw ex;
                }
            }
            return conn;
        }

        public override IEnumerable<string> ShowDatabases()
        {
            return Query<string>("SELECT USERNAME FROM dba_users");
        }

        public override IEnumerable<string> ShowDatabasesExcludeSystem()
        {
            return Query<string>("SELECT USERNAME FROM dba_users WHERE DEFAULT_TABLESPACE NOT IN('SYSTEM','SYSAUX','USERS')");
        }

        public override void Vacuum(string dbname)
        {
            throw new NotImplementedException();
        }
    }
}
