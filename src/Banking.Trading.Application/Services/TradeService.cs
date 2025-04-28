using Banking.Trading.Domain.Aggregates;
using Banking.Trading.Domain.Events;
using Banking.Trading.Domain.Repositories;
using Banking.Trading.Infrastructure.Messaging;

using Microsoft.Extensions.Logging;

namespace Banking.Trading.Application.Services;

public class TradeService : ITradeService
{
    private readonly ILogger<TradeService> _logger;
    private readonly ITradeRepository _tradeRepository;
    private readonly IMessageBus _messageBus;

    public TradeService(ITradeRepository tradeRepository,
        ILogger<TradeService> logger,
        IMessageBus messageBus)
    {
        _tradeRepository = tradeRepository;
        _logger = logger;
        _messageBus = messageBus;
    }

    public async Task ExecuteTrade(Trade trade)
    {
        _logger.LogInformation("Executing trade: {Trade}", trade);

        await _tradeRepository.AddTrade(trade);
        var @event = new TradeCreatedEvent();

        _logger.LogInformation("Trade created: {Trade}", @event);
        await _messageBus.PublishMessageAsync(@event);
    }

    public async Task<IEnumerable<Trade>> GetAllTrades()
    {
        _logger.LogInformation("Getting all trades");
        var trades = await _tradeRepository.GetAllTrades();
        return trades;
    }

    public async Task<Trade?> GetTradeById(Guid id)
    {
        _logger.LogInformation("Getting trade by id: {TradeId}", id);
        var trade = await _tradeRepository.GetTradeById(id);
        return trade;
    }
}
