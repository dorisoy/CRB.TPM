using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Sharding
{
    internal class OracleTableManager : ITableManager
    {
        public OracleTableManager(string name, IDatabase database) : base(name, database)
        {

        }

        public override void CreateIndex(string name, string columns, IndexType indexType)
        {
            if (indexType == IndexType.Normal)
            {
                DataBase.Execute($"CREATE INDEX {DataBase.Client.Config.UserId.ToUpper()}.\"{Name}_{name}\" ON {DataBase.Client.Config.UserId.ToUpper()}.\"{Name.ToUpper()}\" ({columns})");
            }
            else if (indexType == IndexType.Unique)
            {
                DataBase.Execute($"CREATE UNIQUE INDEX {DataBase.Client.Config.UserId.ToUpper()}.\"{Name}_{name}\" ON {DataBase.Client.Config.UserId.ToUpper()}.\"{Name.ToUpper()}\" ({columns})");
            }
        }

        public override void DropIndex(string name)
        {
            DataBase.Execute($"drop index \"{name}\"");
        }

        public override void AddColumn(string name, Type t, double length = 0, string comment = null, string columnType = null)
        {
            var dbType = CsharpTypeToDbType.Create(DataBase.DbType, t, length, columnType);
            if (t.IsValueType && t != typeof(DateTime) && t != typeof(DateTimeOffset))
            {
                dbType += " DEFAULT 0";
            }
            DataBase.Execute($"ALTER TABLE {DataBase.Client.Config.UserId.ToUpper()}.{Name.ToUpper()} ADD({name.ToUpper()} {dbType})");

        }

        public override void DropColumn(string name)
        {
            DataBase.Execute($"ALTER TABLE {Name.ToUpper()} DROP COLUMN {name.ToUpper()}");
        }

        public override void ModifyColumn(string name, Type t, double length = 0, string comment = null, string columnType = null)
        {
            throw new NotImplementedException();
        }

        public override List<IndexEntity> GetIndexEntityList()
        {
            var sql = $@"select
idx.index_name,
uniqueness,
column_name
from dba_ind_columns col, dba_indexes idx
where col.index_name = idx.index_name
and col.table_name = idx.table_name
and col.table_owner = idx.table_owner
and col.table_owner = '{DataBase.Client.Config.UserId.ToUpper()}'
and col.table_name = '{Name.ToUpper()}'";

            IEnumerable<dynamic> data = DataBase.Query(sql);
            IEnumerable<string> nameList = data.Select(s => (string)s.INDEX_NAME).Distinct();
            var list = new List<IndexEntity>();
            foreach (var n in nameList)
            {
                var model = new IndexEntity();
                model.Name = n;
                var item = data.Where(w => w.INDEX_NAME == n).ToList();
                var row = item.FirstOrDefault();
                var t = row.UNIQUENESS;
                if (n.StartsWith("SYS_") && t == "UNIQUE")
                {
                    model.Type = IndexType.PrimaryKey;
                }
                else if (t == "NONUNIQUE")
                {
                    model.Type = IndexType.Normal;
                }
                else if (t == "UNIQUE")
                {
                    model.Type = IndexType.Unique;
                }
                foreach (var it in item)
                {
                    model.Columns += it.COLUMN_NAME;
                    if (it != item.Last())
                    {
                        model.Columns += ",";
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
            var sql = $"SELECT C.COLUMN_NAME AS \"name\",C.DATA_TYPE AS \"type\",C.DATA_LENGTH AS \"len\",C.DATA_PRECISION AS \"len2\",C.DATA_SCALE AS \"scale\",NVL(CC.COMMENTS, C.COLUMN_NAME) AS \"comment\",to_number(CASE WHEN P.COLUMN_NAME = C.COLUMN_NAME THEN '1' ELSE '0' END) AS \"ispri\" ";
            sql += $@"FROM USER_TAB_COLUMNS C
            LEFT JOIN USER_COL_COMMENTS CC ON C.TABLE_NAME = CC.TABLE_NAME AND C.COLUMN_NAME = CC.COLUMN_NAME
LEFT JOIN(
SELECT CU.COLUMN_NAME FROM USER_CONS_COLUMNS CU
LEFT JOIN USER_CONSTRAINTS AU ON CU.CONSTRAINT_NAME = AU.CONSTRAINT_NAME
WHERE CU.TABLE_NAME = '{Name.ToUpper()}' AND AU.CONSTRAINT_TYPE= 'P'
)P ON C.COLUMN_NAME = P.COLUMN_NAME
WHERE C.TABLE_NAME = '{Name.ToUpper()}' ORDER BY C.COLUMN_ID";
            var data = DataBase.Query(sql);
            var list = new List<ColumnEntity>();
            foreach (var row in data)
            {
                var model = new ColumnEntity();

                model.Name = row.name;
                if (firstCharToUpper)
                {
                    model.Name = model.Name.FirstCharToUpper();
                }
                model.Comment = row.comment;

                var t = ((string)row.type).Split('(')[0];
                model.DbType = t;
                var map = DbCsharpTypeMap.OracleMap.FirstOrDefault(f => f.DbType == t);
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
                var len = (int)row.len;

                if (t == "VARCHAR2" || t == "NVARCHAR2")
                {
                    model.Length = len;
                    model.DbLength = len.ToString();
                }
                else if (t == "CLOB")
                {
                    model.Length = -1;
                    model.DbLength = "4000";

                }
                else if (t == "NUMBER")
                {
                    var len2 = (int)row.len2;
                    var scale = (int)row.scale;

                    model.Length = Convert.ToDouble($"{len2}.{scale}");
                    model.DbLength = len2.ToString();
                    if (scale != 0)
                    {
                        model.DbLength = $"{len2},{scale}";
                        if (len2 == 7 && scale == 3)
                        {
                            model.CsType = typeof(float);
                            model.CsStringType = "float";
                        }
                        if (len2 == 15 && scale == 5)
                        {
                            model.CsType = typeof(double);
                            model.CsStringType = "double";
                        }
                    }
                    else
                    {
                        if (len2 == 1)
                        {
                            model.CsType = typeof(int);
                            model.CsStringType = "int";
                        }
                        else if (len2 <= 4)
                        {
                            model.CsType = typeof(short);
                            model.CsStringType = "short";
                        }
                        else if (len2 <= 9)
                        {
                            model.CsType = typeof(int);
                            model.CsStringType = "int";
                        }
                        else if (len2 <= 19)
                        {
                            model.CsType = typeof(long);
                            model.CsStringType = "long";
                        }
                    }
                }

                if (row.ispri.ToString() == "1")
                {
                    tb.PrimaryKey = model.Name;
                }
                list.Add(model);
            }
            return list;
        }
    }
}
