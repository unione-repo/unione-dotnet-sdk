using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Webhook.Model;

/// <summary>
/// Webhook callback format description.
/// </summary>
public class WebhookCallback
{
    /// <summary>
    /// MD5-hash of the message body, in which the value "auth" is replaced by api key of the user/project;<br/>
    /// with this field the recipient of the notification can both authenticate and verify the notification<br/>
    /// integrity
    /// </summary>
    [JsonPropertyName("auth")]
    public string? Auth { get; set; }

    /// <summary>
    /// Array with only one element, containing events of a user/project.
    /// </summary>
    [JsonPropertyName("events_by_user")]
    public List<WebhookCallbackEventsByUserItem>? EventsByUser { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
