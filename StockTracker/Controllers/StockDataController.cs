using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StockTracker.Services;

namespace StockTracker.Controllers;

[ApiController]
[Route("stock")]
public class StockDataController : ControllerBase
{
    private readonly FinnHubApi _finnHubApi;

    public StockDataController(FinnHubApi finnHubApi)
    {
        _finnHubApi = finnHubApi;
    }

    [HttpGet("lookup")]
    public async Task<IActionResult> GetTickerLookup(string ticker)
    {
        var data = await _finnHubApi.GetStockLookup(ticker);

        return Ok(data);
    }

    [HttpGet("latest")]
    public async Task<IActionResult> GetLatestPrice([FromQuery(Name = "ticker")]string ticker)
    {
        var data = await _finnHubApi.GetLatestPrice(ticker);

        return Ok(data);
    }

    [HttpGet("historic")]
    public async Task<IActionResult> GetFromToData([FromQuery(Name = "ticker")] string ticker,
        [FromQuery(Name = "dateFrom")] string from, [FromQuery(Name = "dateTo")] string to)
    {
        var data = await _finnHubApi.GetFromToData(ticker, from, to);

        return Ok(data);
    }
}