using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.SyncAdvertiser.Dto
{
    /// <summary>
    /// 广告商地点分配 M_AdvertiserAccount_Address
    /// </summary>
    public class ET_ADVERT_ASSIGN
    {
        public string VENDOR_CODE { get; set; }
        public string ADDRNO { get; set; }
        public string ACOUNTNO { get; set; }
        public string ASSIGNSTAU { get; set; }
        public string UUID { get; set; }
        public string STDATE { get; set; }
        public string ENDATE { get; set; }
        public string CODE { get; set; }
        public string MAINCD { get; set; }
        public string BUCODE { get; set; }
        public string ZDATE { get; set; }
        public string ZTIME { get; set; }
    }
}
