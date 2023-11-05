using Common.ReportDto;
using Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public interface ICashFlowReportService
    {
        Task<CashFlowDto> GenerateReport(ReportParams parms);
    }
    public class CashFlowReportService : AbstractReportService, ICashFlowReportService
    {
        private readonly HotelDbContext _context;

        public CashFlowReportService(HotelDbContext context)
        {
            _context = context;
        }
        private int PreviousMonthFinder(int monthID)
        {
            var prevMonths = _context.Months
                            .Include(x => x.Class)
                            .ThenInclude(x => x.Months)
                            .Where(x => x.MonthId == monthID)
                            .SelectMany(x => x.Class.Months)
                            .OrderBy(x => x.MonthId)
                           .Select(x => x.MonthId)
                            .ToList();

            int prevMonthIndex = prevMonths.FindIndex(p => p == monthID);
            return prevMonths[prevMonthIndex - 1];
        }
        public async Task<CashFlowDto> GenerateReport(ReportParams parms)
        {
            var currentMonth = await _context.BalanceSheet.AsNoTracking().FirstOrDefaultAsync(x => x.GroupID == parms.GroupId && x.MonthID == parms.MonthId);
            var lastMonth = await _context.BalanceSheet.AsNoTracking().FirstOrDefaultAsync(x => x.GroupID == parms.GroupId && x.MonthID == PreviousMonthFinder(parms.MonthId));
            if (lastMonth == null)
            {
                lastMonth = new BalanceSheet { AcctReceivable = 0, Inventories = 0, TotLiab = 0, Cash = 0 };
            }
            var incomestate = await _context.IncomeState.AsNoTracking().FirstOrDefaultAsync(x => x.GroupID == parms.GroupId && x.MonthID == parms.MonthId);
            var cashflow = new CashFlowDto();

            decimal netIncome = incomestate.NetIncom;
            decimal changeInAsset = (currentMonth.AcctReceivable - lastMonth.AcctReceivable) * 97 / 100 + (currentMonth.Inventories - lastMonth.Inventories);
            decimal changeInLiabi = currentMonth.TotLiab - lastMonth.TotLiab;
            decimal changeInPropertyEquip = await _context.AttributeDecision.AsNoTracking()
                .Where(x => x.GroupID == parms.GroupId && x.MonthID == parms.MonthId)
                .SumAsync(x => x.NewCapital);

            decimal netCashFlow = netIncome - changeInAsset + changeInLiabi + incomestate.PropDepreciationerty;
            decimal previousCash = lastMonth.Cash;
            decimal currentCash = netCashFlow + previousCash - changeInPropertyEquip;
            cashflow.NetIncome.AddChild("Net Income", netIncome);
            cashflow.NetIncome.AddChild("Increase/Decrease in Current Assets", changeInAsset);
            cashflow.NetIncome.AddChild("Increase/Decrease in Total Liabilities", changeInLiabi);
            cashflow.NetIncome.AddChild("Depreciation & Amortization", incomestate.PropDepreciationerty);

            cashflow.NetCashFlow.Data = Money(netCashFlow);
            cashflow.NetCashFlow.AddChild("Increase/Decrease in Property and Equipment", changeInPropertyEquip);
            cashflow.NetCashFlow.AddChild("Cash Balance from Previous Month", previousCash);

            cashflow.CurrentCashBalnce.Data = Money(currentCash);

            return cashflow;

        }

    }
}