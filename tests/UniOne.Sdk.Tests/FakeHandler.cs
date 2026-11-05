using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UniOne.Sdk.Tests;

/// <summary>Fake transport: records the requests and answers without any network.</summary>
public sealed class FakeHandler : HttpMessageHandler
{
    private readonly Func<CancellationToken, Task<HttpResponseMessage>> _respond;

    public FakeHandler(Func<CancellationToken, Task<HttpResponseMessage>> respond)
    {
        _respond = respond;
    }

    public List<RecordedRequest> Requests { get; } = new();

    public static FakeHandler Json(int status, string body)
    {
        return Text(status, body, "application/json");
    }

    public static FakeHandler Text(int status, string body, string mediaType)
    {
        return new FakeHandler(_ => Task.FromResult(new HttpResponseMessage((HttpStatusCode)status)
        {
            Content = new StringContent(body, Encoding.UTF8, mediaType),
        }));
    }

    public static FakeHandler Throws(Exception error)
    {
        return new FakeHandler(_ => Task.FromException<HttpResponseMessage>(error));
    }

    /// <summary>Never answers: waits until the request is cancelled (a timeout or the token of the caller).</summary>
    public static FakeHandler Hangs()
    {
        return new FakeHandler(async token =>
        {
            await Task.Delay(Timeout.InfiniteTimeSpan, token);
            throw new InvalidOperationException("not cancelled");
        });
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var body = request.Content == null ? "" : await request.Content.ReadAsStringAsync(cancellationToken);
        lock (Requests)
        {
            Requests.Add(new RecordedRequest(request, body));
        }

        return await _respond(cancellationToken);
    }
}
