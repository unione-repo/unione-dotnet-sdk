using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UniOne.Client;

namespace UniOne.Api.Email.Model;

/// <summary>
/// EmailSendRequest
/// </summary>
public class EmailSendRequest
{
    /// <summary>
    /// Object containing all message properties.
    /// </summary>
    [JsonPropertyName("message")]
    public MessageObject? Message { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
