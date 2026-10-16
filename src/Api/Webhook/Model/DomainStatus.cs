using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Webhook.Model;

/// <summary>
/// Whether it's a block or unblock event. This property exists only if<br/>
/// event_name="transactional_spam_block".
/// </summary>
[JsonConverter(typeof(StringEnumConverter<DomainStatus>))]
public enum DomainStatus
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("blocked")]
    Blocked,

    [EnumValue("unblocked")]
    Unblocked,
}
