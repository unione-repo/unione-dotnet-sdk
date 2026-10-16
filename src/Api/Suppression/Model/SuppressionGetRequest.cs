using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Suppression.Model;

/// <summary>
/// SuppressionGetRequest
/// </summary>
public class SuppressionGetRequest
{
    /// <summary>
    /// Email to get suppression details for.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// If a user has projects functionality enabled, he/she can pass all_projects=true to search in all<br/>
    /// projects' data.<br/>
    /// Default: `False`.
    /// </summary>
    [JsonPropertyName("all_projects")]
    public bool? AllProjects { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
