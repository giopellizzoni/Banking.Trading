using Banking.Trading.Domain.Aggregates;
using Banking.Trading.Domain.Repositories;

using Microsoft.Extensions.Logging;

namespace Banking.Trading.Application.Services;

public class TradeService : ITradeService
{
    private readonly ILogger<TradeService> _logger;
    private readonly ITradeRepository _tradeRepository;

    public TradeService(ITradeRepository tradeRepository,
        ILogger<TradeService> logger)
    {
        _tradeRepository = tradeRepository;
        _logger = logger;
    }

    public async Task ExecuteTrade(Trade trade)
    {
        _logger.LogInformation("Executing trade: {Trade}", trade);
        await _tradeRepository.AddTrade(trade);
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
