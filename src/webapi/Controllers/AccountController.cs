using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using webapi.Authentication;
using webapi.Models;

namespace webapi.Controllers;

//[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly KitchHubDbContext _dbContext;
    private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

    public AccountController(ILogger<AccountController> logger,
        KitchHubDbContext dbContext,
        IJwtAuthenticationManager jWTAuthenticationManager)
    {
        _logger = logger;
        _dbContext = dbContext;
        _jwtAuthenticationManager = jWTAuthenticationManager;
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult Authenticate([FromBody] Login request)
    {
        _jwtAuthenticationManager.Users = _dbContext.Users.ToList();
        var token = _jwtAuthenticationManager.Authenticate(request.Email,PasswordHashing.PasswordSHA512(request.Password));

        // Проверка введенных данных (если пользователь есть в системе и введенные данные корректны)
        if (token == null)
        {
            return Unauthorized();
        }

        var user = _dbContext.Users.Include(r => r.Role)
                                   .FirstOrDefault(u => u.Email == request.Email);

        //if (user.UserBan != null)
        //{
        //    return Conflict(new { error = "User has banned!" });
        //}

        dynamic result = new ExpandoObject();
        result.accessToken = token;
        result.email = user.Email;
        result.nickname = user.NickName;
        result.role = user.Role.RoleId;                // Работает не корректно!
        result.avatar = user.Avatar;
        result.birthday = user.Birthday;

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
            RegistrationDate = DateTime.UtcNow,
            Role = _dbContext.Roles.FirstOrDefault(r => r.RoleId == 1)!
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
