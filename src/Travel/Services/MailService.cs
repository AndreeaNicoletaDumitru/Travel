using System.Diagnostics;

namespace Travel.Services
{
    public class MailService : IMailService
    {
        public void SendMail(string to, string from, string subject, string body)
        {
            Debug.WriteLine($"Sending Mail: To {to} From {from} Subject: {subject}");
        }
    }
}
