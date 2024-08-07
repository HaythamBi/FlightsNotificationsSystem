namespace FlightNotificationSystem.Notification.Interfaces
{
    public interface INotificationService
    {
        Task StartProcessingAsync();

        Task StopProcessingAsync();
    }
}
