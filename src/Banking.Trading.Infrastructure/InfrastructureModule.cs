using Banking.Trading.Domain.Repositories;
using Banking.Trading.Infrastructure.Data;
using Banking.Trading.Infrastructure.Messaging;
using Banking.Trading.Infrastructure.Repositories;

using MassTransit;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Banking.Trading.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDatabaseContext(configuration)
            .AddRepositories()
            .AddMessaging()
            .AddServiceBus(configuration);

        return services;

    }

    private static IServiceCollection AddDatabaseContext(
        this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<TradingDbContext>(options => options.UseSqlServer(connectionString));
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITradeRepository, TradeRepository>();
        return services;
    }

    private static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddSingleton<IMessageBus, MessageBus>();
        return services;
    }

    private static IServiceCollection AddServiceBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();
            config.UsingRabbitMq((
                context,
                configurator) =>
            {

                var hostUri = new Uri(configuration["RabbitMq:Host"]!);
                configurator.Host(hostUri, configure =>
                {
                    configure.Username(configuration["RabbitMq:Username"]!);
                    configure.Password(configuration["RabbitMq:Password"]!);
                });
                configurator.ConfigureEndpoints(context);
            });

        });
        return services;
    }
}
