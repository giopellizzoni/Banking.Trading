using Banking.Trading.Domain.Events;

namespace Banking.Trading.Infrastructure.Messaging;

public interface IMessageBus
{
    Task PublishMessageAsync(IDomainEvent message);
}
