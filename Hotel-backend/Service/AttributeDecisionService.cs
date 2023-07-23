using Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service;

using MapsterMapper;
using System;

public interface IAttributeDecisionService
{
        
        Task<IList<AttributeDecisionDto>> AttributeDecisionDetails(int MonthID,int? groupId,int quarterNo);

        Task UpdateAttributeDecision(List<AttributeDecisionDto> attributeDecisionDto);
}

public class AttributeDecisionService : IAttributeDecisionService
{
    private readonly IMapper _mapper;
    private readonly HotelDbContext _context;

    public AttributeDecisionService(IMapper mapper, HotelDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task UpdateAttributeDecision(List<AttributeDecisionDto> attributeDecisionDtos)
    {
        var AttributeDecisionList = new List<AttributeDecision>();
        foreach (var item in attributeDecisionDtos)
        {
            var attributeDecision = _context.AttributeDecision.SingleOrDefault(x => x.ID == item.ID);
            attributeDecision.NewCapital = Convert.ToDecimal(item.NewCapital);
            attributeDecision.OperationBudget = Convert.ToDecimal(item.OperationBudget);
            attributeDecision.LaborBudget = Convert.ToDecimal(item.LaborBudget);
            AttributeDecisionList.Add(attributeDecision);
        }
        _context.AttributeDecision.UpdateRange(AttributeDecisionList);
        await _context.SaveChangesAsync();
    }

    public async Task<IList<AttributeDecisionDto>> AttributeDecisionDetails(int MonthID, int? groupId, int quarterNo)
    {
        var attributeDecisionDetails = _context.AttributeDecision.Where(c => c.MonthID == MonthID && c.GroupID == groupId && c.QuarterNo == quarterNo).Select(s=> new AttributeDecisionDto
        {
            MonthID = s.MonthID,
            GroupID = s.GroupID,
            QuarterNo = s.QuarterNo,
            AccumulatedCapital= s.AccumulatedCapital,
            LaborBudget= s.LaborBudget,
            OperationBudget= s.OperationBudget,
            QuarterForecast= s.QuarterForecast,
            NewCapital= s.NewCapital,
            Attribute=s.Attribute.Replace("\t", "").Trim(),
            ID = s.ID,

        }).ToList();


        return attributeDecisionDetails;

    }
  

}