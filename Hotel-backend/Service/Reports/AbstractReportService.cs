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


}


