using System;
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
    public const string Platform = "dotnet-sdk.2.1.3";

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
    /// <exception cref="ApiException">The API returned an error.</exception>
    public async Task<T> CallAsync<T>(string path, object? body, CancellationToken cancellationToken = default)
    {
        string json;
        try
        {
            json = body == null ? "{}" : JsonSerializer.Serialize(body, body.GetType(), RequestJsonOptions);
        }
        catch (JsonException e)
        {
            throw new ApiException("Can not encode the request: " + e.Message, 0, null, null, e);
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
            throw new ApiException("HTTP request failed: " + e.Message, 0, null, null, e);
        }
        catch (OperationCanceledException e) when (!cancellationToken.IsCancellationRequested)
        {
            throw new ApiException("HTTP request timed out", 0, null, null, e);
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
                throw new ApiException("Can not read the response: " + e.Message, status, null, null, e);
            }
            catch (OperationCanceledException e) when (!cancellationToken.IsCancellationRequested)
            {
                throw new ApiException("HTTP request timed out", status, null, null, e);
            }

            JsonDocument document;
            try
            {
                document = JsonDocument.Parse(raw);
            }
            catch (JsonException e)
            {
                throw new ApiException($"Invalid JSON in response (HTTP {status})", status, null, raw, e);
            }

            using (document)
            {
                var root = document.RootElement;
                if (root.ValueKind != JsonValueKind.Object)
                {
                    throw new ApiException($"Unexpected response (HTTP {status})", status, null, raw);
                }

                var isError = root.TryGetProperty("status", out var statusField)
                    && statusField.ValueKind == JsonValueKind.String
                    && statusField.GetString() == "error";
                if (status >= 400 || isError)
                {
                    var message = root.TryGetProperty("message", out var messageField)
                        && messageField.ValueKind == JsonValueKind.String
                            ? messageField.GetString() ?? "API error"
                            : "API error";
                    int? code = root.TryGetProperty("code", out var codeField)
                        && codeField.ValueKind == JsonValueKind.Number
                        && codeField.TryGetInt32(out var parsed)
                            ? parsed
                            : null;
                    throw new ApiException(message, status, code, raw);
                }
            }

            try
            {
                return JsonSerializer.Deserialize<T>(raw, JsonOptions)
                    ?? throw new ApiException("Empty response", status, null, raw);
            }
            catch (JsonException e)
            {
                throw new ApiException("Can not decode the response: " + e.Message, status, null, raw, e);
            }
        }
    }
}
