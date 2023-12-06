using Common;
using Common.ReportDto;
using Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using ZstdNet;

namespace Service.Reports
{
    public interface IQualityPerceptionRatingReportService
    {
        Task<QualityPreceptionReportDto> ReportAsync(ReportParams p);
    }

    public class QualityPerceptionRatingReportService : IQualityPerceptionRatingReportService
    {
        private readonly HotelDbContext _context;
        private Dictionary<string, int> _segmentWeight;
        private List<CustomerRawRating> _rawRatingList;
        private List<IdealRatingAttributeWeightConfig> _idealRatingList;
        private List<WeightedAttributeRating> _weightedRatingList;

        public QualityPerceptionRatingReportService(HotelDbContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<QualityPreceptionReportDto> ReportAsync(ReportParams p)
        {
            _segmentWeight = new Dictionary<string, int>
            {
                {SEGMENTS.BUSINESS,96 },
                {SEGMENTS.SMALL_BUSINESS,51 },
                {SEGMENTS.CORPORATE_CONTRACT,80 },
                {SEGMENTS.FAMILIES,52 },
                {SEGMENTS.AFLUENT_MATURE_TRAVELERS,103 },
                {SEGMENTS.INTERNATIONAL_LEISURE_TRAVELERS,84 },
                {SEGMENTS.CORPORATE_BUSINESS_MEETINGS,95 },
                {SEGMENTS.ASSOCIATION_MEETINGS,101 },
            };
            _rawRatingList = await _context.CustomerRawRating.Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter && x.GroupID == p.GroupId && x.Segment == p.Segment).ToListAsync();

            _idealRatingList = await (from ideal in _context.IdealRatingAttributeWeightConfig
                                      join month in _context.Months on ideal.ConfigID equals month.ConfigId
                                      where month.MonthId == p.MonthId && month.Sequence == p.CurrentQuarter && month.ClassId == p.ClassId && ideal.Segment == p.Segment
                                      select ideal).ToListAsync();
            _weightedRatingList = await _context.WeightedAttributeRating.Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter).ToListAsync();


            SegmentRating spa = GetAttributeRating(ATTRIBUTES.SPA);
            SegmentRating fitness = GetAttributeRating(ATTRIBUTES.FITNESS_CENTER);
            SegmentRating businessCenter = GetAttributeRating(ATTRIBUTES.BUSINESS_CENTER);
            SegmentRating golfCourse = GetAttributeRating(ATTRIBUTES.GOLF_COURSE);
            SegmentRating otherFacilies = GetAttributeRating(ATTRIBUTES.OTHER_FACILITES);
            SegmentRating managment = GetAttributeRating(ATTRIBUTES.MANAGEMENT);
            SegmentRating resturant = GetAttributeRating(ATTRIBUTES.RESTURANTS);
            SegmentRating bars = GetAttributeRating(ATTRIBUTES.BARS);
            SegmentRating roomServcie = GetAttributeRating(ATTRIBUTES.ROOM_SERVICS);
            SegmentRating banquet = GetAttributeRating(ATTRIBUTES.BANQUET);
            SegmentRating meetingRoom = GetAttributeRating(ATTRIBUTES.MEETING_ROOMS);
            SegmentRating entertainment = GetAttributeRating(ATTRIBUTES.ENTERTAINMENT);
            SegmentRating courtensy = GetAttributeRating(ATTRIBUTES.COURTESY_FB);
            SegmentRating guestRoom = GetAttributeRating(ATTRIBUTES.GUEST_ROOM);
            SegmentRating reservation = GetAttributeRating(ATTRIBUTES.RESERVATIONS);
            SegmentRating guestCheckInOut = GetAttributeRating(ATTRIBUTES.GUEST_CHECKINOUT);
            SegmentRating concierge = GetAttributeRating(ATTRIBUTES.CONCIERGE);
            SegmentRating houseKeeping = GetAttributeRating(ATTRIBUTES.HOUSEKEEPING);
            SegmentRating maintanence = GetAttributeRating(ATTRIBUTES.MAINTANENCE);
            SegmentRating building = GetAttributeRating(ATTRIBUTES.COURTESY);
            building.Label = ATTRIBUTES.BUILDING;

            QualityPreceptionReportDto reportDto = new QualityPreceptionReportDto();


            reportDto.Attributes = new List<SegmentRating> { spa, fitness, businessCenter, golfCourse, otherFacilies, managment, resturant, bars, roomServcie, banquet, meetingRoom, entertainment, courtensy, guestRoom, reservation, guestCheckInOut, concierge, houseKeeping, maintanence, building };

            reportDto.Segments = _segmentWeight.Select(x => GetSegmentRating(x.Key, x.Value, p)).ToList();

