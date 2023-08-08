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
        private readonly IBalanceReportService _balanceReportService;

        public ReportController(IGoalReportService goalReportService, IPerformanceReportService performanceReportService, IIncomeReportService incomeReportService, IBalanceReportService balanceReportService)
        {
            _goalReportService = goalReportService;
            _performanceReportService = performanceReportService;
            _incomeReportService = incomeReportService;
            _balanceReportService = balanceReportService;
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

        [HttpPost("cashflow")]
        public async Task<BalanceReportDto> CashFlowReport(ReportParams goalReport)
        {
           
        }
    }
}
