using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Webhook.Model;

/// <summary>
/// WebhookSetResponse
/// </summary>
public class WebhookSetResponse
{
    /// <summary>
    /// "success" string<br/>
    /// Allowed values: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Object with all the webhook properties.
    /// </summary>
    [JsonPropertyName("object")]
    public WebhookObject? Object { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
