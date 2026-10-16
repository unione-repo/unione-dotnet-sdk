using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Suppression.Model;

/// <summary>
/// SuppressionDeleteRequest
/// </summary>
public class SuppressionDeleteRequest
{
    /// <summary>
    /// An email to delete from suppression list.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
