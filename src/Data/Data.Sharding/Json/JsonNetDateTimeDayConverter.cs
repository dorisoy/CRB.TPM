using Newtonsoft.Json.Converters;

namespace CRB.TPM.Data.Sharding
{
    public class JsonNetDateTimeDayConverter : IsoDateTimeConverter
    {
        public JsonNetDateTimeDayConverter()
        {
            // 默认日期时间格式
            DateTimeFormat = "yyyy-MM-dd";
        }

        public JsonNetDateTimeDayConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
