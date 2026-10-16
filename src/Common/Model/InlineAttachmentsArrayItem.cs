using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Common.Model;

/// <summary>
/// InlineAttachmentsArrayItem
/// </summary>
public class InlineAttachmentsArrayItem
{
    /// <summary>
    /// Attachment type, see [MIME](https://en.wikipedia.org/wiki/MIME). If unsure, use<br/>
    /// "application/octet-stream".
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Attachment content id. For example, "name"="IMAGECID1" should be referenced in HTML like &lt;img<br/>
    /// src="cid:IMAGECID1"&gt;.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// File contents in [base64](https://en.wikipedia.org/wiki/Base64). Maximum file size 7MB (9786710<br/>
    /// bytes in base64).
    /// </summary>
    [JsonPropertyName("content")]
    public string? Content { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
