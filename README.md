# UniOne .NET (C#) SDK 2.1.3

Web API 1.90

Official .NET (C#) library for the [UniOne](https://unione.io) transactional email API.

Quickstart guide: [Send emails with .NET (C#)](https://docs.unione.io/en/send-with-dotnet) · Docs for LLMs: [llms.txt](https://docs.unione.io/llms.txt) · [API reference](https://docs.unione.io/en/web-api-ref)

## Install

```bash
dotnet add package UniOne
```

Requirements: .NET 8. No third-party dependencies.

For .NET Standard 2.0 use the 1.x line (branch `1.x`), the same API.

## Get an API key

1. Sign up at [cp.unione.io](https://cp.unione.io/en/user/registration/).
2. Copy your API key from [Settings → Security → API](https://cp.unione.io/en/settings/security/api).
3. Put it in an environment variable:

```bash
export UNIONE_API_KEY="your-api-key"
```

> **Server-side only.** Use this SDK in backend code: API routes, server functions, workers.
> Never ship the API key to a browser or a mobile app, and never commit it.

## Send your first email — no DNS setup

Every account can send test email right away from a **sandbox domain**: pre-verified, DKIM and SPF already set.
It delivers only to addresses you confirm.

<!-- checked examples -->

**1. Get your sandbox domain** (`domain/get-sandbox-domain`). The call creates the domain on first use and returns the same one afterwards.

```csharp
using UniOne;

var client = new UniOneClient(System.Environment.GetEnvironmentVariable("UNIONE_API_KEY") ?? throw new System.InvalidOperationException("UNIONE_API_KEY is not set"));

var response = await client.Domain.GetSandboxDomainAsync();
Console.WriteLine(response.Domain);
```

**2. Confirm your inbox** (`confirmed-email/create`). UniOne sends a confirmation email; click the link in it within 48 hours.

```csharp
using UniOne;
using UniOne.Api.ConfirmedEmail.Model;

var client = new UniOneClient(System.Environment.GetEnvironmentVariable("UNIONE_API_KEY") ?? throw new System.InvalidOperationException("UNIONE_API_KEY is not set"));

var response = await client.ConfirmedEmail.CreateAsync(new ConfirmedEmailRequest
{
    Email = "you@example.com",
});
Console.WriteLine(response.ConfirmedEmail);
```

Check the status with `confirmed-email/list` — your address should show `"status": "confirmed"`.

**3. Send the email** (`email/send`) from any address on the sandbox domain to the confirmed address.

```csharp
using System.Collections.Generic;
using UniOne;
using UniOne.Api.Email.Model;
using UniOne.Common.Model;

var client = new UniOneClient(System.Environment.GetEnvironmentVariable("UNIONE_API_KEY") ?? throw new System.InvalidOperationException("UNIONE_API_KEY is not set"));

var response = await client.Email.SendAsync(new EmailSendRequest
{
    Message = new MessageObject
    {
        Recipients = new List<MessageObjectRecipientsItem>
        {
            new MessageObjectRecipientsItem
            {
                Email = "you@example.com",
            },
        },
        Body = new BodyObject
        {
            Html = "<p>It works!</p>",
        },
        Subject = "Hello from UniOne",
        FromEmail = "noreply@<sandbox-domain>",
    },
});
Console.WriteLine(response.JobId);
```

<!-- /checked examples -->

### What you get back

```json
{
  "status": "success",
  "job_id": "1ZymBc-00041N-9X",
  "emails": [
    "you@example.com"
  ]
}
```

On error the API returns `status: "error"`, a `message` and a numeric `code`:

```json
{
  "status": "error",
  "code": 102,
  "message": "Error ID:83274F98-C7D5-11F1-B221-6214FF8707B1. Can not decode key wrongkey"
}
```

### Same flow with curl

```bash
BASE=https://api.unione.io/en/transactional/api/v1

curl -s -X POST $BASE/domain/get-sandbox-domain.json \
  -H "Content-Type: application/json" -H "X-API-KEY: $UNIONE_API_KEY" -d '{}'

curl -s -X POST $BASE/confirmed-email/create.json \
  -H "Content-Type: application/json" -H "X-API-KEY: $UNIONE_API_KEY" \
  -d '{"email": "you@example.com"}'

curl -s -X POST $BASE/email/send.json \
  -H "Content-Type: application/json" -H "X-API-KEY: $UNIONE_API_KEY" \
  -d '{
        "message": {
          "recipients": [
            {
              "email": "you@example.com"
            }
          ],
          "from_email": "noreply@<sandbox-domain>",
          "subject": "Hello from UniOne",
          "body": {
            "html": "<p>It works!</p>"
          }
        }
      }'
```

## Sandbox limits

- Sends only to confirmed addresses: up to 10 per account.
- Up to 100 emails a day.
- The sandbox domain expires after 31 days; call `domain/get-sandbox-domain` again to get a new one.
- Works only with the account API key, not a project key.
- Not listed in `domain/list`; the other `domain/*` methods do not apply to it.

The sandbox is for development and testing. Do not use a sandbox domain as the sender in production.

## Before production: verify your domain

1. `domain/get-dns-records` — returns the values for your DNS records.
2. Add these records at your DNS provider (the domain page of the control panel shows them too):

   | Type | Host | Value |
   |---|---|---|
   | TXT | `@` | the `verification-record` value (`unione-validate-hash=…`) |
   | TXT | `<selector>._domainkey` (the selector is on the domain page of the control panel, e.g. `us`) | `v=DKIM1; k=rsa; p=<dkim>` |
   | TXT | `@` | `v=spf1 include:spf.unione.io ~all` |
   | CNAME | `_dmarc` | the `dmarc` value |

3. `domain/validate-verification-record`, then `domain/validate-dkim`.
4. `domain/list` — wait until the verification record is confirmed and DKIM is active.
5. Read `from_email` from your config, so switching from the sandbox needs no code change.

## Sending reliably

- **Retries:** set `message.idempotence_key` on sends that may be retried, so a repeated request does not send twice.
- **Limits:** up to 500 recipients and 10 MB per request; each attachment up to 7 MB.
- **Body:** always include `body.html` or `body.plaintext`.
- **Delivery status:** register a webhook (`webhook/set`) with a fast, idempotent HTTPS handler instead of polling.
- **Defaults:** keep the default unsubscribe footer and tracking; changing them requires UniOne support.
- **Errors:** retry `429` and `5xx` with backoff; do not retry other errors unchanged.

## Links

- [Send emails with .NET (C#)](https://docs.unione.io/en/send-with-dotnet) and other [integration guides](https://docs.unione.io/en/integrations/)
- [Web API reference](https://docs.unione.io/en/web-api-ref)
- [llms.txt](https://docs.unione.io/llms.txt) for AI coding agents
- [UniOne MCP server and agent setup](https://unione.io/en/agents)
- [Support](https://cp.unione.io/en/support)

## For SDK contributors

See [AGENTS.md](AGENTS.md): how this SDK is generated from the OpenAPI spec, built, tested and released.

---

# SDK reference

## API host

The second argument selects the API host (datacenter) or a full base URL.

```csharp
using UniOne;
using UniOne.Client;

var client = new UniOneClient("YOUR_API_KEY", "us1.unione.io");
var other = new UniOneClient("YOUR_API_KEY", Configuration.HostUs1);
```

## Custom HTTP client / headers

Pass your own `HttpClient` (proxy, handlers, `IHttpClientFactory`) to the constructor; `Configuration.Timeout` still applies to every request. Models with free-form keys (`HeadersObject`) have an `Extra` dictionary: its keys go next to the named properties.

```csharp
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using UniOne;
using UniOne.Client;
using UniOne.Common.Model;

var http = new HttpClient(new HttpClientHandler { Proxy = new WebProxy("http://proxy.local:3128") });
var client = new UniOneClient(new Configuration("YOUR_API_KEY") { Timeout = TimeSpan.FromSeconds(10) }, http);

var headers = new HeadersObject
{
    Extra = new Dictionary<string, object>
    {
        ["X-MyHeader"] = "some data",
        ["CC"] = "copy@example.com",
        ["List-Unsubscribe"] = "<mailto:unsubscribe@example.com>",
    },
};
```

## Error handling

Every method raises `ApiException` when the API returns an error; it carries the [API error code](https://docs.unione.io/en/web-api-ref#api-errors) and the HTTP status.

## Sections

| Section | Methods | Description |
|---|---|---|
| [Email](docs/Email.md) | `SendAsync`, `SubscribeAsync` | This group of methods is related with email sending. |
| [EmailValidation](docs/EmailValidation.md) | `SingleAsync` | Email validation methods. As a result of the check, you’ll get detailed information on email validity. It will help eliminate nonexistent emails, decrease the number of complaints, clean up the subscribers list and eventually enhance your sender reputation score. For example, you may add validation to a subscription form on your site to let users know they’ve just entered an invalid address. |
| [ConfirmedEmail](docs/ConfirmedEmail.md) | `CreateAsync`, `ListAsync`, `DeleteAsync` | Manage email addresses confirmed for sandbox-domain sending. These methods are available only with a main account API key. |
| [Template](docs/Template.md) | `SetAsync`, `GetAsync`, `ListAsync`, `DeleteAsync` | You have the option of storing a template on the UniOne side and then using it by id, which is highly recommended for sending repetitive messages or high volume traffic. UniOne supports two different [template engines](https://docs.unione.io/en/template-engines). |
| [Webhook](docs/Webhook.md) | `SetAsync`, `GetAsync`, `ListAsync`, `DeleteAsync` | UniOne lets you recieve notifications about email status changes or spam blocks. You provide an URL and every time a watched event occurs we will notify you by submitting a request with JSON data to this URL. |
| [Suppression](docs/Suppression.md) | `SetAsync`, `GetAsync`, `ListAsync`, `DeleteAsync` | Methods for managing suppression lists. An email can be added to suppression list because of unsubscription, bounce or complain. |
| [Domain](docs/Domain.md) | `GetDnsRecordsAsync`, `ValidateVerificationRecordAsync`, `ValidateDkimAsync`, `ListAsync`, `GetSandboxDomainAsync`, `DeleteAsync` | You can use these methods to setup sender domains and check their statuses. They might be useful in case you want to automate adding domains for your own clients. |
| [EventDump](docs/EventDump.md) | `CreateAsync`, `GetAsync`, `ListAsync`, `DeleteAsync` | These methods are used to obtain a list of events for a given period of time. The data is stored for up to 45 days, depending on your tariff, i. e. you may fetch all event data starting from 00:00:00 31 days ago and up to current time. For custom tarifs with big sending volumes event dumps are available only for the last 7 days. |
| [Tag](docs/Tag.md) | `ListAsync`, `DeleteAsync` | This family of methods is used to manage user-defined tags. Tags are used to categorize letters based on your own criteria, e. g. to create a distinction between marketing and transactional letters or group together all notifications for a particular order type. UniOne allows you to assign tags to all outgoing emails, and the tags you’ve set may then be used to filter and analyze delivery statistics. |
| [Project](docs/Project.md) | `CreateAsync`, `UpdateAsync`, `ListAsync`, `DeleteAsync` | Projects allow you to group several independent domains, webhooks, templates, and unsubscription lists together. It's very useful if you have your own clients and manage their data from a single UniOne account. Each project receives its own API key, and all API calls with that API key only deal with the data of that project. This way, you can assign a separate project for each client and be sure that they won't conflict; e.g. unsubscribing from one project won't affect another. |
| [System](docs/System.md) | `PingAsync`, `InfoAsync` | System operations. |
| [Obsolete](docs/Obsolete.md) | `UnsubscribedSetAsync`, `UnsubscribedCheckAsync`, `UnsubscribedListAsync` | Obsolete methods dealing with unsubscribes. We recommend using the more flexible [suppression](https://docs.unione.io/en/web-api-ref#suppression) methods instead. Note that you can't resubscribe an unsubscribed recipient using WebAPI, but you can send him an email with subscription link using email/subscribe method. |
