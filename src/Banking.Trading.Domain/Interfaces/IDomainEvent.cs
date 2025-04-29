namespace Banking.Trading.Domain.Interfaces;

public interface IDomainEvent
{
    Guid EventId => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.UtcNow;
    public string? EventTime => GetType().AssemblyQualifiedName;
}
