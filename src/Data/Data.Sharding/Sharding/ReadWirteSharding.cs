namespace CRB.TPM.Data.Sharding
{
    public class ReadWirteSharding<T> where T : class
    {
        public ReadWirteSharding(ISharding<T> writeSharding, params ISharding<T>[] readShardingList)
        {
            WriteSharding = writeSharding;
            _readShardingList = readShardingList;
            if (_readShardingList.Length > 1)
                _balance = new BalanceRound<ISharding<T>>(readShardingList);
        }

        private ISharding<T> WriteSharding { get; }
        private readonly ISharding<T>[] _readShardingList;
        private BalanceRound<ISharding<T>> _balance;

        public ISharding<T> GetRead()
        {
            if (_readShardingList.Length == 1)
                return _readShardingList[0];
            return _balance.Get();
        }

        public ISharding<T> GetWrite()
        {
            return WriteSharding;
        }
    }
}
