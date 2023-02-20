using System.IO;
using System.Text;
using System.Data.Common;
using System;
using System.Data.OracleClient;
using System.Data.SQLite;
using Oracle.ManagedDataAccess.Client;
using MySqlConnector;
using Microsoft.Data.Sqlite;
using Npgsql;


namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    /// 数据库连接字符串构建器
    /// </summary>
    public class ConnectionStringBuilder
    {
        /// <summary>
        /// 构建MYSQL
        /// </summary>
        /// <param name="config"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public static string BuilderMySql(DataBaseConfig config, string databaseName = null)
        {
            var sb = new StringBuilder();
            if (string.IsNullOrEmpty(config.Server))
            {
                config.Server = "127.0.0.1";
            }
            sb.Append($"server={config.Server}");
            if (config.Port != 0) //3306
            {
                sb.Append($";port={config.Port}");
            }
            if (string.IsNullOrEmpty(config.UserId))
            {
                config.UserId = "root";
            }
            sb.Append($";uid={config.UserId}");
            if (!string.IsNullOrEmpty(config.Password))
            {
                sb.Append($";pwd={config.Password}");
            }
            if (!string.IsNullOrEmpty(databaseName))
            {
                sb.Append($";database={databaseName}");
            }
            if (config.MinPoolSize != 0)
            {
                sb.Append($";min pool size={config.MinPoolSize}");
            }
            if (config.MaxPoolSize != 0)
            {
                sb.Append($";max pool size={config.MaxPoolSize}");
            }
            if (config.TimeOut != 0)
            {
                sb.Append($";connect timeout={config.TimeOut}");
            }
            if (!string.IsNullOrEmpty(config.CharSet))
            {
                sb.Append($";charset={config.CharSet}");
            }
            if (!string.IsNullOrEmpty(config.OtherConfig))
            {
                sb.Append($";{config.OtherConfig}");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 构建MSSQL
        /// </summary>
        /// <param name="config"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public static string BuilderSqlServer(DataBaseConfig config, string databaseName = null)
        {
            var sb = new StringBuilder();
            if (string.IsNullOrEmpty(config.Server))
            {
                config.Server = ".";
            }
            sb.Append($"server={config.Server}");
            if (config.Port != 0) //1433
            {
                sb.Append($",{config.Port}");
            }
            if (string.IsNullOrEmpty(config.UserId))
            {
                config.UserId = "sa";
            }
            sb.Append($";uid={config.UserId}");

            if (!string.IsNullOrEmpty(config.Password))
            {
                sb.Append($";pwd={config.Password}");
            }
            if (!string.IsNullOrEmpty(databaseName))
            {
                sb.Append($";database={databaseName}");
            }
            if (config.MinPoolSize != 0)
            {
                sb.Append($";min pool size={config.MinPoolSize}");
            }
            if (config.MaxPoolSize != 0)
            {
                sb.Append($";max pool size={config.MaxPoolSize}");
            }
            if (config.TimeOut != 0)
            {
                sb.Append($";timeout={config.TimeOut}");
            }
            if (!string.IsNullOrEmpty(config.OtherConfig))
            {
                sb.Append($";{config.OtherConfig}");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 构建PostgreSQL
        /// </summary>
        /// <param name="config"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public static string BuilderPostgresql(DataBaseConfig config, string databaseName = null)
        {
            var sb = new StringBuilder();
            if (string.IsNullOrEmpty(config.Server))
            {
                config.Server = "127.0.0.1";
            }
            sb.Append($"server={config.Server}");
            if (config.Port != 0) //5432
            {
                sb.Append($";port={config.Port}");
            }
            if (string.IsNullOrEmpty(config.UserId))
            {
                config.UserId = "postgres";
            }
            sb.Append($";uid={config.UserId}");
            if (!string.IsNullOrEmpty(config.Password))
            {
                sb.Append($";pwd={config.Password}");
            }
            if (!string.IsNullOrEmpty(databaseName))
            {
                sb.Append($";database={databaseName}");
            }
            if (config.MinPoolSize != 0)
            {
                sb.Append($";minpoolsize={config.MinPoolSize}");
            }
            if (config.MaxPoolSize != 0)
            {
                sb.Append($";maxpoolsize={config.MaxPoolSize}");
            }
            if (config.TimeOut != 0)
            {
                sb.Append($";commandtimeout={config.TimeOut}");
            }
            if (!string.IsNullOrEmpty(config.CharSet))
            {
                sb.Append($";encoding={config.CharSet}");
            }
            if (!string.IsNullOrEmpty(config.OtherConfig))
            {
                sb.Append($";{config.OtherConfig}");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 构建Oracle
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static string BuilderOracleSysdba(DataBaseConfig config)
        {
            var sb = new StringBuilder();
            if (string.IsNullOrEmpty(config.Server))
            {
                config.Server = "127.0.0.1";
            }
            sb.Append($"data source={config.Server}");
            if (config.Port != 0) //1521
            {
                sb.Append($":{config.Port}");
            }
            if (!string.IsNullOrEmpty(config.Oracle_ServiceName))
            {
                sb.Append($"/{config.Oracle_ServiceName}");
            }
            sb.Append($";user id={config.Oracle_SysUserId}");
            if (!string.IsNullOrEmpty(config.Oracle_SysPassword))
            {
                sb.Append($";password={config.Oracle_SysPassword}");
            }
            if (config.MinPoolSize != 0)
            {
                sb.Append($";min pool size={config.MinPoolSize}");
            }
            if (config.MaxPoolSize != 0)
            {
                sb.Append($";max pool size={config.MaxPoolSize}");
            }
            if (config.TimeOut != 0)
            {
                sb.Append($";connect timeout={config.TimeOut}");
            }
            sb.Append(";dba privilege=sysdba");
            if (!string.IsNullOrEmpty(config.OtherConfig))
            {
                sb.Append($";{config.OtherConfig}");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 构建Oracle
        /// </summary>
        /// <param name="config"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string BuilderOracle(DataBaseConfig config, string userId = null)
        {
            var sb = new StringBuilder();
            if (string.IsNullOrEmpty(config.Server))
            {
                config.Server = "127.0.0.1";
            }
            sb.Append($"data source={config.Server}");
            if (config.Port != 0) //1521
            {
                sb.Append($":{config.Port}");
            }
            if (!string.IsNullOrEmpty(config.Oracle_ServiceName))
            {
                sb.Append($"/{config.Oracle_ServiceName}");
            }
            if (userId != null && userId.ToLower() != config.UserId)
            {
                config.UserId = userId;
            }
            sb.Append($";user id={config.UserId}");

            if (!string.IsNullOrEmpty(config.Password))
            {
                sb.Append($";password={config.Password}");
            }
            if (config.MinPoolSize != 0)
            {
                sb.Append($";min pool size={config.MinPoolSize}");
            }
            if (config.MaxPoolSize != 0)
            {
                sb.Append($";max pool size={config.MaxPoolSize}");
            }
            if (config.TimeOut != 0)
            {
                sb.Append($";connect timeout={config.TimeOut}");
            }
            if (!string.IsNullOrEmpty(config.OtherConfig))
            {
                sb.Append($";{config.OtherConfig}");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 构建ClickHouse
        /// </summary>
        /// <param name="config"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public static string BuilderClickHouse(DataBaseConfig config, string databaseName = null)
        {
            var sb = new StringBuilder();
            if (string.IsNullOrEmpty(config.Server))
            {
                config.Server = "127.0.0.1";
            }
            sb.Append($"Host={config.Server}");
            if (config.Port == 0) //9000
            {
                config.Port = 9000;
            }
            sb.Append($";Port={config.Port}");
            if (string.IsNullOrEmpty(config.UserId))
            {
                config.UserId = "default";
            }
            sb.Append($";User={config.UserId}");
            if (!string.IsNullOrEmpty(config.Password))
            {
                sb.Append($";Password={config.Password}");
            }
            if (string.IsNullOrEmpty(databaseName))
            {
                databaseName = "default";
            }
            sb.Append($";Database={databaseName}");
            sb.Append(";Compress=True");
            if (!string.IsNullOrEmpty(config.OtherConfig))
            {
                sb.Append($";{config.OtherConfig}");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 构建SQLite
        /// </summary>
        /// <param name="config"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public static string BuilderSQLite(DataBaseConfig config, string databaseName = null)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(config.Server))
            {
                sb.Append($"data source={Path.Combine(config.Server, databaseName)}");
            }
            if (config.SQLite_CacheSize != 0)
            {
                sb.Append($";Cache Size={config.SQLite_CacheSize}");
            }
            if (config.SQLite_PageSize != 0)
            {
                sb.Append($";Page Size={config.SQLite_PageSize}");
            }
            if (config.MinPoolSize != 0 || config.MaxPoolSize != 0)
            {
                sb.Append($";Pooling=True");
                if (config.MinPoolSize != 0)
                {
                    sb.Append($";Min Pool Size={config.MinPoolSize}");
                }
                if (config.MaxPoolSize != 0)
                {
                    sb.Append($";Max Pool Size={config.MaxPoolSize}");
                }

            }
            sb.Append($";Synchronous={config.SQLite_Synchronous}");
            if (!string.IsNullOrEmpty(config.OtherConfig))
            {
                sb.Append($";{config.OtherConfig}");
            }
            return sb.ToString();
        }


        /// <summary>
        /// 解析 SqlServer 连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static DataBaseConfig ParsingSqlServer(string connectionString)
        {
            try
            {
                var builder = new DbConnectionStringBuilder();
                builder.ConnectionString = connectionString;
                //builder.ConnectionString = "initial catalog=test;data source=localhost;password=123456;User id=root;MultipleActiveResultSets=True"
                string database = (string)builder["initial catalog"];
                string server = (string)builder["data source"];
                string userId = (string)builder["User id"];
                string password = (string)builder["Password"];
                return new DataBaseConfig { Server = server, Database = database, UserId = userId, Password = password, Port = 1433 };
            }
            catch (Exception)
            {
                return new();
            }
        }

        /// <summary>
        /// 解析 Oracle 连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static DataBaseConfig ParsingOracle(string connectionString)
        {
            try
            {
                var builder = new OracleConnectionStringBuilder();
                builder.ConnectionString = connectionString;
                //builder.ConnectionString = "Data Source=myOracleDB;User Id=myUsername;Password=myPassword;Proxy User Id=pUserId;Proxy Password=pPassword;";
                string database = builder.DataSource;
                string userId = builder.UserID;
                string password = builder.Password;
                return new DataBaseConfig { Server = "", Database = database, UserId = userId, Password = password };
            }
            catch (Exception)
            {
                return new();
            }
        }


        /// <summary>
        /// 解析 Npgsql 连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static DataBaseConfig ParsingNpgsql(string connectionString)
        {
            try
            {
                var builder = new Npgsql.NpgsqlConnectionStringBuilder();
                builder.ConnectionString = connectionString;
                //builder.ConnectionString = "User ID=root;Password=123456;Host=localhost;Port=5432;Database=myDataBase;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;";
                string server = builder.Host;
                string userId = builder.Username;
                string password = builder.Password;
                int port = builder.Port;
                return new DataBaseConfig { Server = server, UserId = userId, Password = password, Port = port };
            }
            catch (Exception)
            {
                return new();
            }
        }

        /// <summary>
        /// 解析 MySql 连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static DataBaseConfig ParsingMySql(string connectionString)
        {
            try
            {
                var builder = new MySqlConnectionStringBuilder();
                builder.ConnectionString = connectionString;

                //builder.ConnectionString = "server=localhost;user id=root;password=123;port=3306;persistsecurityinfo=True;database=db;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;";

                string database = builder.Database;
                string server = builder.Server;
                string userId = builder.UserID;
                string password = builder.Password;
                int port = (int)builder.Port;

                return new DataBaseConfig { Server = server, Database = database, UserId = userId, Password = password, Port = port };
            }
            catch (Exception)
            {
                return new();
            }
        }


        /// <summary>
        /// 解析 Sqlite 连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static DataBaseConfig ParsingSqlite(string connectionString)
        {
            try
            {
                var builder = new SqliteConnectionStringBuilder();
                builder.ConnectionString = connectionString;

                //builder.ConnectionString = "Data Source=c:\mydb.db;Version=3;UseUTF16Encoding=True;Password=myPassword;";
                string database = (string)builder["Data Source"];
                string password = (string)builder["password"];

                return new DataBaseConfig { Server = "", UserId = "", Database = database, Password = password };
            }
            catch (Exception)
            {
                return new();
            }
        }
    }
}
