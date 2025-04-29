using Banking.Trading.Domain.Aggregates;

namespace Banking.Trading.Application.DTO.InputModels;

public sealed record TradeInputModel
{
    public string Asset { get; set; }
    public int Quantity { get;  set; }
    public decimal Price { get; set; }
    public DateTime ExecutedAt { get; set; }
    public Guid ClientId { get; set; }

    public Trade ToEntity()
    {
        return Trade.Create(
            Domain.ValueObject.Asset.Of(Asset),
            Domain.ValueObject.Quantity.Of(Quantity),
            Domain.ValueObject.Price.Of(Price),
            Domain.ValueObject.ClientId.Of(ClientId)
        );

    }
}
