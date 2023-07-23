using Database;
using System.Threading.Tasks;
using Common.ReportDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Common;

namespace Service;

public interface IPerformanceReportService
{

    Task<PerformanceReportDto> PerformanceReport(ReportParams goalArgs);
}

public class PerformanceReportService : IPerformanceReportService
{
    private readonly HotelDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public PerformanceReportService(HotelDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    private IQueryable<IncomeState> IncomeStateQuery => _context.IncomeState.AsNoTracking();
    private IQueryable<SoldRoomByChannel> SoldRoomByChannelQuery => _context.SoldRoomByChannel.AsNoTracking();

    public async Task<PerformanceReportDto> PerformanceReport(ReportParams goalArgs)
    {
        AppUser student = await _userManager.FindByIdAsync(goalArgs.UserId);
        int monthId = goalArgs.MonthId;

        ClassSession classSession = await _context.ClassSessions.Include(x => x.Groups)
            .Include(x => x.Months)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ClassId == goalArgs.ClassId);
        int quarter = goalArgs.CurrentQuarter;
        int groupId = goalArgs.GroupId;
        int hotelCount = classSession.HotelsCount;
        var incomeState = await IncomeStateQuery.FirstOrDefaultAsync(x => x.QuarterNo == quarter && x.GroupID == goalArgs.GroupId);
        decimal monthlyProfit = incomeState.NetIncom;
        decimal accumplateProfit = IncomeStateQuery.Where(x => x.QuarterNo <= quarter).Sum(x => x.NetIncom);
        var soldRoomListMonth = await _context.SoldRoomByChannel.AsNoTracking()
            .Where(x => x.QuarterNo == goalArgs.CurrentQuarter).ToListAsync();
        var ScalarGroupRoomRevenueByMonth = soldRoomListMonth.Where(x => x.GroupID == goalArgs.GroupId).Sum(x => x.Revenue);
        var ScalarMarketRomRevenByMonth = await _context.SoldRoomByChannel.AsNoTracking()
            .Where(x => x.QuarterNo == goalArgs.CurrentQuarter)
            .SumAsync(x => x.Revenue);
        var ScalarGroupRomSoldByMonth = soldRoomListMonth.Where(x => x.GroupID == goalArgs.GroupId).Sum(x => x.SoldRoom);
        var ScalarMarketRomSoldByMonth = soldRoomListMonth.Sum(x => x.SoldRoom);
        var ScalarQueryRoomAllocated = await _context.RoomAllocation.AsNoTracking()
            .Where(x => x.QuarterNo == goalArgs.CurrentQuarter && x.GroupID == goalArgs.GroupId)
            .SumAsync(x => x.RoomsAllocated);

        StatisticsDto statisticsDto = new StatisticsDto
        {
            MonthlyProfit = new Currency(monthlyProfit),
            AccumulativeProfit = new Currency(accumplateProfit),
            MarketShareRevenueBased = new Percent(ScalarMarketRomRevenByMonth == 0 ? 0 : ScalarGroupRoomRevenueByMonth / ScalarMarketRomRevenByMonth),
            MarketShareRoomSoldBased = new Percent(ScalarMarketRomSoldByMonth == 0 ? 0 : ScalarGroupRomSoldByMonth / ScalarMarketRomSoldByMonth),
            Occupancy = new Percent(ScalarQueryRoomAllocated == 0 ? 0 : (ScalarGroupRomSoldByMonth / 500 / 30)),
            REVPAR = new Currency(ScalarGroupRomSoldByMonth == 0 ? 0 : (ScalarGroupRoomRevenueByMonth / 15000))
        };

        BalanceSheet currentMonthBalSheet = await _context.BalanceSheet.AsNoTracking().FirstOrDefaultAsync(x => x.MonthID == monthId && x.QuarterNo == quarter && x.GroupID == groupId);
        FinancialRatio liquidtyRatios = new FinancialRatio("Liquidity Ratios");
        {
            // Current Ratio
            liquidtyRatios.Add("Current Ratio", currentMonthBalSheet.TotCurrentLiab == 0 ? null : new Number(currentMonthBalSheet.TotCurrentAsset / currentMonthBalSheet.TotCurrentLiab));
            // Quick (Acid Test) Ration
            decimal quickAcidTest = (currentMonthBalSheet.TotCurrentAsset - currentMonthBalSheet.Inventories);
            liquidtyRatios.Add("Quick (Acid Test) Ratio", currentMonthBalSheet.TotCurrentLiab == 0 ? null : new Number(quickAcidTest / currentMonthBalSheet.TotCurrentLiab));
            //Accounts Receivable Percentage

            decimal b = incomeState.Room1 * 100 / 52;
            decimal a = currentMonthBalSheet.AcctReceivable;
            if (quarter > 1)
            {
                var lastMonthBalSheet = await _context.BalanceSheet.Where(x => x.QuarterNo == (quarter - 1) && x.GroupID == groupId).FirstOrDefaultAsync();
                a = (currentMonthBalSheet.AcctReceivable + lastMonthBalSheet.AcctReceivable) / 2;
            }
            Number AccountsReceivablePercentage = b == 0 ? null : new Number(a / b);

            liquidtyRatios.Add("Accounts Receivable Percentage", AccountsReceivablePercentage);
            Number AccountsReceivableTurnover = a == 0 ? null : new Number(b / a);
            liquidtyRatios.Add("Accounts Receivable Turnover", AccountsReceivableTurnover);

        }

        FinancialRatio solvencyRatios = new FinancialRatio("Solvency Ratios");
        {
            /*
             * Total Assets to Total Liabilities
             * Total Liabilities to Total Assests 
             */
            {
                decimal a = currentMonthBalSheet.TotAsset;
                decimal b = currentMonthBalSheet.TotLiab;
                solvencyRatios.Add("Total Assets to Total Liabilities", b == 0 ? null : new Number(a / b));
                solvencyRatios.Add("Total Liabilities to Total Assests", a == 0 ? null : new Number(b / a));
            }
            //  Total Liabilities to Total Equity
            // Stockholder Equity
            {
                decimal a = currentMonthBalSheet.TotLiab;
                decimal b = currentMonthBalSheet.RetainedEarn + 35000000;
                solvencyRatios.Add("Total Liabilities to Total Equity", b == 0 ? null : new Number(a / b));
                //todo: find misssing piece
                solvencyRatios.Add("Stockholder Equity", null);
            }
        }

        FinancialRatio ProfitablityRation = new FinancialRatio("Profitablity Ratios");
        {
            /*
              Operating Efficenty Ratio
            Net Income to Revenue (Profit Margin)
            Gross Return on Assests (Gross ROA)
            Net Returun on Assests (Net ROA)
            return on Equity (ROE)

             */

            //Operating Efficenty Ratio
            {
                decimal a = incomeState.IncomBfCharg;
                decimal b = incomeState.Room1 * 100 / 52;

                ProfitablityRation.Add("Operating Efficenty Ratio", b == 0 ? null : new Number(a / b));
            }

            //Net Income to Revenue (Profit Margin)
            {
                decimal a = incomeState.NetIncom;
                decimal b = incomeState.Room1 * 100 / 52;
                ProfitablityRation.Add("Net Income to Revenue (Profit Margin)", b == 0 ? null : new Number(a / b));
            }
            //Gross Return on Assests (Gross ROA)
            //Net Returun on Assests (Net ROA)
            {
                decimal a = incomeState.AjstNetIncom;
                decimal b = 0;


                if (quarter > 1)
                {
                    var lastMonthBalSheet = await _context.BalanceSheet.AsNoTracking().Where(x => x.MonthID == monthId && x.QuarterNo == (quarter - 1) && x.GroupID == groupId).FirstOrDefaultAsync();
                    b = (currentMonthBalSheet.TotAsset + lastMonthBalSheet.TotAsset) / 2;
                }
                b = currentMonthBalSheet.TotAsset;
                if (b == 0)
                {
                    ProfitablityRation.Add("Gross Return on Assests (Gross ROA)", null);
                    ProfitablityRation.Add("Net Returun on Assests (Net ROA)", null);
                }
                {
                    ProfitablityRation.Add("Gross Return on Assests (Gross ROA)", new Number(a / b));
                    ProfitablityRation.Add("Net Returun on Assests (Net ROA)", new Number(incomeState.NetIncom / b));
                }
            }
            //Return on Equity (ROE)
            {
                decimal a = incomeState.NetIncom;
                decimal b = 0;
                decimal c = 0;

                if (quarter > 1)
                {
                    var lastMonthBalSheet = await _context.BalanceSheet.AsNoTracking().Where(x => x.MonthID == monthId && x.QuarterNo == (quarter - 1) && x.GroupID == groupId).FirstOrDefaultAsync();
                    b = (currentMonthBalSheet.RetainedEarn + 35000000 + lastMonthBalSheet.RetainedEarn + 35000000) / 2;
                }
                else
                {
                    b = currentMonthBalSheet.RetainedEarn + 35000000;
                }

                ProfitablityRation.Add("Return on Equity (ROE)", b == 0 ? null : new Number(a / b));
            }

        }

        return new PerformanceReportDto
        {
            Statstics = statisticsDto,
            FinancialRatios = new() { liquidtyRatios, solvencyRatios, ProfitablityRation }
        };

    }
}




