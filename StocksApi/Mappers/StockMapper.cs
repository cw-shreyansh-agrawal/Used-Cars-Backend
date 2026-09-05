// This mapper is used to map Stock entity to StockDTO before returning the response to the client. It uses Mapperly library to generate the mapping code at compile time, which improves performance and reduces boilerplate code.

using Riok.Mapperly.Abstractions;
using StocksApi.DTOs;
using StocksApi.Entities;

namespace StocksApi.Mappers;

[Mapper]
public partial class StockMapper
{
    [MapProperty(nameof(Stock.Id), nameof(StockDTO.StockId))]
    [MapProperty(nameof(Stock.Kms), nameof(StockDTO.Km))]
    [MapPropertyFromSource(nameof(StockDTO.CarName), Use = nameof(MapCarName))]
    [MapPropertyFromSource(nameof(StockDTO.FormattedPrice), Use = nameof(MapFormattedPrice))]
    [MapPropertyFromSource(nameof(StockDTO.Fuel), Use = nameof(MapFuel))]
    [MapProperty(nameof(Stock.CityName), nameof(StockDTO.CityName))]
    public partial StockDTO ToStockDTO(Stock stock);

    private static string MapCarName(Stock stock)
    {
        return $"{stock.MakeYear} {stock.MakeName} {stock.ModelName}";
    }

    private static string MapFormattedPrice(Stock stock)
    {
        var priceInLakhs = stock.Price / 100000;

        return $"Rs. {priceInLakhs:0.##} Lakh"; // ## is used to remove trailing zeros after decimal point
    }

    private static string MapFuel(Stock stock)
    {
        return stock.FuelType.ToString();
    }
}
