using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Project.Model;

/// <summary>
/// ProjectDeleteRequest
/// </summary>
public class ProjectDeleteRequest
{
    /// <summary>
    /// Unique project identifier. You should identify project to delete either with project_id or with<br/>
    /// project_api_key field. We recommend usage of project_id as a more secure option.
    /// </summary>
    [JsonPropertyName("project_id")]
    public string? ProjectId { get; set; }

    /// <summary>
    /// Optional project API key. You should identify project to delete either with project_id or with<br/>
    /// project_api_key field. We recommend usage of project_id as a more secure option.
    /// </summary>
    [JsonPropertyName("project_api_key")]
    public string? ProjectApiKey { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
