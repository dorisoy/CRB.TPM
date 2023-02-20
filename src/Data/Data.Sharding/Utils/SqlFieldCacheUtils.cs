using System;
using System.Collections.Concurrent;

namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    /// 用于SQL实体缓存
    /// </summary>
    internal class SqlFieldCacheUtils
    {
        #region Mysql

        private static readonly ConcurrentDictionary<RuntimeTypeHandle, SqlFieldEntity> MySqlDict = new ConcurrentDictionary<RuntimeTypeHandle, SqlFieldEntity>();

        private static readonly object _mysqlLocker = new object();

        public static SqlFieldEntity GetMySqlFieldEntity<T>()
        {
            var typeHandle = typeof(T).TypeHandle;
            var exists = MySqlDict.TryGetValue(typeHandle, out var val);
            if (!exists)
            {
                lock (_mysqlLocker)
                {
                    if (!MySqlDict.ContainsKey(typeHandle))
                    {
                        MySqlDict.TryAdd(typeHandle, new SqlFieldEntity(ClassToTableEntityUtils.Get<T>(DataBaseType.MySql), "`", "`", "@"));
                    }
                }
                val = MySqlDict[typeHandle];
            }
            return val;
        }

        #endregion

        #region SqlServer

        private static readonly ConcurrentDictionary<RuntimeTypeHandle, SqlFieldEntity> SqlServerDict = new ConcurrentDictionary<RuntimeTypeHandle, SqlFieldEntity>();

        private static readonly object _sqlserverLocker = new object();

        public static SqlFieldEntity GetSqlServerFieldEntity<T>()
        {
            var typeHandle = typeof(T).TypeHandle;
            var exists = SqlServerDict.TryGetValue(typeHandle, out var val);
            if (!exists)
            {
                lock (_sqlserverLocker)
                {
                    if (!SqlServerDict.ContainsKey(typeHandle))
                    {
                        SqlServerDict.TryAdd(typeHandle, new SqlFieldEntity(ClassToTableEntityUtils.Get<T>(DataBaseType.SqlServer2008), "[", "]", "@"));
                    }
                }
                val = SqlServerDict[typeHandle];
            }
            return val;
        }

        #endregion

        #region Sqlite

        private static readonly ConcurrentDictionary<RuntimeTypeHandle, SqlFieldEntity> SqliteDict = new ConcurrentDictionary<RuntimeTypeHandle, SqlFieldEntity>();

        private static readonly object _sqliteLocker = new object();

        public static SqlFieldEntity GetSqliteFieldEntity<T>()
        {
            var typeHandle = typeof(T).TypeHandle;
            var exists = SqliteDict.TryGetValue(typeHandle, out var val);
            if (!exists)
            {
                lock (_sqliteLocker)
                {
                    if (!SqliteDict.ContainsKey(typeHandle))
                    {
                        SqliteDict.TryAdd(typeHandle, new SqlFieldEntity(ClassToTableEntityUtils.Get<T>(DataBaseType.Sqlite), "[", "]", "@"));
                    }
                }
                val = SqliteDict[typeHandle];
            }
            return val;
        }

        #endregion

        #region Postgresql

        private static readonly ConcurrentDictionary<RuntimeTypeHandle, SqlFieldEntity> PostgreDict = new ConcurrentDictionary<RuntimeTypeHandle, SqlFieldEntity>();

        private static readonly object _postgreLocker = new object();

        public static SqlFieldEntity GetPostgreFieldEntity<T>()
        {
            var typeHandle = typeof(T).TypeHandle;
            var exists = PostgreDict.TryGetValue(typeHandle, out var val);
            if (!exists)
            {
                lock (_postgreLocker)
                {
                    if (!PostgreDict.ContainsKey(typeHandle))
                    {
                        //PostgreDict.TryAdd(typeHandle, new SqlFieldEntity(ClassToTableEntityUtils.Get<T>(), "\"", "\"", "@"));
                        PostgreDict.TryAdd(typeHandle, new SqlFieldEntity(ClassToTableEntityUtils.Get<T>(DataBaseType.Postgresql), "", "", "@"));
                    }
                }
                val = PostgreDict[typeHandle];
            }
            return val;
        }

        #endregion

        #region Oracle

        private static readonly ConcurrentDictionary<RuntimeTypeHandle, SqlFieldEntity> OracleDict = new ConcurrentDictionary<RuntimeTypeHandle, SqlFieldEntity>();

        private static readonly object _oracleLocker = new object();

        public static SqlFieldEntity GetOracleFieldEntity<T>()
        {
            var typeHandle = typeof(T).TypeHandle;
            var exists = OracleDict.TryGetValue(typeHandle, out var val);
            if (!exists)
            {
                lock (_oracleLocker)
                {
                    if (!OracleDict.ContainsKey(typeHandle))
                    {
                        //OracleDict.TryAdd(typeHandle, new SqlFieldEntity(ClassToTableEntityUtils.Get<T>(), "\"", "\"", ":"));
                        OracleDict.TryAdd(typeHandle, new SqlFieldEntity(ClassToTableEntityUtils.Get<T>(DataBaseType.Oracle), "", "", ":"));
                    }
                }
                val = OracleDict[typeHandle];
            }
            return val;
        }

        #endregion

    }
}
