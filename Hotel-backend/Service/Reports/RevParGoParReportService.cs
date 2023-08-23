using Common.ReportDto;
using Database;
using Database.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Service;

public interface IRevParGoParReportService
{
    Task<RevparReportDto> ReportAsync(ReportParams p);
}

public class RevParGoParReportService : IRevParGoParReportService
{
    private readonly HotelDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public RevParGoParReportService(HotelDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    public async Task<RevparReportDto> ReportAsync(ReportParams p)
    {

        decimal reven;
        int groupNumber;


        decimal overall;
        decimal overallMarket;
        decimal weekdayRevpar;
        decimal weekdayRevparMarket;
        decimal weekendRevpar;
        decimal weekendRevparMarket;

        decimal totalPar;
        decimal totalParMarket;

        decimal goPar;
        decimal goParMarket;

        groupNumber = await _context.ClassGroups.CountAsync(x => x.ClassId == p.ClassId);
        decimal averageShare;
        if (groupNumber == 0)
        {
            averageShare = 1;
        }
        else
        {
            averageShare = 1 / Convert.ToDecimal(groupNumber);
        }

        var soldRoomList = await _context.SoldRoomByChannel.Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter).ToListAsync();
        //overallRevpar for a hotel
        reven = soldRoomList.Where(x => x.GroupID == p.GroupId).Sum(x => x.Revenue);
        overall = reven / 15000;

        //Market Revpar Overall
        reven = soldRoomList.Sum(x => x.Revenue);
        overallMarket = reven / 15000 / groupNumber;

        //Weekday REVPAR
        reven = soldRoomList.Where(x => x.GroupID == p.GroupId && x.Weekday).Sum(x => x.Revenue);
        weekdayRevpar = reven / 8500;

        //Weekday Market REVPAR
        reven = soldRoomList.Where(x => x.Weekday).Sum(x => x.Revenue);
        weekdayRevparMarket = reven / 8500 / groupNumber;

        //Weekend REVPAR
        reven = soldRoomList.Where(x => x.GroupID == p.GroupId && !x.Weekday).Sum(x => x.Revenue);
        weekendRevpar = reven / 6500;

        //Weekend Market REVPAR
        reven = soldRoomList.Where(x => !x.Weekday).Sum(x => x.Revenue);
        weekendRevparMarket = reven / 6500 / groupNumber;

        //Total Revpar for one group

        var incomeStateList = await _context.IncomeState.Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter).ToListAsync();
        var incomeState = incomeStateList.FirstOrDefault(x => x.GroupID == p.GroupId);
        reven = incomeState.Room1 * 100 / 52;
        totalPar = reven / 15000;

        //Total REVPAR Market
        reven = incomeStateList.Sum(x => x.Room1) * 100 / 52;
        totalParMarket = reven / 15000 / groupNumber;

        //GoPar for a hotel
        reven = incomeState.GrossProfit;
        goPar = reven / 15000;

        //GoPar market
        reven = incomeStateList.Sum(x => x.GrossProfit);
        goParMarket = reven / 15000 / groupNumber;

        RevparReportDto reportDto = new RevparReportDto();
        reportDto.AddOverAll(overall, overallMarket);

        reportDto.WeekdayRevPar(weekdayRevpar, weekdayRevparMarket);

        reportDto.WeekdendRevPar(weekendRevpar, weekendRevparMarket);

        reportDto.AddTotalRevPar(totalPar, totalParMarket);

        reportDto.AddGoRevPar(goPar, goParMarket);

        return reportDto;

    }
}

