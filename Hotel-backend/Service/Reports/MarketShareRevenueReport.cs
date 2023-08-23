using Common;
using Common.ReportDto;
using Database;
using Database.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service.Reports
{
    public interface IMarketShareRevenueReport
    {
        Task<MarketShareReportDto> ReportAsync(ReportParams p);
    }

    public class MarketShareRevenueReport : AbstractReportService, IMarketShareRevenueReport
    {
        private readonly HotelDbContext _context;
        private List<SoldRoomByChannel> soldRoomList;
        private List<RoomAllocation> roomAllocationList;
        private decimal _groupNumber;

        public MarketShareRevenueReport(HotelDbContext context)
        {
            _context = context;
        }



        public async Task<MarketShareReportDto> ReportAsync(ReportParams p)
        {
            ClassGroup group = _context.ClassGroups.FirstOrDefault(x => x.Serial == p.GroupId);
            soldRoomList = await _context.SoldRoomByChannel.AsNoTracking().Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter).ToListAsync();
            roomAllocationList = await _context.RoomAllocation.AsNoTracking().Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter).ToListAsync();

            decimal occupancy;
            decimal roomSold;
            decimal roomAllocated;
            decimal occuIndex;

            decimal overallOccu;
            decimal overallOccuMarket;
            decimal weekdayOccu;
            decimal weekdayOccuMarket;
            decimal weekendOccu;
            decimal weekendOccuMarket;

            decimal businessWeekday;
            decimal businessWeekdayMarket;
            decimal businessWeekend;
            decimal businessWeekendMarket;
            decimal businessOverall;
            decimal businessOverallMarket;

            decimal smallBusinessWeekday;
            decimal smallBusinessWeekdayMarket;
            decimal smallBusinessWeekend;
            decimal smallBusinessWeekendMarket;
            decimal smallBusinessOverall;
            decimal smallBusinessOverallMarket;

            decimal corpContraWeekday;
            decimal corpContraWeekdayMarket;
            decimal corpContraWeekend;
            decimal corpContraWeekendMarket;
            decimal corpContraOverall;
            decimal corpContraOverallMarket;

            decimal familiesWeekday;
            decimal familiesWeekdayMarket;
            decimal familiesWeekend;
            decimal familiesWeekendMarket;
            decimal familiesOverall;
            decimal familiesOverallMarket;

            decimal afluWeekday;
            decimal afluWeekdayMarket;
            decimal afluWeekend;
            decimal afluWeekendMarket;
            decimal afluOverall;
            decimal afluOverallMarket;

            decimal interWeekday;
            decimal interWeekdayMarket;
            decimal interWeekend;
            decimal interWeekendMarket;
            decimal interOverall;
            decimal interOverallMarket;

            decimal corpMeetingWeekday;
            decimal corpMeetingWeekdayMarket;
            decimal corpMeetingWeekend;
            decimal corpMeetingWeekendMarket;
            decimal corpMeetingOverall;
            decimal corpMeetingOverallMarket;

            decimal assoMeetingWeekday;
            decimal assoMeetingWeekdayMarket;
            decimal assoMeetingWeekend;
            decimal assoMeetingWeekendMarket;
            decimal assoMeetingOverall;
            decimal assoMeetingOverallMarket;



            _groupNumber = await _context.ClassGroups.Where(x => x.ClassId == p.ClassId).CountAsync();


            decimal averageShare;
            if (_groupNumber == 0)
            {
                averageShare = 1;
            }
            else
            {
                averageShare = 1 / Convert.ToDecimal(_groupNumber);
            }

            //overallShare
            roomSold = soldRoomList.Where(x => x.GroupID == p.GroupId).Sum(x => x.Revenue);
            roomAllocated = soldRoomList.Sum(x => x.Revenue);
            if (roomAllocated == 0)
            {
                overallOccu = 0;
            }
            else
            {
                overallOccu = roomSold / roomAllocated;
            }
            overallOccuMarket = averageShare;


            //Market Overall Occupancy

            overallOccuMarket = averageShare;


            //Weekday Revenue Share
            roomSold = soldRoomList.Where(x => x.GroupID == p.GroupId && x.Weekday).Sum(x => x.Revenue);
            roomAllocated = soldRoomList.Where(x => x.Weekday).Sum(x => x.Revenue);
            if (roomAllocated == 0)
            {
                weekdayOccu = 0;
            }
            else
            {
                weekdayOccu = roomSold / roomAllocated;

            }
            //Weekday Revenue Market

            weekdayOccuMarket = averageShare;

            //Weekend Revenue Share
            roomSold = soldRoomList.Where(x => x.GroupID == p.GroupId && !x.Weekday).Sum(x => x.Revenue);
            roomAllocated = roomAllocationList.Where(x => !x.Weekday).Sum(x => x.Revenue);
            if (roomAllocated == 0)
            {
                weekendOccu = 1;
            }
            else
            {
                weekendOccu = roomSold / roomAllocated;
            }
            //Weekend Revenue Share Market
            weekendOccuMarket = averageShare;



            //Business Weekday
            businessWeekday = WeekDay(p, SEGMENTS.BUSINESS);
            //Business Weekday Market
            businessWeekdayMarket = averageShare;

            //Business Weekend 
            businessWeekend = Weekend(p, SEGMENTS.BUSINESS);
            //Business Weekend Market
            businessWeekendMarket = averageShare;

            //Business Overall
            businessOverall = Overall(p, SEGMENTS.BUSINESS);

            //Business Overall Market
            businessOverallMarket = averageShare;

            Common.ReportDto.Segment business = Common.ReportDto.Segment.Create(SEGMENTS.BUSINESS)
                                    .WeekDay(businessWeekday, businessWeekdayMarket)
                                    .WeekEnd(businessWeekend, businessWeekendMarket)
                                    .Overall(businessOverall, businessOverallMarket);


            //Small Business Weekday
            smallBusinessWeekday = WeekDay(p, SEGMENTS.SMALL_BUSINESS);

            //small Business Weekday Market
            smallBusinessWeekdayMarket = averageShare;

            //Small Business Weekend
            smallBusinessWeekend = Weekend(p, SEGMENTS.SMALL_BUSINESS);

            //Small Business Weekend Market
            smallBusinessWeekendMarket = averageShare;

            //Small Business Overall
            smallBusinessOverall = Overall(p, SEGMENTS.SMALL_BUSINESS);

            //Small Business Overall Market
            smallBusinessOverallMarket = averageShare;

            Common.ReportDto.Segment smallBusiness = Common.ReportDto.Segment.Create(SEGMENTS.SMALL_BUSINESS)
                                  .WeekDay(smallBusinessWeekday, smallBusinessWeekdayMarket)
                                  .WeekEnd(smallBusinessWeekend, smallBusinessWeekendMarket)
                                  .Overall(smallBusinessOverall, smallBusinessOverallMarket);

            //----------------------
            //Corporate contract Weekday
            corpContraWeekday = WeekDay(p, SEGMENTS.CORPORATE_CONTRACT);

            //Corporate contract Weekday Market
            corpContraWeekdayMarket = averageShare;

            //Corporate contract Weekend
            corpContraWeekend = Weekend(p, SEGMENTS.CORPORATE_CONTRACT);

            //Corporate contract Weekend Market
            corpContraWeekendMarket = averageShare;

            //Corporate contract Overall
            corpContraOverall = Overall(p, SEGMENTS.CORPORATE_CONTRACT);

            //Corporate contract Overall Market
            corpContraOverallMarket = averageShare;

            Common.ReportDto.Segment cropContract = Common.ReportDto.Segment.Create(SEGMENTS.CORPORATE_CONTRACT)
                                .WeekDay(corpContraWeekday, corpContraWeekdayMarket)
                                .WeekEnd(corpContraWeekend, corpContraWeekendMarket)
                                .Overall(corpContraOverall, corpContraOverallMarket);

            //----------------------
            //Families Weekday
            familiesWeekday = WeekDay(p, SEGMENTS.FAMILIES);

            //Families Weekday Market
            familiesWeekdayMarket = averageShare;

            //Families Weekend
            familiesWeekend = Weekend(p, SEGMENTS.FAMILIES);

            //Families Weekend Market
            familiesWeekendMarket = averageShare;

            //Families Overall
            familiesOverall = Overall(p, SEGMENTS.FAMILIES);

            //Families Overall Market
            familiesOverallMarket = averageShare;

            Common.ReportDto.Segment families = Common.ReportDto.Segment.Create(SEGMENTS.FAMILIES)
                              .WeekDay(familiesWeekday, familiesWeekdayMarket)
                              .WeekEnd(familiesWeekend, familiesWeekendMarket)
                              .Overall(familiesOverall, familiesOverallMarket);
            //----------------------
            //Afluent Mature Travelers Weekday
            afluWeekday = WeekDay(p, SEGMENTS.AFLUENT_MATURE_TRAVELERS);

            //Afluent Mature Travelers Weekday Market
            afluWeekdayMarket = averageShare;

            //Afluent Mature Travelers Weekend
            afluWeekend = Weekend(p, SEGMENTS.AFLUENT_MATURE_TRAVELERS);

            //Afluent Mature Travelers Weekend Market
            afluWeekendMarket = averageShare;

            //Afluent Mature Travelers Overall
            afluOverall = Overall(p, SEGMENTS.AFLUENT_MATURE_TRAVELERS);

            //Afluent Mature Travelers Overall Market
            afluOverallMarket = averageShare;

            Common.ReportDto.Segment afluentMatureTravlers = Common.ReportDto.Segment.Create(SEGMENTS.AFLUENT_MATURE_TRAVELERS)
                              .WeekDay(afluWeekday, afluWeekdayMarket)
                              .WeekEnd(afluWeekend, afluWeekendMarket)
                              .Overall(afluOverall, afluOverallMarket);

            //----------------------
            //International leisure travelers Weekday
            interWeekday = WeekDay(p, SEGMENTS.INTERNATIONAL_LEISURE_TRAVELERS);

            //International leisure travelers Weekday Market
            interWeekdayMarket = averageShare;

            //International leisure travelers Weekend
            interWeekend = Weekend(p, SEGMENTS.INTERNATIONAL_LEISURE_TRAVELERS);

            //International leisure travelers Weekend Market
            interWeekendMarket = averageShare;

            //International leisure travelers Overall
            interOverall = Overall(p, SEGMENTS.INTERNATIONAL_LEISURE_TRAVELERS);

            //International leisure travelers Overall Market
            interOverallMarket = averageShare;

            Common.ReportDto.Segment interLeisureTravel = Common.ReportDto.Segment.Create(SEGMENTS.INTERNATIONAL_LEISURE_TRAVELERS)
                              .WeekDay(interWeekday, interWeekdayMarket)
                              .WeekEnd(interWeekend, interWeekendMarket)
                              .Overall(interOverall, interOverallMarket);

            //===  Corporate/Business Meetings
            corpMeetingWeekday = WeekDay(p, SEGMENTS.CORPORATE_BUSINESS_MEETINGS);
            corpMeetingWeekdayMarket = averageShare;

            corpMeetingWeekend = Weekend(p, SEGMENTS.CORPORATE_BUSINESS_MEETINGS);
            corpMeetingWeekendMarket = averageShare;

            corpMeetingOverall = Overall(p, SEGMENTS.CORPORATE_BUSINESS_MEETINGS);
            corpMeetingOverallMarket = averageShare;

            Common.ReportDto.Segment corporate = Common.ReportDto.Segment.Create(SEGMENTS.CORPORATE_BUSINESS_MEETINGS)
                              .WeekDay(corpMeetingWeekday, corpMeetingWeekdayMarket)
                              .WeekEnd(corpMeetingWeekend, corpMeetingWeekendMarket)
                              .Overall(corpMeetingOverall, corpMeetingOverallMarket);

            //===  Association Meetings
            assoMeetingWeekday = WeekDay(p, SEGMENTS.ASSOCIATION_MEETINGS);
            assoMeetingWeekdayMarket = averageShare;
            assoMeetingWeekend = Weekend(p, SEGMENTS.ASSOCIATION_MEETINGS);
            assoMeetingWeekendMarket = averageShare;
            assoMeetingOverall = Overall(p, SEGMENTS.ASSOCIATION_MEETINGS);
            assoMeetingOverallMarket = averageShare;

            Common.ReportDto.Segment assocateMeeting = Common.ReportDto.Segment.Create(SEGMENTS.ASSOCIATION_MEETINGS)
                             .WeekDay(assoMeetingWeekday, assoMeetingWeekdayMarket)
                             .WeekEnd(assoMeetingWeekend, assoMeetingWeekendMarket)
                             .Overall(assoMeetingOverall, assoMeetingOverallMarket);

            MarketShareReportDto reportDto = new MarketShareReportDto();
            reportDto.AddSegment(business);
            reportDto.AddSegment(smallBusiness);
            reportDto.AddSegment(cropContract);
            reportDto.AddSegment(families);
            reportDto.AddSegment(afluentMatureTravlers);
            reportDto.AddSegment(interLeisureTravel);
            reportDto.AddSegment(corporate);
            reportDto.AddSegment(assocateMeeting);

            reportDto.AddOverAll("Overall Market share based on Revenue", overallOccu, overallOccuMarket);
            reportDto.AddOverAll("Weekday Market share based on Revenue", weekdayOccu, weekdayOccuMarket);
            reportDto.AddOverAll("Weekend Market share based on Revenue", weekendOccu, weekendOccuMarket);

            return reportDto;




        }
        private decimal WeekDay(ReportParams p, string segment)
        {
            var roomSold = soldRoomList.Where(x => x.GroupID == p.GroupId && x.Segment == segment && x.Weekday).Sum(x => x.Revenue);
            var roomAllocated = soldRoomList.Where(x => x.GroupID == p.GroupId && x.Segment == segment && x.Weekday).Sum(x => x.Revenue);
            return roomAllocated == 0 ? 0 : roomSold / roomAllocated;
        }


        private decimal Weekend(ReportParams p, string segment)
        {
            var roomSold = soldRoomList.Where(x => x.GroupID == p.GroupId && x.Segment == segment && !x.Weekday).Sum(x => x.Revenue);
            var roomAllocated = soldRoomList.Where(x => x.GroupID == p.GroupId && x.Segment == segment && !x.Weekday).Sum(x => x.Revenue);
            return roomAllocated == 0 ? 0 : roomSold / roomAllocated;

        }


        private decimal Overall(ReportParams p, string segment)
        {
            var roomSold = soldRoomList.Where(x => x.GroupID == p.GroupId && x.Segment == segment).Sum(x => x.Revenue);
            var roomAllocated = soldRoomList.Where(x => x.Segment == segment).Sum(x => x.Revenue);
            return roomAllocated == 0 ? 0 : (roomSold / roomAllocated);

        }



    }


}
