using Microsoft.AspNetCore.Mvc;
using Database;
using System.Security.Claims;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{

}

public abstract class AbstractBaseController : ControllerBase
{

    public string LoggedUserId => User.FindFirstValue(ClaimTypes.NameIdentifier);
    public bool IsAdmin => User.FindFirstValue(ClaimTypes.Role) == RoleType.Admin;

}




public record User(string UserId, string[] roles);