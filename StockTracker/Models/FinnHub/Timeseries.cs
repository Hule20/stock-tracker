using System.Text.Json.Serialization;

namespace StockTracker.Models.FinnHub;

public class Timeseries
{
    public List<double> c { get; set; }
    public List<double> h { get; set; }
    public List<double> l { get; set; }
    public List<double> o { get; set; }
    public List<long> t { get; set; }
    public List<long> v { get; set; }
    public string s { get; set; }
}

public class TimeseriesEntry
{
    public double Close { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Open { get; set; }
    public DateOnly Date { get; set; }
    public long Volume { get; set; }
}