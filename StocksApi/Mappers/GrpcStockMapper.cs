// This mapper is used to map gRPC Stock objects to API Stock objects.

using Riok.Mapperly.Abstractions;
using StocksApi.Entities;
using StocksMicroservice;

namespace StocksApi.Mappers;

[Mapper]
public partial class GrpcStockMapper
{
    [MapProperty(
        nameof(StocksMicroservice.Stock.FuelType),
        nameof(StocksApi.Entities.Stock.FuelType),
        Use = nameof(MapFuelType))]

    [MapProperty(
        nameof(StocksMicroservice.Stock.Price),
        nameof(StocksApi.Entities.Stock.Price),
        Use = nameof(MapPrice))]

    [MapProperty(
        nameof(StocksMicroservice.Stock.StockImages),
        nameof(StocksApi.Entities.Stock.StockImages),
        Use = nameof(MapImages))]

    public partial StocksApi.Entities.Stock ToApiStock(
        StocksMicroservice.Stock stock);

    private static FuelType MapFuelType(int fuelType)
    {
        return (FuelType)fuelType;
    }

    private static decimal MapPrice(double price)
    {
        return (decimal)price;
    }

    private static List<string> MapImages(IEnumerable<string> images)
    {
        return images.ToList();
    }
}
