using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;
using UniOne.Common.Model;

namespace UniOne.Api.System.Model;

/// <summary>
/// Object with email counter properties. Present only if the API key used for request is the user API<br/>
/// key.
/// </summary>
public class ProjectAccountingObject
{
    /// <summary>
    /// Project's email counter value. It increases on every email sent.
    /// </summary>
    [JsonPropertyName("email_counter")]
    public int? EmailCounter { get; set; }

    /// <summary>
    /// Allows you to limit the number of emails sent by the project. The sending stops when the limit is<br/>
    /// reached. Any non-negative 64-bit integer is acceptable. 0 means no limit.
    /// </summary>
    [JsonPropertyName("email_counter_limit")]
    public int? EmailCounterLimit { get; set; }

    /// <summary>
    /// 'default' - the counter resets to zero automatically at the beginning of each accounting period.<br/>
    /// 'permanent' - the counter never resets automatically.<br/>
    /// Default: `default`.
    /// </summary>
    [JsonPropertyName("email_counter_mode")]
    public EmailCounterMode? EmailCounterMode { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
