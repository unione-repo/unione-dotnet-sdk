using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Suppression.Model;

/// <summary>
/// SuppressionSetRequest
/// </summary>
public class SuppressionSetRequest
{
    /// <summary>
    /// Email to add in suppression list.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

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
    /// - __complained__ -  the recipient reported spam in the previous emails;
    /// </summary>
    [JsonPropertyName("cause")]
    public string? Cause { get; set; }

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
