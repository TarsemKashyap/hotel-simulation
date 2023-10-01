namespace Common.ReportDto
{
    public class PerformanceReportDto
    {
        public StatisticsDto Statstics { get; set; }
        public FinancialRatio FinancialRatios { get; set; }
    }

    public class PerformanceInstructorReportDto
    {
        public List<StatisticsDto> Statstics { get; set; }
        public List<FinancialRatio> FinancialRatio { get; set; }
    }

}
