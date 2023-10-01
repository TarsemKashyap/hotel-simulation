namespace Common.ReportDto
{
    public class StatisticsDto
    {
        public string HotelName { get; set; }
        public AbstractDecimal MonthlyProfit { get; set; }
        public AbstractDecimal AccumulativeProfit { get; set; }
        public AbstractDecimal MarketShareRevenueBased { get; set; }
        public AbstractDecimal MarketShareRoomSoldBased { get; set; }
        public AbstractDecimal Occupancy { get; set; }
        public AbstractDecimal REVPAR { get; set; }

    }


}
