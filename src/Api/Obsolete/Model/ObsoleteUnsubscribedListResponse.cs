using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Obsolete.Model;

/// <summary>
/// ObsoleteUnsubscribedListResponse
/// </summary>
public class ObsoleteUnsubscribedListResponse
{
    /// <summary>
    /// "success" string<br/>
    /// Allowed values: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Array of object describing unsubscribed emails.
    /// </summary>
    [JsonPropertyName("unsubscribed")]
    public List<ObsoleteUnsubscribedListResponseUnsubscribedItem>? Unsubscribed { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
