using System.Data;
using MySqlConnector;

namespace StocksMicroservice.Database;

public class MySqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public MySqlConnectionFactory(IConfiguration configuration) // IConfiguration provides access to configuration settings.
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public IDbConnection CreateConnection()
    {
        return new MySqlConnection(_connectionString); // MySqlConnection comes from the MySqlConnector package.
    }
}