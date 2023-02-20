using System;
using System.Data;

namespace CRB.TPM.Data.Abstractions.Annotations;

/// <summary>
/// 属性长度
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class LengthAttribute : Attribute
{
    /// <summary>
    /// 长度
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// 属性长度，0标识最大长度
    /// </summary>
    /// <param name="length"></param>
    public LengthAttribute(int length = 50)
    {
        Length = length;
    }
}


[AttributeUsage(AttributeTargets.Property)]
public class LongStringAttribute : Attribute
{
}


public class LongString
{

    public static void AddParameter(IDbCommand command, string name, string value)
    {
        var param = command.CreateParameter();
        param.ParameterName = name;
        param.Value = (object)value ?? DBNull.Value;
        param.DbType = DbType.String;

        int length = -1;
        if (!string.IsNullOrEmpty(value))
            length = value.Length;
        if (length == -1 && value != null && value.Length <= 4000)
        {
            param.Size = 4000;
        }
        else
        {
            param.Size = length;
        }

        if (value != null)
        {
            if (length > 4000 && param.GetType().Name == "SqlCeParameter")
            {
                param.GetType().GetProperty("SqlDbType").SetValue(param, SqlDbType.NText, null);
                param.Size = length;
            }
        }

        command.Parameters.Add(param);
    }

}