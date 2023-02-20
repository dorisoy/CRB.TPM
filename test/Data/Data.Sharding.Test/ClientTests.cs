using CRB.TPM.Data.Sharding;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Data.Sharding.Test;

/// <summary>
/// 客户端类操作测试
/// </summary>
public class ClientTests : BaseTest
{
    [SetUp]
    public void Setup()
    {
    }


    [Test]
    public void CreateDatabase()
    {
        Client.CreateDatabase("demo");
    }

    [Test]
    public void DropDatabase()
    {

        Client.DropDatabase("demo");
    }


    [Test]
    public void GetDatbase()
    {
        Client.GetDatabase("demo");
    }

    [Test]
    public void ExistDatabase()
    {
        bool exists = Client.ExistsDatabase("demo");
        Console.WriteLine(exists);
    }

    [Test]
    public void ShowDatabases()
    {
        var databases = Client.ShowDatabases();
        Console.WriteLine(JsonConvert.SerializeObject(databases));
    }

    [Test]
    public void ShowDatabasesWithOutSystem()
    {
        var databases = Client.ShowDatabasesExcludeSystem();
        Console.WriteLine(JsonConvert.SerializeObject(databases));
    }

    [Test]
    public void ClearCache()
    {
        Client.ClearCache();
    }

    [Test]
    public void NextId()
    {
        var id = ShardingFactory.NextLongIdAsString();
        var id2 = ShardingFactory.NextObjectId();
        var id3 = ShardingFactory.NextSnowIdAsString();
        Assert.Pass($"{id}\n{id2}\n{id3}");
    }

    [Test]
    public async Task TestThreadDb()
    {
        var task1 = Task.Run(() =>
        {
            return Client.GetDatabase("zzz1");
        });

        var task2 = Task.Run(() =>
        {
            return Client.GetDatabase("zzz1");
        });

        await Task.WhenAll(task1, task2);
        Console.WriteLine(task1.Result.Equals(task2.Result));
        Console.WriteLine("没问题");

    }

    [Test]
    public async Task TestThreadTable()
    {
        var task1 = Task.Run(() =>
        {
            return Client.GetDatabase("zzz1").GetTable<People>("p");
        });

        var task2 = Task.Run(() =>
        {
            return Client.GetDatabase("zzz1").GetTable<People>("p");
        });

        await Task.WhenAll(task1, task2);
        Console.WriteLine(task1.Result.Equals(task2.Result));
        Console.WriteLine("没问题");
    }

}


