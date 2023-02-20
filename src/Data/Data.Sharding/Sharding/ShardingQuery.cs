using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Sharding
{
    public class ShardingQuery<T> where T : class
    {
        public ShardingQuery(params ITable<T>[] tableList)
        {
            TableList = tableList;
            SqlField = tableList[0].SqlField;
        }

        public SqlFieldEntity SqlField { get; }

        public ITable<T>[] TableList { get; }

        #region dapper method

        private static string InitTable(string sql, string name)
        {
            return sql.Replace("$table", name);
        }

        public async Task<int[]> ExecuteAsync(string sql, object param = null, int? timeout = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.DataBase.ExecuteAsync(InitTable(sql, s.Name), param, null, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<object[]> ExecuteScalarAsync(string sql, object param = null, int? timeout = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.DataBase.ExecuteScalarAsync(InitTable(sql, s.Name), param, null, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<TResult[]> ExecuteScalarAsync<TResult>(string sql, object param = null, int? timeout = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.DataBase.ExecuteScalarAsync<TResult>(InitTable(sql, s.Name), param, null, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<dynamic[]> QueryFirstOrDefaultAsync(string sql, object param = null, int? timeout = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.DataBase.QueryFirstOrDefaultAsync(InitTable(sql, s.Name), param, null, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<TResult[]> QueryFirstOrDefaultAsync<TResult>(string sql, object param = null, int? timeout = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.DataBase.QueryFirstOrDefaultAsync<TResult>(InitTable(sql, s.Name), param, null, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<IEnumerable<dynamic>[]> QueryAsync(string sql, object param = null, int? timeout = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.DataBase.QueryAsync(InitTable(sql, s.Name), param, null, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<IEnumerable<TResult>[]> QueryAsync<TResult>(string sql, object param = null, int? timeout = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.DataBase.QueryAsync<TResult>(InitTable(sql, s.Name), param, null, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<DataTable[]> QueryDataTableAsync(string sql, object param = null, int? timeout = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.DataBase.QueryDataTableAsync(InitTable(sql, s.Name), param, null, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<DataSet[]> QueryDataSetAsync(string sql, object param = null, int? timeout = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.DataBase.QueryDataSetAsync(InitTable(sql, s.Name), param, null, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        #endregion

        public async Task<bool> ExistsAsync(object id, int? timeout = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.ExistsAsync(id, timeout: timeout);
            });
            var result = await Task.WhenAll(taskList);
            return result.Any(a => a == true);
        }

        public async Task<bool> ExistsAsync(T model, int? timeout = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.ExistsAsync(model, timeout: timeout);
            });
            var result = await Task.WhenAll(taskList);
            return result.Any(a => a == true);
        }

        public async Task<long> CountAsync(string where = null, object param = null, int? timeout = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.CountAsync(where, param, timeout: timeout);
            });
            var result = await Task.WhenAll(taskList);
            return result.Sum();
        }

        public async Task<TResult> MinAsync<TResult>(string field, string where = null, object param = null, int? timeout = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.MinAsync<TResult>(field, where, param, timeout: timeout);
            });
            var result = await Task.WhenAll(taskList);
            return result.Min();
        }

        public async Task<TResult> MaxAsync<TResult>(string field, string where = null, object param = null, int? timeout = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.MaxAsync<TResult>(field, where, param, timeout: timeout);
            });
            var result = await Task.WhenAll(taskList);
            return result.Max();
        }

        public async Task<TResult[]> SumListAsync<TResult>(string field, string where = null, object param = null, int? timeout = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.SumAsync<TResult>(field, where, param, timeout: timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<int> SumIntAsync(string field, string where = null, object param = null, int? timeout = null)
        {
            var data = await SumListAsync<int>(field, where, param, timeout: timeout);
            return data.Sum();
        }

        public async Task<long> SumLongAsync(string field, string where = null, object param = null, int? timeout = null)
        {
            var data = await SumListAsync<long>(field, where, param, timeout: timeout);
            return data.Sum();
        }

        public async Task<float> SumFloatAsync(string field, string where = null, object param = null, int? timeout = null)
        {
            var data = await SumListAsync<float>(field, where, param, timeout: timeout);
            return data.Sum();
        }

        public async Task<double> SumDoubleAsync(string field, string where = null, object param = null, int? timeout = null)
        {
            var data = await SumListAsync<double>(field, where, param, timeout: timeout);
            return data.Sum();
        }

        public async Task<decimal> SumDecimalAsync(string field, string where = null, object param = null, int? timeout = null)
        {
            var data = await SumListAsync<decimal>(field, where, param, timeout: timeout);
            return data.Sum();
        }

        public async Task<decimal> AvgAsync(string field, string where = null, object param = null, int? timeout = null)
        {
            var countTask = CountAsync(where, param);
            var sumTask = SumDecimalAsync(field, where, param, timeout: timeout);
            await Task.WhenAll(countTask, sumTask);
            var count = countTask.Result;
            if (count == 0)
            {
                return 0;
            }
            return sumTask.Result / count;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string returnFields = null, string orderby = null, int? timeout = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.GetAllAsync(returnFields, orderby, timeout: timeout);
            });
            if (string.IsNullOrEmpty(orderby))
            {
                orderby = SqlField.PrimaryKey;
            }
            var result = await Task.WhenAll(taskList);
            return result.ConcatItem().AsQueryable().OrderBy(orderby).AsEnumerable<T>();
        }

        public async Task<T> GetByIdAsync(object id, string returnFields = null, int? timeout = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.GetByIdAsync(id, returnFields, timeout: timeout);
            });
            var result = await Task.WhenAll(taskList);
            return result.FirstOrDefault(f => f != null);
        }

        public async Task<IEnumerable<T>> GetByIdsAsync(object ids, string returnFields = null, int? timeout = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.GetByIdsAsync(ids, returnFields, timeout: timeout);
            });
            var result = await Task.WhenAll(taskList);
            return result.ConcatItem();
        }

        public async Task<IEnumerable<T>> GetByIdsWithFieldAsync(object ids, string field, string returnFields = null, int? timeout = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.GetByIdsWithFieldAsync(ids, field, returnFields, timeout: timeout);
            });
            var result = await Task.WhenAll(taskList);
            return result.ConcatItem();
        }

        public async Task<IEnumerable<T>> GetByWhereAsync(string where, object param = null, string returnFields = null, string orderby = null, int limit = 0, int? timeout = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.GetByWhereAsync(where, param, returnFields, orderby, limit, timeout: timeout);
            });
            var result = await Task.WhenAll(taskList);
            if (string.IsNullOrEmpty(orderby))
            {
                orderby = SqlField.PrimaryKey;
            }

            if (limit != 0)
            {
                return result.ConcatItem().AsQueryable().OrderBy(orderby).Take(limit).AsEnumerable<T>();
            }
            return result.ConcatItem().AsQueryable().OrderBy(orderby).AsEnumerable<T>();
        }

        public async Task<T> GetByWhereFirstAsync(string where, object param = null, string returnFields = null, int? timeout = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.GetByWhereFirstAsync(where, param, returnFields, timeout: timeout);
            });
            var result = await Task.WhenAll(taskList);
            return result.FirstOrDefault(f => f != null);
        }

        public async Task<IEnumerable<T>> GetBySkipTakeAsync(int skip, int take, string where = null, object param = null, string returnFields = null, string orderby = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.GetByWhereAsync(where, param, returnFields, orderby, skip + take);
            });
            var result = await Task.WhenAll(taskList);
            if (string.IsNullOrEmpty(orderby))
            {
                orderby = SqlField.PrimaryKey;
            }
            return result.ConcatItem().AsQueryable().OrderBy(orderby).Skip(skip).Take(take).AsEnumerable<T>();
        }

        public async Task<IEnumerable<T>> GetByPageAsync(int page, int pageSize, string where = null, object param = null, string returnFields = null, string orderby = null)
        {
            int skip = 0;
            if (page > 0)
            {
                skip = (page - 1) * pageSize;
            }
            return await GetBySkipTakeAsync(skip, pageSize, where, param, returnFields, orderby);
        }

        public async Task<PageEntity<T>> GetByPageAndCountAsync(int page, int pageSize, string where = null, object param = null, string returnFields = null, string orderby = null)
        {
            var task1 = GetByPageAsync(page, pageSize, where, param, returnFields, orderby);
            var task2 = CountAsync(where, param);
            await Task.WhenAll(task1, task2);
            var pageEntity = new PageEntity<T>
            {
                Data = task1.Result,
                Count = task2.Result
            };
            return pageEntity;
        }

        public async Task<IEnumerable<T>> GetByAscFirstPageAsync(int pageSize, object param = null, string and = null, string returnFields = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.GetByAscFirstPageAsync(pageSize, param, and, returnFields);
            });
            var result = await Task.WhenAll(taskList);
            return result.ConcatItem().AsQueryable().OrderBy(SqlField.PrimaryKey).Take(pageSize).AsEnumerable<T>();
        }

        public async Task<IEnumerable<T>> GetByAscPrevPageAsync(int pageSize, object param, string and = null, string returnFields = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.GetByAscPrevPageAsync(pageSize, param, and, returnFields);
            });
            var result = await Task.WhenAll(taskList);
            return result.ConcatItem().AsQueryable().OrderBy(SqlField.PrimaryKey + " DESC").Take(pageSize).OrderBy(SqlField.PrimaryKey).AsEnumerable<T>();
        }

        public async Task<IEnumerable<T>> GetByAscCurrentPageAsync(int pageSize, object param, string and = null, string returnFields = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.GetByAscCurrentPageAsync(pageSize, param, and, returnFields);
            });
            var result = await Task.WhenAll(taskList);
            return result.ConcatItem().AsQueryable().OrderBy(SqlField.PrimaryKey).Take(pageSize).AsEnumerable<T>();
        }

        public async Task<IEnumerable<T>> GetByAscNextPageAsync(int pageSize, object param, string and = null, string returnFields = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.GetByAscNextPageAsync(pageSize, param, and, returnFields);
            });
            var result = await Task.WhenAll(taskList);
            return result.ConcatItem().AsQueryable().OrderBy(SqlField.PrimaryKey).Take(pageSize).AsEnumerable<T>();
        }

        public async Task<IEnumerable<T>> GetByAscLastPageAsync(int pageSize, object param = null, string and = null, string returnFields = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.GetByAscLastPageAsync(pageSize, param, and, returnFields);
            });
            var result = await Task.WhenAll(taskList);
            return result.ConcatItem().AsQueryable().OrderBy(SqlField.PrimaryKey + " DESC").Take(pageSize).OrderBy(SqlField.PrimaryKey).AsEnumerable<T>();
        }

        public async Task<IEnumerable<T>> GetByDescFirstPageAsync(int pageSize, object param = null, string and = null, string returnFields = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.GetByDescFirstPageAsync(pageSize, param, and, returnFields);
            });
            var result = await Task.WhenAll(taskList);
            return result.ConcatItem().AsQueryable().OrderBy(SqlField.PrimaryKey + " DESC").Take(pageSize).AsEnumerable<T>();
        }

        public async Task<IEnumerable<T>> GetByDescPrevPageAsync(int pageSize, object param, string and = null, string returnFields = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.GetByDescPrevPageAsync(pageSize, param, and, returnFields);
            });
            var result = await Task.WhenAll(taskList);
            return result.ConcatItem().AsQueryable().OrderBy(SqlField.PrimaryKey).Take(pageSize).OrderBy(SqlField.PrimaryKey + " DESC").AsEnumerable<T>();
        }

        public async Task<IEnumerable<T>> GetByDescCurrentPageAsync(int pageSize, object param, string and = null, string returnFields = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.GetByDescCurrentPageAsync(pageSize, param, and, returnFields);
            });
            var result = await Task.WhenAll(taskList);
            return result.ConcatItem().AsQueryable().OrderBy(SqlField.PrimaryKey + " DESC").Take(pageSize).AsEnumerable<T>();
        }

        public async Task<IEnumerable<T>> GetByDescNextPageAsync(int pageSize, object param, string and = null, string returnFields = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.GetByDescNextPageAsync(pageSize, param, and, returnFields);
            });
            var result = await Task.WhenAll(taskList);
            return result.ConcatItem().AsQueryable().OrderBy(SqlField.PrimaryKey + " DESC").Take(pageSize).AsEnumerable<T>();
        }

        public async Task<IEnumerable<T>> GetByDescLastPageAsync(int pageSize, object param = null, string and = null, string returnFields = null)
        {
            var taskList = TableList.Select(s =>
            {
                return s.GetByDescLastPageAsync(pageSize, param, and, returnFields);
            });
            var result = await Task.WhenAll(taskList);
            return result.ConcatItem().AsQueryable().OrderBy(SqlField.PrimaryKey).Take(pageSize).OrderBy(SqlField.PrimaryKey + " DESC").AsEnumerable<T>();

        }

        public Task<IEnumerable<T>> GetByAscDescPageAsync(bool asc, AscDescPage adPage, int pageSize, object param, string and = null, string returnFields = null)
        {
            if (asc)
            {
                switch (adPage)
                {
                    case AscDescPage.Fist:
                        return GetByAscFirstPageAsync(pageSize, param, and, returnFields);
                    case AscDescPage.Prev:
                        return GetByAscPrevPageAsync(pageSize, param, and, returnFields);
                    case AscDescPage.Current:
                        return GetByAscCurrentPageAsync(pageSize, param, and, returnFields);
                    case AscDescPage.Next:
                        return GetByAscNextPageAsync(pageSize, param, and, returnFields);
                    case AscDescPage.Last:
                        return GetByAscLastPageAsync(pageSize, param, and, returnFields);
                }
            }
            else
            {
                switch (adPage)
                {
                    case AscDescPage.Fist:
                        return GetByDescFirstPageAsync(pageSize, param, and, returnFields);
                    case AscDescPage.Prev:
                        return GetByDescPrevPageAsync(pageSize, param, and, returnFields);
                    case AscDescPage.Current:
                        return GetByDescCurrentPageAsync(pageSize, param, and, returnFields);
                    case AscDescPage.Next:
                        return GetByDescNextPageAsync(pageSize, param, and, returnFields);
                    case AscDescPage.Last:
                        return GetByDescLastPageAsync(pageSize, param, and, returnFields);
                }
            }
            return null;
        }

    }
}
