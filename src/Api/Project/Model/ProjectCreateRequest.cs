using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Project.Model;

/// <summary>
/// ProjectCreateRequest
/// </summary>
public class ProjectCreateRequest
{
    /// <summary>
    /// Object with project properties.
    /// </summary>
    [JsonPropertyName("project")]
    public ProjectObject? Project { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
