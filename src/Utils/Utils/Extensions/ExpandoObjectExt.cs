using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace CRB.TPM;

/// <summary>
/// ExpandoObject 扩展
/// </summary>
public static class ExpandoObjectExt
{
    public static void Set(this ExpandoObject obj, string propertyName, object value)
    {
        IDictionary<string, object> dic = obj;
        dic[propertyName] = value;
    }

    public static ExpandoObject ToExpando(this object initialObj)
    {
        ExpandoObject obj = new ExpandoObject();
        IDictionary<string, object> dic = obj;
        Type tipo = initialObj.GetType();
        foreach (var prop in tipo.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            dic.Add(prop.Name, prop.GetValue(initialObj));
        }
        return obj;
    }

    public static dynamic ToExpando2(this object value)
    {
        IDictionary<string, object> dapperRowProperties = value as IDictionary<string, object>;

        IDictionary<string, object> expando = new ExpandoObject();

        foreach (KeyValuePair<string, object> property in dapperRowProperties)
            expando.Add(property.Key, property.Value);

        return expando as ExpandoObject;
    }
}

