using Common;

namespace Common.ReportDto
{
    public class BalanceReportDto
    {
        public CurrentAssests CurrentAssests { get; set; }

        public PropertyEquipement PropertyEquipement { get; set; }
        public ReportAttribute OtherAssets { get; set; } = "Other Assests";
        public ReportAttribute TotalAssests { get; set; } = "Total Assests";
        public LibilitiesAndOwnerEquity LibilitiesAndOwnerEquity { get; set; }

        public ReportAttribute ShareHolderEquity { get; set; } = "SHAREHOLDERS' EQUITY";
        public ReportAttribute RetainedEarnings { get; set; } = "RETAINED EARNINGS";
        public ReportAttribute TotalShareHoldersEquity { get; set; } = "TOTAL SHAREHOLDERS’ EQUITY";
        public ReportAttribute TotalLiablitiesAndEquity { get; set; } = "TOTAL LIABILITIES AND SHAREHOLDERS’ EQUITY";


    }
    public class CurrentAssests : ReportAttribute
    {
        public CurrentAssests()
        {
            Label = "CURRENT ASSETS";
        }

        public ReportAttribute Cash { get; private set; } = "Cash";
        public ReportAttribute AccountReceivables { get; set; } = "Account Receivables";
        public ReportAttribute Inventories { get; set; } = "Inventories";
        public ReportAttribute Total { get; set; } = "Total";
    }

    public class PropertyEquipement : ReportAttribute
    {
        public PropertyEquipement()
        {
            Label = "Property and equipment";
        }

        public ReportAttribute PropertyAndEquipment { get; private set; } = "Property and equipment";
        public ReportAttribute LessAccumlatedDepreciation { get; set; } = "Less Accumulated Depreciation and Amortization";
        public ReportAttribute NetPropertyAndEquipment { get; private set; } = "Net Property and Equipment";

    }

    public class LibilitiesAndOwnerEquity : ReportAttribute
    {
        public LibilitiesAndOwnerEquity()
        {
            Label = "LIABILITIES AND OWNER'S EQUITY";
        }
        public ReportAttribute CurrentLibalities { get; set; } = "CURRENT LIABILITIES";
        public ReportAttribute LongTermDebt { get; set; } = "Long Term Debt";
        public ReportAttribute EmergencyLoan { get; set; } = "Emergency Loan";
        public ReportAttribute TotalLibbalities { get; set; } = "TOTAL LIABILITIES";
    }
}
