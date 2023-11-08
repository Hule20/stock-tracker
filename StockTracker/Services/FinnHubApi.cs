using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using StockTracker.Utilities;
using StockTracker.Models.FinnHub;
using DateTimeConverter = StockTracker.Utilities.DateTimeConverter;
using Metrics = Microsoft.Identity.Client.Metrics;

namespace StockTracker.Services;

public class FinnHubApi
{
    private readonly IHttpClientFactory _httpClientFactory;

    public FinnHubApi(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<TickerLookup> GetStockLookup(string ticker)
    {
        var client = _httpClientFactory.CreateClient("FinnHubClient");

        var httpRequest = await client.GetAsync(
            $"search?q={ticker}");

        var responseContent = await httpRequest.Content.ReadAsStringAsync();

        TickerLookup? tickerLookupResult = JsonSerializer
            .Deserialize<TickerLookup>(responseContent);

        return tickerLookupResult;
    }

    public async Task<Ohlc> GetLatestPrice(string ticker)
    {
        var client = _httpClientFactory.CreateClient("FinnHubClient");

        var httpRequest = await client.GetAsync(
            $"quote?symbol={ticker}");

        var responseContent = await httpRequest.Content.ReadAsStringAsync();

        var serializerOptions = new JsonSerializerOptions
        {
            Converters = { new OhlcNameConverter() }
        };

        Ohlc? ohlc = JsonSerializer.Deserialize<Ohlc>(responseContent, serializerOptions);

        return ohlc;
    }

    public async Task<List<TimeseriesEntry>> GetFromToData(string ticker, string from, string to)
    {
        var client = _httpClientFactory.CreateClient("FinnHubClient");

        var fromDate = DateTimeConverter.DateTimeToUnixTimeStamp(DateTime.Parse(from));
        var toDate = DateTimeConverter.DateTimeToUnixTimeStamp(DateTime.Parse(to));

        var httpRequest = await client.GetAsync(
            $"stock/candle?symbol={ticker.ToUpper()}&resolution=D&from={fromDate}&to={toDate}");

        var responseContent = await httpRequest.Content.ReadAsStringAsync();

        Timeseries? timeseries = JsonSerializer.Deserialize<Timeseries>(responseContent);

        List<TimeseriesEntry> timeseriesList = new List<TimeseriesEntry>();
        for (int i = 0; i < timeseries.c.Count; i++)
        {
            var timeseriesEntry = new TimeseriesEntry
            {
                Close = timeseries.c[i],
                High = timeseries.h[i],
                Low = timeseries.l[i],
                Open = timeseries.o[i],
                Date = DateOnly.FromDateTime(
                    DateTimeConverter.UnixTimeStampToDateTime(timeseries.t[i])),
                Volume = timeseries.v[i]
            };

            timeseriesList.Add(timeseriesEntry);
        }

        return timeseriesList;
    }

    public async Task<List<NewsArticle>> GetNews()
    {
        var client = _httpClientFactory.CreateClient("FinnHubClient");

        var httpRequest = await client.GetAsync("news?category=general");

        var responseContent = await httpRequest.Content.ReadAsStringAsync();

        var serializerOptions = new JsonSerializerOptions
        {
            Converters = { new NewsArticleConverter() }
        };

        var articles = JsonSerializer.Deserialize<List<NewsArticle>>(responseContent, serializerOptions);

        return articles;
    }

    public async Task<CompanyProfile> GetCompanyProfile(string ticker)
    {
        var client = _httpClientFactory.CreateClient("FinnHubClient");

        var httpRequest = await client.GetAsync($"stock/profile2?symbol={ticker}");

        var responseContent = await httpRequest.Content.ReadAsStringAsync();
        var companyProfile = JsonSerializer.Deserialize<CompanyProfile>(responseContent);

        return companyProfile;
    }

    public async Task<YearlyFinancials> GetYearlyFinancials(string ticker)
    {
        var client = _httpClientFactory.CreateClient("FinnHubClient");

        var httpRequest = await client.GetAsync($"stock/metric?symbol={ticker}&metric=all");

        var responseContent = await httpRequest.Content.ReadAsStringAsync();

        var yearlyHighLow = JsonSerializer.Deserialize<YearlyFinancials>(responseContent);

        return yearlyHighLow;
    }
}