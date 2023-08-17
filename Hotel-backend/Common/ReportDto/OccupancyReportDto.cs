using System.Runtime.InteropServices;

namespace Common.ReportDto
{
    public class OccupancyReportDto
    {
        public List<OccupancyDetails> OverAllPercentages { get; set; } = new List<OccupancyDetails>();
        public List<OccupancyBySegement> OccupancyBySegment { get; private set; } = new List<OccupancyBySegement>();
        public void AddOverAll(string label, decimal hotel, decimal marketAvg)
        {
            OverAllPercentages.Add(new OccupancyDetails { Label = label, Hotel = hotel, MarketAverage = marketAvg, Index = GetIndex(hotel, marketAvg) });
        }

        public void AddSegment(OccupancyBySegement overallPercentage)
        {
            OccupancyBySegment.Add(overallPercentage);
        }

        private static decimal? GetIndex(decimal hotel, decimal marketAvg)
        {
            return marketAvg == 0 ? null : (hotel / marketAvg);
        }

    }
    public class OccupancyDetails
    {
        public string Label { get; set; }
        public decimal Hotel { get; set; }
        public decimal MarketAverage { get; set; }
        public decimal? Index { get; set; }
    }

    public class OccupancyBySegement
    {
        public string SegmentTitle { get; private set; }

        public OccupancyBySegement(string segment)
        {
            SegmentTitle = segment;
        }

        public static OccupancyBySegement Create(string segment)
        {
            return new OccupancyBySegement(segment);
        }

        public List<OccupancyDetails> Segments { get; private set; } = new List<OccupancyDetails>();


        public OccupancyBySegement WeekDay(decimal hotel, decimal marketAvg)
        {
            Segments.Add(new OccupancyDetails { Label = "Weekday", Hotel = hotel, MarketAverage = marketAvg, Index = GetIndex(hotel, marketAvg) });
            return this;
        }

        private static decimal? GetIndex(decimal hotel, decimal marketAvg)
        {
            return marketAvg == 0 ? null : (hotel / marketAvg);
        }

        public OccupancyBySegement WeekEnd(decimal hotel, decimal marketAvg)
        {
            Segments.Add(new OccupancyDetails { Label = "Weekday", Hotel = hotel, MarketAverage = marketAvg, Index = GetIndex(hotel, marketAvg) });
            return this;
        }
        public OccupancyBySegement Overall(decimal hotel, decimal marketAvg)
        {
            Segments.Add(new OccupancyDetails { Label = "Overall", Hotel = hotel, MarketAverage = marketAvg, Index = GetIndex(hotel, marketAvg) });
            return this;
        }
    }
}
