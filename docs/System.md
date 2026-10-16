# System

[Back to the list of sections](../README.md)

System operations.

`UniOne.Api.System.SystemApi`, `client.System`

| Method | Endpoint | Description |
|---|---|---|
| [`PingAsync`](#pingasync) | `POST /system/ping.json` | Checks API key validity and returns user ID. |
| [`InfoAsync`](#infoasync) | `POST /system/info.json` | Gets user or project info by API key. |

## PingAsync

`POST /system/ping.json` · [Documentation](https://docs.unione.io/en/web-api-ref#system-ping)

Checks API key validity and returns user ID.

### Request

The method has no parameters.

### Response: `SystemPingResponse`

| Field | Type | Description |
|---|---|---|
| `Status` | `string` | "success" string (`success`) |
| `UserId` | `int` | Unique user identifier. |

### Example

```csharp
using UniOne;

var client = new UniOneClient("YOUR_API_KEY");

var response = await client.System.PingAsync();
Console.WriteLine(response.UserId);
```

## InfoAsync

`POST /system/info.json` · [Documentation](https://docs.unione.io/en/web-api-ref#system-info)

Gets user or project info by API key.

### Request

The method has no parameters.

### Response: `SystemInfoResponse`

| Field | Type | Description |
|---|---|---|
| `Status` | `string` | "success" string (`success`) |
| `UserId` | `int` | Unique user identifier. |
| `Email` | `string` | Email of the user. |
| `ProjectId` | `string` | Unqiue project identifier, ASCII string up to 36 characters long. Present only if the API key used for request is the project API key. |
| `ProjectName` | `string` | Project name, unique for user account. Present only if the API key used for request is the project API key. |
| `ProjectAccounting` | `ProjectAccountingObject` | Object with email counter properties. Present only if the API key used for request is the user API key. |
| `Accounting` | `SystemInfoResponseAccounting` | Object with accounting period properties. Present only if the API key used for request is the user API key. |

### Example

```csharp
using UniOne;

var client = new UniOneClient("YOUR_API_KEY");

var response = await client.System.InfoAsync();
Console.WriteLine(response.UserId);
```
