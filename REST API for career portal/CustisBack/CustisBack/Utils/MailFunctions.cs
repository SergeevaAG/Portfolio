using MailKit.Net.Smtp;
using MimeKit;

namespace CustisBack.Utils
{
    public class MailFunctions
    {
        public static string SendEmail(string email, string bodyText, string emailSubject)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "ekzgos@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = emailSubject;
            emailMessage.Body = new TextPart("plain")
            {
                Text = bodyText
            };
            return SendEmail(emailMessage);
        }
        public static string SendEmail(string email, string subject, MimeEntity body)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "ekzgos@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = body;
            return SendEmail(emailMessage);
        }
        public static string SendEmail(MimeMessage message)
        {
            SmtpClient client = new SmtpClient();
            try
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("ekzgos@gmail.com", "linalinaqwert12");
                client.Send(message);
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.GetType().ToString();
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
