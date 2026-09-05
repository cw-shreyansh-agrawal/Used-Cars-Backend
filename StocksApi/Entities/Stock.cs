namespace StocksApi.Entities;

public class Stock
{
    public int Id { get; set; }
    public string MakeName { get; set; } = string.Empty;
    public string ModelName { get; set; } = string.Empty;
    public int MakeYear { get; set; }
    public decimal Price { get; set; }
    public int Kms { get; set; }
    public FuelType FuelType { get; set; }
    public int MakeId { get; set; }
    public int CityId { get; set; }
    public string CityName { get; set; } = string.Empty;
    public List<string> StockImages { get; set; } = new();
}
