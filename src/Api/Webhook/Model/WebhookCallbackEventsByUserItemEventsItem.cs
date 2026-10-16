using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Webhook.Model;

/// <summary>
/// WebhookCallbackEventsByUserItemEventsItem
/// </summary>
public class WebhookCallbackEventsByUserItemEventsItem
{
    /// <summary>
    /// Type of event data contained in event_data field, either "transactional_email_status" or<br/>
    /// "transactional_spam_block".
    /// </summary>
    [JsonPropertyName("event_name")]
    public EventName? EventName { get; set; }

    /// <summary>
    /// Object with different event properties depending on "event_name". Below you can see all the<br/>
    /// properties, "transactional_email_status"-related first and then "transactional_spam_block"-related.
    /// </summary>
    [JsonPropertyName("event_data")]
    public WebhookCallbackEventsByUserItemEventsItemEventData? EventData { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
