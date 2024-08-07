using FlightNotificationSystem.Notification.Interfaces;

public class NotificationWorker : BackgroundService
{
    private readonly INotificationService _notificationService;
    private readonly ILogger<NotificationWorker> _logger;

    public NotificationWorker(INotificationService notificationService, ILogger<NotificationWorker> logger)
    {
        _notificationService = notificationService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Notification Worker running.");

        // Start processing messages from the queue
        await _notificationService.StartProcessingAsync();

        while (!stoppingToken.IsCancellationRequested)
        {
            // Wait for a bit before checking the cancellation token
            await Task.Delay(1000, stoppingToken);
        }

        _logger.LogInformation("Notification Worker is stopping.");

        // Stop processing messages from the queue
        await _notificationService.StopProcessingAsync();
    }
}
