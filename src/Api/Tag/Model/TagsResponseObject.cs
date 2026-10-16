using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Tag.Model;

/// <summary>
/// An object with the properties of the user tags.
/// </summary>
public class TagsResponseObject
{
    /// <summary>
    /// Unique tag id.
    /// </summary>
    [JsonPropertyName("tag_id")]
    public int? TagId { get; set; }

    /// <summary>
    /// Tag name.
    /// </summary>
    [JsonPropertyName("tag")]
    public string? Tag { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
