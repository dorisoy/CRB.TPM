using CRB.TPM.Data.Sharding;
using System;
using System.Collections.Generic;
using IgnoreAttribute = CRB.TPM.Data.Sharding.IgnoreAttribute;

namespace Data.Sharding.Test
{
    //[Index("Name", "Name", IndexType.Normal)]
    //[Index("Age", "Age", IndexType.Unique)]
    //[Index("NameAndAge", "Name,Age", IndexType.Unique)]
    [Table("Id", true, "人类表", "Log")]
    public class People
    {
        [Column(11, "主键id")]
        public long Id { get; set; }

        [Column(50, "名字")]
        public string Name { get; set; }

        [Column(20, "年龄")]
        public long Age { get; set; }

        [Column(-1, "长文章")]
        public string Text { get; set; }

        [Column(-2, "超级长文章")]
        public string LongText { get; set; }

        [Column(18.2, "金钱一")]
        public decimal Money { get; set; }

        public float Money2 { get; set; }

        public double Money3 { get; set; }

        public int IsAdmin { get; set; }

        //[Column(6)]
        public DateTime AddTime { get; set; }

        public short ShortField { get; set; }

        //public byte ByteField { get; set; }

        [Ignore]
        public string NoDataBaseColumn { get; set; }

        public bool bb { get; set; }

        [JsonString]
        public IList<int> MyJson { get; set; }

    }
}
