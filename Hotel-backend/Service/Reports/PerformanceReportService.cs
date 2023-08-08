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

public class PerformanceReportService : AbstractReportService , IPerformanceReportService
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
       // AppUser student = await _userManager.FindByIdAsync(goalArgs.UserId);
        int monthId = goalArgs.MonthId;

        ClassSession classSession = await _context.ClassSessions.Include(x => x.Groups)
            .Include(x => x.Months)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ClassId == goalArgs.ClassId);
        int quarter = classSession.CurrentQuater;
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

        FinancialRatio finacialRatio = new FinancialRatio();

        finacialRatio.LiquidtyRatios.AddChild(Number("Current Ratio", currentMonthBalSheet.TotCurrentLiab == 0 ? 0 :  currentMonthBalSheet.TotCurrentAsset / currentMonthBalSheet.TotCurrentLiab));
        decimal quickAcidTest = (currentMonthBalSheet.TotCurrentAsset - currentMonthBalSheet.Inventories);
        finacialRatio.LiquidtyRatios.AddChild(Number("Quick (Acid Test) Ratio", currentMonthBalSheet.TotCurrentLiab == 0 ? 0 : quickAcidTest / currentMonthBalSheet.TotCurrentLiab));

        //FinancialRatio liquidtyRatios = new FinancialRatio();
        
            //// Current Ratio
            //liquidtyRatios.Add("Current Ratio", currentMonthBalSheet.TotCurrentLiab == 0 ? null : new Number(currentMonthBalSheet.TotCurrentAsset / currentMonthBalSheet.TotCurrentLiab));
            //// Quick (Acid Test) Ration
            
            //liquidtyRatios.Add("Quick (Acid Test) Ratio", currentMonthBalSheet.TotCurrentLiab == 0 ? null : new Number(quickAcidTest / currentMonthBalSheet.TotCurrentLiab));
            //Accounts Receivable Percentage

            decimal b = incomeState.Room1 * 100 / 52;
            decimal a = currentMonthBalSheet.AcctReceivable;
            if (quarter > 1)
            {
                var lastMonthBalSheet = await _context.BalanceSheet.Where(x => x.QuarterNo == (quarter - 1) && x.GroupID == groupId).FirstOrDefaultAsync();
                a = (currentMonthBalSheet.AcctReceivable + lastMonthBalSheet.AcctReceivable) / 2;
            }
            Number AccountsReceivablePercentage = b == 0 ? null : new Number(a / b);
            Number AccountsReceivableTurnover = a == 0 ? null : new Number(b / a);
            finacialRatio.LiquidtyRatios.AddChild(Number("Accounts Receivable Percentage", AccountsReceivablePercentage));
            finacialRatio.LiquidtyRatios.AddChild(Number("Accounts Receivable Turnover", AccountsReceivableTurnover));
            

        

        
        
            /*
             * Total Assets to Total Liabilities
             * Total Liabilities to Total Assests 
             */
            
                decimal aSolvency = currentMonthBalSheet.TotAsset;
                decimal bSolvency = currentMonthBalSheet.TotLiab;
                finacialRatio.SolvencyRatios.AddChild(Number("Total Assets to Total Liabilities", bSolvency == 0 ? null : new Number(aSolvency / bSolvency)));
                finacialRatio.SolvencyRatios.AddChild(Number("Total Liabilities to Total Assests", aSolvency == 0 ? null : new Number(bSolvency / aSolvency)));
            
            //  Total Liabilities to Total Equity
            // Stockholder Equity
            
                decimal aLiab = currentMonthBalSheet.TotLiab;
                decimal bRetained = currentMonthBalSheet.RetainedEarn + 35000000;
    finacialRatio.SolvencyRatios.AddChild(Number("Total Liabilities to Total Equity", bRetained == 0 ? null : new Number(aLiab / bRetained)));
        //todo: find misssing piece
        finacialRatio.SolvencyRatios.AddChild(Number("Stockholder Equity", null));
            
        

        //FinancialRatio ProfitablityRation = new FinancialRatio();
        
            /*
              Operating Efficenty Ratio
            Net Income to Revenue (Profit Margin)
            Gross Return on Assests (Gross ROA)
            Net Returun on Assests (Net ROA)
            return on Equity (ROE)

             */

            //Operating Efficenty Ratio
            
                decimal abfCharg = incomeState.IncomBfCharg;
                decimal bRooml = incomeState.Room1 * 100 / 52;

        finacialRatio.ProfitablityRation.AddChild(Number("Operating Efficenty Ratio", bRooml == 0 ? null : (abfCharg / bRooml)));
            

            //Net Income to Revenue (Profit Margin)
            
                decimal aIncom = incomeState.NetIncom;
                decimal bIncomeStateRooml = incomeState.Room1 * 100 / 52;
        finacialRatio.ProfitablityRation.AddChild(Number("Net Income to Revenue (Profit Margin)", b == 0 ? null : new Number(a / b)));
            
            //Gross Return on Assests (Gross ROA)
            //Net Returun on Assests (Net ROA)
            
                decimal aAjstNetIncom = incomeState.AjstNetIncom;
                decimal bAjstNetIncom = 0;


                if (quarter > 1)
                {
                    var lastMonthBalSheet = await _context.BalanceSheet.AsNoTracking().Where(x => x.MonthID == monthId && x.QuarterNo == (quarter - 1) && x.GroupID == groupId).FirstOrDefaultAsync();
            bAjstNetIncom = (currentMonthBalSheet.TotAsset + lastMonthBalSheet.TotAsset) / 2;
                }
        bAjstNetIncom = currentMonthBalSheet.TotAsset;
                if (bAjstNetIncom == 0)
                {
            finacialRatio.ProfitablityRation.AddChild(Number("Gross Return on Assests (Gross ROA)", null));
            finacialRatio.ProfitablityRation.AddChild(Number("Net Returun on Assests (Net ROA)", null));
                }
                {
            finacialRatio.ProfitablityRation.AddChild(Number("Gross Return on Assests (Gross ROA)", new Number(aAjstNetIncom / bAjstNetIncom)));
            finacialRatio.ProfitablityRation.AddChild(Number("Net Returun on Assests (Net ROA)", new Number(incomeState.NetIncom / bAjstNetIncom)));
                }
            
            //Return on Equity (ROE)
            
                decimal aNetIncom = incomeState.NetIncom;
                decimal bNetIncom = 0;
                decimal cNetIncom = 0;

                if (quarter > 1)
                {
                    var lastMonthBalSheet = await _context.BalanceSheet.AsNoTracking().Where(x => x.MonthID == monthId && x.QuarterNo == (quarter - 1) && x.GroupID == groupId).FirstOrDefaultAsync();
            bNetIncom = (currentMonthBalSheet.RetainedEarn + 35000000 + lastMonthBalSheet.RetainedEarn + 35000000) / 2;
                }
                else
                {
                    bNetIncom = currentMonthBalSheet.RetainedEarn + 35000000;
                }

        finacialRatio.ProfitablityRation.AddChild(Number("Return on Equity (ROE)", b == 0 ? null : new Number(aNetIncom / bNetIncom)));
            

        

        return new PerformanceReportDto
        {
            Statstics = statisticsDto,
            FinancialRatios = finacialRatio
        };

    }
}




