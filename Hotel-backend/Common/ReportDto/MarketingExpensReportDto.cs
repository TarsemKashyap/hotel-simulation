using System.Xml.Schema;

namespace Common.ReportDto
{
    public class MarketingExpensReportDto
    {
        public List<MarketingSegment> Segments { get; set; }
        public SegmentDetail Total { get; set; }
    }
    public class MarketingSegment
    {
        public string Label { get; set; }
        public SegmentDetail Labor { get; set; }
        public SegmentDetail Other { get; set; }
    }


    public class SegmentDetail
    {
        public string Label { get; set; }
        public decimal Advertising { get; set; }
        public decimal SalesForce { get; set; }
        public decimal Promotions { get; set; }
        public decimal PublicRelations { get; set; }
        public decimal Total => Advertising + SalesForce + Promotions + PublicRelations;
    }


}
