using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Webhook.Model;

/// <summary>
/// WebhookCallbackEventsByUserItem
/// </summary>
public class WebhookCallbackEventsByUserItem
{
    /// <summary>
    /// Unique user identifier.
    /// </summary>
    [JsonPropertyName("user_id")]
    public int? UserId { get; set; }

    /// <summary>
    /// Project identifier, present only if webhook was registered for the project using project API key.
    /// </summary>
    [JsonPropertyName("project_id")]
    public string? ProjectId { get; set; }

    /// <summary>
    /// Project name, present only if webhook was registered for the project using project API key.
    /// </summary>
    [JsonPropertyName("project_name")]
    public string? ProjectName { get; set; }

    /// <summary>
    /// Array of events reported by webhook.
    /// </summary>
    [JsonPropertyName("events")]
    public List<WebhookCallbackEventsByUserItemEventsItem>? Events { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
