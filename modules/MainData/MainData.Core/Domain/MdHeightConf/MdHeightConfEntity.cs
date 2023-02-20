
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MdHeightConf
{
    /// <summary>
    /// 制高点配置 M_HeightConf
    /// </summary>
    [Table("MD_HeightConf")]
    public partial class MdHeightConfEntity : Entity<Guid>
    {
        /// <summary>
        ///  
        /// </summary>
        [Length(14)]
        public string SaleOrg { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        [Length(10)]
        public string Height { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(40)]
        public string Text { get; set; } = string.Empty;

    }
}
