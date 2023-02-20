namespace CRB.TPM.Data.Sharding
{
    internal class OracleUnion : IUnion
    {
        public OracleUnion(IDatabase db) : base(db)
        {
        }
        public override string GetSql()
        {
            if (take == 0)
            {
                return $"SELECT {returnFields} FROM ({sqlTable}) AS UTable{string.Concat(sqlWhere, sqlGroupBy, sqlHaving, sqlOrderBy)}";
            }
            else
            {
                return $"SELECT * FROM(SELECT AA.*,rownum rn FROM(SELECT {returnFields} FROM ({sqlTable}) AS UTable{string.Concat(sqlWhere, sqlGroupBy, sqlHaving, sqlOrderBy)}) AA WHERE rownum<={skip + take}) BB WHERE rn>={skip + 1}";
            }
        }

        public override string GetSqlCount()
        {
            return $"SELECT COUNT(1) FROM ({sqlTable}) AS UTable{string.Concat(sqlWhere, sqlGroupBy, sqlHaving)}";
        }
    }
}
