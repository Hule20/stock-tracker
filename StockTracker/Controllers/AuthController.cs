using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StockTracker.Database;
using StockTracker.Models.Authentication;
using StockTracker.Models.Database;

namespace StockTracker.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public AuthController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpPost("register")]
    public ActionResult<User> Register(RegisterUser registerUserDto)
    {
        var usernameExists = _appDbContext.Users.FirstOrDefault(u => u.Username == registerUserDto.Username);

        if (usernameExists != null)
        {
            return BadRequest("Username already exists!");
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password);

        var newUser = new User()
        {
            Username = registerUserDto.Username,
            PasswordHash = passwordHash
        };

        _appDbContext.Users.Add(newUser);
        _appDbContext.SaveChanges();

        return Ok($"User {newUser.Username} created successfully!");
    }
}