// This mapper is used to map request DTO to Filter entities.

using Riok.Mapperly.Abstractions;
using StocksApi.DTOs;
using StocksApi.Entities;

namespace StocksApi.Mappers;

[Mapper] // tells Mapperly to generate the mapping code for this class
public partial class FiltersMapper // partial class allows Mapperly to generate the implementation in a separate file
{
    [MapProperty(nameof(StockFilterRequestDto.Car), nameof(Filters.CarMakeId))] // MapProperty tells Mapperly which properties correspond to which.
    [MapProperty(nameof(StockFilterRequestDto.City), nameof(Filters.CityId))]
    [MapProperty(nameof(StockFilterRequestDto.Fuel), nameof(Filters.FuelTypes))]
    [MapProperty(nameof(StockFilterRequestDto.Budget), nameof(Filters.MinimumBudget), Use = nameof(MapMinimumBudget))] // Use tells Mapperly to use a custom mapping method for this property.
    [MapProperty(nameof(StockFilterRequestDto.Budget), nameof(Filters.MaximumBudget), Use = nameof(MapMaximumBudget))]
    public partial Filters ToFilters(StockFilterRequestDto request);

    private static List<FuelType> MapFuel(string? fuel) // incoming values: 1+2
    {
        if (string.IsNullOrWhiteSpace(fuel))
        {
            return new List<FuelType>();
        }

        return fuel
            .Split('+')
            .Select(value => int.Parse(value))
            .Select(value => (FuelType)value)
            .ToList();
    }

    private static int? MapMinimumBudget(string? budget)
    {
        if (string.IsNullOrWhiteSpace(budget))
            return null;

        var parts = budget.Split('-');

        return int.Parse(parts[0]) * 100000;
    }

    private static int? MapMaximumBudget(string? budget)
    {
        if (string.IsNullOrWhiteSpace(budget))
            return null;

        var parts = budget.Split('-');

        return int.Parse(parts[1]) * 100000;
    }
}
