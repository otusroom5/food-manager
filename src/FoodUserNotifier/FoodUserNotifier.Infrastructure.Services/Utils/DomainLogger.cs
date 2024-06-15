using FoodUserNotifier.Core.Interfaces;

namespace FoodUserNotifier.Infrastructure.Services.Utils;

public class DomainLogger : IDomainLogger
{
    private readonly HashSet<IObserver<DomainLogMessage>> _subscribers = new ();
    
    public void Error(string message)
    {
        Log(message, DomainLogRecordType.Error);
    }

    public void Info(string message)
    {
        Log(message, DomainLogRecordType.Information);
    }

    public void Warn(string message)
    {
        Log(message, DomainLogRecordType.Warning);
    }


    private void Log(string message, DomainLogRecordType Type)
    {
        foreach (var subscriber in _subscribers) {
            subscriber?.OnNext(new DomainLogMessage()
            {
                Message = message,
                Type = Type
            });
        };
    }


    public IDisposable Subscribe(IObserver<DomainLogMessage> observer)
    {
        if (!_subscribers.Contains(observer))
        {
            _subscribers.Add(observer);
        }

        return new UnSubscriber(_subscribers, observer);
    }

    private sealed class UnSubscriber : IDisposable
    {
        private readonly HashSet<IObserver<DomainLogMessage>> _subscribers;
        private readonly IObserver<DomainLogMessage> _subscriber;
        
        public UnSubscriber(HashSet<IObserver<DomainLogMessage>> subscribers, IObserver<DomainLogMessage> subscriber)
        {
            _subscribers = subscribers;
            _subscriber = subscriber;
        }
        public void Dispose()
        {
            _subscribers.Remove(_subscriber);
        }
    }
}
