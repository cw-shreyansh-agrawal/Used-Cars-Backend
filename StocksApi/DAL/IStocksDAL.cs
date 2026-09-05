using StocksApi.Entities;

namespace StocksApi.DAL;

public interface IStocksDAL
{
    Task<List<Stock>> GetStocksAsync(Filters filters); // Task represents an asynchronous operation that can return a value.
}
