using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Sharding
{

    /// <summary>
    /// 用于表示MySql数据库访问客户端
    /// </summary>
    internal class MySqlClient : IClient
    {
        public MySqlClient(DataBaseConfig config, string connectionString = "") : base(DataBaseType.MySql, config)
        {
            //Charset = "utf8";
            Charset = "utf8mb4";
            ConnectionString = string.IsNullOrEmpty(connectionString) ? ConnectionStringBuilder.BuilderMySql(config) : connectionString;
        }

        #region protected method

        protected override IDatabase CreateIDatabase(string name)
        {
            return new MySqlDatabase(name, this);
        }

        #endregion

        public override string ConnectionString { get; }


        public override IDbConnection GetConn()
        {
            var conn = new MySqlConnection(ConnectionString);
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
            var conn = new MySqlConnection(ConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    await conn.OpenAsync();
                }
                catch (Exception ex)
                {
                    await conn.DisposeAsync();
                    throw ex;
                }
            }
            return conn;
        }

        public override string GetDatabaseScript(string name, bool useGis = false, string ext = null)
        {
            return $"CREATE DATABASE IF NOT EXISTS `{name}` DEFAULT CHARACTER SET {Charset} COLLATE {Charset}_general_ci";
        }

        public override void CreateDatabase(string name, bool useGis = false, string ext = null)
        {
            Execute($"CREATE DATABASE IF NOT EXISTS `{name}` DEFAULT CHARACTER SET {Charset} COLLATE {Charset}_general_ci");
        }

        public override void DropDatabase(string name)
        {
            Execute($"DROP DATABASE IF EXISTS `{name}`");
            DataBaseCache.TryRemove(name, out _);
        }

        public override IEnumerable<string> ShowDatabases()
        {
            return Query<string>("SHOW DATABASES");
        }

        public override IEnumerable<string> ShowDatabasesExcludeSystem()
        {
            return ShowDatabases().Where(w => w != "mysql" && w != "information_schema" && w != "performance_schema" && w != "sys");
        }

        public override bool ExistsDatabase(string name)
        {
            return ExecuteScalar<int>($"select COUNT(1) from information_schema.schemata where schema_name='{name}'") > 0;
        }

        public override void Vacuum(string dbname)
        {
            throw new NotImplementedException();
        }

    }
}
