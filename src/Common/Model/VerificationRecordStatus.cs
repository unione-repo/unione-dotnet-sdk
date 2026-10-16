using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Common.Model;

/// <summary>
/// Only domains with "confirmed" verification record are allowed as sender domains.
/// </summary>
[JsonConverter(typeof(StringEnumConverter<VerificationRecordStatus>))]
public enum VerificationRecordStatus
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("confirmed")]
    Confirmed,

    [EnumValue("unconfirmed")]
    Unconfirmed,
}
