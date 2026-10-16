using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.System.Model;

/// <summary>
/// SystemInfoResponse
/// </summary>
public class SystemInfoResponse
{
    /// <summary>
    /// "success" string<br/>
    /// Allowed values: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Unique user identifier.
    /// </summary>
    [JsonPropertyName("user_id")]
    public int? UserId { get; set; }

    /// <summary>
    /// Email of the user.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Unqiue project identifier, ASCII string up to 36 characters long. Present only if the API key used<br/>
    /// for request is the project API key.
    /// </summary>
    [JsonPropertyName("project_id")]
    public string? ProjectId { get; set; }

    /// <summary>
    /// Project name, unique for user account. Present only if the API key used for request is the project<br/>
    /// API key.
    /// </summary>
    [JsonPropertyName("project_name")]
    public string? ProjectName { get; set; }

    /// <summary>
    /// Object with email counter properties. Present only if the API key used for request is the user API<br/>
    /// key.
    /// </summary>
    [JsonPropertyName("project_accounting")]
    public ProjectAccountingObject? ProjectAccounting { get; set; }

    /// <summary>
    /// Object with accounting period properties. Present only if the API key used for request is the user<br/>
    /// API key.
    /// </summary>
    [JsonPropertyName("accounting")]
    public SystemInfoResponseAccounting? Accounting { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
