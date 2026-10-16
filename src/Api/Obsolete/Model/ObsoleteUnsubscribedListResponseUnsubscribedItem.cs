using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Obsolete.Model;

/// <summary>
/// ObsoleteUnsubscribedListResponseUnsubscribedItem
/// </summary>
public class ObsoleteUnsubscribedListResponseUnsubscribedItem
{
    /// <summary>
    /// Unsubscribed email.
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; set; }

    /// <summary>
    /// Date and time when a recipient unsubscribed, in UTC timezone and "YYYY-MM-DD hh:mm:ss" format.
    /// </summary>
    [JsonPropertyName("unsubscribed_on")]
    public string? UnsubscribedOn { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
