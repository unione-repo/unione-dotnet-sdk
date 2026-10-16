using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Webhook.Model;

/// <summary>
/// Type of event data contained in event_data field, either "transactional_email_status" or<br/>
/// "transactional_spam_block".
/// </summary>
[JsonConverter(typeof(StringEnumConverter<EventName>))]
public enum EventName
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("transactional_email_status")]
    TransactionalEmailStatus,

    [EnumValue("transactional_spam_block")]
    TransactionalSpamBlock,
}
