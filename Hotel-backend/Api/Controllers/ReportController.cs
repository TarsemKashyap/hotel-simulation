using Common;
using Common.Dto;
using Common.ReportDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Route("Reports")]
    // [Authorize(Roles = RoleType.Student)]
    public class ReportController : AbstractBaseController
    {
        private readonly IGoalReportService _goalReportService;
        private readonly IPerformanceReportService _performanceReportService;
        private readonly IIncomeReportService _incomeReportService;
        private readonly IBalanceReportService _balanceReportService;
        private readonly IClassSessionService _classSessionService;

        public ReportController(IGoalReportService goalReportService, IPerformanceReportService performanceReportService, IIncomeReportService incomeReportService, IBalanceReportService balanceReportService, IClassSessionService classSessionService)
        {
            _goalReportService = goalReportService;
            _performanceReportService = performanceReportService;
            _incomeReportService = incomeReportService;
            _balanceReportService = balanceReportService;
            _classSessionService = classSessionService;
        }


        [HttpGet("monthFilterDetails/{classId}")]
        public async Task<ActionResult> MonthFilterDetails(int classId)
        {
            var monthData = await _classSessionService.MonthFilterList(classId);
            return Ok(monthData);
        }

        [HttpGet("groupFilterDetails/{classId}")]
        public async Task<ActionResult> GroupFilterDetails(int classId)
        {
            var groupData = await _classSessionService.GetGroupList(classId);
            return Ok(groupData);
        }

        [HttpPost("goal")]
        public async Task<IEnumerable<GoalReportResponse>> GoalReport(ReportParams goalReport)
        {
            // goalReport.UserId = LoggedUserId;
            return await _goalReportService.GenerateReport(goalReport);
        }

        [HttpPost("performance")]
        public async Task<PerformanceReportDto> PerformanceReport(ReportParams goalReport)
        {
            return await _performanceReportService.PerformanceReport(goalReport);
        }

        [HttpPost("income")]
        public async Task<IncomeReportDto> IncomeReport(ReportParams goalReport)
        {
            return await _incomeReportService.Report(goalReport);
        }

        [HttpPost("balance")]
        public async Task<BalanceReportDto> BalanceReport(ReportParams goalReport)
        {
            return await _balanceReportService.Report(goalReport);
        }
    }
}
