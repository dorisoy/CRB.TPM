
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MTerminalUser
{
    /// <summary>
    /// 终端与经销商的关系信息
    /// </summary>
    [Table("M_Terminal_User")]
    public partial class MTerminalUserEntity : Entity<Guid>
    {
        /// <summary>
        /// 终端编码
        /// </summary>
        [Length(10)]
        public string TerminalCode { get; set; } = string.Empty;

        /// <summary>
        /// 业务员
        /// </summary>
        [Length(10)]
        public string UserBP { get; set; } = string.Empty;
        /// <summary>
        /// 用户id
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// 终端id
        /// </summary>
        public Guid TerminalId { get; set; }
    }
}
