using Azure.Messaging.ServiceBus;
using FlightNotificationSystem.Notification.Interfaces;
using FlightNotificationSystem.Shared.Models;

public class NotificationService : INotificationService
{
    private readonly ServiceBusClient _serviceBusClient;
    private readonly ServiceBusProcessor _processor;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(ServiceBusClient serviceBusClient, IConfiguration configuration, ILogger<NotificationService> logger)
    {
        _serviceBusClient = serviceBusClient;
        var queueName = configuration["ServiceBus:FlightPriceChangeQueue"];
        _processor = _serviceBusClient.CreateProcessor(queueName, new ServiceBusProcessorOptions());
        _logger = logger;
    }

    public async Task StartProcessingAsync()
    {
        _processor.ProcessMessageAsync += MessageHandler;
        _processor.ProcessErrorAsync += ErrorHandler;

        await _processor.StartProcessingAsync();
        _logger.LogInformation("Started processing messages.");
    }

    public async Task StopProcessingAsync()
    {
        await _processor.StopProcessingAsync();
        await _processor.DisposeAsync();
        _logger.LogInformation("Stopped processing messages.");
    }

    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        var alertMessage = AlertMessage.FromJson(args.Message.Body.ToString());

        _logger.LogInformation($"Received message: {alertMessage.ToJson()}");

        // Process the flight price change and notify users
        await SendNotificationAsync(alertMessage.UserId, "Price changed for your flight!");

        await args.CompleteMessageAsync(args.Message);
    }

    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        _logger.LogError($"Error occurred: {args.Exception.Message}");
        return Task.CompletedTask;
    }

    public Task SendNotificationAsync(string userId, string message)
    {
        // Implement the logic to send a push notification to the user.
        // This could involve using a third-party push notification service.
        _logger.LogInformation($"Sending notification to user {userId}: {message}");
        return Task.CompletedTask;
    }

   
}
