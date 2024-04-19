
using FoodUserNotifier.DataAccess.Types;

namespace FoodUserNotifier.BusinessLogic.Abstractions;

public sealed class DataMessage
{
    public Role Role { get; set; }
    public string Data { get; set; }
}
