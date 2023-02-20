using System;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Json.Converters;
using Microsoft.Extensions.Options;

namespace CRB.TPM.Utils.Json;

/// <summary>
/// JSON序列化帮助类
/// <para>该帮助类使用一组默认的JsonSerializerOptions配置来进行序列化/反序列化，配置如下：</para>
/// <para>1、不区分大小写的反序列化</para>
/// <para>2、属性名称使用 camel 大小写</para>
/// <para>3、最大限度减少字符转义</para>
/// <para>4、自定义日期转换器 DateTimeConverter</para>
/// </summary>
[SingletonInject]
public class JsonHelper
{
    private readonly JsonSerializerOptions _options = new();

    /// <summary>
    /// 静态实例
    /// </summary>
    public static readonly JsonHelper Instance = new();

    public JsonHelper()
    {
        //不区分大小写的反序列化
        _options.PropertyNameCaseInsensitive = true;

        //忽略空字段
        _options.DefaultIgnoreCondition =  JsonIgnoreCondition.WhenWritingNull;

        //属性名称使用 camel 大小写
        _options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        //最大限度减少字符转义
        _options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

        //添加日期转换器
        _options.Converters.Add(new DateTimeConverter());

        //添加GUID转换器
        _options.Converters.Add(new GuidConverter());

        //添加元组（ValueTuples）支持
        _options.Converters.Add(new ValueTupleFactory());

        //添加System.Type类型支持
        _options.Converters.Add(new CustomJsonConverterForType());

        //添加多态嵌套序列化
        _options.AddPolymorphism();

    }

    /// <summary>
    /// 序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public string Serialize<T>(T obj)
    {
        return JsonSerializer.Serialize(obj, typeof(T), _options);
    }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json"></param>
    /// <returns></returns>
    public T Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, _options);
    }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="json">json文本</param>
    /// <param name="type">类型</param>
    /// <returns></returns>
    public object Deserialize(string json, Type type)
    {
        return JsonSerializer.Deserialize(json, type, _options);
    }
}

///// <summary>
///// https://dotnetfiddle.net/MXnhJx
///// </summary>
//public class NullableConverterFactory : JsonConverterFactory
//{
//    static readonly byte[] Empty = Array.Empty<byte>();

//    public override bool CanConvert(Type typeToConvert) => Nullable.GetUnderlyingType(typeToConvert) != null;

//    public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options) =>
//        (JsonConverter)Activator.CreateInstance(
//            typeof(NullableConverter<>).MakeGenericType(
//                new Type[] { Nullable.GetUnderlyingType(type) }),
//            BindingFlags.Instance | BindingFlags.Public,
//            binder: null,
//            args: new object[] { options },
//            culture: null);

//    class NullableConverter<T> : JsonConverter<T?> where T : struct
//    {
//        // DO NOT CACHE the return of (JsonConverter<T>)options.GetConverter(typeof(T)) as DoubleConverter.Read() and DoubleConverter.Write()
//        // DO NOT WORK for nondefault values of JsonSerializerOptions.NumberHandling which was introduced in .NET 5
//        public NullableConverter(JsonSerializerOptions options) { }

//        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//        {
//            if (reader.TokenType == JsonTokenType.String)
//            {
//                if (reader.ValueTextEquals(Empty))
//                    return null;
//            }
//            return JsonSerializer.Deserialize<T>(ref reader, options);
//        }

//        public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options) =>
//            JsonSerializer.Serialize(writer, value.Value, options);
//    }
//}