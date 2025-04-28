using Banking.Trading.Domain.Exceptions;

namespace Banking.Trading.Domain.ValueObject;

public sealed record Quantity
{
    public int Value { get; }

    private Quantity(int value) => Value = value;

    private Quantity()
    {
    }

    public static Quantity Of(int value)
    {
        if (value <= 0)
        {
            throw new DomainException("Quantity cannot be Zero or Negative");
        }

        return new Quantity(value);
    }
}
