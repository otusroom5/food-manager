using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using System.Net.Mail;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using System.Text;
using MimeKit;
using Google.Apis.Requests;
using static System.Environment;
using FoodUserNotifier.Infrastructure.Smtp.Services.Interfaces;

namespace FoodUserNotifier.Infrastructure.Smtp.Services.Implementations
{
    public class GmailMessage: IGmailMessage
    {
        public GmailMessage() { }

        private static string EncodeBase64Url(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes).Replace('+', '-').Replace('/', '_').Replace("=", "");
        }


        public async void Message(string FromEmail, string ToEmail, string subject, string content)
        {
            var mess = new MailMessage { Subject = subject, From = new MailAddress(FromEmail) };
            mess.ReplyToList.Add(new MailAddress(ToEmail));
            mess.Attachments.Add(new Attachment(content));
            mess.To.Add(new MailAddress(ToEmail));
            var adds = AlternateView.CreateAlternateViewFromString("Отчет от " + DateTime.Now, new System.Net.Mime.ContentType("text/plain"));
            adds.ContentType.CharSet = Encoding.UTF8.WebName;
            mess.AlternateViews.Add(adds);
            var mime = MimeMessage.CreateFromMailMessage(mess);

            SpecialFolder specialFolder = new SpecialFolder("Google.Apis.Auth");
            string specialPatch = specialFolder.CreateFolder();

            var baseDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string path2 = Path.Combine(baseDirectoryPath, "client_secrets.json");

            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
        new ClientSecrets()
        {
            ClientId = "589235263377-b6kph65ouruqntgfffim011tlv1v1j4t.apps.googleusercontent.com",
            ClientSecret = "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"
        },
        new string[] { GmailService.Scope.GmailCompose },
        ToEmail,
        CancellationToken.None,
        new FileDataStore(specialPatch)).Result;

            var service = new GmailService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential

            });

            var gmailMessage = new Google.Apis.Gmail.v1.Data.Message { Raw = EncodeBase64Url(mime.ToString()) };

            var query = service.Users.Messages.Send(gmailMessage, credential.UserId);

            await ((IClientServiceRequest<Google.Apis.Gmail.v1.Data.Message>)query).ExecuteAsync();


        }

    }
}
