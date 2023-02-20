using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CRB.TPM.Utils.Json.Converters;

/// <summary>
/// ValueTuple自定义转换器 (由于System.Text.Json不支持ValueTuple
/// https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/migrate-from-newtonsoft?pivots=dotnet-7-0)
/// </summary>
public class ValueTupleFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        Type iTuple = typeToConvert.GetInterface("System.Runtime.CompilerServices.ITuple");
        return iTuple != null;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        Type[] genericArguments = typeToConvert.GetGenericArguments();

        Type converterType = genericArguments.Length switch
        {
            1 => typeof(ValueTupleConverter<>).MakeGenericType(genericArguments),
            2 => typeof(ValueTupleConverter<,>).MakeGenericType(genericArguments),
            3 => typeof(ValueTupleConverter<,,>).MakeGenericType(genericArguments),
            // And add other cases as needed
            _ => throw new NotSupportedException(),
        };
        return (JsonConverter)Activator.CreateInstance(converterType);
    }

    public class ValueTupleConverter<T1> : JsonConverter<ValueTuple<T1>>
    {
        public override ValueTuple<T1> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            ValueTuple<T1> result = default;

            if (!reader.Read())
            {
                throw new JsonException();
            }

            while (reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.ValueTextEquals("Item1") && reader.Read())
                {
                    result.Item1 = JsonSerializer.Deserialize<T1>(ref reader, options);
                }
                else
                {
                    throw new JsonException();
                }
                reader.Read();
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, ValueTuple<T1> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("item1");
            JsonSerializer.Serialize<T1>(writer, value.Item1, options);
            writer.WriteEndObject();
        }
    }

    public class ValueTupleConverter<T1, T2> : JsonConverter<ValueTuple<T1, T2>>
    {
        public override (T1, T2) Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            (T1, T2) result = default;

            if (!reader.Read())
            {
                throw new JsonException();
            }

            while (reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.ValueTextEquals("Item1") && reader.Read())
                {
                    result.Item1 = JsonSerializer.Deserialize<T1>(ref reader, options);
                }
                else if (reader.ValueTextEquals("Item2") && reader.Read())
                {
                    result.Item2 = JsonSerializer.Deserialize<T2>(ref reader, options);
                }
                else
                {
                    throw new JsonException();
                }
                reader.Read();
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, (T1, T2) value, JsonSerializerOptions options)
        {

            writer.WriteStartObject();
            writer.WritePropertyName("item1");
            JsonSerializer.Serialize<T1>(writer, value.Item1, options);
            writer.WritePropertyName("item2");
            JsonSerializer.Serialize<T2>(writer, value.Item2, options);
            writer.WriteEndObject();
        }
    }

    public class ValueTupleConverter<T1, T2, T3> : JsonConverter<ValueTuple<T1, T2, T3>>
    {
        public override (T1, T2, T3) Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            (T1, T2, T3) result = default;

            if (!reader.Read())
            {
                throw new JsonException();
            }

            while (reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.ValueTextEquals("item1") && reader.Read())
                {
                    result.Item1 = JsonSerializer.Deserialize<T1>(ref reader, options);
                }
                else if (reader.ValueTextEquals("item2") && reader.Read())
                {
                    result.Item2 = JsonSerializer.Deserialize<T2>(ref reader, options);
                }
                else if (reader.ValueTextEquals("item3") && reader.Read())
                {
                    result.Item3 = JsonSerializer.Deserialize<T3>(ref reader, options);
                }
                else
                {
                    throw new JsonException();
                }
                reader.Read();
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, (T1, T2, T3) value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("item1");
            JsonSerializer.Serialize<T1>(writer, value.Item1, options);
            writer.WritePropertyName("item2");
            JsonSerializer.Serialize<T2>(writer, value.Item2, options);
            writer.WritePropertyName("item3");
            JsonSerializer.Serialize<T3>(writer, value.Item3, options);
            writer.WriteEndObject();
        }
    }

    public class ValueTupleConverter<T1, T2, T3, T4> : JsonConverter<ValueTuple<T1, T2, T3, T4>>
    {
        public override (T1, T2, T3, T4) Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            (T1, T2, T3, T4) result = default;

            if (!reader.Read())
            {
                throw new JsonException();
            }

            while (reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.ValueTextEquals("item1") && reader.Read())
                {
                    result.Item1 = JsonSerializer.Deserialize<T1>(ref reader, options);
                }
                else if (reader.ValueTextEquals("item2") && reader.Read())
                {
                    result.Item2 = JsonSerializer.Deserialize<T2>(ref reader, options);
                }
                else if (reader.ValueTextEquals("item3") && reader.Read())
                {
                    result.Item3 = JsonSerializer.Deserialize<T3>(ref reader, options);
                }
                else if (reader.ValueTextEquals("item4") && reader.Read())
                {
                    result.Item4 = JsonSerializer.Deserialize<T4>(ref reader, options);
                }
                else
                {
                    throw new JsonException();
                }
                reader.Read();
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, (T1, T2, T3, T4) value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("item1");
            JsonSerializer.Serialize<T1>(writer, value.Item1, options);
            writer.WritePropertyName("item2");
            JsonSerializer.Serialize<T2>(writer, value.Item2, options);
            writer.WritePropertyName("item3");
            JsonSerializer.Serialize<T3>(writer, value.Item3, options);
            writer.WritePropertyName("item4");
            JsonSerializer.Serialize<T4>(writer, value.Item4, options);
            writer.WriteEndObject();
        }
    }

}
