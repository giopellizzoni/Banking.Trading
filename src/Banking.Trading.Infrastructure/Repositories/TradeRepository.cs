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

    public async Task AddTrade(Trade trade)
    {
        await _dbContext.Trades.AddAsync(trade);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Trade>> GetAllTrades()
    {
        return await _dbContext.Trades.ToListAsync();
    }

    public async Task<Trade?> GetTradeById(Guid id)
    {
        var trade = await _dbContext.Trades
            .FirstOrDefaultAsync(t => t.Id.Value == id);
        return trade;
    }
}
