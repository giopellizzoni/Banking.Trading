using Banking.Trading.Domain.Exceptions;

namespace Banking.Trading.Domain.ValueObject;

public sealed record TradeId
{
    public Guid Value { get; }
    private TradeId(Guid value) => Value = value;

    private TradeId()
    {
    }

    public static TradeId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("TradeId cannot be empty");
        }

        return new TradeId(value);
    }

}
