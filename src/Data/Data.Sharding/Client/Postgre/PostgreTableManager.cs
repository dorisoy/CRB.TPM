using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Sharding
{
    internal class PostgreTableManager : ITableManager
    {
        public PostgreTableManager(string name, IDatabase database) : base(name, database)
        {

        }

        public override void CreateIndex(string name, string columns, IndexType indexType)
        {
            string sql = null;
            switch (indexType)
            {
                case IndexType.Normal: sql = $"CREATE INDEX {name} ON {Name} ({columns});"; break;
                case IndexType.Unique: sql = $"CREATE UNIQUE INDEX {name} ON {Name} ({columns});"; break;
                case IndexType.Gist: sql = $"CREATE INDEX {name} ON {Name} USING GIST({columns});"; break;
                case IndexType.JsonbGin: sql = $"CREATE INDEX {name} ON {Name} USING GIN({columns});"; break;
                case IndexType.JsonbGinPath: sql = $"CREATE INDEX {name} ON {Name} USING GIN({columns} JSONB_PATH_OPS);"; break;
                case IndexType.JsonBtree: sql = $"CREATE INDEX {name} ON {Name} USING BTREE({columns});"; break;
            }
            DataBase.Execute(sql);
        }

        public override void DropIndex(string name)
        {
            DataBase.Execute($"DROP INDEX {name}");
        }

        public override void AddColumn(string name, Type t, double length = 0, string comment = null, string columnType = null)
        {
            var dbType = CsharpTypeToDbType.Create(DataBase.DbType, t, length, columnType);
            if (t.IsValueType && t != typeof(DateTime) && t != typeof(DateTimeOffset))
            {
                if (t != typeof(bool))
                {
                    dbType += " DEFAULT 0";
                }
                else
                {
                    dbType += " DEFAULT FALSE";
                }

            }
            if (string.IsNullOrEmpty(comment))
            {
                DataBase.Execute($"ALTER TABLE {Name} ADD COLUMN {name} {dbType}");
            }
            else
            {
                DataBase.Execute($"ALTER TABLE {Name} ADD COLUMN {name} {dbType};COMMENT ON COLUMN {Name.ToLower()}.{name.ToLower()} IS '{comment}'");
            }
        }

        public override void DropColumn(string name)
        {
            DataBase.Execute($"ALTER TABLE {Name} DROP COLUMN {name}");
        }

        public override void ModifyColumn(string name, Type t, double length = 0, string comment = null, string columnType = null)
        {
            var dbType = CsharpTypeToDbType.Create(DataBase.DbType, t, length, columnType);
            DataBase.Execute($"ALTER TABLE {Name} ALTER COLUMN {name} TYPE {dbType}");
        }

        public override List<IndexEntity> GetIndexEntityList()
        {
            var sql1 = $@"SELECT
A.INDEXNAME as name,
C.INDISUNIQUE uni,
C.INDISPRIMARY pri,
B.amname
FROM
PG_AM B
LEFT JOIN PG_CLASS F ON B.OID = F.RELAM
LEFT JOIN PG_STAT_ALL_INDEXES E ON F.OID = E.INDEXRELID
LEFT JOIN PG_INDEX C ON E.INDEXRELID = C.INDEXRELID
LEFT OUTER JOIN PG_DESCRIPTION D ON C.INDEXRELID = D.OBJOID,
PG_INDEXES A
WHERE
A.SCHEMANAME = E.SCHEMANAME AND A.TABLENAME = E.RELNAME AND A.INDEXNAME = E.INDEXRELNAME
AND E.SCHEMANAME = 'public' AND E.RELNAME = '{Name}'";

            var sql2 = $@"select
    i.relname as name,
    array_to_string(array_agg(a.attname), ', ') as column
from
    pg_class t,
    pg_class i,
    pg_index ix,
    pg_attribute a
where
    t.oid = ix.indrelid
    and i.oid = ix.indexrelid
    and a.attrelid = t.oid
    and a.attnum = ANY(ix.indkey)
    and t.relkind = 'r'
    and t.relname like '{Name}%'
group by
    t.relname,
    i.relname
order by
    t.relname,
    i.relname;";
            var data1 = DataBase.Query(sql1);
            var data2 = DataBase.Query(sql2);

            var list = new List<IndexEntity>();
            foreach (var row in data2)
            {
                var model = new IndexEntity();
                model.Name = row.name;
                model.Columns = row.column;
                var msg = data1.FirstOrDefault(f => f.name == model.Name);
                if (msg != null)
                {
                    if (msg.pri)
                    {
                        model.Type = IndexType.PrimaryKey;
                    }
                    else if (msg.uni)
                    {
                        model.Type = IndexType.Unique;
                    }
                    else
                    {
                        if (msg.amname == "gin")
                        {
                            model.Type = IndexType.JsonbGin;
                        }
                        else if (msg.amname == "gist")
                        {
                            model.Type = IndexType.Gist;
                        }
                        else
                        {
                            model.Type = IndexType.Normal;
                        }

                    }
                }
                list.Add(model);
            }
            return list;
        }

        public override List<ColumnEntity> GetColumnEntityList(TableEntity tb = null, bool firstCharToUpper = false)
        {
            if (tb == null)
                tb = new TableEntity();
            var sql1 = $@"set session myapp.name='{Name}';
SELECT 
a.attname as name,
col_description(a.attrelid,a.attnum) as comment,
concat_ws('',t.typname,SUBSTRING(format_type(a.atttypid,a.atttypmod) from '\(.*\)')) as type
FROM pg_class as c,pg_attribute as a, pg_type t
where c.relname=current_setting('myapp.name') and a.attrelid =c.oid and a.attnum>0 and a.atttypid=t.oid";

            var sql2 = $@"set session myapp.name='{Name}';
select column_name as name,
case  when position('nextval' in column_default)>0 then 1 else 0 end as IsIdentity, 
case when b.pk_name is null then 0 else 1 end as IsPK
from information_schema.columns 
left join (
    select pg_attr.attname as colname,pg_constraint.conname as pk_name from pg_constraint  
    inner join pg_class on pg_constraint.conrelid = pg_class.oid 
    inner join pg_attribute pg_attr on pg_attr.attrelid = pg_class.oid and  pg_attr.attnum = pg_constraint.conkey[1] 
    inner join pg_type on pg_type.oid = pg_attr.atttypid
    where pg_class.relname = current_setting('myapp.name') and pg_constraint.contype='p' 
) b on b.colname = information_schema.columns.column_name
left join (
    select attname,description as DeText from pg_class
    left join pg_attribute pg_attr on pg_attr.attrelid= pg_class.oid
    left join pg_description pg_desc on pg_desc.objoid = pg_attr.attrelid and pg_desc.objsubid=pg_attr.attnum
    where pg_attr.attnum>0 and pg_attr.attrelid=pg_class.oid and pg_class.relname=current_setting('myapp.name')
)c on c.attname = information_schema.columns.column_name
where table_schema='public' and table_name=current_setting('myapp.name') order by ordinal_position asc";

            IEnumerable<dynamic> data = DataBase.Query(sql1);
            IEnumerable<dynamic> data2 = DataBase.Query(sql2);
            var list = new List<ColumnEntity>();
            foreach (var row in data)
            {
                var model = new ColumnEntity();
                var row2 = data2.FirstOrDefault(s => s.name == row.name);
                if (firstCharToUpper)
                {
                    model.Name = ((string)row.name).FirstCharToUpper();
                }
                else
                {
                    model.Name = (string)row.name;
                }

                if (row2.ispk.ToString() == "1")
                {
                    tb.PrimaryKey = model.Name;
                    if (row2.isidentity.ToString() == "1")
                    {
                        tb.IsIdentity = true;
                    }
                }

                string columnType = row.type;//数据类型
                var array = columnType.Split('(');
                var t = array[0].ToLower();
                model.DbType = t;
                var map = DbCsharpTypeMap.PostgreSqlMap.FirstOrDefault(f => f.DbType == t);

                if (map != null)
                {
                    model.CsStringType = map.CsStringType;
                    model.CsType = map.CsType;
                }
                else
                {
                    model.CsStringType = "object";
                    model.CsType = typeof(object);
                }

                if (array.Length == 2)
                {
                    var length = array[1].Split(')')[0];
                    model.DbLength = length;
                    model.Length = Convert.ToDouble(length.Replace(',', '.'));
                }
                else
                {
                    if (t.ToLower() == "text")
                    {
                        model.Length = -1;
                    }
                    else if (t.ToLower() == "longtext")
                    {
                        model.Length = -2;
                    }
                    else if (t.ToLower() == "timestamp")
                    {
                        model.Length = 6;
                        model.DbLength = "6";
                    }
                    else if (t.ToLower() == "timetz")
                    {
                        model.Length = 6;
                        model.DbLength = "6";
                    }
                    else
                    {
                        model.Length = 0;
                        model.DbLength = "0";
                    }
                }
                model.Comment = row.comment; //说明

                list.Add(model);
            }
            return list;
        }
    }
}
