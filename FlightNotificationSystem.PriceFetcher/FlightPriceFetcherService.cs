using Azure.Messaging.ServiceBus;
using FlightNotificationSystem.Shared.Models;

public class FlightPriceFetcherService
{
    private readonly ServiceBusClient _serviceBusClient;
    private readonly string _queueName;

    public FlightPriceFetcherService(ServiceBusClient serviceBusClient, IConfiguration configuration)
    {
        _serviceBusClient = serviceBusClient;
        _queueName = configuration["ServiceBus:FlightPriceChangeQueue"];
    }

    public async Task FetchAndSendPriceChangeAsync()
    {
        // Fetch flight prices from external sources
        var fetchedPrice = 100; // Example price
        var alertMessage = new AlertMessage
        {
            FlightId = 1,
            Price = fetchedPrice,
            UserId = "user123"
        };

        var sender = _serviceBusClient.CreateSender(_queueName);
        var message = new ServiceBusMessage(alertMessage.ToJson());
        await sender.SendMessageAsync(message);
    }
}
