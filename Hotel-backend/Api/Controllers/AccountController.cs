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
    private readonly IValidator<InstructorSignupRequest> _accountValidator;
    private readonly IValidator<InstructorDto> _instructorDtoValidator;

    public AccountController(IAccountService accountService, IValidator<InstructorSignupRequest> accountValidator, IValidator<InstructorDto> instructorDtoValidator)
    {
        _accountService = accountService;
        _accountValidator = accountValidator;
        _instructorDtoValidator = instructorDtoValidator;
    }

    [HttpPost("admin"), AllowAnonymous]
    public async Task<ActionResult> CreateAdmin()
    {
        await _accountService.CreateAdminAccount();
        return Ok();
    }

    [HttpPost("instructor"), AllowAnonymous]
    public async Task<ActionResult> InstructorAccount(InstructorSignupRequest account)
    {
        _accountValidator.ValidateAndThrow(account);
        var instructor = new InstructorAccountDto()
        {
            FirstName = account.FirstName,
            LastName = account.LastName,
            Email = account.Email,
            Institute = account.Institute,
            Password = account.Password

        };
        await _accountService.InstructorAccount(instructor);
        return Ok(instructor);

    }

    [HttpPost("instructor/update/{id}")]
    public async Task<ActionResult> InstructorUpdate(string id, InstructorDto account)
    {
        _instructorDtoValidator.ValidateAndThrow(account);
        var instructor = new InstructorDto()
        {
            FirstName = account.FirstName,
            LastName = account.LastName,
            Email = account.Email,
            Institute = account.Institute,

        };
        await _accountService.InstructorUpdate(id, instructor);
        return Ok();

    }
    [HttpGet("instructor/{id}")]
    public async Task<ActionResult> InstructorDelete(string id)
    {
        await _accountService.InstructorDelete(id);
        return Ok();

    }

    [HttpGet("instructor/{id}")]
    public ActionResult InstructorUpdate(string id)
    {
        var instructor = _accountService.InstructorById(id);
        return Ok(instructor);

    }

    [HttpGet("instructor/list")]
    public async Task<ActionResult> InstructorList()
    {
        var data = await _accountService.InstructorList();
        return Ok(data);
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
