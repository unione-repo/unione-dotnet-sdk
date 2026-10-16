using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;
using UniOne.Common.Model;

namespace UniOne.Api.Webhook.Model;

/// <summary>
/// Object containing events to notify of.
/// </summary>
public class WebhookEventsObject
{
    /// <summary>
    /// If present then spam block events will be reported. Should contain single array element with "*"<br/>
    /// string.
    /// </summary>
    [JsonPropertyName("spam_block")]
    public List<string>? SpamBlock { get; set; }

    /// <summary>
    /// If present then email status change events will be reported. Contains names of statuses to notify<br/>
    /// of.
    /// </summary>
    [JsonPropertyName("email_status")]
    public List<EmailStatus>? EmailStatus { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
