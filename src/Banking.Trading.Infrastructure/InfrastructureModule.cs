using Banking.Trading.Domain.Repositories;
using Banking.Trading.Infrastructure.Data;
using Banking.Trading.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Banking.Trading.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add infrastructure services here
        services.AddDatabaseContext(configuration);
        return services;
    }

    private static IServiceCollection AddDatabaseContext(
        this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnectionString");
        services.AddDbContext<TradingDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<ITradeRepository, TradeRepository>();
        return services;
    }
}
