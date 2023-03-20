using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Service;
using FluentValidation;
using Service;

namespace Api.Controllers;

[ApiController]
[Authorize]
[Route("class")]
public class ClassSessionController : ControllerBase
{
    private readonly IValidator<ClassSessionDto> _validator;
    private readonly IClassSessionService _classSessionService;

    public ClassSessionController(IValidator<ClassSessionDto> validator, IClassSessionService classSessionService)
    {
        _validator = validator;
        _classSessionService = classSessionService;
    }
    [HttpPost]
    public async Task<IActionResult> Create(ClassSessionDto dto)
    {
        _validator.ValidateAndThrow(dto);
        await _classSessionService.Create(dto);
        return Ok(dto);
    }
}