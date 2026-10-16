using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Common.Model;

/// <summary>
/// EmailStatus
/// </summary>
[JsonConverter(typeof(StringEnumConverter<EmailStatus>))]
public enum EmailStatus
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("sent")]
    Sent,

    [EnumValue("delivered")]
    Delivered,

    [EnumValue("opened")]
    Opened,

    [EnumValue("clicked")]
    Clicked,

    [EnumValue("unsubscribed")]
    Unsubscribed,

    [EnumValue("subscribed")]
    Subscribed,

    [EnumValue("soft_bounced")]
    SoftBounced,

    [EnumValue("hard_bounced")]
    HardBounced,

    [EnumValue("spam")]
    Spam,
}
