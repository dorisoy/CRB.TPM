#if CORE

using System;
using System.Data;
using System.Text.Json;
using Dapper;

namespace CRB.TPM.Data.Sharding
{
    internal class SystemTextJsonTypeHandler : SqlMapper.ITypeHandler
    {
        public object Parse(Type destinationType, object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return default;
            }
            try
            {
                var val = (string)value;
                if (val == "")
                {
                    return default;
                }
                return JsonSerializer.Deserialize(val, destinationType, TextJsonOptions.Options);
            }
            catch
            {
                return default;
            }
        }

        public void SetValue(IDbDataParameter parameter, object value)
        {
            parameter.DbType = DbType.String;
            if (value == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = JsonSerializer.Serialize(value, TextJsonOptions.Options);
            }
        }
    }
}
#endif