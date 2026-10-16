using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Suppression.Model;

/// <summary>
/// SuppressionGetResponse
/// </summary>
public class SuppressionGetResponse
{
    /// <summary>
    /// "success" string<br/>
    /// Allowed values: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// The email for which suppression details were requested.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Array of suppression objects.
    /// </summary>
    [JsonPropertyName("suppressions")]
    public List<SuppressionGetObject>? Suppressions { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
