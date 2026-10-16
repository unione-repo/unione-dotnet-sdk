using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Webhook.Model;

/// <summary>
/// WebhookSetRequest
/// </summary>
public class WebhookSetRequest
{
    /// <summary>
    /// URL that will receive the notification when an event occurs. The URL should be unique for a user or<br/>
    /// a project. Only ASCII characters are supported for now in URL, please convert to<br/>
    /// [Punycode](https://en.wikipedia.org/wiki/Punycode) if you need to use non-ASCII characters.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Webhook status, "active" by default. "disabled" means that webhook has been disabled by the user,<br/>
    /// "stopped" means that webhook has been stopped by the system after 24h of failed calls (with minimum<br/>
    /// of 10 distinct events).<br/>
    /// Default: `active`.
    /// </summary>
    [JsonPropertyName("status")]
    public WebhookStatus? Status { get; set; }

    /// <summary>
    /// Notification format. "json_post"(default) or "json_post_gzip".<br/>
    /// Default: `json_post`.
    /// </summary>
    [JsonPropertyName("event_format")]
    public WebhookEventFormat? EventFormat { get; set; }

    /// <summary>
    /// Shall detailed delivery info be returned(1) or not(0). When it's 1, the information about SMTP<br/>
    /// response and internal delivery status is returned for "hard_bounced" and "soft_bounced" email<br/>
    /// statuses; the user agent, recipient IP address, detected geo fields, device info detected from the<br/>
    /// user agent (device type and brand, OS name, browser or email client name) and traffic_type (human,<br/>
    /// machine, protected or unknown) are returned for "opened" and "clicked" statuses; URL is returned for<br/>
    /// "clicked" status. For "delivered", "soft_bounced" and "hard_bounced" statuses UniOne may also return<br/>
    /// sender_ip, the IP address of the SMTP server that sent the message, if available.<br/>
    /// Allowed values: `0`, `1`.<br/>
    /// Default: `0`.
    /// </summary>
    [JsonPropertyName("delivery_info")]
    public int? DeliveryInfo { get; set; }

    /// <summary>
    /// 0=several event notifications can be reported in single webhook call, 1=only single event can be<br/>
    /// reported in one call (not recommended).<br/>
    /// Allowed values: `0`, `1`.<br/>
    /// Default: `0`.
    /// </summary>
    [JsonPropertyName("single_event")]
    public int? SingleEvent { get; set; }

    /// <summary>
    /// Maximum quantity of permitted parallel queries to your server. The more your server can handle - the<br/>
    /// better.<br/>
    /// Default: `10`.
    /// </summary>
    [JsonPropertyName("max_parallel")]
    public int? MaxParallel { get; set; }

    /// <summary>
    /// Object containing events to notify of.
    /// </summary>
    [JsonPropertyName("events")]
    public WebhookEventsObject? Events { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
