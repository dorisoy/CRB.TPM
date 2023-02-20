using System;
using System.Collections.Generic;
using Z.BulkOperations;
using Z.Dapper.Plus;
using Dapper;

namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    /// 用于分库分表Sharding构建工厂
    /// </summary>
    public class ShardingFactory
    {

#if CORE6
        public static DbTypeDateOnly DateOnlyFormat { get; set; } = DbTypeDateOnly.Date;
        public static DbTypeTimeOnly TimeOnlyFormat { get; set; } = DbTypeTimeOnly.TimeSpan;//only mysql and pgsql,other use time
        static ShardingFactory()
        {
            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
            SqlMapper.AddTypeHandler(new TimeOnlyTypeHandler());
            DapperPlusManager.AddValueConverter(typeof(DateOnly), new DateOnlyTypeHandlerZ());
            DapperPlusManager.AddValueConverter(typeof(TimeOnly), new TimeOnlyTypeHandlerZ());
        }
#endif

        /// <summary>
        /// 设置雪花算法工作机器
        /// </summary>
        /// <param name="workerId">工作机器ID</param>
        /// <param name="datacenterId">数据中心ID</param>
        /// <param name="seqLength">序列长度</param>
        public static void SetSnowFlakeWorker(long workerId, long datacenterId, long seqLength = 0)
        {
            //实例工作机器
            SnowflakeId.worker = new IdWorker(workerId, datacenterId, seqLength);
        }

        /// <summary>
        /// 雪花算法数字ID生成
        /// </summary>
        /// <param name="workerId">工作机器ID</param>
        /// <param name="workLength">机器ID长度</param>
        /// <param name="seqLength">序列长度</param>
        public static void SetLongIdWorker(ushort workerId = 0, byte workLength = 6, byte seqLength = 6)
        {
            var opt = new IdGeneratorOptions();
            if (workLength != 6)
            {
                //默认值6，取值范围 [1, 15]（要求：序列数位长+机器码位长不超过22）
                opt.WorkerIdBitLength = workLength; 
            }
            //当workLength等于6,workerId最大值63
            if (workerId != 0) 
            {
                //最大值 2 ^ WorkerIdBitLength - 1
                opt.WorkerId = workerId;
            }
            //默认6支持10万并发,10才能支持50-200万并发,取值范围[3,21]
            if (seqLength != 6) 
            {
                opt.SeqBitLength = seqLength;
            }
            IdHelper.IdGenInstance = new DefaultIdGenerator(opt);
        }

        /// <summary>
        /// 创建数据操作客户端
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="config">数据库配置</param>
        /// <param name="connectionString">数据库配置</param>
        /// <returns></returns>
        public static IClient CreateClient(DataBaseType dbType, DataBaseConfig config, string connectionString = "")
        {
            switch (dbType)
            {
                case DataBaseType.MySql:
                    return new MySqlClient(config, connectionString);
                case DataBaseType.SqlServer2005:
                    return new SqlServerClient(config, DataBaseType.SqlServer2005, connectionString);
                case DataBaseType.SqlServer2008:
                    return new SqlServerClient(config, DataBaseType.SqlServer2008, connectionString);
                case DataBaseType.SqlServer2012:
                    return new SqlServerClient(config, DataBaseType.SqlServer2012, connectionString);
                case DataBaseType.Sqlite:
                    return new SQLiteClient(config, connectionString);
                case DataBaseType.Postgresql:
                    return new PostgreClient(config, connectionString);
                case DataBaseType.Oracle:
                    return new OracleClient(config, connectionString);
            }
            return null;
        }

        /// <summary>
        /// 创建分布式事务
        /// </summary>
        /// <returns></returns>
        public static DistributedTransaction CreateDistributedTransaction()
        {
            return new DistributedTransaction();
        }

        /// <summary>
        /// 创建数据读写操作客户端
        /// </summary>
        /// <param name="writeClient">写客户端</param>
        /// <param name="readClientList">读客户端</param>
        /// <returns></returns>
        public static ReadWirteClient CreateReadWriteClient(IClient writeClient, params IClient[] readClientList)
        {
            return new ReadWirteClient(writeClient, readClientList);
        }

        /// <summary>
        /// 创建表查询
        /// </summary>
        /// <typeparam name="T">要查询表实体类型</typeparam>
        /// <param name="tableList">表实体列表</param>
        /// <returns></returns>
        public static ShardingQuery<T> CreateShardingQuery<T>(params ITable<T>[] tableList) where T : class
        {
            return new ShardingQuery<T>(tableList);
        }

        /// <summary>
        /// 创建数据库查询
        /// </summary>
        /// <param name="dbList">数据DB列表</param>
        /// <returns></returns>
        public static ShardingQueryDb CreateShardingQueryDb(params IDatabase[] dbList)
        {
            return new ShardingQueryDb(dbList);
        }

        /// <summary>
        /// 创建查询客户端
        /// </summary>
        /// <param name="clientList"></param>
        /// <returns></returns>
        public static ShardingQueryClient CreateShardingQueryClient(params IClient[] clientList)
        {
            return new ShardingQueryClient(clientList);
        }

        /// <summary>
        /// 创建Hash分片
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableList"></param>
        /// <returns></returns>
        public static ISharding<T> CreateShardingHash<T>(params ITable<T>[] tableList) where T : class
        {
            return new HashSharding<T>(tableList);
        }

        /// <summary>
        /// 创建区间分片
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static ISharding<T> CreateShardingRange<T>(Dictionary<long, ITable<T>> dict) where T : class
        {
            return new RangeSharding<T>(dict);
        }

        /// <summary>
        /// 自动创建分片
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableList"></param>
        /// <returns></returns>
        public static ISharding<T> CreateShardingAuto<T>(params ITable<T>[] tableList) where T : class
        {
            return new AutoSharding<T>(tableList);
        }

        /// <summary>
        /// 创建读写分片
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="write"></param>
        /// <param name="readList"></param>
        /// <returns></returns>
        public static ReadWirteSharding<T> CreateReadWirteSharding<T>(ISharding<T> write, params ISharding<T>[] readList) where T : class
        {
            return new ReadWirteSharding<T>(write, readList);
        }

        /// <summary>
        /// 生成下一个对象ID
        /// </summary>
        /// <returns></returns>
        public static string NextObjectId()
        {
            return ObjectId.GenerateNewIdAsString();
        }

        /// <summary>
        /// 生成下一个雪花ID
        /// </summary>
        /// <returns></returns>
        public static long NextSnowId()
        {
            return SnowflakeId.GenerateNewId();
        }

        /// <summary>
        /// 生成下一个雪花ID字符串
        /// </summary>
        /// <returns></returns>
        public static string NextSnowIdAsString()
        {
            return NextSnowId().ToString();
        }

        /// <summary>
        /// 生成下一个新的long型Id
        /// </summary>
        /// <returns></returns>
        public static long NextLongId()
        {
            return IdHelper.IdGenInstance.NewLong();
        }

        /// <summary>
        /// 生成下一个新的long型Id字符串
        /// </summary>
        /// <returns></returns>
        public static string NextLongIdAsString()
        {
            return NextLongId().ToString();
        }

        /// <summary>
        /// 尝试添加类型处理
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="handler">实现了SqlMapper.ITypeHandler</param>
        /// <param name="handlerz">实现了IBulkValueConverter</param>
        public static void AddTypeHandler(Type type, SqlMapper.ITypeHandler handler, IBulkValueConverter handlerz)
        {
            TypeHandlerCache.Add(type, () =>
            {
                SqlMapper.AddTypeHandler(type, handler);
                DapperPlusManager.AddValueConverter(type, handlerz);
            });
        }
    }
}
