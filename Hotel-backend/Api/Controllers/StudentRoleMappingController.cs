using Common.Dto;
using Common.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IAttributeDecisionService _attributeDecisionService;
        private readonly IPriceDecisionService _priceDecisionService;
        private readonly IMarketingService _marketingService;
        private readonly IGoalSettingService _goalSettingService;
        private readonly IBalanceSheetService _balanceSheetService;

        public StudentRoleMappingController(IStudentRolesMappingService studentRolesMappingService,
            IStudentClassMappingService studentClassMappingService, IClassSessionService classSessionService, IMonthService monthService,
            IStudentGroupMappingService studentGroupMappingService, IRoomAllocationService roomAllocationService, IAttributeDecisionService attributeDecisionService, IPriceDecisionService priceDecisionService, IMarketingService marketingService, IGoalSettingService goalSettingService, IBalanceSheetService balanceSheetService)
        {
            _studentRolesMappingService = studentRolesMappingService;
            _studentClassMappingService = studentClassMappingService;
            _classSessionService = classSessionService;
            _studentGroupMappingService = studentGroupMappingService;
            _monthService = monthService;
            _roomAllocationService = roomAllocationService;
            _attributeDecisionService = attributeDecisionService;
            _priceDecisionService = priceDecisionService;
            _marketingService = marketingService;
            _goalSettingService = goalSettingService;
            _balanceSheetService = balanceSheetService;
        }

        [HttpPost("list")]
        public async Task<ActionResult> StudentRoleList(StudentAssignmentRequestParam parms)
        {
            var studentRoleResult = await _studentRolesMappingService.StudentRolesList(parms.StudentId, parms.ClassId);
            return Ok(studentRoleResult);
        }

        [HttpPost("studentRolelist")]
        public async Task<ActionResult> GetStudentRoles()
        {
            var studentRoleResult = await _studentRolesMappingService.GetStudentRolesById(LoggedUserId);
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

        [HttpGet("RoomAllocationDetails")]
        public async Task<ActionResult> RoomAllocationDetails()
        {
            
            var studenClassMappingDtls = await _studentClassMappingService.GetDefaultByStudentID(LoggedUserId);
            var groupId = studenClassMappingDtls.GroupSerial;
            var classId = studenClassMappingDtls.ClassId;
            var monthsDtls = await _monthService.GetMonthDtlsByClassId(classId);
            if (monthsDtls == null)
            {
                return BadRequest("Month Details Not Found");
            }
            var monthId = monthsDtls.MonthId;
            var classDtls = await _classSessionService.GetById(classId);
            if (classDtls == null)
            {
                return BadRequest("Class details Not Found");
            }
            var currentQuarter = classDtls.CurrentQuater;
            var roomAllocationDetails = await _roomAllocationService.RoomAllocationDetails(monthId, groupId, currentQuarter);
            return Ok(new RoomAllocationDetailsDto
            {
                RoomAllocation = roomAllocationDetails.ToList(),
                WeekdayTotal = classDtls.RoomInEachHotel,
                WeekendTotal = classDtls.RoomInEachHotel
            });
        }

        [HttpGet("AttributeDecisionDetails")]
        public async Task<ActionResult> AttributeDecisionDetails()
        {
            var studenClassMappingDtls = await _studentClassMappingService.GetDefaultByStudentID(LoggedUserId);
            var groupId = studenClassMappingDtls.GroupSerial;
            var classId = studenClassMappingDtls.ClassId;
            var monthsDtls = await _monthService.GetMonthDtlsByClassId(classId);
            var monthId = monthsDtls.MonthId;
            var classDtls = await _classSessionService.GetById(classId);
            var currentQuarter = classDtls.CurrentQuater;
            var attributeDecisionDetails = await _attributeDecisionService.AttributeDecisionDetails(monthId, groupId, currentQuarter);
            return Ok(attributeDecisionDetails);
        }

        [HttpPost("UpdateAttributeDecision")]
        public async Task<ActionResult> UpdateAttributeDecision(List<AttributeDecisionDto> attributeDecisionDtos)
        {
            await _attributeDecisionService.UpdateAttributeDecision(attributeDecisionDtos);
            return Ok();
        }

        [HttpGet("GoalSettingDetails")]
        public async Task<ActionResult> GoalSettingDetails()
        {
            var studenClassMappingDtls = await _studentClassMappingService.GetDefaultByStudentID(LoggedUserId);
            var groupId = studenClassMappingDtls.GroupSerial;
            var classId = studenClassMappingDtls.ClassId;
            var monthsDtls = await _monthService.GetMonthDtlsByClassId(classId);
            var monthId = monthsDtls.MonthId;
            var classDtls = await _classSessionService.GetById(classId);
            var currentQuarter = classDtls.CurrentQuater;
            var goalSettingDetails = await _goalSettingService.GoalSettingDetails(monthId, groupId, currentQuarter);
            return Ok(goalSettingDetails);
        }

        [HttpPost("UpdateGoalSetting")]
        public async Task<ActionResult> UpdateGoalSetting(GoalDto goalDtos)
        {
            await _goalSettingService.UpdateGoalSettings(goalDtos);
            return Ok();
        }

        [HttpGet("PriceDecisionDetails"), AllowAnonymous]
        public async Task<ActionResult> PriceDecisionDetails()
        {
            var studenClassMappingDtls = await _studentClassMappingService.GetDefaultByStudentID(LoggedUserId);
            var groupId = studenClassMappingDtls.GroupSerial;
            var classId = studenClassMappingDtls.ClassId;
            var monthsDtls = await _monthService.GetMonthDtlsByClassId(classId);
            var monthId = monthsDtls.MonthId;
            var classDtls = await _classSessionService.GetById(classId);
            var currentQuarter = classDtls.CurrentQuater;
            var priceDecisionDetails = await _priceDecisionService.PriceDecisionDetails(monthId, groupId, currentQuarter);
            return Ok(priceDecisionDetails);
        }

        [HttpPost("UpdatePriceDecision")]
        public async Task<ActionResult> UpdatePriceDecision(List<PriceDecisionDto> priceDecisionDtos)
        {
            await _priceDecisionService.UpdatePriceDecision(priceDecisionDtos);
            return Ok();
        }

        [HttpGet("MarketingDetails")]
        public async Task<ActionResult> MarketingDetails()
        {
            var studenClassMappingDtls = await _studentClassMappingService.GetDefaultByStudentID(LoggedUserId);
            var groupId = studenClassMappingDtls.GroupSerial;
            var classId = studenClassMappingDtls.ClassId;
            var monthsDtls = await _monthService.GetMonthDtlsByClassId(classId);
            var monthId = monthsDtls.MonthId;
            var classDtls = await _classSessionService.GetById(classId);
            var currentQuarter = classDtls.CurrentQuater;
            var marketingDetails = await _marketingService.MarketingDetails(monthId, groupId, currentQuarter);
            return Ok(marketingDetails);
        }

        [HttpPost("UpdateMarketingDetails")]
        public async Task<ActionResult> UpdateMarketingDetails(List<MarketingDecisionDto> marketingDecisionDtos)
        {
            await _marketingService.UpdateMarketingDetails(marketingDecisionDtos);
            return Ok();
        }

        [HttpGet("GetBalanceSheet")]
        public async Task<ActionResult> GetBalanceSheet()
        {
            var studenClassMappingDtls = await _studentClassMappingService.GetDefaultByStudentID(LoggedUserId);
            var groupId = studenClassMappingDtls.GroupSerial;
            var classId = studenClassMappingDtls.ClassId;
            var monthsDtls = await _monthService.GetMonthDtlsByClassId(classId);
            var monthId = monthsDtls.MonthId;
            var classDtls = await _classSessionService.GetById(classId);
            var currentQuarter = classDtls.CurrentQuater;
            var balanceSheetDetails = await _balanceSheetService.BalanceSheetDetails(monthId, groupId, currentQuarter);
            return Ok(balanceSheetDetails);
        }

        [HttpPost("UpdateBalanceSheet")]
        public async Task<ActionResult> UpdateBalanceSheet(BalanceSheetDto balanceSheetDto)
        {
            await _balanceSheetService.UpdateBalanceSheetDetails(balanceSheetDto);
            return Ok();
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

