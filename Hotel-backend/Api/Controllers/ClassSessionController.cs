using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Service;
using FluentValidation;
using Service;

namespace Api.Controllers;

[ApiController]
[Authorize]
[Route("class")]
public class ClassSessionController : AbstractBaseController
{
    private readonly IValidator<ClassSessionDto> _validator;
    private readonly IValidator<ClassGroupDto> _classGroupValidator;
    private readonly IClassSessionService _classSessionService;

    public ClassSessionController(IValidator<ClassSessionDto> validator, IValidator<ClassGroupDto> classGroupValidator, IClassSessionService classSessionService)
    {
        _validator = validator;
        _classGroupValidator = classGroupValidator;
        _classSessionService = classSessionService;
    }
    [HttpPost()]
    public async Task<IActionResult> Create(ClassSessionDto dto)
    {
        _validator.ValidateAndThrow(dto);
        dto.CreatedBy = LoggedUserId;
        var response = await _classSessionService.Create(dto);
        return Ok(response);
    }

    [HttpPost("edit/{classId}")]
    public async Task<IActionResult> CreateGroup(int classId, ClassGroupDto[] classGroup)
    {
        foreach (var item in classGroup)
        {
            _classGroupValidator.ValidateAndThrow(item);
        }
        var clasGroupResult = await _classSessionService.AddGroupAsync(classId, classGroup);
        return Ok(clasGroupResult);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var appUser = await _classSessionService.GetById(id);
        return Ok(appUser);

    }

    [HttpPost("editClass/{id}")]
    public async Task<IActionResult> ClassUpdate(int id, ClassSessionUpdateDto account)
    {
        // _validator.ValidateAndThrow(account);
        var response = await _classSessionService.Update(id, account);
        return Ok(response);

    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> ClassDelete(int id)
    {
        await _classSessionService.DeleteId(id);
        return Ok();

    }

    [HttpGet("list")]
    public IActionResult ClassList()
    {
        string instructorId = IsAdmin ? null : LoggedUserId;
        var clasGroupResult = _classSessionService.List(instructorId);
        return Ok(clasGroupResult);

    }

}