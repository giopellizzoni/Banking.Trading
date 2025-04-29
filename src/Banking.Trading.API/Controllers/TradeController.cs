using Banking.Trading.Application.DTO.InputModels;
using Banking.Trading.Application.Services;
using Banking.Trading.Domain.Aggregates;

using Microsoft.AspNetCore.Mvc;

namespace Banking.Trading.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TradeController : ControllerBase
{
    private readonly ILogger<TradeController> _logger;
    private readonly ITradeService _tradeService;

    public TradeController(
        ILogger<TradeController> logger,
        ITradeService tradeService)
    {
        _logger = logger;
        _tradeService = tradeService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting all trades");
        var trades = await _tradeService.GetAllTrades(cancellationToken);
        return Ok(trades);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting trade by id: {Id}", id);
        var trade = await _tradeService.GetTradeById(id, cancellationToken);

        if (trade == null)
        {
            return NotFound();
        }

        return Ok(trade);
    }

    [HttpGet("client/{userId:guid}")]
    public async Task<IActionResult> GetByClientId(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting trades by user id: {UserId}", userId);
        var trades = await _tradeService.GetAllTradesByClientId(userId, cancellationToken);

        if (!trades.Any())
        {
            return NotFound();
        }

        return Ok(trades);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TradeInputModel trade, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating a new trade");
        var result = await _tradeService.ExecuteTrade(trade, cancellationToken);

        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }
}
