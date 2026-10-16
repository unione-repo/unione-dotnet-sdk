using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Email.Model;

/// <summary>
/// EmailSendResponse
/// </summary>
public class EmailSendResponse
{
    /// <summary>
    /// "success" string<br/>
    /// Allowed values: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Job identifier, might be useful for investigating errors.
    /// </summary>
    [JsonPropertyName("job_id")]
    public string? JobId { get; set; }

    /// <summary>
    /// Array of recipients emails successfully accepted for sending.
    /// </summary>
    [JsonPropertyName("emails")]
    public List<string>? Emails { get; set; }

    /// <summary>
    /// Object with emails rejected for sending as property names and their statuses as property values,<br/>
    /// e.g.: {"email1@gmail.com": "temporary_unavailable"}. Possible statuses:
    /// <para/>
    /// - __unsubscribed__ - specified email is unsubscribed;<br/>
    /// - __invalid__ - the email does not exist or has been entered incorrectly;<br/>
    /// - __duplicate__ - the email already exists in the request, email duplicating is prevented;<br/>
    /// - __temporary_unavailable__ - the email address is unavailable. This means that over the next three days sending to this address will return an error. Email may be temporarily unavailable due to several reasons, e.g.:
    /// <para/>
    /// - a previous email has been rejected by the recipient's server for spam;<br/>
    /// - the recipient's mailbox is full or is not used;<br/>
    /// - recipient's domain does not accept mail;<br/>
    /// - sending server was rejected due to blacklisting;
    /// <para/>
    /// - __permanent_unavailable__ - the email address is permanently unavailable or unsubscribed globally.<br/>
    /// - __complained__ -  the recipient reported spam in the previous emails;<br/>
    /// - __blocked__ -  sending to the email is prohibited by administration of UniOne.<br/>
    /// We may added new statuses in the future.
    /// </summary>
    [JsonPropertyName("failed_emails")]
    public Dictionary<string, string>? FailedEmails { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
