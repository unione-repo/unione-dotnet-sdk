using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Webhook.Model;

/// <summary>
/// Object with detailed delivery info.Event date &amp; time in UTC time zone in "YYYY-MM-DD hh:mm:ss"<br/>
/// format. This property exists only if webhook has delivery_info proeprty set to 1 and<br/>
/// event_name="transactional_email_status".
/// </summary>
public class WebhookCallbackEventsByUserItemEventsItemEventDataDeliveryInfo
{
    /// <summary>
    /// UniOne internal detailed delivery status. Some possible values are:
    /// <para/>
    /// __err_user_unknown__ -- email address doesn't exist;<br/>
    /// __err_user_inactive__ -- email address isn't used anymore;<br/>
    /// __err_will_retry__ -- email was temporary rejected, delivery will be retried later;<br/>
    /// __err_mailbox_discarded__ -- email address was active earlier, but now it's deleted;<br/>
    /// __err_mailbox_full__ -- mailbox is full;<br/>
    /// __err_spam_rejected__ -- email was rejected as a spam;<br/>
    /// __err_blacklisted__ -- email was rejected because sender IP or sender domain is found in a black<br/>
    /// list;<br/>
    /// __err_too_large__ -- email size is over limit, according to recipient's server;<br/>
    /// __err_unsubscribed__ -- email was unsubscribed;<br/>
    /// __err_unreachable__ -- numerous delivery failures lead to mark this email address as permanently<br/>
    /// unavailable;<br/>
    /// __err_skip_letter__ -- sending has been canceled because the email address is temporarily<br/>
    /// unavailable;<br/>
    /// __err_domain_inactive__ -- the domain does not accept mail or does not exist;<br/>
    /// __err_destination_misconfigured__ -- the domain does not accept mail due to incorrect settings on<br/>
    /// the recipient’s side, and the server’s response contains information about a cause that can be fixed<br/>
    /// (for example, an inoperative blacklist is used, etc.);<br/>
    /// __err_delivery_failed__ -- delivery failed due to other reasons;<br/>
    /// __err_spam_skipped__ -- sending has been canceled because the campaign has been blocked as a spam;<br/>
    /// __err_lost__ -- the message was not sent due to inconsistency of its parts or it has been lost<br/>
    /// because of the failure on our side. The sender needs to re-send the letter on his own, since the<br/>
    /// original has not been saved;
    /// <para/>
    /// We reserve the right to change or add internal delivery statuses as we need, so the following list<br/>
    /// is provided as an example only, without any description: ok_sent, ok_delivered, ok_read,<br/>
    /// ok_link_visited, ok_unsubscribed, ok_resubscribed, ok_spam_folder, ok_fbl, not_sent,<br/>
    /// skip_dup_unreachable, skip_dup_temp_unreachable, skip_dup_mailbox_full, err_spam_removed,<br/>
    /// err_resend, err_unknown, err_retry_letter, err_src_invalid, err_dest_invalid, err_not_available,<br/>
    /// err_internal, err_no_dns, err_no_smtp, err_giveup.
    /// </summary>
    [JsonPropertyName("delivery_status")]
    public string? DeliveryStatus { get; set; }

    /// <summary>
    /// SMTP response.
    /// </summary>
    [JsonPropertyName("destination_response")]
    public string? DestinationResponse { get; set; }

    /// <summary>
    /// User agent of recipient. Present only if detected for "clicked" and "opened" statuses.
    /// </summary>
    [JsonPropertyName("user_agent")]
    public string? UserAgent { get; set; }

    /// <summary>
    /// Recipient's device type, detected from the user agent. Present for "clicked" and "opened" statuses;<br/>
    /// equals "unknown" if the device type couldn't be detected.
    /// </summary>
    [JsonPropertyName("device_type")]
    public DeviceType? DeviceType { get; set; }

    /// <summary>
    /// Recipient's device brand, detected from the user agent. Present for "clicked" and "opened" statuses<br/>
    /// if detected. For device_type "mobile" and "tablet" it's always present and equals "unknown" when the<br/>
    /// brand couldn't be detected.
    /// </summary>
    [JsonPropertyName("device_brand")]
    public string? DeviceBrand { get; set; }

    /// <summary>
    /// Name of the operating system of the recipient's device, detected from the user agent. Present for<br/>
    /// "clicked" and "opened" statuses; equals "unknown" if the OS couldn't be detected.
    /// </summary>
    [JsonPropertyName("os_name")]
    public string? OsName { get; set; }

    /// <summary>
    /// Name of the browser the email was opened in, detected from the user agent. Present for "clicked" and<br/>
    /// "opened" statuses only if a browser was detected, so it never comes together with email_client_name.
    /// </summary>
    [JsonPropertyName("browser_name")]
    public string? BrowserName { get; set; }

    /// <summary>
    /// Name of the email client application the email was opened in, detected from the user agent. Present<br/>
    /// for "clicked" and "opened" statuses only if an email client was detected, so it never comes together<br/>
    /// with browser_name.
    /// </summary>
    [JsonPropertyName("email_client_name")]
    public string? EmailClientName { get; set; }

    /// <summary>
    /// Traffic quality for "opened" and "clicked" statuses. "human" means a recognized browser or email<br/>
    /// client without machine signs; "machine" means a bot, link scanner or security proxy; "protected"<br/>
    /// means a mailbox/OS privacy proxy where a human action cannot be confirmed (Apple Mail Privacy<br/>
    /// Protection / iCloud Private Relay, Gmail Image Proxy — Apple MPP is protected, not machine);<br/>
    /// "unknown" means the user agent is missing, broken or not recognized.
    /// </summary>
    [JsonPropertyName("traffic_type")]
    public TrafficType? TrafficType { get; set; }

    /// <summary>
    /// Recipient's IP address. Present only if detected for "clicked" and "opened" statuses.
    /// </summary>
    [JsonPropertyName("ip")]
    public string? Ip { get; set; }

    /// <summary>
    /// ISO 3166-1 alpha-2 country code detected from recipient IP. Present for "clicked" and "opened"<br/>
    /// statuses.
    /// </summary>
    [JsonPropertyName("country_code")]
    public string? CountryCode { get; set; }

    /// <summary>
    /// Country detected from recipient IP. Present for "clicked" and "opened" statuses.
    /// </summary>
    [JsonPropertyName("country")]
    public string? Country { get; set; }

    /// <summary>
    /// City detected from recipient IP. Present for "clicked" and "opened" statuses.
    /// </summary>
    [JsonPropertyName("city")]
    public string? City { get; set; }

    /// <summary>
    /// IP address of the SMTP server that sent the message. Present only for "delivered", "soft_bounced"<br/>
    /// and "hard_bounced" statuses, if available.
    /// </summary>
    [JsonPropertyName("sender_ip")]
    public string? SenderIp { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
