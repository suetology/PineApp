namespace PineAPP.Services;

public interface INotificationClient
{
    Task SendNotification(string message);
}