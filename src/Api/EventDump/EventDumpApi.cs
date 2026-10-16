using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniOne.Api.EventDump.Model;
using UniOne.Client;
using UniOne.Common.Model;

namespace UniOne.Api.EventDump;

/// <summary>
/// These methods are used to obtain a list of events for a given period of time. The data is stored for<br/>
/// up to 45 days, depending on your tariff, i. e. you may fetch all event data starting from 00:00:00<br/>
/// 31 days ago and up to current time. For custom tarifs with big sending volumes event dumps are<br/>
/// available only for the last 7 days.
/// </summary>
public class EventDumpApi
{
    private readonly ApiClient _client;

    public EventDumpApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// This asynchronous method initiates the preparation of data dump in CSV format. The number of dump<br/>
    /// files created or stored is limited to 10; to order more files, you have to delete one of the<br/>
    /// previous ones or wait until they will be deleted automatically. A newly created dump file is kept<br/>
    /// for 8 hours. If the request period is more than a day or there are too many events then several<br/>
    /// files will be generated and each file will contain no more than 100,000 events. File contains<br/>
    /// columns with the following information - 'event_time', 'job_id', 'from_email', 'email', 'status',<br/>
    /// 'delivery_status', 'metadata', 'destination_response', 'user_agent', 'url', 'ip', 'tags' and<br/>
    /// 'project_id' (for accounts with projects enabled). You can change the fields and include 'subject'<br/>
    /// field using 'dump_fileds' parameter.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#event-dump-create
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<EventDumpCreateResponse> CreateAsync(EventDumpCreateRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<EventDumpCreateResponse>("/event-dump/create.json", request, cancellationToken);
    }

    /// <summary>
    /// Returns dump properties for a given identifier
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#event-dump-get
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<EventDumpGetResponse> GetAsync(EventDumpGetRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<EventDumpGetResponse>("/event-dump/get.json", request, cancellationToken);
    }

    /// <summary>
    /// This method does not require any parameters; returns the full list of existing dumps.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#event-dump-list
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<EventDumpListResponse> ListAsync(CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<EventDumpListResponse>("/event-dump/list.json", null, cancellationToken);
    }

    /// <summary>
    /// Removes dump from queue or storage.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#event-dump-delete
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SuccessResponse> DeleteAsync(EventDumpDeleteRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SuccessResponse>("/event-dump/delete.json", request, cancellationToken);
    }
}
