using Microsoft.AspNetCore.Mvc;
using StocksApi.BAL;

namespace StocksApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly ICitiesBAL _citiesBAL;

    public CitiesController(ICitiesBAL citiesBAL)
    {
        _citiesBAL = citiesBAL;
    }

    [HttpGet]
    public async Task<IActionResult> GetCities()
    {
        var cities = await _citiesBAL.GetCitiesAsync();

        return Ok(cities);
    }
}