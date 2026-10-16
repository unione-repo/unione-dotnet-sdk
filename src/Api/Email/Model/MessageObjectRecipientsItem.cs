using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Email.Model;

/// <summary>
/// MessageObjectRecipientsItem
/// </summary>
public class MessageObjectRecipientsItem
{
    /// <summary>
    /// Recipient email
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Object to pass the substitutions(merge tags) for the recipient (e.g. recipient name, discount code,<br/>
    /// password change link, etc. See [Template engines](https://docs.unione.io/en/template-engines)). The<br/>
    /// substitutions can be used in the following parameters:
    /// <para/>
    /// - body.html<br/>
    /// - body.plaintext<br/>
    /// - body.amp<br/>
    /// - subject<br/>
    /// - from_name<br/>
    /// - headers["List-Unsubscribe"]<br/>
    /// - options.unsubscribe_url<br/>
    /// A substitution name can consist from latin characters, numbers and "_" symbol, and should start with<br/>
    /// the letter. There's a special substitution "to_name" which is used to put recipent's name like "Name<br/>
    /// Surname" to include it to SMTP header "To" in the form "Name Surname &lt;email@example.com&gt;". The<br/>
    /// "to_name" length is limited to 78 symbols.
    /// </summary>
    [JsonPropertyName("substitutions")]
    public Dictionary<string, string>? Substitutions { get; set; }

    /// <summary>
    /// Object for passing the metadata, such as "key": "value". Max key quantity: 10. Max key length: 64<br/>
    /// symbols. Max value length: 1024 symbols. The metadata is returned by<br/>
    /// [webhook](https://docs.unione.io/en/web-api-ref#webhook) and<br/>
    /// [event-dump](https://docs.unione.io/en/web-api-ref#event-dump) methods. If you pass strign up to 128<br/>
    /// bit with "campaign_id" field name (name is configurable thru support), it will be considered as<br/>
    /// campaign identifier in statistics.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
