namespace NotificationsService.Services;

public interface IEmailService
{
    void SendEmail(string recipient, string message);
}