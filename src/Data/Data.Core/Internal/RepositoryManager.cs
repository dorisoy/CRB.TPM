using System.Collections.Generic;
using CRB.TPM.Data.Abstractions;

namespace CRB.TPM.Data.Core.Internal;

/// <summary>
/// 仓储管理器，用于管理当前请求中(Scoped模式注入)的仓储实例
/// </summary>
internal class RepositoryManager : IRepositoryManager
{
    public List<IRepository> Repositories { get; }

    public RepositoryManager()
    {
        Repositories = new List<IRepository>();
    }

    public void Add(IRepository repository)
    {
        Repositories.Add(repository);
    }
}