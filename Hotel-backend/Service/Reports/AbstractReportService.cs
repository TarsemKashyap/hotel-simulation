using Common;

namespace Service;

public abstract class AbstractReportService
{

    protected virtual Currency Money(decimal? money)
    {
        if (!money.HasValue)
            return null;
        return new Currency(money.Value);
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
        return first == 0 ? 0 : first / divisor;
    }
}


