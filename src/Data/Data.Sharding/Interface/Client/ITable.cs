using FastMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
namespace CRB.TPM.Data.Sharding
{
    public abstract partial class ITable<T> where T : class
    {
        protected abstract string SqlInsert();

        protected abstract string SqlInsertIdentity();

        protected abstract string SqlUpdate(List<string> fields = null);

        protected abstract string SqlUpdateIgnore(List<string> fields);

        protected abstract string SqlUpdateByWhere(string where, List<string> fields = null);

        protected abstract string SqlUpdateByWhereIgnore(string where, List<string> fields);

        protected abstract string SqlDeleteById();

        protected abstract string SqlDeleteByIds();

        protected abstract string SqlDeleteByWhere(string where);

        protected abstract string SqlDeleteAll();

        protected abstract string SqlExists();

        protected abstract string SqlCount(string where = null);

        protected abstract string SqlMin(string field, string where = null);

        protected abstract string SqlMax(string field, string where = null);

        protected abstract string SqlSum(string field, string where = null);

        protected abstract string SqlAvg(string field, string where = null);

        protected abstract string SqlGetAll(string returnFields = null, string orderby = null);

        protected abstract string SqlGetById(string returnFields = null);

        protected abstract string SqlGetByIdForUpdate(string returnFields = null);

        protected abstract string SqlGetByIdForUpdateNoWait(string returnFields = null);

        protected abstract string SqlGetByIds(string returnFields = null);

        protected abstract string SqlGetByIdsForUpdate(string returnFields = null);

        protected abstract string SqlGetByIdsForUpdateNoWait(string returnFields = null);

        protected abstract string SqlGetByIdsWithField(string field, string returnFields = null);

        protected abstract string SqlGetByWhere(string where, string returnFields = null, string orderby = null, int limit = 0);

        protected abstract string SqlGetByWhereFirst(string where, string returnFields = null);

        protected abstract string SqlGetBySkipTake(int skip, int take, string where = null, string returnFields = null, string orderby = null);

        protected abstract string SqlGetByAscFirstPage(int pageSize, string and = null, string returnFields = null);

        protected abstract string SqlGetByAscPrevPage(int pageSize, string and = null, string returnFields = null);

        protected abstract string SqlGetByAscCurrentPage(int pageSize, string and = null, string returnFields = null);

        protected abstract string SqlGetByAscNextPage(int pageSize, string and = null, string returnFields = null);

        protected abstract string SqlGetByAscLastPage(int pageSize, string and = null, string returnFields = null);

        protected abstract string SqlGetByDescFirstPage(int pageSize, string and = null, string returnFields = null);

        protected abstract string SqlGetByDescPrevPage(int pageSize, string and = null, string returnFields = null);

        protected abstract string SqlGetByDescCurrentPage(int pageSize, string and = null, string returnFields = null);

        protected abstract string SqlGetByDescNextPage(int pageSize, string and = null, string returnFields = null);

        protected abstract string SqlGetByDescLastPage(int pageSize, string and = null, string returnFields = null);

        public abstract void SeqUpdate(string name = null);
    }

    public abstract partial class ITable<T> where T : class
    {
        public ITable(string name, IDatabase database, SqlFieldEntity sqlField)
        {
            Name = name;
            DataBase = database;
            SqlField = sqlField;
        }

        #region prototype

        public string Name { get; }

        public IDatabase DataBase { get; }

        public DataBaseType DbType
        {
            get
            {
                return DataBase.Client.DbType;
            }
        }

        public SqlFieldEntity SqlField { get; }

        #endregion

        #region insert

        public void Insert(T model, DistributedTransaction tran = null, int? timeout = null)
        {
            var sql = SqlInsert();
            if (SqlField.IsIdentity)
            {
                var accessor = TypeAccessor.Create(typeof(T));
                if (SqlField.PrimaryKeyType == typeof(int))
                {
                    var id = DataBase.ExecuteScalar<int>(sql, model, tran, timeout);
                    accessor[model, SqlField.PrimaryKey] = id;
                }
                else
                {
                    var id = DataBase.ExecuteScalar<long>(sql, model, tran, timeout);
                    accessor[model, SqlField.PrimaryKey] = id;
                }
            }
            else
            {
                DataBase.Execute(sql, model, tran, timeout);
            }
        }

