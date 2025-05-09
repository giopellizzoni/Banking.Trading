﻿using Banking.Trading.Application.DTO.InputModels;
using Banking.Trading.Application.DTO.OutputModels;
using Banking.Trading.Domain.Aggregates;
using Banking.Trading.Domain.Repositories;
using Banking.Trading.Domain.ValueObject;
using Banking.Trading.Infrastructure.Messaging;

using Mapster;

using Microsoft.Extensions.Logging;

namespace Banking.Trading.Application.Services;

public sealed class TradeService : ITradeService
{
    private readonly ILogger<TradeService> _logger;
    private readonly ITradeRepository _tradeRepository;
    private readonly IMessageBus _messageBus;

    public TradeService(
        ITradeRepository tradeRepository,
        ILogger<TradeService> logger,
        IMessageBus messageBus)
    {
        _tradeRepository = tradeRepository;
        _logger = logger;
        _messageBus = messageBus;
    }

    public async Task<TradeOutputModel> ExecuteTrade(
        TradeInputModel inputModel,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing trade: {Trade}", inputModel);

        var trade = inputModel.ToEntity();

        await _tradeRepository.AddTrade(trade, cancellationToken);

        foreach (var domainEvent in trade.DomainEvents)
        {
            _logger.LogInformation("Publishing event: {@Event}", domainEvent);
            await _messageBus.PublishMessageAsync(@domainEvent);
        }

        var outputModel = trade.Adapt<TradeOutputModel>();
        return outputModel;
    }

    public async Task<IEnumerable<TradeOutputModel>> GetAllTrades(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all trades");
        var trades = await _tradeRepository.GetAllTrades(cancellationToken);

        var allTrades = trades.ToList();
        var tradesList = allTrades
            .Select(t => t.Adapt<TradeOutputModel>());

        _logger.LogInformation("Number of trades found: {Trades}", allTrades.Count);

        return tradesList;
    }

    public async Task<TradeOutputModel?> GetTradeById(
        Guid id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting trade by id: {TradeId}", id);
        var trade = await _tradeRepository.GetTradeById(id, cancellationToken);
        if (trade != null)
        {
            _logger.LogInformation("Trade found: {Trade}", trade);
            var tradeOutputModel = trade.Adapt<TradeOutputModel>();
            return tradeOutputModel;
        }

        _logger.LogWarning("Trade not found: {TradeId}", id);
        return null;
    }

    public async Task<IEnumerable<TradeOutputModel>> GetAllTradesByClientId(
        Guid userId,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all trades by client id: {ClientId}", userId);
        var trades = await _tradeRepository.GetTradesByClientId(userId, cancellationToken);

        var allTrades = trades.ToList();
        var tradesList = allTrades
            .Select(t => t.Adapt<TradeOutputModel>());

        _logger.LogInformation("Number of trades found: {Trades}", allTrades.Count);

        return tradesList;
    }
}
