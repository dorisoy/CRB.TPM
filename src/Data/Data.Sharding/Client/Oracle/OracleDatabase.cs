using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace CRB.TPM.Data.Sharding
{
    internal class OracleDatabase : IDatabase
    {
        public OracleDatabase(string name, OracleClient client) : base(name, client)
        {
            ConnectionString = ConnectionStringBuilder.BuilderOracle(client.Config, name);
        }

        public override string ConnectionString { get; }

        public override ITableManager GetTableManager(string name)
        {
            return new OracleTableManager(name, this);
        }

        protected override ITable<T> CreateITable<T>(string name)
        {
            return new OracleTable<T>(name, this);
        }

        public override void DropTable(string name)
        {
            if (ExistsTable(name))
            {
                Execute("DROP TABLE " + name);
            }
            TableCache.TryRemove(name, out _);
        }

        public override bool ExistsTable(string name)
        {
            return ExecuteScalar<int>($"SELECT COUNT(*) FROM ALL_TABLES WHERE OWNER='{Client.Config.UserId.ToUpper()}' AND TABLE_NAME='{name.ToUpper()}'") > 0;
        }

        public override IDbConnection GetConn()
        {
            var conn = new OracleConnection(ConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    conn.Dispose();
                    throw ex;
                }
            }
            return conn;
        }

        public override async Task<IDbConnection> GetConnAsync()
        {
            var conn = new OracleConnection(ConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    await conn.OpenAsync();
                }
                catch (Exception ex)
                {
#if CORE
                    await conn.DisposeAsync();
#else
                    conn.Dispose();
#endif
                    throw ex;
                }
            }
            return conn;
        }

        public override IEnumerable<string> GetTableColumnList(string name)
        {
            var sql = $@"SELECT
C.COLUMN_NAME
FROM USER_TAB_COLUMNS C
LEFT JOIN USER_COL_COMMENTS CC ON C.TABLE_NAME = CC.TABLE_NAME AND C.COLUMN_NAME = CC.COLUMN_NAME
LEFT JOIN(
SELECT CU.COLUMN_NAME FROM USER_CONS_COLUMNS CU
LEFT JOIN USER_CONSTRAINTS AU ON CU.CONSTRAINT_NAME = AU.CONSTRAINT_NAME
WHERE CU.TABLE_NAME = '{name.ToUpper()}' AND AU.CONSTRAINT_TYPE= 'P'
)P ON C.COLUMN_NAME = P.COLUMN_NAME
WHERE C.TABLE_NAME = '{name.ToUpper()}' ORDER BY C.COLUMN_ID";
            return Query<string>(sql);
        }

        public override TableEntity GetTableEntityFromDatabase(string name, bool firstCharToUpper = false)
        {
            var entity = new TableEntity();
            entity.PrimaryKey = "";
            var manager = GetTableManager(name);
            entity.IndexList = manager.GetIndexEntityList();
            entity.ColumnList = manager.GetColumnEntityList(entity, firstCharToUpper);
            var col = entity.ColumnList.FirstOrDefault(w => w.Name.ToLower() == entity.PrimaryKey.ToLower());
            if (col != null)
            {
                entity.PrimaryKeyType = col.CsType;
            }

            var sql = $"SELECT T.TABLE_NAME AS \"name\",'T' AS \"t\",NVL(C.COMMENTS, T.TABLE_NAME) AS \"comment\" FROM USER_TABLES T ";
            sql += $"LEFT JOIN USER_TAB_COMMENTS C ON T.TABLE_NAME = C.TABLE_NAME WHERE C.TABLE_NAME = '{name.ToUpper()}' ";
            sql += "UNION ALL SELECT T.VIEW_NAME AS \"Name\",'V' AS \"TypeName\",NVL(C.COMMENTS, T.VIEW_NAME) AS \"Description\" FROM USER_VIEWS T ";
            sql += "LEFT JOIN USER_TAB_COMMENTS C ON T.VIEW_NAME = C.TABLE_NAME";
            var row = QueryFirstOrDefault(sql);
            if (row != null)
            {
                entity.Comment = row.comment;
            }
            return entity;
        }

        public override IEnumerable<string> GetTableList()
        {
            return Query<string>($"SELECT OBJECT_NAME FROM DBA_OBJECTS WHERE OWNER='{Client.Config.UserId.ToUpper()}' AND OBJECT_TYPE='TABLE'");
        }

        /// <summary>
        /// 获取创建表脚本
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="name">表名</param>
        /// <returns></returns>
        public override string GetTableScript<T>(string name)
        {
            var tableEntity = ClassToTableEntityUtils.Get<T>(Client.DbType);
            if (tableEntity.IsIdentity)
                throw new Exception($"oracle is not supported identity key,table name is {name}");
            var sb = new StringBuilder();
            sb.Append($"CREATE TABLE {name.ToUpper()} (");
            foreach (var item in tableEntity.ColumnList)
            {
                sb.Append($"{item.Name.ToUpper()} {item.DbType}");
                if (tableEntity.PrimaryKey.ToLower() == item.Name.ToLower())
                {
                    sb.Append(" PRIMARY KEY");
                }
                else
                {
#if CORE6
                    if (item.CsType.IsValueType && item.CsType != typeof(Nullable<DateTime>) && item.CsType != typeof(DateTime) && item.CsType != typeof(DateTimeOffset) && item.CsType != typeof(DateOnly) && item.CsType != typeof(TimeOnly))
#else
                    if (item.CsType.IsValueType && item.CsType != typeof(Nullable<DateTime>) && item.CsType != typeof(DateTime) && item.CsType != typeof(DateTimeOffset))
#endif
                    {
                        sb.Append(" DEFAULT 0");
                    }
                }
                if (item != tableEntity.ColumnList.Last())
                {
                    sb.Append(",");
                }
            }
            sb.Append(")");
            return sb.ToString();
        }

        public override void TruncateTable(string name)
        {
            Execute("TRUNCATE TABLE " + name);
        }

        public override void CreateTable<T>(string name)
        {
            var script = GetTableScript<T>(name);
            var tableEntity = ClassToTableEntityUtils.Get<T>(Client.DbType);
            using (var conn = GetConn())
            {
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        conn.Execute(script, transaction: tran);

                        foreach (var item in tableEntity.IndexList)
                        {
                            if (item.Type == IndexType.Normal)
                            {
                                conn.Execute($"CREATE INDEX {Client.Config.UserId.ToUpper()}.\"{name}_{item.Name}\" ON {Client.Config.UserId.ToUpper()}.\"{name.ToUpper()}\" ({item.Columns})", transaction: tran);
                            }
                            else if (item.Type == IndexType.Unique)
                            {
                                conn.Execute($"CREATE UNIQUE INDEX {Client.Config.UserId.ToUpper()}.\"{name}_{item.Name}\" ON {Client.Config.UserId.ToUpper()}.\"{name.ToUpper()}\" ({item.Columns})", transaction: tran);
                            }
                        }
                        if (!string.IsNullOrEmpty(tableEntity.Comment))
                        {
                            conn.Execute($"COMMENT ON TABLE {name.ToUpper()} IS '{tableEntity.Comment}'", transaction: tran);
                        }
                        foreach (var item in tableEntity.ColumnList)
                        {
                            if (!string.IsNullOrEmpty(item.Comment))
                            {
                                conn.Execute($"COMMENT ON COLUMN {name.ToUpper()}.{item.Name.ToUpper()} IS '{item.Comment}'", transaction: tran);
                            }
                        }
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }

                }

            }
        }

        public override void OptimizeTable(string name, bool final = false, bool deduplicate = false)
        {
            throw new NotImplementedException();
        }

        public override void OptimizeTable(string name, string partition, bool final = false, bool deduplicate = false)
        {
            throw new NotImplementedException();
        }

        public override void Vacuum()
        {
            throw new NotImplementedException();
        }

        public override void CreateSequence()
        {
            throw new NotImplementedException();
        }

        public override void CreateCurrvalFun()
        {
            throw new NotImplementedException();
        }

        public override void CreateNextvalFun()
        {
            throw new NotImplementedException();
        }

        public override void CreateSetvalFun()
        {
            throw new NotImplementedException();
        }
    }
}
