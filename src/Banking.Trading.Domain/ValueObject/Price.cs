using Banking.Trading.Domain.Exceptions;

namespace Banking.Trading.Domain.ValueObject;

public sealed record Price
{
    public decimal Value { get; }

    private Price(decimal value) => Value = value;

    private Price()
    {
    }

    public static Price Of(decimal value)
    {
        if (value <= 0)
        {
            throw new DomainException("Price cannot be Zero or Negative");
        }

        return new Price(value);
    }


}
