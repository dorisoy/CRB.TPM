using CRB.TPM.TaskScheduler.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.TaskScheduler.Core;

/// <summary>
/// 表示任务日志实现
/// </summary>
public class TaskLogger : ITaskLogger
{
    private readonly ILogger _logger;

    public TaskLogger(ILogger<TaskLogger> logger)
    {
        _logger = logger;
    }

    public Guid JobId { get; set; }

    public Task Info(string msg)
    {
        _logger.LogInformation($"任务编号:{JobId}, {msg}");
        return Task.CompletedTask;
    }

    public Task Debug(string msg)
    {
        _logger.LogDebug($"任务编号:{JobId}, {msg}");
        return Task.CompletedTask;
    }

    public Task Error(string msg)
    {
        _logger.LogError($"任务编号:{JobId}, {msg}");
        return Task.CompletedTask;
    }
}
