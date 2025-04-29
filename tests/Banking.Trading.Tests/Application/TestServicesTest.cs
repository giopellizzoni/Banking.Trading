using Banking.Trading.Application.DTO.InputModels;
using Banking.Trading.Application.DTO.OutputModels;
using Banking.Trading.Application.Services;
using Banking.Trading.Domain.Aggregates;
using Banking.Trading.Domain.Interfaces;
using Banking.Trading.Domain.Repositories;
using Banking.Trading.Infrastructure.Messaging;

using Mapster;

using Microsoft.Extensions.Logging;

using NSubstitute;

namespace Banking.Trading.Tests.Application;

public class TestServicesTest
{
    private readonly ITradeRepository _tradeRepository;
    private readonly ILogger<TradeService> _logger;
    private readonly IMessageBus _messageBus;
    private readonly TradeService _sut;

    public TestServicesTest()
    {
        _tradeRepository = Substitute.For<ITradeRepository>();
        _logger = Substitute.For<ILogger<TradeService>>();
        _messageBus = Substitute.For<IMessageBus>();
        _sut = new TradeService(_tradeRepository, _logger, _messageBus);

        ConfigureMappings();
    }

    private void ConfigureMappings()
    {
        TypeAdapterConfig<Trade, TradeOutputModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Asset, src => src.Asset.Value)
            .Map(dest => dest.Quantity, src => src.Quantity.Value)
            .Map(dest => dest.Price, src => src.Price.Value)
            .Map(dest => dest.ExecutedAt, src => src.ExecutedAt)
            .Map(dest => dest.ClientId, src => src.ClientId.Value);
    }

    private TradeInputModel CreateInputModel(
        string asset,
        int quantity,
        decimal price,
        Guid clientId)
    {
        return new TradeInputModel
        {
            Asset = asset,
            Quantity = quantity,
            Price = price,
            ClientId = clientId
        };
    }

    private Trade CreateTradeFromInputModel(TradeInputModel inputModel)
    {
        return inputModel.ToEntity();
    }

    [Fact]
    public async Task AddTrade_Should_Call_Repository_And_Publish_Message()
    {
        var inputModel = CreateInputModel("AAPL", 10, 150.00m, Guid.NewGuid());
        var trade = CreateTradeFromInputModel(inputModel);

        var result = await _sut.ExecuteTrade(inputModel, CancellationToken.None);

        await _tradeRepository.Received(1).AddTrade(Arg.Is<Trade>(t =>
            t.Asset == trade.Asset &&
            t.Quantity == trade.Quantity &&
            t.Price == trade.Price &&
            t.ClientId == trade.ClientId), CancellationToken.None);

        await _messageBus.Received(1).PublishMessageAsync(Arg.Any<IDomainEvent>());

        Assert.NotNull(result);
        Assert.Equal(inputModel.Asset, result.Asset);
        Assert.Equal(inputModel.Quantity, result.Quantity);
        Assert.Equal(inputModel.Price, result.Price);
        Assert.Equal(inputModel.ClientId, result.ClientId);
    }

    [Fact]
    public async Task GetAllTrades_Should_ReturnListOfTradeOutputModels()
    {
        var trades = new List<Trade>
        {
            CreateTradeFromInputModel(CreateInputModel("AAPL", 10, 150.00m, Guid.NewGuid())),
            CreateTradeFromInputModel(CreateInputModel("META", 5, 200.00m, Guid.NewGuid()))
        };

        _tradeRepository.GetAllTrades(Arg.Any<CancellationToken>()).Returns(trades);

        var result = await _sut.GetAllTrades(CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(trades.Count, result.Count());
    }

    [Fact]
    public async Task GetTradeById_Should_ReturnTrade_WhenFound()
    {
        var inputModel = CreateInputModel("AAPL", 10, 150.00m, Guid.NewGuid());
        var trade = CreateTradeFromInputModel(inputModel);

        _tradeRepository.GetTradeById(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(trade);

        var result = await _sut.GetTradeById(Arg.Any<Guid>(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(inputModel.ClientId, result.ClientId);
        Assert.Equal(trade.ClientId.Value, result.ClientId);
    }

    [Fact]
    public async Task GetTradeById_Should_ReturnNull_WhenNotFound()
    {
        var id = Guid.NewGuid();
        _tradeRepository.GetTradeById(id, Arg.Any<CancellationToken>())
            .Returns((Trade?)null);

        var result = await _sut.GetTradeById(id, CancellationToken.None);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllTradesByClientId_Should_ReturnTradesForClient()
    {
        var clientId = Guid.NewGuid();

        var trades = new List<Trade>
        {
            CreateTradeFromInputModel(CreateInputModel("AAPL", 10, 150.00m, clientId)),
            CreateTradeFromInputModel(CreateInputModel("META", 5, 200.00m, clientId))
        };


        _tradeRepository.GetTradesByClientId(clientId, Arg.Any<CancellationToken>())
            .Returns(trades);

        var result = await _sut.GetAllTradesByClientId(clientId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(trades.Count, result.Count());
    }
}
