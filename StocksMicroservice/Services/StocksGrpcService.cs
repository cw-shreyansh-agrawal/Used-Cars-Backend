using Grpc.Core;
using StocksMicroservice.DAL;

namespace StocksMicroservice.Services;

public class StocksGrpcService: StocksService.StocksServiceBase // StocksServiceBase is generated from the .proto file
{
    private readonly IStocksDAL _stocksDAL;
    private readonly ILogger<StocksGrpcService> _logger;

    public StocksGrpcService(IStocksDAL stocksDAL, ILogger<StocksGrpcService> logger)
    {
        _stocksDAL = stocksDAL;
        _logger = logger;
    }

    public override async Task<StockResponse> GetStocks( // provides the implementation for the GetStocks method defined in the .proto file
        StockFilterRequest request,
        ServerCallContext context) // context provides information about the gRPC call, such as headers, cancellation tokens, etc.
    {
        _logger.LogInformation("Received GetStocks request.");

        var stocks = await _stocksDAL.GetStocks(request);

        var response = new StockResponse();

        response.Stocks.AddRange(stocks);

        _logger.LogInformation("Returning {StockCount} stocks.", response.Stocks.Count);

        return response;
    }
}
 