using Common.ReportDto;
using Database;
using Database.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service;

public interface IAverageDailyRateReportService
{
    Task<AverageDailyRateDto> ReportAsync(ReportParams p);
}

public class AverageDailyRateReportService : AbstractReportService, IAverageDailyRateReportService
{
    private readonly HotelDbContext _context;

    public AverageDailyRateReportService(HotelDbContext context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    public async Task<AverageDailyRateDto> ReportAsync(ReportParams p)
    {
        decimal overallADR;
        decimal overallADRMarket;
        decimal weekdayADR;
        decimal weekdayADRMarket;
        decimal weekendADR;
        decimal weekendADRMarket;


        int groupCount = _context.ClassGroups.Where(x => x.ClassId == p.ClassId).Count();
        var soldRoomList = await _context.SoldRoomByChannel.Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter).ToListAsync();

        decimal roomRevenue = soldRoomList.Where(x => x.GroupID == p.GroupId).Sum(x => x.Revenue);

        decimal roomSold = soldRoomList.Where(x => x.GroupID == p.GroupId).Sum(x => x.SoldRoom);

        if (roomSold == 0)
        {
            overallADR = 0;
        }
        else
        {
            overallADR = roomRevenue / roomSold;
        }

        //overallADRMarket
        roomRevenue = soldRoomList.Sum(x => x.Revenue);
        roomSold = soldRoomList.Sum(x => x.SoldRoom);
        if (roomSold == 0)
        {
            overallADRMarket = 1;
        }
        else
        {
            overallADRMarket = roomRevenue / roomSold;
        }

        //weekdayADR
        roomRevenue = soldRoomList.Where(x => x.GroupID == p.GroupId && x.Weekday).Sum(x => x.Revenue);
        roomSold = soldRoomList.Where(x => x.GroupID == p.GroupId && x.Weekday).Sum(x => x.SoldRoom);
        if (roomSold == 0)
        {
            weekdayADR = 0;
        }
        else
        {
            weekdayADR = roomRevenue / roomSold;
        }

        //weekdayADRMarket
        roomRevenue = soldRoomList.Where(x => x.Weekday).Sum(x => x.Revenue);
        roomSold = soldRoomList.Where(x => x.Weekday).Sum(x => x.SoldRoom);
        if (roomSold == 0)
        {
            weekdayADRMarket = 1;
        }
        else
        {
            weekdayADRMarket = roomRevenue / roomSold;
        }

        //weekendADR
        roomRevenue = soldRoomList.Where(x => x.GroupID == p.GroupId && !x.Weekday).Sum(x => x.Revenue);
        roomSold = soldRoomList.Where(x => x.GroupID == p.GroupId && !x.Weekday).Sum(x => x.SoldRoom);
        if (roomSold == 0)
        {
            weekendADR = 0;
        }
        else
        {
            weekendADR = roomRevenue / roomSold;
        }

        //weekendADRMarket
        roomRevenue = soldRoomList.Where(x => !x.Weekday).Sum(x => x.Revenue);
        roomSold = soldRoomList.Where(x => !x.Weekday).Sum(x => x.SoldRoom);
        if (roomSold == 0)
        {
            weekendADRMarket = 1;
        }
        else
        {
            weekendADRMarket = roomRevenue / roomSold;
        }

        AverageDailyRateDto reportDto = new AverageDailyRateDto();
        reportDto.OverAllADR(overallADR, overallADRMarket);

        reportDto.WeekdayADR(weekdayADR, weekdayADRMarket);
        reportDto.WeekendADR(weekendADR, weekendADRMarket);
        return reportDto;

    }
}

