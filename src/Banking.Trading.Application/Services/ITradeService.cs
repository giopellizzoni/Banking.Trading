using Banking.Trading.Application.DTO.InputModels;
using Banking.Trading.Application.DTO.OutputModels;
using Banking.Trading.Domain.Aggregates;

namespace Banking.Trading.Application.Services;

public interface ITradeService
{
    Task<TradeOutputModel> ExecuteTrade(TradeInputModel trade);
    Task<IEnumerable<TradeOutputModel>> GetAllTrades();
    Task<TradeOutputModel?> GetTradeById(Guid id);
}
