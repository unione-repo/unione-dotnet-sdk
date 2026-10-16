using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UniOne.Client;

/// <summary>
/// API value of a string enum member.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public sealed class EnumValueAttribute : Attribute
{
    public EnumValueAttribute(string value)
    {
        Value = value;
    }

    public string Value { get; }
}

/// <summary>
/// Marks ApiClient.RequestJsonOptions (the options of the request bodies); converts nothing.
/// </summary>
internal sealed class RequestOptionsMarker : JsonConverter<RequestOptionsMarker>
{
    public static bool IsRequest(JsonSerializerOptions options)
    {
        foreach (var converter in options.Converters)
        {
            if (converter is RequestOptionsMarker)
            {
                return true;
            }
        }

        return false;
    }

    public override RequestOptionsMarker Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotSupportedException();
    }

    public override void Write(Utf8JsonWriter writer, RequestOptionsMarker value, JsonSerializerOptions options)
    {
        throw new NotSupportedException();
    }
}

/// <summary>
/// (De)serializes string enums by their <see cref="EnumValueAttribute"/> values. Unknown values (added to the API
/// later, or not a string at all) are read as the member without the attribute (UnknownValue); UnknownValue is
/// written as null by ApiClient.JsonOptions (ToString() of a model) and can not be sent in a request (ApiException).
/// </summary>
public sealed class StringEnumConverter<T> : JsonConverter<T>
    where T : struct, Enum
{
    private static readonly Dictionary<string, T> FromValue = new();
    private static readonly Dictionary<T, string> ToValue = new();
    private static readonly T Unknown;

    static StringEnumConverter()
    {
        foreach (var field in typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var member = (T)field.GetValue(null)!;
            var attribute = field.GetCustomAttribute<EnumValueAttribute>();
            if (attribute == null)
            {
                Unknown = member;
                continue;
            }

            FromValue[attribute.Value] = member;
            ToValue[member] = attribute.Value;
        }
    }

    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            // An object or an array is skipped as a whole, a number or a bool is a single token.
            reader.Skip();
            return Unknown;
        }

        var value = reader.GetString();
        return value != null && FromValue.TryGetValue(value, out var member) ? member : Unknown;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        if (ToValue.TryGetValue(value, out var text))
        {
            writer.WriteStringValue(text);
        }
        else if (RequestOptionsMarker.IsRequest(options))
        {
            throw new JsonException($"{typeof(T).Name}.{value} is not a value of the API: it can not be sent");
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
