# Tag

[Back to the list of sections](../README.md)

This family of methods is used to manage user-defined tags. Tags are used to categorize letters based on your own criteria, e. g. to create a distinction between marketing and transactional letters or group together all notifications for a particular order type. UniOne allows you to assign tags to all outgoing emails, and the tags you’ve set may then be used to filter and analyze delivery statistics.

`UniOne.Api.Tag.TagApi`, `client.Tag`

| Method | Endpoint | Description |
|---|---|---|
| [`ListAsync`](#listasync) | `POST /tag/list.json` | This method returns the full list of user-defined tags. Does not receive any parameters. |
| [`DeleteAsync`](#deleteasync) | `POST /tag/delete.json` | Removes tag. |

## ListAsync

`POST /tag/list.json` · [Documentation](https://docs.unione.io/en/web-api-ref#tag-list)

This method returns the full list of user-defined tags. Does not receive any parameters.

### Request

The method has no parameters.

### Response: `TagListResponse`

| Field | Type | Description |
|---|---|---|
| `Status` | `string` | "success" string (`success`) |
| `Tags` | `List<TagsResponseObject>` | An array of objects containing data for user-defined tags. |

### Example

```csharp
using UniOne;

var client = new UniOneClient("YOUR_API_KEY");

var response = await client.Tag.ListAsync();
Console.WriteLine(response.Tags);
```

## DeleteAsync

`POST /tag/delete.json` · [Documentation](https://docs.unione.io/en/web-api-ref#tag-delete)

Removes tag.

### Request: `TagDeleteRequest`

| Field | Type | Required | Description |
|---|---|---|---|
| `TagId` | `int` | yes | Unique tag id. |

### Response: `SuccessResponse`

| Field | Type | Description |
|---|---|---|
| `Status` | `string` | "success" string (`success`) |

### Example

```csharp
using UniOne;
using UniOne.Api.Tag.Model;

var client = new UniOneClient("YOUR_API_KEY");

var response = await client.Tag.DeleteAsync(new TagDeleteRequest
{
    TagId = 54,
});
Console.WriteLine(response);
```
