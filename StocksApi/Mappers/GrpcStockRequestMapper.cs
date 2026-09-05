using Riok.Mapperly.Abstractions;
using StocksApi.Entities;
using StocksMicroservice;

namespace StocksApi.Mappers;

[Mapper]
public partial class GrpcStockRequestMapper
{
    [MapProperty(
        nameof(Filters.FuelTypes),
        nameof(StockFilterRequest.FuelTypes),
        Use = nameof(MapFuelTypes))]
    public partial StockFilterRequest ToGrpcRequest(Filters filters);

    private static IEnumerable<int> MapFuelTypes(
        List<FuelType> fuelTypes)
    {
        return fuelTypes.Select(fuel => (int)fuel);
    }
}
