using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Template.Model;

/// <summary>
/// Editor type, "html" or "visual". Default is "html". You can create a template with "visual" editor<br/>
/// type only in web interface.
/// </summary>
[JsonConverter(typeof(StringEnumConverter<EditorType>))]
public enum EditorType
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("html")]
    Html,

    [EnumValue("visual")]
    Visual,
}
