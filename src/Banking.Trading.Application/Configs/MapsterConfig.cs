using Banking.Trading.Application.DTO.InputModels;
using Banking.Trading.Application.DTO.OutputModels;
using Banking.Trading.Domain.Aggregates;
using Banking.Trading.Domain.ValueObject;

using Mapster;

namespace Banking.Trading.Application.Configs;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<TradeInputModel, Trade>.NewConfig()
            .Map(dest => dest.Id, src => TradeId.Of(Guid.NewGuid()))
            .Map(dest => dest.Asset, src => Asset.Of(src.Asset))
            .Map(dest => dest.Quantity, src => Quantity.Of(src.Quantity))
            .Map(dest => dest.Price, src => Price.Of(src.Price))
            .Map(dest => dest.ExecutedAt, src => DateTime.UtcNow)
            .Map(dest => dest.ClientId, src => ClientId.Of(src.ClientId));

        TypeAdapterConfig<Trade, TradeOutputModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Asset, src => src.Asset.Value)
            .Map(dest => dest.Quantity, src => src.Quantity.Value)
            .Map(dest => dest.Price, src => src.Price.Value)
            .Map(dest => dest.ExecutedAt, src => src.ExecutedAt)
            .Map(dest => dest.ClientId, src => src.ClientId.Value);
    }
}
