using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Domain.Model;

/// <summary>
/// DomainGetDnsRecordsResponse
/// </summary>
public class DomainGetDnsRecordsResponse
{
    /// <summary>
    /// "success" string<br/>
    /// Allowed values: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Domain to get DNS records for.
    /// </summary>
    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    /// <summary>
    /// Record to be added "as is" to verify ownership of this domain.
    /// </summary>
    [JsonPropertyName("verification-record")]
    public string? VerificationRecord { get; set; }

    /// <summary>
    /// DKIM signature for the domain. This property contains only the key part, you should prepend it with<br/>
    /// "v=DKIM1; k=rsa; p=" for the TXT record to be valid.
    /// </summary>
    [JsonPropertyName("dkim")]
    public string? Dkim { get; set; }

    /// <summary>
    /// Target of the CNAME record for the "_dmarc" host of the domain.
    /// </summary>
    [JsonPropertyName("dmarc")]
    public string? Dmarc { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
