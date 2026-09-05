using Dapper;
using StocksApi.Database;
using StocksApi.Entities;

namespace StocksApi.DAL;

public class MakesDAL : IMakesDAL
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly ILogger<MakesDAL> _logger;

    public MakesDAL(IDbConnectionFactory connectionFactory, ILogger<MakesDAL> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    public async Task<List<Make>> GetMakesAsync()
    {
        try
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = @"
                SELECT
                    id AS MakeId,
                    name AS MakeName
                FROM makes
                ORDER BY name
                ";

            var makes = await connection.QueryAsync<Make>(sql);

            return makes.ToList();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to retrieve makes from database.");
            throw;
        }
    }
}