using Banking.Trading.Domain.Aggregates;

namespace Banking.Trading.Application.Services;

public interface ITradeService
{
    Task ExecuteTrade(Trade trade);
    Task<IEnumerable<Trade>> GetAllTrades();
    Task<Trade?> GetTradeById(Guid id);
}
