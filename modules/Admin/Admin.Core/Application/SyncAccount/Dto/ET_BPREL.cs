using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Application.SyncAccount.Dto
{
    /// <summary>
    /// 用户岗位信息
    /// </summary>
    public class ET_BPREL
    {
        public string OBJID { get; set; }
        public string EMPLOYEE { get; set; }
        public string BEGDA { get; set; }
        public string ENDDA { get; set; }
        public string STATUS { get; set; }
        public string LEADBP { get; set; }
        public string AEDTM { get; set; }
        public string UNAME { get; set; }
    }
}
