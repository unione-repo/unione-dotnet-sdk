using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
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

    private static readonly Regex AddressPattern = new Regex("^(.*)<([^>]+)>$", RegexOptions.Singleline);

    /// <summary>
    /// A recipient from "email" or "Name &lt;email&gt;": the name goes to the to_name substitution (the name in the
    /// "To" header).
    /// </summary>
    /// <param name="address">"email" or "Name &lt;email&gt;".</param>
    /// <param name="substitutions">More substitutions of the recipient.</param>
    public static MessageObjectRecipientsItem FromAddress(string address, IDictionary<string, string>? substitutions = null)
    {
        var email = address.Trim();
        var name = "";
        var match = AddressPattern.Match(email);
        if (match.Success)
        {
            email = match.Groups[2].Value.Trim();
            name = match.Groups[1].Value.Trim().Trim('"');
        }
        var all = substitutions == null ? null : new Dictionary<string, string>(substitutions);
        if (name.Length > 0)
        {
            all = new Dictionary<string, string> { ["to_name"] = name };
            if (substitutions != null)
            {
                foreach (var entry in substitutions)
                {
                    all[entry.Key] = entry.Value;  // an explicit to_name wins
                }
            }
        }
        return new MessageObjectRecipientsItem { Email = email, Substitutions = all };
    }

    /// <summary>Sets the metadata of the recipient (returned in its webhook events).</summary>
    /// <param name="metadata">The metadata.</param>
    /// <returns>This recipient.</returns>
    public MessageObjectRecipientsItem WithMetadata(IDictionary<string, string> metadata)
    {
        Metadata = new Dictionary<string, string>(metadata);
        return this;
    }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
