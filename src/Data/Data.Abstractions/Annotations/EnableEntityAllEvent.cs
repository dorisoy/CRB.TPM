using System;

namespace CRB.TPM.Data.Abstractions.Annotations
{
    /// <summary>
    /// 启用实体所有事件
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EnableEntityAllEvent : Attribute
    {
    }
}
