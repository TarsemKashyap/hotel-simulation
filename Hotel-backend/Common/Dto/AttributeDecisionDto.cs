using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AttributeDecisionDto
{
    public int ID { get; set; }
    public int QuarterNo { get; set; }
    public int GroupID { get; set; }
    public string Attribute { get; set; }
    public decimal AccumulatedCapital { get; set; }
    public decimal NewCapital { get; set; }
    public decimal OperationBudget { get; set; }
    public decimal LaborBudget { get; set; }
    public bool Confirmed { get; set; }
    public int QuarterForecast { get; set; }
    public int MonthID { get; set; }
   
}

