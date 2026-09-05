using StocksMicroservice;
using StocksMicroservice.DAL;
using StocksMicroservice.Tests.Database;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;

namespace StocksMicroservice.Tests.DAL;

public class StocksDALTests
{
    private readonly IStocksDAL _stocksDAL;

    public StocksDALTests()
    {
        var connectionString = Environment.GetEnvironmentVariable("TEST_DB_CONNECTION_STRING");
        var connectionFactory = new TestDbConnectionFactory(connectionString);
        var logger = new Mock<ILogger<StocksDAL>>();

        _stocksDAL = new StocksDAL(connectionFactory, logger.Object);
    }

    [Fact]
    public async Task GetStocks_ReturnsAllStocks_WhenNoFiltersProvided()
    {
        // Arrange
        var request = new StockFilterRequest();

        // Act
        var result = await _stocksDAL.GetStocks(request);

        // Assert
        Assert.Equal(8, result.Count);
    }

    [Fact]
    public async Task GetStocks_WithMaximumBudget_ReturnsMatchingStocks()
    {
        // Arrange
        var request = new StockFilterRequest
        {
            MaximumBudget = 700000
        };

        // Act
        var result = await _stocksDAL.GetStocks(request);

        // Assert
        Assert.Equal(4, result.Count);

        Assert.All(
            result,
            stock => Assert.True(stock.Price <= 700000));
    }

    [Fact]
    public async Task GetStocks_WithPetrolFilter_ReturnsPetrolStocks()
    {
        // Arrange
        var request = new StockFilterRequest();

        request.FuelTypes.Add(1);

        // Act
        var result = await _stocksDAL.GetStocks(request);

        // Assert
        Assert.Equal(3, result.Count);

        Assert.All(
            result,
            stock => Assert.Equal(1, stock.FuelType));
    }

    [Fact]
    public async Task GetStocks_WithMultipleFuelTypes_ReturnsMatchingStocks()
    {
        // Arrange
        var request = new StockFilterRequest();

        request.FuelTypes.Add(1);
        request.FuelTypes.Add(2);

        // Act
        var result = await _stocksDAL.GetStocks(request);

        // Assert
        Assert.Equal(8, result.Count);

        Assert.All(
            result,
            stock => Assert.Contains(
                stock.FuelType,
                new[] { 1, 2 }));
    }

    [Fact]
    public async Task GetStocks_WithMakeAndCity_ReturnsMatchingStocks()
    {
        // Arrange
        var request = new StockFilterRequest
        {
            CarMakeId = 50,
            CityId = 7
        };

        // Act
        var result = await _stocksDAL.GetStocks(request);

        // Assert
        Assert.Single(result);

        Assert.Equal("Honda", result[0].MakeName);
        Assert.Equal("Civic [2010-2013]", result[0].ModelName);
    }

    [Fact]
    public async Task GetStocks_ReturnsStocksSortedByPrice()
    {
        // Arrange
        var request = new StockFilterRequest();

        // Act
        var result = await _stocksDAL.GetStocks(request);

        // Assert
        var prices = result
            .Select(stock => stock.Price)
            .ToList();

        Assert.Equal(
            prices.OrderBy(price => price),
            prices);
    }   

}