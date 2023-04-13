using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using webapi.Authentication;
using webapi.Models;

namespace webapi.Controllers;

[Route("[controller]")]
public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly UsersKitchHubDbContext _dbContext;
    private readonly IJWTAuthenticationManager _jWTAuthenticationManager;

    public AccountController(ILogger<AccountController> logger,
        UsersKitchHubDbContext dbContext,
        IJWTAuthenticationManager jWTAuthenticationManager)
    {
        _logger = logger;
        _dbContext = dbContext;
        _jWTAuthenticationManager = jWTAuthenticationManager;
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult Authenticate([FromBody] Login request)
    {
        _jWTAuthenticationManager.Users = _dbContext.Users.ToList();
        var token = _jWTAuthenticationManager.Authenticate(request.Email, PasswordHashing.PasswordSHA512(request.Password));

        // Проверка введенных данных (если пользователь есть в системе и введенные данные корректны)
        if (token == null)
        {
            return Unauthorized();
        }

        var user = _dbContext.Users.FirstOrDefault(u => u.Email == request.Email);

        dynamic result = new ExpandoObject();
        result.accessToken = token;
        result.email = user.Email;
        result.nickname = user.NickName;

        Console.WriteLine(result);

        return Ok(result);
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult Registration([FromBody] Reg request)
    {

        Console.WriteLine($"WebAPI Account/Registration input: " +
            $"email: {request.Email} nickname: {request.NickName} password: {request.Password}");

        if(_dbContext.Users.Any(u => u.Email == request.Email || u.NickName == request.NickName))
        {
            return Conflict(new { error = "User already exist" });
        }

        var newUser = new User
        {
            Email = request.Email,
            NickName = request.NickName,
            Password = PasswordHashing.PasswordSHA512(request.Password),
            RegistrationDate = DateTime.UtcNow
        };

        _dbContext.Users.Update(newUser);
        _dbContext.SaveChanges();

        return Ok(newUser);
    }

    /// <summary>
    /// Метод для получения данных из JWT
    /// </summary>
    /// <param name="token">Токен</param>
    /// <returns>Необходимые данные из токена</returns>
    //public IActionResult GetData(string token)
    //{
        
    //}

}
