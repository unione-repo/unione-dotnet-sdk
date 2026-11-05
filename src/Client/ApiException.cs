using System;

namespace UniOne.Client;

/// <summary>
/// Thrown when the API returns an error, the request fails or is invalid, or the response can not be processed.
/// The message names the API method and, for an API error, the code, the HTTP status and how to fix it:
/// "email/send: Unknown recipient (API error 204, HTTP 400). Fix: ... See ...#api-errors".
/// </summary>
public class ApiException : Exception
{
    /// <summary>An error of the API (<see cref="ApiErrorKind.Api"/>) of an unknown method.</summary>
    public ApiException(string message, int httpStatus, int? errorCode, string? responseBody, Exception? inner = null)
        : this(message, httpStatus, errorCode, responseBody, inner, "", null, ApiErrorKind.Api)
    {
    }

    /// <summary>An error of the given kind of a request of the API method.</summary>
    public ApiException(string message, int httpStatus, int? errorCode, string? responseBody, Exception? inner,
        string method, string? hint, ApiErrorKind kind)
        : this(message, httpStatus, errorCode, responseBody, inner, method, hint, kind, null)
    {
    }

    /// <summary>An error of the given kind of a request of the API method with the message text of the API.</summary>
    public ApiException(string message, int httpStatus, int? errorCode, string? responseBody, Exception? inner,
        string method, string? hint, ApiErrorKind kind, string? apiMessage)
        : base(message, inner)
    {
        HttpStatus = httpStatus;
        ErrorCode = errorCode;
        ResponseBody = responseBody;
        Method = method;
        Hint = hint;
        Kind = kind;
        ApiMessage = apiMessage;
    }

    /// <summary>HTTP status of the response, 0 when the request was not sent.</summary>
    public int HttpStatus { get; }

    /// <summary>API error code (the "code" field of the error response).</summary>
    public int? ErrorCode { get; }

    /// <summary>Raw response body.</summary>
    public string? ResponseBody { get; }

    /// <summary>API method of the request, e.g. "email/send"; empty when unknown.</summary>
    public string Method { get; }

    /// <summary>How to fix the API error (the errors table of the docs), null when unknown.</summary>
    public string? Hint { get; }

    /// <summary>What failed.</summary>
    public ApiErrorKind Kind { get; }

    /// <summary>
    /// The message of the API as is (the "message" field of the error response); Message adds the method, the code, the
    /// HTTP status and the hint and cuts a long text. Null when there was no API response or it had no message.
    /// </summary>
    public string? ApiMessage { get; }

    /// <summary>A repeat of the request may succeed: a network error, a timeout, HTTP 429 or 5xx.</summary>
    public bool IsRetryable => Kind switch
    {
        ApiErrorKind.Network or ApiErrorKind.Timeout => true,
        ApiErrorKind.Api or ApiErrorKind.InvalidResponse => HttpStatus == 429 || HttpStatus >= 500,
        _ => false,
    };
}
