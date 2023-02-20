#if CORE6
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace CRB.TPM.Data.Sharding
{
    internal class TimeOnlyTypeHandler : SqlMapper.TypeHandler<TimeOnly>
    {
        public override TimeOnly Parse(object value)
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

        public override void SetValue(IDbDataParameter parameter, TimeOnly value)
        {
            if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.TimeSpan)
            {
                parameter.DbType = DbType.Time;
                parameter.Value = value.ToTimeSpan();
            }
            else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.Time)
            {
                parameter.DbType = DbType.Time;
                var date = new DateOnly(2000, 1, 1);
                parameter.Value = date.ToDateTime(value);
            }
            else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.DateTime)
            {
                parameter.DbType = DbType.DateTime;
                var date = new DateOnly(2000, 1, 1);
                parameter.Value = date.ToDateTime(value);
            }
            else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.Number)
            {
                parameter.DbType = DbType.Int64;
                parameter.Value = value.Ticks;
            }
            else
            {
                parameter.DbType = DbType.String;
                parameter.Value = value.ToString("HH:mm:ss");
            }
        }
    }
}
#endif