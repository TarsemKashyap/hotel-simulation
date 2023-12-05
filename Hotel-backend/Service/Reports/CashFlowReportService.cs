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

        public async Task<CashFlowDto> GenerateReport(ReportParams parms)
        {
            var currentMonth = await _context.BalanceSheet.AsNoTracking().FirstOrDefaultAsync(x => x.GroupID == parms.GroupId && x.QuarterNo == parms.CurrentQuarter && x.MonthID == parms.MonthId);
            TryFindLastMonth(_context, parms.MonthId, out Month lastMonth);
            var lastMonthBalance = await _context.BalanceSheet.AsNoTracking().FirstOrDefaultAsync(x => x.GroupID == parms.GroupId && x.MonthID == lastMonth.MonthId && x.QuarterNo == lastMonth.Sequence);
            if (lastMonthBalance == null)
            {
                lastMonthBalance = new BalanceSheet { AcctReceivable = 0, Inventories = 0, TotLiab = 0, Cash = 0 };
            }
            var incomestate = await _context.IncomeState.AsNoTracking().FirstOrDefaultAsync(x => x.GroupID == parms.GroupId && x.MonthID == parms.MonthId);
            var cashflow = new CashFlowDto();

            decimal netIncome = incomestate.NetIncom;
            decimal changeInAsset = (currentMonth.AcctReceivable - lastMonthBalance.AcctReceivable) * 97 / 100 + (currentMonth.Inventories - lastMonthBalance.Inventories);
            decimal changeInLiabi = currentMonth.TotLiab - lastMonthBalance.TotLiab;
            decimal changeInPropertyEquip = await _context.AttributeDecision.AsNoTracking()
                .Where(x => x.GroupID == parms.GroupId && x.MonthID == parms.MonthId)
                .SumAsync(x => x.NewCapital);

            decimal netCashFlow = netIncome - changeInAsset + changeInLiabi + incomestate.PropDepreciationerty;
            decimal previousCash = lastMonthBalance.Cash;
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