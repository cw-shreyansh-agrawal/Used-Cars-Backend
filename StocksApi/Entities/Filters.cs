// Filters represents how our application internally understands those filters

using System.Collections.Generic;

namespace StocksApi.Entities;

public class Filters
{
    public List<FuelType> FuelTypes { get; set; } = new(); 

    public int? MinimumBudget { get; set; }

    public int? MaximumBudget { get; set; }

    public int? CarMakeId { get; set; }

    public int? CityId { get; set; }
}
