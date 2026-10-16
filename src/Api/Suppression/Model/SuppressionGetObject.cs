using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Suppression.Model;

/// <summary>
/// Object with suppression details.
/// </summary>
public class SuppressionGetObject
{
    /// <summary>
    /// Unqiue project identifier, ASCII string up to 36 characters long.
    /// </summary>
    [JsonPropertyName("project_id")]
    public string? ProjectId { get; set; }

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
    /// Is it possible to delete this suppression by calling suppression/delete method.
    /// </summary>
    [JsonPropertyName("is_deletable")]
    public bool? IsDeletable { get; set; }

    /// <summary>
    /// When suppression was created, in UTC timezone in "YYYY-MM-DD hh:mm:ss" format.
    /// </summary>
    [JsonPropertyName("created")]
    public string? Created { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
