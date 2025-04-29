using Banking.Trading.Domain.Events;

using MassTransit;

namespace Banking.Trading.Infrastructure.Messaging;

public sealed class MessageBus: IMessageBus
{
    private readonly IBus _bus;

    public MessageBus(IBus bus)
    {
        _bus = bus;
    }

    public async Task PublishMessageAsync(IDomainEvent message)
    {
        var uri = new Uri("queue:trading-execution");
        var endPoint = await _bus.GetSendEndpoint(uri);

        await endPoint.Send(message);
    }
}
