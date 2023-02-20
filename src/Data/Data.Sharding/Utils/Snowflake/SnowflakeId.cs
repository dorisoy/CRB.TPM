namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    /// 雪花算法ID生成
    /// </summary>
    internal class SnowflakeId
    {
        internal static IdWorker worker = new IdWorker(0, 0);
        public static long GenerateNewId()
        {
            return worker.NextId();
        }
    }
}
