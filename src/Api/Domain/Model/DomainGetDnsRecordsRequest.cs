using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Domain.Model;

/// <summary>
/// DomainGetDnsRecordsRequest
/// </summary>
public class DomainGetDnsRecordsRequest
{
    /// <summary>
    /// Domain to get DNS records for.
    /// </summary>
    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
