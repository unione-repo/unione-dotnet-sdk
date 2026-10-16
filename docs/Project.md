# Project

[Back to the list of sections](../README.md)

Projects allow you to group several independent domains, webhooks, templates, and unsubscription lists together. It's very useful if you have your own clients and manage their data from a single UniOne account. Each project receives its own API key, and all API calls with that API key only deal with the data of that project. This way, you can assign a separate project for each client and be sure that they won't conflict; e.g. unsubscribing from one project won't affect another.

Projects methods are disabled by default due to antispam measures. To enable them, [contact support](https://cp.unione.io/en/support).

Please note that sending domains are confirmed and used on per-project basis by default. But it's possible to allow projects to send emails from domains confirmed for main account - just [ask support](https://cp.unione.io/en/support) to enable it.

`UniOne.Api.Project.ProjectApi`, `client.Project`

| Method | Endpoint | Description |
|---|---|---|
| [`CreateAsync`](#createasync) | `POST /project/create.json` | Creates a new project. |
| [`UpdateAsync`](#updateasync) | `POST /project/update.json` | Updates a project. |
| [`ListAsync`](#listasync) | `POST /project/list.json` | Lists projects. |
| [`DeleteAsync`](#deleteasync) | `POST /project/delete.json` | Deletes a project. |

## CreateAsync

`POST /project/create.json` · [Documentation](https://docs.unione.io/en/web-api-ref#project-create)

Creates a new project.

### Request: `ProjectCreateRequest`

| Field | Type | Required | Description |
|---|---|---|---|
| `Project` | `ProjectObject` | yes | Object with project properties. |

### Response: `ProjectCreateResponse`

| Field | Type | Description |
|---|---|---|
| `Status` | `string` | "success" string (`success`) |
| `ProjectId` | `string` | Unqiue project identifier, ASCII string up to 36 characters long. |
| `ProjectApiKey` | `string` | API key of the project. You can use it instead of the user API key parameter in all methods except project/* methods. |

### Example

```csharp
using UniOne;
using UniOne.Api.Project.Model;

var client = new UniOneClient("YOUR_API_KEY");

var response = await client.Project.CreateAsync(new ProjectCreateRequest
{
    Project = new ProjectObject
    {
        Name = "Project 1A",
        Country = "ES",
        BackendDomainId = 1234,
        UnsubscribePageId = 1,
    },
});
Console.WriteLine(response.ProjectId);
```

## UpdateAsync

`POST /project/update.json` · [Documentation](https://docs.unione.io/en/web-api-ref#project-update)

Updates a project.

### Request: `ProjectUpdateRequest`

| Field | Type | Required | Description |
|---|---|---|---|
| `ProjectId` | `string` |  | Unique project identifier. You should identify project to update either with project_id or with project_api_key field. But we recommend usage of project_id as a more secure option. |
| `ProjectApiKey` | `string` |  | Optional project API key. You should identify project to update either with project_id or with project_api_key field. But we recommend usage of project_id as a more secure option. |
| `Project` | `ProjectUpdateObject` | yes | Object with project properties. |

### Response: `ProjectUpdateResponse`

| Field | Type | Description |
|---|---|---|
| `Status` | `string` | "success" string (`success`) |
| `ProjectApiKey` | `string` | API key of the project. Returned only if the project to update was identified by project_api_key. Not returned in case of updating by project_id. |

### Example

```csharp
using UniOne;
using UniOne.Api.Project.Model;

var client = new UniOneClient("YOUR_API_KEY");

var response = await client.Project.UpdateAsync(new ProjectUpdateRequest
{
    ProjectId = "1234567890",
    ProjectApiKey = "your api_key",
    Project = new ProjectUpdateObject
    {
        Name = "Project 1A",
        Country = "ES",
        BackendDomainId = 1234,
        UnsubscribePageId = 1,
    },
});
Console.WriteLine(response.ProjectApiKey);
```

## ListAsync

`POST /project/list.json` · [Documentation](https://docs.unione.io/en/web-api-ref#project-list)

Lists projects.

### Request: `ProjectListRequest`

| Field | Type | Required | Description |
|---|---|---|---|
| `ProjectId` | `string` |  | Optional possibility to specify project_id to include only this project to list. |
| `ProjectApiKey` | `string` |  | Optional possibility to specify project_api_key to include only this project to list. |

### Response: `ProjectListResponse`

| Field | Type | Description |
|---|---|---|
| `Status` | `string` | "success" string (`success`) |
| `Projects` | `List<ProjectListObject>` | Array of objects where each object represents one project. |

### Example

```csharp
using UniOne;
using UniOne.Api.Project.Model;

var client = new UniOneClient("YOUR_API_KEY");

var response = await client.Project.ListAsync(new ProjectListRequest
{
    ProjectId = "1234567890",
    ProjectApiKey = "your api_key",
});
Console.WriteLine(response.Projects);
```

## DeleteAsync

`POST /project/delete.json` · [Documentation](https://docs.unione.io/en/web-api-ref#project-delete)

Deletes a project.

The project will be moved to the "trash bin" before being permanently deleted. You can restore the project by contacting support.

### Request: `ProjectDeleteRequest`

| Field | Type | Required | Description |
|---|---|---|---|
| `ProjectId` | `string` |  | Unique project identifier. You should identify project to delete either with project_id or with project_api_key field. We recommend usage of project_id as a more secure option. |
| `ProjectApiKey` | `string` |  | Optional project API key. You should identify project to delete either with project_id or with project_api_key field. We recommend usage of project_id as a more secure option. |

### Response: `SuccessResponse`

| Field | Type | Description |
|---|---|---|
| `Status` | `string` | "success" string (`success`) |

### Example

```csharp
using UniOne;
using UniOne.Api.Project.Model;

var client = new UniOneClient("YOUR_API_KEY");

var response = await client.Project.DeleteAsync(new ProjectDeleteRequest
{
    ProjectId = "1234567890",
    ProjectApiKey = "your api_key",
});
Console.WriteLine(response);
```
