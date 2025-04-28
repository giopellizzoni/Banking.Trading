using Banking.Trading.Domain.Abstractions;
using Banking.Trading.Domain.Events;
using Banking.Trading.Domain.ValueObject;

namespace Banking.Trading.Domain.Aggregates;

public class Trade : Aggregate
{
    public TradeId Id { get; private set; } = null!;
    public Asset Asset { get; private set; } = null!;
    public Quantity Quantity { get; private set; } = null!;
    public Price Price { get; private set; } = null!;
    public DateTime ExecutedAt { get; private set; }
    public ClientId ClientId { get; private set; } = null!;

    public static Trade Create(
        TradeId id,
        Asset asset,
        Quantity quantity,
        Price price,
        ClientId clientId)
    {
        var trade = new Trade
        {
            Id = id,
            Asset = asset,
            Quantity = quantity,
            Price = price,
            ExecutedAt = DateTime.UtcNow,
            ClientId = clientId
        };

        trade.AddDomainEvent(new TradeCreatedEvent());

        return trade;
    }
}
