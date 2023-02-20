
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MdSaleLine
{
    /// <summary>
    /// 业务线 D_SaleLine
    /// </summary>
    [Table("MD_SaleLine")]
    public partial class MdSaleLineEntity : Entity<Guid>
    {
        /// <summary>
        ///  
        /// </summary>
        [Length(10)]
        public string LineCD { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        [Length(40)]
        public string LineNm { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        public int Status { get; set; }

    }
}
