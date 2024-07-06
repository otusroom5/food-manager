

namespace FoodUserNotifier.Infrastructure.Services.Interfaces;

public interface IGmailMessage
{
    public void Message(string FromEmail, string ToEmail, string subject, string content);

}
