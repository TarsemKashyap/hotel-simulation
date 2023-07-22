using Common;
using Common.ReportDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = RoleType.Student)]
    public class ReportController : AbstractBaseController
    {
        private readonly IGoalReportService _goalReportService;
        private readonly IPerformanceReportService _performanceReportService;
        private readonly IIncomeReportService _incomeReportService;

        public ReportController(IGoalReportService goalReportService, IPerformanceReportService performanceReportService, IIncomeReportService incomeReportService)
        {
            _goalReportService = goalReportService;
            _performanceReportService = performanceReportService;
            _incomeReportService = incomeReportService;
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

        [HttpPost("incomereport")]
        public async Task<IncomeReportDto> IncomeReport(ReportParams goalReport)
        {
            return await _incomeReportService.Report(goalReport);
        }
    }
}
