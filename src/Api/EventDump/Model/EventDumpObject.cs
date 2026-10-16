using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.EventDump.Model;

/// <summary>
/// An object with the properties of the event dump.
/// </summary>
public class EventDumpObject
{
    /// <summary>
    /// Dump ID received by the method<br/>
    /// [event-dump/create](https://docs.unione.io/en/web-api-ref#event-dump-create).
    /// </summary>
    [JsonPropertyName("dump_id")]
    public string? DumpId { get; set; }

    /// <summary>
    /// Task status, possible values are:
    /// <para/>
    /// - __queued__ - the task is in the queue, processing is not started yet;<br/>
    /// - __in_process__ - the request is being processed;<br/>
    /// - __ready__ - the task is finished, dump file is ready for download;<br/>
    /// - __failed__ -  processing failed due to an internal error.
    /// </summary>
    [JsonPropertyName("dump_status")]
    public string? DumpStatus { get; set; }

    /// <summary>
    /// An array of objects, each representing a file, ready for download. You may start downloading<br/>
    /// everything listed in “files” even if "dump_status" is still "in_process". If there are no events,<br/>
    /// according to the specified parameters, an empty array will be returned.
    /// </summary>
    [JsonPropertyName("files")]
    public List<EventDumpFilesObject>? Files { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
