using Common.ReportDto;
using Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Service
{
    public interface ICashFlowReportService
    {
        Task<CashFlowDto> GenerateReport(ReportParams parms);
    }
    public class CashFlowReportService : ICashFlowReportService
    {
        private readonly HotelDbContext _context;

        public CashFlowReportService(HotelDbContext context)
        {
            _context = context;
        }
        public async Task<CashFlowDto> GenerateReport(ReportParams parms)
        {
            // var balanceSheet = _context.BalanceSheet.AsNoTracking();
            return new CashFlowDto();

        }

    }
}