using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CollectionApi.Controllers;

[ApiController]
public class ApiControllerBase : ControllerBase
{
    protected string UserId => User.FindFirstValue("id") ?? string.Empty;
}
