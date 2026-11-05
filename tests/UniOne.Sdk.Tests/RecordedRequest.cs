using System;
using System.Collections.Generic;
using System.Net.Http;

namespace UniOne.Sdk.Tests;

/// <summary>A request as the handler got it (the client disposes the request after the call).</summary>
public sealed class RecordedRequest
{
    private readonly Dictionary<string, string> _headers = new(StringComparer.OrdinalIgnoreCase);

    public RecordedRequest(HttpRequestMessage request, string body)
    {
        Method = request.Method;
        Url = request.RequestUri!;
        ContentType = request.Content?.Headers.ContentType?.MediaType;
        Body = body;
        foreach (var header in request.Headers)
        {
            _headers[header.Key] = string.Join(",", header.Value);
        }
    }

    public HttpMethod Method { get; }

    public Uri Url { get; }

    public string? ContentType { get; }

    public string Body { get; }

    public string? Header(string name)
    {
        return _headers.TryGetValue(name, out var value) ? value : null;
    }
}
