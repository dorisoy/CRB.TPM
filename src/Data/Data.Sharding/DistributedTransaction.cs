using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Sharding
{

    /*
    public class DistributedTransaction
    {
        /// <summary>
        /// 数据库操作
        /// </summary>
        private IDatabase defaultDb = null;
        private (IDbConnection, IDbTransaction) defaultVal = default;
        private Dictionary<IDatabase, (IDbConnection, IDbTransaction)> dict = null;

        /// <summary>
        /// 是否返回结构
        /// </summary>
        public bool ShowResult { get; set; }
        public ConcurrentDictionary<IDatabase, bool> Result;

        /// <summary>
        /// 创建带事务连接
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        private (IDbConnection, IDbTransaction) CreateConnAndTran(IDatabase db)
        {
            IDbConnection conn = null;
            IDbTransaction tran = null;
            try
            {
                conn = db.GetConn();
                tran = conn.BeginTransaction();
                return (conn, tran);
            }
            catch
            {
                if (tran != null)
                {
                    tran.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
                throw;
            }
        }

        /// <summary>
        /// 异步创建带事务连接
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        private async Task<(IDbConnection, IDbTransaction)> CreateConnAndTranAsync(IDatabase db)
        {
            IDbConnection conn = null;
            IDbTransaction tran = null;
            try
            {
                conn = await db.GetConnAsync();
                tran = conn.BeginTransaction();
                return (conn, tran);
            }
            catch
            {
                if (tran != null)
                {
                    tran.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
                throw;
            }

        }

        /// <summary>
        /// 获取事务连接
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public (IDbConnection, IDbTransaction) GetVal(IDatabase db)
        {
            //第一次初始化
            if (defaultDb == null)
            {
                IDbConnection conn = null;
                IDbTransaction tran = null;
                try
                {
                    conn = db.GetConn();
                    tran = conn.BeginTransaction();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Dispose();
                    }
                    if (conn != null)
                    {
                        conn.Dispose();
                    }
                    throw;
                }
                defaultDb = db;
                defaultVal.Item1 = conn;
                defaultVal.Item2 = tran;
                return defaultVal;
            }
            else if (defaultDb.Equals(db))
            {
                return defaultVal;
            }
            else
            {
                //dict为null第一次创建
                if (dict == null)
                {
                    dict = new Dictionary<IDatabase, (IDbConnection, IDbTransaction)>();
                    var val = CreateConnAndTran(db);
                    dict.Add(db, val);
                    return val;
                }
                else
                {
                    var ok = dict.TryGetValue(db, out var val);
                    if (!ok)
                    {
                        val = CreateConnAndTran(db);
                        dict.Add(db, val);
                    }
                    return val;
                }
            }
        }

        /// <summary>
        /// 异步获取事务连接
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<(IDbConnection, IDbTransaction)> GetValAsync(IDatabase db)
        {
            if (defaultDb == null) //第一次初始化
            {
                IDbConnection conn = null;
                IDbTransaction tran = null;
                try
                {
                    conn = await db.GetConnAsync();
                    tran = conn.BeginTransaction();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Dispose();
                    }
                    if (conn != null)
                    {
                        conn.Dispose();
                    }
                    throw;
                }
                defaultDb = db;
                defaultVal.Item1 = conn;
                defaultVal.Item2 = tran;
                return defaultVal;
            }
            else if (defaultDb.Equals(db))
            {
                return defaultVal;
            }
            else
            {
                if (dict == null)//dict为null第一次创建
                {
                    dict = new Dictionary<IDatabase, (IDbConnection, IDbTransaction)>();
                    var val = await CreateConnAndTranAsync(db);
                    dict.Add(db, val);
                    return val;
                }
                else
                {
                    var ok = dict.TryGetValue(db, out var val);
                    if (!ok)
                    {
                        val = await CreateConnAndTranAsync(db);
                        dict.Add(db, val);
                    }
                    return val;
                }
            }
        }

        /// <summary>
        /// 获取处理结果
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool GetResult(IDatabase db)
        {
            if (!ShowResult)
            {
                throw new Exception("需要配置参数：ShowResult=true");
            }
            Result.TryGetValue(db, out var val);
            return val;
        }

        /// <summary>
        /// 添加结果
        /// </summary>
        /// <param name="db"></param>
        /// <param name="ok"></param>
        private void AddResult(IDatabase db, bool ok)
        {
            if (ShowResult)
            {
                if (Result == null)
                {
                    Result = new ConcurrentDictionary<IDatabase, bool>();
                }
                Result.TryAdd(db, ok);
            }
        }

        /// <summary>
        ///  提交事务
        /// </summary>
        public void Commit()
        {
            if (defaultDb != null)
            {
                try
                {
                    defaultVal.Item2.Commit();
                    AddResult(defaultDb, true);
                }
                catch
                {
                    //如果第一个提交失败，抛出异常执行Rollback全部回滚
                    throw;
                }
                defaultVal.Item2.Dispose();
                defaultVal.Item1.Dispose();
                defaultDb = null;
            }
            if (dict != null && dict.Count > 0)
            {
                foreach (var d in dict)
                {
                    var item = d.Value;
                    try
                    {
                        item.Item2.Commit();
                        AddResult(d.Key, true);
                    }
                    catch
                    {
                        try
                        {
                            AddResult(d.Key, false);
                            //回滚事务
                            item.Item2.Rollback();
                        }
                        catch { }
                    }
                    finally
                    {
                        //释放
                        item.Item2.Dispose();
                        item.Item1.Dispose();
                    }
                }
                dict.Clear();
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            if (defaultDb != null)
            {
                try
                {
                    AddResult(defaultDb, false);
                    //回滚事务
                    defaultVal.Item2.Rollback();
                }
                finally
                {
                    //释放
                    defaultVal.Item2.Dispose();
                    defaultVal.Item1.Dispose();
                }
                defaultDb = null;
            }
            if (dict != null && dict.Count > 0)
            {
                foreach (var d in dict)
                {
                    var item = d.Value;
                    try
                    {
                        AddResult(d.Key, false);
                        //回滚事务
                        item.Item2.Rollback();
                    }
                    finally
                    {
                        //释放
                        item.Item2.Dispose();
                        item.Item1.Dispose();
                    }
                }
                dict.Clear();
            }
        }
    }
    */


    /// <summary>
    /// 表示分布式事务
    /// </summary>
    public class DistributedTransaction : IDisposable
    {
        private IDatabase defaultDb = null;
        private (IDbConnection, IDbTransaction) defaultVal = default;
        private Dictionary<IDatabase, (IDbConnection, IDbTransaction)> dict = null;

        private static (IDbConnection, IDbTransaction) CreateConnAndTran(IDatabase db)
        {
            IDbConnection conn = null;
            IDbTransaction tran = null;
            try
            {
                conn = db.GetConn();
                tran = conn.BeginTransaction();
                return (conn, tran);
            }
            catch (Exception ex)
            {
                if (tran != null)
                {
                    tran.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
                throw ex;
            }
        }

        private static async Task<(IDbConnection, IDbTransaction)> CreateConnAndTranAsync(IDatabase db)
        {
            IDbConnection conn = null;
            IDbTransaction tran = null;
            try
            {
                conn = await db.GetConnAsync();
                tran = conn.BeginTransaction();
                return (conn, tran);
            }
            catch (Exception ex)
            {
                if (tran != null)
                {
                    tran.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
                throw ex;
            }

        }

        public (IDbConnection, IDbTransaction) GetVal(IDatabase db)
        {
            if (defaultDb == null) //第一次初始化
            {
                IDbConnection conn = null;
                IDbTransaction tran = null;
                try
                {
                    conn = db.GetConn();
                    tran = conn.BeginTransaction();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Dispose();
                    }
                    if (conn != null)
                    {
                        conn.Dispose();
                    }
                    throw;
                }
                defaultDb = db;
                defaultVal.Item1 = conn;
                defaultVal.Item2 = tran;
                return defaultVal;
            }
            else if (defaultDb.Equals(db))
            {
                return defaultVal;
            }
            else
            {
                if (dict == null)//dict为null第一次创建
                {
                    dict = new Dictionary<IDatabase, (IDbConnection, IDbTransaction)>();
                    var val = CreateConnAndTran(db);
                    dict.Add(db, val);
                    return val;
                }
                else
                {
                    var ok = dict.TryGetValue(db, out var val);
                    if (!ok)
                    {
                        val = CreateConnAndTran(db);
                        dict.Add(db, val);
                    }
                    return val;
                }
            }
        }

        public async Task<(IDbConnection, IDbTransaction)> GetValAsync(IDatabase db)
        {
            if (defaultDb == null) //第一次初始化
            {
                IDbConnection conn = null;
                IDbTransaction tran = null;
                try
                {
                    conn = await db.GetConnAsync();
                    tran = conn.BeginTransaction();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Dispose();
                    }
                    if (conn != null)
                    {
                        conn.Dispose();
                    }
                    throw;
                }
                defaultDb = db;
                defaultVal.Item1 = conn;
                defaultVal.Item2 = tran;
                return defaultVal;
            }
            else if (defaultDb.Equals(db))
            {
                return defaultVal;
            }
            else
            {
                if (dict == null)//dict为null第一次创建
                {
                    dict = new Dictionary<IDatabase, (IDbConnection, IDbTransaction)>();
                    var val = await CreateConnAndTranAsync(db);
                    dict.Add(db, val);
                    return val;
                }
                else
                {
                    var ok = dict.TryGetValue(db, out var val);
                    if (!ok)
                    {
                        val = await CreateConnAndTranAsync(db);
                        dict.Add(db, val);
                    }
                    return val;
                }
            }
        }

        public bool ShowResult { get; set; }

        public ConcurrentDictionary<IDatabase, bool> Result;

        public bool GetResult(IDatabase db)
        {
            if (!ShowResult)
            {
                throw new Exception("you must set ShowResult=true");
            }
            Result.TryGetValue(db, out var val);
            return val;
        }

        private void AddResult(IDatabase db, bool ok)
        {
            if (ShowResult)
            {
                if (Result == null)
                {
                    Result = new ConcurrentDictionary<IDatabase, bool>();
                }
                Result.TryAdd(db, ok);
            }
        }

        public void Commit()
        {
            if (defaultDb != null)
            {
                try
                {
                    defaultVal.Item2.Commit();
                    AddResult(defaultDb, true);
                }
                catch
                {
                    throw; //如果第一个提交失败，抛出异常执行Rollback全部回滚
                }
                defaultVal.Item2.Dispose();
                defaultVal.Item1.Dispose();
                defaultDb = null;
                defaultVal.Item2 = null;
                defaultVal.Item1 = null;
            }
            if (dict != null && dict.Count > 0)
            {
                foreach (var d in dict)
                {
                    var item = d.Value;
                    try
                    {
                        item.Item2.Commit();
                        AddResult(d.Key, true);
                    }
                    catch
                    {
                        try
                        {
                            AddResult(d.Key, false);
                            item.Item2.Rollback();
                        }
                        catch { }
                    }
                    finally
                    {
                        item.Item2.Dispose();
                        item.Item1.Dispose();
                    }
                }
                dict.Clear();
                dict = null;
            }
            done = true;
        }

        public void Rollback()
        {
            if (defaultDb != null)
            {
                try
                {
                    AddResult(defaultDb, false);
                    defaultVal.Item2.Rollback();
                }
                finally
                {
                    defaultVal.Item2.Dispose();
                    defaultVal.Item1.Dispose();
                }
                defaultDb = null;
                defaultVal.Item2 = null;
                defaultVal.Item1 = null;
            }
            if (dict != null && dict.Count > 0)
            {
                foreach (var d in dict)
                {
                    var item = d.Value;
                    try
                    {
                        AddResult(d.Key, false);
                        item.Item2.Rollback();
                    }
                    finally
                    {
                        item.Item2.Dispose();
                        item.Item1.Dispose();
                    }
                }
                dict.Clear();
                dict = null;
            }
            done = true;
        }

        private bool done = false;

        #region Dispose

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (!done)
                    {
                        Rollback();
                    }
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
