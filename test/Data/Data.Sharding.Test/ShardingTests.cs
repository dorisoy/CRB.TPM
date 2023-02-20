using CRB.TPM.Data.Sharding;
using NUnit.Framework;
using System.Collections.Generic;


namespace Data.Sharding.Test;

/// <summary>
/// 测试区间分片
/// </summary>
public class ShardingTests : BaseTest
{
    [SetUp]
    public void Setup()
    {
    }


    [Test]
    public void Insert()
    {
        var student = new Student { Id = ShardingFactory.NextObjectId(), Name = "李四", Age = 1 };
        ShardingHash.Insert(student);

        var teacher = new Teacher { Id = 1, Name = "李四", Age = 1 };
        ShardingRange.Merge(teacher);
    }

    [Test]
    public void InsertList()
    {
        //var list = new List<Student>();
        //for (int i = 0; i < 100000; i++)
        //{
        //    list.Add(new Student { Id = ShardingNextObjectId(), Name = "李四" + i, Age = i });
        //}
        //ShardingHash.Insert(list);

        var list2 = new List<Teacher>();
        for (int i = 0; i < 100000; i++)
        {
            list2.Add(new Teacher { Id = i, Name = "李四" + i, Age = i });
        }
        ShardingRange.Insert(list2);
    }
}
