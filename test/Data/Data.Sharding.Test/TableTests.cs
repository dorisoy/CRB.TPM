using CRB.TPM.Data.Sharding;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace Data.Sharding.Test;


/// <summary>
/// 测试表操作
/// </summary>
public class TableTests : BaseTest
{
    [SetUp]
    public void Setup()
    {
    }


    [Test]
    public void Insert()
    {
        var p = new People
        {
            Id = DateTime.Now.Millisecond,
            Name = "李四",
            Age = 50,
            AddTime = DateTime.Now,
            IsAdmin = 1,
            Text = "你好",
            Money = 10.5M,
            Money2 = 10.888F,
            Money3 = 50.55

        };
        peopleTable.Insert(p);
        Console.WriteLine(p.Id);

        //使用雪花ID
        var teacher = new Teacher
        {
            Id = ShardingFactory.NextSnowId(),
            Name = "王老师",
            Age = 5
        };
        teacherTable.Insert(teacher);
        Console.WriteLine(teacher.Id);

        //使用ObjectId
        var student = new Student
        {
            Id = ShardingFactory.NextObjectId(),
            Name = "李同学",
            Age = 100
        };
        studentTable.Insert(student);
        Console.WriteLine(student.Id);
    }

    [Test]
    public void InsertIfNoExists()
    {
        var p = new People
        {
            Id = 12,
            Name = "李四",
            Age = 50,
            AddTime = DateTime.Now,
            IsAdmin = 1,
            Text = "你好"
        };
        //标识插入，如果不存在
        peopleTable.InsertIdentity(p);
        Console.WriteLine(p.Id);
    }

    [Test]
    public void InsertList()
    {
        var modelList = new List<People>();
        for (int i = 0; i < 40000; i++)
        {
            modelList.Add(new People { Id = i, Name = "李白" + i, AddTime = DateTime.Now });
        }
        peopleTable.Insert(modelList);

        Console.WriteLine(modelList[0].Id);

        //var list = new List<Student>();
        //for (int i = 0; i < 40000; i++)
        //{
        //    list.Add(new Student { Id = ShardingNextObjectId(), Name = "李四" + i, Age = i });
        //}
        //studentTable.Insert(list);

    }

    [Test]
    public void InsertIdentity()
    {

        var table = new People
        {
            Id = 11,
            Name = "自动添加id11",
            AddTime = DateTime.Now,
            IsAdmin = 1,
            Text = "你好",
            LongText = "1",
            Money = 10.5M,
            Money2 = 10.888F,
            Money3 = 50.55
        };
        //标识插入，如果不存在
        peopleTable.InsertIdentityIfNoExists(table);
    }

    [Test]
    public void InsertIdentityList()
    {
        var modelList = new List<People>
        {
            new People{ Id = 17, Name = "李白17",AddTime = DateTime.Now },
            new People{ Id = 18,Name = "李白18",AddTime = DateTime.Now },
            new People{ Id = 19,Name = "李白19",AddTime = DateTime.Now },
            new People{ Id = 20,Name = "李白20",AddTime = DateTime.Now },
            new People{ Id = 21, Name = "李白21",AddTime = DateTime.Now },
            new People{ Id = 22,Name = "李白22",AddTime = DateTime.Now },
            new People{ Id = 23,Name = "李白23",AddTime = DateTime.Now }
        };
        //批量标识插入，如果不存在
        peopleTable.InsertIdentityIfNoExists(modelList);
    }

    [Test]
    public void Merge()
    {
        var p = new People
        {
            Id = 1,
            Name = "DF",
            Age = 222,
            AddTime = DateTime.Now,
            IsAdmin = 1,
            Text = "水电费第三方"
        };
        //合并到表
        peopleTable.Merge(p);
        Console.WriteLine(p.Id);
    }

