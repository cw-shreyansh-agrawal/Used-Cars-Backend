using System.Data;

namespace StocksMicroservice.Database;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection(); // IdbConnection is an interface that represents a connection to a database. It comes from the System.Data namespace. 
}
