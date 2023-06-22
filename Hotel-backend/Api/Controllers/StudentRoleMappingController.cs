using Common.Dto;
using Common.Model;
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
        [HttpPost("list")]
        public async Task<ActionResult> StudentRoleList(StudentAssignmentRequestParam parms)
        {
            var studentRoleResult = await _studentRolesMappingService.StudentRolesList(parms.StudentId, parms.ClassId);
            return Ok(studentRoleResult);
        }

        [HttpPost("studentRolelist")]
        public async Task<ActionResult> GetStudentRoles(StudentAssignmentRequestParam parms)
        {
            var studentRoleResult = await _studentRolesMappingService.GetStudentRolesById(parms.StudentId);
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

        [HttpGet("RoomList")]
        public async Task<ActionResult> RoomList()
        {
            List<RoomDto> RoomList = new List<RoomDto>();
            RoomList.Add(new RoomDto { Label = "Business", Weekday = "0", Weekend = "" });
            RoomList.Add(new RoomDto { Label = "Small Business", Weekday = "", Weekend = "" });
            RoomList.Add(new RoomDto { Label = "Corporate contract", Weekday = "", Weekend = "" });
            RoomList.Add(new RoomDto { Label = "Families", Weekday = "", Weekend = "" });
            RoomList.Add(new RoomDto { Label = "Afluent Mature Travelers", Weekday = "", Weekend = "" });
            RoomList.Add(new RoomDto { Label = "International leisure travelers", Weekday = "", Weekend = "" });
            RoomList.Add(new RoomDto { Label = "Corporate/Business Meetings", Weekday = "", Weekend = "" });
            RoomList.Add(new RoomDto { Label = "Association Meetings", Weekday = "", Weekend = "" });
           
            return Ok(RoomList);
        }
        
        [HttpPost()]
        public async Task<ActionResult> UpsertStudentData(StudentRoleGroupAssign studentGroupMappingDto)
        {
            var studentMapping = await _studentGroupMappingService.UpsertStudentData(studentGroupMappingDto);
            return Ok(studentMapping);
        }

        [HttpGet("{studentId}")]
        public async Task<ActionResult> GetById(string studentId)
        {
            var studentAssignRole = await _studentGroupMappingService.GetById(studentId);
            return Ok(studentAssignRole);

        }
    }
}
