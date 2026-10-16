using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using UniOne.Api.Webhook.Model;
using UniOne.Client;
using UniOne.Common.Model;

namespace UniOne.Api.Webhook;

/// <summary>
/// UniOne lets you recieve notifications about email status changes or spam blocks. You provide an URL<br/>
/// and every time a watched event occurs we will notify you by submitting a request with JSON data to<br/>
/// this URL.
/// <para/>
/// You can configure the format of webhook calls (JSON or gzipped JSON), which events you want to be<br/>
/// notified of, whether or not you let us groups several events in one request, do you want to receive<br/>
/// extra details like SMTP answer or recipient's user agent, etc.
/// <para/>
/// Please note that your webhook handler should be able to process at least 5 parallel requests, up to<br/>
/// 100 events each (but you can configure the maximum number of parallel connections up to 100, which<br/>
/// is recommended).
/// <para/>
/// If the URL of your webhook handler is not available (the response is not HTTP 200 OK within 3<br/>
/// seconds timeout), the system will repeat requests each 10 minutes during 24 hours, passing extra<br/>
/// parameter retry_count. If there were at least 10 events during last 24 hours and all the webhook<br/>
/// requests about these events have failed then we consider webhook handler broken and change it's<br/>
/// status to inactive automatically. A notification about that is sent to an email [configured in<br/>
/// control panel](https://cp.unione.io/en/user/alerting/technical/).
/// <para/>
/// The format of webhook request and details of each event are described<br/>
/// [here](https://docs.unione.io/en/web-api-ref#callback-format).
/// </summary>
public class WebhookApi
{
    private readonly ApiClient _client;

    public WebhookApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Sets or edits a [webhook](https://docs.unione.io/en/web-api-ref#webhook), i.e. an event notification<br/>
    /// handler.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#webhook-set
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<WebhookSetResponse> SetAsync(WebhookSetRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<WebhookSetResponse>("/webhook/set.json", request, cancellationToken);
    }

    /// <summary>
    /// Gets properties of a [webhook](https://docs.unione.io/en/web-api-ref#webhook).
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#webhook-get
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<WebhookGetResponse> GetAsync(WebhookGetRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<WebhookGetResponse>("/webhook/get.json", request, cancellationToken);
    }

    /// <summary>
    /// List all or some [webhooks](https://docs.unione.io/en/web-api-ref#webhook) (event notification<br/>
    /// handlers) of a user or a project.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#webhook-list
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<WebhookListResponse> ListAsync(WebhookListRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<WebhookListResponse>("/webhook/list.json", request, cancellationToken);
    }

    /// <summary>
    /// Deletes an event notification handler
    /// <para/>
    /// You can also temporarily deactivate webhook without deleting it by setting it's status to "disabled"<br/>
    /// in [webhook/set](https://docs.unione.io/en/web-api-ref#webhook-set) method.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#webhook-delete
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SuccessResponse> DeleteAsync(WebhookDeleteRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SuccessResponse>("/webhook/delete.json", request, cancellationToken);
    }

    /// <summary>
    /// Webhook callback format description.
    /// </summary>
    /// <param name="body">Raw body of the incoming request.</param>
    public static WebhookCallback ParseCallback(string body)
    {
        return JsonSerializer.Deserialize<WebhookCallback>(body, ApiClient.JsonOptions)
            ?? throw new ApiException("Empty callback body", 0, null, body);
    }
}
