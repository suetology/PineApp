namespace NotificationsService.Services;

public class MockEmailService : IEmailService
{
    public void SendEmail(string recipient, string message)
    {
        Console.WriteLine("Sending email with message '" + message + "' to " + recipient);
    }
}