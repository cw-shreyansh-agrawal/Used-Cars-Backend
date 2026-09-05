using StocksApi.Entities;
using StocksMicroservice; // generated from the proto file
using Grpc.Core; // For RpcException
using ApiStock = StocksApi.Entities.Stock;
using StocksApi.Mappers;

namespace StocksApi.DAL;


public class StocksDAL : IStocksDAL
{
    private readonly StocksService.StocksServiceClient _stocksClient; // StocksServiceClient is generated from the proto file.
    private readonly ILogger<StocksDAL> _logger;
    private readonly GrpcStockMapper _stockMapper; // GrpcStockMapper is used to map gRPC Stock objects to API Stock objects.
    private readonly GrpcStockRequestMapper _stockRequestMapper; // GrpcStockRequestMapper is used to map API Filter objects to gRPC StockFilterRequest objects.    

    public StocksDAL(StocksService.StocksServiceClient stocksClient, ILogger<StocksDAL> logger, GrpcStockMapper stockMapper, GrpcStockRequestMapper stockRequestMapper)
    {
        _stocksClient = stocksClient;
        _logger = logger;
        _stockMapper = stockMapper;
        _stockRequestMapper = stockRequestMapper;
    }

    public async Task<List<ApiStock>> GetStocksAsync(Filters filters)
    {
        try{
            // Convert API Filters → gRPC request
            var request = _stockRequestMapper.ToGrpcRequest(filters); // StockFilterRequest is generated from the proto file.

            var response = await _stocksClient.GetStocksAsync(request);

            // Convert gRPC Stock objects into API Stock objects
            return response.Stocks
                .Select(stock => _stockMapper.ToApiStock(stock))
                .ToList();
            }
        catch(RpcException ex) // Catching RpcException to handle gRPC specific errors
        {
            _logger.LogError(ex, "Failed to retrieve stocks from StocksMicroservice.");
            throw; // Re-throw the exception to be handled by the middleware or higher-level exception handlers.
        }
    }
}
