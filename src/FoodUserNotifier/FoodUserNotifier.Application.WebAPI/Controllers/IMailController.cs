using Microsoft.AspNetCore.Mvc;

namespace FoodUserNotifier.Application.WebAPI.Controllers
{
    public interface IMailController
    {
        public void SendMessage(string FromEmail, string ToEmail, string subject, string content);
    }
}
