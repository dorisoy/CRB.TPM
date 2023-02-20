using System;
using System.Collections.Generic;
using CRB.TPM.Data.Abstractions.Sharding;

namespace CRB.TPM.Data.Abstractions.Annotations
{
    /// <summary>
    /// 表示分表特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ShardingAttribute : Attribute
    {
        /// <summary>
        /// 分表策略
        /// </summary>
        public ShardingPolicy Policy { get; set; }

        /// <summary>
        /// 自定义分表提供器类型
        /// </summary>
        public Type CustomProviderType { get; set; }

        /// <summary>
        /// 分表策略
        /// </summary>
        /// <param name="policy"></param>
        public ShardingAttribute(ShardingPolicy policy)
        {
            Policy = policy;
        }

        /// <summary>
        /// 自定义分表策略
        /// </summary>
        /// <param name="customProviderType"></param>
        public ShardingAttribute(Type customProviderType)
        {
            Policy = ShardingPolicy.Custom;
            CustomProviderType = customProviderType;
        }
    }

    /// <summary>
    /// 表示分表字段特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ShardingFieldAttribute : AbstractValidateAttribute
    {
        protected bool _enable;

        /// <summary>
        ///  是否为分表字段
        /// </summary>
        public bool Enable
        {
            get
            {
                return this._enable;
            }
        }

        /// <summary>
        /// 分表字段
        /// </summary>
        /// <param name="enable"></param>
        public ShardingFieldAttribute(bool enable = true)
        {
            _enable = enable;
        }

        /// <summary>
        /// 验证类型
        /// </summary>
        /// <param name="value">非空日期类型</param>
        /// <returns></returns>
        public override bool Validate(object value)
        {
            return value != null && value is DateTime;
        }
    }


    /// <summary>
    /// 用于Sharding分表字段抽象验证
    /// </summary>
    public abstract class AbstractValidateAttribute : Attribute
    {
        /// <summary>
        /// 验证类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract bool Validate(Object value);
    }

    public static class AttributeExtend
    {
        /// <summary>
        /// 扩展方法（用来验分表字段参数是否符合预期）
        /// </summary>
        /// <typeparam name="T">目标类</typeparam>
        /// <param name="t">目标类的实例化对象</param>
        /// <param name="values"></param>
        /// <returns>验证结果true或false</returns>
        public static bool Validate<T>(this T t, out List<object> values)
        {
            values = new ();
            //获取目标类的类型(实际就是传进来的实体类对象)
            Type type = t.GetType();  
            int ss = type.GetProperties().Length;
            //获取实体类对象的所有属性集合，循环取出
            foreach (var prop in type.GetProperties())
            {
                if (prop.IsDefined(typeof(ShardingFieldAttribute), true)) 
                {
                    //获取当前属性的值
                    object value = prop.GetValue(t);
                    values.Add(value);
                    foreach (ShardingFieldAttribute attr in prop.GetCustomAttributes(typeof(ShardingFieldAttribute), true))
                    {
                        //启用时间分表字段特性
                        if (attr.Validate(value) && attr.Enable)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
