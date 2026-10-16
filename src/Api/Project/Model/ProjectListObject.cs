using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;
using UniOne.Common.Model;

namespace UniOne.Api.Project.Model;

/// <summary>
/// ProjectListObject
/// </summary>
public class ProjectListObject
{
    /// <summary>
    /// Unqiue project identifier, ASCII string up to 36 characters long.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// API key of the project. You can use it instead of the user API key parameter in all methods except<br/>
    /// project/* methods.
    /// </summary>
    [JsonPropertyName("api_key")]
    public string? ApiKey { get; set; }

    /// <summary>
    /// Project name, unique for user account.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// ISO-3166 alpha-2 country code. If set, UniOne treats project personal data according to country<br/>
    /// laws, e.g. GDPR for european countries.
    /// </summary>
    [JsonPropertyName("country")]
    public string? Country { get; set; }

    /// <summary>
    /// Date and time of project creation in UTC timezone in "YYYY-MM-DD hh:mm:ss" format.
    /// </summary>
    [JsonPropertyName("reg_time")]
    public string? RegTime { get; set; }

    /// <summary>
    /// Whether email sending is enabled for this project or not.<br/>
    /// Default: `True`.
    /// </summary>
    [JsonPropertyName("send_enabled")]
    public bool? SendEnabled { get; set; }

    /// <summary>
    /// If it's false then UniOne adds default unsubscribe footer to each email sent with API key of the<br/>
    /// project. If it's true then appending default unsubscribe footer for this project is avoided and<br/>
    /// sending with custom unsubscribe url or even without unsubscribe url is permitted for the project.<br/>
    /// Please note that custom_unsubscribe_url_enabled=true is available only if removing unsubscribe link<br/>
    /// is approved for the user account by support. More on this<br/>
    /// [here](https://docs.unione.io/en/unsubscribe-link). When custom_unsubscribe_url_enabled is skipped<br/>
    /// on creating a project, it's value is taken from user
    /// </summary>
    [JsonPropertyName("custom_unsubscribe_url_enabled")]
    public bool? CustomUnsubscribeUrlEnabled { get; set; }

    /// <summary>
    /// A unique domain identifier which will determine the [tracking<br/>
    /// domain](https://docs.unione.io/en/tracking-domains) or [dedicated IP](/dedicated-ip) pool to be used<br/>
    /// by default. If the value is not specified, a default backend domain will be assigned for your<br/>
    /// account or project instead.
    /// </summary>
    [JsonPropertyName("backend_domain_id")]
    public int? BackendDomainId { get; set; }

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

    /// <summary>
    /// A unique identifier that defines the default unsubscribe page. If absent, the account/project will<br/>
    /// be assigned the system default unsubscribe page.
    /// </summary>
    [JsonPropertyName("unsubscribe_page_id")]
    public int? UnsubscribePageId { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
