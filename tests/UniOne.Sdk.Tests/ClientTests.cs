using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using UniOne;
using UniOne.Api.Email.Model;
using UniOne.Client;
using UniOne.Common.Model;
using Xunit;
using Recipient = UniOne.Api.Email.Model.MessageObjectRecipientsItem;

namespace UniOne.Sdk.Tests;

/// <summary>
/// Tests of the client: every request goes to <see cref="FakeHandler"/> through the HttpClient of the client, nothing
/// leaves the process (the base URL is a closed local port that is never connected).
/// </summary>
public class ClientTests
{
    private const string ApiKey = "test-key";
    private const string BaseUrl = "http://127.0.0.1:9/api";
    private const string Method = "email/send";
    private const string SendResponse = """
        {
          "status": "success",
          "job_id": "1ZymBc-00041N-9X",
          "emails": ["alex@example.com"],
          "failed_emails": {"bad@example.com": "invalid"}
        }
        """;

    private static UniOneClient Client(
        FakeHandler handler, string apiKey = ApiKey, Action<Configuration>? configure = null)
    {
        var config = new Configuration(apiKey) { BaseUrl = BaseUrl };
        configure?.Invoke(config);
        return new UniOneClient(config, new HttpClient(handler));
    }

    private static EmailSendRequest Message()
    {
        return new EmailSendRequest
        {
            Message = new MessageObject
            {
                Recipients = [Recipient.FromAddress("Alex <alex@example.com>")],
                FromEmail = "shop@example.com",
                Subject = "Your order",
                Body = BodyObject.FromHtml("<p>Hello, {{to_name}}!</p>"),
            },
        };
    }

    private static Task<EmailSendResponse> Send(UniOneClient client)
    {
        return client.Email.SendAsync(Message());
    }

    private static string ApiError(string message, string code)
    {
        return "{\"status\":\"error\",\"message\":" + JsonSerializer.Serialize(message) + ",\"code\":" + code + "}";
    }

    [Fact]
    public async Task SendPostsTheMessageAndParsesTheResponse()
    {
        var handler = FakeHandler.Json(200, SendResponse);

        var response = await Send(Client(handler));

        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal(BaseUrl + "/email/send.json", request.Url.GetLeftPart(UriPartial.Path));
        Assert.Equal("?platform=" + ApiClient.Platform, Uri.UnescapeDataString(request.Url.Query));
        Assert.Equal(ApiKey, request.Header("X-API-KEY"));
        Assert.Equal("application/json", request.ContentType);
        using (var body = JsonDocument.Parse(request.Body))
        {
            var message = body.RootElement.GetProperty("message");
            var recipient = message.GetProperty("recipients")[0];
            Assert.Equal("alex@example.com", recipient.GetProperty("email").GetString());
            Assert.Equal("Alex", recipient.GetProperty("substitutions").GetProperty("to_name").GetString());
            Assert.Equal("shop@example.com", message.GetProperty("from_email").GetString());
            Assert.Equal("Your order", message.GetProperty("subject").GetString());
            Assert.Equal("<p>Hello, {{to_name}}!</p>", message.GetProperty("body").GetProperty("html").GetString());
            // null fields are not sent
            Assert.False(message.GetProperty("body").TryGetProperty("plaintext", out _));
        }

        Assert.Equal("1ZymBc-00041N-9X", response.JobId);
        Assert.Equal(new[] { "alex@example.com" }, response.Emails);
        Assert.Equal("invalid", response.FailedEmails!["bad@example.com"]);
    }

    [Fact]
    public async Task PlatformOfAWrapperGoesBeforeTheSdkOne()
    {
        var handler = FakeHandler.Json(200, SendResponse);

        await Send(Client(handler, configure: config => config.Platform = "framework.1.0.0"));

        var request = Assert.Single(handler.Requests);
        Assert.Equal("?platform=framework.1.0.0," + ApiClient.Platform, Uri.UnescapeDataString(request.Url.Query));
    }

    [Fact]
    public async Task InfoCallsItsPathAndParsesTheResponse()
    {
        var handler = FakeHandler.Json(200, """{"status":"success","user_id":1234,"email":"user@example.com"}""");

        var response = await Client(handler).System.InfoAsync();

        var request = Assert.Single(handler.Requests);
        Assert.Equal(BaseUrl + "/system/info.json", request.Url.GetLeftPart(UriPartial.Path));
        Assert.Equal("{}", request.Body);
        Assert.Equal(1234, response.UserId);
        Assert.Equal("user@example.com", response.Email);
    }

    [Fact]
    public async Task ApiErrorHasTheCodeTheStatusAndTheHint()
    {
        var e = await Assert.ThrowsAsync<ApiException>(
            () => Send(Client(FakeHandler.Json(400, ApiError("Unknown recipient", "204")))));

        Assert.Equal(ApiErrorKind.Api, e.Kind);
        Assert.False(e.IsRetryable);
        Assert.Equal(204, e.ErrorCode);
        Assert.Equal(400, e.HttpStatus);
        Assert.Equal(Method, e.Method);
        Assert.False(string.IsNullOrEmpty(e.Hint));
        Assert.Equal("Unknown recipient", e.ApiMessage);
        Assert.StartsWith(Method + ": ", e.Message);
        Assert.Contains("(API error 204, HTTP 400)", e.Message);
        Assert.Contains("Fix: ", e.Message);
    }

    [Theory]
    [InlineData(429)]
    [InlineData(503)]
    public async Task RateLimitAndServerErrorsAreRetryable(int status)
    {
        var e = await Assert.ThrowsAsync<ApiException>(
            () => Send(Client(FakeHandler.Json(status, ApiError("Try again later", "null")))));

        Assert.Equal(ApiErrorKind.Api, e.Kind);
        Assert.Equal(status, e.HttpStatus);
        Assert.True(e.IsRetryable);
    }

