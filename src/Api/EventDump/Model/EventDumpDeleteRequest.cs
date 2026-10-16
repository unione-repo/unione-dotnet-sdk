using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.EventDump.Model;

/// <summary>
/// EventDumpDeleteRequest
/// </summary>
public class EventDumpDeleteRequest
{
    /// <summary>
    /// Dump identifier, obtained via the<br/>
    /// [event-dump/create](https://docs.unione.io/en/web-api-ref#event-dump-create) method.
    /// </summary>
    [JsonPropertyName("dump_id")]
    public string? DumpId { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
