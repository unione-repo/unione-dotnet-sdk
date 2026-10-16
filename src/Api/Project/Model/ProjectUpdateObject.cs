using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;
using UniOne.Common.Model;

namespace UniOne.Api.Project.Model;

/// <summary>
/// Object with project properties.
/// </summary>
public class ProjectUpdateObject
{
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
    /// A unique identifier that defines the default unsubscribe page. If absent, the account/project will<br/>
    /// be assigned the system default unsubscribe page.
    /// </summary>
    [JsonPropertyName("unsubscribe_page_id")]
    public int? UnsubscribePageId { get; set; }

    /// <summary>
    /// Project's email counter value. 0 value only is accepted (which means resetting the counter to zero),<br/>
    /// or refreshes automatically depending on the counter mode set with the 'email_counter_mode'<br/>
    /// parameter. If the request doesn't contain the field or contains null, the limit value doesn't<br/>
    /// change.
    /// </summary>
    [JsonPropertyName("email_counter")]
    public int? EmailCounter { get; set; }

    /// <summary>
    /// Allows you to limit the number of emails sent by the project. The sending stops when the limit is<br/>
    /// reached. Any non-negative 64-bit integer is acceptable. Setting to 0 means no limit. If the request<br/>
    /// doesn't contain the field or contains null, the limit value doesn't change.<br/>
    /// Default: `0`.
    /// </summary>
    [JsonPropertyName("email_counter_limit")]
    public int? EmailCounterLimit { get; set; }

    /// <summary>
    /// Sets the email counter mode. It allows you to choose whether the counter should reset automatically,<br/>
    /// or remain permanent. The following values are acceptable: 'default' - the counter resets to zero<br/>
    /// automatically at the beginning of each accounting period, 'permanent' - the counter never resets<br/>
    /// automatically. The reset can be done only manually by the user in the request with the passes 0<br/>
    /// value in the email_counter field. If the request doesn't contain the field or contains a null value,<br/>
    /// the limit value doesn't change.<br/>
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
