using System.Net.Http;
using UniOne.Api.ConfirmedEmail;
using UniOne.Api.Domain;
using UniOne.Api.Email;
using UniOne.Api.EmailValidation;
using UniOne.Api.EventDump;
using UniOne.Api.Obsolete;
using UniOne.Api.Project;
using UniOne.Api.Suppression;
using UniOne.Api.System;
using UniOne.Api.Tag;
using UniOne.Api.Template;
using UniOne.Api.Webhook;
using UniOne.Client;

namespace UniOne;

/// <summary>
/// Entry point of the UniOne API: one property per section.
/// </summary>
public class UniOneClient
{
    /// <param name="apiKey">User or project API key.</param>
    /// <param name="host">API host (Configuration.HostUs1, "us1.unione.io") or a full base URL; null: the default one.</param>
    public UniOneClient(string apiKey, string? host = null)
        : this(new Configuration(apiKey) { BaseUrl = host == null ? Configuration.DefaultBaseUrl : Configuration.BaseUrlFor(host) })
    {
    }

    public UniOneClient(Configuration config, HttpClient? httpClient = null)
    {
        ApiClient = new ApiClient(config, httpClient);
        Email = new EmailApi(ApiClient);
        EmailValidation = new EmailValidationApi(ApiClient);
        ConfirmedEmail = new ConfirmedEmailApi(ApiClient);
        Template = new TemplateApi(ApiClient);
        Webhook = new WebhookApi(ApiClient);
        Suppression = new SuppressionApi(ApiClient);
        Domain = new DomainApi(ApiClient);
        EventDump = new EventDumpApi(ApiClient);
        Tag = new TagApi(ApiClient);
        Project = new ProjectApi(ApiClient);
        System = new SystemApi(ApiClient);
        Obsolete = new ObsoleteApi(ApiClient);
    }

    /// <summary>Low level transport.</summary>
    public ApiClient ApiClient { get; }

    /// <summary>
    /// This group of methods is related with email sending.
    /// </summary>
    public EmailApi Email { get; }

    /// <summary>
    /// Email validation methods. As a result of the check, you’ll get detailed information on email<br/>
    /// validity. It will help eliminate nonexistent emails, decrease the number of complaints, clean up the<br/>
    /// subscribers list and eventually enhance your sender reputation score. For example, you may add<br/>
    /// validation to a subscription form on your site to let users know they’ve just entered an invalid<br/>
    /// address.
    /// </summary>
    public EmailValidationApi EmailValidation { get; }

    /// <summary>
    /// Manage email addresses confirmed for sandbox-domain sending. These methods are available only with a<br/>
    /// main account API key.
    /// </summary>
    public ConfirmedEmailApi ConfirmedEmail { get; }

    /// <summary>
    /// You have the option of storing a template on the UniOne side and then using it by id, which is<br/>
    /// highly recommended for sending repetitive messages or high volume traffic. UniOne supports two<br/>
    /// different [template engines](https://docs.unione.io/en/template-engines).
    /// </summary>
    public TemplateApi Template { get; }

    /// <summary>
    /// UniOne lets you recieve notifications about email status changes or spam blocks. You provide an URL<br/>
    /// and every time a watched event occurs we will notify you by submitting a request with JSON data to<br/>
    /// this URL.
    /// </summary>
    public WebhookApi Webhook { get; }

    /// <summary>
    /// Methods for managing suppression lists. An email can be added to suppression list because of<br/>
    /// unsubscription, bounce or complain.
    /// </summary>
    public SuppressionApi Suppression { get; }

    /// <summary>
    /// You can use these methods to setup sender domains and check their statuses. They might be useful in<br/>
    /// case you want to automate adding domains for your own clients.
    /// </summary>
    public DomainApi Domain { get; }

    /// <summary>
    /// These methods are used to obtain a list of events for a given period of time. The data is stored for<br/>
    /// up to 45 days, depending on your tariff, i. e. you may fetch all event data starting from 00:00:00<br/>
    /// 31 days ago and up to current time. For custom tarifs with big sending volumes event dumps are<br/>
    /// available only for the last 7 days.
    /// </summary>
    public EventDumpApi EventDump { get; }

    /// <summary>
    /// This family of methods is used to manage user-defined tags. Tags are used to categorize letters<br/>
    /// based on your own criteria, e. g. to create a distinction between marketing and transactional<br/>
    /// letters or group together all notifications for a particular order type. UniOne allows you to assign<br/>
    /// tags to all outgoing emails, and the tags you’ve set may then be used to filter and analyze delivery<br/>
    /// statistics.
    /// </summary>
    public TagApi Tag { get; }

    /// <summary>
    /// Projects allow you to group several independent domains, webhooks, templates, and unsubscription<br/>
    /// lists together. It's very useful if you have your own clients and manage their data from a single<br/>
    /// UniOne account. Each project receives its own API key, and all API calls with that API key only deal<br/>
    /// with the data of that project. This way, you can assign a separate project for each client and be<br/>
    /// sure that they won't conflict; e.g. unsubscribing from one project won't affect another.
    /// </summary>
    public ProjectApi Project { get; }

    /// <summary>
    /// System operations.
    /// </summary>
    public SystemApi System { get; }

    /// <summary>
    /// Obsolete methods dealing with unsubscribes. We recommend using the more flexible<br/>
    /// [suppression](https://docs.unione.io/en/web-api-ref#suppression) methods instead. Note that you<br/>
    /// can't resubscribe an unsubscribed recipient using WebAPI, but you can send him an email with<br/>
    /// subscription link using email/subscribe method.
    /// </summary>
    public ObsoleteApi Obsolete { get; }
}
