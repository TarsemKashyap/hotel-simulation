using System.Runtime.InteropServices;

namespace Common.ReportDto
{

    public class MarketShareReportDto
    {
        public List<CategoryLine> OverAllPercentages { get; set; } = new List<CategoryLine>();
        public List<Segment> OccupancyBySegment { get; private set; } = new List<Segment>();
        public void AddOverAll(string label, decimal hotel, decimal marketAvg)
        {
            OverAllPercentages.Add(new CategoryLine { Label = label, Hotel = hotel * 100, MarketAverage = marketAvg * 100, Index = GetIndex(hotel, marketAvg) });
        }

        public void AddSegment(Segment overallPercentage)
        {
            OccupancyBySegment.Add(overallPercentage);
        }

        private static decimal? GetIndex(decimal hotel, decimal marketAvg)
        {
            return marketAvg == 0 ? null : (hotel / marketAvg);
        }

    }
    public class CategoryLine
    {
        public string Label { get; set; }
        public decimal Hotel { get; set; }
        public decimal MarketAverage { get; set; }
        public decimal? Index { get; set; }
    }

    public class Segment
    {
        public string SegmentTitle { get; private set; }

        public Segment(string segment)
        {
            SegmentTitle = segment;
        }

        public static Segment Create(string segment)
        {
            return new Segment(segment);
        }

        public List<CategoryLine> Segments { get; private set; } = new List<CategoryLine>();


        public Segment WeekDay(decimal hotel, decimal marketAvg)
        {
            Segments.Add(new CategoryLine { Label = "Weekday", Hotel = hotel * 100, MarketAverage = marketAvg * 100, Index = GetIndex(hotel, marketAvg) });
            return this;
        }

        private static decimal? GetIndex(decimal hotel, decimal marketAvg)
        {
            return marketAvg == 0 ? null : (hotel / marketAvg);
        }

        public Segment WeekEnd(decimal hotel, decimal marketAvg)
        {
            Segments.Add(new CategoryLine { Label = "Weekend", Hotel = hotel * 100, MarketAverage = marketAvg * 100, Index = GetIndex(hotel, marketAvg) });
            return this;
        }
        public Segment Overall(decimal hotel, decimal marketAvg)
        {
            Segments.Add(new CategoryLine { Label = "Overall", Hotel = hotel * 100, MarketAverage = marketAvg * 100, Index = GetIndex(hotel, marketAvg) });
            return this;
        }
    }
}
