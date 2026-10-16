using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniOne.Api.Template.Model;
using UniOne.Client;
using UniOne.Common.Model;

namespace UniOne.Api.Template;

/// <summary>
/// You have the option of storing a template on the UniOne side and then using it by id, which is<br/>
/// highly recommended for sending repetitive messages or high volume traffic. UniOne supports two<br/>
/// different [template engines](https://docs.unione.io/en/template-engines).
/// </summary>
public class TemplateApi
{
    private readonly ApiClient _client;

    public TemplateApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Creates or updates an email template.
    /// <para/>
    /// Restrictions:<br/>
    /// * The maximum request size is 10 MB.<br/>
    /// * The number of templates is limited to 10000.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#template-set
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<TemplateSetResponse> SetAsync(TemplateSetRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<TemplateSetResponse>("/template/set.json", request, cancellationToken);
    }

    /// <summary>
    /// Gets template properties by it's id.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#template-get
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<TemplateGetResponse> GetAsync(TemplateGetRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<TemplateGetResponse>("/template/get.json", request, cancellationToken);
    }

    /// <summary>
    /// Returns the list of all or some templates of the account.
    /// <para/>
    /// You can iterate thru large list using "offset" and "limit" parameters. When less than "limit"<br/>
    /// templates are returned then the end of list is reached.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#template-list
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<TemplateListResponse> ListAsync(TemplateListRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<TemplateListResponse>("/template/list.json", request, cancellationToken);
    }

    /// <summary>
    /// Deletes a template by id.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#template-delete
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SuccessResponse> DeleteAsync(TemplateDeleteRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SuccessResponse>("/template/delete.json", request, cancellationToken);
    }
}
