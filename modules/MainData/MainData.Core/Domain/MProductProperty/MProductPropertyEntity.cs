
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MProductProperty
{
    /// <summary>
    /// 产品属性
    /// </summary>
    [Table("M_ProductProperty")]
    public partial class MProductPropertyEntity : Entity<Guid>
    {
        /// <summary>
        /// 类型id
        /// </summary>
        public int ProductPropertiesType { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        //public string ProductPropertiesTypeName { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
		[Nullable]
        public string ProductPropertiesCode { get; set; } = string.Empty;

        /// <summary>
        /// 名称
        /// </summary>
		[Nullable]
        [Length(100)]
        public string ProductPropertiesName { get; set; } = string.Empty;

        /// <summary>
        ///  排序
        /// </summary>
        public int Sort { get; set; }
    }
}
