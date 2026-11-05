using System;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace UniOne.Client;

/// <summary>
/// Low level transport: every API method is a POST request with a JSON body that returns JSON.
/// </summary>
public class ApiClient
{
    /// <summary>The SDK and its version in the "platform" query parameter of every request.</summary>
    public const string Platform = "dotnet-sdk.2.3.1";

    /// <summary>JSON options of the requests and responses: null fields are omitted.</summary>
    public static readonly JsonSerializerOptions JsonOptions = CreateJsonOptions(false);

    /// <summary>JsonOptions of the request bodies: an enum UnknownValue can not be sent (ApiException).</summary>
    internal static readonly JsonSerializerOptions RequestJsonOptions = CreateJsonOptions(true);

    private readonly Configuration _config;
    private readonly HttpClient _http;

    /// <param name="config">Connection settings; its Timeout applies to every request, also with a custom httpClient
    /// (the shorter of it and HttpClient.Timeout wins).</param>
    /// <param name="httpClient">Custom HttpClient (proxy, handlers, IHttpClientFactory); null: an own one.</param>
    public ApiClient(Configuration config, HttpClient? httpClient = null)
    {
        _config = config;
        _http = httpClient ?? new HttpClient { Timeout = System.Threading.Timeout.InfiniteTimeSpan };
    }

    private static JsonSerializerOptions CreateJsonOptions(bool request)
    {
        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
        };
        if (request)
        {
            // Converters see the options by settings (equal options share their metadata): a marker, not the instance.
            options.Converters.Add(new RequestOptionsMarker());
        }

        return options;
    }

    /// <summary>Calls an API method, e.g. "/email/send.json", and decodes the response.</summary>
    /// <exception cref="ApiException">The API returned an error, the request failed or is invalid (an empty API key, a
    /// required field missing: <see cref="ApiException.Kind"/>), or the response can not be processed.</exception>
    public async Task<T> CallAsync<T>(string path, object? body, CancellationToken cancellationToken = default)
    {
        var method = ErrorInfo.MethodOf(path);
        if (string.IsNullOrEmpty(_config.ApiKey))
        {
            throw Error(method, "the API key is empty: pass the API key of the user or of the project to the client", 0, null,
                null, ApiErrorKind.InvalidRequest);
        }

        string json;
        try
        {
            json = body == null ? "{}" : JsonSerializer.Serialize(body, body.GetType(), RequestJsonOptions);
        }
        catch (JsonException e)
        {
            throw Error(method, "can not encode the request: " + e.Message, 0, null, e, ApiErrorKind.InvalidRequest);
        }

        var missing = ErrorInfo.Missing(method, json);
        if (missing != null)
        {
            throw Error(method, missing + " is required", 0, null, null, ApiErrorKind.InvalidRequest);
        }

        var url = _config.BaseUrl.TrimEnd('/') + "/" + path.TrimStart('/') + "?platform=" + Uri.EscapeDataString(string.IsNullOrEmpty(_config.Platform) ? Platform : _config.Platform + "," + Platform);
        using var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json"),
        };
        request.Headers.TryAddWithoutValidation("Accept", "application/json");
        request.Headers.TryAddWithoutValidation("User-Agent", _config.UserAgent);
        request.Headers.TryAddWithoutValidation("X-API-KEY", _config.ApiKey);

        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        if (_config.Timeout > TimeSpan.Zero)
        {
            timeout.CancelAfter(_config.Timeout);
        }


        HttpResponseMessage response;
        string raw;
        try
        {
            response = await _http.SendAsync(request, timeout.Token).ConfigureAwait(false);
        }
        catch (HttpRequestException e)
        {
            throw Error(method, "HTTP request failed: " + e.Message, 0, null, e, ApiErrorKind.Network);
        }
        catch (OperationCanceledException e) when (!cancellationToken.IsCancellationRequested)
        {
            throw Error(method, TimedOut(), 0, null, e, ApiErrorKind.Timeout);
        }

        using (response)
        {
            var status = (int)response.StatusCode;
            try
            {
                raw = await response.Content.ReadAsStringAsync(timeout.Token).ConfigureAwait(false);
            }
            catch (HttpRequestException e)
            {
                throw Error(method, "can not read the response: " + e.Message, status, null, e, ApiErrorKind.Network);
            }
            catch (OperationCanceledException e) when (!cancellationToken.IsCancellationRequested)
            {
                throw Error(method, TimedOut(), status, null, e, ApiErrorKind.Timeout);
            }

            JsonDocument document;
            try
            {
                document = JsonDocument.Parse(raw);
            }
            catch (JsonException e)
            {
                throw Error(method, $"invalid JSON in response (HTTP {status}): {Excerpt(raw)}", status, raw, e,
                    ApiErrorKind.InvalidResponse);
            }

            using (document)
            {
                var root = document.RootElement;
                if (root.ValueKind != JsonValueKind.Object)
                {
                    throw Error(method, $"unexpected response (HTTP {status}): {Excerpt(raw)}", status, raw, null,
                        ApiErrorKind.InvalidResponse);
                }

                var isError = root.TryGetProperty("status", out var statusField)
                    && statusField.ValueKind == JsonValueKind.String
                    && statusField.GetString() == "error";
                if (status >= 400 || isError)
                {
                    string? message = null;
                    if (root.TryGetProperty("message", out var messageField) && messageField.ValueKind != JsonValueKind.Null)
                    {
                        message = messageField.ValueKind == JsonValueKind.String
                            ? messageField.GetString()
                            : messageField.GetRawText();
                    }

                    int? code = root.TryGetProperty("code", out var codeField) ? ErrorInfo.Code(codeField) : null;
                    throw new ApiException(ErrorInfo.ApiMessage(method, ErrorInfo.Quote(message), status, code), status, code,
                        raw, null, method, ErrorInfo.Hint(code), ApiErrorKind.Api, message);
                }
            }

            try
            {
                return JsonSerializer.Deserialize<T>(raw, JsonOptions)
                    ?? throw Error(method, $"empty response (HTTP {status})", status, raw, null, ApiErrorKind.InvalidResponse);
            }
            catch (JsonException e)
            {
                throw Error(method, "can not decode the response: " + e.Message, status, raw, e, ApiErrorKind.InvalidResponse);
            }
        }
    }

    /// <summary>The message of a timeout: the shorter of Configuration.Timeout and HttpClient.Timeout (a custom
    /// HttpClient may have its own one) fired first.</summary>
    private string TimedOut()
    {
        var timeout = _config.Timeout;
        if (_http.Timeout > TimeSpan.Zero && (timeout <= TimeSpan.Zero || _http.Timeout < timeout))
        {
            timeout = _http.Timeout;
        }

        return timeout > TimeSpan.Zero
            ? "HTTP request timed out after " + timeout.TotalSeconds.ToString(CultureInfo.InvariantCulture) + " s"
            : "HTTP request timed out";
    }

    private static string Excerpt(string raw)
    {
        return ErrorInfo.Quote(raw);
    }

    private static ApiException Error(string method, string message, int status, string? raw, Exception? inner,
        ApiErrorKind kind)
    {
        return new ApiException(method + ": " + message, status, null, raw, inner, method, null, kind);
    }
}
