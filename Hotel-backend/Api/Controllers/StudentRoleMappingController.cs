using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service;

namespace Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("roleMapping")]
    public class StudentRoleMappingController : AbstractBaseController
    {
        private readonly IStudentRolesMappingService _studentRolesMappingService;
        private readonly IStudentClassMappingService _studentClassMappingService;
        private readonly IClassSessionService _classSessionService;
        private readonly IStudentGroupMappingService _studentGroupMappingService;

        public StudentRoleMappingController(IStudentRolesMappingService studentRolesMappingService,
            IStudentClassMappingService studentClassMappingService, IClassSessionService classSessionService,
            IStudentGroupMappingService studentGroupMappingService)
        {
            _studentRolesMappingService = studentRolesMappingService;
            _studentClassMappingService = studentClassMappingService;
            _classSessionService = classSessionService;
            _studentGroupMappingService = studentGroupMappingService;

        }
        [HttpGet("list")]
        public async Task<ActionResult> StudentRoleList()
        {
            var studentRoleResult = await _studentRolesMappingService.StudentRolesList();
            return Ok(studentRoleResult);
        }

        [HttpGet("studentlist/{classId}")]
        public async Task<ActionResult> StudentListByClassId(int classId)
        {
            var studentListByClassId = _studentClassMappingService.List(classId);
            return Ok(studentListByClassId);
        }

        [HttpGet("student/{StudentClassMappingId}")]
        public IActionResult StudentClassMappingId(Guid StudentClassMappingId)
        {
            var StudentData = _studentClassMappingService.GetById(StudentClassMappingId);
            return Ok(StudentData);
        }

        [HttpGet("studentGroups")]
        public async Task<ActionResult> StudentGroups()
        {
            var StudentData = await _classSessionService.StudentGroupList();
            return Ok(StudentData);
        }
        [HttpPost()]
        public async Task<ActionResult> UpsertStudentData(StudentRoleGroupAssign studentGroupMappingDto)
        {
            var studentMapping = await _studentGroupMappingService.UpsertStudentData(studentGroupMappingDto);
            return Ok(studentMapping);
        }
    }
}
