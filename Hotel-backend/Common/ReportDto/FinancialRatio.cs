using System.Runtime.Serialization;

namespace Common.ReportDto
{
    public class FinancialRatio
    {
        public FinancialRatio(string label)
        {
            Label = label;
        }

        public string Label { get; private set; }
        public Dictionary<string, AbstractDecimal> Child { get; set; } = new Dictionary<string, AbstractDecimal>();
        public void Add(string label, AbstractDecimal value)
        {
            Child[label] = value;
        }
    }




}
