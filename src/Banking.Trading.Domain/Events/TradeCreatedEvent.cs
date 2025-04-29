using Banking.Trading.Domain.Aggregates;

namespace Banking.Trading.Domain.Events;

public sealed class TradeCreatedEvent : IDomainEvent
{
    public TradeCreatedEvent(Guid tradeId,
        string asset,
        Guid clientId,
        DateTime executedAt)
    {
        TradeId = tradeId;
        Asset = asset;
        ClientId = clientId;
        ExecutedAt = executedAt;
    }

    public Guid TradeId { get; set; }
    public string Asset { get; set; }
    public Guid ClientId { get; set; }
    public DateTime ExecutedAt { get; set; }
}
