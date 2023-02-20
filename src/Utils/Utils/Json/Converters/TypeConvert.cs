using System;
using System.Globalization;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace CRB.TPM.Utils.Json.Converters;


/// <summary>
/// 添加System.Type类型支持
/// </summary>
public class CustomJsonConverterForType : JsonConverter<Type>
{
    public override Type Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotSupportedException();
    }

    public override void Write(Utf8JsonWriter writer, Type value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.AssemblyQualifiedName);
    }
}
