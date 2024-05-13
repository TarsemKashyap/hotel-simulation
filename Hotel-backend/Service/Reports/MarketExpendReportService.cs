using Common;
using Common.ReportDto;
using Database;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service;

public interface IMarketExpendReportService
{
    Task<MarketingExpensReportDto> ReportAsync(ReportParams p);
}

public class MarketExpendReportService : IMarketExpendReportService
{
    private readonly HotelDbContext _context;

    public MarketExpendReportService(HotelDbContext context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    public async Task<MarketingExpensReportDto> ReportAsync(ReportParams p)
    {
        var _marketingList = await _context.MarketingDecision.Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter && x.GroupID == p.GroupId).ToListAsync();

        MarketingSegment business = Segment(SEGMENTS.BUSINESS, _marketingList);
        MarketingSegment smallBusiness = Segment(SEGMENTS.SMALL_BUSINESS, _marketingList);
        MarketingSegment CoroporateContract = Segment(SEGMENTS.CORPORATE_CONTRACT, _marketingList);
        MarketingSegment families = Segment(SEGMENTS.FAMILIES, _marketingList);
        MarketingSegment afluentMatureTravlers = Segment(SEGMENTS.AFLUENT_MATURE_TRAVELERS, _marketingList);
        MarketingSegment internationLeisureTravel = Segment(SEGMENTS.INTERNATIONAL_LEISURE_TRAVELERS, _marketingList);
        MarketingSegment corporateBusinessMeet = Segment(SEGMENTS.CORPORATE_BUSINESS_MEETINGS, _marketingList);
        MarketingSegment associationMeetings = Segment(SEGMENTS.ASSOCIATION_MEETINGS, _marketingList);

        MarketingExpensReportDto reportDto = new MarketingExpensReportDto();
        reportDto.Segments = new List<MarketingSegment>() { business, smallBusiness, CoroporateContract, families, afluentMatureTravlers, internationLeisureTravel, CoroporateContract, associationMeetings };

        await SetMarketAvg(p, reportDto.Segments);

        MarketingSegment subTotal = new MarketingSegment
        {

            Labor = new SegmentDetail
            {
                Label = "Labor Total",
                Advertising = _marketingList.Where(x => x.MarketingTechniques.Equals(MARKETING_TECHNIQUE.ADVERTISING)).Sum(x => x.LaborSpending),
                Promotions = _marketingList.Where(x => x.MarketingTechniques.Equals(MARKETING_TECHNIQUE.PROMOTIONS)).Sum(x => x.LaborSpending),
                PublicRelations = _marketingList.Where(x => x.MarketingTechniques.Equals(MARKETING_TECHNIQUE.PUBLIC_RELATIONS)).Sum(x => x.LaborSpending),
                SalesForce = _marketingList.Where(x => x.MarketingTechniques.Equals(MARKETING_TECHNIQUE.SALES_FORCE)).Sum(x => x.LaborSpending)
            },
            Other = new SegmentDetail
            {
                Label = "Other Total",
                Advertising = _marketingList.Where(x => x.MarketingTechniques.Equals(MARKETING_TECHNIQUE.ADVERTISING)).Sum(x => x.Spending),
                Promotions = _marketingList.Where(x => x.MarketingTechniques.Equals(MARKETING_TECHNIQUE.PROMOTIONS)).Sum(x => x.Spending),
                PublicRelations = _marketingList.Where(x => x.MarketingTechniques.Equals(MARKETING_TECHNIQUE.PUBLIC_RELATIONS)).Sum(x => x.Spending),
                SalesForce = _marketingList.Where(x => x.MarketingTechniques.Equals(MARKETING_TECHNIQUE.SALES_FORCE)).Sum(x => x.Spending)
            },
        };

        reportDto.Segments.Add(subTotal);

        reportDto.Total = new SegmentDetail
        {
            Label = "TOTAL",
            Advertising = subTotal.Labor.Advertising + subTotal.Other.Advertising,
            Promotions = subTotal.Labor.Promotions + subTotal.Other.Promotions,
            PublicRelations = subTotal.Labor.PublicRelations + subTotal.Other.PublicRelations,
            SalesForce = subTotal.Labor.SalesForce + subTotal.Other.SalesForce,
        };

        return reportDto;


    }

    private async Task SetMarketAvg(ReportParams p, List<MarketingSegment> marketingSegments)
    {

        foreach (var marketingSegment in marketingSegments)
        {

            var data = await _context.MarketingDecision.Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter && x.Segment == marketingSegment.Label && x.MarketingTechniques == p.Segment).Select(x => new { x.LaborSpending, x.Spending }).ToListAsync();
            marketingSegment.Labor.MarketAvg = data.Average(x => x.LaborSpending);
            marketingSegment.Other.MarketAvg = data.Average(x => x.Spending);
        }

    }

    private MarketingSegment Segment(string segment, List<MarketingDecision> _marketingList)
    {
        var dict = _marketingList.Where(x => x.Segment == segment).ToDictionary(x => x.MarketingTechniques);
        return new MarketingSegment
        {
            Label = segment,
            Labor = new SegmentDetail
            {
                Label = "Labor",
                Advertising = dict[MARKETING_TECHNIQUE.ADVERTISING].LaborSpending,
                Promotions = dict[MARKETING_TECHNIQUE.PROMOTIONS].LaborSpending,
                PublicRelations = dict[MARKETING_TECHNIQUE.PUBLIC_RELATIONS].LaborSpending,
                SalesForce = dict[MARKETING_TECHNIQUE.SALES_FORCE].LaborSpending
            },
            Other = new SegmentDetail
            {
                Label = "Other",
                Advertising = dict[MARKETING_TECHNIQUE.ADVERTISING].Spending,
                Promotions = dict[MARKETING_TECHNIQUE.PROMOTIONS].Spending,
                PublicRelations = dict[MARKETING_TECHNIQUE.PUBLIC_RELATIONS].Spending,
                SalesForce = dict[MARKETING_TECHNIQUE.SALES_FORCE].Spending
            },
        };
    }
}







