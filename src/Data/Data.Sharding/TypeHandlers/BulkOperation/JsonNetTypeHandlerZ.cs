using Newtonsoft.Json;
using System;
using Z.BulkOperations;

namespace CRB.TPM.Data.Sharding
{
    internal class JsonNetTypeHandlerZ : IBulkValueConverter
    {
        public object ConvertFromProvider(Type destinationType, object value)
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

        public object ConvertToProvider(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            return JsonConvert.SerializeObject(value);
        }
    }
}
