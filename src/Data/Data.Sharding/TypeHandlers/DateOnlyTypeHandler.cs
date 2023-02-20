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
    internal class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
    {
        public override DateOnly Parse(object value)
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

        public override void SetValue(IDbDataParameter parameter, DateOnly value)
        {
            if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.Date)
            {
                parameter.DbType = DbType.Date;
                parameter.Value = value.ToDateTime(TimeOnly.MinValue);
            }
            else if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.DateTime)
            {
                parameter.DbType = DbType.DateTime;
                parameter.Value = value.ToDateTime(TimeOnly.MinValue);

            }
            else if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.Number)
            {
                parameter.DbType = DbType.Int32;
                parameter.Value = value.DayNumber;
            }
            else
            {
                parameter.DbType = DbType.String;
                parameter.Value = value.ToString("yyyy-MM-dd");
            }

        }
    }
}
#endif