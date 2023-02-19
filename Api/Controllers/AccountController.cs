using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Api.Dto;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<ActionResult> CreateAdmin()
    {
        var appuser = new IdentityUser
        {
            UserName = "greatGursoy",
            PasswordHash = "12345"
        };
        var result = await _userManager.CreateAsync(appuser);
        if (!await _roleManager.RoleExistsAsync(UserType.Admin.ToString()))
        {
            await _userManager.AddToRoleAsync(appuser, UserType.Admin.ToString());
        }

        return Ok();
    }

    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
    {
        //  await _userManager.ChangePasswordAsync(User, model.CurrentPassword, model.NewPassword);
        return Ok();

    }



}