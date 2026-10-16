using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Project.Model;

/// <summary>
/// ProjectCreateResponse
/// </summary>
public class ProjectCreateResponse
{
    /// <summary>
    /// "success" string<br/>
    /// Allowed values: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Unqiue project identifier, ASCII string up to 36 characters long.
    /// </summary>
    [JsonPropertyName("project_id")]
    public string? ProjectId { get; set; }

    /// <summary>
    /// API key of the project. You can use it instead of the user API key parameter in all methods except<br/>
    /// project/* methods.
    /// </summary>
    [JsonPropertyName("project_api_key")]
    public string? ProjectApiKey { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
