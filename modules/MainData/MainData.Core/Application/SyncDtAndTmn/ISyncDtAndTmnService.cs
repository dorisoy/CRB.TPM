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
using CRB.TPM.Mod.MainData.Core.Application.SyncDtAndTmn.Dto;

namespace CRB.TPM.Mod.MainData.Core.Application.SyncDtAndTmn
{
    /// <summary>
    /// 同步经销商终端服务
    /// </summary>
    public interface ISyncDtAndTmnService
    {
        /// <summary>
        /// 同步CRM经销商、终端主数据
        /// </summary>
        /// <param name="dto">请求模型</param>
        /// <returns></returns>
        Task<IResultModel> SyncData(SyncDtAndTmnDto dto);
    }
}
