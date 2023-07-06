using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


public class PriceList
{
}
public class MarketDecisionPriceList
{
    public string MarketingTechniques { get; set; }

    public string Segment { get; set; }
    public decimal Spending { get; set; }
    public decimal LaborSpending { get; set; }
    public decimal ActualDemand { get; set; }
}
public class PriceDecisionPriceList
{
    public string DistributionChannel { get; set; }

    public string Segment { get; set; }
    public decimal Price { get; set; }
    public decimal ActualDemand { get; set; }

}
public class AttributeDecisionPriceList
{
    public string Attribute { get; set; }
    public int AccumulatedCapital { get; set; }
    public int NewCapital { get; set; }
    public int OperationBudget { get; set; }
    public int LaborBudget { get; set; }
    public int QuarterForecast { get; set; }

}
public class RoomAllocationPriceList
{
    public string Segment { get; set; }
    public int RoomsAllocated { get; set; }
    public int ActualDemand { get; set; }
    public int RoomsSold { get; set; }
    public int Revenue { get; set; }
    public int QuarterForecast { get; set; }
    public bool WeekDay { get; set; }

}

public class PriceListCreated
{
    public List<MarketDecisionPriceList> MarketDecisionPriceList()

    {
        var list = new List<MarketDecisionPriceList> {
        new MarketDecisionPriceList {  MarketingTechniques = "Advertising", Segment = "Business", Spending = 3300, LaborSpending = 1400, ActualDemand = 0,  },
        new MarketDecisionPriceList {  MarketingTechniques = "Advertising", Segment = "Small Business", Spending = 3300, LaborSpending = 1400, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Advertising", Segment = "Corporate contract", Spending = 550, LaborSpending = 240, ActualDemand = 0},
        new MarketDecisionPriceList { MarketingTechniques = "Advertising", Segment = "Families", Spending = 5500, LaborSpending = 2400, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Advertising", Segment = "Afluent Mature Travelers", Spending = 7700, LaborSpending = 3300, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Advertising", Segment = "International leisure travelers", Spending = 7100, LaborSpending = 3000, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Advertising", Segment = "Corporate/Business Meetings", Spending = 550, LaborSpending = 240, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Advertising", Segment = "Association Meetings", Spending = 550, LaborSpending = 240, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Sales Force", Segment = "Business", Spending = 3120, LaborSpending = 4700, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Sales Force", Segment = "Small Business", Spending = 1900, LaborSpending = 2800, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Sales Force", Segment = "Corporate contract", Spending = 5640, LaborSpending = 8500, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Sales Force", Segment = "Families", Spending = 0, LaborSpending = 0, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Sales Force", Segment = "Afluent Mature Travelers", Spending = 0, LaborSpending = 0, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Sales Force", Segment = "International leisure travelers", Spending = 0, LaborSpending = 0, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Sales Force", Segment = "Corporate/Business Meetings", Spending = 5640, LaborSpending = 8500, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Sales Force", Segment = "Association Meetings", Spending = 5000, LaborSpending = 7500, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Promotions", Segment = "Business", Spending = 550, LaborSpending = 220, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Promotions", Segment = "Small Business", Spending = 3300, LaborSpending = 1400, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Promotions", Segment = "Corporate contract", Spending = 0, LaborSpending = 0, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Promotions", Segment = "Families", Spending = 3300, LaborSpending = 1400, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Promotions", Segment = "Afluent Mature Travelers", Spending = 1100, LaborSpending = 460, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Promotions", Segment = "International leisure travelers", Spending = 1100, LaborSpending = 470, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Promotions", Segment = "Corporate/Business Meetings", Spending = 0, LaborSpending = 0, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Promotions", Segment = "Association Meetings", Spending = 0, LaborSpending = 0, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Public Relations", Segment = "Business", Spending = 1180, LaborSpending = 1200, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Public Relations", Segment = "Small Business", Spending = 800, LaborSpending = 800, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Public Relations", Segment = "Corporate contract", Spending = 400, LaborSpending = 400, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Public Relations", Segment = "Families", Spending = 1600, LaborSpending = 1560, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Public Relations", Segment = "Afluent Mature Travelers", Spending = 1600, LaborSpending = 1560, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Public Relations", Segment = "International leisure travelers", Spending = 1960, LaborSpending = 1960, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Public Relations", Segment = "Corporate/Business Meetings", Spending = 400, LaborSpending = 400, ActualDemand = 0, },
        new MarketDecisionPriceList { MarketingTechniques = "Public Relations", Segment = "Association Meetings", Spending = 1180, LaborSpending = 1200, ActualDemand = 0, }
         };


        return list;
    }
    public List<PriceDecisionPriceList> PriceDecisionPriceList()
    {

        var list = new List<PriceDecisionPriceList>
        {
        new PriceDecisionPriceList { DistributionChannel = "Direct", Segment = "Business", Price = 200, ActualDemand = 0 },
        new PriceDecisionPriceList { DistributionChannel = "Direct", Segment = "Corporate contract", Price = 160, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Direct", Segment = "Families", Price = 125, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Direct", Segment = "Afluent Mature Travelers", Price = 165, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Direct", Segment = "International leisure travelers", Price = 175, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Direct", Segment = "Corporate/Business Meetings", Price = 200, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Direct", Segment = "Association Meetings", Price = 150, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Travel Agent", Segment = "Business", Price = 200, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Travel Agent", Segment = "Small Business", Price = 150, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Travel Agent", Segment = "Corporate contract", Price = 160, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Travel Agent", Segment = "Families", Price = 125, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Travel Agent", Segment = "Afluent Mature Travelers", Price = 165, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Travel Agent", Segment = "International leisure travelers", Price = 175, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Travel Agent", Segment = "Corporate/Business Meetings", Price = 200, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Travel Agent", Segment = "Association Meetings", Price = 150, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Online Travel Agent", Segment = "Business", Price = 200, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Online Travel Agent", Segment = "Small Business", Price = 150, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Online Travel Agent", Segment = "Corporate contract", Price = 160, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Online Travel Agent", Segment = "Families", Price = 125, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Online Travel Agent", Segment = "Afluent Mature Travelers", Price = 165, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Online Travel Agent", Segment = "International leisure travelers", Price = 175, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Online Travel Agent", Segment = "Corporate/Business Meetings", Price = 200, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Online Travel Agent", Segment = "Association Meetings", Price = 150, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Opaque", Segment = "Business", Price = 200, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Opaque", Segment = "Small Business", Price = 150, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Opaque", Segment = "Corporate contract", Price = 160, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Opaque", Segment = "Families", Price = 125, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Opaque", Segment = "Afluent Mature Travelers", Price = 165, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Opaque", Segment = "International leisure travelers", Price = 175, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Opaque", Segment = "Corporate/Business Meetings", Price = 200, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Opaque", Segment = "Association Meetings", Price = 150, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Direct", Segment = "Business", Price = 165, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Direct", Segment = "Small Business", Price = 125, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Direct", Segment = "Corporate contract", Price = 160, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Direct", Segment = "Families", Price = 150, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Direct", Segment = "Afluent Mature Travelers", Price = 200, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Direct", Segment = "International leisure travelers", Price = 175, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Direct", Segment = "Corporate/Business Meetings", Price = 165, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Direct", Segment = "Association Meetings", Price = 175, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Travel Agent", Segment = "Business", Price = 165, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Travel Agent", Segment = "Small Business", Price = 125, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Travel Agent", Segment = "Corporate contract", Price = 160, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Travel Agent", Segment = "Families", Price = 150, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Travel Agent", Segment = "Afluent Mature Travelers", Price = 200, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Travel Agent", Segment = "International leisure travelers", Price = 175, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Travel Agent", Segment = "Corporate/Business Meetings", Price = 165, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Travel Agent", Segment = "Association Meetings", Price = 175, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Online Travel Agent", Segment = "Business", Price = 165, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Online Travel Agent", Segment = "Small Business", Price = 125, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Online Travel Agent", Segment = "Corporate contract", Price = 160, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Online Travel Agent", Segment = "Families", Price = 150, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Online Travel Agent", Segment = "Afluent Mature Travelers", Price = 200, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Online Travel Agent", Segment = "International leisure travelers", Price = 175, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Online Travel Agent", Segment = "Corporate/Business Meetings", Price = 165, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Online Travel Agent", Segment = "Association Meetings", Price = 175, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Opaque", Segment = "Business", Price = 165, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Opaque", Segment = "Small Business", Price = 125, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Opaque", Segment = "Corporate contract", Price = 160, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Opaque", Segment = "Families", Price = 150, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Opaque", Segment = "Afluent Mature Travelers", Price = 200, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Opaque", Segment = "International leisure travelers", Price = 175, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Opaque", Segment = "Corporate/Business Meetings", Price = 165, ActualDemand = 0},
        new PriceDecisionPriceList { DistributionChannel = "Opaque", Segment = "Association Meetings", Price = 175, ActualDemand = 0}

    };
        return list;
    }
    public List<AttributeDecisionPriceList> AttributeDecisionPriceList()
    {
        var list = new List<AttributeDecisionPriceList>
        {
        new AttributeDecisionPriceList {  Attribute = "Spa", AccumulatedCapital = 0, NewCapital = 5000, OperationBudget = 14000, LaborBudget = 19700, QuarterForecast =0},
        new AttributeDecisionPriceList {  Attribute = "Fitness Center", AccumulatedCapital = 0, NewCapital = 1200, OperationBudget = 5500, LaborBudget = 3100, QuarterForecast =0},
        new AttributeDecisionPriceList {  Attribute = "Business Center", AccumulatedCapital = 0, NewCapital = 800, OperationBudget = 5500, LaborBudget = 3100, QuarterForecast =0},
        new AttributeDecisionPriceList {  Attribute = "Golf Course", AccumulatedCapital = 0, NewCapital = 32000, OperationBudget = 16000, LaborBudget = 31500, QuarterForecast =0},
        new AttributeDecisionPriceList {  Attribute = "Other Recreation Facilities - Pools, game rooms, tennis courts, ect", AccumulatedCapital = 0, NewCapital = 6000, OperationBudget = 8300, LaborBudget = 15700, QuarterForecast =0},
        new AttributeDecisionPriceList {  Attribute = "Management/Sales Attention", AccumulatedCapital = 0, NewCapital = 4000, OperationBudget = 6200, LaborBudget = 3900, QuarterForecast =0},
        new AttributeDecisionPriceList {  Attribute = "Resturants", AccumulatedCapital = 0, NewCapital = 10000, OperationBudget = 56000, LaborBudget = 111600, QuarterForecast =0},
        new AttributeDecisionPriceList {  Attribute = "Bars", AccumulatedCapital = 0, NewCapital = 5000, OperationBudget = 28000, LaborBudget = 55800, QuarterForecast =0},
        new AttributeDecisionPriceList {  Attribute = "Room Service", AccumulatedCapital = 0, NewCapital = 1000, OperationBudget = 14000, LaborBudget = 11100, QuarterForecast =0},
        new AttributeDecisionPriceList {  Attribute = "Banquet & Catering", AccumulatedCapital = 0, NewCapital = 1000, OperationBudget = 35000, LaborBudget = 40200, QuarterForecast =0},
        new AttributeDecisionPriceList {  Attribute = "Meeting Rooms", AccumulatedCapital = 0, NewCapital = 3000, OperationBudget = 7000, LaborBudget = 4400, QuarterForecast =0},
        new AttributeDecisionPriceList {  Attribute = "Entertainment", AccumulatedCapital = 0, NewCapital = 500, OperationBudget = 2800, LaborBudget = 5500, QuarterForecast =0},
        new AttributeDecisionPriceList {  Attribute = "Courtesy(FB)", AccumulatedCapital = 0, NewCapital = 500, OperationBudget = 12500, LaborBudget = 6500, QuarterForecast =0},
        new AttributeDecisionPriceList {  Attribute = "Guest Rooms", AccumulatedCapital = 0, NewCapital = 48000, OperationBudget = 8800, LaborBudget = 15000, QuarterForecast =0},
        new AttributeDecisionPriceList {  Attribute = "Reservations", AccumulatedCapital = 0, NewCapital = 5000, OperationBudget = 13000, LaborBudget = 9000, QuarterForecast =0},
        new AttributeDecisionPriceList {  Attribute = "Guest Check in/Guest Check out", AccumulatedCapital = 0, NewCapital = 4000, OperationBudget = 18000, LaborBudget = 31600, QuarterForecast =0},
        new AttributeDecisionPriceList {  Attribute = "Concierge", AccumulatedCapital = 0, NewCapital = 1000, OperationBudget = 9000, LaborBudget = 6000, QuarterForecast =0},
        new AttributeDecisionPriceList {  Attribute = "Housekeeping", AccumulatedCapital = 0, NewCapital = 5000, OperationBudget = 40000, LaborBudget = 88900, QuarterForecast =0},
        new AttributeDecisionPriceList {  Attribute = "Maintanence and security", AccumulatedCapital = 0, NewCapital = 4000, OperationBudget = 28500, LaborBudget = 31800, QuarterForecast =0},
        new AttributeDecisionPriceList {  Attribute = "Courtesy (Rooms)", AccumulatedCapital = 0, NewCapital = 72000, OperationBudget = 12200, LaborBudget = 13600, QuarterForecast =0},

    };
        return list;
    }
    public List<RoomAllocationPriceList> RoomAllocationPriceList()
    {
        var list = new List<RoomAllocationPriceList> {

        new RoomAllocationPriceList { Segment= "Business",                           WeekDay=true,RoomsAllocated= 1632,   ActualDemand= 0,RoomsSold= 0,    Revenue= 0, QuarterForecast=0 },
        new RoomAllocationPriceList { Segment=  "Small Business",                    WeekDay=true,RoomsAllocated=1530,    ActualDemand=0, RoomsSold=0,     Revenue=0, QuarterForecast=    0},
        new RoomAllocationPriceList { Segment = "Corporate contract",                WeekDay=true,RoomsAllocated=1717,    ActualDemand=0, RoomsSold=0,     Revenue=0, QuarterForecast=    0},
        new RoomAllocationPriceList { Segment = "Families",                          WeekDay=true,RoomsAllocated=357,     ActualDemand=0, RoomsSold=0,     Revenue=0, QuarterForecast=    0},
        new RoomAllocationPriceList { Segment = "Afluent Mature Travelers",          WeekDay=true,RoomsAllocated=544,     ActualDemand=0, RoomsSold=0,     Revenue=0, QuarterForecast=    0},
        new RoomAllocationPriceList { Segment = "International leisure travelers",   WeekDay=true,RoomsAllocated=901,     ActualDemand=0, RoomsSold=0,     Revenue=0, QuarterForecast=    0},
        new RoomAllocationPriceList { Segment = "Corporate/Business Meetings",       WeekDay=true,RoomsAllocated=1445,    ActualDemand=0, RoomsSold=0,     Revenue=0, QuarterForecast=    0},
        new RoomAllocationPriceList { Segment = "Association Meetings",              WeekDay=true,RoomsAllocated=357,     ActualDemand=0, RoomsSold=0,     Revenue=0, QuarterForecast=    0},
        new RoomAllocationPriceList { Segment = "Business",                          WeekDay=false,RoomsAllocated=180,     ActualDemand=0, RoomsSold=0,     Revenue=0, QuarterForecast=    0},
        new RoomAllocationPriceList { Segment = "Small Business",                    WeekDay=false,RoomsAllocated=276,     ActualDemand=0, RoomsSold=0,     Revenue=0, QuarterForecast=    0},
        new RoomAllocationPriceList { Segment = "Corporate contract",                WeekDay=false,RoomsAllocated=96,      ActualDemand=0, RoomsSold=0,     Revenue=0, QuarterForecast=    0},
        new RoomAllocationPriceList { Segment = "Families",                          WeekDay=false,RoomsAllocated=1452,    ActualDemand=0, RoomsSold=0,     Revenue=0, QuarterForecast=    0},
        new RoomAllocationPriceList { Segment = "Afluent Mature Travelers",          WeekDay=false,RoomsAllocated=1272,    ActualDemand=0, RoomsSold=0,     Revenue=0, QuarterForecast=    0},
        new RoomAllocationPriceList { Segment = "International leisure travelers",   WeekDay=false,RoomsAllocated=912,     ActualDemand=0, RoomsSold=0,     Revenue=0, QuarterForecast=    0},
        new RoomAllocationPriceList { Segment = "Corporate/Business Meetings",       WeekDay=false,RoomsAllocated=360,     ActualDemand=0, RoomsSold=0,     Revenue=0, QuarterForecast=    0},
        new RoomAllocationPriceList { Segment = "Association Meetings",              WeekDay=false,RoomsAllocated = 1452, ActualDemand = 0, RoomsSold = 0, Revenue = 0, QuarterForecast  = 0 }

    };
        return list;
    }
}