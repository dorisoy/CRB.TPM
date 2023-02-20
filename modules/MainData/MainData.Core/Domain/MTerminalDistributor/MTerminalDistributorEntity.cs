
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Vo;
using CRB.TPM.Utils.Annotations;

namespace CRB.TPM.Mod.MainData.Core.Domain.MTerminalDistributor
{
    /// <summary>
    /// 终端与经销商的关系信息
    /// </summary>
    [Table("M_Terminal_Distributor")]
    public partial class MTerminalDistributorEntity : Entity<Guid>
    {
        /// <summary>
        /// 终端编码
        /// </summary>
        [Length(10)]
        public string TerminalCode { get; set; } = string.Empty;

        /// <summary>
        /// 经销商编码
        /// </summary>
        [Length(10)]
        public string DistributorCode { get; set; } = string.Empty;

        /// <summary>
        /// 终端id
        /// </summary>
        public Guid TerminalId { get; set; }

        /// <summary>
        /// 经销商id/分销商id
        /// </summary>
        public Guid DistributorId { get; set; }
    }
}
