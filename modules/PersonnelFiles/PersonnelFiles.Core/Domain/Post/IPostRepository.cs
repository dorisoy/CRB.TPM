using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.PS.Core.Application.Post.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using System;

namespace CRB.TPM.Mod.PS.Core.Domain.Post;

/// <summary>
/// 岗位仓储
/// </summary>
public interface IPostRepository : IRepository<PostEntity>
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<PagingQueryResultModel<PostEntity>> Query(PostQueryDto dto, IList<AccountEntity> accounts);

    /// <summary>
    /// 是否存在关联职位
    /// </summary>
    /// <param name="positionId"></param>
    /// <returns></returns>
    Task<bool> ExistsPosition(Guid positionId);

    /// <summary>
    /// 名称是否存在
    /// </summary>
    /// <param name="name"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> ExistsName(string name, Guid? id = null);
}
