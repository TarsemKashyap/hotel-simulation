﻿using Common.ReportDto;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Reports;

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
        private readonly IClassSessionService _classSessionService;
        private readonly ICashFlowReportService _cashFlowReportService;
        private readonly IOccupancyPercentageReport _occupancyPercentageReport;
        private readonly IAverageDailyRateReportService _averageDailyRateReportService;
        private readonly IRevParGoParReportService _revParGoParReportService;

        public ReportController(IGoalReportService goalReportService, IPerformanceReportService performanceReportService, IIncomeReportService incomeReportService, IBalanceReportService balanceReportService, IClassSessionService classSessionService, ICashFlowReportService cashFlowReportService, IOccupancyPercentageReport occupancyPercentageReport, IAverageDailyRateReportService averageDailyRateReportService, IRevParGoParReportService revParGoParReportService)
        {
            _goalReportService = goalReportService;
            _performanceReportService = performanceReportService;
            _incomeReportService = incomeReportService;
            _balanceReportService = balanceReportService;
            _classSessionService = classSessionService;
            _cashFlowReportService = cashFlowReportService;
            _occupancyPercentageReport = occupancyPercentageReport;
            _averageDailyRateReportService = averageDailyRateReportService;
            _revParGoParReportService = revParGoParReportService;
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

        [HttpPost("cashflow")]
        public async Task<CashFlowDto> CashFlowReport(ReportParams goalReport)
        {
            return await _cashFlowReportService.GenerateReport(goalReport);
        }

        [HttpPost("occupancy")]
        public async Task<OccupancyReportDto> OccupancyReport(ReportParams goalReport)
        {
            return await _occupancyPercentageReport.Report(goalReport);
        }

        [HttpPost("avg-daily-rate")]
        public async Task<AverageDailyRateDto> AvgDailyRate(ReportParams dto)
        {
            return await _averageDailyRateReportService.ReportAsync(dto);
        }

        [HttpPost("rev-par-gopar")]
        public async Task<RevparReportDto> RevParGoPar(ReportParams dto)
        {
            return await _revParGoParReportService.ReportAsync(dto);
        }
    }
}