    [Test]
    public void Update()
    {
        var model = new People
        {
            Id = 1,
            Name = "李四1111",
            Age = 51111,
            Text = "你好11",
            LongText = "2",
            Money = 500M,
            AddTime = DateTime.Now

        };
        peopleTable.Update(model);
    }

    [Test]
    public void UpdateList()
    {
        var modelList = new List<People>
        {
            new People{ Id=1,Name="小黑11" ,Age = 1,AddTime = DateTime.Now},
            new People{ Id=2,Name="小白222",Age = 2 ,AddTime = DateTime.Now}
        };
        peopleTable.Update(modelList, new List<string> { "Name" });
    }

    [Test]
    public void UpdateFields()
    {
        var model = new People
        {
            Id = 1,
            Name = "111",
            Age = 123,
            Text = "666",
            Money = 200M,
            AddTime = DateTime.Now
        };
        peopleTable.Update(model, new List<string> { "Money", "AddTime" });
    }

    [Test]
    public void UpdateIgnore()
    {
        var model = new People
        {
            Id = 1,
            Name = "333",
            Age = 333,
            Text = "444",
            Money = 800M,
            AddTime = DateTime.Now
        };

        //忽略更新
        peopleTable.UpdateIgnore(model, new List<string> { "Name" });
    }

    [Test]
    public void UpdateByWhere()
    {
        var model = new People
        {
            Id = 1,
            Name = "李四1111",
            Age = 333,
            Text = "你好11",
            Money = 500M,
            AddTime = DateTime.Now
        };

        //按条件更新
        peopleTable.UpdateByWhere(model, "WHERE Age=@Age");
    }

    [Test]
    public void UpdateByWhere2()
    {
        var model = new People
        {
            Id = 1,
            Name = "111",
            Age = 333,
            Text = "666",
            Money = 200M,
            AddTime = DateTime.Now
        };
        peopleTable.UpdateByWhere(model, "WHERE Age=@Age", new List<string> { "Name" });
    }

    [Test]
    public void UpdateByWhere3()
    {
        var model = new People
        {
            Id = 1,
            Name = "333",
            Age = 333,
            Text = "444",
            Money = 800M,
            AddTime = DateTime.Now
        };

        peopleTable.UpdateByWhereIgnore(model, "WHERE Age=@Age", new List<string> { "Name" });
    }

    [Test]
    public void Delete()
    {
        peopleTable.Delete(1);
    }

    [Test]
    public void DeleteByIds()
    {
        peopleTable.DeleteByIds(new int[] { 5, 6, 7 });
    }

    [Test]
    public void DeleteByWhere()
    {
        peopleTable.DeleteByWhere("WHERE Age=@Age", new { Age = 44 });
    }

    [Test]
    public void DeleteAll()
    {
        studentTable.DeleteAll();
    }

    [Test]
    public void DataTable()
    {
        DataTable dt = null;
        Db.Using(conn =>
        {
            dt = conn.GetDataTable("SELECT * FROM people LIMIT 1");
        });

        Console.WriteLine(JsonConvert.SerializeObject(dt));
    }

    [Test]
    public void GetAllPeople()
    {
        peopleTable.Insert(new People { Name = "马六", bb = true });
        var data = peopleTable.GetAll();

        Console.WriteLine(data.FirstOrDefault()?.bb);

        var dt = data.ToDataTable();

        Console.WriteLine(dt.Rows.Count + dt.Rows[0]["Name"].ToString());

        var lisss = dt.ToEnumerableList<People>();

        Console.WriteLine(lisss.Count() + lisss.First().Name);
    }

    [Test]
    public void SeqUpdate()
    {
        if (Db.DbType != DataBaseType.Postgresql)
            return;

        //获取ppp表，如果不存在则创建
        var tt = Db.GetTable<People>("ppp");
        //合并到表
        tt.Merge(new People { Id = 1 });
        tt.Merge(new People { Id = 5 });

        //更新序列
        tt.SeqUpdate();
        tt.Insert(new People());
    }

}
