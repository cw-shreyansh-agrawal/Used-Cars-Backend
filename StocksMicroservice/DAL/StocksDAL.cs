using StocksMicroservice.Database;
using Dapper;

namespace StocksMicroservice.DAL;

public class StocksDAL : IStocksDAL
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly ILogger<StocksDAL> _logger;

    public StocksDAL(IDbConnectionFactory connectionFactory, ILogger<StocksDAL> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    public async Task<List<Stock>> GetStocks(StockFilterRequest request)
    {
        try{
            using var connection = _connectionFactory.CreateConnection(); // using statement ensures that the connection is disposed of properly after use, preventing resource leaks.

            string get_stocks_query = @"
                SELECT
                    s.id,
                    m.name AS MakeName,
                    s.model_name AS ModelName,
                    s.make_year AS MakeYear,
                    s.price AS Price,
                    s.kms AS Kms,
                    s.fuel_type AS FuelType,
                    s.make_id AS MakeId,
                    s.city_id AS CityId,
                    c.name AS CityName
                FROM stocks s
                INNER JOIN makes m
                    ON s.make_id = m.id
                INNER JOIN cities c
                    ON s.city_id = c.id
                WHERE 1 = 1
            ";

            DynamicParameters parameters = new DynamicParameters();

            if (request.FuelTypes.Count > 0)
            {
                get_stocks_query += " AND fuel_type IN @FuelTypes";
                parameters.Add(
                    "FuelTypes",
                    request.FuelTypes);
            }

            if (request.MinimumBudget > 0)
            {
                get_stocks_query += " AND price >= @MinimumBudget";
                parameters.Add(
                    "MinimumBudget",
                    request.MinimumBudget);
            }

            if (request.MaximumBudget > 0)
            {
                get_stocks_query += " AND price <= @MaximumBudget";
                parameters.Add(
                    "MaximumBudget",
                    request.MaximumBudget);
            }

            if (request.CarMakeId > 0)
            {
                get_stocks_query += " AND make_id = @CarMakeId";
                parameters.Add(
                    "CarMakeId",
                    request.CarMakeId);
            }

            if (request.CityId > 0)
            {
                get_stocks_query += " AND city_id = @CityId";
                parameters.Add(
                    "CityId",
                    request.CityId);
            }

            get_stocks_query += " ORDER BY price ASC";

            var stocks = (await connection.QueryAsync<Stock>( // do not block the thread during I/O operations
                get_stocks_query,
                parameters)).ToList();

            if (stocks.Count == 0)
            {
                return stocks;
            }

            var stockIds = stocks
                .Select(stock => stock.Id)
                .ToList();

            const string get_images_query = @"
                SELECT
                    stock_id AS StockId,
                    image_url AS ImageUrl
                FROM stock_images
                WHERE stock_id IN @StockIds
                ORDER BY id ASC
                ";

            var images = await connection.QueryAsync<(int StockId, string ImageUrl)>(
                get_images_query,
                new { StockIds = stockIds });

            var imagesByStockId = images
                .GroupBy(image => image.StockId) // group images by stock id
                .ToDictionary(
                    group => group.Key,
                    group => group
                        .Select(image => image.ImageUrl)
                        .ToList());

            foreach (var stock in stocks)
            {
                if (imagesByStockId.TryGetValue(
                    stock.Id,
                    out var stockImages))
                {
                    stock.StockImages.AddRange(stockImages);
                }
            }

            return stocks;
        
    }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching stocks from the database.");
            throw; // Re-throw the exception to be handled by the middleware or higher-level exception handlers.
        }
    }
}
