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
}