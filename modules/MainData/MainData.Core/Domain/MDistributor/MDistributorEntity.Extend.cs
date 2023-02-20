
using CRB.TPM.Data.Abstractions.Annotations;
using System;
using System.ComponentModel;

namespace CRB.TPM.Mod.MainData.Core.Domain.MDistributor
{
    public partial class MDistributorEntity
    {
        /// <summary>
        /// 工作站编码
        /// </summary>
        [NotMappingColumn]
        public string StationOrgCD { get; set; }
    }
    /// <summary>
    /// 1表示主户；2管理开户的子户；3TPM虚拟子户
    /// </summary>
    public enum DetailTypeEnum
    {
        [Description("表示主户")]
        PrimaryAccount = 1,
        [Description("管理开户的子户")]
        ManageSubAccounts = 2,
        [Description("TPM虚拟子户")]
        TPMVirtualSubAccount = 3
    }
}
