using Common.ReportDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportInstructorController : AbstractBaseController
    {
        private readonly IPerformanceReportService _performanceReportService;

        public ReportInstructorController(IPerformanceReportService performanceReportService)
        {
            _performanceReportService = performanceReportService;
        }

        [HttpPost("performance")]
        public async Task<PerformanceInstructorReportDto> PerformanceReport(ReportParams goalReport)
        {
            return await _performanceReportService.InstructorPerformanceReport(goalReport);
        }

        [HttpPost("summary-all-hotels")]
        public async Task<List<StatisticsHotelDto>> AllHotelSummery(ReportParams goalReport)
        {
            return await _performanceReportService.AllHotelPerformanceReport(goalReport);
        }
    }
}
