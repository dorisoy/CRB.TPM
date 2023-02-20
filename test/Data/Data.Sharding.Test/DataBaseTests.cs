using Newtonsoft.Json;
using NUnit.Framework;
using System;

namespace Data.Sharding.Test;

/// <summary>
/// 基本操作测试
/// </summary>
public class DataBaseTests : BaseTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GetTable()
    {
        Db.GetTable<People>("people");
        Db.GetTable<Teacher>("teacher");
        Db.GetTable<Student>("student");
    }

    [Test]
    public void DropTable()
    {
        Db.DropTable("p2");
    }

    [Test]
    public void TruncateTable()
    {
        Db.TruncateTable("p2");
    }

    [Test]
    public void GetTableList()
    {
        var data = Db.GetTableList();
        Assert.Pass(JsonConvert.SerializeObject(data));
    }

    [Test]
    public void GetTableColumnList()
    {
        var data = Db.GetTableColumnList("people");
        Assert.Pass(JsonConvert.SerializeObject(data));
    }

    [Test]
    public void ExistsTable()
    {
        Console.WriteLine(Db.ExistsTable("people"));
        Console.WriteLine(Db.ExistsTable("people2222"));
    }

    [Test]
    public void ShowTableScript()
    {
        Console.WriteLine(Db.GetTableScript<People>("people"));
        Console.WriteLine("\r\n");
        Console.WriteLine("\r\n");
        Console.WriteLine(Db.GetTableScript<Student>("sss"));
    }

    [Test]
    public void GetTableEntityFromDatabase()
    {
        Console.WriteLine(JsonConvert.SerializeObject(Db.GetTableEntityFromDatabase("people")));
    }

    [Test]
    public void GeneratorTableFile()
    {
        Db.GeneratorTableFile("D:\\Class", classNameToUpper: false);
    }

    [Test]
    public void GeneratorDbContextFile()
    {
        Db.GeneratorDbContextFile("D:\\Class");
    }

}
