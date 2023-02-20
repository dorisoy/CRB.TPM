using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Sharding
{
    public abstract class IQuery
    {
        protected IDatabase db;
        internal int skip;
        internal int take;
        object par;
        DistributedTransaction tran;
        int? timeout;
        public IQuery(IDatabase db)
        {
            this.db = db;
        }

        #region Abstract

        protected string _sqlTable;
        protected string sqlTable;
        internal abstract IQuery Add<T>(ITable<T> table, string asName = null) where T : class;

        public abstract IQuery InnerJoin<T>(ITable<T> table, string asName, string on) where T : class;

        public abstract IQuery LeftJoin<T>(ITable<T> table, string asName, string on) where T : class;

        public abstract IQuery RightJoin<T>(ITable<T> table, string asName, string on) where T : class;

        public abstract string GetSql();

        public abstract string GetSqlCount();

        #endregion

        #region Method

        public void Clear()
        {
            sqlTable = _sqlTable;
            returnFields = _returnFields;
            sqlOrderBy = _sqlOrderBy;
            skip = 0;
            take = 0;
            par = null;
            tran = null;
            timeout = 0;
            sqlWhere = null;
            sqlGroupBy = null;
            sqlHaving = null;
        }

        #endregion

        #region Query Condition

        protected string sqlWhere;
        public IQuery Where(string where)
        {
            sqlWhere = $" WHERE {where}";
            return this;
        }

        protected string sqlGroupBy;
        public IQuery GroupBy(string groupBy)
        {
            sqlGroupBy = $" GROUP BY {groupBy}";
            return this;
        }

        protected string sqlHaving;
        public IQuery Having(string having)
        {
            sqlHaving = $" HAVING {having}";
            return this;
        }

        protected string _sqlOrderBy;
        internal string sqlOrderBy;
        public IQuery OrderBy(string orderBy)
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

        protected string _returnFields;
        protected string returnFields;
        public IQuery ReturnFields(string fields)
        {
            returnFields = fields;
            return this;
        }

        public IQuery Limit(int count)
        {
            skip = 0;
            take = count;
            return this;
        }

        public IQuery Limit(int skip, int take)
        {
            this.skip = skip;
            this.take = take;
            return this;
        }

        public IQuery Page(int page, int pageSize)
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

        public IQuery Param(object param, DistributedTransaction tran = null, int? timeout = null)
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

        #region Union

        public IUnion Union(params IQuery[] querys)
        {
            var union = db.CreateUnion();
            union.Union(this);
            foreach (var item in querys)
            {
                union.Union(item);
            }
            return union;
        }

        public IUnion UnionAll(params IQuery[] querys)
        {
            var union = db.CreateUnion();
            union.UnionAll(this);
            foreach (var item in querys)
            {
                union.UnionAll(item);
            }
            return union;
        }

        #endregion

    }
}
