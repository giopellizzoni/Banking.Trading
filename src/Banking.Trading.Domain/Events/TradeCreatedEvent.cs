using Banking.Trading.Domain.Aggregates;

namespace Banking.Trading.Domain.Events;

public sealed class TradeCreatedEvent : IDomainEvent
{
    public TradeCreatedEvent(Trade trade)
    {
        Trade = trade;
    }

    public Trade Trade { get; set; }
}
