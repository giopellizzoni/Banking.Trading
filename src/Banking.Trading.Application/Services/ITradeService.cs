using Banking.Trading.Application.DTO.InputModels;
using Banking.Trading.Application.DTO.OutputModels;
using Banking.Trading.Domain.Aggregates;

namespace Banking.Trading.Application.Services;

public interface ITradeService
{
    Task<TradeOutputModel> ExecuteTrade(
        TradeInputModel trade,
        CancellationToken cancellationToken);
    Task<IEnumerable<TradeOutputModel>> GetAllTrades(CancellationToken cancellationToken);
    Task<TradeOutputModel?> GetTradeById(
        Guid id,
        CancellationToken cancellationToken);
    Task<IEnumerable<TradeOutputModel>> GetAllTradesByClientId(
        Guid userId,
        CancellationToken cancellationToken);
}
