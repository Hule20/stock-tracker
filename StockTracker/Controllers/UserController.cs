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
    [HttpPost]
    //[Authorize]
    public ActionResult<User> AddStockToUser(string ticker)
    {
        var usernameFromToken = User.Identity?.Name;
        if (usernameFromToken == null)
        {
            return Unauthorized("No user info found in token");
        }

        var userExists = _appDbContext.Users
            .Include(u => u.Stocks)
            .FirstOrDefault(u => u.Username == usernameFromToken);
        if (userExists == null)
        {
            return Unauthorized("User not found in database");
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
            return BadRequest($"Stock already exists in the {userExists}'s watchlist");
        }

        userExists.Stocks.Add(newStock);
        _appDbContext.SaveChanges();

        return Ok($"user found {userExists.Username}, added {newStock.Ticker}");
    }
}