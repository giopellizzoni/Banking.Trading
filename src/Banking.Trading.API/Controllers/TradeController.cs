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
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Getting all trades");
        var trades = await _tradeService.GetAllTrades();
        return Ok(trades);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        _logger.LogInformation("Getting trade by id: {Id}", id);
        var trade = await _tradeService.GetTradeById(id);

        if (trade == null)
        {
            return NotFound();
        }

        return Ok(trade);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TradeInputModel trade)
    {
        _logger.LogInformation("Creating a new trade");
        var result = await _tradeService.ExecuteTrade(trade);

        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }
}
