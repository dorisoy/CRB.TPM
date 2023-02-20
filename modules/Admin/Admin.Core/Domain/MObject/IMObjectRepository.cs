using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Vo;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Domain.MObject
{
    /// <summary>
    /// 对象表，营销中心、大区、业务部、工作站、客户 的主键是 数据本身的主键仓储
    /// </summary>
    public interface IMObjectRepository : IRepository<MObjectEntity>
    {
        /// <summary>
        /// 根据各层级id获取指定层级数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<OrgSelectVo>> QueryByLevel(OrgSelectDto dto);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<int> Deletes(IEnumerable<Guid> ids);
        /// <summary>
        /// 校验指定层级组织ID是否有权限
        /// </summary>
        /// <param name="orgIds"></param>
        /// <param name="orgType"></param>
        /// <param name="whereAuthSqlStr"></param>
        /// <returns></returns>
        Task<(bool isAuth, IList<string> noAuthCode)> CheckOrgIdsAuth(IList<Guid> orgIds, OrgEnumType orgType, string whereAuthSqlStr);
    }
}


