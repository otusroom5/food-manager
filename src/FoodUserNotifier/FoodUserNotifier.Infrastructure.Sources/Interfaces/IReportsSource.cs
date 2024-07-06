namespace FoodUserNotifier.Infrastructure.Sources.Interfaces;

public interface IReportsSource
{
    Task<Stream> GetReportAsync(Guid reportId);
}