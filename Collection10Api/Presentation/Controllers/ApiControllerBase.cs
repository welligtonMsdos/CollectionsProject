using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Collection10Api.Presentation.Controllers;

[ApiController]
public class ApiControllerBase : ControllerBase
{
    protected string UserId => User.FindFirstValue("id") ?? string.Empty;
}
}