    [Fact]
    public async Task ProxyPageIsAnInvalidResponseAndRetryable()
    {
        const string page = "<html><body>502 Bad Gateway</body></html>";
        var e = await Assert.ThrowsAsync<ApiException>(
            () => Send(Client(FakeHandler.Text(502, page, "text/html"))));

        Assert.Equal(ApiErrorKind.InvalidResponse, e.Kind);
        Assert.Equal(502, e.HttpStatus);
        Assert.Equal(page, e.ResponseBody);
        Assert.True(e.IsRetryable);
    }

    [Fact]
    public async Task NonJsonSuccessIsAnInvalidResponseAndNotRetryable()
    {
        var e = await Assert.ThrowsAsync<ApiException>(
            () => Send(Client(FakeHandler.Text(200, "OK", "text/plain"))));

        Assert.Equal(ApiErrorKind.InvalidResponse, e.Kind);
        Assert.Equal(200, e.HttpStatus);
        Assert.False(e.IsRetryable);
    }

    [Fact]
    public async Task TransportFailureIsANetworkErrorAndRetryable()
    {
        var e = await Assert.ThrowsAsync<ApiException>(
            () => Send(Client(FakeHandler.Throws(new HttpRequestException("Connection refused")))));

        Assert.Equal(ApiErrorKind.Network, e.Kind);
        Assert.Equal(0, e.HttpStatus);
        Assert.Equal(Method, e.Method);
        Assert.IsType<HttpRequestException>(e.InnerException);
        Assert.True(e.IsRetryable);
    }

    [Fact]
    public async Task TimeoutIsRetryable()
    {
        var handler = FakeHandler.Hangs();

        var e = await Assert.ThrowsAsync<ApiException>(
            () => Send(Client(handler, configure: config => config.Timeout = TimeSpan.FromMilliseconds(100))));

        Assert.Equal(ApiErrorKind.Timeout, e.Kind);
        Assert.True(e.IsRetryable);
        Assert.Single(handler.Requests);
    }

    [Fact]
    public async Task CancellationIsNotAnApiError()
    {
        using var cancel = new CancellationTokenSource(TimeSpan.FromMilliseconds(100));

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => Client(FakeHandler.Hangs()).Email.SendAsync(Message(), cancel.Token));
    }

    [Fact]
    public async Task EmptyApiKeyIsRejectedBeforeSending()
    {
        var handler = FakeHandler.Json(200, SendResponse);

        var e = await Assert.ThrowsAsync<ApiException>(() => Send(Client(handler, apiKey: "")));

        Assert.Equal(ApiErrorKind.InvalidRequest, e.Kind);
        Assert.Contains("API key", e.Message);
        Assert.False(e.IsRetryable);
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task MissingRecipientsAreRejectedBeforeSending()
    {
        await AssertInvalid(new EmailSendRequest
        {
            Message = new MessageObject
            {
                FromEmail = "shop@example.com",
                Subject = "Your order",
                Body = BodyObject.FromHtml("<p>Hello, {{to_name}}!</p>"),
            },
        }, "message.recipients is required");
    }

    [Fact]
    public async Task EmptyRecipientListIsRejectedBeforeSending()
    {
        await AssertInvalid(new EmailSendRequest
        {
            Message = new MessageObject
            {
                Recipients = [],
                FromEmail = "shop@example.com",
                Subject = "Your order",
                Body = BodyObject.FromHtml("<p>Hello, {{to_name}}!</p>"),
            },
        }, "message.recipients is required");
    }

    [Fact]
    public async Task RecipientWithoutEmailIsRejectedBeforeSending()
    {
        await AssertInvalid(new EmailSendRequest
        {
            Message = new MessageObject
            {
                Recipients = [Recipient.FromAddress("")],
                FromEmail = "shop@example.com",
                Subject = "Your order",
                Body = BodyObject.FromHtml("<p>Hello, {{to_name}}!</p>"),
            },
        }, "message.recipients[0].email is required");
    }

    private static async Task AssertInvalid(EmailSendRequest request, string problem)
    {
        var handler = FakeHandler.Json(200, SendResponse);

        var e = await Assert.ThrowsAsync<ApiException>(() => Client(handler).Email.SendAsync(request));

        Assert.Equal(ApiErrorKind.InvalidRequest, e.Kind);
        Assert.Equal(Method + ": " + problem, e.Message);
        Assert.False(e.IsRetryable);
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task LongApiMessageIsCutInTheMessageOnly()
    {
        var text = new string('x', 600);

        var e = await Assert.ThrowsAsync<ApiException>(
            () => Send(Client(FakeHandler.Json(400, ApiError(text, "204")))));

        Assert.Equal(text, e.ApiMessage);
        Assert.Contains(new string('x', 500) + "... (600 chars)", e.Message);
        Assert.DoesNotContain(new string('x', 501), e.Message);
    }

    [Theory]
    [InlineData("\"204\"", 204)]
    [InlineData("\"abc\"", null)]
    public async Task ErrorCodeMayBeAString(string code, int? expected)
    {
        var e = await Assert.ThrowsAsync<ApiException>(
            () => Send(Client(FakeHandler.Json(400, ApiError("Bad request", code)))));

        Assert.Equal(expected, e.ErrorCode);
    }

    [Fact]
    public async Task OwnErrorOfACustomHandlerIsKept()
    {
        var own = new ApiException("custom: no route", 0, null, null, null, "custom", null, ApiErrorKind.Network);

        var e = await Assert.ThrowsAsync<ApiException>(() => Send(Client(FakeHandler.Throws(own))));

        Assert.Same(own, e);
        Assert.Equal("custom", e.Method);
    }
}