        public void InsertMany(IEnumerable<T> list, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                tran = new DistributedTransaction();
                try
                {
                    foreach (var item in list)
                    {
                        Insert(item, tran, timeout);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            else
            {
                foreach (var item in list)
                {
                    Insert(item, tran, timeout);
                }
            }
        }

        public async Task InsertAsync(T model, DistributedTransaction tran = null, int? timeout = null)
        {
            var sql = SqlInsert();
            if (SqlField.IsIdentity)
            {
                var accessor = TypeAccessor.Create(typeof(T));
                if (SqlField.PrimaryKeyType == typeof(int))
                {
                    var id = await DataBase.ExecuteScalarAsync<int>(sql, model, tran, timeout);
                    accessor[model, SqlField.PrimaryKey] = id;
                }
                else
                {
                    var id = await DataBase.ExecuteScalarAsync<long>(sql, model, tran, timeout);
                    accessor[model, SqlField.PrimaryKey] = id;
                }
            }
            else
            {
                await DataBase.ExecuteAsync(sql, model, tran, timeout);
            }
        }

        public async Task InsertManyAsync(IEnumerable<T> list, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                tran = new DistributedTransaction();
                try
                {
                    foreach (var item in list)
                    {
                        await InsertAsync(item, tran, timeout);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            else
            {
                foreach (var item in list)
                {
                    await InsertAsync(item, tran, timeout);
                }
            }
        }

        public void InsertIdentity(T model, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.Execute(SqlInsertIdentity(), model, tran, timeout);
        }

        public void InsertIdentityMany(IEnumerable<T> list, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                tran = new DistributedTransaction();
                try
                {
                    foreach (var item in list)
                    {
                        InsertIdentity(item, tran, timeout);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            else
            {
                foreach (var item in list)
                {
                    InsertIdentity(item, tran, timeout);
                }
            }
        }

        public async Task InsertIdentityAsync(T model, DistributedTransaction tran = null, int? timeout = null)
        {
            await DataBase.ExecuteAsync(SqlInsertIdentity(), model, tran, timeout);
        }

        public async Task InsertIdentityManyAsync(IEnumerable<T> list, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                tran = new DistributedTransaction();
                try
                {
                    foreach (var item in list)
                    {
                        await InsertIdentityAsync(item, tran, timeout);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            else
            {
                foreach (var item in list)
                {
                    await InsertIdentityAsync(item, tran, timeout);
                }
            }
        }

        public virtual void Insert(T model, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkInsert(Name, model, opt =>
            {
                opt.IgnoreOnInsertNames = SqlField.AllFieldExceptKeyList.Except(fields).ToList();
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task InsertAsync(T model, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkInsertAsync(Name, model, opt =>
            {
                opt.IgnoreOnInsertNames = SqlField.AllFieldExceptKeyList.Except(fields).ToList();
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual void InsertIgnore(T model, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkInsert(Name, model, opt =>
            {
                opt.IgnoreOnInsertNames = fields;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task InsertIgnoreAsync(T model, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkInsertAsync(Name, model, opt =>
            {
                opt.IgnoreOnInsertNames = fields;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual void Insert(IEnumerable<T> modelList, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkInsert(Name, modelList, opt =>
            {
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }

            }, tran);
        }

        public virtual Task InsertAsync(IEnumerable<T> modelList, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkInsertAsync(Name, modelList, opt =>
            {
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }

            }, tran);
        }

        public virtual void Insert(IEnumerable<T> modelList, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkInsert(Name, modelList, opt =>
            {
                opt.IgnoreOnInsertNames = SqlField.AllFieldExceptKeyList.Except(fields).ToList();
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task InsertAsync(IEnumerable<T> modelList, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkInsertAsync(Name, modelList, opt =>
            {
                opt.IgnoreOnInsertNames = SqlField.AllFieldExceptKeyList.Except(fields).ToList();
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual void InsertIgnore(IEnumerable<T> modelList, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkInsert(Name, modelList, opt =>
            {
                opt.IgnoreOnInsertNames = fields;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task InsertIgnoreAsync(IEnumerable<T> modelList, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkInsertAsync(Name, modelList, opt =>
            {
                opt.IgnoreOnInsertNames = fields;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual void InsertIfNoExists(T model, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkInsert(Name, model, opt =>
            {
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task InsertIfNoExistsAsync(T model, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkInsertAsync(Name, model, opt =>
            {
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual void InsertIfNoExists(T model, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkInsert(Name, model, opt =>
            {
                opt.IgnoreOnInsertNames = SqlField.AllFieldExceptKeyList.Except(fields).ToList();
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task InsertIfNoExistsAsync(T model, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkInsertAsync(Name, model, opt =>
            {
                opt.IgnoreOnInsertNames = SqlField.AllFieldExceptKeyList.Except(fields).ToList();
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual void InsertIfNoExistsIgnore(T model, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkInsert(Name, model, opt =>
            {
                opt.IgnoreOnInsertNames = fields;
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task InsertIfNoExistsIgnoreAsync(T model, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkInsertAsync(Name, model, opt =>
            {
                opt.IgnoreOnInsertNames = fields;
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual void InsertIfNoExists(IEnumerable<T> modelList, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkInsert(Name, modelList, opt =>
            {
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task InsertIfNoExistsAsync(IEnumerable<T> modelList, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkInsertAsync(Name, modelList, opt =>
            {
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual void InsertIfNoExists(IEnumerable<T> modelList, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkInsert(Name, modelList, opt =>
            {
                opt.IgnoreOnInsertNames = SqlField.AllFieldExceptKeyList.Except(fields).ToList();
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task InsertIfNoExistsAsync(IEnumerable<T> modelList, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkInsertAsync(Name, modelList, opt =>
            {
                opt.IgnoreOnInsertNames = SqlField.AllFieldExceptKeyList.Except(fields).ToList();
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual void InsertIfNoExistsIgnore(IEnumerable<T> modelList, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkInsert(Name, modelList, opt =>
            {
                opt.IgnoreOnInsertNames = fields;
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task InsertIfNoExistsIgnoreAsync(IEnumerable<T> modelList, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkInsertAsync(Name, modelList, opt =>
            {
                opt.IgnoreOnInsertNames = fields;
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual void InsertIdentity(T model, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkInsert(Name, model, opt =>
            {
                opt.IgnoreOnInsertNames = SqlField.AllFieldExceptKeyList.Except(fields).ToList();
                opt.InsertKeepIdentity = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task InsertIdentityAsync(T model, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkInsertAsync(Name, model, opt =>
            {
                opt.IgnoreOnInsertNames = SqlField.AllFieldExceptKeyList.Except(fields).ToList();
                opt.InsertKeepIdentity = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual void InsertIdentityIgnore(T model, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkInsert(Name, model, opt =>
            {
                opt.IgnoreOnInsertNames = fields;
                opt.InsertKeepIdentity = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task InsertIdentityIgnoreAsync(T model, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkInsertAsync(Name, model, opt =>
            {
                opt.IgnoreOnInsertNames = fields;
                opt.InsertKeepIdentity = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual void InsertIdentity(IEnumerable<T> modelList, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkInsert(Name, modelList, opt =>
            {
                opt.InsertKeepIdentity = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task InsertIdentityAsync(IEnumerable<T> modelList, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkInsertAsync(Name, modelList, opt =>
            {
                opt.InsertKeepIdentity = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual void InsertIdentity(IEnumerable<T> modelList, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkInsert(Name, modelList, opt =>
            {
                opt.IgnoreOnInsertNames = SqlField.AllFieldExceptKeyList.Except(fields).ToList();
                opt.InsertKeepIdentity = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task InsertIdentityAsync(IEnumerable<T> modelList, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkInsertAsync(Name, modelList, opt =>
            {
                opt.IgnoreOnInsertNames = SqlField.AllFieldExceptKeyList.Except(fields).ToList();
                opt.InsertKeepIdentity = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual void InsertIdentityIgnore(IEnumerable<T> modelList, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkInsert(Name, modelList, opt =>
            {
                opt.IgnoreOnInsertNames = fields;
                opt.InsertKeepIdentity = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task InsertIdentityIgnoreAsync(IEnumerable<T> modelList, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkInsertAsync(Name, modelList, opt =>
            {
                opt.IgnoreOnInsertNames = fields;
                opt.InsertKeepIdentity = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual void InsertIdentityIfNoExists(T model, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkInsert(Name, model, opt =>
            {
                opt.InsertKeepIdentity = true;
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task InsertIdentityIfNoExistsAsync(T model, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkInsertAsync(Name, model, opt =>
            {
                opt.InsertKeepIdentity = true;
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual void InsertIdentityIfNoExists(T model, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkInsert(Name, model, opt =>
            {
                opt.IgnoreOnInsertNames = SqlField.AllFieldExceptKeyList.Except(fields).ToList();
                opt.InsertKeepIdentity = true;
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task InsertIdentityIfNoExistsAsync(T model, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkInsertAsync(Name, model, opt =>
            {
                opt.IgnoreOnInsertNames = SqlField.AllFieldExceptKeyList.Except(fields).ToList();
                opt.InsertKeepIdentity = true;
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual void InsertIdentityIfNoExistsIgnore(T model, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkInsert(Name, model, opt =>
            {
                opt.IgnoreOnInsertNames = fields;
                opt.InsertKeepIdentity = true;
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task InsertIdentityIfNoExistsIgnoreAsync(T model, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkInsertAsync(Name, model, opt =>
            {
                opt.IgnoreOnInsertNames = fields;
                opt.InsertKeepIdentity = true;
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual void InsertIdentityIfNoExists(IEnumerable<T> modelList, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkInsert(Name, modelList, opt =>
            {
                opt.InsertKeepIdentity = true;
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task InsertIdentityIfNoExistsAsync(IEnumerable<T> modelList, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkInsertAsync(Name, modelList, opt =>
            {
                opt.InsertKeepIdentity = true;
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual void InsertIdentityIfNoExists(IEnumerable<T> modelList, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkInsert(Name, modelList, opt =>
            {
                opt.IgnoreOnInsertNames = SqlField.AllFieldExceptKeyList.Except(fields).ToList();
                opt.InsertKeepIdentity = true;
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task InsertIdentityIfNoExistsAsync(IEnumerable<T> modelList, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkInsertAsync(Name, modelList, opt =>
            {
                opt.IgnoreOnInsertNames = SqlField.AllFieldExceptKeyList.Except(fields).ToList();
                opt.InsertKeepIdentity = true;
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual void InsertIdentityIfNoExistsIgnore(IEnumerable<T> modelList, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkInsert(Name, modelList, opt =>
            {
                opt.IgnoreOnInsertNames = fields;
                opt.InsertKeepIdentity = true;
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task InsertIdentityIfNoExistsIgnoreAsync(IEnumerable<T> modelList, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkInsertAsync(Name, modelList, opt =>
            {
                opt.IgnoreOnInsertNames = fields;
                opt.InsertKeepIdentity = true;
                opt.InsertIfNotExists = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fields"></param>
        /// <param name="tran"></param>
        /// <param name="timeout"></param>
        public virtual void Merge(T model, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkMerge(Name, model, opt =>
            {
                if (fields != null)
                {
                    var ignoreFileds = SqlField.AllFieldExceptKeyList.Except(fields).ToList();
                    opt.IgnoreOnMergeUpdateNames = ignoreFileds;
                }
                opt.MergeKeepIdentity = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        /// <summary>
        /// 异步合并
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fields"></param>
        /// <param name="tran"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public virtual Task MergeAsync(T model, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkMergeAsync(Name, model, opt =>
            {
                if (fields != null)
                {
                    var ignoreFileds = SqlField.AllFieldExceptKeyList.Except(fields).ToList();
                    opt.IgnoreOnMergeUpdateNames = ignoreFileds;
                }
                opt.MergeKeepIdentity = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="modelList"></param>
        /// <param name="fields"></param>
        /// <param name="tran"></param>
        /// <param name="timeout"></param>
        public virtual void Merge(IEnumerable<T> modelList, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkMerge(Name, modelList, opt =>
            {
                if (fields != null)
                {
                    var ignoreFileds = SqlField.AllFieldExceptKeyList.Except(fields).ToList();
                    opt.IgnoreOnMergeUpdateNames = ignoreFileds;
                }
                opt.MergeKeepIdentity = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        /// <summary>
        /// 异步合并
        /// </summary>
        /// <param name="modelList"></param>
        /// <param name="fields"></param>
        /// <param name="tran"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public virtual Task MergeAsync(IEnumerable<T> modelList, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkMergeAsync(Name, modelList, opt =>
            {
                if (fields != null)
                {
                    var ignoreFileds = SqlField.AllFieldExceptKeyList.Except(fields).ToList();
                    opt.IgnoreOnMergeUpdateNames = ignoreFileds;
                }
                opt.MergeKeepIdentity = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        /// <summary>
        /// 合并忽略
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fields"></param>
        /// <param name="tran"></param>
        /// <param name="timeout"></param>
        public virtual void MergeIgnore(T model, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkMerge(Name, model, opt =>
            {
                if (fields != null)
                {
                    opt.IgnoreOnMergeUpdateNames = fields;
                }
                opt.MergeKeepIdentity = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task MergeIgnoreAsync(T model, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkMergeAsync(Name, model, opt =>
            {
                if (fields != null)
                {
                    opt.IgnoreOnMergeUpdateNames = fields;
                }
                opt.MergeKeepIdentity = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual void MergeIgnore(IEnumerable<T> modelList, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkMerge(Name, modelList, opt =>
            {
                if (fields != null)
                {
                    opt.IgnoreOnMergeUpdateNames = fields;
                }
                opt.MergeKeepIdentity = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task MergeIgnoreAsync(IEnumerable<T> modelList, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkMergeAsync(Name, modelList, opt =>
            {
                if (fields != null)
                {
                    opt.IgnoreOnMergeUpdateNames = fields;
                }
                opt.MergeKeepIdentity = true;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        #region InsertOrUpdate com method

        private bool InitInsertOrUpdateModel(T model, IdType t)
        {
            var accessor = TypeAccessor.Create(typeof(T));
            var id = accessor[model, SqlField.PrimaryKey];
            bool insert = true;
            if (id is int val)
            {
                if (val > 0)
                {
                    insert = false;
                }
            }
            else if (id is long val2)
            {
                if (val2 > 0)
                {
                    insert = false;
                }
                else
                {
                    if (!SqlField.IsIdentity)
                    {
                        if (t == IdType.SnowId)
                        {
                            accessor[model, SqlField.PrimaryKey] = ShardingFactory.NextSnowId();
                        }
                        else
                        {
                            accessor[model, SqlField.PrimaryKey] = ShardingFactory.NextLongId();
                        }
                    }
                }
            }
            else if (id is string val3)
            {
                if (!string.IsNullOrEmpty(val3))
                {
                    insert = false;
                }
                else
                {
                    if (t == IdType.ObjectId)
                    {
                        accessor[model, SqlField.PrimaryKey] = ShardingFactory.NextObjectId();
                    }
                    else if (t == IdType.SnowId)
                    {
                        accessor[model, SqlField.PrimaryKey] = ShardingFactory.NextSnowIdAsString();
                    }
                    else
                    {
                        accessor[model, SqlField.PrimaryKey] = ShardingFactory.NextLongIdAsString();
                    }

                }
            }
            else if (id is decimal val4)
            {
                if (val4 > 0)
                {
                    insert = false;
                }
            }
            return insert;
        }

        #endregion

        public void InsertOrUpdate(T model, List<string> updateFields = null, DistributedTransaction tran = null, int? timeout = null, IdType t = IdType.ObjectId)
        {
            var insert = InitInsertOrUpdateModel(model, t);
            if (insert)
            {
                Insert(model, tran, timeout);
            }
            else
            {
                Update(model, updateFields, tran, timeout);
            }

        }

        public async Task InsertOrUpdateAsync(T model, List<string> updateFields = null, DistributedTransaction tran = null, int? timeout = null, IdType t = IdType.ObjectId)
        {
            var insert = InitInsertOrUpdateModel(model, t);
            if (insert)
            {
                await InsertAsync(model, tran, timeout);
            }
            else
            {
                await UpdateAsync(model, updateFields, tran, timeout);
            }

        }

        public void InsertOrUpdateIgnore(T model, List<string> ignoreFields, DistributedTransaction tran = null, int? timeout = null, IdType t = IdType.ObjectId)
        {
            var insert = InitInsertOrUpdateModel(model, t);
            if (insert)
            {
                Insert(model, tran, timeout);
            }
            else
            {
                UpdateIgnore(model, ignoreFields, tran, timeout);
            }

        }

        public async Task InsertOrUpdateIgnoreAsync(T model, List<string> ignoreFields, DistributedTransaction tran = null, int? timeout = null, IdType t = IdType.ObjectId)
        {
            var insert = InitInsertOrUpdateModel(model, t);
            if (insert)
            {
                await InsertAsync(model, tran, timeout);
            }
            else
            {
                await UpdateIgnoreAsync(model, ignoreFields, tran, timeout);
            }

        }

        #endregion

        #region update

        public int Update(T model, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Execute(SqlUpdate(fields), model, tran, timeout);
        }

        public void UpdateMany(IEnumerable<T> list, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                tran = new DistributedTransaction();
                try
                {
                    foreach (var item in list)
                    {
                        Update(item, fields, tran, timeout);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            else
            {
                foreach (var item in list)
                {
                    Update(item, fields, tran, timeout);
                }
            }
        }

        public Task<int> UpdateAsync(T model, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.ExecuteAsync(SqlUpdate(fields), model, tran, timeout);
        }

        public async Task UpdateManyAsync(IEnumerable<T> list, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                tran = new DistributedTransaction();
                try
                {
                    foreach (var item in list)
                    {
                        await UpdateAsync(item, fields, tran, timeout);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            else
            {
                foreach (var item in list)
                {
                    await UpdateAsync(item, fields, tran, timeout);
                }
            }
        }

        public int UpdateIgnore(T model, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Execute(SqlUpdateIgnore(fields), model, tran, timeout);
        }

        public void UpdateIgnoreMany(IEnumerable<T> list, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                tran = new DistributedTransaction();
                try
                {
                    foreach (var item in list)
                    {
                        UpdateIgnore(item, fields, tran, timeout);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            else
            {
                foreach (var item in list)
                {
                    UpdateIgnore(item, fields, tran, timeout);
                }
            }
        }

        public Task<int> UpdateIgnoreAsync(T model, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.ExecuteAsync(SqlUpdateIgnore(fields), model, tran, timeout);
        }

        public async Task UpdateIgnoreManyAsync(IEnumerable<T> list, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            if (tran == null)
            {
                tran = new DistributedTransaction();
                try
                {
                    foreach (var item in list)
                    {
                        await UpdateIgnoreAsync(item, fields, tran, timeout);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            else
            {
                foreach (var item in list)
                {
                    await UpdateIgnoreAsync(item, fields, tran, timeout);
                }
            }
        }

        public int UpdateByWhere(object model, string where, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Execute(SqlUpdateByWhere(where, fields), model, tran, timeout);
        }

        public Task<int> UpdateByWhereAsync(object model, string where, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.ExecuteAsync(SqlUpdateByWhere(where, fields), model, tran, timeout);
        }

        public int UpdateByWhereIgnore(object model, string where, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Execute(SqlUpdateByWhereIgnore(where, fields), model, tran, timeout);
        }

        public Task<int> UpdateByWhereIgnoreAsync(object model, string where, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.ExecuteAsync(SqlUpdateByWhereIgnore(where, fields), model, tran, timeout);
        }

        public virtual void Update(IEnumerable<T> modelList, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkUpdate(Name, modelList, opt =>
            {
                if (fields != null)
                {
                    var ignoreFileds = SqlField.AllFieldExceptKeyList.Except(fields).ToList();
                    opt.IgnoreOnUpdateNames = ignoreFileds;
                    if (timeout != null)
                    {
                        opt.BatchTimeout = timeout.Value;
                    }
                }
            }, tran);
        }

        public virtual Task UpdateAsync(IEnumerable<T> modelList, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkUpdateAsync(Name, modelList, opt =>
            {
                if (fields != null)
                {
                    var ignoreFileds = SqlField.AllFieldExceptKeyList.Except(fields).ToList();
                    opt.IgnoreOnUpdateNames = ignoreFileds;
                    if (timeout != null)
                    {
                        opt.BatchTimeout = timeout.Value;
                    }
                }
            }, tran);
        }

        public virtual void UpdateIgnore(IEnumerable<T> modelList, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkUpdate(Name, modelList, opt =>
            {
                opt.IgnoreOnUpdateNames = fields;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        public virtual Task UpdateIgnoreAsync(IEnumerable<T> modelList, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkUpdateAsync(Name, modelList, opt =>
            {
                opt.IgnoreOnUpdateNames = fields;
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        #endregion

        #region delete

        public int Delete(object id, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Execute(SqlDeleteById(), new { id }, tran, timeout);
        }

        public Task<int> DeleteAsync(object id, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.ExecuteAsync(SqlDeleteById(), new { id }, tran, timeout);
        }

        public int Delete(T model, DistributedTransaction tran = null, int? timeout = null)
        {
            var accessor = TypeAccessor.Create(typeof(T));
            var id = accessor[model, SqlField.PrimaryKey];
            return Delete(id, tran, timeout);
        }

        public Task<int> DeleteAsync(T model, DistributedTransaction tran = null, int? timeout = null)
        {
            var accessor = TypeAccessor.Create(typeof(T));
            var id = accessor[model, SqlField.PrimaryKey];
            return DeleteAsync(id, tran, timeout);
        }

        public int DeleteByIds(object ids, DistributedTransaction tran = null, int? timeout = null)
        {
            if (CommonUtil.ObjectIsEmpty(ids))
                return 0;
            var dpar = new DynamicParameters();
            if (DbType != DataBaseType.Oracle)
            {
                dpar.Add("@ids", ids);
            }
            else
            {
                dpar.Add(":ids", ids);
            }
            return DataBase.Execute(SqlDeleteByIds(), dpar, tran, timeout);
        }

        public Task<int> DeleteByIdsAsync(object ids, DistributedTransaction tran = null, int? timeout = null)
        {
            if (CommonUtil.ObjectIsEmpty(ids))
                return Task.FromResult(0);
            var dpar = new DynamicParameters();
            if (DbType != DataBaseType.Oracle)
            {
                dpar.Add("@ids", ids);
            }
            else
            {
                dpar.Add(":ids", ids);
            }
            return DataBase.ExecuteAsync(SqlDeleteByIds(), dpar, tran, timeout);
        }

        public int DeleteByWhere(string where, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Execute(SqlDeleteByWhere(where), param, tran, timeout);
        }

        public Task<int> DeleteByWhereAsync(string where, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.ExecuteAsync(SqlDeleteByWhere(where), param, tran, timeout);
        }

        public int DeleteAll(DistributedTransaction tran = null, int? timeout = null)
        {
            var sql = SqlDeleteAll();
            if (sql == null)
            {
                return 1;
            }
            return DataBase.Execute(sql, null, tran, timeout);
        }

        public Task<int> DeleteAllAsync(DistributedTransaction tran = null, int? timeout = null)
        {
            var sql = SqlDeleteAll();
            if (sql == null)
            {
                return Task.FromResult(1);
            }
            return DataBase.ExecuteAsync(sql, null, tran, timeout);
        }

        public virtual void Delete(IEnumerable<T> modelList, DistributedTransaction tran = null, int? timeout = null)
        {
            DataBase.BulkDelete(Name, modelList, opt =>
            {
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }

            }, tran);
        }

        public virtual Task DeleteAsync(IEnumerable<T> modelList, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.BulkDeleteAsync(Name, modelList, opt =>
            {
                if (timeout != null)
                {
                    opt.BatchTimeout = timeout.Value;
                }
            }, tran);
        }

        #endregion

        #region aggregate

        public bool Exists(object id, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.ExecuteScalar(SqlExists(), new { id }, tran, timeout) != null;
        }

        public async Task<bool> ExistsAsync(object id, DistributedTransaction tran = null, int? timeout = null)
        {
            return await DataBase.ExecuteScalarAsync(SqlExists(), new { id }, tran, timeout) != null;
        }

        public bool Exists(T model, DistributedTransaction tran = null, int? timeout = null)
        {
            var accessor = TypeAccessor.Create(typeof(T));
            var id = accessor[model, SqlField.PrimaryKey];
            return Exists(id, tran, timeout);
        }

        public Task<bool> ExistsAsync(T model, DistributedTransaction tran = null, int? timeout = null)
        {
            var accessor = TypeAccessor.Create(typeof(T));
            var id = accessor[model, SqlField.PrimaryKey];
            return ExistsAsync(id, tran, timeout);
        }

        public long Count(string where = null, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.ExecuteScalar<long>(SqlCount(where), param, tran, timeout);
        }

        public Task<long> CountAsync(string where = null, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.ExecuteScalarAsync<long>(SqlCount(where), param, tran, timeout);
        }

        public TValue Min<TValue>(string field, string where = null, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.ExecuteScalar<TValue>(SqlMin(field, where), param, tran, timeout);
        }

        public Task<TValue> MinAsync<TValue>(string field, string where = null, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.ExecuteScalarAsync<TValue>(SqlMin(field, where), param, tran, timeout);
        }

        public TValue Max<TValue>(string field, string where = null, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.ExecuteScalar<TValue>(SqlMax(field, where), param, tran, timeout);
        }

        public Task<TValue> MaxAsync<TValue>(string field, string where = null, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.ExecuteScalarAsync<TValue>(SqlMax(field, where), param, tran, timeout);
        }

        public TValue Sum<TValue>(string field, string where = null, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.ExecuteScalar<TValue>(SqlSum(field, where), param, tran, timeout);
        }

        public Task<TValue> SumAsync<TValue>(string field, string where = null, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.ExecuteScalarAsync<TValue>(SqlSum(field, where), param, tran, timeout);
        }

        public decimal Avg(string field, string where = null, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.ExecuteScalar<decimal>(SqlAvg(field, where), param, tran, timeout);
        }

        public Task<decimal> AvgAsync(string field, string where = null, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.ExecuteScalarAsync<decimal>(SqlAvg(field, where), param, tran, timeout);
        }

        #endregion

        #region GetAll

        public IEnumerable<T> GetAll(string returnFields = null, string orderby = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query<T>(SqlGetAll(returnFields, orderby), null, tran, timeout);
        }

        public Task<IEnumerable<T>> GetAllAsync(string returnFields = null, string orderby = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync<T>(SqlGetAll(returnFields, orderby), null, tran, timeout);
        }

        public IEnumerable<dynamic> GetAllDynamic(string returnFields = null, string orderby = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query(SqlGetAll(returnFields, orderby), null, tran, timeout);
        }

        public Task<IEnumerable<dynamic>> GetAllDynamicAsync(string returnFields = null, string orderby = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync(SqlGetAll(returnFields, orderby), null, tran, timeout);
        }

        #endregion

        #region GetById

        public T GetById(object id, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryFirstOrDefault<T>(SqlGetById(returnFields), new { id }, tran, timeout);
        }

        public Task<T> GetByIdAsync(object id, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryFirstOrDefaultAsync<T>(SqlGetById(returnFields), new { id }, tran, timeout);
        }

        public dynamic GetByIdDynamic(object id, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryFirstOrDefault(SqlGetById(returnFields), new { id }, tran, timeout);
        }

        public Task<dynamic> GetByIdDynamicAsync(object id, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryFirstOrDefaultAsync(SqlGetById(returnFields), new { id }, tran, timeout);
        }

        #endregion

        #region GetByIdForUpdate

        public T GetByIdForUpdate(object id, DistributedTransaction tran, string returnFields = null, int? timeout = null)
        {
            return DataBase.QueryFirstOrDefault<T>(SqlGetByIdForUpdate(returnFields), new { id }, tran, timeout);
        }

        public Task<T> GetByIdForUpdateAsync(object id, DistributedTransaction tran, string returnFields = null, int? timeout = null)
        {
            return DataBase.QueryFirstOrDefaultAsync<T>(SqlGetByIdForUpdate(returnFields), new { id }, tran, timeout);
        }

        public dynamic GetByIdForUpdateDynamic(object id, DistributedTransaction tran, string returnFields = null, int? timeout = null)
        {
            return DataBase.QueryFirstOrDefault(SqlGetByIdForUpdate(returnFields), new { id }, tran, timeout);
        }

        public Task<dynamic> GetByIdForUpdateDynamicAsync(object id, DistributedTransaction tran, string returnFields = null, int? timeout = null)
        {
            return DataBase.QueryFirstOrDefaultAsync(SqlGetByIdForUpdate(returnFields), new { id }, tran, timeout);
        }

        #endregion

        #region GetByIdForUpdateNoWait

        public T GetByIdForUpdateNoWait(object id, DistributedTransaction tran, string returnFields = null, int? timeout = null)
        {
            return DataBase.QueryFirstOrDefault<T>(SqlGetByIdForUpdateNoWait(returnFields), new { id }, tran, timeout);
        }

        public Task<T> GetByIdForUpdateNoWaitAsync(object id, DistributedTransaction tran, string returnFields = null, int? timeout = null)
        {
            return DataBase.QueryFirstOrDefaultAsync<T>(SqlGetByIdForUpdateNoWait(returnFields), new { id }, tran, timeout);
        }

        public dynamic GetByIdForUpdateNoWaitDynamic(object id, DistributedTransaction tran, string returnFields = null, int? timeout = null)
        {
            return DataBase.QueryFirstOrDefault(SqlGetByIdForUpdateNoWait(returnFields), new { id }, tran, timeout);
        }

        public Task<dynamic> GetByIdForUpdateNoWaitDynamicAsync(object id, DistributedTransaction tran, string returnFields = null, int? timeout = null)
        {
            return DataBase.QueryFirstOrDefaultAsync(SqlGetByIdForUpdateNoWait(returnFields), new { id }, tran, timeout);
        }

        #endregion

        #region GetByIds

        public IEnumerable<T> GetByIds(object ids, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (CommonUtil.ObjectIsEmpty(ids))
                return Enumerable.Empty<T>();
            var dpar = new DynamicParameters();
            if (DbType != DataBaseType.Oracle)
            {
                dpar.Add("@ids", ids);
            }
            else
            {
                dpar.Add(":ids", ids);
            }
            return DataBase.Query<T>(SqlGetByIds(returnFields), dpar, tran, timeout);
        }

        public Task<IEnumerable<T>> GetByIdsAsync(object ids, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (CommonUtil.ObjectIsEmpty(ids))
                return Task.FromResult(Enumerable.Empty<T>());
            var dpar = new DynamicParameters();
            if (DbType != DataBaseType.Oracle)
            {
                dpar.Add("@ids", ids);
            }
            else
            {
                dpar.Add(":ids", ids);
            }
            return DataBase.QueryAsync<T>(SqlGetByIds(returnFields), dpar, tran, timeout);
        }

        public IEnumerable<dynamic> GetByIdsDynamic(object ids, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (CommonUtil.ObjectIsEmpty(ids))
                return Enumerable.Empty<dynamic>();
            var dpar = new DynamicParameters();
            if (DbType != DataBaseType.Oracle)
            {
                dpar.Add("@ids", ids);
            }
            else
            {
                dpar.Add(":ids", ids);
            }
            return DataBase.Query(SqlGetByIds(returnFields), dpar, tran, timeout);
        }

        public Task<IEnumerable<dynamic>> GetByIdsDynamicAsync(object ids, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (CommonUtil.ObjectIsEmpty(ids))
                return Task.FromResult(Enumerable.Empty<dynamic>());
            var dpar = new DynamicParameters();
            if (DbType != DataBaseType.Oracle)
            {
                dpar.Add("@ids", ids);
            }
            else
            {
                dpar.Add(":ids", ids);
            }
            return DataBase.QueryAsync(SqlGetByIds(returnFields), dpar, tran, timeout);
        }

        #endregion

        #region GetByIdsForUpdate

        public IEnumerable<T> GetByIdsForUpdate(object ids, DistributedTransaction tran, string returnFields = null, int? timeout = null)
        {
            if (CommonUtil.ObjectIsEmpty(ids))
                return Enumerable.Empty<T>();
            var dpar = new DynamicParameters();
            if (DbType != DataBaseType.Oracle)
            {
                dpar.Add("@ids", ids);
            }
            else
            {
                dpar.Add(":ids", ids);
            }
            return DataBase.Query<T>(SqlGetByIdsForUpdate(returnFields), dpar, tran, timeout);
        }

        public Task<IEnumerable<T>> GetByIdsForUpdateAsync(object ids, DistributedTransaction tran, string returnFields = null, int? timeout = null)
        {
            if (CommonUtil.ObjectIsEmpty(ids))
                return Task.FromResult(Enumerable.Empty<T>());
            var dpar = new DynamicParameters();
            if (DbType != DataBaseType.Oracle)
            {
                dpar.Add("@ids", ids);
            }
            else
            {
                dpar.Add(":ids", ids);
            }
            return DataBase.QueryAsync<T>(SqlGetByIdsForUpdate(returnFields), dpar, tran, timeout);
        }

        public IEnumerable<dynamic> GetByIdsForUpdateDynamic(object ids, DistributedTransaction tran, string returnFields = null, int? timeout = null)
        {
            if (CommonUtil.ObjectIsEmpty(ids))
                return Enumerable.Empty<dynamic>();
            var dpar = new DynamicParameters();
            if (DbType != DataBaseType.Oracle)
            {
                dpar.Add("@ids", ids);
            }
            else
            {
                dpar.Add(":ids", ids);
            }
            return DataBase.Query(SqlGetByIdsForUpdate(returnFields), dpar, tran, timeout);
        }

        public Task<IEnumerable<dynamic>> GetByIdsForUpdateDynamicAsync(object ids, DistributedTransaction tran, string returnFields = null, int? timeout = null)
        {
            if (CommonUtil.ObjectIsEmpty(ids))
                return Task.FromResult(Enumerable.Empty<dynamic>());
            var dpar = new DynamicParameters();
            if (DbType != DataBaseType.Oracle)
            {
                dpar.Add("@ids", ids);
            }
            else
            {
                dpar.Add(":ids", ids);
            }
            return DataBase.QueryAsync(SqlGetByIdsForUpdate(returnFields), dpar, tran, timeout);
        }

        #endregion

        #region GetByIdsForUpdateNoWait

        public IEnumerable<T> GetByIdsForUpdateNoWait(object ids, DistributedTransaction tran, string returnFields = null, int? timeout = null)
        {
            if (CommonUtil.ObjectIsEmpty(ids))
                return Enumerable.Empty<T>();
            var dpar = new DynamicParameters();
            if (DbType != DataBaseType.Oracle)
            {
                dpar.Add("@ids", ids);
            }
            else
            {
                dpar.Add(":ids", ids);
            }
            return DataBase.Query<T>(SqlGetByIdsForUpdateNoWait(returnFields), dpar, tran, timeout);
        }

        public Task<IEnumerable<T>> GetByIdsForUpdateNoWaitAsync(object ids, DistributedTransaction tran, string returnFields = null, int? timeout = null)
        {
            if (CommonUtil.ObjectIsEmpty(ids))
                return Task.FromResult(Enumerable.Empty<T>());
            var dpar = new DynamicParameters();
            if (DbType != DataBaseType.Oracle)
            {
                dpar.Add("@ids", ids);
            }
            else
            {
                dpar.Add(":ids", ids);
            }
            return DataBase.QueryAsync<T>(SqlGetByIdsForUpdateNoWait(returnFields), dpar, tran, timeout);
        }

        public IEnumerable<dynamic> GetByIdsForUpdateNoWaitDynamic(object ids, DistributedTransaction tran, string returnFields = null, int? timeout = null)
        {
            if (CommonUtil.ObjectIsEmpty(ids))
                return Enumerable.Empty<dynamic>();
            var dpar = new DynamicParameters();
            if (DbType != DataBaseType.Oracle)
            {
                dpar.Add("@ids", ids);
            }
            else
            {
                dpar.Add(":ids", ids);
            }
            return DataBase.Query(SqlGetByIdsForUpdateNoWait(returnFields), dpar, tran, timeout);
        }

        public Task<IEnumerable<dynamic>> GetByIdsForUpdateNoWaitDynamicAsync(object ids, DistributedTransaction tran, string returnFields = null, int? timeout = null)
        {
            if (CommonUtil.ObjectIsEmpty(ids))
                return Task.FromResult(Enumerable.Empty<dynamic>());
            var dpar = new DynamicParameters();
            if (DbType != DataBaseType.Oracle)
            {
                dpar.Add("@ids", ids);
            }
            else
            {
                dpar.Add(":ids", ids);
            }
            return DataBase.QueryAsync(SqlGetByIdsForUpdateNoWait(returnFields), dpar, tran, timeout);
        }

        #endregion

        #region GetByIdsWithField

        public IEnumerable<T> GetByIdsWithField(object ids, string field, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (CommonUtil.ObjectIsEmpty(ids))
                return Enumerable.Empty<T>();
            var dpar = new DynamicParameters();
            if (DbType != DataBaseType.Oracle)
            {
                dpar.Add("@ids", ids);
            }
            else
            {
                dpar.Add(":ids", ids);
            }
            return DataBase.Query<T>(SqlGetByIdsWithField(field, returnFields), dpar, tran, timeout);
        }

        public Task<IEnumerable<T>> GetByIdsWithFieldAsync(object ids, string field, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (CommonUtil.ObjectIsEmpty(ids))
                return Task.FromResult(Enumerable.Empty<T>());
            var dpar = new DynamicParameters();
            if (DbType != DataBaseType.Oracle)
            {
                dpar.Add("@ids", ids);
            }
            else
            {
                dpar.Add(":ids", ids);
            }
            return DataBase.QueryAsync<T>(SqlGetByIdsWithField(field, returnFields), dpar, tran, timeout);
        }

        public IEnumerable<dynamic> GetByIdsWithFieldDynamic(object ids, string field, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (CommonUtil.ObjectIsEmpty(ids))
                return Enumerable.Empty<dynamic>();
            var dpar = new DynamicParameters();
            if (DbType != DataBaseType.Oracle)
            {
                dpar.Add("@ids", ids);
            }
            else
            {
                dpar.Add(":ids", ids);
            }
            return DataBase.Query(SqlGetByIdsWithField(field, returnFields), dpar, tran, timeout);
        }

        public Task<IEnumerable<dynamic>> GetByIdsWithFieldDynamicAsync(object ids, string field, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (CommonUtil.ObjectIsEmpty(ids))
                return Task.FromResult(Enumerable.Empty<dynamic>());
            var dpar = new DynamicParameters();
            if (DbType != DataBaseType.Oracle)
            {
                dpar.Add("@ids", ids);
            }
            else
            {
                dpar.Add(":ids", ids);
            }
            return DataBase.QueryAsync(SqlGetByIdsWithField(field, returnFields), dpar, tran, timeout);
        }

        #endregion

        #region GetByWhere

        public IEnumerable<T> GetByWhere(string where, object param = null, string returnFields = null, string orderby = null, int limit = 0, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query<T>(SqlGetByWhere(where, returnFields, orderby, limit), param, tran, timeout);
        }

        public Task<IEnumerable<T>> GetByWhereAsync(string where, object param = null, string returnFields = null, string orderby = null, int limit = 0, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync<T>(SqlGetByWhere(where, returnFields, orderby, limit), param, tran, timeout);
        }

        public IEnumerable<dynamic> GetByWhereDynamic(string where, object param = null, string returnFields = null, string orderby = null, int limit = 0, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query(SqlGetByWhere(where, returnFields, orderby, limit), param, tran, timeout);
        }

        public Task<IEnumerable<dynamic>> GetByWhereDynamicAsync(string where, object param = null, string returnFields = null, string orderby = null, int limit = 0, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync(SqlGetByWhere(where, returnFields, orderby, limit), param, tran, timeout);
        }

        #endregion

        #region GetByWhereFirst

        public T GetByWhereFirst(string where, object param = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryFirstOrDefault<T>(SqlGetByWhereFirst(where, returnFields), param, tran, timeout);
        }

        public Task<T> GetByWhereFirstAsync(string where, object param = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryFirstOrDefaultAsync<T>(SqlGetByWhereFirst(where, returnFields), param, tran, timeout);
        }

        public dynamic GetByWhereFirstDynamic(string where, object param = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryFirstOrDefault(SqlGetByWhereFirst(where, returnFields), param, tran, timeout);
        }

        public Task<dynamic> GetByWhereFirstDynamicAsync(string where, object param = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryFirstOrDefaultAsync(SqlGetByWhereFirst(where, returnFields), param, tran, timeout);
        }

        #endregion

        #region GetBySkipTake

        public IEnumerable<T> GetBySkipTake(int skip, int take, string where = null, object param = null, string returnFields = null, string orderby = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query<T>(SqlGetBySkipTake(skip, take, where, returnFields, orderby), param, tran, timeout);
        }

        public Task<IEnumerable<T>> GetBySkipTakeAsync(int skip, int take, string where = null, object param = null, string returnFields = null, string orderby = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync<T>(SqlGetBySkipTake(skip, take, where, returnFields, orderby), param, tran, timeout);
        }

        public IEnumerable<dynamic> GetBySkipTakeDynamic(int skip, int take, string where = null, object param = null, string returnFields = null, string orderby = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query(SqlGetBySkipTake(skip, take, where, returnFields, orderby), param, tran, timeout);
        }

        public Task<IEnumerable<dynamic>> GetBySkipTakeDynamicAsync(int skip, int take, string where = null, object param = null, string returnFields = null, string orderby = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync(SqlGetBySkipTake(skip, take, where, returnFields, orderby), param, tran, timeout);
        }

        #endregion

        #region GetByAscFirstPage

        public IEnumerable<T> GetByAscFirstPage(int pageSize, object param = null, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query<T>(SqlGetByAscFirstPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public Task<IEnumerable<T>> GetByAscFirstPageAsync(int pageSize, object param = null, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync<T>(SqlGetByAscFirstPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public IEnumerable<dynamic> GetByAscFirstPageDynamic(int pageSize, object param = null, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query(SqlGetByAscFirstPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public Task<IEnumerable<dynamic>> GetByAscFirstPageDynamicAsync(int pageSize, object param = null, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync(SqlGetByAscFirstPage(pageSize, and, returnFields), param, tran, timeout);
        }

        #endregion

        #region GetByAscPrevPage

        public IEnumerable<T> GetByAscPrevPage(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query<T>(SqlGetByAscPrevPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public Task<IEnumerable<T>> GetByAscPrevPageAsync(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync<T>(SqlGetByAscPrevPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public IEnumerable<dynamic> GetByAscPrevPageDynamic(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query(SqlGetByAscPrevPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public Task<IEnumerable<dynamic>> GetByAscPrevPageDynamicAsync(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync(SqlGetByAscPrevPage(pageSize, and, returnFields), param, tran, timeout);
        }

        #endregion

        #region GetByAscCurrentPage

        public IEnumerable<T> GetByAscCurrentPage(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query<T>(SqlGetByAscCurrentPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public Task<IEnumerable<T>> GetByAscCurrentPageAsync(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync<T>(SqlGetByAscCurrentPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public IEnumerable<dynamic> GetByAscCurrentPageDynamic(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query(SqlGetByAscCurrentPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public Task<IEnumerable<dynamic>> GetByAscCurrentPageDynamicAsync(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync(SqlGetByAscCurrentPage(pageSize, and, returnFields), param, tran, timeout);
        }

        #endregion

        #region GetByAscNextPage

        public IEnumerable<T> GetByAscNextPage(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query<T>(SqlGetByAscNextPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public Task<IEnumerable<T>> GetByAscNextPageAsync(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync<T>(SqlGetByAscNextPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public IEnumerable<dynamic> GetByAscNextPageDynamic(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query(SqlGetByAscNextPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public Task<IEnumerable<dynamic>> GetByAscNextPageDynamicAsync(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync(SqlGetByAscNextPage(pageSize, and, returnFields), param, tran, timeout);
        }

        #endregion

        #region GetByAscLastPage


        public IEnumerable<T> GetByAscLastPage(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query<T>(SqlGetByAscLastPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public Task<IEnumerable<T>> GetByAscLastPageAsync(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync<T>(SqlGetByAscLastPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public IEnumerable<dynamic> GetByAscLastPageDynamic(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query(SqlGetByAscLastPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public Task<IEnumerable<dynamic>> GetByAscLastPageDynamicAsync(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync(SqlGetByAscLastPage(pageSize, and, returnFields), param, tran, timeout);
        }


        #endregion

        #region GetByDescFirstPage

        public IEnumerable<T> GetByDescFirstPage(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query<T>(SqlGetByDescFirstPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public Task<IEnumerable<T>> GetByDescFirstPageAsync(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync<T>(SqlGetByDescFirstPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public IEnumerable<dynamic> GetByDescFirstPageDynamic(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query(SqlGetByDescFirstPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public Task<IEnumerable<dynamic>> GetByDescFirstPageDynamicAsync(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync(SqlGetByDescFirstPage(pageSize, and, returnFields), param, tran, timeout);
        }

        #endregion

        #region GetByDescPrevPage

        public IEnumerable<T> GetByDescPrevPage(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query<T>(SqlGetByDescPrevPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public Task<IEnumerable<T>> GetByDescPrevPageAsync(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync<T>(SqlGetByDescPrevPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public IEnumerable<dynamic> GetByDescPrevPageDynamic(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query(SqlGetByDescPrevPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public Task<IEnumerable<dynamic>> GetByDescPrevPageDynamicAsync(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync(SqlGetByDescPrevPage(pageSize, and, returnFields), param, tran, timeout);
        }


        #endregion

        #region GetByDescCurrentPage

        public IEnumerable<T> GetByDescCurrentPage(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query<T>(SqlGetByDescCurrentPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public Task<IEnumerable<T>> GetByDescCurrentPageAsync(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync<T>(SqlGetByDescCurrentPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public IEnumerable<dynamic> GetByDescCurrentPageDynamic(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query(SqlGetByDescCurrentPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public Task<IEnumerable<dynamic>> GetByDescCurrentPageDynamicAsync(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync(SqlGetByDescCurrentPage(pageSize, and, returnFields), param, tran, timeout);
        }

        #endregion

        #region GetByDescNextPage

        public IEnumerable<T> GetByDescNextPage(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query<T>(SqlGetByDescNextPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public Task<IEnumerable<T>> GetByDescNextPageAsync(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync<T>(SqlGetByDescNextPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public IEnumerable<dynamic> GetByDescNextPageDynamic(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query(SqlGetByDescNextPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public Task<IEnumerable<dynamic>> GetByDescNextPageDynamicAsync(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync(SqlGetByDescNextPage(pageSize, and, returnFields), param, tran, timeout);
        }

        #endregion

        #region GetByDescLastPage

        public IEnumerable<T> GetByDescLastPage(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query<T>(SqlGetByDescLastPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public Task<IEnumerable<T>> GetByDescLastPageAsync(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync<T>(SqlGetByDescLastPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public IEnumerable<dynamic> GetByDescLastPageDynamic(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.Query(SqlGetByDescLastPage(pageSize, and, returnFields), param, tran, timeout);
        }

        public Task<IEnumerable<dynamic>> GetByDescLastPageDynamicAsync(int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return DataBase.QueryAsync(SqlGetByDescLastPage(pageSize, and, returnFields), param, tran, timeout);
        }

        #endregion

        /*******以下方法仅调用上面方法********/

        #region GetByPage

        public IEnumerable<T> GetByPage(int page, int pageSize, string where = null, object param = null, string returnFields = null, string orderby = null, DistributedTransaction tran = null, int? timeout = null)
        {
            int skip = 0;
            if (page > 1)
            {
                skip = (page - 1) * pageSize;
            }
            return GetBySkipTake(skip, pageSize, where, param, returnFields, orderby, tran, timeout);
        }

        public Task<IEnumerable<T>> GetByPageAsync(int page, int pageSize, string where = null, object param = null, string returnFields = null, string orderby = null, DistributedTransaction tran = null, int? timeout = null)
        {
            int skip = 0;
            if (page > 1)
            {
                skip = (page - 1) * pageSize;
            }
            return GetBySkipTakeAsync(skip, pageSize, where, param, returnFields, orderby, tran, timeout);
        }

        public IEnumerable<dynamic> GetByPageDynamic(int page, int pageSize, string where = null, object param = null, string returnFields = null, string orderby = null, DistributedTransaction tran = null, int? timeout = null)
        {
            int skip = 0;
            if (page > 1)
            {
                skip = (page - 1) * pageSize;
            }
            return GetBySkipTakeDynamic(skip, pageSize, where, param, returnFields, orderby, tran, timeout);
        }

        public Task<IEnumerable<dynamic>> GetByPageDynamicAsync(int page, int pageSize, string where = null, object param = null, string returnFields = null, string orderby = null, DistributedTransaction tran = null, int? timeout = null)
        {
            int skip = 0;
            if (page > 1)
            {
                skip = (page - 1) * pageSize;
            }
            return GetBySkipTakeDynamicAsync(skip, pageSize, where, param, returnFields, orderby, tran, timeout);
        }

        #endregion

        #region GetByPageAndCount

        public PageEntity<T> GetByPageAndCount(int page, int pageSize, string where = null, object param = null, string returnFields = null, string orderby = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return new PageEntity<T>
            {
                Data = GetByPage(page, pageSize, where, param, returnFields, orderby, tran, timeout),
                Count = Count(where, param, tran, timeout)
            };
        }

        public async Task<PageEntity<T>> GetByPageAndCountAsync(int page, int pageSize, string where = null, object param = null, string returnFields = null, string orderby = null, DistributedTransaction tran = null, int? timeout = null)
        {
            var task1 = GetByPageAsync(page, pageSize, where, param, returnFields, orderby, tran, timeout);
            var task2 = CountAsync(where, param, tran, timeout);
            await Task.WhenAll(task1, task2);
            return new PageEntity<T>
            {
                Data = task1.Result,
                Count = task2.Result
            };
        }

        public PageEntity<dynamic> GetByPageAndCountDynamic(int page, int pageSize, string where = null, object param = null, string returnFields = null, string orderby = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return new PageEntity<dynamic>
            {
                Data = GetByPageDynamic(page, pageSize, where, param, returnFields, orderby, tran, timeout),
                Count = Count(where, param, tran, timeout)
            };
        }

        public async Task<PageEntity<dynamic>> GetByPageAndCountDynamicAsync(int page, int pageSize, string where = null, object param = null, string returnFields = null, string orderby = null, DistributedTransaction tran = null, int? timeout = null)
        {
            var task1 = GetByPageDynamicAsync(page, pageSize, where, param, returnFields, orderby, tran, timeout);
            var task2 = CountAsync(where, param, tran, timeout);
            await Task.WhenAll(task1, task2);
            return new PageEntity<dynamic>
            {
                Data = task1.Result,
                Count = task2.Result
            };
        }

        #endregion

        #region GetByAscDescPage

        public IEnumerable<T> GetByAscDescPage(bool asc, AscDescPage adPage, int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (asc)
            {
                switch (adPage)
                {
                    case AscDescPage.Fist:
                        return GetByAscFirstPage(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Prev:
                        return GetByAscPrevPage(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Current:
                        return GetByAscCurrentPage(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Next:
                        return GetByAscNextPage(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Last:
                        return GetByAscLastPage(pageSize, param, and, returnFields, tran, timeout);
                }
            }
            else
            {
                switch (adPage)
                {
                    case AscDescPage.Fist:
                        return GetByDescFirstPage(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Prev:
                        return GetByDescPrevPage(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Current:
                        return GetByDescCurrentPage(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Next:
                        return GetByDescNextPage(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Last:
                        return GetByDescLastPage(pageSize, param, and, returnFields, tran, timeout);
                }
            }
            return null;
        }

        public Task<IEnumerable<T>> GetByAscDescPageAsync(bool asc, AscDescPage adPage, int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (asc)
            {
                switch (adPage)
                {
                    case AscDescPage.Fist:
                        return GetByAscFirstPageAsync(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Prev:
                        return GetByAscPrevPageAsync(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Current:
                        return GetByAscCurrentPageAsync(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Next:
                        return GetByAscNextPageAsync(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Last:
                        return GetByAscLastPageAsync(pageSize, param, and, returnFields, tran, timeout);
                }
            }
            else
            {
                switch (adPage)
                {
                    case AscDescPage.Fist:
                        return GetByDescFirstPageAsync(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Prev:
                        return GetByDescPrevPageAsync(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Current:
                        return GetByDescCurrentPageAsync(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Next:
                        return GetByDescNextPageAsync(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Last:
                        return GetByDescLastPageAsync(pageSize, param, and, returnFields, tran, timeout);
                }
            }
            return null;
        }

        public IEnumerable<dynamic> GetByAscDescPageDynamic(bool asc, AscDescPage adPage, int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (asc)
            {
                switch (adPage)
                {
                    case AscDescPage.Fist:
                        return GetByAscFirstPageDynamic(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Prev:
                        return GetByAscPrevPageDynamic(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Current:
                        return GetByAscCurrentPageDynamic(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Next:
                        return GetByAscNextPageDynamic(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Last:
                        return GetByAscLastPageDynamic(pageSize, param, and, returnFields, tran, timeout);
                }
            }
            else
            {
                switch (adPage)
                {
                    case AscDescPage.Fist:
                        return GetByDescFirstPageDynamic(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Prev:
                        return GetByDescPrevPageDynamic(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Current:
                        return GetByDescCurrentPageDynamic(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Next:
                        return GetByDescNextPageDynamic(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Last:
                        return GetByDescLastPageDynamic(pageSize, param, and, returnFields, tran, timeout);
                }
            }
            return null;
        }

        public Task<IEnumerable<dynamic>> GetByAscDescPageDynamicAsync(bool asc, AscDescPage adPage, int pageSize, object param, string and = null, string returnFields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (asc)
            {
                switch (adPage)
                {
                    case AscDescPage.Fist:
                        return GetByAscFirstPageDynamicAsync(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Prev:
                        return GetByAscPrevPageDynamicAsync(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Current:
                        return GetByAscCurrentPageDynamicAsync(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Next:
                        return GetByAscNextPageDynamicAsync(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Last:
                        return GetByAscLastPageDynamicAsync(pageSize, param, and, returnFields, tran, timeout);
                }
            }
            else
            {
                switch (adPage)
                {
                    case AscDescPage.Fist:
                        return GetByDescFirstPageDynamicAsync(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Prev:
                        return GetByDescPrevPageDynamicAsync(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Current:
                        return GetByDescCurrentPageDynamicAsync(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Next:
                        return GetByDescNextPageDynamicAsync(pageSize, param, and, returnFields, tran, timeout);
                    case AscDescPage.Last:
                        return GetByDescLastPageDynamicAsync(pageSize, param, and, returnFields, tran, timeout);
                }
            }
            return null;
        }


        #endregion

        #region Truncate

        public void Truncate()
        {
            DataBase.TruncateTable(Name);
        }

        #endregion

        #region Optimize

        public void Optimize(bool final = false, bool deduplicate = false)
        {
            DataBase.OptimizeTable(Name, final, deduplicate);
        }

        public void Optimize(string partition, bool final = false, bool deduplicate = false)
        {
            DataBase.OptimizeTable(Name, partition, final, deduplicate);
        }

        #endregion

        /*******IQuery********/
        public IQuery AsQuery(string asName = null)
        {
            IQuery query;
            if (DbType == DataBaseType.MySql)
            {
                query = new MySqlQuery(DataBase);
            }
            else if (DbType == DataBaseType.Postgresql)
            {
                query = new PostgreQuery(DataBase);
            }
            else if (DbType == DataBaseType.Sqlite)
            {
                query = new SQLiteQuery(DataBase);
            }
            else if (DbType == DataBaseType.Oracle)
            {
                query = new OracleQuery(DataBase);
            }
            else
            {
                query = new SqlServerQuery(DataBase);
            }
            query.Add(this, asName);
            return query;
        }
    }

}
