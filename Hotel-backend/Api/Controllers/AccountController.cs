using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Service;
using FluentValidation;

namespace Api.Controllers;

[ApiController]
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
            Institue = account.Institue
        };
        await _accountService.InstructorAccount(instructor);
        return Ok(instructor);

    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest model)
    {
        await _accountService.ChangePassword(User.Identity.Name, model.CurrentPassword, model.NewPassword);
        return Ok();

    }



}
