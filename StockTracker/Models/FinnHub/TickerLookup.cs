namespace StockTracker.Models.FinnHub;

public class TickerLookup
{
    public int count { get; set; }
    public List<Result> result { get; set; }
}

public class Result
{
    public string description { get; set; }
    public string displaySymbol { get; set; }
    public string symbol { get; set; }
    public string type { get; set; }
}