
using Common.ReportDto;
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
        private readonly IRoomRateReportService _roomRateReportService;
        private readonly IMarketShareRevenueReport _marketShareRevenueReport;
        private readonly IMarketShareRoomSoldReport _marketShareRoomSoldReport;
        private readonly IMarketSharePositionReport _marketSharePositionReport;
        private readonly IAttributeAmentitiesReportService _attributeAmentitiesReportService;
        private readonly IMarketExpendReportService _marketExpendReportService;
        private readonly IQualityPerceptionRatingReportService _qualityPerceptionRatingReportService;
        private readonly IPositionMapReportService _positionMapReportService;

        public ReportController(IGoalReportService goalReportService, IPerformanceReportService performanceReportService, IIncomeReportService incomeReportService, IBalanceReportService balanceReportService, IClassSessionService classSessionService, ICashFlowReportService cashFlowReportService, IOccupancyPercentageReport occupancyPercentageReport, IAverageDailyRateReportService averageDailyRateReportService, IRevParGoParReportService revParGoParReportService, IRoomRateReportService roomRateReportService, IMarketShareRevenueReport marketShareRevenueReport, IMarketShareRoomSoldReport marketShareRoomSoldReport, IMarketSharePositionReport marketSharePositionReport, IAttributeAmentitiesReportService attributeAmentitiesReportService, IMarketExpendReportService marketExpendReportService, IQualityPerceptionRatingReportService qualityPerceptionRatingReportService, IPositionMapReportService positionMapReportService)
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
            _roomRateReportService = roomRateReportService;
            _marketShareRevenueReport = marketShareRevenueReport;
            _marketShareRoomSoldReport = marketShareRoomSoldReport;
            _marketSharePositionReport = marketSharePositionReport;
            _attributeAmentitiesReportService = attributeAmentitiesReportService;
            _marketExpendReportService = marketExpendReportService;
            _qualityPerceptionRatingReportService = qualityPerceptionRatingReportService;
            _positionMapReportService = positionMapReportService;
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
        public async Task<MarketShareReportDto> OccupancyReport(ReportParams goalReport)
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

        [HttpPost("roomRate")]
        public async Task<RoomRateReportDto> RoomRate(ReportParams dto)
        {
            return await _roomRateReportService.ReportAsync(dto);
        }


        [HttpPost("market-share/revenue")]
        public async Task<MarketShareReportDto> MarketShareRevenue(ReportParams dto)
        {
            return await _marketShareRevenueReport.ReportAsync(dto);
        }

        [HttpPost("market-share/roomsold")]
        public async Task<MarketShareReportDto> MarketShareRoomSold(ReportParams dto)
        {
            return await _marketShareRoomSoldReport.ReportAsync(dto);
        }

        [HttpPost("market-share/position-alone")]
        public async Task<MarketSharePositionReportDto> MarketSharePosition(ReportParams dto)
        {
            return await _marketSharePositionReport.ReportAsync(dto);
        }

        [HttpPost("attribute-amentities")]
        public async Task<AttributeAmentitiesReportDto> HoteAttributeAmenties(ReportParams dto)
        {
            return await _attributeAmentitiesReportService.ReportAsync(dto);
        }

        [HttpPost("market-expenditure")]
        public async Task<MarketingExpensReportDto> MarketExpenditure(ReportParams dto)
        {
            return await _marketExpendReportService.ReportAsync(dto);
        }

        [HttpPost("quality-rating")]
        public async Task<QualityPreceptionReportDto> QualityPerceptionRating(ReportParams dto)
        {
            return await _qualityPerceptionRatingReportService.ReportAsync(dto);
        }

        [HttpPost("position-map")]
        public async Task<PositionMapReportDto> PositionMapReport(ReportParams dto)
        {
            return await _positionMapReportService.ReportAsync(dto);
        }
    }
}
