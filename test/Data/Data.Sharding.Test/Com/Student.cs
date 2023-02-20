using CRB.TPM.Data.Sharding;

namespace Data.Sharding.Test
{
    [Table("Id", false, "学生表", "Memory")]
    public class Student
    {
        [Column(24, "主键id")]
        public string Id { get; set; }

        [Column(50, "名字")]
        public string Name { get; set; }

        [Column(20, "年龄")]
        public long Age { get; set; }

    }
}
