using CRB.TPM.Data.Sharding;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Data.Sharding.Test;

/// <summary>
/// 测试表合并
/// </summary>
class Map2Tests : BaseTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Map()
    {
        var list1 = new List<AA>
        {
            new AA{ Id=1,BId=null },
            new AA{ Id=2,BId="" },
            new AA{ Id=3,BId="1" },
            new AA{ Id=4,BId="2" }
        };

        var list2 = new List<BB>
        {
            new BB { Id = "", Name = "哈哈1" },
            new BB { Id = "1", Name = "哈哈2" },
            new BB { Id = "2", Name = "哈哈3" }
        };

        var table1 = Client.GetDatabase("test").GetTable<AA>("aa");
        table1.Merge(list1);
        var table2 = Client.GetDatabase("test").GetTable<BB>("bb");
        table2.Merge(list2);

        var data1 = table1.GetAll();
        data1.TableOneToOne("BId", "_BB", table2, "Id");
        Console.WriteLine(JsonConvert.SerializeObject(data1));


    }


    [Table("Id", false)]
    public class AA
    {
        public int Id { get; set; }

        public string BId { get; set; }

        [CRB.TPM.Data.Sharding.Ignore]
        public BB _BB { get; set; }

    }

    [Table("Id", false)]
    public class BB
    {
        public string Id { get; set; }

        public string Name { get; set; }

    }
}
