using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.BulkOperations;
using Z.Dapper.Plus;
using Dapper;

namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    /// 用于表示数据库基础提供
    /// </summary>
    public abstract class IDatabase
    {
        public IDatabase(string name, IClient client)
        {
            Name = name;
            Client = client;
            AutoCreateTable = client.AutoCreateTable;
            AutoCompareTableColumn = client.AutoCompareTableColumn;
            AutoCompareTableColumnDelete = client.AutoCompareTableColumnDelete;
        }

        #region Dapper 方法封装

        public int Execute(string sql, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                using (var cnn = GetConn())
                {
                    return cnn.Execute(sql, param, commandTimeout: timeout);
                }
            }
            var val = tran.GetVal(this);
            return val.Item1.Execute(sql, param, val.Item2, timeout);
        }

        public object ExecuteScalar(string sql, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                using (var cnn = GetConn())
                {
                    return cnn.ExecuteScalar(sql, param, commandTimeout: timeout);
                }
            }
            var val = tran.GetVal(this);
            return val.Item1.ExecuteScalar(sql, param, val.Item2, timeout);
        }

        public T ExecuteScalar<T>(string sql, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                using (var cnn = GetConn())
                {
                    return cnn.ExecuteScalar<T>(sql, param, commandTimeout: timeout);
                }
            }
            var val = tran.GetVal(this);
            return val.Item1.ExecuteScalar<T>(sql, param, val.Item2, timeout);
        }

        public dynamic QueryFirstOrDefault(string sql, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                using (var cnn = GetConn())
                {
                    return cnn.QueryFirstOrDefault(sql, param, commandTimeout: timeout);
                }
            }
            var val = tran.GetVal(this);
            return val.Item1.QueryFirstOrDefault(sql, param, val.Item2, timeout);
        }

        public T QueryFirstOrDefault<T>(string sql, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                using (var cnn = GetConn())
                {
                    return cnn.QueryFirstOrDefault<T>(sql, param, commandTimeout: timeout);
                }
            }
            var val = tran.GetVal(this);
            return val.Item1.QueryFirstOrDefault<T>(sql, param, val.Item2, timeout);
        }

        public IEnumerable<dynamic> Query(string sql, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                using (var cnn = GetConn())
                {
                    return cnn.Query(sql, param, commandTimeout: timeout);
                }
            }
            var val = tran.GetVal(this);
            return val.Item1.Query(sql, param, val.Item2, commandTimeout: timeout);
        }

        public IEnumerable<T> Query<T>(string sql, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                using (var cnn = GetConn())
                {
                    return cnn.Query<T>(sql, param, commandTimeout: timeout);
                }
            }
            var val = tran.GetVal(this);
            return val.Item1.Query<T>(sql, param, val.Item2, commandTimeout: timeout);
        }

        public void QueryMultiple(string sql, object param = null, Action<SqlMapper.GridReader> onReader = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                using (var conn = GetConn())
                {
                    using (var reader = conn.QueryMultiple(sql, param, commandTimeout: timeout))
                    {
                        onReader?.Invoke(reader);
                    }
                }
                return;
            }
            var val = tran.GetVal(this);
            using (var reader = val.Item1.QueryMultiple(sql, param, val.Item2, commandTimeout: timeout))
            {
                onReader?.Invoke(reader);
            }
        }

        public DataTable QueryDataTable(string sql, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                using (var cnn = GetConn())
                {
                    return cnn.GetDataTable(sql, param, commandTimeout: timeout);
                }
            }
            var val = tran.GetVal(this);
            return val.Item1.GetDataTable(sql, param, val.Item2, timeout);
        }

        public DataSet QueryDataSet(string sql, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                using (var cnn = GetConn())
                {
                    return cnn.GetDataSet(sql, param, commandTimeout: timeout);
                }
            }
            var val = tran.GetVal(this);
            return val.Item1.GetDataSet(sql, param, val.Item2, timeout);
        }

        #endregion

        #region Dapper 异步方法封装

        public async Task<int> ExecuteAsync(string sql, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                using (var cnn = await GetConnAsync())
                {
                    return await cnn.ExecuteAsync(sql, param, commandTimeout: timeout);
                }
            }
            var val = await tran.GetValAsync(this);
            return await val.Item1.ExecuteAsync(sql, param, val.Item2, timeout);
        }

        public async Task<object> ExecuteScalarAsync(string sql, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                using (var cnn = await GetConnAsync())
                {
                    return await cnn.ExecuteScalarAsync(sql, param, commandTimeout: timeout);
                }
            }
            var val = await tran.GetValAsync(this);
            return await val.Item1.ExecuteScalarAsync(sql, param, val.Item2, timeout);
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {

            if (tran == null)
            {
                using (var cnn = await GetConnAsync())
                {
                    return await cnn.ExecuteScalarAsync<T>(sql, param, commandTimeout: timeout);
                }
            }
            var val = await tran.GetValAsync(this);
            return await val.Item1.ExecuteScalarAsync<T>(sql, param, val.Item2, timeout);
        }

        public async Task<dynamic> QueryFirstOrDefaultAsync(string sql, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                using (var cnn = await GetConnAsync())
                {
                    return await cnn.QueryFirstOrDefaultAsync(sql, param, commandTimeout: timeout);
                }
            }
            var val = await tran.GetValAsync(this);
            return await val.Item1.QueryFirstOrDefaultAsync(sql, param, val.Item2, timeout);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                using (var cnn = await GetConnAsync())
                {
                    return await cnn.QueryFirstOrDefaultAsync<T>(sql, param, commandTimeout: timeout);
                }
            }
            var val = await tran.GetValAsync(this);
            return await val.Item1.QueryFirstOrDefaultAsync<T>(sql, param, val.Item2, timeout);
        }

        public async Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                using (var cnn = await GetConnAsync())
                {
                    return await cnn.QueryAsync(sql, param, commandTimeout: timeout);
                }
            }
            var val = await tran.GetValAsync(this);
            return await val.Item1.QueryAsync(sql, param, val.Item2, timeout);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                using (var cnn = await GetConnAsync())
                {
                    return await cnn.QueryAsync<T>(sql, param, commandTimeout: timeout);
                }
            }
            var val = await tran.GetValAsync(this);
            return await val.Item1.QueryAsync<T>(sql, param, val.Item2, timeout);
        }

        public async Task QueryMultipleAsync(string sql, object param = null, Action<SqlMapper.GridReader> onReader = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                using (var conn = await GetConnAsync())
                {
                    using (var reader = await conn.QueryMultipleAsync(sql, param, commandTimeout: timeout))
                    {
                        onReader?.Invoke(reader);
                    }
                }
                return;
            }
            var val = await tran.GetValAsync(this);
            using (var reader = await val.Item1.QueryMultipleAsync(sql, param, val.Item2, timeout))
            {
                onReader?.Invoke(reader);
            }
        }

        public async Task<DataTable> QueryDataTableAsync(string sql, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                using (var cnn = await GetConnAsync())
                {
                    return await cnn.GetDataTableAsync(sql, param, commandTimeout: timeout);
                }
            }
            var val = await tran.GetValAsync(this);
            return await val.Item1.GetDataTableAsync(sql, param, val.Item2, timeout);
        }

        public async Task<DataSet> QueryDataSetAsync(string sql, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                using (var cnn = await GetConnAsync())
                {
                    return await cnn.GetDataSetAsync(sql, param, commandTimeout: timeout);
                }
            }
            var val = await tran.GetValAsync(this);
            return await val.Item1.GetDataSetAsync(sql, param, val.Item2, timeout);
        }


        #endregion

        #region Z.Dapper.Plus Dapper 方法封装

        public void BulkInsert<T>(string tableName, T model, Action<BulkOperation> action, DistributedTransaction tran = null) where T : class
        {
            var key = DapperPlusUtils.Map<T>(tableName);
            if (tran == null)
            {
                using (var cnn = GetConn())
                {
                    cnn.UseBulkOptions(option =>
                    {
                        action(option);
                    }).BulkInsert(key, model);
                }
            }
            else
            {
                var val = tran.GetVal(this);
                val.Item2.UseBulkOptions(option =>
                {
                    action(option);

                }).BulkInsert(key, model);
            }
        }

        public void BulkInsert<T>(string tableName, IEnumerable<T> modelList, Action<BulkOperation> action, DistributedTransaction tran = null) where T : class
        {
            var key = DapperPlusUtils.Map<T>(tableName);
            if (tran == null)
            {
                using (var cnn = GetConn())
                {
                    using (var tr = cnn.BeginTransaction())
                    {
                        try
                        {
                            tr.UseBulkOptions(option =>
                            {
                                action(option);

                            }).BulkInsert(key, modelList);
                            tr.Commit();
                        }
                        catch (Exception ex)
                        {
                            tr.Rollback();
                            throw ex;
                        }
                    }

                }
            }
            else
            {
                var val = tran.GetVal(this);
                val.Item2.UseBulkOptions(option =>
                {
                    action(option);

                }).BulkInsert(key, modelList);
            }
        }

        public void BulkUpdate<T>(string tableName, T model, Action<BulkOperation> action, DistributedTransaction tran = null) where T : class
        {
            var key = DapperPlusUtils.Map<T>(tableName);
            if (tran == null)
            {
                using (var cnn = GetConn())
                {
                    cnn.UseBulkOptions(option =>
                    {
                        action(option);

                    }).BulkUpdate(key, model);
                }
            }
            else
            {
                var val = tran.GetVal(this);
                val.Item2.UseBulkOptions(option =>
                {
                    action(option);

                }).BulkUpdate(key, model);
            }
        }

        public void BulkUpdate<T>(string tableName, IEnumerable<T> modelList, Action<BulkOperation> action, DistributedTransaction tran = null) where T : class
        {
            var key = DapperPlusUtils.Map<T>(tableName);
            if (tran == null)
            {
                using (var cnn = GetConn())
                {
                    using (var tr = cnn.BeginTransaction())
                    {
                        try
                        {
                            tr.UseBulkOptions(option =>
                            {
                                action(option);

                            }).BulkUpdate(key, modelList);
                            tr.Commit();
                        }
                        catch (Exception ex)
                        {
                            tr.Rollback();
                            throw ex;
                        }
                    }

                }
            }
            else
            {
                var val = tran.GetVal(this);
                val.Item2.UseBulkOptions(option =>
                {
                    action(option);

                }).BulkUpdate(key, modelList);
            }
        }

        public void BulkDelete<T>(string tableName, T model, Action<BulkOperation> action, DistributedTransaction tran = null) where T : class
        {
            var key = DapperPlusUtils.Map<T>(tableName);
            if (tran == null)
            {
                using (var cnn = GetConn())
                {
                    cnn.UseBulkOptions(option =>
                    {
                        action(option);

                    }).BulkDelete(key, model);
                }
            }
            else
            {
                var val = tran.GetVal(this);
                val.Item2.UseBulkOptions(option =>
                {
                    action(option);

                }).BulkDelete(key, model);
            }
        }

        public void BulkDelete<T>(string tableName, IEnumerable<T> modelList, Action<BulkOperation> action, DistributedTransaction tran = null) where T : class
        {
            var key = DapperPlusUtils.Map<T>(tableName);
            if (tran == null)
            {
                using (var cnn = GetConn())
                {
                    using (var tr = cnn.BeginTransaction())
                    {
                        try
                        {
                            tr.UseBulkOptions(option =>
                            {
                                action(option);

                            }).BulkDelete(key, modelList);
                            tr.Commit();
                        }
                        catch (Exception ex)
                        {
                            tr.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            else
            {
                var val = tran.GetVal(this);
                val.Item2.UseBulkOptions(option =>
                {
                    action(option);

                }).BulkDelete(key, modelList);
            }
        }

        public void BulkMerge<T>(string tableName, T model, Action<BulkOperation> action, DistributedTransaction tran = null) where T : class
        {
            var key = DapperPlusUtils.Map<T>(tableName);
            if (tran == null)
            {
                using (var cnn = GetConn())
                {
                    cnn.UseBulkOptions(option =>
                    {
                        action(option);

                    }).BulkMerge(key, model);
                }
            }
            else
            {
                var val = tran.GetVal(this);
                val.Item2.UseBulkOptions(option =>
                {
                    action(option);

                }).BulkMerge(key, model);
            }
        }

        public void BulkMerge<T>(string tableName, IEnumerable<T> modelList, Action<BulkOperation> action, DistributedTransaction tran = null) where T : class
        {
            var key = DapperPlusUtils.Map<T>(tableName);
            if (tran == null)
            {
                using (var cnn = GetConn())
                {
                    using (var tr = cnn.BeginTransaction())
                    {
                        try
                        {
                            tr.UseBulkOptions(option =>
                            {
                                action(option);

                            }).BulkMerge(key, modelList);
                            tr.Commit();
                        }
                        catch (Exception ex)
                        {
                            tr.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            else
            {
                var val = tran.GetVal(this);
                val.Item2.UseBulkOptions(option =>
                {
                    action(option);

                }).BulkMerge(key, modelList);
            }
        }

        #endregion

        #region Z.Dapper.Plus Dapper 异步方法封装

        public Task BulkInsertAsync<T>(string tableName, T model, Action<BulkOperation> action, DistributedTransaction tran = null) where T : class
        {
            return Task.Run(() =>
            {
                BulkInsert(tableName, model, action, tran);
            });
        }

        public Task BulkInsertAsync<T>(string tableName, IEnumerable<T> modelList, Action<BulkOperation> action, DistributedTransaction tran = null) where T : class
        {
            return Task.Run(() =>
            {
                BulkInsert(tableName, modelList, action, tran);
            });
        }

        public Task BulkUpdateAsync<T>(string tableName, T model, Action<BulkOperation> action, DistributedTransaction tran = null) where T : class
        {
            return Task.Run(() =>
            {
                BulkUpdate(tableName, model, action, tran);
            });
        }

        public Task BulkUpdateAsync<T>(string tableName, IEnumerable<T> modelList, Action<BulkOperation> action, DistributedTransaction tran = null) where T : class
        {
            return Task.Run(() =>
            {
                BulkUpdate(tableName, modelList, action, tran);
            });
        }

        public Task BulkDeleteAsync<T>(string tableName, T model, Action<BulkOperation> action, DistributedTransaction tran = null) where T : class
        {
            return Task.Run(() =>
            {
                BulkDelete(tableName, model, action, tran);
            });
        }

        public Task BulkDeleteAsync<T>(string tableName, IEnumerable<T> modelList, Action<BulkOperation> action, DistributedTransaction tran = null) where T : class
        {
            return Task.Run(() =>
            {
                BulkDelete(tableName, modelList, action, tran);
            });
        }

        public Task BulkMergeAsync<T>(string tableName, T model, Action<BulkOperation> action, DistributedTransaction tran = null) where T : class
        {
            return Task.Run(() =>
            {
                BulkMerge(tableName, model, action, tran);
            });
        }

        public Task BulkMergeAsync<T>(string tableName, IEnumerable<T> modelList, Action<BulkOperation> action, DistributedTransaction tran = null) where T : class
        {
            return Task.Run(() =>
            {
                BulkMerge(tableName, modelList, action, tran);
            });
        }

        #endregion

        #region 子类派生

        protected readonly LockManager Locker = new LockManager();

        protected readonly ConcurrentDictionary<string, object> TableCache = new ConcurrentDictionary<string, object>();

        protected readonly ConcurrentDictionary<string, object> TableCache2 = new ConcurrentDictionary<string, object>();
        protected abstract ITable<T> CreateITable<T>(string name) where T : class;

        #endregion

        #region 公共方法

        public string Name { get; }

        public IClient Client { get; }

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

        public DataBaseType DbType
        {
            get
            {
                return Client.DbType;
            }
        }

        public void Using(Action<IDbConnection> action)
        {
            using (var conn = GetConn())
            {
                action(conn);
            }
        }

        public TResult Using<TResult>(Func<IDbConnection, TResult> func)
        {
            using (var conn = GetConn())
            {
                return func(conn);
            }
        }

        public void UsingTran(Action<IDbConnection, IDbTransaction> action)
        {
            using (var conn = GetConn())
            {
                using (var tran = conn.BeginTransaction())
                {
                    action(conn, tran);
                }
            }
        }

        public TResult UsingTran<TResult>(Func<IDbConnection, IDbTransaction, TResult> func)
        {
            using (var conn = GetConn())
            {
                using (var tran = conn.BeginTransaction())
                {
                    return func(conn, tran);
                }
            }
        }

        public virtual void CreateTable<T>(string name)
        {
            var script = GetTableScript<T>(name);
            Execute(script);
        }

        public ITable<T> GetTable<T>(string name) where T : class
        {
            var exists = TableCache.TryGetValue(name, out var val);
            if (!exists)
            {
                lock (Locker.GetObject(name))
                {
                    if (!TableCache.ContainsKey(name))
                    {
                        if (AutoCreateTable)
                        {
                            #region 创建表、对比表

                            if (!ExistsTable(name))
                            {
                                CreateTable<T>(name);
                            }
                            else if (AutoCompareTableColumn)
                            {
                                var dbColumns = GetTableColumnList(name);
                                var tableEntity = ClassToTableEntityUtils.Get<T>(Client.DbType);
                                var manager = GetTableManager(name);

                                foreach (var item in tableEntity.ColumnList)
                                {
                                    if (!dbColumns.Any(a => a.ToLower().Equals(item.Name.ToLower())))
                                    {
                                        manager.AddColumn(item.Name, item.CsType, item.Length, item.Comment, item.DbType);
                                    }
                                }

                                if (AutoCompareTableColumnDelete)
                                {
                                    foreach (var item in dbColumns)
                                    {
                                        if (!tableEntity.ColumnList.Any(a => a.Name.ToLower().Equals(item.ToLower())))
                                        {
                                            manager.DropColumn(item);
                                        }
                                    }
                                }
                            }

                            #endregion
                        }
                        TableCache.TryAdd(name, CreateITable<T>(name));
                    }
                }
                val = TableCache[name];
            }
            return (ITable<T>)val;
        }

        public ITable<T> GetTableExist<T>(string name) where T : class
        {
            var key = typeof(T).FullName + "@" + name;
            var exists = TableCache2.TryGetValue(key, out var val);
            if (!exists)
            {
                lock (Locker.GetObject(key))
                {
                    if (!TableCache2.ContainsKey(key))
                    {
                        TableCache2.TryAdd(key, CreateITable<T>(name));
                    }
                }
                val = TableCache2[key];
            }
            return (ITable<T>)val;
        }

        /// <summary>
        /// 生成C#表实体文件
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="tableList"></param>
        /// <param name="nameSpace"></param>
        /// <param name="suffix"></param>
        /// <param name="partialClass"></param>
        /// <param name="classNameToUpper"></param>
        /// <param name="columnToUpper"></param>
        public void GeneratorTableFile(string savePath, List<string> tableList = null, string nameSpace = "Model", string suffix = "", bool partialClass = false, bool classNameToUpper = false, bool columnToUpper = false)
        {
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            if (tableList == null || tableList.Count == 0)
            {
                tableList = GetTableList().ToList();
            }
            foreach (var name in tableList)
            {
                var entity = GetTableEntityFromDatabase(name, columnToUpper);
                string className;
                if (classNameToUpper)
                {
                    className = name.FirstCharToUpper() + suffix;
                }
                else
                {
                    className = name + suffix;
                }
                var sb = new StringBuilder();
                sb.Append("using System;");
                sb.AppendLine();
                sb.Append("using CRB.TPM.Data.Sharding;");
                sb.AppendLine();
                sb.AppendLine();
                sb.Append($"namespace {nameSpace}");
                sb.AppendLine();
                sb.Append("{");
                sb.AppendLine();
                if (entity.IndexList != null)
                {
                    var indexList = entity.IndexList.Where(w => w.Type != IndexType.PrimaryKey);
                    foreach (var item in indexList)
                    {
                        sb.Append($"    [Index(\"{item.Name}\", \"{item.Columns}\", {item.StringType})]");
                        sb.AppendLine();
                    }
                }
                if (!string.IsNullOrEmpty(entity.Comment))
                {
                    entity.Comment = entity.Comment.Replace("\r", "").Replace("\n", "");
                }
                sb.Append($"    [Table(\"{entity.PrimaryKey}\", {entity.IsIdentity.ToString().ToLower()}, \"{entity.Comment}\")]");
                sb.AppendLine();
                if (partialClass)
                {
                    sb.Append($"    public partial class {className}");
                    sb.AppendLine();
                }
                else
                {
                    sb.Append($"    public class {className}");
                    sb.AppendLine();
                }
                sb.Append("    {");
                sb.AppendLine();
                foreach (var item in entity.ColumnList)
                {
                    if (item.Length != 0 || !string.IsNullOrEmpty(item.Comment))
                    {
                        if (!string.IsNullOrEmpty(item.Comment))
                        {
                            item.Comment = item.Comment.Replace("\r", "").Replace("\n", "");
                        }
                        sb.Append($"        [Column({item.Length}, \"{item.Comment}\")]");
                        sb.AppendLine();
                    }
                    else
                    {
                        sb.Append("        public " + item.CsStringType + " " + item.Name + " { get; set; }");
                        sb.AppendLine();
                    }
                    if (item != entity.ColumnList.Last())
                    {
                        sb.AppendLine();
                    }
                }
                sb.Append("    }");
                sb.AppendLine();
                sb.Append("}");
                var path = Path.Combine(savePath, $"{className}.cs");
                File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
            }
        }

        /// <summary>
        /// 生成C#数据库上下文文件
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="nameSpace"></param>
        /// <param name="modelNameSpace"></param>
        /// <param name="classNameToUpper"></param>
        /// <param name="modelSuffix"></param>
        /// <param name="proSuffix"></param>
        /// <param name="staticClass"></param>
        public void GeneratorDbContextFile(string savePath, string nameSpace = "Model", string modelNameSpace = "Model", bool classNameToUpper = false, string modelSuffix = "", bool proSuffix = false, bool staticClass = true)
        {
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            var tableList = GetTableList().ToList();
            var sb = new StringBuilder();
            sb.Append("using CRB.TPM.Data.Sharding;");
            sb.AppendLine();
            if (nameSpace != modelNameSpace)
            {
                sb.Append($"using {modelNameSpace};");
                sb.AppendLine();
                sb.AppendLine();
            }
            else
            {
                sb.AppendLine();
            }
            sb.Append($"namespace {nameSpace}");
            sb.AppendLine();
            sb.Append("{");
            sb.AppendLine();
            sb.Append("    public class DbContext");
            sb.AppendLine();
            sb.Append("    {");
            sb.AppendLine();
            if (!staticClass)
            {
                sb.Append("        public readonly IDatabase Db;");
                sb.AppendLine();
                sb.Append("        public DbContext(IDatabase db)");
                sb.AppendLine();
                sb.Append("        {");
                sb.AppendLine();
                sb.Append("            Db = db;");
                sb.AppendLine();
                sb.Append("        }");
            }
            else
            {
                sb.Append("        public static readonly IClient Client;");
                sb.AppendLine();
                sb.Append($"        public static IDatabase Db => Client.GetDatabase(\"{Name}\");");
                sb.AppendLine();
                sb.Append("        static DbContext()");
                sb.AppendLine();
                sb.Append("        {");
                sb.AppendLine();
                sb.Append("            Client = ShardingFactory.CreateClient(DataBaseType.MySql, new DataBaseConfig { Server = \"127.0.0.1\", UserId = \"root\", Password = \"123\" });");
                sb.AppendLine();
                sb.Append("            //Client = ShardingFactory.CreateClient(DataBaseType.Postgresql, new DataBaseConfig { Server = \"127.0.0.1\", UserId = \"postgres\", Password = \"123\" });");
                sb.AppendLine();
                sb.Append("            //Client = ShardingFactory.CreateClient(DataBaseType.Sqlite, new DataBaseConfig { Server = \"db\" });");
                sb.AppendLine();
                sb.Append("            //Client = ShardingFactory.CreateClient(DataBaseType.SqlServer2008, new DataBaseConfig { Server = \".\", UserId = \"sa\", Password = \"123\" });");
                sb.AppendLine();
                sb.Append("            //Client = ShardingFactory.CreateClient(DataBaseType.ClickHouse, new DataBaseConfig { Server = \"127.0.0.1\", UserId = \"default\", Password = \"\" });");
                sb.AppendLine();
                sb.Append("            //Client = ShardingFactory.CreateClient(DataBaseType.Oracle, new DataBaseConfig { Server = \"127.0.0.1\", UserId = \"\", Password = \"\" });");
                sb.AppendLine();
                sb.Append("        }");
            }
            sb.AppendLine();
            sb.AppendLine();
            var st = " static";
            if (!staticClass)
            {
                st = "";
            }
            foreach (var name in tableList)
            {
                string className;
                if (classNameToUpper)
                {
                    className = name.FirstCharToUpper() + modelSuffix;
                }
                else
                {
                    className = name + modelSuffix;
                }

                string className2;
                if (proSuffix)
                {
                    className2 = className;
                }
                else
                {
                    className2 = name.FirstCharToUpper();
                }
                sb.Append($"        public{st} ITable<{className}> {className2} => Db.GetTable<{className}>(\"{name}\");");
                sb.AppendLine();
            }
            sb.Append("    }");
            sb.AppendLine();
            sb.Append("}");
            var path = Path.Combine(savePath, "DbContext.cs");
            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
        }

        public void ClearCache(string tablename = null)
        {
            if (!string.IsNullOrEmpty(tablename))
            {
                TableCache.TryRemove(tablename, out _);
            }
            else
            {
                TableCache.Clear();
            }

        }

        #endregion

        #region 抽象方法

        public abstract string ConnectionString { get; }

        public abstract IDbConnection GetConn();

        public abstract Task<IDbConnection> GetConnAsync();

        public abstract void DropTable(string name);

        public abstract void TruncateTable(string name);

        public abstract IEnumerable<string> GetTableList();

        public abstract IEnumerable<string> GetTableColumnList(string name);

        public abstract bool ExistsTable(string name);

        public abstract string GetTableScript<T>(string name);

        public abstract TableEntity GetTableEntityFromDatabase(string name, bool firstCharToUpper = false);

        public abstract ITableManager GetTableManager(string name);

        public abstract void OptimizeTable(string name, bool final = false, bool deduplicate = false);

        public abstract void OptimizeTable(string name, string partition, bool final = false, bool deduplicate = false);

        public abstract void Vacuum();

        #endregion

        #region IUnion

        /// <summary>
        /// 创建 Union 合并操作
        /// </summary>
        /// <returns></returns>
        public IUnion CreateUnion()
        {
            if (DbType == DataBaseType.MySql)
            {
                return new MySqlUnion(this);
            }
            else if (DbType == DataBaseType.Postgresql)
            {
                return new PostgreUnion(this);
            }
            else if (DbType == DataBaseType.Sqlite)
            {
                return new SQLiteUnion(this);
            }
            else if (DbType == DataBaseType.Oracle)
            {
                return new OracleUnion(this);
            }
            else
            {
                return new SqlServerUnion(this);
            }
        }

        #endregion

        #region 抽象方法-函数

        /// <summary>
        /// 创建SEQUENCE表
        /// </summary>
        public abstract void CreateSequence();
       
        /// <summary>
        /// 创建–取当前值的函数
        /// </summary>
        public abstract void CreateCurrvalFun();

        /// <summary>
        /// 创建–取下一个值的函数
        /// </summary>
        public abstract void CreateNextvalFun();

        /// <summary>
        /// 创建–更新当前值的函数
        /// </summary>
        public abstract void CreateSetvalFun();

        #endregion

    }
}
