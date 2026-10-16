using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Domain.Model;

/// <summary>
/// DomainListResponseDomainsItem
/// </summary>
public class DomainListResponseDomainsItem
{
    /// <summary>
    /// Domain name.
    /// </summary>
    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    /// <summary>
    /// Object describing verification record value and status.
    /// </summary>
    [JsonPropertyName("verification-record")]
    public DomainListResponseDomainsItemVerificationRecord? VerificationRecord { get; set; }

    /// <summary>
    /// Object describing DKIM record value and status.
    /// </summary>
    [JsonPropertyName("dkim")]
    public DomainListResponseDomainsItemDkim? Dkim { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
