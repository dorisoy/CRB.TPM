#if CORE6
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.BulkOperations;

namespace CRB.TPM.Data.Sharding
{
    internal class DateOnlyTypeHandlerZ : IBulkValueConverter
    {
        public object ConvertFromProvider(Type destinationType, object value)
        {
            var t = value.GetType();
            if (t == typeof(DateTime))
            {
                return DateOnly.FromDateTime((DateTime)value);
            }
            else if (t == typeof(int))
            {
                return DateOnly.FromDayNumber((int)value);
            }
            else if (t == typeof(decimal))
            {
                return DateOnly.FromDayNumber(Convert.ToInt32(value));
            }
            else
            {
                string val = (string)value;
                if (string.IsNullOrEmpty(val))
                {
                    return default;
                }
                DateOnly.TryParse(val, out var d);
                return d;
            }
        }

        public object ConvertToProvider(object v)
        {
            var value = (DateOnly)v;
            if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.Date)
            {
                return value.ToDateTime(TimeOnly.MinValue);
            }
            else if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.DateTime)
            {
                return value.ToDateTime(TimeOnly.MinValue);

            }
            else if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.Number)
            {
                return value.DayNumber;
            }
            else
            {
                return value.ToString("yyyy-MM-dd");
            }
        }
    }
}
#endif