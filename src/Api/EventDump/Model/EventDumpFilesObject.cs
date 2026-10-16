using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.EventDump.Model;

/// <summary>
/// An object with the properties of the event dump file.
/// </summary>
public class EventDumpFilesObject
{
    /// <summary>
    /// Use this unique URL to download the requested dump file (either .csv or .csv.gz, depending on your<br/>
    /// request).
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// File size.
    /// </summary>
    [JsonPropertyName("size")]
    public int? Size { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
