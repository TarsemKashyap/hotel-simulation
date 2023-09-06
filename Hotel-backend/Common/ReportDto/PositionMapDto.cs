namespace Common.ReportDto
{

    public class PositionMapReportDto
    {
        public string Segment { get; set; }
        public List<PositionMapDto> GroupRating { get; set; }
    }
    public class PositionMapDto
    {
        public string ClassGroup { get; set; }
        public decimal QualityRating { get; set; }
        public decimal RoomRate { get; set; }
    }


}
