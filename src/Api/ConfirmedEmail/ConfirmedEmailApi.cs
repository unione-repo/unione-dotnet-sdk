using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniOne.Api.ConfirmedEmail.Model;
using UniOne.Client;

namespace UniOne.Api.ConfirmedEmail;

/// <summary>
/// Manage email addresses confirmed for sandbox-domain sending. These methods are available only with a<br/>
/// main account API key.
/// </summary>
public class ConfirmedEmailApi
{
    private readonly ApiClient _client;

    public ConfirmedEmailApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Adds an email address and sends a confirmation link.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#confirmed-email-create
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<ConfirmedEmailCreateResponse> CreateAsync(ConfirmedEmailRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<ConfirmedEmailCreateResponse>("/confirmed-email/create.json", request, cancellationToken);
    }

    /// <summary>
    /// Lists confirmed and unconfirmed email addresses for the account.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#confirmed-email-list
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<ConfirmedEmailListResponse> ListAsync(CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<ConfirmedEmailListResponse>("/confirmed-email/list.json", null, cancellationToken);
    }

    /// <summary>
    /// Deletes an email address from the account's confirmed email list.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#confirmed-email-delete
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<ConfirmedEmailDeleteResponse> DeleteAsync(ConfirmedEmailRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<ConfirmedEmailDeleteResponse>("/confirmed-email/delete.json", request, cancellationToken);
    }
}
