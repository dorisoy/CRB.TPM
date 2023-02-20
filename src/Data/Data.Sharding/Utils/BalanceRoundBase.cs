using System.Threading;

namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    /// 负载均衡
    /// </summary>
    internal class BalanceRoundBase
    {
        private int start = -1;
        private readonly int t1;
        private readonly int t2;
        private readonly int t3;

        public BalanceRoundBase(int num)
        {
            t1 = num;
            t2 = -t1;
            t3 = t1 - 1;
        }

        public int Get()
        {
            var data = Interlocked.Increment(ref start);
            if (data >= 0)
                return data % t1;
            else
                return -(data % t2);
        }

        public int GetAsc()
        {
            var data = Interlocked.Increment(ref start);
            if (data >= 0)
                return data % t1;
            else
                return (data % t2) + t3;
        }

    }
}
