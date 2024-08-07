using FlightNotificationSystem.Shared.Models;

namespace FlightNotificationSystem.Notification.Interfaces
{
    public interface IQueueService
    {
        Task<AlertMessage> DequeueAlertMessageAsync();
        Task EnqueueFlightPriceChangeMessageAsync(AlertMessage alertMessage);
    }

}
