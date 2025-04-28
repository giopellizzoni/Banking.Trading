using Banking.Trading.Application.DTO.InputModels;
using Banking.Trading.Application.DTO.OutputModels;
using Banking.Trading.Domain.Aggregates;
using Banking.Trading.Domain.Repositories;
using Banking.Trading.Infrastructure.Messaging;

using Mapster;

using Microsoft.Extensions.Logging;

namespace Banking.Trading.Application.Services;

public sealed class TradeService : ITradeService
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

    public async Task ExecuteTrade(TradeInputModel inputModel)
    {
        _logger.LogInformation("Executing trade: {Trade}", inputModel);

        var trade = inputModel.Adapt<Trade>();

        await _tradeRepository.AddTrade(trade);

        foreach (var domainEvent in trade.DomainEvents)
        {
            _logger.LogInformation("Publishing event: {@Event}", domainEvent);
            await _messageBus.PublishMessageAsync(@domainEvent);
        }
    }

    public async Task<IEnumerable<TradeOutputModel>> GetAllTrades()
    {
        _logger.LogInformation("Getting all trades");
        var trades = await _tradeRepository.GetAllTrades();

        var allTrades = trades.ToList();
        var tradesList = allTrades
            .Select(t => t.Adapt<TradeOutputModel>());

        _logger.LogInformation("Number of trades found: {Trades}", allTrades.Count);

        return tradesList;
    }

    public async Task<TradeOutputModel?> GetTradeById(Guid id)
    {
        _logger.LogInformation("Getting trade by id: {TradeId}", id);
        var trade = await _tradeRepository.GetTradeById(id);
        if (trade != null)
        {
            _logger.LogInformation("Trade found: {Trade}", trade);
            var tradeOutputModel = trade.Adapt<TradeOutputModel>();
            return tradeOutputModel;
        }

        _logger.LogWarning("Trade not found: {TradeId}", id);
        return null;
    }
}
