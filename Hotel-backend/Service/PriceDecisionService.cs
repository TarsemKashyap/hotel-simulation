using Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service;

using Common.Dto;
using MapsterMapper;
using System;

public interface IPriceDecisionService
{
        
        Task<IList<PriceDecisionDto>> PriceDecisionDetails(int MonthID,int? groupId,int quarterNo);

        Task UpdatePriceDecision(List<PriceDecisionDto> PriceDecisionDto);
}

public class PriceDecisionService : IPriceDecisionService
{
    private readonly IMapper _mapper;
    private readonly HotelDbContext _context;

    public PriceDecisionService(IMapper mapper, HotelDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task UpdatePriceDecision(List<PriceDecisionDto> priceDecisionDtos)
    {
        var priceDecisionList = new List<PriceDecision>();
        foreach (var item in priceDecisionDtos)
        {
            var priceDecision = _context.PriceDecision.SingleOrDefault(x => x.ID == item.ID);
            priceDecision.Price = Convert.ToDecimal(item.Price);
            priceDecisionList.Add(priceDecision);
        }
        _context.PriceDecision.UpdateRange(priceDecisionList);
        await _context.SaveChangesAsync();
    }

    public async Task<IList<PriceDecisionDto>> PriceDecisionDetails(int MonthID, int? groupId, int quarterNo)
    {
        var priceDecisionDetails = _context.PriceDecision.Where(c => c.MonthID == MonthID && c.GroupID == groupId && c.QuarterNo == quarterNo).Select(s => new PriceDecisionDto
        {
            Weekday = s.Weekday,
            GroupID = s.GroupID,
            QuarterNo = s.QuarterNo,
            DistributionChannel = s.DistributionChannel,
            Segment = s.Segment.Replace("\t", "").Trim(),
            Price = s.Price,
            priceNOFormat = s.Price.ToString("N0"),
            ID = s.ID
        }).ToList();

        return priceDecisionDetails;

    }
  

}