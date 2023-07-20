using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class MarketingDecisionDto
{
    public int ID { get; set; }
    public int MonthID { get; set; }
    public int QuarterNo { get; set; }
    public int GroupID { get; set; }
    public string MarketingTechniques { get; set; }
    public string Segment { get; set; }
    public string SpendingFormatN0 { get; set; }
    public string LaborSpendingFormatN0 { get; set; }
    public decimal Spending { get; set; }
    public decimal LaborSpending { get; set; }
    public int ActualDemand { get; set; }
    public bool Confirmed { get; set; }
   
}

