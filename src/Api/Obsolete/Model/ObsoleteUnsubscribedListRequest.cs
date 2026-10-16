using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Obsolete.Model;

/// <summary>
/// ObsoleteUnsubscribedListRequest
/// </summary>
public class ObsoleteUnsubscribedListRequest
{
    /// <summary>
    /// Date in the format YYYY-MM-DD to get all unsubscribed emails from the "date_from" to the present<br/>
    /// day. If the parameter is not specified, the default period is today (from 00:00 UTC).
    /// </summary>
    [JsonPropertyName("date_from")]
    public string? DateFrom { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
