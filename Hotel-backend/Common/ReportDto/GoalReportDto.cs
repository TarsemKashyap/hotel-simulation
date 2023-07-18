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
    }

    public static class ReportFormatExtension
    {
        public static string ToPercentage(this decimal value)
        {
            return value.ToString("P");
        }
        public static string Currency(this decimal value)
        {
            return value.ToString("C0");
        }
    }
}
