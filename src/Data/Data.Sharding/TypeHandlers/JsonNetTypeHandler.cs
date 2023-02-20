using Newtonsoft.Json;
using System;
using System.Data;
using Dapper;

namespace CRB.TPM.Data.Sharding
{
    internal class JsonNetTypeHandler : SqlMapper.ITypeHandler
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
                return JsonConvert.DeserializeObject(val, destinationType);
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
                parameter.Value = JsonConvert.SerializeObject(value);

            }
        }
    }
}
