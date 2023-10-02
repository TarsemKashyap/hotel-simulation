using System.Runtime.Serialization;

namespace Common.ReportDto
{
    public class FinancialRatio
    {
        public FinancialRatio()
        {
        }

        public string HotelName { get; set; }

        public ReportAttribute LiquidtyRatios { get; set; } = "Liquidity Ratios";

        public ReportAttribute SolvencyRatios { get; set; } = "Solvency Ratios";

        public ReportAttribute ProfitablityRation { get; set; } = "Profitablity Ratio";

        public ReportAttribute TurnOverRatio { get; set; } = "Turn Over Ratio";
      
    }




}
