using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Webhook.Model;

/// <summary>
/// Notification format. "json_post"(default) or "json_post_gzip".
/// </summary>
[JsonConverter(typeof(StringEnumConverter<WebhookEventFormat>))]
public enum WebhookEventFormat
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("json_post")]
    JsonPost,

    [EnumValue("json_post_gzip")]
    JsonPostGzip,
}
