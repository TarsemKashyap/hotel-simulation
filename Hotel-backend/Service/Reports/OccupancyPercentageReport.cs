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
    public interface IOccupancyPercentageReport
    {
        Task<OccupancyReportDto> Report(ReportParams p);
    }

    public class OccupancyPercentageReport : AbstractReportService, IOccupancyPercentageReport
    {
        private const string SEG_BUSINESS = "Business";
        private const String SEG_SMALL_BUSINESS = "Small Business";
        private const string SEG_CORPORATE_CONTRACT = "Corporate contract";
        private const string SEG_FAMILIES = "Families";
        private const string SEG_AFLUENT_MATURE_TRAVILER = "Afluent Mature Travelers";
        private const string SEG_INTER_LEISURE_TRAVEL = "International leisure travelers";
        private const string SEG_CORPORATE_BUSINESS = "Corporate/Business Meetings";
        private const string SEG_ASSOCIATE_MEETINGS = "Association Meetings";
        private readonly HotelDbContext _context;
        private List<SoldRoomByChannel> soldRoomList;
        private List<RoomAllocation> roomAllocationList;
        private decimal _groupNumber;

        public OccupancyPercentageReport(HotelDbContext context)
        {
            _context = context;
        }



        public async Task<OccupancyReportDto> Report(ReportParams p)
        {
            ClassGroup group = _context.ClassGroups.FirstOrDefault(x => x.Serial == p.GroupId);
            soldRoomList = await _context.SoldRoomByChannel.AsNoTracking().Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter).ToListAsync();
            roomAllocationList = await _context.RoomAllocation.AsNoTracking().Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter).ToListAsync();
            double occupancy;
            double roomSold;
            double roomAllocated;

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
            //overallOccu
            roomSold = soldRoomList.Where(x => x.GroupID == p.GroupId).Sum(x => x.SoldRoom);

            roomAllocated = roomAllocationList.Where(x => x.GroupID == p.GroupId).Sum(x => x.RoomsAllocated);
            if (roomAllocated == 0)
            {
                overallOccu = 0;
            }
            else
            {
                overallOccu = Convert.ToDecimal(roomSold) / 500 / 30;
            }

            //Market Overall Occupancy
            roomSold = soldRoomList.Sum(x => x.SoldRoom);
            roomAllocated = roomAllocationList.Sum(x => x.RoomsAllocated);
            if (roomAllocated == 0)
            {
                overallOccuMarket = 1;
            }
            else
            {
                overallOccuMarket = Convert.ToDecimal(roomSold) / 500 / 30 / _groupNumber;
            }

            //Weekday Occupany
            roomSold = soldRoomList.Where(x => x.GroupID == p.GroupId && x.Weekday).Sum(x => x.SoldRoom);
            roomAllocated = roomAllocationList.Where(x => x.GroupID == p.GroupId && x.Weekday).Sum(x => x.RoomsAllocated);
            if (roomAllocated == 0)
            {
                weekdayOccu = 0;
            }
            else
            {
                weekdayOccu = Convert.ToDecimal(roomSold) / 500 / 17;

            }
            //Weekday Occupancy Market
            roomSold = soldRoomList.Where(x => x.Weekday).Sum(x => x.SoldRoom);
            roomAllocated = roomAllocationList.Where(x => x.Weekday).Sum(x => x.RoomsAllocated);
            if (roomAllocated == 0)
            {
                weekdayOccuMarket = 1;
            }
            else
            {
                weekdayOccuMarket = Convert.ToDecimal(roomSold) / 500 / 17 / _groupNumber;
            }

            //Weekend Occupancy 
            roomSold = soldRoomList.Where(x => x.GroupID == p.GroupId && !x.Weekday).Sum(x => x.SoldRoom);
            roomAllocated = roomAllocationList.Where(x => x.GroupID == p.GroupId && !x.Weekday).Sum(x => x.RoomsAllocated);
            if (roomAllocated == 0)
            {
                weekendOccu = 0;
            }
            else
            {
                weekendOccu = Convert.ToDecimal(roomSold) / 500 / 13;
            }


            //Weekend Occupancy Market
            roomSold = soldRoomList.Where(x => x.GroupID == p.GroupId && !x.Weekday).Sum(x => x.SoldRoom);
            roomAllocated = roomAllocationList.Where(x => x.GroupID == p.GroupId && !x.Weekday).Sum(x => x.RoomsAllocated);
            if (roomAllocated == 0)
            {
                weekendOccuMarket = 1;
            }
            else
            {
                weekendOccuMarket = Convert.ToDecimal(roomSold) / 500 / 13 / _groupNumber;
            }



            //Business Weekday
            businessWeekday = WeekDay(p, SEG_BUSINESS);
            //Business Weekday Market
            businessWeekdayMarket = WeekDayMarket(SEG_BUSINESS);
            //Business Weekend
            businessWeekend = Weekend(p, SEG_BUSINESS);
            //Business Weekend Market
            businessWeekendMarket = WeekendMarket(SEG_BUSINESS);



            //Business Overall
            businessOverall = Overall(p, SEG_BUSINESS);

            //Business Overall Market
            businessOverallMarket = OverallMarket(SEG_BUSINESS);
            OccupancyBySegement business = OccupancyBySegement.Create(SEG_BUSINESS)
                                    .WeekDay(businessWeekday, businessWeekdayMarket)
                                    .WeekEnd(businessWeekend, businessWeekendMarket)
                                    .Overall(businessOverall, businessOverallMarket);


            //Small Business Weekday
            smallBusinessWeekday = WeekDay(p, SEG_SMALL_BUSINESS);

            //small Business Weekday Market
            smallBusinessWeekdayMarket = WeekDayMarket(SEG_SMALL_BUSINESS);

            //Small Business Weekend
            smallBusinessWeekend = Weekend(p, SEG_SMALL_BUSINESS);

            //Small Business Weekend Market
            smallBusinessWeekendMarket = WeekendMarket(SEG_SMALL_BUSINESS);

            //Small Business Overall
            smallBusinessOverall = Overall(p, SEG_SMALL_BUSINESS);

            //Small Business Overall Market
            smallBusinessOverallMarket = OverallMarket(SEG_SMALL_BUSINESS);

            OccupancyBySegement smallBusiness = OccupancyBySegement.Create(SEG_SMALL_BUSINESS)
                                  .WeekDay(smallBusinessWeekday, smallBusinessWeekdayMarket)
                                  .WeekEnd(smallBusinessWeekend, smallBusinessWeekendMarket)
                                  .Overall(smallBusinessOverall, smallBusinessOverallMarket);

            //----------------------
            //Corporate contract Weekday
            corpContraWeekday = WeekDay(p, SEG_CORPORATE_CONTRACT);

            //Corporate contract Weekday Market
            corpContraWeekdayMarket = WeekDayMarket(SEG_CORPORATE_CONTRACT);

            //Corporate contract Weekend
            corpContraWeekend = Weekend(p, SEG_CORPORATE_CONTRACT);

            //Corporate contract Weekend Market
            corpContraWeekendMarket = WeekendMarket(SEG_CORPORATE_CONTRACT);

            //Corporate contract Overall
            corpContraOverall = Overall(p, SEG_CORPORATE_CONTRACT);

            //Corporate contract Overall Market
            corpContraOverallMarket = OverallMarket(SEG_CORPORATE_CONTRACT);

            OccupancyBySegement cropContract = OccupancyBySegement.Create(SEG_CORPORATE_CONTRACT)
                                .WeekDay(corpContraWeekday, corpContraWeekdayMarket)
                                .WeekEnd(corpContraWeekend, corpContraWeekendMarket)
                                .Overall(corpContraOverall, corpContraOverallMarket);

            //----------------------
            //Families Weekday
            familiesWeekday = WeekDay(p, SEG_FAMILIES);

            //Families Weekday Market
            familiesWeekdayMarket = WeekDayMarket(SEG_FAMILIES);

            //Families Weekend
            familiesWeekend = Weekend(p, SEG_FAMILIES);

            //Families Weekend Market
            familiesWeekendMarket = WeekendMarket(SEG_FAMILIES);

            //Families Overall
            familiesOverall = Overall(p, SEG_FAMILIES);

            //Families Overall Market
            familiesOverallMarket = OverallMarket(SEG_FAMILIES);

            OccupancyBySegement families = OccupancyBySegement.Create(SEG_FAMILIES)
                              .WeekDay(familiesWeekday, familiesWeekdayMarket)
                              .WeekEnd(familiesWeekend, familiesWeekendMarket)
                              .Overall(familiesOverall, familiesOverallMarket);
            //----------------------
            //Afluent Mature Travelers Weekday
            afluWeekday = WeekDay(p, SEG_AFLUENT_MATURE_TRAVILER);

            //Afluent Mature Travelers Weekday Market
            afluWeekdayMarket = WeekDayMarket(SEG_AFLUENT_MATURE_TRAVILER);

            //Afluent Mature Travelers Weekend
            afluWeekend = Weekend(p, SEG_AFLUENT_MATURE_TRAVILER);

            //Afluent Mature Travelers Weekend Market
            afluWeekendMarket = WeekendMarket(SEG_AFLUENT_MATURE_TRAVILER);

            //Afluent Mature Travelers Overall
            afluOverall = Overall(p, SEG_AFLUENT_MATURE_TRAVILER);

            //Afluent Mature Travelers Overall Market
            afluOverallMarket = OverallMarket(SEG_AFLUENT_MATURE_TRAVILER);

            OccupancyBySegement afluentMatureTravlers = OccupancyBySegement.Create(SEG_AFLUENT_MATURE_TRAVILER)
                              .WeekDay(afluWeekday, afluWeekdayMarket)
                              .WeekEnd(afluWeekend, afluWeekendMarket)
                              .Overall(afluOverall, afluOverallMarket);

            //----------------------
            //International leisure travelers Weekday
            interWeekday = WeekDay(p, SEG_INTER_LEISURE_TRAVEL);

            //International leisure travelers Weekday Market
            interWeekdayMarket = WeekDayMarket(SEG_INTER_LEISURE_TRAVEL);

            //International leisure travelers Weekend
            interWeekend = Weekend(p, SEG_INTER_LEISURE_TRAVEL);

            //International leisure travelers Weekend Market
            interWeekendMarket = WeekendMarket(SEG_INTER_LEISURE_TRAVEL);

            //International leisure travelers Overall
            interOverall = Overall(p, SEG_INTER_LEISURE_TRAVEL);

            //International leisure travelers Overall Market
            interOverallMarket = OverallMarket(SEG_INTER_LEISURE_TRAVEL);

            OccupancyBySegement interLeisureTravel = OccupancyBySegement.Create(SEG_INTER_LEISURE_TRAVEL)
                              .WeekDay(interWeekday, interWeekdayMarket)
                              .WeekEnd(interWeekend, interWeekendMarket)
                              .Overall(interOverall, interOverallMarket);

            //===  Corporate/Business Meetings
            corpMeetingWeekday = WeekDay(p, SEG_CORPORATE_BUSINESS);
            corpMeetingWeekdayMarket = WeekDayMarket(SEG_CORPORATE_BUSINESS);

            corpMeetingWeekend = Weekend(p, SEG_CORPORATE_BUSINESS);
            corpMeetingWeekendMarket = WeekendMarket(SEG_CORPORATE_BUSINESS);

            corpMeetingOverall = Overall(p, SEG_CORPORATE_BUSINESS);
            corpMeetingOverallMarket = OverallMarket(SEG_CORPORATE_BUSINESS);

            OccupancyBySegement corporate = OccupancyBySegement.Create(SEG_CORPORATE_BUSINESS)
                              .WeekDay(corpMeetingWeekday, corpMeetingWeekdayMarket)
                              .WeekEnd(corpMeetingWeekend, corpMeetingWeekendMarket)
                              .Overall(corpMeetingOverall, corpMeetingOverallMarket);

            //===  Association Meetings
            assoMeetingWeekday = WeekDay(p, SEG_ASSOCIATE_MEETINGS);
            assoMeetingWeekdayMarket = WeekDayMarket(SEG_ASSOCIATE_MEETINGS);
            assoMeetingWeekend = Weekend(p, SEG_ASSOCIATE_MEETINGS);
            assoMeetingWeekendMarket = WeekendMarket(SEG_ASSOCIATE_MEETINGS);
            assoMeetingOverall = Overall(p, SEG_ASSOCIATE_MEETINGS);
            assoMeetingOverallMarket = OverallMarket(SEG_ASSOCIATE_MEETINGS);

            OccupancyBySegement assocateMeeting = OccupancyBySegement.Create(SEG_ASSOCIATE_MEETINGS)
                             .WeekDay(assoMeetingWeekday, assoMeetingWeekdayMarket)
                             .WeekEnd(assoMeetingWeekend, assoMeetingWeekendMarket)
                             .Overall(assoMeetingOverall, assoMeetingOverallMarket);

            OccupancyReportDto reportDto = new OccupancyReportDto();
            reportDto.AddSegment(business);
            reportDto.AddSegment(smallBusiness);
            reportDto.AddSegment(cropContract);
            reportDto.AddSegment(families);
            reportDto.AddSegment(afluentMatureTravlers);
            reportDto.AddSegment(interLeisureTravel);
            reportDto.AddSegment(corporate);
            reportDto.AddSegment(assocateMeeting);

            reportDto.AddOverAll("Overall Occupancy Percentage", overallOccu, overallOccuMarket);
            reportDto.AddOverAll("Weekday Occupancy Percentage", weekdayOccu, weekdayOccuMarket);
            reportDto.AddOverAll("Weekend Occupancy Percentage", weekendOccu, weekendOccuMarket);

            return reportDto;




        }
        private decimal WeekDay(ReportParams p, string segment)
        {
            var roomSold = soldRoomList.Where(x => x.GroupID == p.GroupId && x.Segment == segment && x.Weekday).Sum(x => x.SoldRoom);
            var roomAllocated = roomAllocationList.Where(x => x.GroupID == p.GroupId && x.Segment == segment && x.Weekday).Sum(x => x.RoomsAllocated);
            return roomAllocated == 0 ? 0 : (Convert.ToDecimal(roomSold) / 500 / 17);
        }
        private decimal WeekDayMarket(string segment)
        {
            var roomSold = soldRoomList.Where(x => x.Segment == segment && x.Weekday).Sum(x => x.SoldRoom);
            var roomAllocated = roomAllocationList.Where(x => x.Segment == segment && x.Weekday).Sum(x => x.RoomsAllocated);
            return roomAllocated == 0 ? 1 : Convert.ToDecimal(roomSold) / 500 / 17 / _groupNumber;
        }

        private decimal Weekend(ReportParams p, string segment)
        {
            var roomSold = soldRoomList.Where(x => x.GroupID == p.GroupId && x.Segment == segment & !x.Weekday).Sum(x => x.SoldRoom);
            var roomAllocated = roomAllocationList.Where(x => x.GroupID == p.GroupId && !x.Weekday && x.Segment == segment).Sum(x => x.RoomsAllocated);
            return roomAllocated == 0 ? 0 : Convert.ToDecimal(roomSold) / 500 / 13;

        }

        private decimal WeekendMarket(string segment)
        {
            var roomSold = soldRoomList.Where(x => x.Segment == segment & !x.Weekday).Sum(x => x.SoldRoom);
            var roomAllocated = roomAllocationList.Where(x => x.Segment == segment && !x.Weekday).Sum(x => x.RoomsAllocated);
            return roomAllocated == 0 ? 1 : Convert.ToDecimal(roomSold) / 500 / 13 / _groupNumber;
        }

        private decimal Overall(ReportParams p, string segment)
        {
            var roomSold = soldRoomList.Where(x => x.GroupID == p.GroupId && x.Segment == segment).Sum(x => x.SoldRoom);
            var roomAllocated = roomAllocationList.Where(x => x.GroupID == p.GroupId && x.Segment == segment).Sum(x => x.RoomsAllocated);
            return roomAllocated == 0 ? 0 : Convert.ToDecimal(roomSold) / 500 / 30;

        }

        private decimal OverallMarket(string segment)
        {
            var roomSold = soldRoomList.Where(x => x.Segment == segment).Sum(x => x.SoldRoom);
            var roomAllocated = roomAllocationList.Where(x => x.Segment == segment).Sum(x => x.RoomsAllocated);
            return roomAllocated == 0 ? 1 : Convert.ToDecimal(roomSold) / 500 / 30 / _groupNumber;

        }

    }


}
