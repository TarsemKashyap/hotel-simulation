using Common;
using Common.ReportDto;
using Database;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service;

public interface IPositionMapReportService
{
    Task<PositionMapReportDto> ReportAsync(ReportParams p);
}

public class PositionMapReportService : AbstractReportService, IPositionMapReportService
{
    private readonly HotelDbContext _context;
    private Dictionary<string, int> _segmentValue = new Dictionary<string, int>()
    {
        { SEGMENTS.BUSINESS,96 },
        { SEGMENTS.SMALL_BUSINESS,51 },
        { SEGMENTS.CORPORATE_CONTRACT,80 },
        { SEGMENTS.FAMILIES,52 },
        { SEGMENTS.AFLUENT_MATURE_TRAVELERS,103 },
        { SEGMENTS.INTERNATIONAL_LEISURE_TRAVELERS,84 },
        { SEGMENTS.CORPORATE_BUSINESS_MEETINGS,95 },
        { SEGMENTS.ASSOCIATION_MEETINGS,101 }
    };
    public PositionMapReportService(HotelDbContext context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    public async Task<PositionMapReportDto> ReportAsync(ReportParams p)
    {
        try
        {



            var groups = (await _context.ClassGroups.Where(x => x.ClassId == p.ClassId).ToListAsync()).Adapt<List<ClassGroupDto>>();

            if (string.IsNullOrEmpty(p.Segment))
            {
                string overAllSegment = "OverAll";

                //weightedRatingAdpt.ScalarHotelOverallRatingBasedOnSoldRoom
                var _overAllRating = (from weight in _context.WeightedAttributeRating
                                      join soldRoom in _context.SoldRoomByChannel on new
                                      {
                                          weight.MonthID,
                                          weight.QuarterNo,
                                          weight.GroupID,
                                          weight.Segment
                                      }
                                      equals new
                                      {
                                          soldRoom.MonthID,
                                          soldRoom.QuarterNo,
                                          soldRoom.GroupID,
                                          soldRoom.Segment
                                      }
                                      where weight.MonthID == p.MonthId && weight.QuarterNo == p.CurrentQuarter
                                      select new { weight.GroupID, Rating = weight.CustomerRating * soldRoom.SoldRoom }
                                )
                                .AsEnumerable()
                                .GroupBy(x => x.GroupID)
                                .ToDictionary(x => x.Key, x => x.Sum(p => p.Rating));


                //weightedRatingAdpt.ScalarHotelMaxiumRatingBasedOnSoldRoom
                var _maxRating = (
                        from soldRoom in _context.SoldRoomByChannel
                        join segment in _context.Segment on soldRoom.Segment equals segment.SegmentName
                        where soldRoom.MonthID == p.MonthId && soldRoom.QuarterNo == p.CurrentQuarter
                        select new { soldRoom.GroupID, Rating = (soldRoom.SoldRoom * segment.MaxRating) }
                    )
                    .AsEnumerable()
                    .GroupBy(x => x.GroupID)
                    .ToDictionary(x => x.Key, x => x.Sum(y => y.Rating));

                //roomChanelAdpt.ScalarGroupRoomRevenueByMonth
                var roomRevenueByGroup = _context.SoldRoomByChannel
                .Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter)
                .AsEnumerable()
                .GroupBy(x => x.GroupID)
                .ToDictionary(x => x.Key, x => x.Sum(p => p.Revenue));



                //roomChanelAdpt.ScalarQuerySoldRoomByMonth
                var soldRoomByGroup = _context.SoldRoomByChannel
                    .Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter)
                     .AsEnumerable()
                     .GroupBy(x => x.GroupID)
                .ToDictionary(x => x.Key, x => x.Sum(p => p.SoldRoom));

                var reportDto = new PositionMapReportDto() { Segment = overAllSegment };
                reportDto.GroupRating = groups.Select(g =>
                {
                    _overAllRating.TryGetValue(g.Serial, out var trueHotelRating);
                    _maxRating.TryGetValue(g.Serial, out var maxPossibleHotelRating);

                    soldRoomByGroup.TryGetValue(g.Serial, out var roomSold);
                    roomRevenueByGroup.TryGetValue(g.Serial, out var roomRevenue);


                    return new PositionMapDto
                    {
                        ClassGroup = g.Name,
                        QualityRating = DivideSafe(trueHotelRating * 100, maxPossibleHotelRating),
                        RoomRate = DivideSafe(roomRevenue, roomSold)
                    };

                }).ToList();
                return reportDto;


            }
            else
            {
                var _weightAttributeRating = _context.WeightedAttributeRating.Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter && x.Segment == p.Segment).ToDictionary(x => x.GroupID, x => x.CustomerRating);




                //ScalarGroupRomRevenByMonthBySegm 

                var soldRoomList = _context.SoldRoomByChannel
                    .Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter && x.Segment == p.Segment)
                    .Select(x => new { x.GroupID, x.Revenue, x.SoldRoom })
                    .ToLookup(x => x.GroupID);

                var reportDto = new PositionMapReportDto() { Segment = p.Segment };
                reportDto.GroupRating = groups.Select(g =>
                {
                    var customerRating = _weightAttributeRating[g.Serial];
                    decimal roomRevenue = soldRoomList[g.Serial].Sum(x => x.Revenue);
                    decimal soldRoom = soldRoomList[g.Serial].Sum(x => x.SoldRoom);
                    return new PositionMapDto
                    {
                        ClassGroup = g.Name,
                        QualityRating = customerRating * 100 / _segmentValue[p.Segment],
                        RoomRate = DivideSafe(roomRevenue, soldRoom),
                    };

                }).ToList();
                return reportDto;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }




    }

}