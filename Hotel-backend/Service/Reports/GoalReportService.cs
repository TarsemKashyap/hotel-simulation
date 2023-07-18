using Database;
using System.Threading;
using System.Threading.Tasks;
namespace Service;

using Common.ReportDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.Sig;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Transactions;

public class GoalReportService
{
    private readonly HotelDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public GoalReportService(HotelDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    private IQueryable<SoldRoomByChannel> SoldRoomQueryAsync => _context.SoldRoomByChannel.AsNoTracking();

    public async Task GenerateReport(GoalClassParams p)
    {
        AppUser student = await _userManager.FindByIdAsync(p.UserId);

        ClassSession classSession = await _context.ClassSessions.Include(x => x.Groups)
            .Include(x => x.Months)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ClassId == p.ClassId);
        int quarter = p.currentQuarter;
        int hotelCount = classSession.HotelsCount;



        int roomAllocated = _context.RoomAllocation.AsNoTracking().Where(x => x.QuarterNo == quarter).Sum(x => x.RoomsAllocated);




        decimal soldRoomsByMonthGroup = await SoldRoomQueryAsync.Where(x => x.QuarterNo == quarter && x.GroupID == p.GroupId).SumAsync(x => x.SoldRoom);
        decimal soldRoomsByMonth = await SoldRoomQueryAsync.Where(x => x.QuarterNo == quarter).SumAsync(x => x.SoldRoom);
        // decimal soldRoomsMarketAverage = await SoldRoomQueryAsync.Where(x => x.QuarterNo == quarter).SumAsync(x => x.SoldRoom);
        decimal perf = soldRoomsByMonthGroup / 500 / 30;
        decimal marketAverage = soldRoomsByMonth / 500 / 30 / classSession.Groups.Count;
        Goal goalByMonthGroup = _context.Goal.AsNoTracking().Where(x => x.MonthID == quarter && x.GroupID == p.GroupId).FirstOrDefault();
        decimal occupancyM = Convert.ToDecimal(goalByMonthGroup == null ? 0 : goalByMonthGroup.OccupancyM);
        GoalReportDto occupancyPercentage = new GoalReportDto
        {

            Indicators = "Occupancy percentage",
            M_P = perf, //ToString("p"),
            M_M = roomAllocated == 0 ? 0 : marketAverage,//ToString("p")
            M_G = occupancyM,//ToString("p")
            Formatter = "P"
        };

        var incomeState = await _context.IncomeState.AsNoTracking().Where(x => x.QuarterNo == quarter && x.GroupID == p.GroupId).FirstOrDefaultAsync();
        var marketAverageIncome = await _context.IncomeState.AsNoTracking().Where(x => x.QuarterNo == quarter).ToListAsync();
        GoalReportDto roomRevenue = new GoalReportDto
        {
            Indicators = "Room revenue",
            M_P = incomeState == null ? 0 : incomeState.Room1, // C0
            M_M = (decimal)marketAverageIncome.Average(x => x.Room1),
            M_G = Convert.ToDecimal(goalByMonthGroup == null ? 0 : goalByMonthGroup.RoomRevenM),
            Formatter = "C0"
        };

        GoalReportDto totalRevenu = new GoalReportDto
        {
            Indicators = "Total revenue",
            M_P = (incomeState == null ? 0 : incomeState.Room1) * 100 / 52,
            M_M = (decimal)marketAverageIncome.Average(x => x.TotReven),
            M_G = Convert.ToDecimal(goalByMonthGroup == null ? 0 : goalByMonthGroup.TotalRevenM),
            Formatter = "C0"
        };



        GoalReportDto marketShareByRooms = new GoalReportDto
        {
            Indicators = "Market Share based on Number of Rooms",
            M_P = soldRoomsByMonthGroup / soldRoomsByMonth,
            M_M = 1 / classSession.Groups.Count,
            M_G = Convert.ToDecimal(goalByMonthGroup == null ? 0 : goalByMonthGroup.ShareRoomM),
            Formatter = "P"
        };
        int roomSoldByRevenue = await SoldRoomQueryAsync.Where(x => x.QuarterNo == quarter && x.GroupID == p.GroupId).SumAsync(x => x.Revenue);
        int roomSoldByRevenuMonthly = await SoldRoomQueryAsync.Where(x => x.QuarterNo == quarter).SumAsync(x => x.Revenue);
        GoalReportDto marketShareByRevenue = new GoalReportDto
        {
            Indicators = "Market Share based on Revenues",
            M_P = roomSoldByRevenue / roomSoldByRevenuMonthly,
            M_M = 1 / classSession.Groups.Count,
            M_G = Convert.ToDecimal(goalByMonthGroup == null ? 0 : goalByMonthGroup.ShareRevenM),
            Formatter = "P"
        };
        var groupIds = classSession.Groups.Select(x => x.GroupId).ToList();
        int revParRoomSold = await _context.RoomAllocation.AsNoTracking().Where(x => x.QuarterNo == quarter && x.GroupID == p.GroupId).SumAsync(x => x.RoomsAllocated);
        int groupRoomAllocatedsum = await SoldRoomQueryAsync.Where(x => x.QuarterNo == quarter && groupIds.Contains(x.GroupID)).SumAsync(x => x.Revenue);
        int roomAllocatedRevPar = 15000 * classSession.Groups.Count;

        GoalReportDto RevPar = new GoalReportDto
        {
            Indicators = "REVPAR",
            M_P = roomSoldByRevenue / 15000,
            M_M = roomAllocatedRevPar == 0 ? 0 : groupRoomAllocatedsum / roomAllocatedRevPar,
            M_G = Convert.ToDecimal(goalByMonthGroup == null ? 0 : goalByMonthGroup.RevparM),
            Formatter = "P"
        };

        var adRSoldRoomList = await SoldRoomQueryAsync.Where(x => x.QuarterNo == quarter && groupIds.Contains(x.GroupID)).Select(x => new { x.Revenue, x.SoldRoom }).ToListAsync();
        int adrGroupRevenuSum = adRSoldRoomList.Sum(x => x.Revenue);
        int adrRoomSold = adRSoldRoomList.Sum(x => x.SoldRoom);
        GoalReportDto ADR = new GoalReportDto
        {
            Indicators = "ADR",
            M_P = roomSoldByRevenue / soldRoomsByMonthGroup,
            M_M = adrRoomSold == 0 ? 0 : adrGroupRevenuSum / adrRoomSold,
            M_G = Convert.ToDecimal(goalByMonthGroup == null ? 0 : goalByMonthGroup.ADRM),
            Formatter = "C"
        };

        GoalReportDto yieldManagement = new GoalReportDto
        {
            Indicators = "ADR",
            M_P = roomSoldByRevenue / soldRoomsByMonthGroup,
            M_M = adrRoomSold == 0 ? 0 : adrGroupRevenuSum / adrRoomSold,
            M_G = Convert.ToDecimal(goalByMonthGroup == null ? 0 : goalByMonthGroup.ADRM),
            Formatter = "C"
        };



    }
}

public record GoalClassParams(string UserId, int ClassId, int currentQuarter, int GroupId);