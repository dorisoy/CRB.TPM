
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminalUser;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalUser.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalUser.Vo;
using System.Collections.Generic;
using System;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Defaults;

namespace CRB.TPM.Mod.MainData.Core.Domain.MTerminalUser
{
    /// <summary>
    /// 终端与经销商的关系信息仓储
    /// </summary>
    public interface IMTerminalUserRepository : IRepository<MTerminalUserEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MTerminalUserQueryVo>> QueryPage(MTerminalUserQueryDto dto);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IList<MTerminalUserQueryVo>> Query(MTerminalUserQueryDto dto);
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


