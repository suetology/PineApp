namespace PineAPP.Services;

public class NotificationClient : INotificationClient
{
    private readonly string _url = "http://localhost:5023/Notifications";
    private readonly HttpClient _httpClient;

    public NotificationClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task SendNotification(string message)
    {
        var content = new StringContent(message);
        await _httpClient.PostAsync(_url, content);
    }
}