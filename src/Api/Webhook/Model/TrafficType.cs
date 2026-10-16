using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Webhook.Model;

/// <summary>
/// Traffic quality for "opened" and "clicked" statuses. "human" means a recognized browser or email<br/>
/// client without machine signs; "machine" means a bot, link scanner or security proxy; "protected"<br/>
/// means a mailbox/OS privacy proxy where a human action cannot be confirmed (Apple Mail Privacy<br/>
/// Protection / iCloud Private Relay, Gmail Image Proxy — Apple MPP is protected, not machine);<br/>
/// "unknown" means the user agent is missing, broken or not recognized.
/// </summary>
[JsonConverter(typeof(StringEnumConverter<TrafficType>))]
public enum TrafficType
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("human")]
    Human,

    [EnumValue("machine")]
    Machine,

    [EnumValue("protected")]
    Protected,

    [EnumValue("unknown")]
    Unknown,
}
