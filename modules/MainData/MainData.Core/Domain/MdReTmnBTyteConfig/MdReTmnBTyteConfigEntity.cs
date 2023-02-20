
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MdReTmnBTyteConfig
{
    /// <summary>
    ///  终端业态关系表 M_Re_Tmn_BTyte_Config
    /// </summary>
    [Table("MD_Re_Tmn_BTyte_Config")]
    public partial class MdReTmnBTyteConfigEntity : Entity<Guid>
    {
        /// <summary>
        /// 终端编码
        /// </summary>
        [Length(10)]
        public string TmnStoreType1 { get; set; } = string.Empty;

        /// <summary>
        /// 关系类型(ZS003:终端负责员工/ZS001:经销商业务员)
        /// </summary>
        [Length(10)]
        public string ZbnType { get; set; } = string.Empty;

        /// <summary>
        /// 创建人
        /// </summary>
		[Nullable]
        [Length(40)]
        public string ZbnTypeTxt { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        public int Status { get; set; }

    }
}
