using Grpc.Core;
using Moq;
using StocksMicroservice;
using StocksMicroservice.DAL;
using StocksMicroservice.Services;
using Xunit;
using Microsoft.Extensions.Logging;

namespace StocksMicroservice.Tests.Services;

public class StocksGrpcServiceTests
{
    [Fact]
    public async Task GetStocks_ReturnsStocksFromDAL()
    {
        // Arrange
        var stocks = new List<Stock>
        {
            new Stock
            {
                Id = 1,
                MakeName = "Hyundai",
                ModelName = "Creta",
                MakeYear = 2023,
                Price = 650000,
                Kms = 8000,
                FuelType = 1,
                MakeId = 10,
                CityId = 5
            }
        };

        var dalMock = new Mock<IStocksDAL>(); // mock the IStocksDAL interface

        dalMock
            .Setup(dal => dal.GetStocks(It.IsAny<StockFilterRequest>())) // tell what to return
            .ReturnsAsync(stocks);

        var loggerMock = new Mock<ILogger<StocksGrpcService>>();

        var service = new StocksGrpcService(dalMock.Object, loggerMock.Object); // create an instance of the service with the mocked DAL

        var request = new StockFilterRequest
        {
            MaximumBudget = 700000
        };

        // Act
        var response = await service.GetStocks(
            request,
            null!); // passing null for ServerCallContext since it's not used in the method

        // Assert
        Assert.Single(response.Stocks); // check that there is exactly one stock in the response
        Assert.Equal(1, response.Stocks[0].Id);
        Assert.Equal("Hyundai", response.Stocks[0].MakeName);

        dalMock.Verify(
            dal => dal.GetStocks(
                It.IsAny<StockFilterRequest>()), // It.IsAny is used to verify that the method was called with any instance of StockFilterRequest
            Times.Once);
    }
}
