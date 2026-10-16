using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Suppression.Model;

/// <summary>
/// SuppressionListResponse
/// </summary>
public class SuppressionListResponse
{
    /// <summary>
    /// "success" string<br/>
    /// Allowed values: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Array of suppression objects.
    /// </summary>
    [JsonPropertyName("suppressions")]
    public List<SuppressionListObject>? Suppressions { get; set; }

    /// <summary>
    /// The parameter indicates from which position the selection is to be started. Must be empty or omitted<br/>
    /// for the first data chunk. In order to get subsequent chunks, you must set the "cursor" parameter in<br/>
    /// your request, using the value received in response to the previous request.
    /// </summary>
    [JsonPropertyName("cursor")]
    public string? Cursor { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
