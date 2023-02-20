using System;

namespace CRB.TPM.Data.Abstractions.Annotations
{
    /// <summary>
    ///  在实体事件中忽略指定属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreOnEntityEvent : Attribute
    {
    }
}
