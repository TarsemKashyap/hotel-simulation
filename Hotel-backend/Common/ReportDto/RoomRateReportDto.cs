
namespace Common.ReportDto
{
    public class RoomRateReportDto
    {
        public List<RoomRateDto> Direct { get; set; }
        public List<RoomRateDto> TravelAgent { get; set; }
        public List<RoomRateDto> OnlineTravelAgent { get; set; }
        public List<RoomRateDto> Opaque { get; set; }


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
    }

   
}
