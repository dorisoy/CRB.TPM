
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MMarketingSetup
{
    /// <summary>
    /// 营销中心配置
    /// </summary>
    [Table("M_MarketingSetup")]
    public partial class MMarketingSetupEntity : EntityBase<Guid>
    {
        /// <summary>
        /// 营销中心id
        /// </summary>
        public Guid MarketingId { get; set; }

        /// <summary>
        /// 是否真实营销中心
        /// </summary>
        public int IsReal { get; set; }

        /// <summary>
        /// 是否同步CRM组织
        /// </summary>
        public int IsSynchronizeCRM { get; set; }

        /// <summary>
        /// 客户是否同步CRM工作站
        /// </summary>
        public int IsSynchronizeCRMDistributorStation { get; set; }

    }
}
