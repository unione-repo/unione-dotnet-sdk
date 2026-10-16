using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;
using UniOne.Common.Model;

namespace UniOne.Api.Template.Model;

/// <summary>
/// Object with all the template properties.
/// </summary>
public class TemplateObject
{
    /// <summary>
    /// Template unique identifier. If you omit the "id" field then new template will be created. If you<br/>
    /// specify the "id" field, the system will try to find a template with the specified id and edit it.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Template name.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Editor type, "html" or "visual". Default is "html". You can create a template with "visual" editor<br/>
    /// type only in web interface.
    /// </summary>
    [JsonPropertyName("editor_type")]
    public EditorType? EditorType { get; set; }

    /// <summary>
    /// The [template engine](https://docs.unione.io/en/template-engines) for handling the<br/>
    /// substitutions(merge tags), "simple", "velocity", "liquid" or "none". Default value is "simple".
    /// </summary>
    [JsonPropertyName("template_engine")]
    public TemplateEngine? TemplateEngine { get; set; }

    /// <summary>
    /// Object for passing the substitutions(merge tags) common for all recipients - e.g., company name. If<br/>
    /// the substitution names are duplicated in object "substitutions", the values of the variables will be<br/>
    /// taken from the object "substitutions". The substitutions can be used in the following parameters:
    /// <para/>
    /// - body.html<br/>
    /// - body.plaintext<br/>
    /// - body.amp<br/>
    /// - subject<br/>
    /// - from_name<br/>
    /// - options.unsubscribe_url<br/>
    /// A substitution name can consist from latin characeters, numbers and "_" symbol, and should start<br/>
    /// with the letter.
    /// </summary>
    [JsonPropertyName("global_substitutions")]
    public Dictionary<string, string>? GlobalSubstitutions { get; set; }

    /// <summary>
    /// Object for passing the metadata common for all the recipients, such as "key": "value". Max key<br/>
    /// quantity: 10. Max key length: 64 symbols. Max value length: 1024 symbols. The metadata is returned<br/>
    /// by [webhook](https://docs.unione.io/en/web-api-ref#webhook). To group single emails into a campaign,<br/>
    /// you can use the system key 'campaign_id' with a value in the form of a non-negative decimal integer<br/>
    /// up to 128 bits or a UUID in the format 'c7703772-453e-4c77-a9ae-9a1b94051b5a'. Incorrect values will<br/>
    /// be treated as '0'.
    /// </summary>
    [JsonPropertyName("global_metadata")]
    public Dictionary<string, string>? GlobalMetadata { get; set; }

    /// <summary>
    /// Contains HTML/plaintext/AMP parts of the email. Either html or plaintext part is required.
    /// </summary>
    [JsonPropertyName("body")]
    public BodyObject? Body { get; set; }

    /// <summary>
    /// Email subject.
    /// </summary>
    [JsonPropertyName("subject")]
    public string? Subject { get; set; }

    /// <summary>
    /// Sender's email.
    /// </summary>
    [JsonPropertyName("from_email")]
    public string? FromEmail { get; set; }

    /// <summary>
    /// Sender's name.
    /// </summary>
    [JsonPropertyName("from_name")]
    public string? FromName { get; set; }

    /// <summary>
    /// Optional Reply-To email (in case it's different to sender's email)
    /// </summary>
    [JsonPropertyName("reply_to")]
    public string? ReplyTo { get; set; }

    /// <summary>
    /// Optional Reply-To name (if reply_to email is specified and you want to display not only this email<br/>
    /// but also the name)
    /// </summary>
    [JsonPropertyName("reply_to_name")]
    public string? ReplyToName { get; set; }

    /// <summary>
    /// 1 = click tracking is on (default), 0 = click tracking is off. To use track_links = 0, you need to<br/>
    /// ask support to enable this feature.<br/>
    /// Allowed values: `0`, `1`.
    /// </summary>
    [JsonPropertyName("track_links")]
    public int? TrackLinks { get; set; }

    /// <summary>
    /// 1 = read tracking is on (default), 0=read tracking is off. To use track_read = 0, you need to ask<br/>
    /// support to enable this feature.<br/>
    /// Allowed values: `0`, `1`.
    /// </summary>
    [JsonPropertyName("track_read")]
    public int? TrackRead { get; set; }

    /// <summary>
    /// Contains email headers, maximum 50. Only headers with “X-” name prefix are accepted, all other are<br/>
    /// ignored, for example X-UNIONE-Global-Language, X-UNIONE-Template-Engine. Standard headers "To,"<br/>
    /// "CC," and "BCC" are passed without the "X-." Yet, they are processed in a particular way and, as a<br/>
    /// result, have a number of restrictions. You can find more details about it [here](/cc-and-bcc). If<br/>
    /// our support have approved omitting standard unsubscription block for you, you can also pass<br/>
    /// List-Unsubscribe, List-Subscribe, List-Help, List-Owner, List-Archive, In-Reply-To and References<br/>
    /// headers.
    /// </summary>
    [JsonPropertyName("headers")]
    public HeadersObject? Headers { get; set; }

    /// <summary>
    /// Optional array of attachments
    /// </summary>
    [JsonPropertyName("attachments")]
    public List<AttachmentsArrayItem>? Attachments { get; set; }

    /// <summary>
    /// Optional array of inline attachments, e.g. for including images in email instead of loading them<br/>
    /// from external URL.
    /// </summary>
    [JsonPropertyName("inline_attachments")]
    public List<InlineAttachmentsArrayItem>? InlineAttachments { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
