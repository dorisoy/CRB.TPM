namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DataBaseConfig
    {
        public string Database { get; set; }
        public string Server { get; set; }

        public int Port { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }

        public int MinPoolSize { get; set; }

        public int MaxPoolSize { get; set; }

        public string CharSet { get; set; }

        public int TimeOut { get; set; }

        public string Oracle_ServiceName { get; set; }

        public string Oracle_SysUserId { get; set; }

        public string Oracle_SysPassword { get; set; }

        public string Database_Path { get; set; }

        public int Database_Size_Mb { get; set; }

        public int Database_SizeGrowth_Mb { get; set; }

        public int Database_LogSize_Mb { get; set; }

        public int Database_LogSizGrowth_Mb { get; set; }

        public int SQLite_Synchronous { get; set; }

        public int SQLite_CacheSize { get; set; }

        public int SQLite_PageSize { get; set; }

        public string OtherConfig { get; set; }

    }
}
