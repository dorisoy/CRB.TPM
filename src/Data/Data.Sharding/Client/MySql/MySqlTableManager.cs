using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Sharding
{
    internal class MySqlTableManager : ITableManager
    {
        public MySqlTableManager(string name, IDatabase database) : base(name, database)
        {

        }

        public override void CreateIndex(string name, string columns, IndexType indexType)
        {
            string sql = null;
            switch (indexType)
            {
                case IndexType.Normal: sql = $"CREATE INDEX `{name}` ON `{Name}` ({columns});"; break;
                case IndexType.Unique: sql = $"CREATE UNIQUE INDEX `{name}` ON `{Name}` ({columns});"; break;
                case IndexType.FullText: sql = $"CREATE FULLTEXT INDEX `{name}` ON `{Name}` ({columns});"; break;
                case IndexType.Spatial: sql = $"CREATE SPATIAL INDEX `{name}` ON `{Name}` ({columns});"; break;
            }
            DataBase.Execute(sql);
        }

        public override void DropIndex(string name)
        {
            DataBase.Execute($"ALTER TABLE {Name} DROP INDEX {name}");
        }

        public override void AddColumn(string name, Type t, double length = 0, string comment = null, string columnType = null)
        {
            var dbType = CsharpTypeToDbType.Create(DataBase.DbType, t, length, columnType);
            if (t.IsValueType && t != typeof(DateTime) && t != typeof(DateTimeOffset))
            {
                dbType += " DEFAULT 0";
            }
            DataBase.Execute($"ALTER TABLE `{Name}` ADD  `{name}` {dbType} COMMENT '{comment}'");
        }

        public override void DropColumn(string name)
        {
            DataBase.Execute($"ALTER TABLE `{Name}` DROP COLUMN `{name}`");
        }

        public override void ModifyColumn(string name, Type t, double length = 0, string comment = null, string columnType = null)
        {
            var dbType = CsharpTypeToDbType.Create(DataBase.DbType, t, length, columnType);
            DataBase.Execute($"ALTER TABLE `{Name}` MODIFY COLUMN `{name}` {dbType} COMMENT '{comment}'");
        }

        public override List<IndexEntity> GetIndexEntityList()
        {
            var list = new List<IndexEntity>();
            var indexList = DataBase.Query($"SHOW INDEX FROM `{Name}`");
            var groupNames = indexList.Select(s => s.Key_name).Distinct();
            foreach (var g in groupNames)
            {
                var item = indexList.Where(f => f.Key_name == g).ToList();
                var entity = new IndexEntity();
                entity.Name = g;
                var item0 = item[0];

                if (item0.Key_name == "PRIMARY")
                {
                    entity.Type = IndexType.PrimaryKey;
                }
                else if (item0.Index_type == "BTREE" && item0.Non_unique == 0)
                {
                    entity.Type = IndexType.Unique;
                }
                else if (item0.Index_type == "FULLTEXT")
                {
                    entity.Type = IndexType.FullText;
                }
                else if (item0.Index_type == "SPATIAL")
                {
                    entity.Type = IndexType.Spatial;
                }
                else
                {
                    entity.Type = IndexType.Normal;
                }

                if (item.Count == 1)
                {
                    entity.Columns = item0.Column_name;
                }
                else
                {
                    foreach (var it in item)
                    {
                        entity.Columns += it.Column_name;
                        if (it != item.Last())
                        {
                            entity.Columns += ",";
                        }
                    }
                }

                list.Add(entity);
            }
            return list;
        }

        public override List<ColumnEntity> GetColumnEntityList(TableEntity tb = null, bool firstCharToUpper = false)
        {
            if (tb == null)
                tb = new TableEntity();
            var list = new List<ColumnEntity>();
            var columnList = DataBase.Query($"SHOW FULL COLUMNS FROM `{Name}`");
            foreach (var item in columnList)
            {
                ColumnEntity model = new ColumnEntity();
                if (firstCharToUpper)
                {
                    model.Name = ((string)item.Field).FirstCharToUpper(); //列名
                }
                else
                {
                    model.Name = (string)item.Field;
                }

                string columnType = item.Type;//数据类型

                if (string.IsNullOrEmpty(columnType))
                {
                    columnType = "(0)";
                }
                else
                {
                    columnType = columnType.Replace(" unsigned", "");
                }
                var array = columnType.Split('(');
                var t = array[0].ToLower();
                model.DbType = t;
                var map = DbCsharpTypeMap.MySqlMap.FirstOrDefault(f => f.DbType == t);

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

                if (array.Length == 2 && t != "enum")
                {
                    var length = array[1].Split(')')[0];
                    model.DbLength = length;
                    model.Length = Convert.ToDouble(length.Replace(',', '.'));
                }
                else
                {
                    var tlow = t.ToLower();
                    if (tlow == "text")
                    {
                        model.Length = -1;
                    }
                    else if (tlow == "longtext")
                    {
                        model.Length = -2;
                    }
                    else if (tlow == "mediumtext")
                    {
                        model.Length = -3;
                    }
                    else if (tlow == "tinytext")
                    {
                        model.Length = -4;
                    }
                    else if (tlow == "enum")
                    {
                        model.Length = 20;
                        model.DbLength = "20";
                    }
                    else
                    {
                        model.Length = 0;
                        model.DbLength = "0";
                    }
                }
                model.Comment = item.Comment; //说明
                list.Add(model);
            }
            return list;
        }
    }
}
