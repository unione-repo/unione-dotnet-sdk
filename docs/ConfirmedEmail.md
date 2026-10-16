# ConfirmedEmail

[Back to the list of sections](../README.md)

Manage email addresses confirmed for sandbox-domain sending. These methods are available only with a main account API key.

`UniOne.Api.ConfirmedEmail.ConfirmedEmailApi`, `client.ConfirmedEmail`

| Method | Endpoint | Description |
|---|---|---|
| [`CreateAsync`](#createasync) | `POST /confirmed-email/create.json` | Adds an email address and sends a confirmation link. |
| [`ListAsync`](#listasync) | `POST /confirmed-email/list.json` | Lists confirmed and unconfirmed email addresses for the account. |
| [`DeleteAsync`](#deleteasync) | `POST /confirmed-email/delete.json` | Deletes an email address from the account's confirmed email list. |

## CreateAsync

`POST /confirmed-email/create.json` · [Documentation](https://docs.unione.io/en/web-api-ref#confirmed-email-create)

Adds an email address and sends a confirmation link.

### Request: `ConfirmedEmailRequest`

| Field | Type | Required | Description |
|---|---|---|---|
| `Email` | `string` | yes | Email address to add or delete. |

### Response: `ConfirmedEmailCreateResponse`

| Field | Type | Description |
|---|---|---|
| `Status` | `string` | "success" string (`success`) |
| `ConfirmedEmail` | `ConfirmedEmail` |  |

### Example

```csharp
using UniOne;
using UniOne.Api.ConfirmedEmail.Model;

var client = new UniOneClient("YOUR_API_KEY");

var response = await client.ConfirmedEmail.CreateAsync(new ConfirmedEmailRequest
{
    Email = "dev@example.com",
});
Console.WriteLine(response.ConfirmedEmail);
```

## ListAsync

`POST /confirmed-email/list.json` · [Documentation](https://docs.unione.io/en/web-api-ref#confirmed-email-list)

Lists confirmed and unconfirmed email addresses for the account.

### Request

The method has no parameters.

### Response: `ConfirmedEmailListResponse`

| Field | Type | Description |
|---|---|---|
| `Status` | `string` | "success" string (`success`) |
| `ConfirmedEmails` | `List<ConfirmedEmail>` |  |

### Example

```csharp
using UniOne;

var client = new UniOneClient("YOUR_API_KEY");

var response = await client.ConfirmedEmail.ListAsync();
Console.WriteLine(response.ConfirmedEmails);
```

## DeleteAsync

`POST /confirmed-email/delete.json` · [Documentation](https://docs.unione.io/en/web-api-ref#confirmed-email-delete)

Deletes an email address from the account's confirmed email list.

### Request: `ConfirmedEmailRequest`

| Field | Type | Required | Description |
|---|---|---|---|
| `Email` | `string` | yes | Email address to add or delete. |

### Response: `ConfirmedEmailDeleteResponse`

| Field | Type | Description |
|---|---|---|
| `Status` | `string` | "success" string (`success`) |
| `Message` | `string` |  |

### Example

```csharp
using UniOne;
using UniOne.Api.ConfirmedEmail.Model;

var client = new UniOneClient("YOUR_API_KEY");

var response = await client.ConfirmedEmail.DeleteAsync(new ConfirmedEmailRequest
{
    Email = "dev@example.com",
});
Console.WriteLine(response.Message);
```
