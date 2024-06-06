namespace FoodUserNotifier.Core.Interfaces;

public enum DomainLogRecordType { Information, Warning, Error }

public interface IDomainLogger : IObservable<DomainLogMessage>
{
    void Info(string message);
    void Warn(string message);
    void Error(string message);
}

public sealed class DomainLogMessage
{
    public string Message { get; set; }
    public DomainLogRecordType Type { get; set; }
}