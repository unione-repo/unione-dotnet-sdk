using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Common.Model;

/// <summary>
/// The [template engine](https://docs.unione.io/en/template-engines) for handling the<br/>
/// substitutions(merge tags), "simple", "velocity", "liquid" or "none". Default value is "simple".
/// </summary>
[JsonConverter(typeof(StringEnumConverter<TemplateEngine>))]
public enum TemplateEngine
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("simple")]
    Simple,

    [EnumValue("velocity")]
    Velocity,

    [EnumValue("liquid")]
    Liquid,

    [EnumValue("none")]
    None,
}
