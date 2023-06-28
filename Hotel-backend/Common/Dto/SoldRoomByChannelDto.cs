using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SoldRoomByChannelDto
{
    public int ID { get; set; }
    public int MonthID { get; set; }
    public int QuarterNo { get; set; }
    public int GroupID { get; set; }
    public string Segment { get; set; }
    public string Channel { get; set; }
    public bool Weekday { get; set; }
    public int Revenue { get; set; }
    public int SoldRoom { get; set; }
    public int Cost { get; set; }
}

