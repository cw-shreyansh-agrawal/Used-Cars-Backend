using StocksApi.Entities;
using StocksMicroservice; // generated from the proto file
using Grpc.Core; // For RpcException
using ApiStock = StocksApi.Entities.Stock;

namespace StocksApi.DAL;


public class StocksDAL : IStocksDAL
{
    private readonly StocksService.StocksServiceClient _stocksClient; // StocksServiceClient is generated from the proto file.
    private readonly ILogger<StocksDAL> _logger;

    public StocksDAL(StocksService.StocksServiceClient stocksClient, ILogger<StocksDAL> logger)
    {
        _stocksClient = stocksClient;
        _logger = logger;
    }

    public async Task<List<ApiStock>> GetStocksAsync(Filters filters)
    {
        try{
            var request = new StockFilterRequest(); // StockFilterRequest is generated from the proto file.

        request.FuelTypes.AddRange(
            filters.FuelTypes.Select(fuel => (int)fuel));

        if (filters.MinimumBudget.HasValue)
        {
            request.MinimumBudget = filters.MinimumBudget.Value;
        }

        if (filters.MaximumBudget.HasValue)
        {
            request.MaximumBudget = filters.MaximumBudget.Value;
        }

        if (filters.CarMakeId.HasValue)
        {
            request.CarMakeId = filters.CarMakeId.Value;
        }

        if (filters.CityId.HasValue)
        {
            request.CityId = filters.CityId.Value;
        }

        var response = await _stocksClient.GetStocksAsync(request);

        return response.Stocks
            .Select(stock => new ApiStock // The generated gRPC Stock and our application Stock are two different types. So grpc stock is converted to our application stock
            {
                Id = stock.Id,
                MakeName = stock.MakeName,
                ModelName = stock.ModelName,
                MakeYear = stock.MakeYear,
                Price = (decimal)stock.Price,
                Kms = stock.Kms,
                FuelType = (FuelType)stock.FuelType,
                MakeId = stock.MakeId,
                CityId = stock.CityId,
                CityName = stock.CityName,
                StockImages = stock.StockImages.ToList()
            })
            .ToList();
        }
        catch(RpcException ex) // Catching RpcException to handle gRPC specific errors
        {
            _logger.LogError(ex, "Failed to retrieve stocks from StocksMicroservice.");
            throw; // Re-throw the exception to be handled by the middleware or higher-level exception handlers.
        }
    }
}
