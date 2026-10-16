using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Domain.Model;

/// <summary>
/// Only domains with "active" DKIM record are allowed as sender domains.
/// </summary>
[JsonConverter(typeof(StringEnumConverter<DkimStatus>))]
public enum DkimStatus
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("active")]
    Active,

    [EnumValue("inactive")]
    Inactive,
}
