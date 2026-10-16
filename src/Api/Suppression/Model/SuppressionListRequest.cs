using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Suppression.Model;

/// <summary>
/// SuppressionListRequest
/// </summary>
public class SuppressionListRequest
{
    /// <summary>
    /// Cause of email being suppressed. One of:
    /// <para/>
    /// - __unsubscribed__ - email is unsubscribed;<br/>
    /// - __temporary_unavailable__ - the email address is unavailable. This means that over the next three days sending to this address will return an error. Email may be temporarily unavailable due to several reasons, e.g.:
    /// <para/>
    /// - a previous email has been rejected by the recipient's server for spam;<br/>
    /// - the recipient's mailbox is full or is not used;<br/>
    /// - recipient's domain does not accept mail;<br/>
    /// - sending server was rejected due to blacklisting;
    /// <para/>
    /// - __permanent_unavailable__ - the email address is permanently unavailable due to multiple hard bounces;<br/>
    /// - __complained__ -  the recipient reported spam in the previous emails;<br/>
    /// - __blocked__ -  sending to the email is prohibited by administration of UniOne.<br/>
    /// We may add some new causes in the future.
    /// </summary>
    [JsonPropertyName("cause")]
    public string? Cause { get; set; }

    /// <summary>
    /// Source of email being suppressed. One of:
    /// <para/>
    /// - __user__ - suppressed by user with    [suppression/set](https://docs.unione.io/en/web-api-ref#suppression-set);<br/>
    /// - __system__ - sending to the email is prohibited by system, for example due to multiple hard bounces;<br/>
    /// - __subscriber__ - the recipient reported spam or unsubscribed in the previous emails.
    /// </summary>
    [JsonPropertyName("source")]
    public string? Source { get; set; }

    /// <summary>
    /// Date in the format YYYY-MM-DD hh:mm:ss to get suppression list from the "start_time" to the present<br/>
    /// day. Ignored if "cursor" is not empty.
    /// </summary>
    [JsonPropertyName("start_time")]
    public string? StartTime { get; set; }

    /// <summary>
    /// The parameter indicates from which position the selection is to be started. Must be empty or omitted<br/>
    /// for the first data chunk. In order to get subsequent chunks, you must set the "cursor" parameter in<br/>
    /// your request, using the value received in response to the previous request.
    /// </summary>
    [JsonPropertyName("cursor")]
    public string? Cursor { get; set; }

    /// <summary>
    /// Limits the number of records to be returned at one time, default is 50.
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
