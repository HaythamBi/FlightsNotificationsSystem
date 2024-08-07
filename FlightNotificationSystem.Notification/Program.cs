using Azure.Messaging.ServiceBus;
using FlightNotificationSystem.Notification.Interfaces;

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
                    var connectionString = configuration["ServiceBus:ConnectionString"];
                    return new ServiceBusClient(connectionString);
                });

                services.AddScoped<INotificationService, NotificationService>();
                services.AddHostedService<NotificationWorker>();
            });
}
