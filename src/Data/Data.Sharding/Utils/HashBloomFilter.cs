namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    /// 哈希布尔过滤器
    /// </summary>
    public class HashBloomFilter
    {
        public static int BKDRHash(string str)
        {
            int seed = 131; // 31 131 1313 13131 131313 etc..
            int hash = 0;
            int count;
            char[] bitarray = str.ToCharArray();
            count = bitarray.Length;
            while (count > 0)
            {
                hash = hash * seed + (bitarray[bitarray.Length - count]);
                count--;
            }
            return (hash & 0x7FFFFFFF);
        }

        public static int Mod(string str, int count)
        {
            return BKDRHash(str) % count;
        }
    }
}
