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
        public ReportController(IGoalReportService goalReportService)
        {
            _goalReportService = goalReportService;
        }

        [HttpPost]
        public async Task<IEnumerable<GoalReportResponse>> GoalReport(GoalReportParams goalReport)
        {
           // goalReport.UserId = LoggedUserId;
            return await _goalReportService.GenerateReport(goalReport);
        }
    }
}
