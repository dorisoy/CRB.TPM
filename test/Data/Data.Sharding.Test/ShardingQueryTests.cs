using Newtonsoft.Json;
using NUnit.Framework;
using System;

namespace Data.Sharding.Test;

/// <summary>
/// 测试分片查询
/// </summary>
public class ShardingQueryTests : BaseTest
{
    [SetUp]
    public void Setup()
    {
    }


    [Test]
    public void Exists()
    {
        var data = ShardingQueryStudent.ExistsAsync("a").Result;
        Console.WriteLine(data);

        var data2 = ShardingQueryStudent.ExistsAsync("5f6b031cd47126c7f4308212").Result;
        Console.WriteLine(data2);
    }

    [Test]
    public void Count()
    {
        var data = ShardingQueryStudent.CountAsync().Result;
        Console.WriteLine(data);

        var data2 = ShardingQueryStudent.CountAsync("WHERE Name=@Name", new { Name = "李四1" }).Result;
        Console.WriteLine(data2);
    }

    [Test]
    public void Min()
    {
        var data = ShardingQueryStudent.MinAsync<long>("Age").Result;
        Console.WriteLine(data);
    }

    [Test]
    public void Max()
    {
        var data = ShardingQueryStudent.MaxAsync<long>("Age").Result;
        Console.WriteLine(data);
    }

    [Test]
    public void Sum()
    {
        var data = ShardingQueryStudent.SumLongAsync("Age").Result;
        Console.WriteLine(data);

        var data2 = ShardingQueryStudent.SumDecimalAsync("Age").Result;
        Console.WriteLine(data2);
    }

    [Test]
    public void Avg()
    {
        var data = ShardingQueryStudent.AvgAsync("Age").Result;
        Console.WriteLine(data);
    }

    [Test]
    public void GetAll()
    {
        var data = ShardingQueryStudent.GetAllAsync("Age", "Age DESC").Result;
        Console.WriteLine(JsonConvert.SerializeObject(data));
    }

    [Test]
    public void GetById()
    {
        var data = ShardingQueryTeacher.GetByIdAsync(66).Result;
        Console.WriteLine(JsonConvert.SerializeObject(data));
    }

    [Test]
    public void GetByIds()
    {
        var data = ShardingQueryTeacher.GetByIdsAsync(new long[] { 1, 5, 8 }).Result;
        Console.WriteLine(JsonConvert.SerializeObject(data));
    }

    [Test]
    public void GetByIdsWithField()
    {
        var data = ShardingQueryTeacher.GetByIdsWithFieldAsync(new int[] { 1, 3, 5 }, "Age").Result;
        Console.WriteLine(JsonConvert.SerializeObject(data));
    }

    [Test]
    public void GetByWhere()
    {
        var data = ShardingQueryTeacher.GetByWhereAsync("WHERE Age>@Age", new { Age = 50 }, orderby: "Id DESC", limit: 5).Result;
        Console.WriteLine(JsonConvert.SerializeObject(data));
    }

    [Test]
    public void GetByWhereFirst()
    {
        var data = ShardingQueryTeacher.GetByWhereFirstAsync("WHERE Id=@Id", new { Id = 111111111111 }).Result;
        Console.WriteLine(JsonConvert.SerializeObject(data));
    }

    [Test]
    public void GetBySkipTake()
    {
        var data = ShardingQueryTeacher.GetBySkipTakeAsync(0, 2).Result;
        Console.WriteLine(JsonConvert.SerializeObject(data));
    }

    [Test]
    public void GetByPage()
    {
        var data = ShardingQueryTeacher.GetByPageAsync(1, 2).Result;
        Console.WriteLine(JsonConvert.SerializeObject(data));
    }

    [Test]
    public void GetByPageAndCount()
    {
        var data = ShardingQueryTeacher.GetByPageAndCountAsync(2, 8).Result;
        Console.WriteLine(data.Count);
        Console.WriteLine(JsonConvert.SerializeObject(data.Data));
    }

    [Test]
    public void GetByAscPage()
    {
        var data1 = ShardingQueryTeacher.GetByAscFirstPageAsync(2).Result;
        Console.WriteLine(JsonConvert.SerializeObject(data1));

        var data2 = ShardingQueryTeacher.GetByAscPrevPageAsync(2, new Teacher { Id = 5 }).Result;
        Console.WriteLine(JsonConvert.SerializeObject(data2));

        var data3 = ShardingQueryTeacher.GetByAscCurrentPageAsync(2, new Teacher { Id = 5 }).Result;
        Console.WriteLine(JsonConvert.SerializeObject(data3));

        var data4 = ShardingQueryTeacher.GetByAscNextPageAsync(2, new Teacher { Id = 6 }).Result;
        Console.WriteLine(JsonConvert.SerializeObject(data4));

        var data5 = ShardingQueryTeacher.GetByAscLastPageAsync(2).Result;
        Console.WriteLine(JsonConvert.SerializeObject(data5));
    }

    [Test]
    public void GetByDescPage()
    {
        var data1 = ShardingQueryTeacher.GetByDescFirstPageAsync(2).Result;
        Console.WriteLine(JsonConvert.SerializeObject(data1));

        var data2 = ShardingQueryTeacher.GetByDescPrevPageAsync(2, new Teacher { Id = 19 }).Result;
        Console.WriteLine(JsonConvert.SerializeObject(data2));

        var data3 = ShardingQueryTeacher.GetByDescCurrentPageAsync(2, new Teacher { Id = 19 }).Result;
        Console.WriteLine(JsonConvert.SerializeObject(data3));

        var data4 = ShardingQueryTeacher.GetByDescNextPageAsync(2, new Teacher { Id = 18 }).Result;
        Console.WriteLine(JsonConvert.SerializeObject(data4));

        var data5 = ShardingQueryTeacher.GetByDescLastPageAsync(2).Result;
        Console.WriteLine(JsonConvert.SerializeObject(data5));
    }

}
