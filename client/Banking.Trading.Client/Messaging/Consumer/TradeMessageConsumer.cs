using Banking.Trading.Client.Messaging.Processor;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Banking.Trading.Client.Messaging.Consumer;

public sealed class TradeMessageConsumer : BackgroundService
{
    private readonly ILogger<TradeMessageConsumer> _logger;
    private readonly IConfiguration _configuration;
    private readonly IMessageProcessor _messageProcessor;
    private IConnection? _connection;
    private IChannel? _channel;

    private const string QueueName = "trading-execution";

    public TradeMessageConsumer(
        ILogger<TradeMessageConsumer> logger,
        IConfiguration configuration,
        IMessageProcessor messageProcessor)
    {
        _logger = logger;
        _configuration = configuration;
        _messageProcessor = messageProcessor;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQ:HostName"] ?? "localhost"
        };

        _connection = await factory.CreateConnectionAsync(cancellationToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

        await _channel.QueueDeclareAsync(queue: QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: cancellationToken);

        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += OnConsumerOnReceivedAsync;

        await _channel.BasicConsumeAsync(queue: QueueName, autoAck: true, consumer: consumer,
            cancellationToken: stoppingToken);
    }

    private async Task OnConsumerOnReceivedAsync(
        object obj,
        BasicDeliverEventArgs eventArgs)
    {
        try
        {
            await ReceiveMessageAsync(eventArgs);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while processing the message");
        }
    }

    private async Task ReceiveMessageAsync(BasicDeliverEventArgs eventArgs)
    {
        var body = eventArgs.Body.ToArray();
        await _messageProcessor.ReadMessage(body);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_channel != null && _connection != null)
        {
            await _channel.CloseAsync(cancellationToken: cancellationToken);
            await _connection.CloseAsync(cancellationToken: cancellationToken);
            _logger.LogInformation("RabbitMQ connection closed.");
        }
    }

    public override void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
        base.Dispose();
    }
}
