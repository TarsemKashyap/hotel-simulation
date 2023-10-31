using Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Service;
using Common.Dto;
using MapsterMapper;
using System;

public interface IMarketingService
{

    Task<IList<MarketingDecisionDto>> MarketingDetails(int MonthID, int? groupId, int quarterNo);

    Task UpdateMarketingDetails(List<MarketingDecisionDto> marketingDecisionDtos);

}

public class MarketingService : IMarketingService
{
    private readonly IMapper _mapper;
    private readonly HotelDbContext _context;

    public MarketingService(IMapper mapper, HotelDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task UpdateMarketingDetails(List<MarketingDecisionDto> marketingDecisionDtos)
    {
        var marketingList = new List<MarketingDecision>();
        foreach (var item in marketingDecisionDtos)
        {
            var marketingDeatils = _context.MarketingDecision.SingleOrDefault(x => x.ID == item.ID);
            marketingDeatils.Spending = Convert.ToDecimal(item.Spending);
            marketingDeatils.LaborSpending = Convert.ToDecimal(item.LaborSpending);
            marketingList.Add(marketingDeatils);
        }
        _context.MarketingDecision.UpdateRange(marketingList);
        await _context.SaveChangesAsync();
    }

    public async Task<IList<MarketingDecisionDto>> MarketingDetails(int MonthID, int? groupId, int quarterNo)
    {
        var marketingDetails = _context.MarketingDecision.Where(c => c.MonthID == MonthID && c.GroupID == groupId && c.QuarterNo == quarterNo).Select(s => new MarketingDecisionDto
        {
            MarketingTechniques = s.MarketingTechniques,
            GroupID = s.GroupID,
            QuarterNo = s.QuarterNo,
            Spending = s.Spending,
            LaborSpending = s.LaborSpending,
            SpendingFormatN0 = s.Spending.ToString("n0").Replace(",",""),
            LaborSpendingFormatN0 = s.LaborSpending.ToString("n0").Replace(",", ""),
            Segment = s.Segment.Replace("\t", "").Trim(),
            ActualDemand = s.ActualDemand,
            MonthID = s.MonthID,
            Confirmed=s.Confirmed,
            ID = s.ID
        }).ToList();

        return marketingDetails;

    }
  

}