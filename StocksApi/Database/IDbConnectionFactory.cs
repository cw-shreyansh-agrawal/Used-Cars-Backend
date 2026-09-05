using System.Data;

namespace StocksApi.Database;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}