using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniOne.Api.Tag.Model;
using UniOne.Client;
using UniOne.Common.Model;

namespace UniOne.Api.Tag;

/// <summary>
/// This family of methods is used to manage user-defined tags. Tags are used to categorize letters<br/>
/// based on your own criteria, e. g. to create a distinction between marketing and transactional<br/>
/// letters or group together all notifications for a particular order type. UniOne allows you to assign<br/>
/// tags to all outgoing emails, and the tags you’ve set may then be used to filter and analyze delivery<br/>
/// statistics.
/// </summary>
public class TagApi
{
    private readonly ApiClient _client;

    public TagApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// This method returns the full list of user-defined tags. Does not receive any parameters.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#tag-list
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<TagListResponse> ListAsync(CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<TagListResponse>("/tag/list.json", null, cancellationToken);
    }

    /// <summary>
    /// Removes tag.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#tag-delete
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SuccessResponse> DeleteAsync(TagDeleteRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SuccessResponse>("/tag/delete.json", request, cancellationToken);
    }
}
