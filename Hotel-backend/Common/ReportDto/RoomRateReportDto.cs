
namespace Common.ReportDto
{
    public class RoomRateReportDto
    {
        public RoomRateAgg Direct { get; set; }
        public RoomRateAgg TravelAgent { get; set; }
        public RoomRateAgg OnlineTravelAgent { get; set; }
        public RoomRateAgg Opaque { get; set; }


    }

    public class RoomRateAgg
    {
        public RoomRateAgg(List<RoomRateDto> segments)
        {
            Segments = segments;
        }
        public decimal SumWeekdayRoomSold => Segments.Sum(x => x.WeekDayRoomSold);
        public decimal SumWeekEndRoomSold => Segments.Sum(x => x.WeekendRoomSold);
        public decimal SumWeekdayCost => Segments.Sum(x => x.WeekdayCost);
        public decimal SumWeekEndCost => Segments.Sum(x => x.WeekendCost);
        public decimal SumTotalCost => Segments.Sum(x => x.TotalCost);
        public List<RoomRateDto> Segments { get; private set; }
    }

    public class RoomRateDto
    {
        public string Label { get; set; }
        public decimal WeekdayRate { get; set; }
        public decimal WeekDayRoomSold { get; set; }
        public decimal WeekdayCost { get; set; }
        public decimal WeekendRate { get; set; }
        public decimal WeekendRoomSold { get; set; }
        public decimal WeekendCost { get; set; }

        public decimal TotalCost => WeekdayCost + WeekendCost;
    }


}
