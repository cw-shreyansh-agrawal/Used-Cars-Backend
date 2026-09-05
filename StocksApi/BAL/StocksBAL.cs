using StocksApi.DAL;
using StocksApi.Entities;
using StocksApi.DTOs;
using StocksApi.Mappers;

namespace StocksApi.BAL;

public class StocksBAL : IStocksBAL
{
    private readonly IStocksDAL _stocksDAL; // private readonly field to hold the instance of IStocksDAL, which will be injected via constructor injection.
    private readonly StockMapper _stockMapper; // private readonly field to hold the instance of IStockMapper, which will be injected via constructor injection.

    public StocksBAL(IStocksDAL stocksDAL, StockMapper stockMapper) // injecting DAL and Mapper into BAL
    {
        _stocksDAL = stocksDAL;
        _stockMapper = stockMapper;
    }

    public async Task<List<StockDTO>> GetStocksAsync(Filters filters)
    {
        var stocks = await _stocksDAL.GetStocksAsync(filters);

        var stockDTOs = stocks
            .Select(stock =>
            {
                var dto = _stockMapper.ToStockDTO(stock); // mapping Stock entity to StockDTO using the mapper

                dto.IsValueForMoney =
                    stock.Kms < 10000 &&
                    stock.Price < 200000;

                return dto;
            })
            .ToList();

        return stockDTOs;
    }
}
