using StocksApi.Entities;

namespace StocksApi.BAL;

public interface ICitiesBAL
{
    Task<List<City>> GetCitiesAsync();
}