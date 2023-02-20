using System;

namespace CRB.TPM.Utils.Annotations;

/// <summary>
/// 忽略
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
public class IgnoreAttribute : Attribute
{
}