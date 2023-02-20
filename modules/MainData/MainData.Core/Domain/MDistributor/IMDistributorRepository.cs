
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributor;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Dto;
using System.Collections.Generic;
using System;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Vo;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Defaults;

namespace CRB.TPM.Mod.MainData.Core.Domain.MDistributor
{
    /// <summary>
    /// 经销商/分销商仓储
    /// </summary>
    public interface IMDistributorRepository : IRepository<MDistributorEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IList<MDistributorQueryVo>> Query(MDistributorQueryDto dto);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MDistributorQueryVo>> QueryPage(MDistributorQueryDto dto);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<int> Delete(IEnumerable<Guid> ids);
        /// <summary>
        /// 根据id获取经销商和分销商
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        Task<(MDistributorEntity mdb1, MDistributorEntity mdb2)> GetByDistributorId(Guid id1, Guid id2);
        /// <summary>
        /// Select下拉接口
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<DistributorSelectVo>> Select(DistributorSelectDto dto);
        /// <summary>
        /// 是否可以变更过客户编码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<(bool res, string msg)> IsChangeDistributorCode(string code);

        ///// <summary>
        ///// 根据组织权限生成Sql条件字符串
        ///// </summary>
        //public BuildSqlWhereStrByOrgAuthFunc BuildSqlWhereStrAuthFunc { get; set; }
    }
}


