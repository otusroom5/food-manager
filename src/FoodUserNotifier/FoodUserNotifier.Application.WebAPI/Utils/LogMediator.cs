using FoodUserNotifier.Core.Interfaces;

namespace FoodUserNotifier.Application.WebAPI.Utils
{
    public sealed class LogMediator
    {
        public LogMediator(Serilog.ILogger logger, IDomainLogger domainLogger) 
        {
            domainLogger.Subscribe(new Observer(logger));
        }

        private sealed class Observer : IObserver<DomainLogMessage>
        {
            private readonly Serilog.ILogger _logger;

            public Observer(Serilog.ILogger logger)
            {
                _logger = logger;
            }

            public void OnCompleted()
            {
            }

            public void OnError(Exception error)
            {
            }

            public void OnNext(DomainLogMessage value)
            {
                switch (value.Type)
                {
                    case DomainLogRecordType.Information:
                        _logger.Information(value.Message);
                        break;
                    case DomainLogRecordType.Warning:
                        _logger.Warning(value.Message);
                        break;
                    case DomainLogRecordType.Error:
                        _logger.Error(value.Message);
                        break;
                }
            }
        }
    }
}
