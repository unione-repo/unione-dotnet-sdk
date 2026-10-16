using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Obsolete.Model;

/// <summary>
/// ObsoleteUnsubscribedSetResponse
/// </summary>
public class ObsoleteUnsubscribedSetResponse
{
    /// <summary>
    /// "success" string<br/>
    /// Allowed values: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// The unsubscribed email.
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; set; }

    /// <summary>
    /// Unsubscription process details:
    /// <para/>
    /// - "unsubscribed" if email has just been unsubscribed,<br/>
    /// - "was inactive" if it was inactive due some reason,<br/>
    /// - "was unsubscribed" if it already was unsubscribed before the call,<br/>
    /// - "added and unsubscribed" if it's a new email without status history and it was added and then unsubscribed immediately.
    /// </summary>
    [JsonPropertyName("message")]
    public ObsoleteUnsubscribedSetMessage? Message { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
