
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MEntity
{
    /// <summary>
    /// 业务实体
    /// </summary>
    [Table("M_Entity")]
    public partial class MEntityEntity : Entity<Guid>
    {
        /// <summary>
        /// 编码
        /// </summary>
        [Length(30)]
        public string EntityCode { get; set; } = string.Empty;

        /// <summary>
        /// 名称
        /// </summary>
        [Length(40)]
        public string EntityName { get; set; } = string.Empty;

        /// <summary>
        /// 状态
        /// </summary>
        public int Enabled { get; set; }

        /// <summary>
        /// 用于上传OCMS
        /// </summary>
        [Length(30)]
        public string ERPCode { get; set; } = string.Empty;

    }
}
