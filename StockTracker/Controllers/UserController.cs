using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockTracker.Database;
using StockTracker.Models.Base;
using StockTracker.Models.Database;

namespace StockTracker.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public UserController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }


    [HttpGet("watchlist")]
    public ActionResult<List<UserStock>> GetUserWatchlist()
    {
        var usernameFromToken = User.Identity?.Name;
        if (usernameFromToken == null)
        {
            return Unauthorized("No user info found in token");
        }

        var user = _appDbContext.Users
            .Include(u => u.Stocks)
            .FirstOrDefault(u => u.Username == usernameFromToken);

        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }

        var userWatchlist = user.Stocks
            .Select(s => new UserStock { Id = s.Id, Ticker = s.Ticker })
            .ToList();

        return Ok(userWatchlist);
    }

    // adds stock to user, also checks if stock exists in db, otherwise adds it to Stocks table
    [HttpPost("to-watchlist")]
    public ActionResult AddStockToWatchlist(string ticker)
    {
        var usernameFromToken = User.Identity?.Name;
        if (usernameFromToken == null)
        {
            return Unauthorized(new { message = "Token not found!" });
        }

        var userExists = _appDbContext.Users
            .Include(u => u.Stocks)
            .FirstOrDefault(u => u.Username == usernameFromToken);
        if (userExists == null)
        {
            return Unauthorized(new { message = "User not found!" });
        }

        var stockExists = _appDbContext.Stocks
            .FirstOrDefault(s => s.Ticker == ticker);
        var newStock = new Stock();
        if (stockExists == null)
        {
            newStock = new Stock
            {
                Ticker = ticker
            };
            _appDbContext.Stocks.Add(newStock);
            _appDbContext.SaveChanges();
        }

        if (userExists.Stocks.Any(s => s.Ticker == ticker))
        {
            return BadRequest(new { message = $"Stock already exists in the {userExists}'s watchlist" });
        }

        userExists.Stocks.Add(newStock);
        _appDbContext.SaveChanges();

        return Ok(new { message = $"{newStock} added successfully to watchlist" });
    }

    [HttpDelete("remove-from-watchlist")]
    public ActionResult DeleteStockFromWatchlist(string ticker)
    {
        var usernameFromToken = User.Identity?.Name;
        if (usernameFromToken == null)
        {
            return Unauthorized(new { message = "Token not found!" });
        }

        var userExists = _appDbContext.Users
            .Include(u => u.Stocks)
            .FirstOrDefault(u => u.Username == usernameFromToken);
        if (userExists == null)
        {
            return Unauthorized(new { message = "User not found!" });
        }

        var stockToDelete = userExists.Stocks.FirstOrDefault(s => s.Ticker == ticker);
        if (stockToDelete == null)
        {
            return NotFound(new { message = $"Stock with ticker {ticker} not found in the watchlist" });
        }

        userExists.Stocks.Remove(stockToDelete);
        _appDbContext.SaveChanges();

        return Ok(new { message = $"{stockToDelete.Ticker} deleted successfully from watchlist" });
    }
}