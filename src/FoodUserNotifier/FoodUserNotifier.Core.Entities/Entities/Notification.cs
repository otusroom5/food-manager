using FoodUserNotifier.Core.Entities.Types;

namespace FoodUserNotifier.Core.Entities;

public sealed class Notification
{
    public Guid Id {  get; set; } 
    public RecepientGroupType Group { get; set; }
    public string Message { get; set; }
    public int[] AttachmentIds { get; set; }
}
