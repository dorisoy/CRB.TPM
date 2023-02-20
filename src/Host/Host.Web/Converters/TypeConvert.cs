using CRB.TPM.Config.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Globalization;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CRB.TPM.Host.Web;

/// <summary>
/// 添加IConfig类型支持
/// </summary>
public class CustomJsonConverterForIConfig : System.Text.Json.Serialization.JsonConverter<IConfig>
{
    public override IConfig Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, IConfig value, JsonSerializerOptions options)
    {
        var settings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        settings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = "yyyy/MM/dd HH:mm:ss" });

        writer.WriteStringValue(JsonConvert.SerializeObject(value, settings));
    }
}
