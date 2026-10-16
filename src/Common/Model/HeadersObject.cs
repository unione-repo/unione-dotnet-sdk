using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Common.Model;

/// <summary>
/// Contains email headers, maximum 50. Only headers with “X-” name prefix are accepted, all other are<br/>
/// ignored, for example X-UNIONE-Global-Language, X-UNIONE-Template-Engine. Standard headers "To,"<br/>
/// "CC," and "BCC" are passed without the "X-." Yet, they are processed in a particular way and, as a<br/>
/// result, have a number of restrictions. You can find more details about it [here](/cc-and-bcc). If<br/>
/// our support have approved omitting standard unsubscription block for you, you can also pass<br/>
/// List-Unsubscribe, List-Subscribe, List-Help, List-Owner, List-Archive, In-Reply-To and References<br/>
/// headers.
/// <para/>
/// Any other header goes to Extra: ["X-MyHeader"] = "value", ["CC"], ["List-Unsubscribe"], ...; a key<br/>
/// of a named property is sent from the property, not from Extra. Unknown keys of a response are read<br/>
/// into Extra (as JsonElement values).
/// </summary>
public class HeadersObject
{
    /// <summary>
    /// The language of the unsubscribe footer and unsubscribe page. Languages supported are: "be", "de",<br/>
    /// "en", "es", "fr", "it", "pl", "pt", "ru", "ua", "kz".
    /// </summary>
    [JsonPropertyName("X-UNIONE-Global-Language")]
    public string? XUNIONEGlobalLanguage { get; set; }

    /// <summary>
    /// The template engine for handling the substitutions(merge tags), “simple”, “velocity”, “liquid” or<br/>
    /// “none”. Default value is “simple”. Has a priority over the value allowed in the “template_engine”<br/>
    /// parameter.
    /// </summary>
    [JsonPropertyName("X-UNIONE-Template-Engine")]
    public string? XUNIONETemplateEngine { get; set; }

    private static readonly string[] NamedKeys = { "X-UNIONE-Global-Language", "X-UNIONE-Template-Engine" };

    /// <summary>
    /// Keys beyond the named properties, sent merged into the object: string values ("X-MyHeader" = "value").
    /// Unknown keys of a response are read into it as JsonElement values (ToString() gives a string value).
    /// </summary>
    [JsonIgnore]
    public Dictionary<string, object>? Extra { get; set; }

    /// <summary>Extra as the serializer sees it: without the keys of the named properties.</summary>
    [JsonExtensionData]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Dictionary<string, object>? ExtensionData
    {
        get => Extra == null || !Extra.Keys.Any(key => Array.IndexOf(NamedKeys, key) >= 0)
            ? Extra
            : Extra.Where(e => Array.IndexOf(NamedKeys, e.Key) < 0).ToDictionary(e => e.Key, e => e.Value);
        set => Extra = value;
    }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
