using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StockTracker.Database;
using StockTracker.Models.Authentication;
using StockTracker.Models.Database;

namespace StockTracker.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _appDbContext;
    private readonly IConfiguration _configuration;

    public AuthController(AppDbContext appDbContext, IConfiguration configuration)
    {
        _appDbContext = appDbContext;
        _configuration = configuration;
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

        var newUser = new User
        {
            Username = registerUserDto.Username,
            PasswordHash = passwordHash
        };

        _appDbContext.Users.Add(newUser);
        _appDbContext.SaveChanges();

        return Ok(newUser);
    }

    [HttpPost("login")]
    public ActionResult<User> Login(LoginUser loginUserDto)
    {
        var findUserByUsername = _appDbContext.Users.FirstOrDefault(u => u.Username == loginUserDto.Username);

        if (findUserByUsername == null)
        {
            return BadRequest("This username not found!");
        }

        if (!BCrypt.Net.BCrypt.Verify(loginUserDto.Password, findUserByUsername.PasswordHash))
        {
            return BadRequest("Wrong password!");
        }

        var token = GenerateToken(loginUserDto);
        HttpContext.Response.Headers.Add("Authorization", $"Bearer {token}");

        return Ok(new { message = "User logged in successfully!" });
    }

    private string GenerateToken(LoginUser userDto)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userDto.Username),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("Jwt:Key").Value!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }
}