# Template

[Back to the list of sections](../README.md)

You have the option of storing a template on the UniOne side and then using it by id, which is highly recommended for sending repetitive messages or high volume traffic. UniOne supports two different [template engines](https://docs.unione.io/en/template-engines).

`UniOne.Api.Template.TemplateApi`, `client.Template`

| Method | Endpoint | Description |
|---|---|---|
| [`SetAsync`](#setasync) | `POST /template/set.json` | Creates or updates an email template. |
| [`GetAsync`](#getasync) | `POST /template/get.json` | Gets template properties by it's id. |
| [`ListAsync`](#listasync) | `POST /template/list.json` | Returns the list of all or some templates of the account. |
| [`DeleteAsync`](#deleteasync) | `POST /template/delete.json` | Deletes a template by id. |

## SetAsync

`POST /template/set.json` · [Documentation](https://docs.unione.io/en/web-api-ref#template-set)

Creates or updates an email template.

Restrictions:
* The maximum request size is 10 MB.
* The number of templates is limited to 10000.

### Request: `TemplateSetRequest`

| Field | Type | Required | Description |
|---|---|---|---|
| `Template` | `TemplateObject` | yes | Object with all the template properties. |

### Response: `TemplateSetResponse`

| Field | Type | Description |
|---|---|---|
| `Status` | `string` | "success" string (`success`) |
| `Template` | `TemplateObject` | Object with all the template properties. |

### Example

```csharp
using UniOne;
using UniOne.Api.Template.Model;
using UniOne.Common.Model;

var client = new UniOneClient("YOUR_API_KEY");

var response = await client.Template.SetAsync(new TemplateSetRequest
{
    Template = new TemplateObject
    {
        Name = "name",
        EditorType = EditorType.Html,
        TemplateEngine = TemplateEngine.Velocity,
        Subject = "UniOne test email ",
        FromEmail = "user@example.com",
        FromName = "John Smith",
        ReplyTo = "email@example.com",
        ReplyToName = "John Smith",
    },
});
Console.WriteLine(response.Template);
```

## GetAsync

`POST /template/get.json` · [Documentation](https://docs.unione.io/en/web-api-ref#template-get)

Gets template properties by it's id.

### Request: `TemplateGetRequest`

| Field | Type | Required | Description |
|---|---|---|---|
| `Id` | `string` | yes | Template id. |

### Response: `TemplateGetResponse`

| Field | Type | Description |
|---|---|---|
| `Status` | `string` | "success" string (`success`) |
| `Template` | `TemplateObject` | Object with all the template properties. |

### Example

```csharp
using UniOne;
using UniOne.Api.Template.Model;

var client = new UniOneClient("YOUR_API_KEY");

var response = await client.Template.GetAsync(new TemplateGetRequest
{
    Id = "00000000-0000-0000-0000-000000000000",
});
Console.WriteLine(response.Template);
```

## ListAsync

`POST /template/list.json` · [Documentation](https://docs.unione.io/en/web-api-ref#template-list)

Returns the list of all or some templates of the account.

You can iterate thru large list using "offset" and "limit" parameters. When less than "limit" templates are returned then the end of list is reached.

### Request: `TemplateListRequest`

| Field | Type | Required | Description |
|---|---|---|---|
| `Limit` | `int` |  | Limits the number of templates returned, default is 50. |
| `Offset` | `int` |  | Index of the first template in the list, starting with 0. |

### Response: `TemplateListResponse`

| Field | Type | Description |
|---|---|---|
| `Status` | `string` | "success" string (`success`) |
| `Templates` | `List<TemplateListObject>` | Array of template objects. |

### Example

```csharp
using UniOne;
using UniOne.Api.Template.Model;

var client = new UniOneClient("YOUR_API_KEY");

var response = await client.Template.ListAsync(new TemplateListRequest());
Console.WriteLine(response.Templates);
```

## DeleteAsync

`POST /template/delete.json` · [Documentation](https://docs.unione.io/en/web-api-ref#template-delete)

Deletes a template by id.

### Request: `TemplateDeleteRequest`

| Field | Type | Required | Description |
|---|---|---|---|
| `Id` | `string` | yes | Template id. |

### Response: `SuccessResponse`

| Field | Type | Description |
|---|---|---|
| `Status` | `string` | "success" string (`success`) |

### Example

```csharp
using UniOne;
using UniOne.Api.Template.Model;

var client = new UniOneClient("YOUR_API_KEY");

var response = await client.Template.DeleteAsync(new TemplateDeleteRequest
{
    Id = "00000000-0000-0000-0000-000000000000",
});
Console.WriteLine(response);
```
