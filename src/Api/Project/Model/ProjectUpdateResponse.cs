using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Project.Model;

/// <summary>
/// ProjectUpdateResponse
/// </summary>
public class ProjectUpdateResponse
{
    /// <summary>
    /// "success" string<br/>
    /// Allowed values: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// API key of the project. Returned only if the project to update was identified by project_api_key.<br/>
    /// Not returned in case of updating by project_id.
    /// </summary>
    [JsonPropertyName("project_api_key")]
    public string? ProjectApiKey { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
