using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniOne.Api.System.Model;
using UniOne.Client;

namespace UniOne.Api.System;

/// <summary>
/// System operations.
/// </summary>
public class SystemApi
{
    private readonly ApiClient _client;

    public SystemApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Checks API key validity and returns user ID.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#system-ping
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SystemPingResponse> PingAsync(CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SystemPingResponse>("/system/ping.json", null, cancellationToken);
    }

    /// <summary>
    /// Gets user or project info by API key.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#system-info
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SystemInfoResponse> InfoAsync(CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SystemInfoResponse>("/system/info.json", null, cancellationToken);
    }
}
