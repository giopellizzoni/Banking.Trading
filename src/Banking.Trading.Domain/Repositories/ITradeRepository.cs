using Banking.Trading.Domain.Aggregates;

namespace Banking.Trading.Domain.Repositories;
public interface ITradeRepository
{
    Task AddTrade(Trade trade);
    Task<IEnumerable<Trade>> GetAllTrades();
    Task<Trade?> GetTradeById(Guid id);
}
