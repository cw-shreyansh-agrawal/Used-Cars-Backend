using StocksApi.Entities;

namespace StocksApi.DAL;

public interface ICitiesDAL
{
    Task<List<City>> GetCitiesAsync();
}