using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.SyncAdvertiser
{
    public interface ISyncAdvertiserService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<IResultModel> SyncData(string date, string marketOrgCode);
    }
}
