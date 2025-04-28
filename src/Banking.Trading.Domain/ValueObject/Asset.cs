using Banking.Trading.Domain.Exceptions;

namespace Banking.Trading.Domain.ValueObject;

public record Asset
{
    public string Value { get; }

    private Asset(string value) => Value = value;

    protected Asset()
    {
    }

    public static Asset Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Asset cannot be null or empty.");
        }

        return new Asset(value);
    }
}
