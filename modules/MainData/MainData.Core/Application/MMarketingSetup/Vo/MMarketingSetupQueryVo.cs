using CRB.TPM.Data.Abstractions.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.MMarketingSetup.Vo
{
    public class MMarketingSetupQueryVo
    {
        private string createdTime;
        private string modifierTime;

        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 营销中心编码
        /// </summary>
        public string OrgCode { get; set; }
        /// <summary>
        /// 营销中心名称
        /// </summary>
        public string OrgName { get; set; }
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
        /// <summary>
        /// 创建人名称
        /// </summary>
        public virtual string Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreatedTime { get => createdTime.ToDateTime().Format(); set => createdTime = value; }
        /// <summary>
        /// 修改人名称
        /// </summary>
        public string Modifier { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string ModifierTime { get => modifierTime.ToDateTime().Format(); set => modifierTime = value; }
    }
}
