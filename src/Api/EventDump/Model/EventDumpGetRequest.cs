using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.EventDump.Model;

/// <summary>
/// EventDumpGetRequest
/// </summary>
public class EventDumpGetRequest
{
    /// <summary>
    /// Dump identifier received by the method<br/>
    /// [event-dump/create](https://docs.unione.io/en/web-api-ref#event-dump-create).
    /// </summary>
    [JsonPropertyName("dump_id")]
    public string? DumpId { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
