
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MDistributorRelation
{
    /// <summary>
    /// 经销商分销商关系表
    /// </summary>
    [Table("M_DistributorRelation")]
    public partial class MDistributorRelationEntity : Entity<Guid>
    {
        /// <summary>
        /// 经销商编码
        /// </summary>
        [Length(30)]
        public string DistributorCode1 { get; set; } = string.Empty;

        /// <summary>
        /// 分销商编码
        /// </summary>
        [Length(30)]
        public string DistributorCode2 { get; set; } = string.Empty;

        /// <summary>
        /// 经销商id
        /// </summary>
        public Guid DistributorId1 { get; set; }

        /// <summary>
        /// 分销商id
        /// </summary>
        public Guid DistributorId2 { get; set; }
    }
}
