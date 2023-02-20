using System;
using CRB.TPM.Data.Abstractions;

namespace CRB.TPM.Data.Core.Internal;

/// <summary>
/// 默认账户解析器
/// </summary>
internal class DefaultOperatorResolver : IOperatorResolver
{
    public Guid? TenantId => null;

    public string TenantName => string.Empty;

    public Guid? AccountId => null;

    public string AccountName => string.Empty;
}