            reportDto.OverAll = await GetOverAllSegment(p);

            return reportDto;


        }

        private async Task<SegmentRating> GetOverAllSegment(ReportParams p)
        {
            decimal trueHotelRating = await (from weight in _context.WeightedAttributeRating
                                             join soldroom in _context.SoldRoomByChannel on new { weight.MonthID, weight.QuarterNo, weight.GroupID, weight.Segment } equals new { soldroom.MonthID, soldroom.QuarterNo, soldroom.GroupID, soldroom.Segment }
                                             where weight.MonthID == p.MonthId && weight.QuarterNo == p.CurrentQuarter && weight.GroupID == p.GroupId
                                             select new { ratingPerRoom = weight.CustomerRating * soldroom.SoldRoom }).SumAsync(x => (x.ratingPerRoom));

            decimal MaxPossibleHotelRating = await (from soldRoom in _context.SoldRoomByChannel
                                                    join segment in _context.Segment on soldRoom.Segment equals segment.SegmentName
                                                    where soldRoom.MonthID == p.MonthId && soldRoom.QuarterNo == p.CurrentQuarter && soldRoom.GroupID == p.GroupId
                                                    select new { MaxRatingPerRoom = soldRoom.SoldRoom * segment.MaxRating }
                                                   ).SumAsync(x => x.MaxRatingPerRoom);

            decimal trueHotelRating2 = await (from weight in _context.WeightedAttributeRating
                                              join soldroom in _context.SoldRoomByChannel on new { weight.MonthID, weight.QuarterNo, weight.GroupID, weight.Segment } equals new { soldroom.MonthID, soldroom.QuarterNo, soldroom.GroupID, soldroom.Segment }
                                              where weight.MonthID == p.MonthId && weight.QuarterNo == p.CurrentQuarter
                                              select new { ratingPerRoom = weight.CustomerRating * soldroom.SoldRoom }).SumAsync(x => (x.ratingPerRoom));

            decimal MaxPossibleHotelRating2 = await (from soldRoom in _context.SoldRoomByChannel
                                                     join segment in _context.Segment on soldRoom.Segment equals segment.SegmentName
                                                     where soldRoom.MonthID == p.MonthId && soldRoom.QuarterNo == p.CurrentQuarter
                                                     select new { MaxRatingPerRoom = soldRoom.SoldRoom * segment.MaxRating }
                                                   ).SumAsync(x => x.MaxRatingPerRoom);



            return new SegmentRating
            {
                Label = "OverAll",
                Hotel = MaxPossibleHotelRating == 0 ? 0 : ((trueHotelRating * 100) / MaxPossibleHotelRating),
                MarketAverage = MaxPossibleHotelRating2 == 0 ? 0 : ((trueHotelRating2 * 100) / MaxPossibleHotelRating2),
            };
        }

        private SegmentRating GetAttributeRating(string attribute)
        {
            return new SegmentRating
            {
                Label = attribute,
                Hotel = AvgIdealRating(attribute),
                MarketAverage = MktAvg(attribute)
            };
        }

        private decimal AvgIdealRating(string attribute)
        {
            var attr = _rawRatingList.FirstOrDefault(x => x.Attribute.Trim().Equals(attribute));
            var attr2 = _idealRatingList.FirstOrDefault(x => x.Attribute.Trim().Equals(attribute));
            if (attr == null || attr2 == null)
                return 0;
            decimal rawRating = attr.RawRating;

            decimal idealRating = attr2.IdealRating;
            decimal avgIdealRating = (rawRating * 10) / idealRating;
            return avgIdealRating;
        }

        private decimal MktAvg(string attribute)
        {
            var attr1 = _rawRatingList.Where(x => x.Attribute.Trim().Equals(attribute)).ToList();
            var attr2 = _idealRatingList.Where(x => x.Attribute.Trim().Equals(attribute)).ToList();
            if (attr1.Count == 0 || attr2.Count == 0)
                return 0;

            decimal rawRating = attr1.Average(x => x.RawRating);
            decimal idealRating = attr2.Average(x => x.IdealRating);
            decimal rawMktRating = (rawRating * 10) / idealRating;
            return rawMktRating;
        }

        private SegmentRating GetSegmentRating(string segment, int weight, ReportParams p)
        {
            decimal rating = _weightedRatingList.FirstOrDefault(x => x.Segment == segment && x.GroupID == p.GroupId).CustomerRating * 100 / weight;
            decimal avgRating = _weightedRatingList.Where(x => x.Segment.Equals(segment)).Average(x => x.CustomerRating) * 100 / weight;
            return new SegmentRating
            {
                Label = segment,
                Hotel = rating,
                MarketAverage = avgRating

            };
        }
    }
}


