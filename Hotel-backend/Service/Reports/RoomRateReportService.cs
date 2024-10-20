using Common;
using Common.ReportDto;
using Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service;
public interface IRoomRateReportService
{
    Task<RoomRateReportDto> ReportAsync(ReportParams p);
}

public class RoomRateReportService : AbstractReportService, IRoomRateReportService
{
    private readonly HotelDbContext _context;
    private ILookup<string, SoldRoomByChannel> _soldRoomList;
    private ILookup<string, PriceDecision> _priceDecisionList;

    public RoomRateReportService(HotelDbContext context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public async Task<RoomRateReportDto> ReportAsync(ReportParams p)
    {
        _soldRoomList = _context.SoldRoomByChannel.Where(x => x.MonthID == p.MonthId
                                                              && x.QuarterNo == p.CurrentQuarter
                                                              && x.GroupID == p.GroupId).ToLookup(x => x.Channel, x => x);

        _priceDecisionList = _context.PriceDecision.Where(x => x.MonthID == p.MonthId
                                                               && x.QuarterNo == p.CurrentQuarter
                                                               && x.GroupID == p.GroupId).ToLookup(x => x.DistributionChannel, x => x);

        RoomRateReportDto reportDto = new RoomRateReportDto();
        reportDto.Direct = GetRoomRate(CHANNELS.DIRECT);
        reportDto.OnlineTravelAgent = GetRoomRate(CHANNELS.ONLINE_TRAVEL_AGENT);
        reportDto.Opaque = GetRoomRate(CHANNELS.OPAQUE);
        reportDto.TravelAgent = GetRoomRate(CHANNELS.TRAVEL_AGENT);

        return await Task.FromResult(reportDto);

    }

    private RoomRateAgg GetRoomRate(string channel)
    {
        var segemnts = SEGMENTS.list.Select(segment =>
        {
            var soldRoom = _soldRoomList[channel].Where(x => x.Segment == segment).ToList();
            var priceDecision = _priceDecisionList[channel].Where(x => x.Segment == segment);
            var dto = GetSegments(segment, soldRoom, priceDecision);
            return dto;

        }).ToList();

        return new RoomRateAgg(segemnts);


    }

    private static RoomRateDto GetSegments(string label, List<SoldRoomByChannel> soldRoom, IEnumerable<PriceDecision> priceDecision)
    {
        var dto = new RoomRateDto();
        dto.Label = SEGMENTS.UI_Label(label);

        var soldRoomWeeday = soldRoom.FirstOrDefault(x => x.Weekday);
        // weekday
        if (soldRoomWeeday.SoldRoom == 0)
        {
            dto.WeekdayRate = priceDecision.FirstOrDefault(x => x.Weekday).Price;
        }
        else
        {
            dto.WeekdayRate = soldRoomWeeday.Revenue / soldRoomWeeday.SoldRoom;
        }
        dto.WeekDayRoomSold = soldRoomWeeday.SoldRoom;
        dto.WeekdayCost = soldRoomWeeday.Cost;


        // weekend
        var soldRoomWeekend = soldRoom.FirstOrDefault(x => !x.Weekday);
        if (soldRoomWeekend.SoldRoom == 0)
        {
            dto.WeekendRate = priceDecision.FirstOrDefault(x => !x.Weekday).Price;
        }
        else
        {
            dto.WeekendRate = soldRoomWeekend.Revenue / soldRoomWeekend.SoldRoom;
        }
        dto.WeekendRoomSold = soldRoomWeekend.SoldRoom;
        dto.WeekendCost = soldRoomWeekend.Cost;
        return dto;
    }
}







