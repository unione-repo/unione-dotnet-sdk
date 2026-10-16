using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Domain.Model;

/// <summary>
/// Object describing DKIM record value and status.
/// </summary>
public class DomainListResponseDomainsItemDkim
{
    /// <summary>
    /// DKIM signature for the domain. This property contains only the key part, you should prepend it with<br/>
    /// "k=rsa, p=" part for the record to be valid (see<br/>
    /// [domain/get-dns-records](https://docs.unione.io/en/web-api-ref#domain-get-dns-records)).
    /// </summary>
    [JsonPropertyName("key")]
    public string? Key { get; set; }

    /// <summary>
    /// Only domains with "active" DKIM record are allowed as sender domains.
    /// </summary>
    [JsonPropertyName("status")]
    public DkimStatus? Status { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
