using StocksApi.Entities;

namespace StocksApi.BAL;

public interface IMakesBAL
{
    Task<List<Make>> GetMakesAsync();
}