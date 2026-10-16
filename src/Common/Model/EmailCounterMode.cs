using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Common.Model;

/// <summary>
/// Sets the email counter mode. It allows you to choose whether the counter should reset automatically,<br/>
/// or remain permanent. The following values are acceptable: 'default' - the counter resets to zero<br/>
/// automatically at the beginning of each accounting period, 'permanent' - the counter never resets<br/>
/// automatically. The reset can be done only manually by the user in the request with the passes 0<br/>
/// value in the email_counter field. If the request doesn't contain the field or contains a null value,<br/>
/// the limit value doesn't change.
/// </summary>
[JsonConverter(typeof(StringEnumConverter<EmailCounterMode>))]
public enum EmailCounterMode
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("default")]
    Default,

    [EnumValue("permanent")]
    Permanent,
}
