using Microsoft.AspNetCore.Mvc;
using GerenciadorDeTarefas.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace GerenciadorDeTarefas.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly TokenService _tokenService;

    public LoginController(IConfiguration config)
    {
        _config = config;
        _tokenService = new TokenService();
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Login([FromBody] UserLogin userLogin)
    {
        if (userLogin.Username == "admin" && userLogin.Password == "password")
        {
            var tokenString = _tokenService.GerarToken(userLogin.Username, _config);
            return Ok(new { token = tokenString });
        }
        else
        {
            return Unauthorized();
        }
    }
}

public class UserLogin
{
    public string Username { get; set; }
    public string Password { get; set; }
}