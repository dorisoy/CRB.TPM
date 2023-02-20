using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace CRB.TPM.Data.Sharding
{

    /// <summary>
    /// 用于表示数据库访问客户端
    /// </summary>
    public abstract class IClient
    {
        public IClient(DataBaseType dbType, DataBaseConfig config)
        {
            DbType = dbType;
            Config = config;
        }

        #region Dapper方法封装

        public int Execute(string sql, object param = null, int? timeout = null)
        {
            using (var cnn = GetConn())
            {
                return cnn.Execute(sql, param, commandTimeout: timeout);
            }
        }

        public object ExecuteScalar(string sql, object param = null, int? timeout = null)
        {
            using (var cnn = GetConn())
            {
                return cnn.ExecuteScalar(sql, param, commandTimeout: timeout);
            }

        }

        public T ExecuteScalar<T>(string sql, object param = null, int? timeout = null)
        {
            using (var cnn = GetConn())
            {
                return cnn.ExecuteScalar<T>(sql, param, commandTimeout: timeout);
            }
        }

        public dynamic QueryFirstOrDefault(string sql, object param = null, int? timeout = null)
        {
            using (var cnn = GetConn())
            {
                return cnn.QueryFirstOrDefault(sql, param, commandTimeout: timeout);
            }
        }

        public T QueryFirstOrDefault<T>(string sql, object param = null, int? timeout = null)
        {
            using (var cnn = GetConn())
            {
                return cnn.QueryFirstOrDefault<T>(sql, param, commandTimeout: timeout);
            }
        }

        public IEnumerable<dynamic> Query(string sql, object param = null, int? timeout = null)
        {
            using (var cnn = GetConn())
            {
                return cnn.Query(sql, param, commandTimeout: timeout);
            }

        }

        public IEnumerable<T> Query<T>(string sql, object param = null, int? timeout = null)
        {
            using (var cnn = GetConn())
            {
                return cnn.Query<T>(sql, param, commandTimeout: timeout);
            }
        }

        public void QueryMultiple(string sql, object param = null, Action<SqlMapper.GridReader> onReader = null, int? timeout = null)
        {
            using (var conn = GetConn())
            {
                using (var reader = conn.QueryMultiple(sql, param, commandTimeout: timeout))
                {
                    onReader?.Invoke(reader);
                }
            }
        }

        public DataTable QueryDataTable(string sql, object param = null, int? timeout = null)
        {
            using (var cnn = GetConn())
            {
                return cnn.GetDataTable(sql, param, commandTimeout: timeout);
            }
        }

        public DataSet QueryDataSet(string sql, object param = null, int? timeout = null)
        {
            using (var cnn = GetConn())
            {
                return cnn.GetDataSet(sql, param, commandTimeout: timeout);
            }
        }

        #endregion

        #region Dapper异步方法封装

        public async Task<int> ExecuteAsync(string sql, object param = null, int? timeout = null)
        {
            using (var cnn = await GetConnAsync())
            {
                return await cnn.ExecuteAsync(sql, param, commandTimeout: timeout);
            }
        }

        public async Task<object> ExecuteScalarAsync(string sql, object param = null, int? timeout = null)
        {

            using (var cnn = await GetConnAsync())
            {
                return await cnn.ExecuteScalarAsync(sql, param, commandTimeout: timeout);
            }
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, object param = null, int? timeout = null)
        {

            using (var cnn = await GetConnAsync())
            {
                return await cnn.ExecuteScalarAsync<T>(sql, param, commandTimeout: timeout);
            }
        }

        public async Task<dynamic> QueryFirstOrDefaultAsync(string sql, object param = null, int? timeout = null)
        {

            using (var cnn = await GetConnAsync())
            {
                return await cnn.QueryFirstOrDefaultAsync(sql, param, commandTimeout: timeout);
            }

        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, int? timeout = null)
        {

            using (var cnn = await GetConnAsync())
            {
                return await cnn.QueryFirstOrDefaultAsync<T>(sql, param, commandTimeout: timeout);
            }
        }

        public async Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null, int? timeout = null)
        {

            using (var cnn = await GetConnAsync())
            {
                return await cnn.QueryAsync(sql, param, commandTimeout: timeout);
            }

        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, int? timeout = null)
        {

            using (var cnn = await GetConnAsync())
            {
                return await cnn.QueryAsync<T>(sql, param, commandTimeout: timeout);
            }
        }

        public async Task QueryMultipleAsync(string sql, object param = null, Action<SqlMapper.GridReader> onReader = null, int? timeout = null)
        {

            using (var conn = await GetConnAsync())
            {
                using (var reader = await conn.QueryMultipleAsync(sql, param, commandTimeout: timeout))
                {
                    onReader?.Invoke(reader);
                }
            }
        }

        public async Task<DataTable> QueryDataTableAsync(string sql, object param = null, int? timeout = null)
        {

            using (var cnn = await GetConnAsync())
            {
                return await cnn.GetDataTableAsync(sql, param, commandTimeout: timeout);
            }
        }

        public async Task<DataSet> QueryDataSetAsync(string sql, object param = null, int? timeout = null)
        {

            using (var cnn = await GetConnAsync())
            {
                return await cnn.GetDataSetAsync(sql, param, commandTimeout: timeout);
            }
        }

        #endregion

        #region 子类派生

        protected readonly LockManager Locker = new LockManager();

        protected readonly ConcurrentDictionary<string, IDatabase> DataBaseCache = new ConcurrentDictionary<string, IDatabase>();

        protected abstract IDatabase CreateIDatabase(string name);

        #endregion

        #region 公共方法

        public DataBaseType DbType { get; }

        public DataBaseConfig Config { get; }

        public string Charset { get; set; }

        /// <summary>
        /// 是否自动创建数据库
        /// </summary>
        public bool AutoCreateDatabase { get; set; } = true;

        /// <summary>
        /// 是否自动创建表
        /// </summary>
        public bool AutoCreateTable { get; set; }

        /// <summary>
        /// 自动对比表列
        /// </summary>
        public bool AutoCompareTableColumn { get; set; }

        /// <summary>
        /// 自动对比删除表列
        /// </summary>
        public bool AutoCompareTableColumnDelete { get; set; }


        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="name">数据库名称</param>
        /// <param name="useGis">是否启用GIS（当前只支持Postgre）</param>
        /// <param name="ext">GIS扩展（当前只支持Postgre）</param>
        /// <returns></returns>
        public virtual IDatabase GetDatabase(string name, bool useGis = false, string ext = null)
        {
            var exists = DataBaseCache.TryGetValue(name, out var val);
            if (!exists)
            {
                lock (Locker.GetObject(name))
                {
                    if (!DataBaseCache.ContainsKey(name))
                    {
                        if (AutoCreateDatabase)
                        {
                            if (!ExistsDatabase(name))
                            {
                                CreateDatabase(name, useGis, ext);
                            }
                        }
                        DataBaseCache.TryAdd(name, CreateIDatabase(name));
                    }
                }
                val = DataBaseCache[name];
            }
            return val;
        }

        public void ClearCache(string dbname = null)
        {
            if (!string.IsNullOrEmpty(dbname))
            {
                DataBaseCache.TryRemove(dbname, out _);
            }
            else
            {
                DataBaseCache.Clear();
            }
        }

        #endregion

        #region 抽象方法

        public abstract string ConnectionString { get; }

        public abstract IDbConnection GetConn();

        public abstract Task<IDbConnection> GetConnAsync();

        public abstract void CreateDatabase(string name, bool useGis = false, string ext = null);

        public abstract void DropDatabase(string name);

        public abstract IEnumerable<string> ShowDatabases();

        public abstract IEnumerable<string> ShowDatabasesExcludeSystem();

        public abstract bool ExistsDatabase(string name);

        public abstract void Vacuum(string dbname);

        public abstract string GetDatabaseScript(string name, bool useGis = false, string ext = null);

        #endregion

    }
}
