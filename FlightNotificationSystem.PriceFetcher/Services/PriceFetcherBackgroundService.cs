public class PriceFetcherBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PriceFetcherBackgroundService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var priceFetcherService = scope.ServiceProvider.GetRequiredService<FlightPriceFetcherService>();
                await priceFetcherService.FetchAndSendPriceChangeAsync();
            }

            await Task.Delay(10000, stoppingToken); // Polling delay
        }
    }
}
