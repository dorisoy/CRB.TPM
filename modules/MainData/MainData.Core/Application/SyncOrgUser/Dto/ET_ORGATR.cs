using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.SyncOrgUser.Dto
{
    /// <summary>
    /// 组织属性信息
    /// </summary>
    public class ET_ORGATR
    {
        public string ZORG { get; set; }
        public string ZATTRI { get; set; }
        public string ZATTR_VAL { get; set; }
        public string ZSTATU { get; set; }
        public string ZUPDATE { get; set; }
        public string ZUPDTIM { get; set; }
        /// <summary>
        /// 组织属性
        /// </summary>
        public int ORG_ATTR
        {
            get
            {
                string attr = this.ZATTRI;

                int lvl = 0;
                switch (attr)
                {
                    case "ZSORG":
                        lvl = (int)OrgEnumAttr.ZSORG;
                        break;
                    case "ZDIVISION":
                        lvl = (int)OrgEnumAttr.ZDIVISION;
                        break;
                    default:
                        lvl = (int)OrgEnumAttr.ZSORG;
                        break;
                }
                return lvl;
            }
        }
    }
}
