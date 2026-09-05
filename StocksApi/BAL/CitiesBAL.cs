using StocksApi.DAL;
using StocksApi.Entities;

namespace StocksApi.BAL;

public class CitiesBAL : ICitiesBAL
{
    private readonly ICitiesDAL _citiesDAL;

    public CitiesBAL(ICitiesDAL citiesDAL)
    {
        _citiesDAL = citiesDAL;
    }

    public async Task<List<City>> GetCitiesAsync()
    {
        var cities = await _citiesDAL.GetCitiesAsync();

        return cities
            .ToList();
    }
}