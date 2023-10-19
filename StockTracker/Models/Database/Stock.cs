using StockTracker.Models.Database;

namespace StockTracker.Models.Base;

public class Stock
{
    public int Id { get; set; }
    public string Ticker { get; set; }
    public List<User> Users { get; set; }
}