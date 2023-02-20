using FastMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Sharding
{
    public class AutoSharding<T> : ISharding<T> where T : class
    {
        public AutoSharding(ITable<T>[] tableList) : base(tableList)
        {

        }

        private static readonly string _msg = "AutoSharding not support";

        #region base

        public override ITable<T> GetTableById(object id)
        {
            var taskList = TableList.Select(s =>
            {
                return s.ExistsAsync(id);
            });

            var result = Task.WhenAll(taskList).Result;
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i])
                {
                    return TableList[i];
                }
            }
            return null;
        }

        public override ITable<T> GetTableByModel(T model)
        {
            var accessor = TypeAccessor.Create(typeof(T));
            var id = accessor[model, SqlField.PrimaryKey];
            return GetTableById(id);
        }

        public override Dictionary<ITable<T>, List<object>> GetTableByGroupIds(object ids)
        {
            throw new Exception(_msg);
        }

        public override Dictionary<ITable<T>, List<T>> GetTableByGroupModelList(IEnumerable<T> modelList)
        {
            throw new Exception(_msg);
        }


        public ITable<T> _GetTableById(object id)
        {
            return TableList[ShardingUtils.Mod(id, TableList.Length)];
        }

        public ITable<T> _GetTableByModel(T model)
        {
            return TableList[ShardingUtils.Mod(model, SqlField.PrimaryKey, SqlField.PrimaryKeyType, TableList.Length)];
        }

        public Dictionary<ITable<T>, List<object>> _GetTableByGroupIds(object ids)
        {
            var dict = new Dictionary<ITable<T>, List<object>>();
            var idsList = CommonUtil.GetMultiExec(ids);
            if (idsList != null)
            {
                foreach (var id in idsList)
                {
                    var table = _GetTableById(id);
                    if (!dict.ContainsKey(table))
                    {
                        dict.Add(table, new List<object>());
                    }
                    dict[table].Add(id);
                }
            }
            return dict;
        }

        public Dictionary<ITable<T>, List<T>> _GetTableByGroupModelList(IEnumerable<T> modelList)
        {
            var dict = new Dictionary<ITable<T>, List<T>>();
            foreach (var item in modelList)
            {
                var table = _GetTableByModel(item);
                if (!dict.ContainsKey(table))
                {
                    dict.Add(table, new List<T>());
                }
                dict[table].Add(item);
            }
            return dict;
        }

        #endregion

        #region insert

        public override void Insert(T model, DistributedTransaction tran = null, int? timeout = null)
        {
            var tb = _GetTableByModel(model);
            tb.Insert(model, tran, timeout);
        }

        public override void Insert(IEnumerable<T> modelList, DistributedTransaction tran = null, int? timeout = null)
        {
            Wrap(tran, () =>
             {
                 var dict = _GetTableByGroupModelList(modelList);
                 foreach (var item in dict)
                 {
                     item.Key.Insert(item.Value, tran, timeout);
                 }
             });
        }

        public override void InsertIfNoExists(T model, DistributedTransaction tran = null, int? timeout = null)
        {
            if (!ExistsAsync(model).Result)
            {
                Insert(model);
            }
        }

        public override void InsertIfNoExists(IEnumerable<T> modelList, DistributedTransaction tran = null, int? timeout = null)
        {
            foreach (var item in modelList)
            {
                InsertIfNoExists(item);
            }
        }

        public override void Merge(T model, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            if (ExistsAsync(model).Result)
            {
                Update(model, fields);
            }
            else
            {
                Insert(model);
            }
        }

        public override void Merge(IEnumerable<T> modelList, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            foreach (var item in modelList)
            {
                Merge(item, fields);
            }
        }

        public override void MergeIgnore(T model, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            if (ExistsAsync(model).Result)
            {
                UpdateIgnore(model, fields);
            }
            else
            {
                Insert(model);
            }
        }

        public override void MergeIgnore(IEnumerable<T> modelList, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            foreach (var item in modelList)
            {
                MergeIgnore(item, fields);
            }
        }

        #endregion

        #region update

        public override int Update(T model, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            int count = 0;
            Wrap(tran, () =>
             {
                 foreach (var item in TableList)
                 {
                     count += item.Update(model, fields, tran, timeout);
                 }
             });
            return count;
        }

        public override void Update(IEnumerable<T> modelList, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            Wrap(tran, () =>
             {
                 foreach (var item in TableList)
                 {
                     item.Update(modelList, fields, tran, timeout);
                 }
             });
        }

        public override int UpdateIgnore(T model, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            int count = 0;
            Wrap(tran, () =>
             {
                 foreach (var item in TableList)
                 {
                     count += item.UpdateIgnore(model, fields, tran, timeout);
                 }
             });
            return count;
        }

        public override void UpdateIgnore(IEnumerable<T> modelList, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            Wrap(tran, () =>
             {
                 foreach (var item in TableList)
                 {
                     item.UpdateIgnore(modelList, fields, tran, timeout);
                 }
             });
        }

        #endregion

        #region delete

        public override int Delete(object id, DistributedTransaction tran = null, int? timeout = null)
        {
            int count = 0;
            Wrap(tran, () =>
             {
                 foreach (var item in TableList)
                 {
                     count += item.Delete(id, tran, timeout);
                 }
             });
            return count;
        }


        public override void Delete(T model, DistributedTransaction tran = null, int? timeout = null)
        {
            Wrap(tran, () =>
             {
                 foreach (var item in TableList)
                 {
                     item.Delete(model, tran, timeout);
                 }
             });
        }

        public override int DeleteByIds(object ids, DistributedTransaction tran = null, int? timeout = null)
        {
            int count = 0;
            Wrap(tran, () =>
             {
                 foreach (var item in TableList)
                 {
                     count += item.DeleteByIds(ids, tran, timeout);
                 }
             });
            return count;
        }

        public override void Delete(IEnumerable<T> modelList, DistributedTransaction tran = null, int? timeout = null)
        {
            Wrap(tran, () =>
             {
                 foreach (var item in TableList)
                 {
                     item.Delete(modelList, tran, timeout);
                 }
             });
        }

        #endregion

        #region query

        public override Task<bool> ExistsAsync(object id)
        {
            return Query.ExistsAsync(id);
        }

        public override Task<bool> ExistsAsync(T model)
        {
            return Query.ExistsAsync(model);
        }

        public override Task<T> GetByIdAsync(object id, string returnFields = null)
        {
            return Query.GetByIdAsync(id, returnFields);
        }

        public override async Task<IEnumerable<T>> GetByIdsAsync(object ids, string returnFields = null)
        {
            return await Query.GetByIdsAsync(ids, returnFields);
        }

        #endregion
    }
}
