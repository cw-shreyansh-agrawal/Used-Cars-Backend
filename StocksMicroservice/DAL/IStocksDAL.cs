using StocksMicroservice;

namespace StocksMicroservice.DAL;

public interface IStocksDAL
{
    Task<List<Stock>> GetStocks(StockFilterRequest request);
}
