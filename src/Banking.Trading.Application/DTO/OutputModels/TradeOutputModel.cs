namespace Banking.Trading.Application.DTO.OutputModels;

public class TradeOutputModel
{
    public Guid Id { get; set; }
    public string Asset { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime ExecutedAt { get; set; }
    public Guid ClientId { get; set; }
}
