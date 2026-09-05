using StocksApi.Entities;
using StocksApi.DTOs;

namespace StocksApi.BAL;

public interface IStocksBAL
{
    Task<List<StockDTO>> GetStocksAsync(Filters filters);
}
