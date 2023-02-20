using CRB.TPM.Data.Sharding;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using NUnit.Framework.Internal;

namespace Data.Sharding.Test;

/// <summary>
/// 分布式事务测试
/// </summary>
public class DistributedTransactionTests : BaseTest
{
    //new DataBaseConfig config;
    //IClient client;
    //IDatabase db;

    [SetUp]
    public void Setup()
    {
        //config = new DataBaseConfig { Server = "127.0.0.1", UserId = "root", Password = "racing.1", Port = 3306 };
        //client = ShardingFactory.CreateClient(DataBaseType.MySql, config);
        //db = client.GetDatabase("test2");
    }

    [Test]
    public void BeginTran()
    {
        //var tran = new DistributedTransaction();
        var tran = ShardingFactory.CreateDistributedTransaction();

        try
        {
            var p = new People
            {
                Name = "李四",
                Age = 50,
                AddTime = DateTime.Now,
                IsAdmin = 1,
                Text = "你好"
            };
            peopleTable.Insert(p, tran);

            //这里模拟抛出异常
            throw new Exception("发生错误");

            tran.Commit();
        }
        catch
        {
            tran.Rollback();
            Assert.Pass("事务回滚");
        }
    }

    [Test]
    public void BeginTran2()
    {
        int? i = 0;

        var db1 = Client.GetDatabase("z1");
        var db2 = Client.GetDatabase("z2");

        var s1 = db1.GetTable<Student>("student");
        var s2 = db2.GetTable<Student>("student");
   
        var tran = ShardingFactory.CreateDistributedTransaction();

        tran.ShowResult = true;

        try
        {
            var model = new Student { Id = ShardingFactory.NextObjectId(), Name = "李四" };

            s1.Insert(model, tran);
   
            s2.Insert(model, tran);

            //这里模拟抛出异常
            throw new Exception("发生错误");

            tran.Commit();
        }
        catch
        {
            tran.Rollback();
        }
    }
}
