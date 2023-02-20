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
using CRB.TPM.Mod.MainData.Core.Application.MTerminalUser.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalUser;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminalUser;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalUser.Vo;
using CRB.TPM.Excel.Abstractions;

namespace CRB.TPM.Mod.MainData.Core.Application.MTerminalUser
{
    /// <summary>
    /// 终端与经销商的关系信息服务
    /// </summary>
    public interface IMTerminalUserService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MTerminalUserQueryVo>> Query(MTerminalUserQueryDto dto);

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel<ExcelModel>> Export(MTerminalUserQueryDto dto);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IResultModel> Add(MTerminalUserAddDto dto);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        Task<IResultModel> Delete(Guid id);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IResultModel> DeleteSelected(IEnumerable<Guid> ids);

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
        Task<IResultModel> Update(MTerminalUserUpdateDto dto);

    }
}
