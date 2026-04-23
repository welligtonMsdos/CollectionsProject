using CollectionApplication.Dtos;
using CollectionApplication.Interfaces;
using CollectionShared.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Post([FromBody] UserCreateDto userCreateDto)
    {
        var userExists = await _userService.ExistsByEmailAsync(userCreateDto.Email);

        if (userExists)
        {
            return Conflict(Result<object>.Failure("Email already in use"));
        }

        var newUser = await _userService.PostAsync(userCreateDto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = newUser._id.ToString() },
            Result<UserDto>.Ok(newUser, "User successfully created!")
        );
    }

    [AllowAnonymous]
    [HttpPost("[Action]")]
    public async Task<IActionResult> SignUp([FromBody] UserCreateDto userCreateDto)
    {
        return await Post(userCreateDto);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? email)
    {
        if (!string.IsNullOrWhiteSpace(email))
        {
            var user = await _userService.GetByEmailAsync(email);

            if (user is null) return NotFound(Result<object>.Failure("User not found."));

            return Ok(Result<UserDto>.Ok(user));
        }

        var users = await _userService.GetAsync();

        return Ok(Result<IEnumerable<UserDto>>.Ok(users));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user is null) return NotFound(Result<object>.Failure("User not found."));

        return Ok(Result<UserDto>.Ok(user));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] UserUpdateDto userUpdateDto)
    {
        var updateUser = await _userService.PutAsync(id, userUpdateDto);

        if (updateUser is null)
            return NotFound(Result<object>.Failure("User not found for update."));

        return Ok(Result<UserDto>.Ok(updateUser, "User successfully updated!"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deletedUser = await _userService.DeleteAsync(id);

        if (!deletedUser)
            return NotFound(Result<object>.Failure("User not found for deletion."));

        return Ok(Result<bool>.Ok(true, "User removed successfully!"));
    }
}
