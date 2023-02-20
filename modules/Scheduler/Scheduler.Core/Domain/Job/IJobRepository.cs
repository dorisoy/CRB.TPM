using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Scheduler.Core.Application.Job.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRB.TPM.Mod.Admin.Core.Domain.Account;

namespace CRB.TPM.Mod.Scheduler.Core.Domain.Job;

/// <summary>
/// 任务仓储
/// </summary>
public interface IJobRepository : IRepository<JobEntity>
{
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="accounts">指定任务创建人账户</param>
    /// <returns></returns>
    Task<PagingQueryResultModel<JobEntity>> Query(JobQueryDto dto, IList<AccountEntity> accounts = null);

    /// <summary>
    /// 是否存在
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> Exists(JobEntity entity);

    /// <summary>
    /// 根据任务组判断是否存在
    /// </summary>
    /// <param name="group"></param>
    /// <returns></returns>
    Task<bool> ExistsByGroup(string group);

    /// <summary>
    /// 修改任务状态
    /// </summary>
    /// <param name="group">分组</param>
    /// <param name="code">编码</param>
    /// <param name="status">状态</param>
    /// <returns></returns>
    Task<bool> UpdateStatus(string group, string code, JobStatus status);

    /// <summary>
    /// 修改任务状态
    /// </summary>
    /// <param name="id">任务编号</param>
    /// <param name="status">状态</param>
    /// <param name="uow"></param>
    /// <returns></returns>
    Task<bool> UpdateStatus(Guid id, JobStatus status, IUnitOfWork uow = null);

    /// <summary>
    /// 任务是否停止
    /// </summary>
    /// <param name="group"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<bool> HasStop(string group, string code);

}
