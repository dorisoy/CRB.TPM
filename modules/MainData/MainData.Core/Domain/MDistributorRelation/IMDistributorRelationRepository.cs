
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributorRelation;
using CRB.TPM.Mod.MainData.Core.Application.MDistributorRelation.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MDistributorRelation.Vo;
using System.Collections.Generic;
using System;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Defaults;

namespace CRB.TPM.Mod.MainData.Core.Domain.MDistributorRelation
{
    /// <summary>
    /// 经销商分销商关系表仓储
    /// </summary>
    public interface IMDistributorRelationRepository : IRepository<MDistributorRelationEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MDistributorRelationQueryVo>> QueryPage(MDistributorRelationQueryDto dto);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IList<MDistributorRelationQueryVo>> Query(MDistributorRelationQueryDto dto);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<int> Delete(IEnumerable<Guid> ids);

        ///// <summary>
        ///// 根据组织权限生成Sql条件字符串
        ///// </summary>
        //public BuildSqlWhereStrByOrgAuthFunc BuildSqlWhereStrAuthFunc { get; set; }
    }
}


