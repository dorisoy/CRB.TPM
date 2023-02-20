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
    internal class TimeOnlyTypeHandlerZ : IBulkValueConverter
    {
        public object ConvertFromProvider(Type destinationType, object value)
        {
            var t = value.GetType();
            if (t == typeof(TimeSpan))
            {
                return TimeOnly.FromTimeSpan((TimeSpan)value);
            }
            else if (t == typeof(DateTime))
            {
                return TimeOnly.FromDateTime((DateTime)value);
            }
            else if (t == typeof(long))
            {
                return new TimeOnly((long)value);
            }
            else if (t == typeof(decimal))
            {
                return new TimeOnly(Convert.ToInt64(value));
            }
            else
            {
                var val = (string)value;
                if (string.IsNullOrEmpty(val))
                {
                    return default;
                }
                TimeOnly.TryParse(val, out var d);
                return d;
            }

        }

        public object ConvertToProvider(object v)
        {
            var value = (TimeOnly)v;
            if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.TimeSpan)
            {
                return value.ToTimeSpan();
            }
            else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.Time)
            {
                var date = new DateOnly(2000, 1, 1);
                return date.ToDateTime(value);
            }
            else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.DateTime)
            {
                var date = new DateOnly(2000, 1, 1);
                return date.ToDateTime(value);
            }
            else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.Number)
            {
                return value.Ticks;
            }
            else
            {
                return value.ToString("HH:mm:ss");
            }
        }
    }
}
#endif