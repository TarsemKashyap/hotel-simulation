using Database;
using System.Threading;
using System.Threading.Tasks;
namespace Service;

using Common.ReportDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.Sig;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;

public class GoalReportService : IGoalReportService
{
    private readonly HotelDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public GoalReportService(HotelDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    private IQueryable<SoldRoomByChannel> SoldRoomQueryAsync => _context.SoldRoomByChannel.AsNoTracking();

    public async Task<List<GoalReportResponse>> GenerateReport(ReportParams goalArgs)
    {
        AppUser student = await _userManager.FindByIdAsync(goalArgs.UserId);

        ClassSession classSession = await _context.ClassSessions.Include(x => x.Groups)
            .Include(x => x.Months)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ClassId == goalArgs.ClassId);
        int quarter = goalArgs.CurrentQuarter;
        int monthId = goalArgs.MonthId;
        int hotelCount = classSession.HotelsCount;



        int ScalarQueryMarketRoomAllocated = _context.RoomAllocation.AsNoTracking().Where(x => x.MonthID == monthId && x.QuarterNo == quarter).Sum(x => x.RoomsAllocated);




        decimal soldRoomsByMonthGroup = await SoldRoomQueryAsync.Where(x => x.MonthID == monthId && x.QuarterNo == quarter && x.GroupID == goalArgs.GroupId).SumAsync(x => x.SoldRoom);
        decimal ScalarMarketRomSoldByMonth = await SoldRoomQueryAsync.Where(x => x.MonthID == monthId && x.QuarterNo == quarter).SumAsync(x => x.SoldRoom);
        // decimal soldRoomsMarketAverage = await SoldRoomQueryAsync.Where(x => x.QuarterNo == quarter).SumAsync(x => x.SoldRoom);
        decimal perf = soldRoomsByMonthGroup / 500 / 30;
        decimal marketAverage = (ScalarMarketRomSoldByMonth / 500 / 30) / classSession.Groups.Count;
        Goal goalByMonthGroup = _context.Goal.AsNoTracking().Where(x => x.MonthID == monthId && x.QuarterNo == quarter && x.GroupID == goalArgs.GroupId).FirstOrDefault();
        decimal occupancyM = Convert.ToDecimal(goalByMonthGroup == null ? 0 : goalByMonthGroup.OccupancyM);
        GoalReportDto occupancyPercentage = new GoalReportDto
        {

            Indicators = "Occupancy percentage",
            M_P = perf, //ToString("p"),
            M_M = ScalarQueryMarketRoomAllocated == 0 ? 0 : marketAverage,//ToString("p")
            M_G = occupancyM,//ToString("p")
            Formatter = "P"
        };

        var incomeState = await _context.IncomeState.AsNoTracking().Where(x => x.MonthID == monthId && x.QuarterNo == quarter && x.GroupID == goalArgs.GroupId).FirstOrDefaultAsync();
        var incomeStateMonthlyList = await _context.IncomeState.AsNoTracking().Where(x => x.MonthID == monthId && x.QuarterNo == quarter).ToListAsync();
        GoalReportDto roomRevenue = new GoalReportDto
        {
            Indicators = "Room revenue",
            M_P = incomeState == null ? 0 : incomeState.Room1, // C0
            M_M = (decimal)incomeStateMonthlyList.Average(x => x.Room1),
            M_G = Convert.ToDecimal(goalByMonthGroup == null ? 0 : goalByMonthGroup.RoomRevenM),
            Formatter = "C0"
        };

        GoalReportDto totalRevenu = new GoalReportDto
        {
            Indicators = "Total revenue",
            M_P = (incomeState == null ? 0 : incomeState.Room1) * 100 / 52,
            M_M = (decimal)incomeStateMonthlyList.Average(x => x.TotReven),
            M_G = Convert.ToDecimal(goalByMonthGroup == null ? 0 : goalByMonthGroup.TotalRevenM),
            Formatter = "C0"
        };



        GoalReportDto marketShareByRooms = new GoalReportDto
        {
            Indicators = "Market Share based on Number of Rooms",
            M_P = ScalarMarketRomSoldByMonth == 0 ? 0 : soldRoomsByMonthGroup / ScalarMarketRomSoldByMonth,
            M_M = 1 / classSession.Groups.Count,
            M_G = Convert.ToDecimal(goalByMonthGroup == null ? 0 : goalByMonthGroup.ShareRoomM),
            Formatter = "P"
        };
        int roomSoldByRevenue = await SoldRoomQueryAsync.Where(x => x.MonthID == monthId && x.QuarterNo == quarter && x.GroupID == goalArgs.GroupId).SumAsync(x => x.Revenue);
        int roomSoldByRevenuMonthly = await SoldRoomQueryAsync.Where(x => x.QuarterNo == quarter).SumAsync(x => x.Revenue);
        GoalReportDto marketShareByRevenue = new GoalReportDto
        {
            Indicators = "Market Share based on Revenues",
            M_P = roomSoldByRevenuMonthly == 0 ? 0 : roomSoldByRevenue / roomSoldByRevenuMonthly,
            M_M = 1 / classSession.Groups.Count,
            M_G = Convert.ToDecimal(goalByMonthGroup == null ? 0 : goalByMonthGroup.ShareRevenM),
            Formatter = "P"
        };
        var groupIds = classSession.Groups.Select(x => x.GroupId).ToList();
        int revParRoomSold = await _context.RoomAllocation.AsNoTracking().Where(x => x.MonthID == monthId && x.QuarterNo == quarter && x.GroupID == goalArgs.GroupId).SumAsync(x => x.RoomsAllocated);
        int groupRoomAllocatedsum = await SoldRoomQueryAsync.Where(x => x.MonthID == monthId && x.QuarterNo == quarter && groupIds.Contains(x.GroupID)).SumAsync(x => x.Revenue);
        int roomAllocatedRevPar = 15000 * classSession.Groups.Count;

        GoalReportDto RevPar = new GoalReportDto
        {
            Indicators = "REVPAR",
            M_P = roomSoldByRevenue / 15000,
            M_M = roomAllocatedRevPar == 0 ? 0 : groupRoomAllocatedsum / roomAllocatedRevPar,
            M_G = Convert.ToDecimal(goalByMonthGroup == null ? 0 : goalByMonthGroup.RevparM),
            Formatter = "P"
        };

        var adRSoldRoomList = await SoldRoomQueryAsync.Where(x => x.MonthID == monthId && x.QuarterNo == quarter && groupIds.Contains(x.GroupID)).Select(x => new { x.Revenue, x.SoldRoom }).ToListAsync();
        int adrGroupRevenuSum = adRSoldRoomList.Sum(x => x.Revenue);
        int adrRoomSold = adRSoldRoomList.Sum(x => x.SoldRoom);
        GoalReportDto ADR = new GoalReportDto
        {
            Indicators = "ADR",
            M_P = soldRoomsByMonthGroup == 0 ? 0 : roomSoldByRevenue / soldRoomsByMonthGroup,
            M_M = adrRoomSold == 0 ? 0 : adrGroupRevenuSum / adrRoomSold,
            M_G = Convert.ToDecimal(goalByMonthGroup == null ? 0 : goalByMonthGroup.ADRM),
            Formatter = "C"
        };

        decimal yieldOccupancy = 0, AARate = 0, potentialRate = 0;
        yieldOccupancy = soldRoomsByMonthGroup == 0 ? 0 : (soldRoomsByMonthGroup / 500 / 30);
        AARate = soldRoomsByMonthGroup == 0 ? 0 : roomSoldByRevenue / soldRoomsByMonthGroup;
        //  roomSoldByRevenuMonthly ,soldRoomsByMonth
        potentialRate = roomSoldByRevenuMonthly == 0 ? 0 : roomSoldByRevenuMonthly / roomSoldByRevenuMonthly;
        GoalReportDto yieldManagement = new GoalReportDto
        {
            Indicators = "Yield Management",
            M_P = potentialRate == 0 ? 0 : yieldOccupancy * AARate / potentialRate,
            M_M = (ScalarMarketRomSoldByMonth / 500 / 30) / classSession.Groups.Count,
            M_G = Convert.ToDecimal(goalByMonthGroup == null ? 0 : goalByMonthGroup.YieldMgtM),
            Formatter = "P"
        };


        var incomState = await _context.IncomeState.AsNoTracking().FirstOrDefaultAsync(x => x.MonthID == monthId && x.QuarterNo == quarter && x.GroupID == goalArgs.GroupId);
        int roomSoldIncomestate = incomState.Room1 == 0 ? 0 : incomState.Room1 * 100 / 52;

        var ScalarMonthAvgIncomeBFcharge = incomeStateMonthlyList.Average(x => x.IncomBfCharg);
        var ScalarMonthAvgRoomRevenue = incomeStateMonthlyList.Average(x => x.Room1);

        GoalReportDto operatingEffeciyRatio = new GoalReportDto
        {
            Indicators = "Operating Efficiency Ratio",
            M_P = roomSoldIncomestate == 0 ? 0 : incomeState.IncomBfCharg / roomSoldIncomestate,
            M_M = Convert.ToDecimal(ScalarMonthAvgIncomeBFcharge / ScalarMonthAvgRoomRevenue),
            M_G = Convert.ToDecimal(goalByMonthGroup == null ? 0 : goalByMonthGroup.MgtEfficiencyM),
            Formatter = "P"
        };


        var ScalarMonthAvgProfit = incomeStateMonthlyList.Average(x => x.NetIncom);
        var ScalarMonthAvgTotalRevenue = incomeStateMonthlyList.Average(x => x.TotReven);
        int profit = (incomeState.Room1 * 100 / 52);
        GoalReportDto profitMargin = new GoalReportDto
        {
            Indicators = "Profit Margin",
            M_P = profit == 0 ? 0 : incomState.NetIncom / profit,
            M_M = Convert.ToDecimal(ScalarMonthAvgProfit / ScalarMonthAvgTotalRevenue),
            M_G = Convert.ToDecimal(goalByMonthGroup == null ? 0 : goalByMonthGroup.ProfitMarginM),
            Formatter = "P"
        };


        return new List<GoalReportResponse>() {
            occupancyPercentage.ToGoalReportResponse(),
            roomRevenue.ToGoalReportResponse(),
            totalRevenu.ToGoalReportResponse(),
            marketShareByRooms.ToGoalReportResponse(),
            marketShareByRevenue.ToGoalReportResponse(),
            RevPar.ToGoalReportResponse(),
            ADR.ToGoalReportResponse(),
            yieldManagement.ToGoalReportResponse(),
            operatingEffeciyRatio.ToGoalReportResponse()
        };


        //ScalarMarketRomSoldByMonth

    }
}
