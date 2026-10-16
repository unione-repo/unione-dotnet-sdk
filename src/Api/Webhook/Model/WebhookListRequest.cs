using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Webhook.Model;

/// <summary>
/// WebhookListRequest
/// </summary>
public class WebhookListRequest
{
    /// <summary>
    /// Limits the number of webhooks returned. 50 is recommended.
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    /// <summary>
    /// Index of the first webhook in the list, starting with 0.<br/>
    /// Default: `0`.
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
