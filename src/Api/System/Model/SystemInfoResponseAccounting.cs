using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.System.Model;

/// <summary>
/// Object with accounting period properties. Present only if the API key used for request is the user<br/>
/// API key.
/// </summary>
public class SystemInfoResponseAccounting
{
    /// <summary>
    /// Date and time of accounting period start in UTC in "YYYY-MM-DD hh:mm:ss" format.
    /// </summary>
    [JsonPropertyName("period_start")]
    public string? PeriodStart { get; set; }

    /// <summary>
    /// Date and time of accounting period end in UTC in "YYYY-MM-DD hh:mm:ss" format.
    /// </summary>
    [JsonPropertyName("period_end")]
    public string? PeriodEnd { get; set; }

    /// <summary>
    /// Number of emails included into accounting period.
    /// </summary>
    [JsonPropertyName("emails_included")]
    public long? EmailsIncluded { get; set; }

    /// <summary>
    /// Number of emails sent during accounting period. Can be bigger than emails_included in case of<br/>
    /// overage.
    /// </summary>
    [JsonPropertyName("emails_sent")]
    public long? EmailsSent { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
