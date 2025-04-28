using Banking.Trading.Domain.Aggregates;

using Microsoft.EntityFrameworkCore;

namespace Banking.Trading.Infrastructure.Data;

public class TradingDbContext : DbContext
{
    public TradingDbContext(DbContextOptions<TradingDbContext> options): base(options)
    {
    }

    public DbSet<Trade> Trades => Set<Trade>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TradingDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
