
using FoodUserNotifier.Core.Domain.Interfaces;
using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Infrastructure.Sender.Smtp.Options;
using FoodUserNotifier.Infrastructure.Smtp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using FoodUserNotifier.Infrastructure.Sources.Interfaces;

namespace FoodUserNotifier.Infrastructure.Sender.Smtp;

public sealed class GmailMessageSender : IMessageSender
{
    
    private readonly ILogger _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IGmailMessage _gmailMessage;

    public GmailMessageSender( ILoggerFactory loggerFactory, IServiceProvider serviceProvider ) 
    {
        _logger = loggerFactory.CreateLogger<GmailMessageSender>();
        _gmailMessage = serviceProvider.GetService<IGmailMessage>();
        _serviceProvider = serviceProvider;

    }

    public async Task SendAsync(Message message, DeliveryReport report)
    {
        IEnumerable<Recepient> GmailRecepients = message.Recepients.Where(f => f.ContactType == Core.Entities.Types.ContactType.Email);
        IEnumerable<Stream> reportsStream = await DownloadReportsAsync(message.AttachmentIds);

        String patchReport = CreateFolderReport("report"+DateTime.Now.ToString());
        string filePatch = patchReport + Path.DirectorySeparatorChar + "Report.pdf";

        foreach (Stream _report in reportsStream) { SeveStriamToFile(_report, filePatch); }

        foreach (Recepient recepient in GmailRecepients) 
        {
            string _filePatch = patchReport + Path.DirectorySeparatorChar + "Report.pdf";
             _gmailMessage.Message("chuchunov.nikolay@gmail.com", recepient.Address, recepient.RecepientId.ToString(), filePatch);
        }

    }


    public async Task<IEnumerable<Stream>> DownloadReportsAsync(Guid[] AttachmentIds)
    {
        var reportsStream = new ConcurrentBag<Stream>();
        var exceptions = new ConcurrentQueue<Exception>();

        await Parallel.ForEachAsync(AttachmentIds, async (attacmnetId, token) =>
        {
            try
            {
                var reportSource = _serviceProvider.GetRequiredService<IReportsSource>();
                Stream stream = await reportSource.GetReportAsync(attacmnetId);
                reportsStream.Add(stream);

                _logger.LogTrace("Message was send successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                exceptions.Enqueue(ex);
            }
        });

        if (!exceptions.IsEmpty)
        {
            throw new AggregateException($"Aggregate Exception", exceptions);
        }

        return reportsStream.ToArray();
    }


    public string CreateFolderReport(string folder)
    {
        string _folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + folder;
        if (Directory.Exists(_folder) == false) { Directory.CreateDirectory(_folder); }
        return _folder;
    }


    public void SeveStriamToFile(Stream stream, string patch)
    {
        File.Create(patch);
        FileStream fileStream = new FileStream(patch, FileMode.Open);
        fileStream.Position = 0;
        fileStream.CopyTo(stream);
        fileStream.Dispose();
    }





}
