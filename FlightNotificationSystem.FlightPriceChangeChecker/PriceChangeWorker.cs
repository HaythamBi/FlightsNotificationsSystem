using FlightNotificationSystem.Data;
using FlightNotificationSystem.FlightPriceChangeChecker.Interfaces;
using FlightNotificationSystem.Notification.Interfaces;

public class PriceChangeWorker : BackgroundService
{
    private readonly IFlightPriceChangeChecker _priceChangeChecker;
    private readonly ILogger<PriceChangeWorker> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PriceChangeWorker(IServiceScopeFactory serviceScopeFactory,IFlightPriceChangeChecker priceChangeChecker, ILogger<PriceChangeWorker> logger)
    {
        _priceChangeChecker = priceChangeChecker;
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

   
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Price Change Worker running.");
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var queueService = scope.ServiceProvider.GetRequiredService<IQueueService>();
        var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

        while (!stoppingToken.IsCancellationRequested)
        {
            await _priceChangeChecker.CheckPriceChangesAsync(dbContext, queueService, notificationService);
            await Task.Delay(60000, stoppingToken); // Check every 60 seconds
        }

        _logger.LogInformation("Price Change Worker is stopping.");
    }
}
