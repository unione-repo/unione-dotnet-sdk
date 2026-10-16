using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.EventDump.Model;

/// <summary>
/// EventDumpListResponse
/// </summary>
public class EventDumpListResponse
{
    /// <summary>
    /// "success" string<br/>
    /// Allowed values: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// An array of objects describing event dumps. Each object is similar to 'event_dump' object returned<br/>
    /// by the [event-dump/get](https://docs.unione.io/en/web-api-ref#event-dump-get) method.
    /// </summary>
    [JsonPropertyName("event_dumps")]
    public List<EventDumpObject>? EventDumps { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
