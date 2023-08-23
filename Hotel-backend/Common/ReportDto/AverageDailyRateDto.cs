﻿namespace Common.ReportDto
{
    public class AverageDailyRateDto
    {
        public List<AvgDailyRate> Data { get; set; } = new List<AvgDailyRate>();

        public void OverAllADR(decimal hotel, decimal mktAvg)
        {

            Data.Add(new AvgDailyRate { Label = "Overall ADR", Hotel = hotel, MarketAvg = mktAvg, Index = hotel / mktAvg });
        }

        public void WeekdayADR(decimal hotel, decimal mktAvg)
        {

            Data.Add(new AvgDailyRate { Label = "Weekday ADR", Hotel = hotel, MarketAvg = mktAvg, Index = hotel / mktAvg });
        }
        public void WeekendADR(decimal hotel, decimal mktAvg)
        {

            Data.Add(new AvgDailyRate { Label = "Weekend ADR", Hotel = hotel, MarketAvg = mktAvg, Index = hotel / mktAvg });
        }

        public class AvgDailyRate
        {
            public string Label { get; set; }
            public decimal Hotel { get; set; }
            public decimal MarketAvg { get; set; }
            public decimal? Index { get; set; }
        }
    }
}
