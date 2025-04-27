using Banking.Trading.Domain.Abstractions;
using Banking.Trading.Domain.Events;
using Banking.Trading.Domain.ValueObject;

namespace Banking.Trading.Domain;

public class Trade : Aggregate
{
    public TradeId Id { get; private set; }
    public Asset Asset { get; private set; }
    public Quantity Quantity { get; private set; }
    public Price Price { get; private set; }
    public DateTime ExecutedAt { get; private set; }
    public ClientId ClientId { get; private set; }

    public static Trade Create(
        TradeId id,
        Asset asset,
        Quantity quantity,
        Price price,
        DateTime executedAt,
        ClientId clientId)
    {
        var trade = new Trade
        {
            Id = id,
            Asset = asset,
            Quantity = quantity,
            Price = price,
            ExecutedAt = executedAt,
            ClientId = clientId
        };

        trade.AddDomainEvent(new TradeCreatedEvent());

        return trade;
    }
}
