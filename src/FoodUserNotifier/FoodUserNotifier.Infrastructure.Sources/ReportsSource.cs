using FoodUserNotifier.Core.Entities.Types;
using FoodUserNotifier.Infrastructure.Sources.Contracts.Requests;
using FoodUserNotifier.Infrastructure.Sources.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Mail;

namespace FoodUserNotifier.Infrastructure.Sources;

public class ReportsSource : IReportsSource
{
    private const string GetReportAttachmentUrl = "api/Report/GetReportAttachment";
    private readonly ILogger<ReportsSource> _logger;
    public readonly HttpClient _client;

    public ReportsSource(IHttpClientFactory httpClientFactory,
        string serviceName,
        ILogger<ReportsSource> logger)
    {
        _client = httpClientFactory.CreateClient(serviceName);
        _logger = logger;
    }
    public async Task<Stream> GetReportAsync(Guid reportId)
    {
        Uri requestUri = new UriBuilder(_client.BaseAddress)
        {
            Path = GetReportAttachmentUrl,
            Query = QueryString.Create("attachmentId", reportId.ToString()).ToString()
        }.Uri;

        return await _client.GetStreamAsync(requestUri);
    }
}
