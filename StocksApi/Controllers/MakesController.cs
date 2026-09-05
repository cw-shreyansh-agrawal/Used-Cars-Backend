using Microsoft.AspNetCore.Mvc;
using StocksApi.BAL;

namespace StocksApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MakesController : ControllerBase
{
    private readonly IMakesBAL _makesBAL;

    public MakesController(IMakesBAL makesBAL)
    {
        _makesBAL = makesBAL;
    }

    [HttpGet]
    public async Task<IActionResult> GetMakes()
    {
        var makes = await _makesBAL.GetMakesAsync();

        return Ok(makes);
    }
}