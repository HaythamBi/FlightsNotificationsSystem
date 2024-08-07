using FlightNotificationSystem.Notification.Interfaces;
using FlightNotificationSystem.Shared.Models;

namespace FlightNotificationSystem.Notification.Services
{
    public class QueueService : IQueueService
    {
        // Example implementation using in-memory queue (for demonstration purposes)
        private readonly Queue<AlertMessage> _alertMessages = new Queue<AlertMessage>();
        private readonly Queue<AlertMessage> _flightPriceChangeMessages = new Queue<AlertMessage>();

        public Task<AlertMessage> DequeueAlertMessageAsync()
        {
            return Task.FromResult(_alertMessages.Count > 0 ? _alertMessages.Dequeue() : null);
        }

        public Task EnqueueFlightPriceChangeMessageAsync(AlertMessage alertMessage)
        {
            _flightPriceChangeMessages.Enqueue(alertMessage);
            return Task.CompletedTask;
        }

       
    }
}
