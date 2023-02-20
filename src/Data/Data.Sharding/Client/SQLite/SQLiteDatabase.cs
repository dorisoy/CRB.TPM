using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Sharding
{
    internal class SQLiteDatabase : IDatabase
    {
        public SQLiteDatabase(string name, SQLiteClient client) : base(name, client)
        {
            ConnectionString = ConnectionStringBuilder.BuilderSQLite(client.Config, name);
        }

        public override string ConnectionString { get; }


        public override void DropTable(string name)
        {
            Execute($"DROP TABLE IF EXISTS {name}");
            TableCache.TryRemove(name, out _);
        }

        public override bool ExistsTable(string name)
        {
            return ExecuteScalar<int>($"SELECT COUNT(1) FROM sqlite_master WHERE name='{name}'") > 0;
        }

        public override IDbConnection GetConn()
        {
            var conn = new SQLiteConnection(ConnectionString);
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
            var conn = new SQLiteConnection(ConnectionString);
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
            IEnumerable<dynamic> data = Query($"pragma table_info('{name}')");//字段信息;
            return data.Select(s => s.name as string);
        }

        public override TableEntity GetTableEntityFromDatabase(string name, bool firstCharToUpper = false)
        {
            dynamic dynamicTable = QueryFirstOrDefault($"SELECT * FROM sqlite_master where type='table' and tbl_name='{name}'");
            IEnumerable<dynamic> dynamicColums = Query($"pragma table_info('{name}')");
            var entity = new TableEntity();
            entity.PrimaryKey = "";
            var tablesql = ((string)dynamicTable.sql).ToUpper();
            if (tablesql.Contains("AUTOINCREMENT"))
            {
                entity.IsIdentity = true;
            }
            var row = dynamicColums.FirstOrDefault(f => f.pk == 1);
            if (row != null)
            {
                entity.PrimaryKey = ((string)row.name).FirstCharToUpper();
            }
            var manager = GetTableManager(name);
            var indexList = manager.GetIndexEntityList();
            entity.IndexList = indexList;
            entity.ColumnList = manager.GetColumnEntityList(null, firstCharToUpper);
            var col = entity.ColumnList.FirstOrDefault(w => w.Name.ToLower() == entity.PrimaryKey.ToLower());
            if (col != null)
            {
                entity.PrimaryKeyType = col.CsType;
            }
            return entity;
        }

        public override IEnumerable<string> GetTableList()
        {
            return Query<string>("select name from sqlite_master where type='table' and name!='sqlite_sequence'");
        }

        public override ITableManager GetTableManager(string name)
        {
            return new SQLiteTableManager(name, this);
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
            var sb = new StringBuilder();
            sb.Append($"CREATE TABLE IF NOT EXISTS [{name}] (");
            foreach (var item in tableEntity.ColumnList)
            {
                if (tableEntity.PrimaryKey.ToLower() == item.Name.ToLower())
                {
                    if (tableEntity.IsIdentity)
                    {
                        sb.Append($"[{item.Name}] INTEGER PRIMARY KEY AUTOINCREMENT");
                    }
                    else
                    {
                        sb.Append($"[{item.Name}] {item.DbType} PRIMARY KEY");
                    }
                }
                else
                {
                    sb.Append($"[{item.Name}] {item.DbType}");
#if CORE6
                    if (item.CsType.IsValueType && item.CsType != typeof(Nullable<DateTime>) && item.CsType != typeof(DateTime) && item.CsType != typeof(DateTimeOffset) && item.CsType != typeof(DateOnly) && item.CsType != typeof(TimeOnly))
#else
                    if (item.CsType.IsValueType && item.CsType != typeof(Nullable<DateTime>)  && item.CsType != typeof(DateTime) && item.CsType != typeof(DateTimeOffset))
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

            if (tableEntity.IndexList != null && tableEntity.IndexList.Count > 0)
            {
                sb.Append(";");
                foreach (var ix in tableEntity.IndexList)
                {
                    if (ix.Type == IndexType.Normal)
                    {
                        sb.Append("CREATE INDEX");
                    }
                    if (ix.Type == IndexType.Unique)
                    {
                        sb.Append("CREATE UNIQUE INDEX");
                    }
                    sb.Append($" {name}_{ix.Name} ON [{name}] ({ix.Columns})");
                    if (ix != tableEntity.IndexList.Last())
                    {
                        sb.Append(";");
                    }
                }
            }
            return sb.ToString();
        }

        public override void OptimizeTable(string name, bool final = false, bool deduplicate = false)
        {
            throw new NotImplementedException();
        }

        public override void OptimizeTable(string name, string partition, bool final = false, bool deduplicate = false)
        {
            throw new NotImplementedException();
        }

        public override void TruncateTable(string name)
        {
            Execute($"DELETE FROM {name}");
        }

        protected override ITable<T> CreateITable<T>(string name)
        {
            return new SQLiteTable<T>(name, this);
        }

        public override void Vacuum()
        {
            Execute("vacuum");
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
