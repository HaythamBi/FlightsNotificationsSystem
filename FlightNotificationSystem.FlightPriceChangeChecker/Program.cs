using Azure.Messaging.ServiceBus;
using FlightNotificationSystem.FlightPriceChangeChecker.Interfaces;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<ServiceBusClient>(provider =>
                {
                    var configuration = provider.GetRequiredService<IConfiguration>();
                    var connectionString = configuration["ServiceBus:ConnectionString"];
                    return new ServiceBusClient(connectionString);
                });

                services.AddScoped<IFlightPriceChangeChecker, FlightPriceChangeChecker>();
                services.AddHostedService<PriceChangeWorker>();
            });
}
