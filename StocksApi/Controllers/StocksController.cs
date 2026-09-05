using Microsoft.AspNetCore.Mvc;
using StocksApi.DTOs;
using StocksApi.Mappers;
using StocksApi.Validators;
using StocksApi.BAL;

namespace StocksApi.Controllers;

[ApiController] // tells asp.net core that this is a controller class and it should handle HTTP requests
[Route("api/[controller]")] // url becomes api/stocks, [controller] is a placeholder for the controller name without the "Controller" suffix
public class StocksController : ControllerBase // ControllerBase is a base class for controllers that don't need to support views, it provides basic functionality for handling HTTP requests and responses
{
    private readonly IStocksBAL _stocksBAL; // to use BAL instance, we will inject it
    private readonly FiltersMapper _filtersMapper; // to use mapper instance, we will inject it

    public StocksController(IStocksBAL stocksBAL, FiltersMapper filtersMapper)
    {
        _stocksBAL = stocksBAL;
        _filtersMapper = filtersMapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetStocks([FromQuery] StockFilterRequestDto request) // IActionResult represents an HTTP response.
    {
        var errors = StockFilterRequestValidator.Validate(request); // validating the request

        if (errors.Count > 0)
        {
            return BadRequest(errors); // returns a 400 Bad Request response with the list of errors
        }

        var filters = _filtersMapper.ToFilters(request); // mapping the request DTO to the filters model

        var stocks = await _stocksBAL.GetStocksAsync(filters); // calling BAL to get stocks based on filters
        return Ok(new
        {
            stocks = stocks,
            totalCount = stocks.Count
        });
    }
}
