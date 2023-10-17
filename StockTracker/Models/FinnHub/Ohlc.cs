using System.Text.Json.Serialization;

namespace StockTracker.Models.FinnHub;

public class Ohlc
{
    public double Close { get; set; }
    public double Change { get; set; }
    public double ChangePercentage { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Open { get; set; }
    public double PreviousClose { get; set; }
    public  DateTime Time { get; set; }
}