using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.SyncAdvertiser.Dto
{
    public class Result_Advertiser
    {
        public List<ET_ADVERTIER> ET_ADVERTIER { get; set; }
        public List<ET_ADVERT_ADDR> ET_ADVERT_ADDR { get; set; }
        public List<ET_ADVERT_ASSIGN> ET_ADVERT_ASSIGN { get; set; }
        public List<ET_ADVERT_BANK> ET_ADVERT_BANK { get; set; }
        public List<ET_ORG> ET_ORG { get; set; }

    }
}
