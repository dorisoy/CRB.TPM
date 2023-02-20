using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    /// 用于表示Sqlite数据库访问客户端
    /// </summary>
    internal class SQLiteClient : IClient
    {
        public SQLiteClient(DataBaseConfig config, string connectionString = "") : base(DataBaseType.Sqlite, config)
        {
            if (string.IsNullOrEmpty(config.Server))
            {
                config.Server = "db";
            }
            if (!config.Server.Contains(":") && !config.Server.StartsWith("/"))
            {
                config.Server = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, config.Server);
            }

            ConnectionString = string.IsNullOrEmpty(connectionString) ? config.Server : connectionString;
        }

        public override string ConnectionString { get; }

        #region protected method

        protected string GetFileName(string name, bool createDir)
        {
            if (name.StartsWith("/") || name.StartsWith(@"\"))
            {
                throw new Exception(@"sqlite database name cannot start with / or \");
            }
            var fileName = Path.Combine(ConnectionString, name);
            if (createDir)
            {
                var dir = Path.GetDirectoryName(fileName);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }
            return fileName;
        }

        protected override IDatabase CreateIDatabase(string name)
        {
            return new SQLiteDatabase(name, this);
        }

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="name">数据库名称</param>
        /// <param name="useGis">是否启用GIS（当前只支持Postgre）</param>
        /// <param name="ext">GIS扩展（当前只支持Postgre）</param>
        /// <returns></returns>
        public override IDatabase GetDatabase(string name, bool useGis = false, string ext = null)
        {
            var exists = DataBaseCache.TryGetValue(name, out var val);
            if (!exists)
            {
                lock (Locker.GetObject(name))
                {
                    if (!DataBaseCache.ContainsKey(name))
                    {
                        CreateDatabase(name);
                        DataBaseCache.TryAdd(name, CreateIDatabase(name));
                    }
                }
                val = DataBaseCache[name];
            }
            return val;
        }

        #endregion

        public override IDbConnection GetConn()
        {
            throw new NotImplementedException();
        }

        public override Task<IDbConnection> GetConnAsync()
        {
            throw new NotImplementedException();
        }

        public override void CreateDatabase(string name, bool useGis = false, string ext = null)
        {
            var fileName = GetFileName(name, true);
            if (!File.Exists(fileName))
            {
                SQLiteConnection.CreateFile(fileName);
            }
        }

        public override void DropDatabase(string name)
        {
            var fileName = GetFileName(name, false);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            DataBaseCache.TryRemove(name, out _);
        }

        public override bool ExistsDatabase(string name)
        {
            var fileName = GetFileName(name, false);
            return File.Exists(fileName);
        }

        public override IEnumerable<string> ShowDatabases()
        {
            return Directory.GetFiles(ConnectionString).Select(s => Path.GetFileName(s));
        }

        public override IEnumerable<string> ShowDatabasesExcludeSystem()
        {
            return ShowDatabases();
        }

        public override void Vacuum(string dbname)
        {
            GetDatabase(dbname).Vacuum();
        }

        public override string GetDatabaseScript(string name, bool useGis = false, string ext = null)
        {
            throw new NotImplementedException();
        }
    }
}
