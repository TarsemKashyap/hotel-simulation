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
    //[Authorize]
    public class StudentClassMappingController : AbstractBaseController
    {
        private readonly IStudentClassMappingService _studentClassMappingService;
        public StudentClassMappingController(IStudentClassMappingService studentClassMappingService)
        {
            _studentClassMappingService = studentClassMappingService;
        }


        [HttpGet("studentlist"), AllowAnonymous]
        public async Task<ActionResult> StudentListByClassId()
        {
            //var userId = LoggedUserId;
            var userId = "47dee4d6-f687-4373-b66c-47de7489589c";
            var studentListByClassId = _studentClassMappingService.StudentList(userId);
            return Ok(studentListByClassId);
        }

        [HttpPost("studentClassUpdate")]
        public async Task<IActionResult> UpdateIsDefault(StudentClassMappingDto studentClassMappingDto)
        {
           // var userId = LoggedUserId;
            var userId = "47dee4d6-f687-4373-b66c-47de7489589c";
            var studentData =await _studentClassMappingService.IsDefaultUpdate(userId, studentClassMappingDto.IsDefault);
            return Ok(studentData);
        }

        [HttpGet("studentClasslist"), AllowAnonymous]
        public async Task<ActionResult> StudentClassList()
        {
            // var userId = LoggedUserId;
            var userId = "47dee4d6-f687-4373-b66c-47de7489589c";
            var studentListByClassId = _studentClassMappingService.GetMissingClassList();
            return Ok(studentListByClassId);
        }

        [HttpPost("studentClassAssign")]
        public async Task<IActionResult> studentClassAssign(StudentClassMappingDto studentClassMappingDto)
        {
           // var userId = LoggedUserId;
            var userId = "47dee4d6-f687-4373-b66c-47de7489589c";
            studentClassMappingDto.StudentId = userId;
            var studentData = await _studentClassMappingService.StudentAssignClass(studentClassMappingDto);
            return Ok(studentData);
        }
    }
}
