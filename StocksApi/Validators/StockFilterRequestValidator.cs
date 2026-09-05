using StocksApi.DTOs;
using StocksApi.Entities;

namespace StocksApi.Validators;

public static class StockFilterRequestValidator
{
    public static List<string> Validate(StockFilterRequestDto request)
    {
        var errors = new List<string>();

        ValidateFuel(request.Fuel, errors);
        ValidateBudget(request.Budget, errors);

        return errors;
    }

    private static void ValidateFuel(string? fuel, List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(fuel))
        {
            return;
        }

        var fuelValues = fuel.Split('+');

        foreach (var value in fuelValues)
        {
            if (!int.TryParse(value, out var fuelId) ||
                !Enum.IsDefined(typeof(FuelType), fuelId))
            {
                errors.Add($"Invalid fuel type: {value}");
            }
        }
    }

    private static void ValidateBudget(string? budget, List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(budget))
        {
            return;
        }

        var values = budget.Split('-');

        if (values.Length != 2 ||
            !decimal.TryParse(values[0], out var minimum) || // out var minimum is a variable that will hold the parsed value of the first part of the budget string
            !decimal.TryParse(values[1], out var maximum))
        {
            errors.Add("Budget must be in the format 'minimum-maximum'.");
            return;
        }

        if (minimum < 0 || maximum < 0)
        {
            errors.Add("Budget values cannot be negative.");
        }

        if (minimum > maximum)
        {
            errors.Add("Minimum budget cannot be greater than maximum budget.");
        }
    }
}
