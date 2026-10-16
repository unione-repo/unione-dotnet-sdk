using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Webhook.Model;

/// <summary>
/// Recipient's device type, detected from the user agent. Present for "clicked" and "opened" statuses;<br/>
/// equals "unknown" if the device type couldn't be detected.
/// </summary>
[JsonConverter(typeof(StringEnumConverter<DeviceType>))]
public enum DeviceType
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("desktop")]
    Desktop,

    [EnumValue("mobile")]
    Mobile,

    [EnumValue("tablet")]
    Tablet,

    [EnumValue("other")]
    Other,

    [EnumValue("unknown")]
    Unknown,
}
