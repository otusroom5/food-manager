using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using System.Net.Mail;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using System.Text;
using MimeKit;
using Google.Apis.Requests;
using static System.Environment;

namespace FoodUserNotifier.Infrastructure.Smtp.Services.Implementations
{
    internal class GmailMessage
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
            mess.To.Add(new MailAddress(ToEmail));
            var adds = AlternateView.CreateAlternateViewFromString(content, new System.Net.Mime.ContentType("text/plain"));
            adds.ContentType.CharSet = Encoding.UTF8.WebName;
            mess.AlternateViews.Add(adds);
            var mime = MimeMessage.CreateFromMailMessage(mess);

            SpecialFolder specialFolder = new SpecialFolder("Google.Apis.Auth");
            string specialPatch = specialFolder.CreateFolder();

            string currentDirectory = Directory.GetParent(Environment.CurrentDirectory)?.FullName;
            string path1 = Directory.GetParent(currentDirectory)?.FullName;
            string path2 = Directory.GetParent(path1)?.FullName + @"\client_secrets.json";

            UserCredential credential;
            using (var stream = new FileStream(@path2, FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromFile(@path2).Secrets,
                    new[] { GmailService.Scope.GmailCompose },
                    ToEmail, CancellationToken.None, new FileDataStore(specialPatch));
            }

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
