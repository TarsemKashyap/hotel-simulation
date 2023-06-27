using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class RoomAllocationDto
{
    public int ID { get; set; }
    public int MonthID { get; set; }

    public int QuarterNo { get; set; }
    public int GroupID { get; set; }
    public bool Weekday { get; set; }
    public string Segment { get; set; }
    public int RoomsAllocated { get; set; }
    public int ActualDemand { get; set; }
    public int RoomsSold { get; set; }
    public bool Confirmed { get; set; }
    public int Revenue { get; set; }
    public int QuarterForecast { get; set; }
}

