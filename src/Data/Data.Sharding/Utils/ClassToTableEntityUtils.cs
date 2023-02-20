using System.Collections.Generic;
using System.Linq;

namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    /// 数据库类型转数据表工具
    /// </summary>
    internal class ClassToTableEntityUtils
    {
        /// <summary>
        /// 获取表实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static TableEntity Get<T>(DataBaseType dbType)
        {
            var entity = new TableEntity();
            entity.ColumnList = new List<ColumnEntity>();
            entity.IndexList = new List<IndexEntity>();
            entity.OtherColumnDict = new Dictionary<string, double>();
            entity.IgnoreColumnList = new List<string>();
            var t = typeof(T);
            var tableAttr = t.GetCustomAttributes(false).First(f => f is TableAttribute) as TableAttribute;
            if (tableAttr.PrimaryKey == null)
            {
                tableAttr.PrimaryKey = "";
            }
            entity.PrimaryKey = tableAttr.PrimaryKey;
            if ( dbType == DataBaseType.Oracle) //clickhouse oracle是没有自增的
            {
                entity.IsIdentity = false;
            }
            else
            {
                entity.IsIdentity = tableAttr.IsIdentity;
            }
            entity.Comment = tableAttr.Comment;
            entity.Engine = tableAttr.Engine;
            entity.Cluster = tableAttr.Cluster;
            var indexAttrs = t.GetCustomAttributes(false).Where(f => f is IndexAttribute).Select(s => s as IndexAttribute);
            if (indexAttrs.Any())
            {
                foreach (var ix in indexAttrs)
                {
                    entity.IndexList.Add(new IndexEntity() { Name = ix.Name, Columns = ix.Columns, Type = ix.Indextype });
                }
            }

            var proList = t.GetProperties();
            foreach (var pro in proList)
            {
                try
                {
                    var ignoreAttr = pro.GetCustomAttributes(false).FirstOrDefault(f => f is IgnoreAttribute);
                    if (ignoreAttr != null)
                    {
                        entity.IgnoreColumnList.Add(pro.Name);
                        continue;
                    }
                    var column = new ColumnEntity();
                    column.Name = pro.Name;
                    column.CsType = pro.PropertyType;
                    if (column.Name.ToLower() == entity.PrimaryKey.ToLower())
                    {
                        entity.PrimaryKeyType = column.CsType;
                    }
                    var colAttr = pro.GetCustomAttributes(false).FirstOrDefault(f => f is ColumnAttribute) as ColumnAttribute;
                    var ok = pro.GetCustomAttributes(false).Any(f => f is JsonStringAttribute);
                    if (colAttr != null)
                    {
                        column.Comment = colAttr.Comment;
                        column.Length = colAttr.Length;
                        if (ok && string.IsNullOrEmpty(colAttr.ColumnType) && pro.PropertyType != typeof(string))
                        {
                            colAttr.ColumnType = "jsons";
                        }

                        if ("DeletedTime" == pro.Name || "CreatedTime" == pro.Name)
                        {
                            System.Diagnostics.Debug.Print("");
                        }

                        column.DbType = CsharpTypeToDbType.Create(dbType, column.CsType, colAttr.Length, colAttr.ColumnType);

                        if (dbType == DataBaseType.Postgresql)
                        {
                            if (colAttr.ColumnType == "json")
                            {
                                entity.OtherColumnDict.Add(column.Name, -11);
                            }
                            else if (colAttr.ColumnType == "jsonb")
                            {
                                entity.OtherColumnDict.Add(column.Name, -10);
                            }
                            else if (column.Length > -20 && column.Length <= -10)
                            {
                                entity.OtherColumnDict.Add(column.Name, column.Length);
                            }
                        }
                    }
                    else
                    {
                        if (ok && pro.PropertyType != typeof(string))
                        {
                            column.DbType = CsharpTypeToDbType.Create(dbType, column.CsType, 0, "jsons");
                        }
                        else
                        {
                            column.DbType = CsharpTypeToDbType.Create(dbType, column.CsType);
                        }

                    }

                    entity.ColumnList.Add(column);
                }
                catch (System.Exception ex)
                {
                    throw new System.Exception($"CsharpTypeToDbType [{pro.Name}] 异常：{ex.Message} = {ex.StackTrace}");
                }
            }

            return entity;
        }

    }
}
