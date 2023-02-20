
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminal;
using CRB.TPM.Mod.MainData.Core.Application.MTerminal.Dto;
using System.Collections.Generic;
using System;
using CRB.TPM.Mod.MainData.Core.Application.MTerminal.Vo;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Defaults;

namespace CRB.TPM.Mod.MainData.Core.Domain.MTerminal
{
    /// <summary>
    /// 终端信息仓储
    /// </summary>
    public interface IMTerminalRepository : IRepository<MTerminalEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MTerminalQueryVo>> QueryPage(MTerminalQueryDto dto);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IList<MTerminalQueryVo>> Query(MTerminalQueryDto dto);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<int> Delete(IEnumerable<Guid> ids);
        /// <summary>
        /// 是否可以更新终端编码
        /// </summary>
        /// <returns>true=可以更新，false=不能更新</returns>
        Task<(bool res, string msg)> IsChangeMTerminalCode(string code);
        /// <summary>
        /// 终端Select下拉接口
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MTerminalSelectVo>> Select(MTerminalSelectDto dto);

        ///// <summary>
        ///// 根据组织权限生成Sql条件字符串
        ///// </summary>
        //BuildSqlWhereStrByOrgAuthFunc BuildSqlWhereStrAuthFunc { get; set; }
    }
}


