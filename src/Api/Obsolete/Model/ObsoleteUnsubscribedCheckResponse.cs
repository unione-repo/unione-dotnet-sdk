using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Obsolete.Model;

/// <summary>
/// ObsoleteUnsubscribedCheckResponse
/// </summary>
public class ObsoleteUnsubscribedCheckResponse
{
    /// <summary>
    /// "success" string<br/>
    /// Allowed values: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Email that was checked.
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; set; }

    /// <summary>
    /// true if unsubscribed, false if not.
    /// </summary>
    [JsonPropertyName("is_unsubscribed")]
    public bool? IsUnsubscribed { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
