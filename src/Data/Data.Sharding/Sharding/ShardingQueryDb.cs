using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Sharding
{
    public class ShardingQueryDb
    {
        public ShardingQueryDb(params IDatabase[] dbList)
        {
            DbList = dbList;
        }

        public IDatabase[] DbList { get; }

        public async Task<int[]> ExecuteAsync(string sql, object param = null, int? timeout = null)
        {
            var taskList = DbList.Select(s =>
            {
                return s.ExecuteAsync(sql, param, null, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<object[]> ExecuteScalarAsync(string sql, object param = null, int? timeout = null)
        {
            var taskList = DbList.Select(s =>
            {
                return s.ExecuteScalarAsync(sql, param, null, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<TResult[]> ExecuteScalarAsync<TResult>(string sql, object param = null, int? timeout = null)
        {
            var taskList = DbList.Select(s =>
            {
                return s.ExecuteScalarAsync<TResult>(sql, param, null, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<dynamic[]> QueryFirstOrDefaultAsync(string sql, object param = null, int? timeout = null)
        {
            var taskList = DbList.Select(s =>
            {
                return s.QueryFirstOrDefaultAsync(sql, param, null, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<TResult[]> QueryFirstOrDefaultAsync<TResult>(string sql, object param = null, int? timeout = null)
        {
            var taskList = DbList.Select(s =>
            {
                return s.QueryFirstOrDefaultAsync<TResult>(sql, param, null, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<IEnumerable<dynamic>[]> QueryAsync(string sql, object param = null, int? timeout = null)
        {
            var taskList = DbList.Select(s =>
            {
                return s.QueryAsync(sql, param, null, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<IEnumerable<TResult>[]> QueryAsync<TResult>(string sql, object param = null, int? timeout = null)
        {
            var taskList = DbList.Select(s =>
            {
                return s.QueryAsync<TResult>(sql, param, null, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<DataTable[]> QueryDataTableAsync(string sql, object param = null, int? timeout = null)
        {
            var taskList = DbList.Select(s =>
            {
                return s.QueryDataTableAsync(sql, param, null, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<DataSet[]> QueryDataSetAsync(string sql, object param = null, int? timeout = null)
        {
            var taskList = DbList.Select(s =>
            {
                return s.QueryDataSetAsync(sql, param, null, timeout);
            });
            return await Task.WhenAll(taskList);
        }
    }
}
