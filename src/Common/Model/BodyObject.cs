using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Common.Model;

/// <summary>
/// Contains HTML/plaintext/AMP parts of the email. Either html or plaintext part is required.
/// </summary>
public class BodyObject
{
    /// <summary>
    /// HTML part of the email body
    /// </summary>
    [JsonPropertyName("html")]
    public string? Html { get; set; }

    /// <summary>
    /// Plaintext part of the email body.
    /// </summary>
    [JsonPropertyName("plaintext")]
    public string? Plaintext { get; set; }

    /// <summary>
    /// Optional [AMP](http://amp.dev/documentation/guides-and-tutorials/learn/email_fundamentals/) part of<br/>
    /// the email body.
    /// </summary>
    [JsonPropertyName("amp")]
    public string? Amp { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
