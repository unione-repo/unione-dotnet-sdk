using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;
using UniOne.Common.Model;

namespace UniOne.Api.EventDump.Model;

/// <summary>
/// An object with the properties of the event dump filter.
/// </summary>
public class FilterObject
{
    /// <summary>
    /// Job identifier returned earlier by [email/send](https://docs.unione.io/en/web-api-ref#email-send)<br/>
    /// method.
    /// </summary>
    [JsonPropertyName("job_id")]
    public string? JobId { get; set; }

    /// <summary>
    /// Email status. Possible values are:
    /// <para/>
    /// __accepted__ — the message has been accepted, but not sent yet.<br/>
    /// __sent__ — the message has been sent, but not delivered yet.<br/>
    /// __delivered__ — the message has been delivered.<br/>
    /// __opened__ — the message has been delivered and read.<br/>
    /// __clicked__ — the message has been delivered, read, and at least one of the links in email has been<br/>
    /// clicked.<br/>
    /// __unsubscribed__ — the message has been delivered to the recipient and read, but then the recipient<br/>
    /// unsubscribed.<br/>
    /// __subscribed__ — the message has been delivered to the recipient and read, but then the recipient<br/>
    /// unsubscribed and subscribed again.<br/>
    /// __soft_bounced__ — temporary delivery failure. UniOne will try to deliver the message during 48<br/>
    /// hours.<br/>
    /// __hard_bounced__ — failed to deliver the message, there will be no delivery attempts.<br/>
    /// __spam__ — the message has been delivered, but it was reported as spam by the recipient.
    /// </summary>
    [JsonPropertyName("status")]
    public EmailStatus? Status { get; set; }

    /// <summary>
    /// UniOne internal detailed delivery status, you can pass several values at once, comma-separated. Some<br/>
    /// possible values are:
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
    /// Recipient email address
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Sender email address
    /// </summary>
    [JsonPropertyName("email_from")]
    public string? EmailFrom { get; set; }

    /// <summary>
    /// Recipient domain
    /// </summary>
    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    /// <summary>
    /// Campaign identifier, unsigned decimal integer or UUID up to 128-bit, passed in metadata with name<br/>
    /// "campaign_id" (name is configurable thru support).
    /// </summary>
    [JsonPropertyName("campaign_id")]
    public string? CampaignId { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
