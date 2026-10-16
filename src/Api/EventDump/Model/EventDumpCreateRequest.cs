using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.EventDump.Model;

/// <summary>
/// EventDumpCreateRequest
/// </summary>
public class EventDumpCreateRequest
{
    /// <summary>
    /// Date and time in YYYY-MM-DD hh:mm:ss format, specifying period start time. Data is stored for up to<br/>
    /// 32 days, depending on your tariff.
    /// </summary>
    [JsonPropertyName("start_time")]
    public string? StartTime { get; set; }

    /// <summary>
    /// Date and time in YYYY-MM-DD hh:mm:ss format, specifying period end time (non-inclusive).
    /// </summary>
    [JsonPropertyName("end_time")]
    public string? EndTime { get; set; }

    /// <summary>
    /// Maximum number of events returned (default is 50). If this value is over 100 000, several files will<br/>
    /// be created, each having 100 000 events maximum.
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    /// <summary>
    /// For accounts with projects enabled, the value all_projects=true allows to fetch data for all<br/>
    /// existing projects.<br/>
    /// Default: `False`.
    /// </summary>
    [JsonPropertyName("all_projects")]
    public bool? AllProjects { get; set; }

    /// <summary>
    /// An object with the properties of the event dump filter.
    /// </summary>
    [JsonPropertyName("filter")]
    public FilterObject? Filter { get; set; }

    /// <summary>
    /// An array of strings containing the names of columns you need in your report, in your preferred<br/>
    /// order. If the 'dump_fields' array is missing or empty, a default field list and order will be<br/>
    /// applied. Allowed values, in their default order, are - 'project_id', 'event_time', 'job_id',<br/>
    /// 'from_email', 'email', 'status', 'delivery_status', 'metadata', 'destination_response',<br/>
    /// 'user_agent', 'url', 'ip', 'tags'. For maximum security, the 'subject' field is not included by<br/>
    /// default, but you can add it by explicitly including in the 'dump_fields' array.
    /// </summary>
    [JsonPropertyName("dump_fields")]
    public List<string>? DumpFields { get; set; }

    /// <summary>
    /// Allows you to obtain aggregated statistics for a given time period. The two allowed values are:
    /// <para/>
    /// - __day_status__ - fetch aggregated statistics (the filter value must be missing or empty, as filters are not allowed with this option);<br/>
    /// - __an empty string__ - used by default, event_dump/create works as if the aggregate parameter is omitted.
    /// </summary>
    [JsonPropertyName("aggregate")]
    public string? Aggregate { get; set; }

    /// <summary>
    /// Field delimiter, can be set to either  ',' or ';', defaults to  ','.
    /// </summary>
    [JsonPropertyName("delimiter")]
    public string? Delimiter { get; set; }

    /// <summary>
    /// File format, either “csv” (default) or “csv_gzip”.<br/>
    /// Default: `csv`.
    /// </summary>
    [JsonPropertyName("format")]
    public string? Format { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
