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
using CRB.TPM.Mod.MainData.Core.Application.MAdvertiserOrg.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MAdvertiserOrg;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserOrg;

namespace CRB.TPM.Mod.MainData.Core.Application.MAdvertiserOrg
{
    /// <summary>
    /// 广告商营销组织关系表 M_Re_Org_AD服务
    /// </summary>
    public interface IMAdvertiserOrgService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
       Task<PagingQueryResultModel<MAdvertiserOrgEntity>> Query(MAdvertiserOrgQueryDto dto);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel> Add(MAdvertiserOrgAddDto dto);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        Task<IResultModel> Delete(Guid id);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Edit(Guid id);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel> Update(MAdvertiserOrgUpdateDto dto);

    }
}
