using System.Net;
using System.Net.Mail;

namespace NotificationsService.Services;

public class EmailService : IEmailService
{
    private readonly string _senderEmail = "";
    private readonly string _senderPassword = "";
    
    public void SendEmail(string recipient, string message)
    {
        var mailMessage = new MailMessage(_senderEmail, recipient)
        {
            Subject = "noreply",
            Body = message
        };

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(_senderEmail, _senderPassword),
            EnableSsl = true
        };
        
        smtpClient.Send(mailMessage);
    }
}