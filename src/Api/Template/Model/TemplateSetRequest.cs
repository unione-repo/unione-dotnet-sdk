using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Template.Model;

/// <summary>
/// Object with all the properties of the template.
/// </summary>
public class TemplateSetRequest
{
    /// <summary>
    /// Object with all the template properties.
    /// </summary>
    [JsonPropertyName("template")]
    public TemplateObject? Template { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
