using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PriceDecisionDto
{
    public int ID { get; set; }
    public int MonthID { get; set; }
    public int QuarterNo { get; set; }
    public string GroupID { get; set; }
    public bool Weekday { get; set; }
    public string DistributionChannel { get; set; }
    public string Segment { get; set; }
    public int Price { get; set; }
    public int ActualDemand { get; set; }
    public bool Confirmed { get; set; }
}

