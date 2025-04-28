using Banking.Trading.Domain.Events;

namespace Banking.Trading.Infrastructure.Messaging;

public class MessageBus: IMessageBus
{
    public Task PublishMessageAsync(IDomainEvent message)
    {
        throw new NotImplementedException();
    }
}
