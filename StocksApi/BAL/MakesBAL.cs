using StocksApi.DAL;
using StocksApi.Entities;

namespace StocksApi.BAL;

public class MakesBAL : IMakesBAL
{
    private readonly IMakesDAL _makesDAL;

    public MakesBAL(IMakesDAL makesDAL)
    {
        _makesDAL = makesDAL;
    }

    public async Task<List<Make>> GetMakesAsync()
    {
        var makes = await _makesDAL.GetMakesAsync();

        return makes.ToList();
    }
}
