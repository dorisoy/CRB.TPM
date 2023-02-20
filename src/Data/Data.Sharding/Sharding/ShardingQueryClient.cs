using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Sharding
{
    public class ShardingQueryClient
    {
        public ShardingQueryClient(params IClient[] clientList)
        {
            ClientList = clientList;
        }

        public IClient[] ClientList { get; }

        public async Task<int[]> ExecuteAsync(string sql, object param = null, int? timeout = null)
        {
            var taskList = ClientList.Select(s =>
            {
                return s.ExecuteAsync(sql, param, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<object[]> ExecuteScalarAsync(string sql, object param = null, int? timeout = null)
        {
            var taskList = ClientList.Select(s =>
            {
                return s.ExecuteScalarAsync(sql, param, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<TResult[]> ExecuteScalarAsync<TResult>(string sql, object param = null, int? timeout = null)
        {
            var taskList = ClientList.Select(s =>
            {
                return s.ExecuteScalarAsync<TResult>(sql, param, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<dynamic[]> QueryFirstOrDefaultAsync(string sql, object param = null, int? timeout = null)
        {
            var taskList = ClientList.Select(s =>
            {
                return s.QueryFirstOrDefaultAsync(sql, param, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<TResult[]> QueryFirstOrDefaultAsync<TResult>(string sql, object param = null, int? timeout = null)
        {
            var taskList = ClientList.Select(s =>
            {
                return s.QueryFirstOrDefaultAsync<TResult>(sql, param, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<IEnumerable<dynamic>[]> QueryAsync(string sql, object param = null, int? timeout = null)
        {
            var taskList = ClientList.Select(s =>
            {
                return s.QueryAsync(sql, param, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<IEnumerable<TResult>[]> QueryAsync<TResult>(string sql, object param = null, int? timeout = null)
        {
            var taskList = ClientList.Select(s =>
            {
                return s.QueryAsync<TResult>(sql, param, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<DataTable[]> QueryDataTableAsync(string sql, object param = null, int? timeout = null)
        {
            var taskList = ClientList.Select(s =>
            {
                return s.QueryDataTableAsync(sql, param, timeout);
            });
            return await Task.WhenAll(taskList);
        }

        public async Task<DataSet[]> QueryDataSetAsync(string sql, object param = null, int? timeout = null)
        {
            var taskList = ClientList.Select(s =>
            {
                return s.QueryDataSetAsync(sql, param, timeout);
            });
            return await Task.WhenAll(taskList);
        }
    }
}
