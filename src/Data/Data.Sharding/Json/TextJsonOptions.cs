#if CORE
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace CRB.TPM.Data.Sharding
{
    public class TextJsonOptions
    {
#if CORE6
        static TextJsonOptions()
        {
            Options.Converters.Add(new TextJsonDateOnlyConverter());
            Options.Converters.Add(new TextJsonTimeOnlyConverter());
        }
#endif

        public static readonly JsonSerializerOptions Options = new JsonSerializerOptions()
        {
            //Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), //不编码中文
            //PropertyNamingPolicy = null, //序列化属性不转小写
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,//不编码中文和html
            NumberHandling = JsonNumberHandling.AllowReadingFromString, //反序列化时允许string转数字
            PropertyNameCaseInsensitive = true //反序列化忽略大小写
        };
    }
}
#endif