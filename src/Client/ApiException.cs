using System;

namespace UniOne.Client;

/// <summary>
/// Thrown when the API returns an error or the response can not be processed.
/// </summary>
public class ApiException : Exception
{
    public ApiException(string message, int httpStatus, int? errorCode, string? responseBody, Exception? inner = null)
        : base(message, inner)
    {
        HttpStatus = httpStatus;
        ErrorCode = errorCode;
        ResponseBody = responseBody;
    }

    /// <summary>HTTP status of the response, 0 when the request was not sent.</summary>
    public int HttpStatus { get; }

    /// <summary>API error code (the "code" field of the error response).</summary>
    public int? ErrorCode { get; }

    /// <summary>Raw response body.</summary>
    public string? ResponseBody { get; }
}
