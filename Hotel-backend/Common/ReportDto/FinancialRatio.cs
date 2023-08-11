using System.Runtime.Serialization;

namespace Common.ReportDto
{
    public class FinancialRatio
    {
        public FinancialRatio()
        {
        }

        //public string Label { get; private set; }

        //public List<ReportAttribute> Child { get; set; }

        public ReportAttribute LiquidtyRatios { get; set; } = "Liquidity Ratios";

        public ReportAttribute SolvencyRatios { get; set; } = "Solvency Ratios";

        public ReportAttribute ProfitablityRation { get; set; } = "Profitablity Ratio";

        public ReportAttribute TurnOverRatio { get; set; } = "Turn Over Ratio";

        //public Dictionary<string, AbstractDecimal> Child { get; set; } = new Dictionary<string, AbstractDecimal>();
        //public void Add(string label, ReportAttribute value)
        //{
        //    Child.Add(value);
        //}
    }




}
