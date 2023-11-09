using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StockTracker.Models.Database;
using StockTracker.Models.FinnHub;
using StockTracker.Services;
using Metrics = Microsoft.Identity.Client.Metrics;

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
    public async Task<ActionResult<TickerLookup>> GetTickerLookup(string ticker)
    {
        var data = await _finnHubApi.GetStockLookup(ticker);

        return Ok(data);
    }

    [HttpGet("latest")]
    public async Task<IActionResult> GetLatestPrice([FromQuery(Name = "ticker")] string ticker)
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

    [HttpGet("news")]
    public async Task<ActionResult<List<NewsArticle>>> GetMarketNews()
    {
        var data = await _finnHubApi.GetNews();

        return Ok(data);
    }

    [HttpGet("company-profile")]
    public async Task<ActionResult<CompanyProfile>> GetCompanyProfile(string ticker)
    {
        var data = await _finnHubApi.GetCompanyProfile(ticker);

        return Ok(data);
    }
}