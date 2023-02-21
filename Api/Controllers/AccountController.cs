using Microsoft.AspNetCore.Mvc;
using Api.Dto;
using Microsoft.AspNetCore.Authorization;
using Service;
namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateAdmin()
    {
        await _accountService.CreateAdminAccount();
        return Ok();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest model)
    {
       await _accountService.ChangePassword(User.Identity.Name,model.CurrentPassword,model.NewPassword);
        return Ok();

    }



}
