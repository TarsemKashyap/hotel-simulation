namespace Common.ReportDto
{
    public class DemandSegmentReportDto
    {
        public string Segment { get; set; }
        public DemandSegmentDto WeekDay { get; set; }
        public DemandSegmentDto WeekEnd { get; set; }
    }
    public class DemandSegmentDto
    {
        public string Label { get; set; }
        public int RoomAllocated { get; set; }
        public int RoomDemanded { get; set; }
        public int RoomSold { get; set; }
        public int Deficit { get; set; }
    }

    public class DemandReportDto
    {
        public List<DemandSegmentReportDto> HotelDemand { get; set; }
        public List<DemandSegmentReportDto> MarketDemand { get; set; }
    }

}
