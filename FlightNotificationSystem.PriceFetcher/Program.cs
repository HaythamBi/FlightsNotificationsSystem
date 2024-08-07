using Azure.Messaging.ServiceBus;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<ServiceBusClient>(provider =>
                {
                    var configuration = provider.GetRequiredService<IConfiguration>();
                    return new ServiceBusClient(configuration["ServiceBus:ConnectionString"]);
                });

                services.AddSingleton<FlightPriceFetcherService>();
                services.AddHostedService<PriceFetcherBackgroundService>();
            });
}
