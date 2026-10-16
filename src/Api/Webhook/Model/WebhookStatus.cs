using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Webhook.Model;

/// <summary>
/// Webhook status, "active" by default. "disabled" means that webhook has been disabled by the user,<br/>
/// "stopped" means that webhook has been stopped by the system after 24h of failed calls (with minimum<br/>
/// of 10 distinct events).
/// </summary>
[JsonConverter(typeof(StringEnumConverter<WebhookStatus>))]
public enum WebhookStatus
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("active")]
    Active,

    [EnumValue("disabled")]
    Disabled,

    [EnumValue("stopped")]
    Stopped,
}
