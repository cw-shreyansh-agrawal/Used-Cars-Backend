using Dapper;
using StocksApi.Database;
using StocksApi.Entities;

namespace StocksApi.DAL;

public class CitiesDAL : ICitiesDAL
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly ILogger<CitiesDAL> _logger;

    public CitiesDAL(IDbConnectionFactory connectionFactory, ILogger<CitiesDAL> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    public async Task<List<City>> GetCitiesAsync()
    {
        try
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = @"
                SELECT
                    id AS CityId,
                    name AS CityName
                FROM cities
                ORDER BY name
                ";

            var cities = await connection.QueryAsync<City>(sql);

            return cities.ToList();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to retrieve cities from database.");
            throw;
        }
    }
}