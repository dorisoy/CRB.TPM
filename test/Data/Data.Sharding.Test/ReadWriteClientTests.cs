using Newtonsoft.Json;
using NUnit.Framework;
using System;

namespace Data.Sharding.Test;

/// <summary>
/// 测试读写分离
/// </summary>
public class ReadWriteClientTests : BaseTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GetById()
    {

        var dbRead = RWClient.GetReadDatabase("test");
        var dbWrite = RWClient.GetWriteDataBase("test");

        var model = dbRead.GetTable<People>("People").GetById(1);
        var model2 = dbWrite.GetTable<People>("People").GetById(1);

        var model3 = RWClient.GetReadTable<People>("People", "test").GetById(1);

        Console.WriteLine(JsonConvert.SerializeObject(model));
        Console.WriteLine(JsonConvert.SerializeObject(model2));
        Console.WriteLine(JsonConvert.SerializeObject(model3));
    }
}
