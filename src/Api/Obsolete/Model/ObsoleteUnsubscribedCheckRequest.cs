using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Obsolete.Model;

/// <summary>
/// ObsoleteUnsubscribedCheckRequest
/// </summary>
public class ObsoleteUnsubscribedCheckRequest
{
    /// <summary>
    /// Email to check.
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
