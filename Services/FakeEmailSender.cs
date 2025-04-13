using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Assignment_1_G_92_2139.Services
{
    public class FakeEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Debug.WriteLine($"[Fake Email] To: {email}");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {htmlMessage}");
            return Task.CompletedTask;
        }
    }
}