namespace CRB.TPM.Data.Sharding
{
    internal class HashSharding<T> : ISharding<T> where T : class
    {
        public HashSharding(ITable<T>[] tableList) : base(tableList)
        {

        }

        public override ITable<T> GetTableById(object id)
        {
            return TableList[ShardingUtils.Mod(id, TableList.Length)];
        }

        public override ITable<T> GetTableByModel(T model)
        {
            return TableList[ShardingUtils.Mod(model, SqlField.PrimaryKey, SqlField.PrimaryKeyType, TableList.Length)];
        }

    }
}
