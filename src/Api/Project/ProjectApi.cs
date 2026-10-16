using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniOne.Api.Project.Model;
using UniOne.Client;
using UniOne.Common.Model;

namespace UniOne.Api.Project;

/// <summary>
/// Projects allow you to group several independent domains, webhooks, templates, and unsubscription<br/>
/// lists together. It's very useful if you have your own clients and manage their data from a single<br/>
/// UniOne account. Each project receives its own API key, and all API calls with that API key only deal<br/>
/// with the data of that project. This way, you can assign a separate project for each client and be<br/>
/// sure that they won't conflict; e.g. unsubscribing from one project won't affect another.
/// <para/>
/// Projects methods are disabled by default due to antispam measures. To enable them, [contact<br/>
/// support](https://cp.unione.io/en/support).
/// <para/>
/// Please note that sending domains are confirmed and used on per-project basis by default. But it's<br/>
/// possible to allow projects to send emails from domains confirmed for main account - just [ask<br/>
/// support](https://cp.unione.io/en/support) to enable it.
/// </summary>
public class ProjectApi
{
    private readonly ApiClient _client;

    public ProjectApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Creates a new project.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#project-create
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<ProjectCreateResponse> CreateAsync(ProjectCreateRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<ProjectCreateResponse>("/project/create.json", request, cancellationToken);
    }

    /// <summary>
    /// Updates a project.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#project-update
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<ProjectUpdateResponse> UpdateAsync(ProjectUpdateRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<ProjectUpdateResponse>("/project/update.json", request, cancellationToken);
    }

    /// <summary>
    /// Lists projects.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#project-list
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<ProjectListResponse> ListAsync(ProjectListRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<ProjectListResponse>("/project/list.json", request, cancellationToken);
    }

    /// <summary>
    /// Deletes a project.
    /// <para/>
    /// The project will be moved to the "trash bin" before being permanently deleted. You can restore the<br/>
    /// project by contacting support.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#project-delete
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SuccessResponse> DeleteAsync(ProjectDeleteRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SuccessResponse>("/project/delete.json", request, cancellationToken);
    }
}
