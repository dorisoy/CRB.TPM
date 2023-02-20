
using CRB.TPM.Data.Abstractions.Annotations;
using System;
using System.ComponentModel;

namespace CRB.TPM.Mod.MainData.Core.Domain.MDistributor
{
    public partial class MDistributorEntity
    {
        /// <summary>
        /// ����վ����
        /// </summary>
        [NotMappingColumn]
        public string StationOrgCD { get; set; }
    }
    /// <summary>
    /// 1��ʾ������2���������ӻ���3TPM�����ӻ�
    /// </summary>
    public enum DetailTypeEnum
    {
        [Description("��ʾ����")]
        PrimaryAccount = 1,
        [Description("���������ӻ�")]
        ManageSubAccounts = 2,
        [Description("TPM�����ӻ�")]
        TPMVirtualSubAccount = 3
    }
}
