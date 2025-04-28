using Banking.Trading.Application.Configs;
using Banking.Trading.Application.Services;

using Microsoft.Extensions.DependencyInjection;

namespace Banking.Trading.Application;

public static class ApplicationModule
{

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .RegisterMappings()
            .AddServices();

        return services;
    }

    private static IServiceCollection RegisterMappings(this IServiceCollection services)
    {
        MapsterConfig.RegisterMappings();
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITradeService, TradeService>();
        return services;
    }
}
