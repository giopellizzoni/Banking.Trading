using Banking.Trading.Domain.Aggregates;
using Banking.Trading.Domain.Repositories;
using Banking.Trading.Domain.ValueObject;
using Banking.Trading.Infrastructure.Data;
using Banking.Trading.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Banking.Trading.Tests.Infrastructure;

public class TradeRepositoryTests
{

    private readonly TradingDbContext _dbContext;
    private readonly ITradeRepository _tradeRepository;

    public TradeRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<TradingDbContext>()
            .UseInMemoryDatabase(databaseName: "TradeTestDb")
            .Options;

        _dbContext = new TradingDbContext(options);
        _tradeRepository = new TradeRepository(_dbContext);
    }

    [Fact]
    public async Task AddingTrade_SimpleTrade_ShouldPersistTrade()
    {
        var trade = Trade.Create(
            TradeId.Of(Guid.NewGuid()),
            Asset.Of("AAPL"),
            Quantity.Of(10),
            Price.Of(150.00m),
            ClientId.Of(Guid.NewGuid())
        );

        await _tradeRepository.AddTrade(trade);
        await _dbContext.SaveChangesAsync();

        var savedTrade = await _tradeRepository.GetTradeById(trade.Id.Value);

        Assert.NotNull(savedTrade);
        Assert.Equal("AAPL", savedTrade.Asset.Value);
    }

    [Fact]
    public async Task GetTradeById_SimpleTrade_ShouldReturnTrade()
    {
        var trade = Trade.Create(
            TradeId.Of(Guid.NewGuid()),
            Asset.Of("AAPL"),
            Quantity.Of(10),
            Price.Of(150.00m),
            ClientId.Of(Guid.NewGuid())
        );

        await _tradeRepository.AddTrade(trade);
        await _dbContext.SaveChangesAsync();

        var fetchedTrade = await _tradeRepository.GetTradeById(trade.Id.Value);

        Assert.NotNull(fetchedTrade);
        Assert.Equal(trade.Id.Value, fetchedTrade.Id.Value);
    }
}
