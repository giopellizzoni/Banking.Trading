using Banking.Trading.Domain.Aggregates;
using Banking.Trading.Domain.Exceptions;
using Banking.Trading.Domain.ValueObject;

namespace Banking.Trading.Tests.Domain;

public class TradeTests
{
    private Trade CreateTrade(
        string asset,
        int quantity,
        decimal price,
        Guid clientId)
    {
        return Trade.Create(
            Asset.Of(asset),
            Quantity.Of(quantity),
            Price.Of(price),
            ClientId.Of(clientId)
        );
    }

    [Fact]
    public void CreatingNewTrade_WithValidParameters_ShouldSuccessfullyCreateTrade()
    {
        var trade = CreateTrade("AAPL", 10, 150.00m, Guid.NewGuid());

        Assert.NotNull(trade);
        Assert.Single(trade.DomainEvents);
    }

    [Theory]
    [InlineData(null, 10, 150.00, "e2719b6e-1c4b-4b8e-9b6e-1c4b4b8e9b6e")]
    [InlineData("AAPL", 0, 150.00, "e2719b6e-1c4b-4b8e-9b6e-1c4b4b8e9b6e")]
    [InlineData("AAPL", -1, 150.00, "e2719b6e-1c4b-4b8e-9b6e-1c4b4b8e9b6e")]
    [InlineData("AAPL", 10, 0.00, "e2719b6e-1c4b-4b8e-9b6e-1c4b4b8e9b6e")]
    [InlineData("AAPL", 10, -150.00, "e2719b6e-1c4b-4b8e-9b6e-1c4b4b8e9b6e")]
    public void CreatingNewTrade_WithInvalidAdata_ShouldThrowDomainException(
        string? asset,
        int quantity,
        decimal price,
        string? clientId)
    {
        Assert.Throws<DomainException>(() =>
            CreateTrade(asset, quantity, price, Guid.Parse(clientId)));
    }

    [Fact]
    public void CreatingNewTrade_WithInvalidAdata_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            CreateTrade("AAPL", 10, 150.00m, Guid.Parse(null)));
    }
}
