using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;
using UniOne.Common.Model;

namespace UniOne.Api.Webhook.Model;

/// <summary>
/// Object with different event properties depending on "event_name". Below you can see all the<br/>
/// properties, "transactional_email_status"-related first and then "transactional_spam_block"-related.
/// </summary>
public class WebhookCallbackEventsByUserItemEventsItemEventData
{
    /// <summary>
    /// Job identifier returned earlier by [email/send](https://docs.unione.io/en/web-api-ref#email-send)<br/>
    /// method. Property exists only if event_name="transactional_email_status".
    /// </summary>
    [JsonPropertyName("job_id")]
    public string? JobId { get; set; }

    /// <summary>
    /// Metadata passed in [email/send](https://docs.unione.io/en/web-api-ref#email-send) method in<br/>
    /// recipients.metadata or global_metadata properties. This property exists only if<br/>
    /// event_name="transactional_email_status".
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

    /// <summary>
    /// Recipient's email. This property exists only if event_name="transactional_email_status".
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Email delivery status. This property exists only if event_name="transactional_email_status".<br/>
    /// Possible values are:
    /// <para/>
    /// __accepted__ — the message has been accepted, but not sent yet.<br/>
    /// __sent__ — the message has been sent, but not delivered yet.<br/>
    /// __delivered__ — the message has been delivered. It can be changed to “opened”, “clicked”,<br/>
    /// “unsubscribed”, “subscribed” or “spam”.<br/>
    /// __opened__ — the message has been delivered and read. It can be changed to “clicked”,<br/>
    /// “unsubscribed”, “subscribed” or “spam”.<br/>
    /// __clicked__ — the message has been delivered, read, and at least one of the links in email has been<br/>
    /// clicked. It can be changed to “unsubscribed”, “subscribed” or “spam”.<br/>
    /// __unsubscribed__ — the message has been delivered to the recipient and read, but then the recipient<br/>
    /// unsubscribed. It can be changed to “subscribed”.<br/>
    /// __subscribed__ — the message has been delivered to the recipient and read, but then the recipient<br/>
    /// unsubscribed and subscribed again. It can be changed to “unsubscribed”.<br/>
    /// __soft_bounced__ — temporary delivery failure. UniOne will try to deliver the message during 48<br/>
    /// hours. In case of success the status changes to “delivered”, in case of failure to deliver during 48<br/>
    /// hours message status changes to “hard_bounced”.<br/>
    /// __hard_bounced__ — failed to deliver the message, there will be no delivery attempts. It's a final<br/>
    /// status. There are a lot of possible reasons, you can use SMTP response from<br/>
    /// delivery_info.destination_response field or UniOne internal reason classification from<br/>
    /// delivery_info.delivery_status field to analyze the reason.<br/>
    /// __spam__ — the message has been delivered, but it was reported as spam by the recipient. UniOne can<br/>
    /// receive and process the spam complaint from several domains, including msn.com, outlook.com,<br/>
    /// hotmail.com, live.com, ukr.net, yahoo.com, aol.com using<br/>
    /// [FBL](https://en.wikipedia.org/wiki/Feedback_loop_(email)).
    /// </summary>
    [JsonPropertyName("status")]
    public EmailStatus? Status { get; set; }

    /// <summary>
    /// Event date &amp; time in UTC time zone in "YYYY-MM-DD hh:mm:ss" format. This property exists only if<br/>
    /// event_name="transactional_email_status".
    /// </summary>
    [JsonPropertyName("event_time")]
    public string? EventTime { get; set; }

    /// <summary>
    /// URL for "opened" and "clicked" statuses. This property exists only if<br/>
    /// event_name="transactional_email_status".
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Object with detailed delivery info.Event date &amp; time in UTC time zone in "YYYY-MM-DD hh:mm:ss"<br/>
    /// format. This property exists only if webhook has delivery_info proeprty set to 1 and<br/>
    /// event_name="transactional_email_status".
    /// </summary>
    [JsonPropertyName("delivery_info")]
    public WebhookCallbackEventsByUserItemEventsItemEventDataDeliveryInfo? DeliveryInfo { get; set; }

    /// <summary>
    /// Spam block date &amp; time in UTC time zone in "YYYY-MM-DD hh:mm:ss" format. This property exists only<br/>
    /// if event_name="transactional_spam_block".
    /// </summary>
    [JsonPropertyName("block_time")]
    public string? BlockTime { get; set; }

    /// <summary>
    /// Spam block type, either single or multiple sending SMTP. For single sending SMTP block in common<br/>
    /// pool UniOne retries several other SMTPs. This property exists only if<br/>
    /// event_name="transactional_spam_block".
    /// </summary>
    [JsonPropertyName("block_type")]
    public BlockType? BlockType { get; set; }

    /// <summary>
    /// Domain that blocked sending. This property exists only if event_name="transactional_spam_block".
    /// </summary>
    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    /// <summary>
    /// Number of sending SMTPs blocked. This property exists only if event_name="transactional_spam_block".
    /// </summary>
    [JsonPropertyName("SMTP_blocks_count")]
    public int? SMTPBlocksCount { get; set; }

    /// <summary>
    /// Whether it's a block or unblock event. This property exists only if<br/>
    /// event_name="transactional_spam_block".
    /// </summary>
    [JsonPropertyName("domain_status")]
    public DomainStatus? DomainStatus { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
