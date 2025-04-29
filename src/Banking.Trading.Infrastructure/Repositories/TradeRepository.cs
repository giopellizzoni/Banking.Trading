using Banking.Trading.Domain.Aggregates;
using Banking.Trading.Domain.Repositories;
using Banking.Trading.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace Banking.Trading.Infrastructure.Repositories;

public class TradeRepository: ITradeRepository
{
    private readonly TradingDbContext _dbContext;

    public TradeRepository(TradingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddTrade(
        Trade trade,
        CancellationToken cancellationToken)
    {
        await _dbContext.Trades.AddAsync(trade, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Trade>> GetAllTrades(CancellationToken cancellationToken)
    {
        return await _dbContext.Trades.ToListAsync(cancellationToken);
    }

    public async Task<Trade?> GetTradeById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var trade = await _dbContext.Trades
            .FirstOrDefaultAsync(t => t.Id.Value == id, cancellationToken);
        return trade;
    }

    public async Task<IEnumerable<Trade>> GetTradesByClientId(
        Guid id,
        CancellationToken cancellationToken)
    {
        var trades = await _dbContext.Trades
            .Where(t => t.ClientId.Value == id)
            .ToListAsync(cancellationToken);
        return trades;
    }
}
