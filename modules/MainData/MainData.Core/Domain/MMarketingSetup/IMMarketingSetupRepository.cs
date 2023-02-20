
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MMarketingSetup;
using CRB.TPM.Mod.MainData.Core.Application.MMarketingSetup.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MMarketingSetup.Vo;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Dto;
using System.Collections.Generic;
using System;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Defaults;

namespace CRB.TPM.Mod.MainData.Core.Domain.MMarketingSetup
{
    /// <summary>
    /// 营销中心配置仓储
    /// </summary>
    public interface IMMarketingSetupRepository : IRepository<MMarketingSetupEntity>
    {
        ///// <summary>
        ///// 根据组织权限生成Sql条件字符串
        ///// </summary>
        //public BuildSqlWhereStrByOrgAuthFunc BuildSqlWhereStrAuthFunc { get; set; }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MMarketingSetupQueryVo>> QueryPage(MMarketingSetupQueryDto dto);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IList<MDistributorQueryVo>> Query(MMarketingSetupExportDto dto);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<int> Delete(IEnumerable<Guid> ids);
    }
}


