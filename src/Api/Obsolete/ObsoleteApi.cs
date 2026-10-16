using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniOne.Api.Obsolete.Model;
using UniOne.Client;

namespace UniOne.Api.Obsolete;

/// <summary>
/// Obsolete methods dealing with unsubscribes. We recommend using the more flexible<br/>
/// [suppression](https://docs.unione.io/en/web-api-ref#suppression) methods instead. Note that you<br/>
/// can't resubscribe an unsubscribed recipient using WebAPI, but you can send him an email with<br/>
/// subscription link using email/subscribe method.
/// </summary>
public class ObsoleteApi
{
    private readonly ApiClient _client;

    public ObsoleteApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Registers a new unsubscribed email.
    /// <para/>
    /// After registering an email as unsubscribed all future attempts to send a message to this email will<br/>
    /// be rejected. Please note that there's no way to remove an email from unsubscribed thru the API. But<br/>
    /// you can send an email with a link to resubscribe using<br/>
    /// [email/subscribe](https://docs.unione.io/en/web-api-ref#email-subscribe) method or remove<br/>
    /// "unsubscribed" status manually on [Email Search page](https://cp.unione.io/en/user/email) in control<br/>
    /// panel (but only a limited number of unsubscribed email per day can be removed).
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#unsubscribed-set
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<ObsoleteUnsubscribedSetResponse> UnsubscribedSetAsync(ObsoleteUnsubscribedSetRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<ObsoleteUnsubscribedSetResponse>("/unsubscribed/set.json", request, cancellationToken);
    }

    /// <summary>
    /// Checks if an email is unsubscribed.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#unsubscribed-check
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<ObsoleteUnsubscribedCheckResponse> UnsubscribedCheckAsync(ObsoleteUnsubscribedCheckRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<ObsoleteUnsubscribedCheckResponse>("/unsubscribed/check.json", request, cancellationToken);
    }

    /// <summary>
    /// Returns a list of all unsubscribed emails since provided date.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#unsubscribed-list
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<ObsoleteUnsubscribedListResponse> UnsubscribedListAsync(ObsoleteUnsubscribedListRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<ObsoleteUnsubscribedListResponse>("/unsubscribed/list.json", request, cancellationToken);
    }
}
