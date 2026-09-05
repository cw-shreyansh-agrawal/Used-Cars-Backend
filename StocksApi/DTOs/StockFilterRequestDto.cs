// DTOs are used here to map the request to a C# object
// The DTO represents the API contract

namespace StocksApi.DTOs;

public class StockFilterRequestDto
{
    public string? Fuel { get; set; }
    public string? Budget { get; set; }
    public decimal? Car { get; set; }
    public decimal? City { get; set; }
}
