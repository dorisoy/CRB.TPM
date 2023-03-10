using Dapper;
using Microsoft.Extensions.DependencyInjection;
using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Descriptors;
using CRB.TPM.Data.Abstractions.Options;
using CRB.TPM.Data.Abstractions.Schema;
using CRB.TPM.Data.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Entities;

namespace CRB.TPM.Data.Adapter.PostgreSQL
{
    internal class PostgreSQLCodeFirstProvider : CodeFirstProviderAbstract
    {
        public PostgreSQLCodeFirstProvider(CodeFirstOptions options, IDbContext context, IServiceCollection service) : base(options, context, service)
        {
        }

        public override void CreateDatabase()
        {
            var con = Context.NewConnection();

            //数据库名称
            var databaseName = con.Database;


            //判断数据库是否存在
            var schemaProvider = new PostgreSQLSchemaProvider(con);
            var isExistsDatabase = schemaProvider.IsExistsDatabase(databaseName);
            if (isExistsDatabase)
            {
                con.Close();
                return;
            }

            //创建前事件
            Options.BeforeCreateDatabase?.Invoke(Context);

            //创建数据库
            schemaProvider.CreateDatabase(databaseName);

            //创建后事件
            Options.AfterCreateDatabase?.Invoke(Context);
        }


        public override void CreateTable(IEntity entity = null)
        {
            foreach (var descriptor in Context.EntityDescriptors.Where(m => m.AutoCreate))
            {
                CreateTable(descriptor, false, entity);
            }
        }

