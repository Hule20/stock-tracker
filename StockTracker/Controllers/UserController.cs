using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockTracker.Database;
using StockTracker.Models.Authentication;
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

    [HttpPost]
    [Authorize]
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
            return BadRequest("Stock already exists in the watchlist");
        }

        userExists.Stocks.Add(newStock);
        _appDbContext.SaveChanges();

        return Ok($"user found {userExists.Username}, added {newStock.Ticker}");
    }
}