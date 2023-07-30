using Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Service;
using Common.Dto;
using MapsterMapper;
using Mysqlx.Prepare;
using System;

public interface IBalanceSheetService
{

    Task<BalanceSheetDto> BalanceSheetDetails(int MonthID, int? groupId, int quarterNo);

    Task UpdateBalanceSheetDetails(BalanceSheetDto BalanceSheetDtoDtos);

}

public class BalanceSheetService : IBalanceSheetService
{
    private readonly IMapper _mapper;
    private readonly HotelDbContext _context;

    public BalanceSheetService(IMapper mapper, HotelDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task UpdateBalanceSheetDetails(BalanceSheetDto balanceSheetDto)
    {
        var balanceRow = _context.BalanceSheet.SingleOrDefault(x => x.ID == balanceSheetDto.ID);
        decimal longPay = Convert.ToDecimal(balanceSheetDto.LongDebtPay);
        decimal longBorrow = Convert.ToDecimal(balanceSheetDto.longBorrow);
        balanceRow.LongDebtPay = longPay - longBorrow;
        balanceRow.ShortDebtPay = Convert.ToDecimal(balanceSheetDto.ShortDebtPay);
        _context.BalanceSheet.Update(balanceRow);
        await _context.SaveChangesAsync();
    }

    public async Task<BalanceSheetDto> BalanceSheetDetails(int MonthID, int? groupId, int quarterNo)
    {
        var balanceDetails =  _context.BalanceSheet.Where(c => c.MonthID == MonthID && c.GroupID == groupId && c.QuarterNo == quarterNo).Select(s => new BalanceSheetDto
        {
            LongDebt = s.LongDebt,
            ShortDebt = s.ShortDebt,
            LongDebtPay = s.LongDebtPay,
            ShortDebtPay = s.ShortDebtPay,
            GroupID = s.GroupID,
            QuarterNo = s.QuarterNo,
            ID = s.ID
        }).ToList().FirstOrDefault();

        return balanceDetails;

    }
  

}