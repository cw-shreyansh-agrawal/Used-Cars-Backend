using StocksApi.Entities;

namespace StocksApi.DAL;

public interface IMakesDAL
{
    Task<List<Make>> GetMakesAsync();
}