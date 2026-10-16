using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniOne.Api.EmailValidation.Model;
using UniOne.Client;

namespace UniOne.Api.EmailValidation;

/// <summary>
/// Email validation methods. As a result of the check, you’ll get detailed information on email<br/>
/// validity. It will help eliminate nonexistent emails, decrease the number of complaints, clean up the<br/>
/// subscribers list and eventually enhance your sender reputation score. For example, you may add<br/>
/// validation to a subscription form on your site to let users know they’ve just entered an invalid<br/>
/// address.
/// </summary>
public class EmailValidationApi
{
    private readonly ApiClient _client;

    public EmailValidationApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// A maximum of two concurrent requests are allowed; if you send more requests, an error will be<br/>
    /// returned. Since the verification process should be completed promptly, we've set a time limit on<br/>
    /// email check. If we encounter a slow email server that does not respond in 5 seconds, you may get an<br/>
    /// “unknown” status.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#email-validation-single
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<EmailValidationSingleResponse> SingleAsync(EmailValidationSingleRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<EmailValidationSingleResponse>("/email-validation/single.json", request, cancellationToken);
    }
}
