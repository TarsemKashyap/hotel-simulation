using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ReportDto
{
    public class GoalReportDto
    {

        public string Indicators { get; set; }
        public decimal M_P { get; set; }
        public decimal M_M { get; set; }
        public decimal M_PM => M_P - M_M;
        public decimal M_G { get; set; }
        public decimal M_PG => M_P - M_G;

        public string Formatter { get; set; }

        public GoalReportResponse ToGoalReportResponse()
        {
            return new GoalReportResponse
            {

                Indicators = Indicators,
                Performance = M_P,
                MarketAverage = M_M,
                Objective = M_G,
                Formatter = Formatter
            };
        }
    }

    public class GoalReportResponse
    {
        public string Indicators { get; set; }
        public decimal Performance { get; set; }
        public decimal MarketAverage { get; set; }
        public decimal PerfMarketAvgerage => Performance - MarketAverage;
        public decimal Objective { get; set; }
        public decimal PrefObjective => Performance - Objective;
        public string Formatter { get; set; }
    }
}
