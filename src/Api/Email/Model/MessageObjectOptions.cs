using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Email.Model;

/// <summary>
/// Additional message options.
/// </summary>
public class MessageObjectOptions
{
    /// <summary>
    /// Date and time in "YYYY-MM-DD hh:mm:ss" format in the UTC timezone. Allows schedule sending up to 24<br/>
    /// hours in advance.
    /// </summary>
    [JsonPropertyName("send_at")]
    public string? SendAt { get; set; }

    /// <summary>
    /// Custom unsubscribe link. Read more [here](https://docs.unione.io/en/unsubscribe-link).
    /// </summary>
    [JsonPropertyName("unsubscribe_url")]
    public string? UnsubscribeUrl { get; set; }

    /// <summary>
    /// Backend-domain identifier to send message with. If custom_backend_id is not set, default one for<br/>
    /// your account/project will be used. You can pay for one or more dedicated IPs and use this option.
    /// </summary>
    [JsonPropertyName("custom_backend_id")]
    public int? CustomBackendId { get; set; }

    /// <summary>
    /// SMTP pool identifier to send message with. Usually you don't need to pass this parameter because<br/>
    /// correct smtp_pool_id is selected automatically based on the default one for your account/project or<br/>
    /// according to custom_backend_id parameter.
    /// </summary>
    [JsonPropertyName("smtp_pool_id")]
    public string? SmtpPoolId { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
