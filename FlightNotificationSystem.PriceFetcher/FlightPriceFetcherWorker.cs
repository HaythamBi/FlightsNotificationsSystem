namespace FlightNotificationSystem.PriceFetcher
{
    public class FlightPriceFetcherWorker : BackgroundService
    {
        private readonly FlightPriceFetcherService _priceFetcherService;
        private readonly ILogger<FlightPriceFetcherWorker> _logger;

        public FlightPriceFetcherWorker(FlightPriceFetcherService priceFetcherService, ILogger<FlightPriceFetcherWorker> logger)
        {
            _priceFetcherService = priceFetcherService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                // Fetch flight prices
                await _priceFetcherService.FetchAndSendPriceChangeAsync();

                // Wait for a specified interval before running the tasks again
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
