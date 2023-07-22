using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Common.ReportDto
{
    public class StatisticsDto
    {
        public AbstractDecimal MonthlyProfit { get; set; }
        public AbstractDecimal AccumulativeProfit { get; set; }
        public AbstractDecimal MarketShareRevenueBased { get; set; }
        public AbstractDecimal MarketShareRoomSoldBased { get; set; }
        public AbstractDecimal Occupancy { get; set; }
        public AbstractDecimal REVPAR { get; set; }

    }
}
