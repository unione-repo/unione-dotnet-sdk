using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniOne.Api.Email.Model;
using UniOne.Client;
using UniOne.Common.Model;

namespace UniOne.Api.Email;

/// <summary>
/// This group of methods is related with email sending.
/// </summary>
public class EmailApi
{
    private readonly ApiClient _client;

    public EmailApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Sends an email or a bunch of emails
    /// <para/>
    /// While sending you can provide substitutions (merge tags), use a template, turn on read or click<br/>
    /// tracking, etc.
    /// <para/>
    /// Restrictions:<br/>
    /// * maximum request size is 10 MB<br/>
    /// * maximum number of recipients is 500<br/>
    /// * The symbol '/' is not allowed in attachment names
    /// <para/>
    /// If the list of recipients contains invalid, non-existent, duplicate, or unsubscribed emails, those<br/>
    /// emails will be returned in the output parameter failed_emails, and the email will be sent to all<br/>
    /// other valid emails addresses. If all the emails addresses fall into failed_emails, sending will not<br/>
    /// be carried out and API error 204 will be returned.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#email-send
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<EmailSendResponse> SendAsync(EmailSendRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<EmailSendResponse>("/email/send.json", request, cancellationToken);
    }

    /// <summary>
    /// Sends an email with (re)subscribe link.
    /// <para/>
    /// This method allows subscription renewal by a former subscriber who has previously unsubscribed, or<br/>
    /// who is blocked because of complaints. This method sends an email with a new subscription link to a<br/>
    /// former subscriber. Once the recipient clicks the link, their subscription status is restored.<br/>
    /// There is a default restriction that limits this method to being used once a day. If you need to<br/>
    /// resubscribe more often, please ask support about it.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#email-subscribe
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SuccessResponse> SubscribeAsync(EmailSubscribeRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SuccessResponse>("/email/subscribe.json", request, cancellationToken);
    }
}
