using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodUserNotifier.Infrastructure.Smtp.Services.Interfaces
{
    internal interface IGmailMessage
    {
        public void Message(string FromEmail, string ToEmail, string subject, string content);
    }
}
