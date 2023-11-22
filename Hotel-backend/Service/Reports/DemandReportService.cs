using Common;
using Common.ReportDto;
using Database;
using Database.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace Service;

public interface IDemandReportService
{
    Task<DemandReportDto> ReportAsync(ReportParams p);
}

public class DemandReportService : AbstractReportService, IDemandReportService
{
    private readonly HotelDbContext _context;
    private ILookup<string, RoomAllocation> _roomAllocation;
    private ILookup<string, SoldRoomByChannel> _soldRooms;

    public DemandReportService(HotelDbContext context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    public async Task<DemandReportDto> ReportAsync(ReportParams p)
    {
        _roomAllocation = (await _context.RoomAllocation
            .Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter && x.GroupID == p.GroupId).ToListAsync()
            ).ToLookup(x => x.Segment.Trim());


        _soldRooms = (await _context.SoldRoomByChannel
            .Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter && x.GroupID == p.GroupId).ToListAsync())
            .ToLookup(x => x.Segment.Trim());

        var (overAllWeekdayDemand, overAllWeekdayRoomSold) = await OverAllSegmentsAsync(p, true);

        var (overAllWeekEndActualDemand, overallWeekEndRoomSold) = await OverAllSegmentsAsync(p, false);

        DemandSegmentReportDto overAllSeg = new DemandSegmentReportDto
        {
            Segment = "All Segments",
            WeekDay = new DemandSegmentDto
            {
                Label = "WeekDay",
                RoomDemanded = overAllWeekdayDemand,
                RoomSold = overAllWeekdayRoomSold,
                Deficit = -(overAllWeekdayDemand - overAllWeekdayRoomSold)
            },
            WeekEnd = new DemandSegmentDto
            {
                Label = "WeekEnd",
                RoomDemanded = overAllWeekEndActualDemand,
                RoomSold = overallWeekEndRoomSold,
                Deficit = -(overAllWeekEndActualDemand - overallWeekEndRoomSold)
            }
        };

        DemandReportDto report = new DemandReportDto
        {
            HotelDemand = SEGMENTS.list.Select(segment => GetHotelSegment(segment)).ToList(),
            MarketDemand = new List<DemandSegmentReportDto> { overAllSeg }
        };

        var list = SEGMENTS.list.Select(seg => GetMarketSegment(seg)).ToList();
        report.MarketDemand.AddRange(list);
        return report;


    }

    private async Task<(int, int)> OverAllSegmentsAsync(ReportParams p, bool weekday)
    {
        int overAllWeekEndActualDemand = await _context.RoomAllocation.Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter && x.Weekday == weekday).SumAsync(x => x.ActualDemand);
        int overallWeekEndRoomSold = await _context.SoldRoomByChannel.Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter && x.Weekday == weekday).SumAsync(x => x.SoldRoom);
        return (overAllWeekEndActualDemand, overallWeekEndRoomSold);
    }
    private DemandSegmentReportDto GetHotelSegment(string segment)
    {
        return new DemandSegmentReportDto()
        {
            Segment = segment,
            WeekDay = WeekDayHotel(segment),
            WeekEnd = WeekEndHotel(segment),
        };

    }

    private DemandSegmentReportDto GetMarketSegment(string segment)
    {
        return new DemandSegmentReportDto()
        {
            Segment = segment,
            WeekDay = WeekDayMarket(segment),
            WeekEnd = WeekEndMarket(segment),
        };

    }

    private DemandSegmentDto WeekEndHotel(string label)
    {
        var roomAllocationWeekday = _roomAllocation[label].Where(p => !p.Weekday).FirstOrDefault();
        var soldRoom = _soldRooms[label].Where(x => !x.Weekday).Sum(x => x.SoldRoom);
        var surPlus = roomAllocationWeekday.ActualDemand - soldRoom;
        return new DemandSegmentDto
        {
            Label = "WeekEnd",
            RoomAllocated = roomAllocationWeekday.RoomsAllocated,
            RoomSold = soldRoom,
            RoomDemanded = roomAllocationWeekday.ActualDemand,
            Deficit = -surPlus
        };
    }

    private DemandSegmentDto WeekDayHotel(string label)
    {
        var roomAllocationWeekday = _roomAllocation[label].Where(p => p.Weekday).FirstOrDefault();
        var soldRoom = _soldRooms[label].Where(x => x.Weekday).Sum(x => x.SoldRoom);
        var surPlus = roomAllocationWeekday.ActualDemand - soldRoom;
        return new DemandSegmentDto
        {
            Label = "WeekDay",
            RoomAllocated = roomAllocationWeekday.RoomsAllocated,
            RoomSold = soldRoom,
            RoomDemanded = roomAllocationWeekday.ActualDemand,
            Deficit = -surPlus
        };
    }

    private DemandSegmentDto WeekEndMarket(string label)
    {
        var demand = _roomAllocation[label].Where(p => !p.Weekday).Sum(x => x.ActualDemand);
        var soldRoom = _soldRooms[label].Where(x => !x.Weekday).Sum(x => x.SoldRoom);
        return new DemandSegmentDto
        {
            Label = "WeekEnd",
            RoomDemanded = demand,
            RoomSold = soldRoom,
            Deficit = -(demand - soldRoom)
        };
    }

    private DemandSegmentDto WeekDayMarket(string label)
    {
        var demand = _roomAllocation[label].Where(p => p.Weekday).Sum(x => x.ActualDemand);
        var soldRoom = _soldRooms[label].Where(x => x.Weekday).Sum(x => x.SoldRoom);
        return new DemandSegmentDto
        {
            Label = "WeekDay",
            RoomDemanded = demand,
            RoomSold = soldRoom,
            Deficit = -(demand - soldRoom)
        };
    }



}

