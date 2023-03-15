using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Service;
using FluentValidation;

namespace Api.Controllers;

[ApiController]
[Authorize]
[Route("account")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IValidator<InstructorAccountRequest> _accountValidator;

    public AccountController(IAccountService accountService, IValidator<InstructorAccountRequest> accountValidator)
    {
        _accountService = accountService;
        _accountValidator = accountValidator;
    }

    [HttpPost("admin")]
    public async Task<ActionResult> CreateAdmin()
    {
        await _accountService.CreateAdminAccount();
        return Ok();
    }

    [HttpPost("instructor")]
    public async Task<ActionResult> InstructorAccount(InstructorAccountRequest account)
    {
        _accountValidator.ValidateAndThrow(account);
        var instructor = new InstructorAccountDto()
        {
            FirstName = account.FirstName,
            LastName = account.LastName,
            Email = account.Email,
            Institue = account.Institue,
            Password = account.Password

        };
        await _accountService.InstructorAccount(instructor);
        return Ok(instructor);

    }


    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest model)
    {
        await _accountService.ChangePassword(User.Identity.Name, model.CurrentPassword, model.NewPassword);
        return Ok();

    }

    [HttpPost("login"), AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto login)
    {
        var signInResult = await _accountService.SignAsync(login);
        return Ok(signInResult);
    }

    [HttpPost("token/refresh"), AllowAnonymous]
    public async Task<IActionResult> TokenRefresh(TokenApiRequest login)
    {
        try
        {
            var signInResult = await _accountService.RefreshToken(login.AccessToken, login.RefreshToken);
            return Ok(signInResult);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }

    [HttpPost("token/revoke")]
    public async Task<IActionResult> Revoke()
    {
        string userId = User.Identity.Name;
        await _accountService.Revoke(userId);
        return Ok();
    }

}
