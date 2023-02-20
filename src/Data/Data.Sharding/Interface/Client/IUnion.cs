using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    /// 用于表示Union合并操作的抽象
    /// </summary>
    public abstract class IUnion
    {
        protected IDatabase db;
        protected string sqlTable;
        protected int skip;
        protected int take;
        object par;
        DistributedTransaction tran;
        int? timeout;
        public IUnion(IDatabase db)
        {
            returnFields = "*";
            this.db = db;
        }

        private void SetSqlServer(IQuery query)
        {
            if (db.DbType == DataBaseType.SqlServer2012)
            {
                sqlOrderBy = query.sqlOrderBy;
            }
            else if (db.DbType == DataBaseType.SqlServer2005 || db.DbType == DataBaseType.SqlServer2008)
            {
                sqlOrderBy = query.sqlOrderBy;
                if (query.skip > 1)
                {
                    throw new Exception("IQuery must set page=1 or skip=0");
                }
                if (query.take <= 0)
                {
                    query.sqlOrderBy = null;
                }
            }
        }

        public IUnion Union(IQuery query)
        {
            SetSqlServer(query);
            if (string.IsNullOrEmpty(sqlTable))
            {
                if (db.DbType == DataBaseType.Sqlite)
                {
                    sqlTable += $"SELECT * FROM ({query.GetSql()})";
                }
                else
                {
                    sqlTable += $"({query.GetSql()})";
                }
            }
            else
            {
                if (db.DbType == DataBaseType.Sqlite)
                {
                    sqlTable += $" UNION SELECT * FROM ({query.GetSql()})";
                }
                else
                {
                    sqlTable += $" UNION ({query.GetSql()})";
                }
            }
            return this;
        }

        public IUnion UnionAll(IQuery query)
        {
            SetSqlServer(query);
            if (string.IsNullOrEmpty(sqlTable))
            {
                if (db.DbType == DataBaseType.Sqlite)
                {
                    sqlTable += $"SELECT * FROM ({query.GetSql()})";
                }
                else
                {
                    sqlTable += $"({query.GetSql()})";
                }
            }
            else
            {
                if (db.DbType == DataBaseType.Sqlite)
                {
                    sqlTable += $" UNION ALL SELECT * FROM ({query.GetSql()})";
                }
                else
                {
                    sqlTable += $" UNION ALL ({query.GetSql()})";
                }
            }
            return this;
        }

        #region Abstract

        public abstract string GetSql();

        public abstract string GetSqlCount();

        #endregion

        #region Method

        public void Clear()
        {
            returnFields = "*";
            skip = 0;
            take = 0;
            par = null;
            tran = null;
            timeout = 0;
            sqlTable = null;
            sqlWhere = null;
            sqlGroupBy = null;
            sqlHaving = null;
            sqlOrderBy = null;
        }

        #endregion

        #region Query Condition

        protected string sqlWhere;
        public IUnion Where(string where)
        {
            sqlWhere = $" WHERE {where}";
            return this;
        }

        protected string sqlGroupBy;
        public IUnion GroupBy(string groupBy)
        {
            sqlGroupBy = $" GROUP BY {groupBy}";
            return this;
        }

        protected string sqlHaving;
        public IUnion Having(string having)
        {
            sqlHaving = $" HAVING {having}";
            return this;
        }

        protected string sqlOrderBy;
        public IUnion OrderBy(string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                sqlOrderBy = null;
            }
            else
            {
                sqlOrderBy = $" ORDER BY {orderBy}";
            }
            return this;
        }

        protected string returnFields;
        public IUnion ReturnFields(string fields)
        {
            returnFields = fields;
            return this;
        }

        public IUnion Limit(int count)
        {
            skip = 0;
            take = count;
            return this;
        }

        public IUnion Limit(int skip, int take)
        {
            this.skip = skip;
            this.take = take;
            return this;
        }

        public IUnion Page(int page, int pageSize)
        {
            int skip = 0;
            if (page > 1)
            {
                skip = (page - 1) * pageSize;
            }
            this.skip = skip;
            take = pageSize;
            return this;
        }

        public IUnion Param(object param, DistributedTransaction tran = null, int? timeout = null)
        {
            par = param;
            this.tran = tran;
            this.timeout = timeout;
            return this;
        }

        #endregion

        #region Query

        public long Count()
        {
            return db.ExecuteScalar<long>(GetSqlCount(), par, tran, timeout);
        }

        public Task<long> CountAsync()
        {
            return db.ExecuteScalarAsync<long>(GetSqlCount(), par, tran, timeout);
        }

        public T QueryFirstOrDefault<T>()
        {
            return db.QueryFirstOrDefault<T>(GetSql(), par, tran, timeout);
        }

        public Task<T> QueryFirstOrDefaultAsync<T>()
        {
            return db.QueryFirstOrDefaultAsync<T>(GetSql(), par, tran, timeout);
        }

        public dynamic QueryFirstOrDefault()
        {
            return db.QueryFirstOrDefault(GetSql(), par, tran, timeout);
        }

        public Task<dynamic> QueryFirstOrDefaultAsync()
        {
            return db.QueryFirstOrDefaultAsync(GetSql(), par, tran, timeout);
        }

        public IEnumerable<T> Query<T>()
        {
            return db.Query<T>(GetSql(), par, tran, timeout);
        }

        public Task<IEnumerable<T>> QueryAsync<T>()
        {
            return db.QueryAsync<T>(GetSql(), par, tran, timeout);
        }

        public IEnumerable<dynamic> Query()
        {
            return db.Query(GetSql(), par, tran, timeout);
        }

        public Task<IEnumerable<dynamic>> QueryAsync()
        {
            return db.QueryAsync(GetSql(), par, tran, timeout);
        }

        public DataTable QueryDataTable()
        {
            return db.QueryDataTable(GetSql(), par, tran, timeout);
        }

        public Task<DataTable> QueryDataTableAsync()
        {
            return db.QueryDataTableAsync(GetSql(), par, tran, timeout);
        }

        public T ExecuteScalar<T>()
        {
            return db.ExecuteScalar<T>(GetSql(), par, tran, timeout);
        }

        public Task<T> ExecuteScalarAsync<T>()
        {
            return db.ExecuteScalarAsync<T>(GetSql(), par, tran, timeout);
        }

        public object ExecuteScalar()
        {
            return db.ExecuteScalar(GetSql(), par, tran, timeout);
        }

        public Task<object> ExecuteScalarAsync()
        {
            return db.ExecuteScalarAsync(GetSql(), par, tran, timeout);
        }

        /**********QueryPageAndCount************/

        public PageEntity<T> QueryPageAndCount<T>()
        {
            return new PageEntity<T>
            {
                Data = Query<T>(),
                Count = Count()
            };
        }

        public async Task<PageEntity<T>> QueryPageAndCountAsync<T>()
        {
            var task1 = QueryAsync<T>();
            var task2 = CountAsync();
            await Task.WhenAll(task1, task2);
            return new PageEntity<T>
            {
                Data = task1.Result,
                Count = task2.Result
            };
        }

        public PageEntity<dynamic> QueryPageAndCount()
        {
            return new PageEntity<dynamic>
            {
                Data = Query(),
                Count = Count()
            };
        }

        public async Task<PageEntity<dynamic>> QueryPageAndCountAsync()
        {
            var task1 = QueryAsync();
            var task2 = CountAsync();
            await Task.WhenAll(task1, task2);
            return new PageEntity<dynamic>
            {
                Data = task1.Result,
                Count = task2.Result
            };
        }

        #endregion
    }
}
