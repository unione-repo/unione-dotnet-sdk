using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.EventDump.Model;

/// <summary>
/// EventDumpCreateResponse
/// </summary>
public class EventDumpCreateResponse
{
    /// <summary>
    /// "success" string<br/>
    /// Allowed values: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Dump identifier, used to call the<br/>
    /// [event-dump/get](https://docs.unione.io/en/web-api-ref#event-dump-get) method.
    /// </summary>
    [JsonPropertyName("dump_id")]
    public string? DumpId { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
