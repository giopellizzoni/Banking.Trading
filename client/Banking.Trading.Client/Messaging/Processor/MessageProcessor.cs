using System.Text;
using System.Text.Json;

using Banking.Trading.Client.Model;

namespace Banking.Trading.Client.Messaging.Processor;

public class MessageProcessor: IMessageProcessor
{
    public async Task ReadMessage(byte[] message)
    {
        var body = new MemoryStream(message);
        var receivedMessage = await JsonSerializer.DeserializeAsync<MessagePayload>(body);

        if (receivedMessage != null)
        {
            Console.WriteLine("*** Message received ***");
            Console.WriteLine($"TradeId: {receivedMessage.message.tradeId}");
            Console.WriteLine($"Asset: {receivedMessage.message.asset}");
            Console.WriteLine($"ClientId: {receivedMessage.message.clientId}");
            Console.WriteLine($"ExecutedAt: {receivedMessage.message.executedAt}");
            Console.WriteLine("*** End of message ***");
            Console.WriteLine();
        }
    }
}
