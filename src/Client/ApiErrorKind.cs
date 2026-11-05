namespace UniOne.Client;

/// <summary>
/// What failed in an <see cref="ApiException"/>.
/// </summary>
public enum ApiErrorKind
{
    /// <summary>The API returned an error (<see cref="ApiException.ErrorCode"/>, <see cref="ApiException.Hint"/>).</summary>
    Api,

    /// <summary>The request could not be sent or the response could not be received.</summary>
    Network,

    /// <summary>The request timed out.</summary>
    Timeout,

    /// <summary>The response is not the JSON of the API (a proxy page, a broken body).</summary>
    InvalidResponse,

    /// <summary>Found before sending (an empty API key, a required field missing): no request was sent.</summary>
    InvalidRequest,
}
