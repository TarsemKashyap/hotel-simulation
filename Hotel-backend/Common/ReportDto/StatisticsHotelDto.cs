namespace Common.ReportDto
{
    public class StatisticsHotelDto
    {
        public string HotelName { get; set; }
        public Percent OccupancyPercentage { get; set; }
        public Currency RoomRevenue { get; set; }
        public Currency TotalRevenue { get; set; }
        public Percent MarketShareRoomsSold { get; set; }
        public Percent MarketShareRevenue { get; set; }
        public Currency REVPAR { get; set; }
        public Currency ADR { get; set; }
        public Percent YieldMgmt { get; set; }
        public Percent OperatingEfficiencyRatio { get; set; }
        public Percent ProfitMargin { get; set; }
    }


}
