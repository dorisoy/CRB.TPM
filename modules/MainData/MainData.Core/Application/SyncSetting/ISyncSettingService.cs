using System;
using System.Linq;
using System.Threading.Tasks;
//using AutoMapper;

using System.Collections.Generic;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;

using CRB.TPM.Utils.Json;
using CRB.TPM.Utils.Map;
using CRB.TPM.Mod.MainData.Core.Infrastructure;
using CRB.TPM.Mod.MainData.Core.Application.MAdvertiser.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MAdvertiser;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiser;

namespace CRB.TPM.Mod.MainData.Core.Application.SyncSetting
{
    /// <summary>
    /// 同步配置信息
    /// </summary>
    public interface ISyncSettingService
    {
        /// <summary>
        /// 同步配置信息
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<IResultModel> SyncData(string date);
    }
}
