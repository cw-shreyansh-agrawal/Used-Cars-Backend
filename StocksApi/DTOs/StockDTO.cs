// This dto is used to return the data to the client. Being a dto, its used at api boundaries.

namespace StocksApi.DTOs;

public class StockDTO
{
    public int StockId { get; set; }

    public string CarName { get; set; } = string.Empty;

    public string FormattedPrice { get; set; } = string.Empty;

    public int Km { get; set; }

    public string Fuel { get; set; } = string.Empty;

    public string CityName { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public List<string> StockImages { get; set; } = new();

    public bool IsValueForMoney { get; set; }
}
