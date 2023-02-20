#if CORE
using System;
using System.Data;
using System.Text.Json;
using Z.BulkOperations;

namespace CRB.TPM.Data.Sharding
{
    internal class SystemTextJsonTypeHandlerZ : IBulkValueConverter
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
                return JsonSerializer.Deserialize(val, destinationType, TextJsonOptions.Options);
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
            return JsonSerializer.Serialize(value, TextJsonOptions.Options);
        }
    }
}
#endif