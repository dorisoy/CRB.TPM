using CRB.TPM.Mod.MainData.Core.Application.MAdvertiserAccountAddress.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.SyncAdvertiser.Dto
{
    public class MAdvertiserAccountAddressAddExtendDto: MAdvertiserAccountAddressAddDto
    {
        public string AdvertiserCode { get; set; }
        public string AdvertiserAccount { get; set; }
    }
}
