using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Api.Dto;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppUserRole> _roleManager;

    public AccountController(UserManager<AppUser> userManager, RoleManager<AppUserRole> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<ActionResult> CreateAdmin()
    {
        var appuser = new AppUser
        {
            UserName = "greatGursoy",
            PasswordHash = "12345"
        };
        var result = await _userManager.CreateAsync(appuser);
        if (!await _roleManager.RoleExistsAsync(AppRoleType.Admin.ToString()))
        {
            await _userManager.AddToRoleAsync(appuser, AppRoleType.Admin.ToString());
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