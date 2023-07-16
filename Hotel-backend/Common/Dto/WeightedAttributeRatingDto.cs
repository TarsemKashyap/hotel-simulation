using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class WeightedAttributeRatingDto
{
    public int ID { get; set; }
    public int MonthID { get; set; }

    public int QuarterNo { get; set; }
    public int GroupID { get; set; }
    public string Segment { get; set; }
    public decimal CustomerRating { get; set; }
    public decimal ActualDemand { get; set; }
}

