using System.Text.Json.Serialization;

namespace StockTracker.Models.FinnHub;

public class YearlyFinancials
{
    public FinancialMetrics metric { get; set; }
}

public class FinancialMetrics
{
    [JsonPropertyName("10DayAverageTradingVolume")]
    public double _10DayAverageTradingVolume { get; set; }
    [JsonPropertyName("52WeekHigh")]
    public double _52WeekHigh { get; set; }
    [JsonPropertyName("52WeekLow")]
    public double _52WeekLow { get; set; }
    [JsonPropertyName("52WeekLowDate")]
    public string _52WeekLowDate { get; set; }
    [JsonPropertyName("52WeekPriceReturnDaily")]
    public double _52WeekPriceReturnDaily { get; set; }
    public double beta { get; set; }
}