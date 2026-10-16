using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.EmailValidation.Model;

/// <summary>
/// EmailValidationSingleResponse
/// </summary>
public class EmailValidationSingleResponse
{
    /// <summary>
    /// "success" string<br/>
    /// Allowed values: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Email address to be checked.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Possible statuses:
    /// <para/>
    /// - "valid" - the address is valid,<br/>
    /// - "invalid" - the address is invalid,<br/>
    /// - "suspicious" - the address looks suspicious,<br/>
    /// - "unknown" - could not perform validation, the domain’s mail server has not responded within the time limit.
    /// </summary>
    [JsonPropertyName("result")]
    public string? Result { get; set; }

    /// <summary>
    /// Possible statuses:
    /// <para/>
    /// - "no_mx_record" - no MX record found for the target domain,<br/>
    /// - "syntax_error" - the address syntax is invalid,<br/>
    /// - "possible_typo" - the address is likely to have a typo,<br/>
    /// - "mailbox_not_found" - the address does not exist,<br/>
    /// - "global_suppression" - the address has been marked as unreachable due to multiple previous delivery errors,<br/>
    /// - "disposable" - this is a disposable one-time email address which is usually valid for a few minutes only,<br/>
    /// - "role" - the address is not likely to belong to an actual person, but rather to a certain business staff role,<br/>
    /// - "abuse" - the address is known to be a source of a large number of complaints, sometimes issued automatically,<br/>
    /// - "spamtrap" - this email is a spam trap, it is published openly but never used for actual emails. Sending messages to such addresses has a huge negative impact on reputation score,<br/>
    /// - "smtp_connection_failed" - the domain’s SMTP server does not respond; the address may contain a typo.
    /// </summary>
    [JsonPropertyName("cause")]
    public string? Cause { get; set; }

    /// <summary>
    /// Validity score, from 0 to 100 (0 – invalid, 100 – valid).
    /// </summary>
    [JsonPropertyName("validity")]
    public int? Validity { get; set; }

    /// <summary>
    /// Local part (everything preceding the @ sign).
    /// </summary>
    [JsonPropertyName("local_part")]
    public string? LocalPart { get; set; }

    /// <summary>
    /// Domain name part.
    /// </summary>
    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    /// <summary>
    /// True if the address’ domain does have an MX record, false if does not.
    /// </summary>
    [JsonPropertyName("mx_found")]
    public bool? MxFound { get; set; }

    /// <summary>
    /// Preferred MX record for the domain.
    /// </summary>
    [JsonPropertyName("mx_record")]
    public string? MxRecord { get; set; }

    /// <summary>
    /// For addresses which are likely to have typing errors (cause=possible_typo), a suggested variant with<br/>
    /// a fixed typo.
    /// </summary>
    [JsonPropertyName("did_you_mean")]
    public string? DidYouMean { get; set; }

    /// <summary>
    /// Email check date and time, YYYY-MM-DD hh:mm:ss UTC.
    /// </summary>
    [JsonPropertyName("processed_at")]
    public string? ProcessedAt { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
