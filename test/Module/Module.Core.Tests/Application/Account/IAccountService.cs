using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Mod.Module.Core.Application.Account.Dto;
using CRB.TPM.Mod.Module.Core.Domain.Account;

namespace CRB.TPM.Mod.Module.Core.Application.Account;

public interface IAccountService
{
    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<IList<AccountEntity>> Query();

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<IResultModel> Add(AccountAddDto dto);

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IResultModel> Edit(Guid id);

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<IResultModel> Update(AccountUpdateDto dto);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IResultModel> Delete(Guid id);

    /// <summary>
    /// 导出
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<IResultModel<ExcelModel>> Export(AccountQueryDto dto);
}