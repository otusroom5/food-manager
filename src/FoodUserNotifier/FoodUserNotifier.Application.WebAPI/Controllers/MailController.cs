using FoodManager.Shared.Types;
using FoodUserNotifier.Application.WebAPI.Contracts;
using FoodUserNotifier.Application.WebAPI.Models;
using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Core.Interfaces;
using FoodUserNotifier.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FoodUserNotifier.Infrastructure.Services.Interfaces;
using FoodUserNotifier.Infrastructure.Services.Implementations;



namespace FoodUserNotifier.Application.WebAPI.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Administration)]
    public class MailController : ControllerBase
    {
        GmailMessage gmailMessage;
        public   MailController(IServiceProvider service) 
        {
            gmailMessage = (GmailMessage)service.GetRequiredService<IGmailMessage>();   
        }


        [HttpGet("{FromEmail}/{ToEmail}/{subject}/{content}")]
        public void SendMessage([FromRoute]  string FromEmail, [FromRoute] string ToEmail, [FromRoute] string subject, [FromRoute] string content) 
        {
           
            gmailMessage.Message("chuchunov.nikolay@gmail.com", ToEmail, subject, @content);
        }



    }
}
