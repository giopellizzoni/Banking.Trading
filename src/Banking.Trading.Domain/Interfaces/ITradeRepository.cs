using Banking.Trading.Domain.Aggregates;

namespace Banking.Trading.Domain.Repositories;
public interface ITradeRepository
{
    Task AddTrade(
        Trade trade,
        CancellationToken cancellationToken);
    Task<IEnumerable<Trade>> GetAllTrades(CancellationToken cancellationToken);
    Task<Trade?> GetTradeById(
        Guid id,
        CancellationToken cancellationToken);
    Task<IEnumerable<Trade>> GetTradesByClientId(
        Guid id,
        CancellationToken cancellationToken);
}
