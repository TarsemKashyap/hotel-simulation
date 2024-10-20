using Common;
using Common.ReportDto;
using Database;
using Database.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
        private List<RoomAllocation> _roomAllocationList;
        private List<WeightedAttributeRating> _weightedList;
        private List<PriceDecision> _priceDecisionList;
        private decimal _groupNumber;
        decimal _overallwithout = 0;
        decimal _overallMarket = 0;
        public MarketSharePositionReport(HotelDbContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }



        public async Task<MarketSharePositionReportDto> ReportAsync(ReportParams p)
        {
            ClassGroup group = _context.ClassGroups.FirstOrDefault(x => x.Serial == p.GroupId);
            soldRoomList = await _context.SoldRoomByChannel.AsNoTracking().Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter).ToListAsync();
            _roomAllocationList = await _context.RoomAllocation.AsNoTracking().Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter).ToListAsync();

            _weightedList = await _context.WeightedAttributeRating.Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter).ToListAsync();
            _priceDecisionList = await _context.PriceDecision.Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter).ToListAsync();

            _groupNumber = await _context.ClassGroups.Where(x => x.ClassId == p.ClassId).CountAsync();





            // Business
            MarketSharePositionDto businessSeg = PositionDto(p, SEGMENTS.BUSINESS);

            MarketSharePositionDto smallBusiness = PositionDto(p, SEGMENTS.SMALL_BUSINESS);

            MarketSharePositionDto coroporate = PositionDto(p, SEGMENTS.CORPORATE_CONTRACT);

            MarketSharePositionDto families = PositionDto(p, SEGMENTS.FAMILIES);

            MarketSharePositionDto afluentMature = PositionDto(p, SEGMENTS.AFLUENT_MATURE_TRAVELERS);

            MarketSharePositionDto corporateMeeting = PositionDto(p, SEGMENTS.CORPORATE_BUSINESS_MEETINGS);

            MarketSharePositionDto associationMeeting = PositionDto(p, SEGMENTS.ASSOCIATION_MEETINGS);



            MarketSharePositionDto overAll = new MarketSharePositionDto("Overall");
            //overallShare
            decimal soldRoom = soldRoomList.Where(x => x.GroupID == p.GroupId).Sum(x => x.SoldRoom);
            decimal soldRoomQuater = soldRoomList.Sum(x => x.SoldRoom);
            overAll.MarketSharePosition = DivideSafe(soldRoom, soldRoomQuater);

            if (_overallMarket == 0)
                overAll.MarketShare(0);
            else
                overAll.MarketShare(_overallwithout / _overallMarket);


            MarketSharePositionReportDto positionDto = new MarketSharePositionReportDto();

            positionDto.Data.AddRange(new MarketSharePositionDto[] { overAll, businessSeg, smallBusiness, coroporate, families, afluentMature, corporateMeeting, associationMeeting });

            return positionDto;




        }
        private decimal ActualMarketShare(ReportParams p, string segment)
        {
            var roomSold = soldRoomList.Where(x => x.GroupID == p.GroupId && x.Segment == segment).Sum(x => x.SoldRoom);
            var roomAllocated = soldRoomList.Where(x => x.Segment == segment).Sum(x => x.SoldRoom);
            return roomAllocated == 0 ? 0 : Convert.ToDecimal(roomSold) / Convert.ToDecimal(roomAllocated);
        }


        private decimal ActualMarketPosition(ReportParams p, string segment)
        {
            decimal individualAttriDemand = _weightedList.FirstOrDefault(x => x.GroupID == p.GroupId && x.Segment == segment)!.ActualDemand;
            decimal individualPriceDemand = _priceDecisionList.Where(x => x.GroupID == p.GroupId && x.Segment == segment).Sum(x => x.ActualDemand);

            decimal marketAttriDemand = _weightedList.Where(x => x.Segment == segment).Sum(x => x.ActualDemand);
            decimal marketPriceDemand = _priceDecisionList.Where(x => x.Segment == segment).Sum(x => x.ActualDemand);


            var totalIndividualDemand = individualAttriDemand + individualPriceDemand;
            _overallwithout = _overallwithout + totalIndividualDemand;
            var totalMarketDemand = marketAttriDemand + marketPriceDemand;
            _overallMarket = _overallMarket + totalMarketDemand;

            return totalMarketDemand == 0 ? 0 : Convert.ToDecimal(totalIndividualDemand) / Convert.ToDecimal(totalMarketDemand);
        }




        private MarketSharePositionDto PositionDto(ReportParams p, string segment)
        {
            string label = SEGMENTS.UI_Label(segment);
            return new MarketSharePositionDto(label)
               .MarketShare(ActualMarketShare(p, segment))
               .Position(ActualMarketPosition(p, segment));
        }


    }

}

