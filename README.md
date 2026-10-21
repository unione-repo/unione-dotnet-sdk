# UniOne .NET (C#) SDK

[![.NET (C#)](https://img.shields.io/nuget/v/UniOne)](https://www.nuget.org/packages/UniOne) [![License: MIT](https://img.shields.io/badge/license-MIT-blue)](LICENSE)

Official .NET (C#) library for the [UniOne](https://unione.io) transactional email API.

[API reference](https://docs.unione.io/en/web-api-ref) · Docs for LLMs: [llms.txt](https://docs.unione.io/llms.txt)

> **On ASP.NET Core?** Install [`UniOne.AspNetCore`](https://github.com/unione-repo/unione-aspnetcore) instead: this SDK plus configuration, a mailer and a webhook route.

## Install

```bash
dotnet add package UniOne
```

Requirements: .NET 8. No third-party dependencies.

For .NET Standard 2.0 use the 1.x line (branch `1.x`), the same API.

Get an API key in the [control panel](https://cp.unione.io/en/settings/security/api) and keep it in the environment, never in code:

```bash
export UNIONE_API_KEY="your-api-key"
```

## Send an email

```csharp
using UniOne;
using UniOne.Api.Email.Model;
using UniOne.Common.Model;
using Recipient = UniOne.Api.Email.Model.MessageObjectRecipientsItem;

var client = new UniOneClient(System.Environment.GetEnvironmentVariable("UNIONE_API_KEY") ?? throw new System.InvalidOperationException("UNIONE_API_KEY is not set"));

var response = await client.Email.SendAsync(new EmailSendRequest
{
    Message = new MessageObject
    {
        Recipients = [Recipient.FromAddress("Alex <alex@example.com>")],
        FromEmail = "shop@example.com",
        FromName = "Shop",
        Subject = "Your order",
        Body = BodyObject.FromHtml("<p>Hello, {{to_name}}! Your order has shipped.</p>"),
    },
});
Console.WriteLine(response.JobId);
```

The sender address must be on a domain verified in your account. No domain yet? Send from the [sandbox domain](#no-domain-yet-the-test-sandbox-domain) in three steps.

## Contents

- [Recipes](#recipes): personalization · attachments · CC and BCC · scheduling · safe retries · errors · webhooks
- [No domain yet: the test sandbox domain](#no-domain-yet-the-test-sandbox-domain)
- [Going to production](#going-to-production)
- [Reference](#reference)
- [Help](#help)

## Recipes

Every recipe continues the example above: the client and its imports are there.

### Personalize for each recipient

Up to 500 recipients per call, each gets a separate copy with its own substitutions. Metadata of a recipient comes back in its webhook events.

```csharp
using System.Collections.Generic;

await client.Email.SendAsync(new EmailSendRequest
{
    Message = new MessageObject
    {
        Recipients =
        [
            Recipient.FromAddress("Alex <alex@example.com>", new Dictionary<string, string> { ["order"] = "1001" })
                .WithMetadata(new Dictionary<string, string> { ["user_id"] = "42" }),
            Recipient.FromAddress("Maria <maria@example.com>", new Dictionary<string, string> { ["order"] = "1002" })
                .WithMetadata(new Dictionary<string, string> { ["user_id"] = "43" }),
        ],
        FromEmail = "shop@example.com",
        Subject = "Order #{{order}}",
        Body = BodyObject.FromHtml("<p>{{to_name}}, order #{{order}} has shipped.</p>"),
    },
});
```

### Attach a file

```csharp
using System.IO;

var content = Convert.ToBase64String(File.ReadAllBytes("invoice.pdf"));
await client.Email.SendAsync(new EmailSendRequest
{
    Message = new MessageObject
    {
        Recipients = [Recipient.FromAddress("alex@example.com")],
        FromEmail = "shop@example.com",
        Subject = "Invoice",
        Body = BodyObject.FromHtml("<p>Your invoice is attached.</p>"),
        Attachments =
        [
            new AttachmentsArrayItem { Type = "application/pdf", Name = "invoice.pdf", Content = content },
        ],
    },
});
```

### CC and BCC

Every address goes to `recipients` (each gets a copy); the visible ones also go to the headers. `audit@example.com` is in neither header: a BCC. The rules and limits: [CC and BCC](https://docs.unione.io/en/cc-and-bcc).

```csharp
await client.Email.SendAsync(new EmailSendRequest
{
    Message = new MessageObject
    {
        Recipients =
        [
            Recipient.FromAddress("alex@example.com"),
            Recipient.FromAddress("boss@example.com"),
            Recipient.FromAddress("audit@example.com"),
        ],
        FromEmail = "shop@example.com",
        Subject = "Weekly report",
        Body = BodyObject.FromHtml("<p>The report is attached.</p>"),
        Headers = new HeadersObject
        {
            Extra = new() { ["To"] = "alex@example.com", ["CC"] = "boss@example.com" },
        },
    },
});
```

### Schedule

`send_at` is UTC, up to 24 hours ahead.

```csharp
using System.Globalization;

// UTC, "YYYY-MM-DD hh:mm:ss"
var inOneHour = DateTime.UtcNow.AddHours(1).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
await client.Email.SendAsync(new EmailSendRequest
{
    Message = new MessageObject
    {
        Recipients = [Recipient.FromAddress("alex@example.com")],
        FromEmail = "shop@example.com",
        Subject = "Reminder",
        Body = BodyObject.FromText("See you tomorrow!"),
        Options = new MessageObjectOptions { SendAt = inOneHour },
    },
});
```

### Retry safely

A repeat with the same `idempotence_key` within a minute is not sent twice: retry network errors, timeouts, 429 and 5xx with it.

```csharp
await client.Email.SendAsync(new EmailSendRequest
{
    Message = new MessageObject
    {
        Recipients = [Recipient.FromAddress("alex@example.com")],
        FromEmail = "shop@example.com",
        Subject = "Reset your password",
        Body = BodyObject.FromHtml("<p>Use this link to reset your password.</p>"),
        IdempotenceKey = "password-reset-8f3a",
    },
});
```

### Handle errors

Recipients the API drops (unsubscribed, invalid, suppressed) are not an error: they are in `failed_emails` of the response, the rest still get the message.

```csharp
using UniOne.Client;

try
{
    await client.Email.SendAsync(request);
}
catch (ApiException e)
{
    // HTTP status (0: network error or timeout), API error code, message
    Console.Error.WriteLine($"{e.HttpStatus} {e.ErrorCode} {e.Message}");
}
```

### Receive delivery events

Register your HTTPS endpoint once (`webhook/set`). Then check the signature of every notification (`auth`: MD5 of the raw body with the `auth` value replaced by your API key; a wrong one gets 401) and process the events.

```csharp
using System.Security.Cryptography;
using System.Text;
using UniOne.Api.Webhook;

// rawBody: the body of the POST request as received; returns the HTTP status to answer
static int HandleWebhook(string rawBody)
{
    var callback = WebhookApi.ParseCallback(rawBody);
    if (string.IsNullOrEmpty(callback.Auth))
    {
        return 401;
    }
    // auth: MD5 of the raw body with the auth value replaced by your API key
    var signed = rawBody.Replace(callback.Auth, Environment.GetEnvironmentVariable("UNIONE_API_KEY"));
    var md5 = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(signed));
    if (BitConverter.ToString(md5).Replace("-", "").ToLowerInvariant() != callback.Auth)
    {
        return 401;
    }
    foreach (var user in callback.EventsByUser ?? new())
    {
        foreach (var evt in user.Events ?? new())
        {
            // evt.EventData: email, status (delivered, opened, hard_bounced, spam, ...), job_id, metadata
        }
    }
    return 200;
}
```

> **Answer 200 within 3 seconds** and process the events in the background (a queue): otherwise UniOne repeats the request every 10 minutes for 24 hours (`retry_count` grows), and a handler that failed every request for at least 10 events of a day is turned inactive. The handler must take at least **5 parallel requests of up to 100 events each**; deduplicate, a repeat delivers the same events again.

## No domain yet: the test sandbox domain

Until your domain is verified, send from the **sandbox domain**: a test domain every account has, already verified, with DKIM and SPF set. It delivers only to addresses you confirm (the confirmation link comes by email, valid 48 hours), up to 100 emails a day. The sandbox domain is deleted 31 days after it was created, but you can get it again: the next `domain/get-sandbox-domain` call creates a new one (with a new name, so read it from the call, do not hardcode it).

```csharp
using UniOne.Api.ConfirmedEmail.Model;

// 1. Your sandbox domain: created on the first call, the same one afterwards
var sandbox = await client.Domain.GetSandboxDomainAsync();

// 2. Confirm your inbox: UniOne sends a link, click it within 48 hours
await client.ConfirmedEmail.CreateAsync(new ConfirmedEmailRequest { Email = "you@example.com" });

// 3. Send from any address on the sandbox domain to the confirmed one
await client.Email.SendAsync(new EmailSendRequest
{
    Message = new MessageObject
    {
        Recipients = [Recipient.FromAddress("you@example.com")],
        FromEmail = $"noreply@{sandbox.Domain}",
        Subject = "Hello from UniOne",
        Body = BodyObject.FromHtml("<p>It works!</p>"),
    },
});
```

<details>
<summary>Sandbox limits</summary>

- Sends only to confirmed addresses: up to 10 per account.
- Up to 100 emails a day.
- The sandbox domain expires after 31 days; call `domain/get-sandbox-domain` again to get a new one.
- Works only with the account API key, not a project key.
- Not listed in `domain/list`; the other `domain/*` methods do not apply to it.

The sandbox is for development and testing. Do not use a sandbox domain as the sender in production.

</details>

<details>
<summary>The same with curl</summary>

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

</details>

## Going to production

**1. Verify your sending domain.** `domain/get-dns-records` returns the verification record, the DKIM key and the DMARC target; add the records at your DNS provider, then `domain/validate-verification-record` and `domain/validate-dkim`.

| Type | Host | Value | Where it comes from |
|---|---|---|---|
| TXT | `@` | `unione-validate-hash=…` | `verification-record` of the response, as is |
| TXT | `<selector>._domainkey` (the selector is on the domain page of the control panel, e.g. `us`) | `v=DKIM1; k=rsa; p=<dkim>` | `dkim` of the response after `v=DKIM1; k=rsa; p=` |
| TXT | `@` | `v=spf1 include:spf.unione.io ~all` | not in the response; add the include to your SPF record if you have one |
| CNAME | `_dmarc` | `<domain>.dmarc.…` | `dmarc` of the response (UniOne publishes the DMARC policy); keep your own DMARC TXT record instead if the domain has one |

**2. Make sending reliable.**

| | |
|---|---|
| Retries | `429` and `5xx` with backoff and the same `idempotence_key`; fix other errors instead of retrying |
| Limits | 500 recipients and 10 MB per request, attachments included: base64 makes a file about a third larger |
| Body | always `html` or `plaintext` (a template can supply it) |
| Delivery status | a webhook, not polling |
| Queue | send from a background job, not inside the web request |
| Config | `from_email` from config, so the sandbox → production switch needs no code change |
| Defaults | keep the unsubscribe footer and tracking; changing them needs UniOne support |

## Reference

### Sections

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

### API host

The second argument selects the API host (datacenter) or a full base URL.

```csharp
using UniOne;
using UniOne.Client;

var client = new UniOneClient("YOUR_API_KEY", "us1.unione.io");
var other = new UniOneClient("YOUR_API_KEY", Configuration.HostUs1);
```

### Custom HTTP client / headers

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

### Error handling

Every method raises `ApiException` when the API returns an error; it carries the [API error code](https://docs.unione.io/en/web-api-ref#api-errors) and the HTTP status.

## Help

- [Integration guides](https://docs.unione.io/en/integrations)
- AI coding agents: [llms.txt](https://docs.unione.io/llms.txt), the [MCP server](https://unione.io/en/agents), skills: `npx skills add https://github.com/unione-repo/unione-skills.git`
- [Support](https://cp.unione.io/en/support) · [Issues](https://github.com/unione-repo/unione-dotnet-sdk/issues)
- Contributing: [AGENTS.md](AGENTS.md) — the SDK is generated from the OpenAPI spec.
