namespace Common.ReportDto
{
    public class AttributeAmentiDto
    {
        public string Label { get; set; }
        public decimal AccumulatedCapital { get; set; }
        public decimal NewCaptial { get; set; }
        public decimal OperationBudget { get; set; }
        public decimal LaborBudget { get; set; }
    }

    public class AttributeAmentitiesReportDto
    {
        public List<AttributeAmentiDto> Attributes { get; set; }
        public decimal TotalExpense { get; set; }
    }
}
