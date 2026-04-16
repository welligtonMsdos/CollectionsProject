using CollectionApplication.Dtos;
using CollectionApplication.Interfaces;
using CollectionShared.Common;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly string _key;

    public AuthController(IUserService userService, 
                          ITokenService tokenService,
                          string key)
    {
        _userService = userService;
        _tokenService = tokenService;
        _key = key;
    }

    [HttpPost("[Action]")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var user = await _userService.GetDataLoginAsync(userLoginDto);

        if (user is null) return Unauthorized();

        var token = _tokenService.GenerateToken(user, _key);

        return Ok(Result<Task<string>>.Ok(token));
    }
}
