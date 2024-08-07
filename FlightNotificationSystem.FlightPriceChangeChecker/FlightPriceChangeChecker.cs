using Azure.Messaging.ServiceBus;
using FlightNotificationSystem.Data;
using FlightNotificationSystem.FlightPriceChangeChecker.Interfaces;
using FlightNotificationSystem.Notification.Interfaces;

public class FlightPriceChangeChecker : IFlightPriceChangeChecker
{
    private readonly ServiceBusClient _serviceBusClient;
    private readonly ILogger<FlightPriceChangeChecker> _logger;
    private readonly IConfiguration _configuration;

    public FlightPriceChangeChecker(ServiceBusClient serviceBusClient, IConfiguration configuration, ILogger<FlightPriceChangeChecker> logger)
    {
        _serviceBusClient = serviceBusClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task CheckPriceChangesAsync(ApplicationDbContext dbContext, IQueueService queueService, INotificationService notificationService)
    {
        var alertMessage = await queueService.DequeueAlertMessageAsync();

        if (alertMessage != null)
        {
            var existingFlight = dbContext.Flights.FirstOrDefault(f => f.Id == alertMessage.FlightId);

            if (existingFlight != null && existingFlight.Price != alertMessage.Price)
            {
                existingFlight.Price = alertMessage.Price;
                dbContext.Flights.Update(existingFlight);
                await dbContext.SaveChangesAsync();

                var usersToNotify = dbContext.UserAlerts
                    .Where(ua => ua.AlertId == existingFlight.Id)
                    .Select(ua => ua.UserId)
                    .ToList();

                foreach (var userId in usersToNotify)
                {
                    // Example code for sending message to Service Bus 
                    var sender = _serviceBusClient.CreateSender(_configuration["ServiceBus:FlightPriceChangeQueue"]);

                    // Create a sample message
                    var message = new ServiceBusMessage("Price change detected for flight XYZ");

                    await sender.SendMessageAsync(message);
                    _logger.LogInformation("Price change message sent to Service Bus.");
                }
            }
        }
    }
}
