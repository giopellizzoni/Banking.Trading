using Banking.Trading.Domain.Events;
using Banking.Trading.Domain.Interfaces;

namespace Banking.Trading.Infrastructure.Messaging;

public interface IMessageBus
{
    Task PublishMessageAsync(IDomainEvent message);
}
