using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class PriceDecisionDto
    {
        public int ID { get; set; }
        public int MonthID { get; set; }
        public int QuarterNo { get; set; }
        public int GroupID { get; set; }
        public bool Weekday { get; set; }
        public string DistributionChannel { get; set; }
        public string Segment { get; set; }
        public decimal Price { get; set; }
        public int ActualDemand { get; set; }
        public bool Confirmed { get; set; }
        public string priceNOFormat { get; set; }
    }
}
