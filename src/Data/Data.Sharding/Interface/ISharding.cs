using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Sharding
{
    public abstract class ISharding<T> where T : class
    {
        public ISharding(ITable<T>[] list)
        {
            if (list[0].SqlField.IsIdentity)
            {
                throw new Exception("auto increment of primary key is not allowed");
            }
            TableList = list;
            Query = new ShardingQuery<T>(TableList);
            SqlField = list[0].SqlField;
        }

        #region base

        public ITable<T>[] TableList { get; }

        public ShardingQuery<T> Query { get; }

        protected SqlFieldEntity SqlField { get; }


        protected void Wrap(DistributedTransaction tran, Action action)
        {
            if (tran == null)
            {
                tran = new DistributedTransaction();
                try
                {
                    action();
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
                action();
            }
        }

        protected TResult Wrap<TResult>(DistributedTransaction tran, Func<TResult> func)
        {
            if (tran == null)
            {
                tran = new DistributedTransaction();
                TResult result;
                try
                {
                    result = func();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                return result;
            }
            return func();
        }

        public abstract ITable<T> GetTableById(object id);

        public abstract ITable<T> GetTableByModel(T model);

        public virtual Dictionary<ITable<T>, List<object>> GetTableByGroupIds(object ids)
        {
            var dict = new Dictionary<ITable<T>, List<object>>();
            var idsList = CommonUtil.GetMultiExec(ids);
            if (idsList != null)
            {
                foreach (var id in idsList)
                {
                    var table = GetTableById(id);
                    if (!dict.ContainsKey(table))
                    {
                        dict.Add(table, new List<object>());
                    }
                    dict[table].Add(id);
                }
            }
            return dict;
        }

        public virtual Dictionary<ITable<T>, List<T>> GetTableByGroupModelList(IEnumerable<T> modelList)
        {
            var dict = new Dictionary<ITable<T>, List<T>>();
            foreach (var item in modelList)
            {
                var table = GetTableByModel(item);
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

        public virtual void Insert(T model, DistributedTransaction tran = null, int? timeout = null)
        {
            var tb = GetTableByModel(model);
            tb.Insert(model, tran, timeout);
        }

        public virtual void Insert(IEnumerable<T> modelList, DistributedTransaction tran = null, int? timeout = null)
        {
            Wrap(tran, () =>
             {
                 var dict = GetTableByGroupModelList(modelList);
                 foreach (var item in dict)
                 {
                     item.Key.Insert(item.Value, tran, timeout);
                 }
             });
        }

        public virtual void InsertIfNoExists(T model, DistributedTransaction tran = null, int? timeout = null)
        {
            var tb = GetTableByModel(model);
            tb.InsertIfNoExists(model, tran, timeout);
        }

        public virtual void InsertIfNoExists(IEnumerable<T> modelList, DistributedTransaction tran = null, int? timeout = null)
        {
            Wrap(tran, () =>
             {
                 var dict = GetTableByGroupModelList(modelList);
                 foreach (var item in dict)
                 {
                     item.Key.InsertIfNoExists(item.Value, tran, timeout);
                 }
             });
        }

        public virtual void Merge(T model, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            var tb = GetTableByModel(model);
            tb.Merge(model, fields, tran, timeout);
        }

        public virtual void Merge(IEnumerable<T> modelList, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            Wrap(tran, () =>
             {
                 var dict = GetTableByGroupModelList(modelList);
                 foreach (var item in dict)
                 {
                     item.Key.Merge(item.Value, fields, tran, timeout);
                 }
             });
        }

        public virtual void MergeIgnore(T model, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            var tb = GetTableByModel(model);
            tb.MergeIgnore(model, fields, tran, timeout);
        }

        public virtual void MergeIgnore(IEnumerable<T> modelList, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            Wrap(tran, () =>
             {
                 var dict = GetTableByGroupModelList(modelList);
                 foreach (var item in dict)
                 {
                     item.Key.MergeIgnore(item.Value, fields, tran, timeout);
                 }
             });
        }

        #endregion

        #region update

        public virtual int Update(T model, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            var tb = GetTableByModel(model);
            return tb.Update(model, fields, tran, timeout);
        }

        public virtual void Update(IEnumerable<T> modelList, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            Wrap(tran, () =>
             {
                 var dict = GetTableByGroupModelList(modelList);
                 foreach (var item in dict)
                 {
                     item.Key.Update(item.Value, fields, tran, timeout);
                 }
             });
        }

        public virtual int UpdateIgnore(T model, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            var tb = GetTableByModel(model);
            return tb.UpdateIgnore(model, fields, tran, timeout);
        }

        public virtual void UpdateIgnore(IEnumerable<T> modelList, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            Wrap(tran, () =>
             {
                 var dict = GetTableByGroupModelList(modelList);
                 foreach (var item in dict)
                 {
                     item.Key.UpdateIgnore(item.Value, fields, tran, timeout);
                 }
             });
        }

        public int UpdateByWhere(T model, string where, List<string> fields = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return Wrap(tran, () =>
             {
                 int count = 0;
                 foreach (var item in TableList)
                 {
                     count += item.UpdateByWhere(model, where, fields, tran, timeout);
                 }
                 return count;
             });
        }


        public int UpdateByWhereIgnore(T model, string where, List<string> fields, DistributedTransaction tran = null, int? timeout = null)
        {
            return Wrap(tran, () =>
             {
                 int count = 0;
                 foreach (var item in TableList)
                 {
                     count += item.UpdateByWhereIgnore(model, where, fields, tran, timeout);
                 }
                 return count;
             });
        }

        #endregion

        #region delete

        public virtual int Delete(object id, DistributedTransaction tran = null, int? timeout = null)
        {
            var tb = GetTableById(id);
            return tb.Delete(id, tran, timeout);
        }

        public virtual void Delete(T model, DistributedTransaction tran = null, int? timeout = null)
        {
            var tb = GetTableByModel(model);
            tb.Delete(model, tran, timeout);
        }

        public virtual int DeleteByIds(object ids, DistributedTransaction tran = null, int? timeout = null)
        {
            if (CommonUtil.ObjectIsEmpty(ids))
                return 0;
            return Wrap(tran, () =>
             {
                 int count = 0;
                 var dict = GetTableByGroupIds(ids);
                 foreach (var item in dict)
                 {
                     count += item.Key.DeleteByIds(item.Value, tran, timeout);
                 }
                 return count;
             });
        }

        public virtual void Delete(IEnumerable<T> modelList, DistributedTransaction tran = null, int? timeout = null)
        {
            Wrap(tran, () =>
             {
                 var dict = GetTableByGroupModelList(modelList);
                 foreach (var item in dict)
                 {
                     item.Key.Delete(item.Value, tran, timeout);
                 }
             });
        }

        public int DeleteByWhere(string where, object param = null, DistributedTransaction tran = null, int? timeout = null)
        {
            return Wrap(tran, () =>
             {
                 int count = 0;
                 foreach (var item in TableList)
                 {
                     count += item.DeleteByWhere(where, param, tran, timeout);
                 }
                 return count;
             });
        }

        public int DeleteAll(DistributedTransaction tran = null, int? timeout = null)
        {
            return Wrap(tran, () =>
             {
                 int count = 0;
                 foreach (var item in TableList)
                 {
                     count += item.DeleteAll(tran, timeout);
                 }
                 return count;
             });
        }

        #endregion

        #region query

        public virtual Task<bool> ExistsAsync(object id)
        {
            return GetTableById(id).ExistsAsync(id);
        }

        public virtual Task<bool> ExistsAsync(T model)
        {
            return GetTableByModel(model).ExistsAsync(model);
        }

        public virtual Task<T> GetByIdAsync(object id, string returnFields = null)
        {
            return GetTableById(id).GetByIdAsync(id, returnFields);
        }

        public virtual async Task<IEnumerable<T>> GetByIdsAsync(object ids, string returnFields = null)
        {
            if (CommonUtil.ObjectIsEmpty(ids))
                return Enumerable.Empty<T>();
            var dict = GetTableByGroupIds(ids);
            var taskList = dict.Select(s =>
            {
                return s.Key.GetByIdsAsync(s.Value, returnFields);
            });
            var result = await Task.WhenAll(taskList);
            return result.ConcatItem();
        }

        public async Task Truncate()
        {
            var taskList = TableList.Select(s =>
            {
                return Task.Run(() =>
                {
                    s.Truncate();
                });
            });
            await Task.WhenAll(taskList);
        }

        #endregion

    }
}
