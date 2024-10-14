using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;

namespace Api.Controllers
{
    [ApiController]
    [Route("studentClassMapping")]
    [Authorize]
    public class StudentClassMappingController : AbstractBaseController
    {
        private readonly IStudentClassMappingService _studentClassMappingService;
        public StudentClassMappingController(IStudentClassMappingService studentClassMappingService)
        {
            _studentClassMappingService = studentClassMappingService;
        }


        [HttpGet("studentlist")]
        public async Task<ActionResult> StudentListByClassId()
        {
            var studentListByClassId = await _studentClassMappingService.ClassesByStudent(LoggedUserId);
            return Ok(studentListByClassId);
        }

        [HttpPost("studentClassUpdate")]
        public async Task<IActionResult> UpdateIsDefault(ClassSessionDto studentClassMappingDto)
        {
            StudentClassMappingDto dto = new StudentClassMappingDto()
            {
                StudentId = LoggedUserId,
                ClassId = studentClassMappingDto.ClassId
            };
            await _studentClassMappingService.IsDefaultUpdate(LoggedUserId, dto);
            var defaultClass = await _studentClassMappingService.GetDefaultByStudentID(LoggedUserId);
            return Ok(defaultClass);
        }

        [HttpGet("studentClasslist")]
        public ActionResult StudentClassList()
        {
            var userId = LoggedUserId;
            var studentListByClassId = _studentClassMappingService.GetMissingClassList();
            return Ok(studentListByClassId);
        }

        [HttpPost("studentClassAssign")]
        public async Task<IActionResult> StudentClassAssign(StudentClassMappingDto studentClassMappingDto)
        {
            var userId = LoggedUserId;
            // var userId = "47dee4d6-f687-4373-b66c-47de7489589c";
            studentClassMappingDto.StudentId = userId;
            var studentData = await _studentClassMappingService.StudentAssignClass(studentClassMappingDto);
            return Ok(studentData);
        }

        [HttpGet("defaultclass")]
        public async Task<IActionResult> GetDefaultClass()
        {
            var defaultClass = await _studentClassMappingService.GetDefaultByStudentID(LoggedUserId);
            if (defaultClass == null)
            {
                return NotFound();
            }
            return Ok(defaultClass);

        }
    }
}
