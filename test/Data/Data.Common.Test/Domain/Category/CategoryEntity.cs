using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;

namespace Data.Common.Test.Domain.Category
{
    /// <summary>
    /// 文章分类
    /// </summary>
    [Table("MyCategory")]
    public class CategoryEntity : EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Length(100)]
        public string Name { get; set; }
    }
}
