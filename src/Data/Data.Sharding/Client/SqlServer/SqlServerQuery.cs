namespace CRB.TPM.Data.Sharding
{
    internal class SqlServerQuery : IQuery
    {
        public SqlServerQuery(IDatabase db) : base(db)
        {

        }

        internal override IQuery Add<T>(ITable<T> table, string asName = null)
        {
            var primaryKey = table.SqlField.PrimaryKey;
            if (string.IsNullOrEmpty(asName))
            {
                sqlTable = $"[{table.Name}]";
                returnFields = table.SqlField.AllFields;
                sqlOrderBy = $" ORDER BY {primaryKey}";
            }
            else
            {
                sqlTable = $"[{table.Name}] AS {asName}";
                returnFields = $"{asName}.*";
                sqlOrderBy = $" ORDER BY {asName}.{primaryKey}";
            }
            _sqlTable = sqlTable;
            _returnFields = returnFields;
            _sqlOrderBy = sqlOrderBy;
            return this;
        }

        public override IQuery InnerJoin<T>(ITable<T> table, string asName, string on)
        {
            sqlTable += $" INNER JOIN [{table.Name}] AS {asName} ON {on}";
            return this;
        }

        public override IQuery LeftJoin<T>(ITable<T> table, string asName, string on)
        {
            sqlTable += $" LEFT JOIN [{table.Name}] AS {asName} ON {on}";
            return this;
        }

        public override IQuery RightJoin<T>(ITable<T> table, string asName, string on)
        {
            sqlTable += $" RIGHT JOIN [{table.Name}] AS {asName} ON {on}";
            return this;
        }

        public override string GetSql()
        {
            if (take == 0)
            {
                return $"SELECT {returnFields} FROM {string.Concat(sqlTable, sqlWhere, sqlGroupBy, sqlHaving, sqlOrderBy)}";
            }
            else
            {
                if (skip == 0) //第一页,使用Top语句
                {
                    return $"SELECT TOP ({take}) {returnFields} FROM {string.Concat(sqlTable, sqlWhere, sqlGroupBy, sqlHaving, sqlOrderBy)}";
                }
                else
                {
                    if (db.DbType == DataBaseType.SqlServer2012)
                    {
                        return $"SELECT {returnFields} FROM {string.Concat(sqlTable, sqlWhere, sqlGroupBy, sqlHaving, sqlOrderBy)} offset {skip} rows fetch next {take} rows only";
                    }
                    else
                    {
                        //使用ROW_NUMBER()
                        return $"WITH cte AS(SELECT ROW_NUMBER() OVER({sqlOrderBy?.Trim()}) AS Row_Number,{returnFields} FROM {string.Concat(sqlTable, sqlWhere, sqlGroupBy, sqlHaving)}) SELECT * FROM cte WHERE cte.Row_Number BETWEEN {skip + 1} AND {skip + take}";
                    }
                }

            }
        }

        public override string GetSqlCount()
        {
            return $"SELECT COUNT(1) FROM {string.Concat(sqlTable, sqlWhere, sqlGroupBy, sqlHaving)}";
        }
    }
}
