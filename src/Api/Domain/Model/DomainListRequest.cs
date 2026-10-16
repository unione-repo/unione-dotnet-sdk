using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Domain.Model;

/// <summary>
/// DomainListRequest
/// </summary>
public class DomainListRequest
{
    /// <summary>
    /// Optional domain name if you want to get the status of single domain only.
    /// </summary>
    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    /// <summary>
    /// Limits the number of domains returned. Default is 50.<br/>
    /// Default: `50`.
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    /// <summary>
    /// Index of the first domain in the list, starting with 0.<br/>
    /// Default: `0`.
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
