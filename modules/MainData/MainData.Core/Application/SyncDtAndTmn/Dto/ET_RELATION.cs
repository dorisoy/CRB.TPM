using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRB.TPM.Mod.MainData.Core.Application.SyncDtAndTmn.Dto
{
    /// <summary>
    /// 关系表
    /// </summary>
    public class ET_RELATION
    {
        public string PARTNER1 { get; set; }
        public string PARTNER2 { get; set; }
        public string RELTYP { get; set; }
        public string ZUPDMODE { get; set; }
        public string ZDATE { get; set; }
    }
}
