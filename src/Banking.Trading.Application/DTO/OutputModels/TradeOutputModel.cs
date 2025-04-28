namespace Banking.Trading.Application.DTO.OutputModels;

public class TradeOutputModel
{
    public Guid Id { get; private set; }
    public string Asset { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public DateTime ExecutedAt { get; private set; }
    public Guid ClientId { get; private set; }
}
