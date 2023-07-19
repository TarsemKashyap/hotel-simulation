using Common.ReportDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public interface IGoalReportService
    {
        Task<List<GoalReportResponse>> GenerateReport(GoalReportParams goalArgs);
    }
}