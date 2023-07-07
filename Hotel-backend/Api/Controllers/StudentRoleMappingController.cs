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
        private readonly IRoomAllocationService _roomAllocationService;
        private readonly IMonthService _monthService;

        public StudentRoleMappingController(IStudentRolesMappingService studentRolesMappingService,
            IStudentClassMappingService studentClassMappingService, IClassSessionService classSessionService, IMonthService monthService,
            IStudentGroupMappingService studentGroupMappingService, IRoomAllocationService roomAllocationService)
        {
            _studentRolesMappingService = studentRolesMappingService;
            _studentClassMappingService = studentClassMappingService;
            _classSessionService = classSessionService;
            _studentGroupMappingService = studentGroupMappingService;
            _monthService= monthService;
            _roomAllocationService= roomAllocationService;

        }
        [HttpPost("list")]
        public async Task<ActionResult> StudentRoleList(StudentAssignmentRequestParam parms)
        {
            var studentRoleResult = await _studentRolesMappingService.StudentRolesList(parms.StudentId, parms.ClassId);
            return Ok(studentRoleResult);
        }

        [HttpPost("studentRolelist"), AllowAnonymous]
        public async Task<ActionResult> GetStudentRoles()
        {
            var studentRoleResult = await _studentRolesMappingService.GetStudentRolesById(LoggedUserId);
            return Ok(studentRoleResult);
        }

        //[HttpPost("")]

        

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

        [HttpGet("RoomAllocationDetails"), AllowAnonymous]
        public async Task<ActionResult> RoomAllocationDetails()
        {
            var studenClassMappingDtls = await _studentClassMappingService.GetDefaultByStudentID(LoggedUserId);
            var groupId = studenClassMappingDtls.GroupId;
            var classId = studenClassMappingDtls.ClassId;
            var monthsDtls = await _monthService.GetMonthDtlsByClassId(classId);
            var monthId = monthsDtls.MonthId;
            var classDtls = await _classSessionService.GetById(classId);
            var currentQuarter = classDtls.CurrentQuater;
            var roomAllocationDetails = await _roomAllocationService.RoomAllocationDetails(monthId, groupId, currentQuarter);
            return Ok(roomAllocationDetails);
        }


        [HttpPost("UpdateRoomAllocationDtls")]
        public async Task<ActionResult> UpdateRoomAllocationDtls(List<RoomAllocationDto> roomAllocationDto)
        {
            await _roomAllocationService.UpdateRoomAlocations(roomAllocationDto);
            return Ok();
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

