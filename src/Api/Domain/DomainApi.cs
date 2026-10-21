using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniOne.Api.Domain.Model;
using UniOne.Client;

namespace UniOne.Api.Domain;

/// <summary>
/// You can use these methods to setup sender domains and check their statuses. They might be useful in<br/>
/// case you want to automate adding domains for your own clients.
/// <para/>
/// To add new sender domain thru API you should take following steps:<br/>
/// - Get DNS records with [domain/get-dns-records](https://docs.unione.io/en/web-api-ref#domain-get-dns-records) method and add them to your DNS provider.<br/>
/// - Every 5-10 minutes call [domain/list](https://docs.unione.io/en/web-api-ref#domain-list) method for your domain (or several domains) and check verification-record.status and dkim.status in the response. You can call it more often if you like, but please not more than once per minute.<br/>
/// - If verification-record.status="unconfirmed" then you should call [domain/validate-verification-record](https://docs.unione.io/en/web-api-ref#domain-validate-verification-record) to trigger verification record validation.<br/>
/// - If dkim.status="inactive" then start DKIM checking by calling [domain/validate-dkim](https://docs.unione.io/en/web-api-ref#domain-validate-dkim) (UniOne itself checks  DKIM every 30 minutes also)<br/>
/// - When you get verification-record.status="confirmed" and dkim.status="active" for the domain then you can stop checking it's status with domain/list and start using it as a sender domain.
/// </summary>
public class DomainApi
{
    private readonly ApiClient _client;

    public DomainApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Returns info on domain DNS setup for using with UniOne.
    /// <para/>
    /// In order to send emails you have to confirm that you own the domain that you are going to send<br/>
    /// emails from and setup DKIM signature for the domain. This method allows you to register your domain<br/>
    /// in UniOne and generate verification record and DKIM key, or return previously generated ones if<br/>
    /// domain is already registered.<br/>
    /// You should add then DNS records like this to all of the domains you want to confirm:
    /// <para/>
    /// `@             IN TXT "unione-validate-hash=483bb362ebdbeedd755cfb1d4d661"`
    /// <para/>
    /// `us._domainkey IN TXT "v=DKIM1; k=rsa; p=MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDo7"`
    /// <para/>
    /// `@             IN TXT  "v=spf1 include:spf.unione.io ~all"`
    /// <para/>
    /// `_dmarc        IN CNAME "example.com.dmarc.unione.io."`
    /// <para/>
    /// - First record value should contain "verification-record" field returned by get-dns-records method "as is".<br/>
    /// - Second record value should contain "dkim" field value after "v=DKIM1; k=rsa; p=". Mind the name of this record! "us._domainkey." prefix is important for DKIM selector of UniOne to work properly.<br/>
    /// - Third record contains optional, but very recommended SPF entry. It is not returned by the method. Probably you already have one, then just add append include:spf.unione.io right after v=spf1.<br/>
    /// - Fourth record is a CNAME of the "_dmarc" host to the "dmarc" field value: UniOne then publishes the DMARC policy of the domain. If the domain already has its own DMARC TXT record, keep it instead.
    /// <para/>
    /// Syntax requirements of different DNS providers may differ:<br/>
    /// - sometimes it's necesarry to put full domain name instead of @ sign.<br/>
    /// - some providers require add TTL value (like 3600)<br/>
    /// - some providers don't require quotes for enclosing values<br/>
    /// Please check requirements of your provider and advice your users to do it.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#domain-get-dns-records
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<DomainGetDnsRecordsResponse> GetDnsRecordsAsync(DomainGetDnsRecordsRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<DomainGetDnsRecordsResponse>("/domain/get-dns-records.json", request, cancellationToken);
    }

    /// <summary>
    /// Triggers verification record validation for domain.
    /// <para/>
    /// If you get 200 OK then it means the validation process started succesfully, but it doesn't mean the<br/>
    /// verification record is valid. You should check later verification-record.status field returned by<br/>
    /// [domain/list](https://docs.unione.io/en/web-api-ref#domain-list) method to get the result. Please<br/>
    /// read more [here](https://docs.unione.io/en/web-api-ref#domain).
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#domain-validate-verification-record
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<DomainValidateVerificationRecordResponse> ValidateVerificationRecordAsync(DomainValidateVerificationRecordRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<DomainValidateVerificationRecordResponse>("/domain/validate-verification-record.json", request, cancellationToken);
    }

    /// <summary>
    /// Validates DKIM record for domain.
    /// <para/>
    /// If you get 200 OK then it means the validation process started succesfully, but it doesn't mean the<br/>
    /// DKIM record is valid. You should check later dkim.status field returned by<br/>
    /// [domain/list](https://docs.unione.io/en/web-api-ref#domain-list) method to get the result. Please<br/>
    /// read more [here](https://docs.unione.io/en/web-api-ref#domain).
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#domain-validate-dkim
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<DomainValidateDkimResponse> ValidateDkimAsync(DomainValidateDkimRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<DomainValidateDkimResponse>("/domain/validate-dkim.json", request, cancellationToken);
    }

    /// <summary>
    /// Returns a list of all registered domains for user or project, together with statuses of last<br/>
    /// validations.
    /// <para/>
    /// Returns last status of ownership validation and DKIM validation for each domain. Read more<br/>
    /// [here](https://docs.unione.io/en/web-api-ref#domain).
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#domain-list
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<DomainListResponse> ListAsync(DomainListRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<DomainListResponse>("/domain/list.json", request, cancellationToken);
    }

    /// <summary>
    /// Creates a sandbox domain for the account or returns the existing one.
    /// <para/>
    /// Returns an account sandbox domain that is ready for use in the `from_email` field of<br/>
    /// [email/send](https://docs.unione.io/en/web-api-ref#email-send). If the account has no sandbox<br/>
    /// domain, UniOne creates one and returns it. Repeated calls return the same domain.
    /// <para/>
    /// The sandbox domain is removed 31 days after it was created; the next call creates a new one. Up to<br/>
    /// 100 emails a day can be sent from it (the default limit of the account).
    /// <para/>
    /// The method is available only with an account API key. Project API keys cannot create sandbox<br/>
    /// domains.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#domain-get-sandbox-domain
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<DomainGetSandboxDomainResponse> GetSandboxDomainAsync(CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<DomainGetSandboxDomainResponse>("/domain/get-sandbox-domain.json", null, cancellationToken);
    }

    /// <summary>
    /// Deletes a domain. Attention: you should use project's API key to delete a project's domain.
    /// <para/>
    /// Deletes a domain.
    /// <para/>
    /// Documentation: https://docs.unione.io/en/web-api-ref#domain-delete
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<DomainDeleteResponse> DeleteAsync(DomainDeleteRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<DomainDeleteResponse>("/domain/delete.json", request, cancellationToken);
    }
}
