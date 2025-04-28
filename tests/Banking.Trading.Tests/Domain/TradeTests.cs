using Banking.Trading.Domain.Aggregates;
using Banking.Trading.Domain.Exceptions;
using Banking.Trading.Domain.ValueObject;

namespace Banking.Trading.Tests.Domain;

public class TradeTests
{
    [Fact]
    public void CreatingNewTrade_WithValidParameters_ShouldSuccessfullyCreateTrade()
    {
        var trade = Trade.Create(
            TradeId.Of(Guid.NewGuid()),
            Asset.Of("AAPL"),
            Quantity.Of(10),
            Price.Of(150.00m),
            ClientId.Of(Guid.NewGuid())
        );

        Assert.NotNull(trade);
        Assert.Single(trade.DomainEvents);
    }

    [Theory]
    [InlineData("d2719b6e-1c4b-4b8e-9b6e-1c4b4b8e9b6e", null, 10, 150.00, "e2719b6e-1c4b-4b8e-9b6e-1c4b4b8e9b6e")]
    [InlineData("d2719b6e-1c4b-4b8e-9b6e-1c4b4b8e9b6e", "AAPL", 0, 150.00, "e2719b6e-1c4b-4b8e-9b6e-1c4b4b8e9b6e")]
    [InlineData("d2719b6e-1c4b-4b8e-9b6e-1c4b4b8e9b6e", "AAPL", -1, 150.00, "e2719b6e-1c4b-4b8e-9b6e-1c4b4b8e9b6e")]
    [InlineData("d2719b6e-1c4b-4b8e-9b6e-1c4b4b8e9b6e", "AAPL", 10, 0.00, "e2719b6e-1c4b-4b8e-9b6e-1c4b4b8e9b6e")]
    [InlineData("d2719b6e-1c4b-4b8e-9b6e-1c4b4b8e9b6e", "AAPL", 10, -150.00, "e2719b6e-1c4b-4b8e-9b6e-1c4b4b8e9b6e")]
    public void CreatingNewTrade_WithInvalidAdata_ShouldThrowDomainException(
        string? tradeId,
        string? asset,
        int quantity,
        decimal price,
        string? clientId)
    {
        Assert.Throws<DomainException>(() =>
        {
            Trade.Create(
                TradeId.Of(Guid.Parse(tradeId)),
                Asset.Of(asset),
                Quantity.Of(quantity),
                Price.Of(price),
                ClientId.Of(Guid.Parse(clientId))
            );
        });
    }

    [Theory]
    [InlineData(null, "AAPL", 10, 150.00, "e2719b6e-1c4b-4b8e-9b6e-1c4b4b8e9b6e")]
    [InlineData("d2719b6e-1c4b-4b8e-9b6e-1c4b4b8e9b6e", "AAPL", 10, 150.00, null)]
    public void CreatingNewTrade_WithInvalidAdata_ShouldThrowArgumentNullException(
        string? tradeId,
        string? asset,
        int quantity,
        decimal price,
        string? clientId)
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            Trade.Create(
                TradeId.Of(Guid.Parse(tradeId)),
                Asset.Of(asset),
                Quantity.Of(quantity),
                Price.Of(price),
                ClientId.Of(Guid.Parse(clientId))
            );
        });
    }
}
