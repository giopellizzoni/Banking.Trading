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

    private Trade( Asset asset,
        Quantity quantity,
        Price price,
        ClientId clientId)
    {
        Id = TradeId.Of(Guid.NewGuid());
        Asset = asset;
        Quantity = quantity;
        Price = price;
        ExecutedAt = DateTime.UtcNow;
        ClientId = clientId;
        AddDomainEvent(new TradeCreatedEvent(Id.Value, asset.Value, clientId.Value, ExecutedAt));
    }

    public static Trade Create(
        Asset asset,
        Quantity quantity,
        Price price,
        ClientId clientId)
    {
        var trade = new Trade(asset, quantity, price, clientId);

        return trade;
    }
}
