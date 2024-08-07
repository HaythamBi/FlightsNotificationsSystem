using FlightNotificationSystem.Data;
using FlightNotificationSystem.Notification.Interfaces;

namespace FlightNotificationSystem.FlightPriceChangeChecker.Interfaces
{

    public interface IFlightPriceChangeChecker
    {
        Task CheckPriceChangesAsync(ApplicationDbContext dbContext, IQueueService queueService, INotificationService notificationService);
    }

}
