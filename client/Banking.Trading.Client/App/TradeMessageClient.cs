using Banking.Trading.Client.Messaging.Consumer;
using Banking.Trading.Client.Messaging.Processor;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Banking.Trading.Client.App;

public class TradeMessageClient
{

    public static IHost StartClient()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddHostedService<TradeMessageConsumer>();
                services.AddScoped<IMessageProcessor, MessageProcessor>();
            }).Build();

         return host;
    }
}
