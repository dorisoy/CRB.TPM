using CRB.TPM.Data.Sharding;
using Newtonsoft.Json;
using NUnit.Framework;
using System;

namespace Data.Sharding.Test;

/// <summary>
/// 测试表管理器
/// </summary>
public class TableManagerTests : BaseTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CreateIndex()
    {
        TableManager.CreateIndex("Name", "Name", IndexType.Normal);
        TableManager.CreateIndex("Age", "Age", IndexType.Unique);
        TableManager.CreateIndex("NameAndAge", "Name,Age", IndexType.Unique);
    }

    [Test]
    public void DropIndex()
    {
        TableManager.DropIndex("Name");
        TableManager.DropIndex("Age");
        TableManager.DropIndex("NameAndAge");
    }

    [Test]
    public void GetIndexEntityList()
    {
        var data = TableManager.GetIndexEntityList();
        Console.WriteLine(JsonConvert.SerializeObject(data));
    }

    [Test]
    public void GetColumnEntityList()
    {
        var data = TableManager.GetColumnEntityList();
        Console.WriteLine(JsonConvert.SerializeObject(data));
    }

    [Test]
    public void AddColumn()
    {
        TableManager.AddColumn("NewColumn", typeof(string), 60, "新增字段stting");
        TableManager.AddColumn("NewColumn2", typeof(int), 0, "新增字段int");
    }

    [Test]
    public void DropColumn()
    {
        TableManager.DropColumn("NewColumn");
        TableManager.DropColumn("NewColumn2");
    }

}
