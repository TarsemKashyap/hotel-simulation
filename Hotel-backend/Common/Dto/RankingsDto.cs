using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class RankingsDto
{
    public int ID { get; set; }
    public int MonthID { get; set; }
    public int Month { get; set; }
    public int TeamNo { get; set; }
    public string TeamName { get; set; }
    public string Institution { get; set; }
    public string Indicator { get; set; }
    public decimal Performance { get; set; }
    public DateTime Time { get; set; }
}

