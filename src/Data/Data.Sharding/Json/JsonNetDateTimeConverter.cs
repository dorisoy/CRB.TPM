using Newtonsoft.Json.Converters;

namespace CRB.TPM.Data.Sharding
{
    public class JsonNetDateTimeConverter : IsoDateTimeConverter
    {
        public JsonNetDateTimeConverter()
        {
            // 默认日期时间格式
            DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        }

        public JsonNetDateTimeConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}


//[JsonConverter(typeof(JsonNetDateTimeConverter))]
//public DateTime PayTime { get; set; }
//
//或者
//[JsonConverter(typeof(JsonNetDateTimeConverter),"yyyy-MM-dd")]
//public DateTime PayTime { get; set; }



//IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
//timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
//JsonConvert.SerializeObject(stu, timeFormat);


//JsonSerializerSettings setting = new JsonSerializerSettings();
//JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
//{
//    //日期类型默认格式化处理
//    setting.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
//    setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";

//    //空值处理
//    setting.NullValueHandling = NullValueHandling.Ignore;

//    return setting;
//});