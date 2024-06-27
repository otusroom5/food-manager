namespace FoodStorage.Domain.Entities.Common.Events;

public abstract class BaseEventMessage
{
    /// <summary>
    /// Произошло когда
    /// </summary>
    public DateTime OccuredOn { get; }

    protected BaseEventMessage(DateTime occuredOn)
    {
        OccuredOn = occuredOn;
    }
}
