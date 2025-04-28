namespace Banking.Trading.Application.DTO.InputModels;

public sealed record TradeInputModel
{
    public string Asset { get; set; }
    public int Quantity { get;  set; }
    public decimal Price { get; set; }
    public DateTime ExecutedAt { get; set; }
    public Guid ClientId { get; set; }
}
