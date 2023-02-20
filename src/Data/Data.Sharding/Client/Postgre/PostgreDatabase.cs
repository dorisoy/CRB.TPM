using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Sharding
{
    internal class PostgreDatabase : IDatabase
    {
        public PostgreDatabase(string name, PostgreClient client) : base(name, client)
        {
            ConnectionString = ConnectionStringBuilder.BuilderPostgresql(client.Config, name);
        }

        public override string ConnectionString { get; }

        protected override ITable<T> CreateITable<T>(string name)
        {
            return new PostgreTable<T>(name, this);
        }

        public override ITableManager GetTableManager(string name)
        {
            return new PostgreTableManager(name, this);
        }

        public override void DropTable(string name)
        {
            Execute($"DROP TABLE IF EXISTS {name}");
            TableCache.TryRemove(name, out _);
        }

        public override bool ExistsTable(string name)
        {
            return ExecuteScalar<int>($"select count(1) from pg_class where relname='{name}'") > 0;
        }

        public override IDbConnection GetConn()
        {
            var conn = new NpgsqlConnection(ConnectionString);
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
            var conn = new NpgsqlConnection(ConnectionString);
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
            return Query<string>($"select column_name from information_schema.columns where table_schema='public' and table_name = '{name}'");
        }

        public override TableEntity GetTableEntityFromDatabase(string name, bool firstCharToUpper = false)
        {
            var entity = new TableEntity();
            entity.PrimaryKey = "";
            string sql = $@"select a.relname as name , b.description as value from pg_class a 
left join (select * from pg_description where objsubid=0) b on a.oid = b.objoid
where a.relname='{name}' and a.relname in (select tablename from pg_tables where schemaname = 'public')
order by a.relname asc";

            var row = QueryFirstOrDefault(sql);
            if (row != null)
            {
                entity.Comment = row.value;
            }

            var manager = GetTableManager(name);
            var indexList = manager.GetIndexEntityList();
            entity.IndexList = indexList;
            var ix = indexList.FirstOrDefault(f => f.Type == IndexType.PrimaryKey);
            if (ix != null)
            {
                entity.PrimaryKey = ix.Columns.FirstCharToUpper();
            }
            entity.ColumnList = manager.GetColumnEntityList(entity, firstCharToUpper);

            if (entity.PrimaryKey != null)
            {
                var col = entity.ColumnList.FirstOrDefault(w => w.Name.ToLower() == entity.PrimaryKey.ToLower());
                if (col != null)
                {
                    entity.PrimaryKeyType = col.CsType;
                }
            }
            return entity;

        }

        public override IEnumerable<string> GetTableList()
        {
            return Query<string>("select tablename from pg_tables where schemaname='public'");
        }

        /// <summary>
        /// 获取创建表脚本
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="name">表名</param>
        /// <returns></returns>
        public override string GetTableScript<T>(string name)
        {
            string lowName = name.ToLower();
            var tableEntity = ClassToTableEntityUtils.Get<T>(Client.DbType);
            var sb = new StringBuilder();
            sb.Append($"CREATE TABLE IF NOT EXISTS {lowName} (");
            foreach (var item in tableEntity.ColumnList)
            {
                string dbtype = item.DbType;

                if (tableEntity.PrimaryKey.ToLower() == item.Name.ToLower())
                {
                    if (tableEntity.IsIdentity)
                    {
                        if (tableEntity.PrimaryKeyType == typeof(int))
                        {
                            dbtype = "serial4";
                        }
                        else
                        {
                            dbtype = "serial8";
                        }
                    }
                    sb.Append($"{item.Name.ToLower()} {dbtype}");
                    sb.Append(" PRIMARY KEY");
                }
                else
                {
                    sb.Append($"{item.Name.ToLower()} {dbtype}");
#if CORE6
                    if (item.CsType.IsValueType && item.CsType != typeof(Nullable<DateTime>) && item.CsType != typeof(DateTime) && item.CsType != typeof(DateTimeOffset) && item.CsType != typeof(DateOnly) && item.CsType != typeof(TimeOnly))
#else
                    if (item.CsType.IsValueType && item.CsType != typeof(Nullable<DateTime>) && item.CsType != typeof(DateTime) && item.CsType != typeof(DateTimeOffset))
#endif
                    {
                        if (item.CsType != typeof(bool))
                        {
                            sb.Append(" DEFAULT 0");
                        }
                        else
                        {
                            sb.Append(" DEFAULT FALSE");
                        }
                    }
                }

                if (item != tableEntity.ColumnList.Last())
                {
                    sb.Append(",");
                }
            }
            sb.Append(")");
            foreach (var ix in tableEntity.IndexList)
            {
                if (ix.Type == IndexType.Gist)
                {
                    sb.Append($";CREATE INDEX {lowName}_{ix.Name} ON {lowName} USING GIST({ix.Columns})");
                }
                else if (ix.Type == IndexType.JsonbGin)
                {
                    sb.Append($";CREATE INDEX {lowName}_{ix.Name} ON {lowName} USING GIN({ix.Columns})");
                }
                else if (ix.Type == IndexType.JsonbGinPath)
                {
                    sb.Append($";CREATE INDEX {lowName}_{ix.Name} ON {lowName} USING GIN({ix.Columns} JSONB_PATH_OPS)");
                }
                else if (ix.Type == IndexType.JsonBtree)
                {
                    sb.Append($";CREATE INDEX {lowName}_{ix.Name} ON {lowName} USING BTREE({ix.Columns})");
                }
                else
                {
                    if (ix.Type == IndexType.Normal)
                    {
                        sb.Append(";CREATE INDEX");
                    }
                    if (ix.Type == IndexType.Unique)
                    {
                        sb.Append(";CREATE UNIQUE INDEX");
                    }
                    sb.Append($" {lowName}_{ix.Name} ON {lowName} ({ix.Columns})");
                }
            }
            foreach (var item in tableEntity.ColumnList)
            {
                if (!string.IsNullOrEmpty(item.Comment))
                {
                    sb.Append($";COMMENT ON COLUMN {lowName}.{item.Name.ToLower()} IS '{item.Comment}'");
                }
            }
            if (!string.IsNullOrEmpty(tableEntity.Comment))
            {
                sb.Append($";COMMENT ON TABLE {lowName} IS '{tableEntity.Comment}'");
            }
            return sb.ToString();
        }


        public override void TruncateTable(string name)
        {
            Execute($"TRUNCATE TABLE {name}");
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
