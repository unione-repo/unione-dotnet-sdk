using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Obsolete.Model;

/// <summary>
/// Unsubscription process details:
/// <para/>
/// - "unsubscribed" if email has just been unsubscribed,<br/>
/// - "was inactive" if it was inactive due some reason,<br/>
/// - "was unsubscribed" if it already was unsubscribed before the call,<br/>
/// - "added and unsubscribed" if it's a new email without status history and it was added and then unsubscribed immediately.
/// </summary>
[JsonConverter(typeof(StringEnumConverter<ObsoleteUnsubscribedSetMessage>))]
public enum ObsoleteUnsubscribedSetMessage
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("unsubscribed")]
    Unsubscribed,

    [EnumValue("was inactive")]
    WasInactive,

    [EnumValue("was unsubscribed")]
    WasUnsubscribed,

    [EnumValue("added and unsubscribed")]
    AddedAndUnsubscribed,
}
