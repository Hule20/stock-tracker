using StockTracker.Models.Base;

namespace StockTracker.Models.Database;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; }
    public string  PasswordHash { get; set; }

    public List<Stock> Stocks { get; set; }
}