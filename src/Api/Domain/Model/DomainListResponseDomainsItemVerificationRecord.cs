using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;
using UniOne.Common.Model;

namespace UniOne.Api.Domain.Model;

/// <summary>
/// Object describing verification record value and status.
/// </summary>
public class DomainListResponseDomainsItemVerificationRecord
{
    /// <summary>
    /// Record to be added "as is" to verify ownership of this domain.
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }

    /// <summary>
    /// Only domains with "confirmed" verification record are allowed as sender domains.
    /// </summary>
    [JsonPropertyName("status")]
    public VerificationRecordStatus? Status { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
