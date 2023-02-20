using CRB.TPM.Data.Sharding;
using System.Collections.Generic;

namespace Data.Sharding.Test;

/// <summary>
/// 测试基类，client须单例模式
/// </summary>
public class BaseTest
{
    protected readonly DataBaseConfig config = new()
    {
        Server = "127.0.0.1",
        UserId = "root",
        Password = "racing.1",
        Port = 3306
    };

    //MYSQL
    public static readonly IClient Client = ShardingFactory.CreateClient(DataBaseType.MySql, new DataBaseConfig
    {
        Server = "127.0.0.1",
        UserId = "root",
        Password = "racing.1",
        Port = 3306
    });

    public static readonly IClient Client2 = ShardingFactory.CreateClient(DataBaseType.MySql, new DataBaseConfig
    {
        Server = "127.0.0.1",
        UserId = "root",
        Password = "racing.1",
        Port = 3307
    });
    public static readonly IClient Client3 = ShardingFactory.CreateClient(DataBaseType.MySql, new DataBaseConfig
    {
        Server = "127.0.0.1",
        UserId = "root",
        Password = "racing.1",
        Port = 3306
    });

    //SQLITE
    public static readonly IClient Client4 = ShardingFactory.CreateClient(DataBaseType.Sqlite, new DataBaseConfig
    {
        Server = "D:\\"
    });
    public static readonly IClient Client5 = ShardingFactory.CreateClient(DataBaseType.Sqlite, new DataBaseConfig
    {
        Server = "D:\\"
    });

    //SQLSERVER
    public static readonly IClient Client6 = ShardingFactory.CreateClient(DataBaseType.SqlServer2008, new DataBaseConfig
    {
        Server = ".\\sqlexpress2",
        UserId = "sa",
        Password = "racing.1",
        Database_Path = "D:\\"
    });
    public static readonly IClient Client7 = ShardingFactory.CreateClient(DataBaseType.SqlServer2008, new DataBaseConfig
    {
        Server = ".\\sqlexpress2",
        UserId = "sa",
        Password = "racing.1",
        Database_Path = "D:\\"
    });

    //POSTGRESQL
    public static readonly IClient Client8 = ShardingFactory.CreateClient(DataBaseType.Postgresql, new DataBaseConfig
    {
        Server = "127.0.0.1",
        UserId = "postgres",
        Password = "racing.1",
        MinPoolSize = 1,
        MaxPoolSize = 2
    });
    public static readonly IClient Client9 = ShardingFactory.CreateClient(DataBaseType.Postgresql, new DataBaseConfig
    {
        Server = "127.0.0.1",
        UserId = "postgres",
        Password = "racing.1"
    });

    //ORACLE
    public static readonly DataBaseConfig oracleConfig = new()
    {
        Server = "127.0.0.1",
        UserId = "test",
        Password = "123",
        Oracle_ServiceName = "dorisoy",
        Oracle_SysUserId = "sys",
        Oracle_SysPassword = "racing.1",
        Database_Path = "D:\\",
        Database_Size_Mb = 1,
        Database_SizeGrowth_Mb = 1
    };

    public static readonly IClient Client11 = ShardingFactory.CreateClient(DataBaseType.Oracle, oracleConfig);
    public static readonly IClient Client12 = ShardingFactory.CreateClient(DataBaseType.Oracle, oracleConfig);

    public static readonly ReadWirteClient RWClient = ShardingFactory.CreateReadWriteClient(Client, Client2);

    public static IDatabase Db
    {
        get
        {
            Client.AutoCompareTableColumn = true;
            return Client.GetDatabase("demo");
        }
    }

    public static IDatabase Db2
    {
        get
        {
            return Client2.GetDatabase("test");
        }
    }

    public static ITableManager TableManager
    {
        get
        {
            return Db.GetTableManager("people");
        }
    }

    public static ITable<People> peopleTable
    {
        get
        {
            return Db.GetTable<People>("people");
        }
    }

    public static ITable<Student> studentTable
    {
        get
        {
            return Db.GetTable<Student>("student");
        }
    }

    public static ITable<Teacher> teacherTable
    {
        get
        {
            return Db.GetTable<Teacher>("teacher");
        }
    }

    public static ShardingQuery<Student> ShardingQueryStudent
    {
        get
        {
            var list = new ITable<Student>[]
              {
                    Db.GetTable<Student>("s1"),
                    Db.GetTable<Student>("s2"),
                    Db.GetTable<Student>("s3"),
                    Db2.GetTable<Student>("s4"),
                    Db2.GetTable<Student>("s5"),
                    Db2.GetTable<Student>("s6")
              };
            return new ShardingQuery<Student>(list);
        }
    }

    public static ShardingQuery<Teacher> ShardingQueryTeacher
    {
        get
        {
            var list = new ITable<Teacher>[]
              {
                    Db.GetTable<Teacher>("t1"),
                    Db.GetTable<Teacher>("t2"),
                    Db.GetTable<Teacher>("t3"),
                    Db2.GetTable<Teacher>("t4"),
                    Db2.GetTable<Teacher>("t5"),
                    Db2.GetTable<Teacher>("t6")
              };
            return ShardingFactory.CreateShardingQuery(list);
        }
    }

    public static ISharding<Student> ShardingHash
    {
        get
        {
            var list = new ITable<Student>[]
              {
                    Db.GetTable<Student>("s1"),
                    Db.GetTable<Student>("s2"),
                    Db.GetTable<Student>("s3"),
                    Db2.GetTable<Student>("s4"),
                    Db2.GetTable<Student>("s5"),
                    Db2.GetTable<Student>("s6")
              };
            return ShardingFactory.CreateShardingHash(list);
        }
    }

    public static ISharding<Teacher> ShardingRange
    {
        get
        {
            var dict = new Dictionary<long, ITable<Teacher>>()
            {
                {20000, Db.GetTable<Teacher>("t1") },
                {40000, Db.GetTable<Teacher>("t2") },
                {60000, Db.GetTable<Teacher>("t3") },
                {80000, Db2.GetTable<Teacher>("t4") },
                {90000, Db2.GetTable<Teacher>("t5") },
                {100000, Db2.GetTable<Teacher>("t6") },
            };
            return ShardingFactory.CreateShardingRange(dict);
        }
    }
}
