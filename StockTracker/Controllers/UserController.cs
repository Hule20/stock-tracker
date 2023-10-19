using Microsoft.AspNetCore.Mvc;
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
    public ActionResult<User> Create(RegisterUser registerUserDto)
    {
        var userExists = _appDbContext.Users.FirstOrDefault(u => u.Username == registerUserDto.Username);

        if (userExists != null) return BadRequest("Username already exists!");
        
        var newUser = new User()
        {
            Username = registerUserDto.Username
        };

        _appDbContext.Add(newUser);
        _appDbContext.SaveChanges();

        return Ok($"New user {newUser.Username} added to database");
    }
}