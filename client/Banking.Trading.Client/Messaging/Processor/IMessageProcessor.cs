namespace Banking.Trading.Client.Messaging.Processor;

public interface IMessageProcessor
{
    Task ReadMessage(byte[] message);
}