        public override void CreateNextTable()
        {
            var shardingEntities = Context.EntityDescriptors.Where(m => m.AutoCreate && m.IsSharding).ToList();
            if (shardingEntities.Any())
            {
                foreach (var entity in shardingEntities)
                {
                    CreateTable(entity, true);
                }
            }
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="descriptor">实体描述器</param>
        /// <param name="next">下一个表</param>
        /// <param name="entity">实体对象，如果需要根据实体对象创建表，可以指定</param>
        /// <param name="tableName">指定表名创建表</param>
        private void CreateTable(IEntityDescriptor descriptor, bool next = false, IEntity entity = null, string tableName = "")
        {
            using var con = Context.NewConnection();
            con.Open();

            //解析分表名称
            tableName = string.IsNullOrWhiteSpace(tableName) ? ResolveTableName(descriptor, next, entity) : tableName;

            //判断表是否存在，只有不存时会执行创建操作并会触发对应的创建前后事件
            if (Context.SchemaProvider.IsExistsTable(con.Database, tableName))
            {
                //更新列
                if (Options.UpdateColumn)
                    UpdateColumn(descriptor, con, tableName);

                con.Close();
            }
            else
            {
                Options.BeforeCreateTable?.Invoke(Context, descriptor);

                var (sql, commentSql) = GenerateCreateTableSql(descriptor, tableName);

                con.Execute(sql);
                if (!string.IsNullOrWhiteSpace(commentSql))
                {
                    con.Execute(commentSql);
                }

                con.Close();

                Options.AfterCreateTable?.Invoke(Context, descriptor);
            }
        }

        (string sql, string commentSql) GenerateCreateTableSql(IEntityDescriptor descriptor, string tableName)
        {
            var columns = descriptor.Columns;
            var sql = new StringBuilder();
            var sqlComment = new StringBuilder(128);
            sql.AppendFormat("CREATE TABLE {0}(", AppendQuote(tableName));

            for (int i = 0; i < columns.Count; i++)
            {
                var column = columns[i];

                sql.Append(GenerateColumnAddSql(column, tableName, ref sqlComment));

                if (i < columns.Count - 1)
                {
                    sql.Append(",");
                }
            }

            sql.Append(")");


            return (sql.ToString(), sqlComment.ToString());
        }

        /// <summary>
        /// 更新列信息
        /// </summary>
        private void UpdateColumn(IEntityDescriptor descriptor, IDbConnection con, string tableName)
        {
            var columns = Context.SchemaProvider.GetColumns(con.Database, tableName);
            //保存删除后的列信息
            var cleanColumns = new List<ColumnSchema>();

            //删除列
            foreach (var column in columns)
            {
                //如果列名称、列类型或者可空配置不一样，则删除重建
                var deleted = descriptor.Columns.FirstOrDefault(m => m.Name.Equals(column.Name, StringComparison.CurrentCultureIgnoreCase));
                if (deleted == null || CompareColumnInfo(deleted, column))
                {
                    var deleteSql = $"ALTER TABLE {AppendQuote(tableName)} DROP COLUMN {AppendQuote(column.Name)};";
                    con.Execute(deleteSql);
                }
                else
                {
                    cleanColumns.Add(column);
                }
            }

            //添加列
            foreach (var column in descriptor.Columns)
            {
                var add = cleanColumns.FirstOrDefault(m => m.Name.Equals(column.Name, StringComparison.CurrentCultureIgnoreCase));
                if (add == null)
                {
                    var commentSql = new StringBuilder(128);
                    var addSql = $"ALTER TABLE {AppendQuote(tableName)} ADD COLUMN {GenerateColumnAddSql(column, tableName, ref commentSql)}";

                    con.Execute(addSql);

                    if (commentSql.Length > 0)
                    {
                        con.Execute(commentSql.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 比对列信息
        /// </summary>
        /// <returns></returns>
        private bool CompareColumnInfo(IColumnDescriptor descriptor, ColumnSchema schema)
        {
            //类型修改
            if (!descriptor.TypeName.EqualsIgnoreCase(schema.DataType))
                return true;

            switch (descriptor.TypeName?.ToUpper())
            {
                case "CHAR":
                case "VARCHAR":
                    if (descriptor.Length != schema.Length)
                        return true;
                    break;
                    //case "NUMERIC":
                    //case "DOUBLE":
                    //case "FLOAT":
                    //case "FLOAT4":
                    //case "FLOAT8":
                    //    var precision = descriptor.Precision < 1 ? 18 : descriptor.Precision;
                    //    var scale = descriptor.Scale < 1 ? 4 : descriptor.Scale;
                    //    if (precision != schema.Precision || scale != schema.Scale)
                    //        return true;
                    //break;
            }

            //可空修改
            if (descriptor.Nullable != schema.IsNullable)
                return true;

            //说明修改
            if (descriptor.Description != null && !descriptor.Description.EqualsIgnoreCase(schema.Description))
                return true;

            //默认值修改
            if (descriptor.DefaultValue != null && !descriptor.DefaultValue.EqualsIgnoreCase(schema.DefaultValue))
                return true;

            return false;
        }

        private string GenerateColumnAddSql(IColumnDescriptor column, string tableName, ref StringBuilder commentSql)
        {
            var sql = new StringBuilder();
            sql.AppendFormat("{0} ", AppendQuote(column.Name));

            switch (column.TypeName?.ToUpper())
            {
                case "CHAR":
                    sql.AppendFormat("CHAR({0}) ", column.Length);
                    break;
                case "VARCHAR":
                    sql.AppendFormat("VARCHAR({0}) ", column.Length);
                    break;
                case "DECIMAL":
                case "DOUBLE":
                case "FLOAT":
                    var precision = column.Precision < 1 ? 18 : column.Precision;
                    var scale = column.Scale < 1 ? 4 : column.Scale;
                    sql.AppendFormat("{0}({1},{2}) ", column.TypeName, precision, scale);
                    break;
                case "INT2":
                    if (column.IsPrimaryKey)
                    {
                        sql.AppendFormat("SMALLSERIAL ");
                    }
                    else
                    {
                        sql.AppendFormat("{0} ", column.TypeName);
                    }
                    break;
                case "INT4":
                case "INTEGER":
                    if (column.IsPrimaryKey)
                    {
                        sql.AppendFormat("SERIAL ");
                    }
                    else
                    {
                        sql.AppendFormat("{0} ", column.TypeName);
                    }
                    break;
                case "INT8":
                case "BIGINT":
                    if (column.IsPrimaryKey)
                    {
                        sql.AppendFormat("BIGSERIAL ");
                    }
                    else
                    {
                        sql.AppendFormat("{0} ", column.TypeName);
                    }
                    break;
                default:
                    sql.AppendFormat("{0} ", column.TypeName);
                    break;
            }
            if (column.IsPrimaryKey)
            {
                sql.Append("PRIMARY KEY ");
            }

            if (!column.Nullable)
            {
                sql.Append("NOT NULL ");
            }

            if (!column.IsPrimaryKey && column.DefaultValue.NotNull())
            {
                sql.AppendFormat("DEFAULT {0}", column.DefaultValue);
            }

            if (column.Description.NotNull())
            {
                commentSql.AppendFormat($"COMMENT ON COLUMN {this.AppendQuote(tableName)}.{this.AppendQuote(column.Name)} is '{column.Description}'; ");
            }

            return sql.ToString();
        }

        private string AppendQuote(string value)
        {
            return Context.Adapter.AppendQuote(value);
        }
    }
}
