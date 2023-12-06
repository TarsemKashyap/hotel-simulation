using Common;
using Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Service;

public abstract class AbstractReportService
{

    protected virtual Currency Money(decimal? money)
    {
        if (!money.HasValue)
            return null;
        return new Currency(money.Value);
    }


    protected virtual Percent Percent(decimal? number)
    {
        if (number.HasValue)
            return new Percent(number.Value);
        return null;
    }

    protected virtual ReportAttribute Money(string label, decimal? money)
    {
        Currency currency = money.HasValue ? new Currency(money.Value) : null;
        return new ReportAttribute() { Label = label, Data = currency };
    }


    protected virtual ReportAttribute Number(string label, decimal? money)
    {
        Number currency = money.HasValue ? new Number(money.Value) : null;
        return new ReportAttribute() { Label = label, Data = currency };
    }

    protected virtual decimal DivideSafe(decimal first, decimal divisor)
    {
        return divisor == 0 ? 0 : first / divisor;
    }

    protected bool TryFindLastMonth(HotelDbContext _context, int monthID, out Month lastMonth)
    {
        var months = _context.Months
                        .Include(x => x.Class)
                        .ThenInclude(x => x.Months)
                        .Where(x => x.MonthId == monthID)
                        .SelectMany(x => x.Class.Months)
                        .OrderBy(x => x.MonthId)
                        .ToList();
        var month = months.FirstOrDefault(x => x.MonthId == monthID);
        if (month.Sequence > 0)
        {
            lastMonth = months.FirstOrDefault(x => x.Sequence == (month.Sequence - 1));
            return true;
        }
        lastMonth = null;
        return false;

    }

    protected int LastMonthId(HotelDbContext _context, int monthID)
    {
        var months = _context.Months
                        .Include(x => x.Class)
                        .ThenInclude(x => x.Months)
                        .Where(x => x.MonthId == monthID)
                        .SelectMany(x => x.Class.Months)
                        .OrderBy(x => x.MonthId)
                        .Select(x => x.MonthId)
                        .ToList();
        int currentIndex = months.FindIndex(x => x == monthID);
        return months.ElementAt(currentIndex - 1);

    }
}
