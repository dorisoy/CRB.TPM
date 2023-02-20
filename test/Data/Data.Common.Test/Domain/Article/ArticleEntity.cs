using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;

using CRB.TPM.Data.Abstractions.Sharding;


namespace Data.Common.Test.Domain.Article
{
    //分表策略
    [Sharding(ShardingPolicy.Month)]
    public class ArticleEntity : EntityBaseSoftDelete<int>
    {
        [Column("分类编号")]
        public int CategoryId { get; set; }

        [Length(300)]
        [Column("标题")]
        public string Title { get; set; }

        [Length(0)]
        [Column("内容")]
        public string Content { get; set; }

        [Column("是否发布", null, null, "IsPublished")]
        public bool Published { get; set; }

        [Column("发布日期")]
        [ShardingField(true)]
        public DateTime? PublishedTime { get; set; }


        [Column("价格")]
        public decimal Price { get; set; }
    }
}
