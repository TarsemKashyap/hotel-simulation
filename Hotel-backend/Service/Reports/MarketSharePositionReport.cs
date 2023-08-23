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
    public interface IMarketSharePositionReport
    {
        Task<MarketSharePositionReportDto> ReportAsync(ReportParams p);
    }

    public class MarketSharePositionReport : AbstractReportService, IMarketSharePositionReport
    {
        private readonly HotelDbContext _context;
        private List<SoldRoomByChannel> soldRoomList;
        private List<RoomAllocation> roomAllocationList;
        private List<PriceDecision> priceDemandList;
        private decimal _groupNumber;

        public MarketSharePositionReport(HotelDbContext context)
        {
            _context = context;
        }



        public async Task<MarketSharePositionReportDto> ReportAsync(ReportParams p)
        {
            ClassGroup group = _context.ClassGroups.FirstOrDefault(x => x.Serial == p.GroupId);
            soldRoomList = await _context.SoldRoomByChannel.AsNoTracking().Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter).ToListAsync();
            roomAllocationList = await _context.RoomAllocation.AsNoTracking().Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter).ToListAsync();
            priceDemandList = await _context.PriceDecision.AsNoTracking().Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter).ToListAsync();


            decimal[] shareWith = new decimal[9];
            decimal[] shareWithout = new decimal[9];
            decimal roomSold;
            decimal roomAllocated;




            string hotelName;
            int individualAttriDemand;
            int individualPriceDemand;
            int totalIndividualDemand;
            int marketAttriDemand;
            int marketPriceDemand;
            int totalMarketDemand;
            decimal marketShare;
            decimal overallwithout = 0;
            decimal overallMarket = 0;



            int groupNumber = await _context.ClassGroups.Where(x => x.ClassId == p.ClassId).CountAsync();
            MarketSharePositionDto overAll = new MarketSharePositionDto("Overall");

            MarketSharePositionReportDto positionDto = new MarketSharePositionReportDto();
            {
                //overallShare
                decimal soldRoom = soldRoomList.Where(x => x.GroupID == p.GroupId).Sum(x => x.SoldRoom);
                decimal soldRoomQuater = soldRoomList.Sum(x => x.SoldRoom);
                overAll.MarketSharePosition = DivideSafe(soldRoom, soldRoomQuater);

            }


            // Business
            MarketSharePositionDto businessSeg = new MarketSharePositionDto(SEGMENTS.BUSINESS)
                .MarketShare(ActualMarketShare(p, SEGMENTS.BUSINESS));

            MarketSharePositionDto smallBusiness = new MarketSharePositionDto(SEGMENTS.SMALL_BUSINESS)
                .MarketShare(ActualMarketShare(p, SEGMENTS.SMALL_BUSINESS));

            MarketSharePositionDto coroporate = new MarketSharePositionDto(SEGMENTS.CORPORATE_CONTRACT)
               .MarketShare(ActualMarketShare(p, SEGMENTS.CORPORATE_CONTRACT));

            MarketSharePositionDto families = new MarketSharePositionDto(SEGMENTS.FAMILIES)
              .MarketShare(ActualMarketShare(p, SEGMENTS.FAMILIES));

            MarketSharePositionDto afluentMature = new MarketSharePositionDto(SEGMENTS.AFLUENT_MATURE_TRAVELERS)
              .MarketShare(ActualMarketShare(p, SEGMENTS.AFLUENT_MATURE_TRAVELERS));

            MarketSharePositionDto corporateMeeting = new MarketSharePositionDto(SEGMENTS.CORPORATE_BUSINESS_MEETINGS)
             .MarketShare(ActualMarketShare(p, SEGMENTS.CORPORATE_BUSINESS_MEETINGS));


            MarketSharePositionDto associationMeeting = new MarketSharePositionDto(SEGMENTS.ASSOCIATION_MEETINGS)
                 .MarketShare(ActualMarketShare(p, SEGMENTS.ASSOCIATION_MEETINGS));


            ////////////////////Business without marketing 

            return positionDto;




        }
        private decimal ActualMarketShare(ReportParams p, string segment)
        {
            var roomSold = soldRoomList.Where(x => x.GroupID == p.GroupId && x.Segment == segment).Sum(x => x.SoldRoom);
            var roomAllocated = soldRoomList.Where(x => x.Segment == segment).Sum(x => x.SoldRoom);
            return roomAllocated == 0 ? 0 : roomSold / roomAllocated;
        }


        private decimal ActualMarketPosition(ReportParams p, string segment)
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
