
namespace Common.ReportDto
{
    public class RevparReportDto
    {
        public RevPar OverAll { get; set; }
        public List<RevPar> OverAllChild { get; set; } = new List<RevPar>();

        public RevPar TotalRevpar { get; set; }

        public RevPar GoPar { get; set; }

        public class RevPar
        {
            public string Label { get; set; }
            public decimal Hotel { get; set; }
            public decimal MarketAvg { get; set; }
            public decimal? Index { get; set; }
        }

        public void AddOverAll(decimal hotel, decimal marketAvg)
        {
            OverAll = new RevPar { Label = "Overall REVPAR", Hotel = hotel, MarketAvg = marketAvg, Index = GetIndex(hotel, marketAvg) };
        }

        private static decimal GetIndex(decimal hotel, decimal marketAvg)
        {
            return marketAvg == 0 ? 0 : hotel / marketAvg;
        }

        public void AddTotalRevPar(decimal hotel, decimal marketAvg)
        {
            TotalRevpar = new RevPar { Label = "Total REVPAR", Hotel = hotel, MarketAvg = marketAvg, Index = GetIndex(hotel, marketAvg) };
        }

        public void AddGoRevPar(decimal hotel, decimal marketAvg)
        {
            GoPar = new RevPar { Label = "GOPAR", Hotel = hotel, MarketAvg = marketAvg, Index = GetIndex(hotel, marketAvg) };
        }

        public void WeekdayRevPar(decimal hotel, decimal marketAvg)
        {
            OverAllChild.Add(new RevPar { Label = "Weekday REVPAR", Hotel = hotel, MarketAvg = marketAvg, Index = GetIndex(hotel, marketAvg) });
        }
        public void WeekdendRevPar(decimal hotel, decimal marketAvg)
        {
            OverAllChild.Add(new RevPar { Label = "Weekend REVPAR", Hotel = hotel, MarketAvg = marketAvg, Index = GetIndex(hotel, marketAvg) });
        }
    }
}
