using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;
using UniOne.Common.Model;

namespace UniOne.Api.Email.Model;

/// <summary>
/// Object containing all message properties.
/// </summary>
public class MessageObject
{
    /// <summary>
    /// Contains recipient emails, substitutions(merge tags) and metadata.
    /// </summary>
    [JsonPropertyName("recipients")]
    public List<MessageObjectRecipientsItem>? Recipients { get; set; }

    /// <summary>
    /// Optional identifier of the template that had been created by<br/>
    /// [template/set](https://docs.unione.io/en/web-api-ref#template-set) method. If template_id is passed<br/>
    /// then fields from the template are used instead of missed email/send parameters. E.g. if "body"<br/>
    /// parameter in email/send is omitted, "body" from template will be used. If "subject" in email/send is<br/>
    /// omitted, "subject" from template will be used.
    /// </summary>
    [JsonPropertyName("template_id")]
    public string? TemplateId { get; set; }

    /// <summary>
    /// An array of strings. The maximum string length is 100 characters. You can include up to 4 strings<br/>
    /// which must be unique. No more than 10 000 tags are allowed per project; if you try to add more,<br/>
    /// email/send will return an error. You may use tags to categorize emails by your own criteria, and<br/>
    /// they will be passed along by event-dump methods.
    /// </summary>
    [JsonPropertyName("tags")]
    public List<string>? Tags { get; set; }

    /// <summary>
    /// Whether to skip or not appending default unsubscribe footer. 1=skip, 0=append, default is 0. You<br/>
    /// should [ask support](https://cp.unione.io/en/support) to approve<br/>
    /// usage of skip_unsubscribe=1. [Read more](https://docs.unione.io/en/unsubscribe-link).<br/>
    /// Allowed values: `0`, `1`.
    /// </summary>
    [JsonPropertyName("skip_unsubscribe")]
    public int? SkipUnsubscribe { get; set; }

    /// <summary>
    /// The language of the unsubscribe footer and unsubscribe page. Languages supported are: "be", "de",<br/>
    /// "en", "es", "fr", "it", "pl", "pt", "ru", "ua", "kz".
    /// </summary>
    [JsonPropertyName("global_language")]
    public string? GlobalLanguage { get; set; }

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
    /// Sender's email. Required only if template_id parameter is empty.
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
    /// (allowed values 0 or 1, default 0) If set to 1, the global unavailability list will be ignored. Even<br/>
    /// if the address was found to be unreachable while sending other UniOne users' emails, or its owner<br/>
    /// has issued complaints, the message will still be sent. The setting may be ignored for certain<br/>
    /// addresses.<br/>
    /// Allowed values: `0`, `1`.
    /// </summary>
    [JsonPropertyName("bypass_global")]
    public int? BypassGlobal { get; set; }

    /// <summary>
    /// (allowed values 0 or 1, default 0) If set to 1, the current user's or project's unavailability list<br/>
    /// will be ignored. Works only if bypass_global is set to 1.<br/>
    /// Allowed values: `0`, `1`.
    /// </summary>
    [JsonPropertyName("bypass_unavailable")]
    public int? BypassUnavailable { get; set; }

    /// <summary>
    /// (allowed values 0 or 1, default 0) If set to 1, the current list of unsubscribed addresses for this<br/>
    /// account or project will be ignored. Works only if bypass_global is set to 1. The setting is<br/>
    /// available only for users that have been granted the right to omit the unsubscribe link (to request,<br/>
    /// please contact [support](https://cp.unione.io/en/support)).<br/>
    /// Allowed values: `0`, `1`.
    /// </summary>
    [JsonPropertyName("bypass_unsubscribed")]
    public int? BypassUnsubscribed { get; set; }

    /// <summary>
    /// (allowed values 0 or 1, default 0) If set to 1, the user's or project's complaint list will be<br/>
    /// ignored. Works only if bypass_global is set to 1. The setting is available only for users that have<br/>
    /// been granted the right to omit the unsubscribe link (to request, please contact<br/>
    /// [support](https://cp.unione.io/en/support)).<br/>
    /// Allowed values: `0`, `1`.
    /// </summary>
    [JsonPropertyName("bypass_complained")]
    public int? BypassComplained { get; set; }

    /// <summary>
    /// A string of up to 64 characters containing a unique message key. This can be used to prevent<br/>
    /// occasional message duplicates. If you send another API request with the same message key within the<br/>
    /// next minute, it will be declined. We can generate a message key for each letter automatically; to<br/>
    /// enable this option, please contact our [tech support](https://cp.unione.io/en/support).
    /// </summary>
    [JsonPropertyName("idempotence_key")]
    public string? IdempotenceKey { get; set; }

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

    /// <summary>
    /// Additional message options.
    /// </summary>
    [JsonPropertyName("options")]
    public MessageObjectOptions? Options { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
