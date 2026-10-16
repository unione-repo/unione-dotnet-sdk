using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniOne.Api.Suppression.Model;
using UniOne.Client;
using UniOne.Common.Model;

namespace UniOne.Api.Suppression;

/// <summary>
/// Methods for managing suppression lists. An email can be added to suppression list because of<br/>
/// unsubscription, bounce or complain.
/// </summary>
public class SuppressionApi
{
    private readonly ApiClient _client;

    public SuppressionApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Adds an email address to the suppression list. You can always remove this address from the<br/>
    /// suppression list later using the<br/>
    /// [suppression/delete](https://docs.unione.io/en/web-api-ref#suppression-delete) method.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#suppression-set
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SuccessResponse> SetAsync(SuppressionSetRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SuccessResponse>("/suppression/set.json", request, cancellationToken);
    }

    /// <summary>
    /// Gets a reason and date of email suppression.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#suppression-get
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SuppressionGetResponse> GetAsync(SuppressionGetRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SuppressionGetResponse>("/suppression/get.json", request, cancellationToken);
    }

    /// <summary>
    /// Returns a suppression list since provided date.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#suppression-list
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SuppressionListResponse> ListAsync(SuppressionListRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SuppressionListResponse>("/suppression/list.json", request, cancellationToken);
    }

    /// <summary>
    /// Deletes an email from suppression list (only records with is_deletable=true). If there are no<br/>
    /// entries of this email in the suppression list, API error 3003 is returned. If such entries exist but<br/>
    /// no one can be deleted, API error 3004 is returned. If daily limit of deletes exceeded, API error 906<br/>
    /// is returned.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#suppression-delete
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SuccessResponse> DeleteAsync(SuppressionDeleteRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SuccessResponse>("/suppression/delete.json", request, cancellationToken);
    }
}
