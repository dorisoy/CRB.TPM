using Newtonsoft.Json;
using NUnit.Framework;
using System;

namespace Data.Sharding.Test;

/// <summary>
/// 测试表查询
/// </summary>
public class TableQueryTests : BaseTest
{
    [SetUp]
    public void Setup()
    {
    }


    [Test]
    public void Exists()
    {
        var result = peopleTable.Exists(1);
        Console.WriteLine(result);
    }

    [Test]
    public void Count()
    {
        var count = peopleTable.Count();
        peopleTable.Count("WHERE Name=@Name", new { Name = "李四" });
        Console.WriteLine(count);
    }

    [Test]
    public void Min()
    {
        var count = peopleTable.Min<int>("Id");
        Console.WriteLine(count);

    }

    [Test]
    public void Max()
    {
        var count = peopleTable.Max<int>("Id");
        Console.WriteLine(count);
    }

    [Test]
    public void Sum()
    {
        var count = peopleTable.Sum<int>("Id");
        Console.WriteLine(count);

    }

    [Test]
    public void Avg()
    {
        var count = peopleTable.Avg("Id");
        Console.WriteLine(count);

    }

    [Test]
    public void GetAll()
    {
        var data = peopleTable.GetAll();
        Console.WriteLine(JsonConvert.SerializeObject(data));

        var data1 = peopleTable.GetAll("Id,Name");
        Console.WriteLine(JsonConvert.SerializeObject(data1));

        var data2 = peopleTable.GetAll("Id", "Id DESC");

        Console.WriteLine(JsonConvert.SerializeObject(data2));
    }

    [Test]
    public void GetById()
    {
        var model = peopleTable.GetById(1);
        Console.WriteLine(JsonConvert.SerializeObject(model));

        var model2 = peopleTable.GetById(1, "Id,Name,Text");
        Console.WriteLine(JsonConvert.SerializeObject(model2));

        var model3 = peopleTable.GetByIdDynamic(1, "Id,Name,Text");
        Console.WriteLine(JsonConvert.SerializeObject(model3));
    }

    [Test]
    public void GetByIdForUpdate()
    {
        //var model = peopleTable.GetByIdForUpdate(1);
        //Console.WriteLine(JsonConvert.SerializeObject(model));

    }

    [Test]
    public void GetByIds()
    {
        var list = peopleTable.GetByIds(new long[] { 1L, 2L, 3L });
        Console.WriteLine(JsonConvert.SerializeObject(list));

        var list2 = peopleTable.GetByIds(new long[] { 1L, 2L, 3L }, "Id,Name");
        Console.WriteLine(JsonConvert.SerializeObject(list2));
    }

    [Test]
    public void GetByIdsWithField()
    {
        var list = peopleTable.GetByIdsWithField(new string[] { "2", "3" }, "Name");
        Console.WriteLine(JsonConvert.SerializeObject(list));

        var list2 = peopleTable.GetByIdsWithField(new string[] { "2", "3" }, "Name", "Id,Name");
        Console.WriteLine(JsonConvert.SerializeObject(list2));
    }

    [Test]
    public void GetByWhere()
    {
        var list = peopleTable.GetByWhere("WHERE Id>@Id", new { Id = 8 }, limit: 10);
        Console.WriteLine(JsonConvert.SerializeObject(list));

        var list2 = peopleTable.GetByWhere("WHERE Id>@Id", new { Id = 8 }, "Id,Name", "ID DESC", limit: 10);
        Console.WriteLine(JsonConvert.SerializeObject(list2));
    }

    [Test]
    public void GetByWhereFirst()
    {
        var model = peopleTable.GetByWhereFirst("WHERE Id=@Id", new { id = 8 });
        Console.WriteLine(JsonConvert.SerializeObject(model));

        var model2 = peopleTable.GetByWhereFirst("WHERE Id=@Id", new { id = 8 }, "Id,Name");
        Console.WriteLine(JsonConvert.SerializeObject(model2));
    }

    [Test]
    public void GetBySkipTake()
    {
        var list = peopleTable.GetBySkipTake(0, 2);
        Console.WriteLine(JsonConvert.SerializeObject(list));

        var list2 = peopleTable.GetBySkipTake(0, 2, "WHERE Id=@Id", new { Id = 1 });
        Console.WriteLine(JsonConvert.SerializeObject(list2));

        var list3 = peopleTable.GetBySkipTake(0, 2, "WHERE Id=@Id", new { Id = 1 }, "Id,Name");
        Console.WriteLine(JsonConvert.SerializeObject(list3));
    }

    [Test]
    public void GetByPage()
    {
        var list = peopleTable.GetByPage(1, 2);
        Console.WriteLine(JsonConvert.SerializeObject(list));

        var list2 = peopleTable.GetByPage(1, 2, "WHERE Id=@Id", new { Id = 1 });
        Console.WriteLine(JsonConvert.SerializeObject(list2));

        var list3 = peopleTable.GetByPage(1, 2, "WHERE Id=@Id", new { Id = 1 }, "Id,Name");
        Console.WriteLine(JsonConvert.SerializeObject(list3));
    }

    [Test]
    public void GetByPageAndCount()
    {
        //do not use tran at this method
        var list = peopleTable.GetByPageAndCount(1, 2);
        Console.WriteLine(list.Count);
        Console.WriteLine(JsonConvert.SerializeObject(list.Data));

        var list2 = peopleTable.GetByPageAndCount(1, 2, "WHERE Id=@Id", new { Id = 1 });
        Console.WriteLine(list2.Count);
        Console.WriteLine(JsonConvert.SerializeObject(list2.Data));

        var list3 = peopleTable.GetByPageAndCount(1, 2, "WHERE Id=@Id", new { Id = 1 }, "Id,Name");
        Console.WriteLine(list3.Count);
        Console.WriteLine(JsonConvert.SerializeObject(list3.Data));
    }

    [Test]
    public void GetByAscPage()
    {
        var data1 = peopleTable.GetByAscFirstPage(2);
        Console.WriteLine(JsonConvert.SerializeObject(data1));

        var data2 = peopleTable.GetByAscPrevPage(2, new People { Id = 5 });
        Console.WriteLine(JsonConvert.SerializeObject(data2));

        var data3 = peopleTable.GetByAscCurrentPage(2, new People { Id = 5 });
        Console.WriteLine(JsonConvert.SerializeObject(data3));

        var data4 = peopleTable.GetByAscNextPage(2, new People { Id = 6 });
        Console.WriteLine(JsonConvert.SerializeObject(data4));

        var data5 = peopleTable.GetByAscLastPage(2, null);
        Console.WriteLine(JsonConvert.SerializeObject(data5));
    }

    [Test]
    public void GetByDescPage()
    {
        var data1 = peopleTable.GetByDescFirstPage(1, null);
        Console.WriteLine(JsonConvert.SerializeObject(data1));

        var data2 = peopleTable.GetByDescPrevPage(1, new People { Id = 19 });
        Console.WriteLine(JsonConvert.SerializeObject(data2));

        var data3 = peopleTable.GetByDescCurrentPage(1, new People { Id = 19 });
        Console.WriteLine(JsonConvert.SerializeObject(data3));

        var data4 = peopleTable.GetByDescNextPage(1, new People { Id = 19 });
        Console.WriteLine(JsonConvert.SerializeObject(data4));

        var data5 = peopleTable.GetByDescLastPage(1, null);
        Console.WriteLine(JsonConvert.SerializeObject(data5));
    }

}
