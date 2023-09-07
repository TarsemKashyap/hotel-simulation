namespace Common.ReportDto
{
    public class QualityPreceptionReportDto
    {
        public SegmentRating OverAll { get; set; }
        public List<SegmentRating> Segments { get; set; }
        public List<SegmentRating> Attributes { get; set; }
    }

    public class SegmentRating
    {
        public string Label { get; set; }
        public decimal Hotel { get; set; }
        public decimal MarketAverage { get; set; }
    }
}
