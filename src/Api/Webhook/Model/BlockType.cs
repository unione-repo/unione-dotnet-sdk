using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Webhook.Model;

/// <summary>
/// Spam block type, either single or multiple sending SMTP. For single sending SMTP block in common<br/>
/// pool UniOne retries several other SMTPs. This property exists only if<br/>
/// event_name="transactional_spam_block".
/// </summary>
[JsonConverter(typeof(StringEnumConverter<BlockType>))]
public enum BlockType
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("one_smtp")]
    OneSmtp,

    [EnumValue("all_smtp")]
    AllSmtp,
}
