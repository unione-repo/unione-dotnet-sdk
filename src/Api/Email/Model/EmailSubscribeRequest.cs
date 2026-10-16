using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Email.Model;

/// <summary>
/// EmailSubscribeRequest
/// </summary>
public class EmailSubscribeRequest
{
    /// <summary>
    /// Sender’s email on a confirmed domain. (Field alias "email_address_from" is supported, too).
    /// </summary>
    [JsonPropertyName("from_email")]
    public string? FromEmail { get; set; }

    /// <summary>
    /// Sender’s name. (Field alias "name_from" is supported, too).
    /// </summary>
    [JsonPropertyName("from_name")]
    public string? FromName { get; set; }

    /// <summary>
    /// Recipient’s email. (Field alias "email_address_to" is supported, too).
    /// </summary>
    [JsonPropertyName("to_email")]
    public string? ToEmail { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
