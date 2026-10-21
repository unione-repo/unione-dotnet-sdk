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

    /// <summary>An HTML body, with an optional plain-text part.</summary>
    /// <param name="html">The HTML part.</param>
    /// <param name="plaintext">The plain-text part.</param>
    public static BodyObject FromHtml(string html, string? plaintext = null)
    {
        return new BodyObject { Html = html, Plaintext = plaintext };
    }

    /// <summary>A plain-text body.</summary>
    /// <param name="plaintext">The text.</param>
    public static BodyObject FromText(string plaintext)
    {
        return new BodyObject { Plaintext = plaintext };
    }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
