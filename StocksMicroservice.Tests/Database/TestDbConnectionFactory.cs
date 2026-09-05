using System.Data;
using MySqlConnector;
using StocksMicroservice.Database;

namespace StocksMicroservice.Tests.Database;

public class TestDbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public TestDbConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}
