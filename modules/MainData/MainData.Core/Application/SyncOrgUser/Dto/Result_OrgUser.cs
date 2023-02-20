using CRB.TPM.Mod.Admin.Core.Application.SyncAccount.Dto;
using CRB.TPM.Mod.MainData.Core.Application.SyncSetting.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.SyncOrgUser.Dto
{
    public class Result_OrgUser
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public List<ET_ORG> ET_ORG { get; set; }
        /// <summary>
        /// 用户岗位信息
        /// </summary>
        public List<ET_BPREL> ET_BPREL { get; set; }
        /// <summary>
        /// 组织属性信息
        /// </summary>
        public List<ET_ORGATR> ET_ORGATR { get; set; }
        /// <summary>
        /// 组织数据
        /// </summary>
        public List<ET_ORGREL> ET_ORGREL { get; set; }
    }
}
