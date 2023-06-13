using Database.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Microsoft.IdentityModel.Tokens;
//using EFCore.BulkExtensions;
using Mapster;

using ZstdNet;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Asn1.Cms;
using Mysqlx.Expr;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using EFCore.BulkExtensions;

namespace Service
{
    public class FunMonth
    {
        public ClassSessionDto GetClassDetailsById(int classID, HotelDbContext context)
        {
            IQueryable<ClassSession> query = context.ClassSessions;
            if (classID > 0)
            {
                query = query.Where(x => x.ClassId == classID);
            }
            var result = query.Select(x => new ClassSessionDto
            {

                ClassId = x.ClassId,
                CurrentQuater = x.CurrentQuater,
                HotelsCount = x.HotelsCount
            }).ToList();
            ClassSessionDto obj = new ClassSessionDto();
            obj = result[0];
            return obj;

        }
        public int CreateMonth(HotelDbContext context, int classID, int currentQuarter, int totalMarket,bool isCompleted)
        {
            IQueryable<Month> query = context.Months;
            var obj = new Month() { ClassId = classID, Sequence = currentQuarter + 1, TotalMarket = totalMarket, ConfigId = 1, IsComplete = isCompleted };
            context.Months.Add(obj);
            int status = context.SaveChanges();
            int monthID = obj.MonthId;

            return monthID;
        }
        /*
        public async Task<int> CreateMarketingDecision1(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels)
        {
            int index = 1;
            int groupID = index;

            try
            {
                //using (var context1 = new HotelDbContext())
                //{


                IQueryable<MarketingDecision> query = context.MarketingDecision;
                List<MarketingDecision> lstinst = new List<MarketingDecision>();

                while (index < noOfHotels + 1)
                {
                    //var list = new List<MarketingDecision> {
                    var obj1 = new MarketingDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Advertising", Segment = "Business", Spending = 3300, LaborSpending = 1400, ActualDemand = 0, Confirmed = false };
                    var obj2 = new MarketingDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Advertising", Segment = "Small Business", Spending = 3300, LaborSpending = 1400, ActualDemand = 0, Confirmed = false };
                    var obj3 = new MarketingDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Advertising", Segment = "Corporate contract", Spending = 550, LaborSpending = 240, ActualDemand = 0, Confirmed = false };
                    var obj4 = new MarketingDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Advertising", Segment = "Families", Spending = 5500, LaborSpending = 2400, ActualDemand = 0, Confirmed = false };
                    var obj5 = new MarketingDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Advertising", Segment = "Afluent Mature Travelers", Spending = 7700, LaborSpending = 3300, ActualDemand = 0, Confirmed = false };
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Advertising", Segment = "International leisure travelers", Spending = 7100, LaborSpending = 3000, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Advertising", Segment = "Corporate/Business Meetings", Spending = 550, LaborSpending = 240, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Advertising", Segment = "Association Meetings", Spending = 550, LaborSpending = 240, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Sales Force", Segment = "Business", Spending = 3120, LaborSpending = 4700, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Sales Force", Segment = "Small Business", Spending = 1900, LaborSpending = 2800, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Sales Force", Segment = "Corporate contract", Spending = 5640, LaborSpending = 8500, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Sales Force", Segment = "Families", Spending = 0, LaborSpending = 0, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Sales Force", Segment = "Afluent Mature Travelers", Spending = 0, LaborSpending = 0, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Sales Force", Segment = "International leisure travelers", Spending = 0, LaborSpending = 0, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Sales Force", Segment = "Corporate/Business Meetings", Spending = 5640, LaborSpending = 8500, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Sales Force", Segment = "Association Meetings", Spending = 5000, LaborSpending = 7500, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Promotions", Segment = "Business", Spending = 550, LaborSpending = 220, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Promotions", Segment = "Small Business", Spending = 3300, LaborSpending = 1400, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Promotions", Segment = "Corporate contract", Spending = 0, LaborSpending = 0, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Promotions", Segment = "Families", Spending = 3300, LaborSpending = 1400, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Promotions", Segment = "Afluent Mature Travelers", Spending = 1100, LaborSpending = 460, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Promotions", Segment = "International leisure travelers", Spending = 1100, LaborSpending = 470, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Promotions", Segment = "Corporate/Business Meetings", Spending = 0, LaborSpending = 0, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Promotions", Segment = "Association Meetings", Spending = 0, LaborSpending = 0, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Public Relations", Segment = "Business", Spending = 1180, LaborSpending = 1200, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Public Relations", Segment = "Small Business", Spending = 800, LaborSpending = 800, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Public Relations", Segment = "Corporate contract", Spending = 400, LaborSpending = 400, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Public Relations", Segment = "Families", Spending = 1600, LaborSpending = 1560, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Public Relations", Segment = "Afluent Mature Travelers", Spending = 1600, LaborSpending = 1560, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Public Relations", Segment = "International leisure travelers", Spending = 1960, LaborSpending = 1960, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Public Relations", Segment = "Corporate/Business Meetings", Spending = 400, LaborSpending = 400, ActualDemand = 0, Confirmed = false },
                    //new MarketingDecision { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Public Relations", Segment = "Association Meetings", Spending = 1180, LaborSpending = 1200, ActualDemand = 0, Confirmed = false }
                    // };



                    // context.MarketingDecision.Add(new MarketingDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, MarketingTechniques = "Public Relations", Segment = "Association Meetings", Spending = 1180, LaborSpending = 1200, ActualDemand = 0, Confirmed = false });

                    context.MarketingDecision.Add(obj1.Adapt<MarketingDecision>());
                    context.SaveChanges();
                    context.MarketingDecision.Add(obj2.Adapt<MarketingDecision>());
                    context.SaveChanges();

                    context.MarketingDecision.Add(obj3.Adapt<MarketingDecision>());
                    context.SaveChanges();
                    context.MarketingDecision.Add(obj4.Adapt<MarketingDecision>());
                    context.SaveChanges();
                    //context.MarketingDecision.Add(obj5);
                    //context.SaveChanges();
                    //context.MarketingDecision.Add(obj6);
                    //context.SaveChanges();
                    //context.MarketingDecision.Add(obj7);
                    //context.MarketingDecision.Add(obj8);
                    //context.MarketingDecision.Add(obj9);
                    //context.MarketingDecision.Add(obj10);
                    //context.MarketingDecision.Add(obj11);
                    //context.MarketingDecision.Add(obj12);
                    //context.MarketingDecision.Add(obj13);
                    //context.MarketingDecision.Add(obj14);
                    //context.MarketingDecision.Add(obj15);
                    //context.MarketingDecision.Add(obj16);
                    //context.MarketingDecision.Add(obj17);
                    //context.MarketingDecision.Add(obj18);
                    //context.MarketingDecision.Add(obj19);
                    //context.MarketingDecision.Add(obj20);
                    //context.MarketingDecision.Add(obj21);
                    //context.MarketingDecision.Add(obj22);
                    //context.MarketingDecision.Add(obj23);
                    //context.MarketingDecision.Add(obj24);
                    //context.MarketingDecision.Add(obj25);
                    //context.MarketingDecision.Add(obj26);
                    //context.MarketingDecision.Add(obj27);
                    //context.MarketingDecision.Add(obj28);
                    //context.MarketingDecision.Add(obj29);
                    //context.MarketingDecision.Add(obj30);
                    //context.MarketingDecision.Add(obj31);
                    //context.MarketingDecision.Add(obj32);
                    //int status = context.SaveChanges();
                    index++;

                }
                var lstAdapt = lstinst;
                context.AddRange(lstAdapt);

                context.SaveChanges();

                //}
            }
            catch (Exception ex)
            {
                string exp = ex.ToString();
            }


            return 1;
        }

        */

        public async Task<int> CreateMarketingDecision(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels)
        {
            int index = 1;
            int groupID = index;

            try
            {
                List<SegmentDto> lstSegment = GetSegment(context);
                List<MarketingTechniquesDto> lstmarketingTechniques = GetMarketingTechniques(context);
                for (int i = 1; i <= noOfHotels; i++)
                {
                    index = i;
                    foreach (SegmentDto segment in lstSegment)
                    {

                        var data = lstmarketingTechniques.Select(mktTech =>
                        {
                            MarketDecisionPriceList obj = GetMarketDecisionPriceList(mktTech.Techniques.Trim(), segment.SegmentName.Trim());
                            return new MarketingDecision()
                            {
                                // ID = Random.Shared.Next(100),
                                MonthID = monthID,
                                QuarterNo = currentQuarter + 1,
                                GroupID = groupID,
                                MarketingTechniques = mktTech.Techniques,
                                Segment = segment.SegmentName,
                                Spending = (int)obj.Spending,
                                LaborSpending = (int)obj.LaborSpending,
                                ActualDemand = (int)obj.ActualDemand,
                                Confirmed = false
                            };
                        }).ToList();

                        foreach (var marketingTechnique in data)
                        {
                            context.Entry(marketingTechnique).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        // await context.MarketingDecision.AddRangeAsync(data);
                        var cahnge2s = context.ChangeTracker.Entries().Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added);
                        int affected = await context.SaveChangesAsync();

                    }
                    var cahnges = context.ChangeTracker.Entries().Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added);
                }
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                string exp = ex.ToString();
            }


            return 1;
        }

        public async Task<int> CreatePriceDecision(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels)
        {

            // Not underStand for Old Project Run Two Time Insert 


            List<SegmentDto> lstSegment = GetSegment(context);

            List<DistributionChannelsDto> lstChannel = GetDistributionChannels(context);

            for (int i = 1; i <= noOfHotels; i++)
            {
                int index = 1;
                foreach (SegmentDto segment in lstSegment)
                {
                    foreach (DistributionChannelsDto channel in lstChannel)
                    {

                        PriceDecisionPriceList obj = GetPriceDecisionPriceList(channel.Channel.Trim(), segment.SegmentName.Trim(), index);
                        context.PriceDecision.Add(new PriceDecision
                        {
                            MonthID = monthID,
                            QuarterNo = currentQuarter + 1,
                            GroupID = i.ToString(),
                            Weekday = true,
                            DistributionChannel = channel.Channel,
                            Segment = segment.SegmentName,
                            Price = (int)obj.Price,
                            ActualDemand = (int)obj.ActualDemand,
                            Confirmed = false
                        });
                        //int sat = await context.SaveChangesAsync();
                        // context.PriceDecision.Add(obj1);
                        index++;
                    }

                }

                context.SaveChanges();

            }
            return 1;
        }
        /*
        public int CreatePriceDecision1(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels)
        {
            int index = 1;
            int groupID = index;
            while (index < noOfHotels + 1)
            {

                var obj1 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Direct", Segment = "Business", Price = 200, ActualDemand = 0, Confirmed = false };
                var obj2 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Direct", Segment = "Corporate contract", Price = 160, ActualDemand = 0, Confirmed = false };
                var obj3 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Direct", Segment = "Families", Price = 125, ActualDemand = 0, Confirmed = false };
                var obj4 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Direct", Segment = "Afluent Mature Travelers", Price = 165, ActualDemand = 0, Confirmed = false };
                var obj5 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Direct", Segment = "International leisure travelers", Price = 175, ActualDemand = 0, Confirmed = false };
                var obj6 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Direct", Segment = "Corporate/Business Meetings", Price = 200, ActualDemand = 0, Confirmed = false };
                var obj7 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Direct", Segment = "Association Meetings", Price = 150, ActualDemand = 0, Confirmed = false };
                var obj8 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Travel Agent", Segment = "Business", Price = 200, ActualDemand = 0, Confirmed = false };
                var obj9 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Travel Agent", Segment = "Small Business", Price = 150, ActualDemand = 0, Confirmed = false };
                var obj10 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Travel Agent", Segment = "Corporate contract", Price = 160, ActualDemand = 0, Confirmed = false };
                var obj11 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Travel Agent", Segment = "Families", Price = 125, ActualDemand = 0, Confirmed = false };
                var obj12 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Travel Agent", Segment = "Afluent Mature Travelers", Price = 165, ActualDemand = 0, Confirmed = false };
                var obj13 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Travel Agent", Segment = "International leisure travelers", Price = 175, ActualDemand = 0, Confirmed = false };
                var obj14 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Travel Agent", Segment = "Corporate/Business Meetings", Price = 200, ActualDemand = 0, Confirmed = false };
                var obj15 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Travel Agent", Segment = "Association Meetings", Price = 150, ActualDemand = 0, Confirmed = false };
                var obj16 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Online Travel Agent", Segment = "Business", Price = 200, ActualDemand = 0, Confirmed = false };
                var obj17 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Online Travel Agent", Segment = "Small Business", Price = 150, ActualDemand = 0, Confirmed = false };
                var obj18 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Online Travel Agent", Segment = "Corporate contract", Price = 160, ActualDemand = 0, Confirmed = false };
                var obj19 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Online Travel Agent", Segment = "Families", Price = 125, ActualDemand = 0, Confirmed = false };
                var obj20 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Online Travel Agent", Segment = "Afluent Mature Travelers", Price = 165, ActualDemand = 0, Confirmed = false };
                var obj21 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Online Travel Agent", Segment = "International leisure travelers", Price = 175, ActualDemand = 0, Confirmed = false };
                var obj22 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Online Travel Agent", Segment = "Corporate/Business Meetings", Price = 200, ActualDemand = 0, Confirmed = false };
                var obj23 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Online Travel Agent", Segment = "Association Meetings", Price = 150, ActualDemand = 0, Confirmed = false };
                var obj24 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Opaque", Segment = "Business", Price = 200, ActualDemand = 0, Confirmed = false };
                var obj25 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Opaque", Segment = "Small Business", Price = 150, ActualDemand = 0, Confirmed = false };
                var obj26 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Opaque", Segment = "Corporate contract", Price = 160, ActualDemand = 0, Confirmed = false };
                var obj27 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Opaque", Segment = "Families", Price = 125, ActualDemand = 0, Confirmed = false };
                var obj28 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Opaque", Segment = "Afluent Mature Travelers", Price = 165, ActualDemand = 0, Confirmed = false };
                var obj29 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Opaque", Segment = "International leisure travelers", Price = 175, ActualDemand = 0, Confirmed = false };
                var obj30 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Opaque", Segment = "Corporate/Business Meetings", Price = 200, ActualDemand = 0, Confirmed = false };
                var obj31 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = true, DistributionChannel = "Opaque", Segment = "Association Meetings", Price = 150, ActualDemand = 0, Confirmed = false };
                var obj32 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Direct", Segment = "Business", Price = 165, ActualDemand = 0, Confirmed = false };
                var obj33 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Direct", Segment = "Small Business", Price = 125, ActualDemand = 0, Confirmed = false };
                var obj34 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Direct", Segment = "Corporate contract", Price = 160, ActualDemand = 0, Confirmed = false };
                var obj35 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Direct", Segment = "Families", Price = 150, ActualDemand = 0, Confirmed = false };
                var obj36 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Direct", Segment = "Afluent Mature Travelers", Price = 200, ActualDemand = 0, Confirmed = false };
                var obj37 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Direct", Segment = "International leisure travelers", Price = 175, ActualDemand = 0, Confirmed = false };
                var obj38 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Direct", Segment = "Corporate/Business Meetings", Price = 165, ActualDemand = 0, Confirmed = false };
                var obj39 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Direct", Segment = "Association Meetings", Price = 175, ActualDemand = 0, Confirmed = false };
                var obj40 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Travel Agent", Segment = "Business", Price = 165, ActualDemand = 0, Confirmed = false };
                var obj41 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Travel Agent", Segment = "Small Business", Price = 125, ActualDemand = 0, Confirmed = false };
                var obj42 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Travel Agent", Segment = "Corporate contract", Price = 160, ActualDemand = 0, Confirmed = false };
                var obj43 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Travel Agent", Segment = "Families", Price = 150, ActualDemand = 0, Confirmed = false };
                var obj44 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Travel Agent", Segment = "Afluent Mature Travelers", Price = 200, ActualDemand = 0, Confirmed = false };
                var obj45 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Travel Agent", Segment = "International leisure travelers", Price = 175, ActualDemand = 0, Confirmed = false };
                var obj46 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Travel Agent", Segment = "Corporate/Business Meetings", Price = 165, ActualDemand = 0, Confirmed = false };
                var obj47 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Travel Agent", Segment = "Association Meetings", Price = 175, ActualDemand = 0, Confirmed = false };
                var obj48 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Online Travel Agent", Segment = "Business", Price = 165, ActualDemand = 0, Confirmed = false };
                var obj49 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Online Travel Agent", Segment = "Small Business", Price = 125, ActualDemand = 0, Confirmed = false };
                var obj50 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Online Travel Agent", Segment = "Corporate contract", Price = 160, ActualDemand = 0, Confirmed = false };
                var obj51 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Online Travel Agent", Segment = "Families", Price = 150, ActualDemand = 0, Confirmed = false };
                var obj52 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Online Travel Agent", Segment = "Afluent Mature Travelers", Price = 200, ActualDemand = 0, Confirmed = false };
                var obj53 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Online Travel Agent", Segment = "International leisure travelers", Price = 175, ActualDemand = 0, Confirmed = false };
                var obj54 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Online Travel Agent", Segment = "Corporate/Business Meetings", Price = 165, ActualDemand = 0, Confirmed = false };
                var obj55 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Online Travel Agent", Segment = "Association Meetings", Price = 175, ActualDemand = 0, Confirmed = false };
                var obj56 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Opaque", Segment = "Business", Price = 165, ActualDemand = 0, Confirmed = false };
                var obj57 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Opaque", Segment = "Small Business", Price = 125, ActualDemand = 0, Confirmed = false };
                var obj58 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Opaque", Segment = "Corporate contract", Price = 160, ActualDemand = 0, Confirmed = false };
                var obj59 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Opaque", Segment = "Families", Price = 150, ActualDemand = 0, Confirmed = false };
                var obj60 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Opaque", Segment = "Afluent Mature Travelers", Price = 200, ActualDemand = 0, Confirmed = false };
                var obj61 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Opaque", Segment = "International leisure travelers", Price = 175, ActualDemand = 0, Confirmed = false };
                var obj62 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Opaque", Segment = "Corporate/Business Meetings", Price = 165, ActualDemand = 0, Confirmed = false };
                var obj63 = new PriceDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID.ToString(), Weekday = false, DistributionChannel = "Opaque", Segment = "Association Meetings", Price = 175, ActualDemand = 0, Confirmed = false };
                context.PriceDecision.Add(obj1);
                context.PriceDecision.Add(obj2);
                context.PriceDecision.Add(obj3);
                context.PriceDecision.Add(obj4);
                context.PriceDecision.Add(obj5);
                context.PriceDecision.Add(obj6);
                context.PriceDecision.Add(obj7);
                context.PriceDecision.Add(obj8);
                context.PriceDecision.Add(obj9);
                context.PriceDecision.Add(obj10);
                context.PriceDecision.Add(obj11);
                context.PriceDecision.Add(obj12);
                context.PriceDecision.Add(obj13);
                context.PriceDecision.Add(obj14);
                context.PriceDecision.Add(obj15);
                context.PriceDecision.Add(obj16);
                context.PriceDecision.Add(obj17);
                context.PriceDecision.Add(obj18);
                context.PriceDecision.Add(obj19);
                context.PriceDecision.Add(obj20);
                context.PriceDecision.Add(obj21);
                context.PriceDecision.Add(obj22);
                context.PriceDecision.Add(obj23);
                context.PriceDecision.Add(obj24);
                context.PriceDecision.Add(obj25);
                context.PriceDecision.Add(obj26);
                context.PriceDecision.Add(obj27);
                context.PriceDecision.Add(obj28);
                context.PriceDecision.Add(obj29);
                context.PriceDecision.Add(obj30);
                context.PriceDecision.Add(obj31);
                context.PriceDecision.Add(obj32);
                context.PriceDecision.Add(obj33);
                context.PriceDecision.Add(obj34);
                context.PriceDecision.Add(obj35);
                context.PriceDecision.Add(obj36);
                context.PriceDecision.Add(obj37);
                context.PriceDecision.Add(obj38);
                context.PriceDecision.Add(obj39);
                context.PriceDecision.Add(obj40);
                context.PriceDecision.Add(obj41);
                context.PriceDecision.Add(obj42);
                context.PriceDecision.Add(obj43);
                context.PriceDecision.Add(obj44);
                context.PriceDecision.Add(obj45);
                context.PriceDecision.Add(obj46);
                context.PriceDecision.Add(obj47);
                context.PriceDecision.Add(obj48);
                context.PriceDecision.Add(obj49);
                context.PriceDecision.Add(obj50);
                context.PriceDecision.Add(obj51);
                context.PriceDecision.Add(obj52);
                context.PriceDecision.Add(obj53);
                context.PriceDecision.Add(obj54);
                context.PriceDecision.Add(obj55);
                context.PriceDecision.Add(obj56);
                context.PriceDecision.Add(obj57);
                context.PriceDecision.Add(obj58);
                context.PriceDecision.Add(obj59);
                context.PriceDecision.Add(obj60);
                context.PriceDecision.Add(obj61);
                context.PriceDecision.Add(obj62);
                context.PriceDecision.Add(obj63);
                int status = context.SaveChanges();
                index++;
            }
            return 1;
        }
        */
        public int CreateAttributeDecision(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels)
        {

            List<AttributeDto> lstAttribute = GetAttribute(context);
            for (int i = 1; i <= noOfHotels; i++)
            {
                foreach (var item in lstAttribute)
                {
                    int groupID = i;
                    var datafi = GetDataBySingleRowAttributeDecision(context, groupID, monthID, currentQuarter, item.AttributeName.Trim());
                    decimal accumuCapital = ScalarQueryInitialCapitalInvestAttributeConfig(context, monthID, currentQuarter, item.AttributeName);
                    AttributeDecisionPriceList AttPlist = GetAttributeDecisionPriceList(item.AttributeName.Trim());
                    var obj1 = new AttributeDecision()
                    {
                        MonthID = monthID,
                        QuarterNo = currentQuarter + 1,
                        GroupID = groupID,
                        Attribute = item.AttributeName,
                        AccumulatedCapital = (int)accumuCapital,
                        NewCapital = AttPlist.NewCapital,
                        OperationBudget = AttPlist.OperationBudget,
                        LaborBudget = AttPlist.LaborBudget,
                        Confirmed = false,
                        QuarterForecast = currentQuarter
                    };
                    context.AttributeDecision.Add(obj1);
                }

                int status = context.SaveChanges();
            }
            return 1;
        }
        /*
        public int CreateAttributeDecision1(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels)
        {

            List<AttributeDto> lstAttribute = GetAttribute(context);
            for (int i = 1; i <= noOfHotels; i++)
            {
                int groupID = i;
                int accumuCapital = 0;

                var obj1 = new AttributeDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Attribute = "Spa", AccumulatedCapital = accumuCapital, NewCapital = 5000, OperationBudget = 14000, LaborBudget = 19700, Confirmed = false, QuarterForecast = currentQuarter };
                var obj2 = new AttributeDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Attribute = "Fitness Center", AccumulatedCapital = accumuCapital, NewCapital = 1200, OperationBudget = 5500, LaborBudget = 3100, Confirmed = false, QuarterForecast = currentQuarter };
                var obj3 = new AttributeDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Attribute = "Business Center", AccumulatedCapital = accumuCapital, NewCapital = 800, OperationBudget = 5500, LaborBudget = 3100, Confirmed = false, QuarterForecast = currentQuarter };
                var obj4 = new AttributeDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Attribute = "Golf Course", AccumulatedCapital = accumuCapital, NewCapital = 32000, OperationBudget = 16000, LaborBudget = 31500, Confirmed = false, QuarterForecast = currentQuarter };
                var obj5 = new AttributeDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Attribute = "Other Recreation Facilities - Pools, game rooms, tennis courts, ect", AccumulatedCapital = accumuCapital, NewCapital = 6000, OperationBudget = 8300, LaborBudget = 15700, Confirmed = false, QuarterForecast = currentQuarter };
                var obj6 = new AttributeDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Attribute = "Management/Sales Attention", AccumulatedCapital = accumuCapital, NewCapital = 4000, OperationBudget = 6200, LaborBudget = 3900, Confirmed = false, QuarterForecast = currentQuarter };
                var obj7 = new AttributeDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Attribute = "Resturants", AccumulatedCapital = accumuCapital, NewCapital = 10000, OperationBudget = 56000, LaborBudget = 111600, Confirmed = false, QuarterForecast = currentQuarter };
                var obj8 = new AttributeDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Attribute = "Bars", AccumulatedCapital = accumuCapital, NewCapital = 5000, OperationBudget = 28000, LaborBudget = 55800, Confirmed = false, QuarterForecast = currentQuarter };
                var obj9 = new AttributeDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Attribute = "Room Service", AccumulatedCapital = accumuCapital, NewCapital = 1000, OperationBudget = 14000, LaborBudget = 11100, Confirmed = false, QuarterForecast = currentQuarter };
                var obj10 = new AttributeDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Attribute = "Banquet & Catering", AccumulatedCapital = accumuCapital, NewCapital = 1000, OperationBudget = 35000, LaborBudget = 40200, Confirmed = false, QuarterForecast = currentQuarter };
                var obj11 = new AttributeDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Attribute = "Meeting Rooms", AccumulatedCapital = accumuCapital, NewCapital = 3000, OperationBudget = 7000, LaborBudget = 4400, Confirmed = false, QuarterForecast = currentQuarter };
                var obj12 = new AttributeDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Attribute = "Entertainment", AccumulatedCapital = accumuCapital, NewCapital = 500, OperationBudget = 2800, LaborBudget = 5500, Confirmed = false, QuarterForecast = currentQuarter };
                var obj13 = new AttributeDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Attribute = "Courtesy(FB)", AccumulatedCapital = accumuCapital, NewCapital = 500, OperationBudget = 12500, LaborBudget = 6500, Confirmed = false, QuarterForecast = currentQuarter };
                var obj14 = new AttributeDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Attribute = "Guest Rooms", AccumulatedCapital = accumuCapital, NewCapital = 48000, OperationBudget = 8800, LaborBudget = 15000, Confirmed = false, QuarterForecast = currentQuarter };
                var obj15 = new AttributeDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Attribute = "Reservations", AccumulatedCapital = accumuCapital, NewCapital = 5000, OperationBudget = 13000, LaborBudget = 9000, Confirmed = false, QuarterForecast = currentQuarter };
                var obj16 = new AttributeDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Attribute = "Guest Check in/Guest Check out", AccumulatedCapital = accumuCapital, NewCapital = 4000, OperationBudget = 18000, LaborBudget = 31600, Confirmed = false, QuarterForecast = currentQuarter };
                var obj17 = new AttributeDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Attribute = "Concierge", AccumulatedCapital = accumuCapital, NewCapital = 1000, OperationBudget = 9000, LaborBudget = 6000, Confirmed = false, QuarterForecast = currentQuarter };
                var obj18 = new AttributeDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Attribute = "Housekeeping", AccumulatedCapital = accumuCapital, NewCapital = 5000, OperationBudget = 40000, LaborBudget = 88900, Confirmed = false, QuarterForecast = currentQuarter };
                var obj19 = new AttributeDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Attribute = "Maintanence and security", AccumulatedCapital = accumuCapital, NewCapital = 4000, OperationBudget = 28500, LaborBudget = 31800, Confirmed = false, QuarterForecast = currentQuarter };
                var obj20 = new AttributeDecision() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Attribute = "Courtesy (Rooms)", AccumulatedCapital = accumuCapital, NewCapital = 72000, OperationBudget = 12200, LaborBudget = 13600, Confirmed = false, QuarterForecast = currentQuarter };

                context.AttributeDecision.Add(obj1);
                context.AttributeDecision.Add(obj2);
                context.AttributeDecision.Add(obj3);
                context.AttributeDecision.Add(obj4);
                context.AttributeDecision.Add(obj5);
                context.AttributeDecision.Add(obj6);
                context.AttributeDecision.Add(obj7);
                context.AttributeDecision.Add(obj8);
                context.AttributeDecision.Add(obj9);
                context.AttributeDecision.Add(obj10);
                context.AttributeDecision.Add(obj11);
                context.AttributeDecision.Add(obj12);
                context.AttributeDecision.Add(obj13);
                context.AttributeDecision.Add(obj14);
                context.AttributeDecision.Add(obj15);
                context.AttributeDecision.Add(obj16);
                context.AttributeDecision.Add(obj17);
                context.AttributeDecision.Add(obj18);
                context.AttributeDecision.Add(obj19);
                context.AttributeDecision.Add(obj20);

                int status = context.SaveChanges();
            }
            return 1;
        }
        

        public int CreateRoomAllocation1(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels)
        {
            int index = 1;
            int groupID = index;
            while (index < noOfHotels + 1)
            {

                int accumuCapital = 0;
                IQueryable<RoomAllocation> query = context.RoomAllocation;
                var obj1 = new RoomAllocation() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Weekday = true, Segment = "Business", RoomsAllocated = 1632, ActualDemand = 0, RoomsSold = 0, Confirmed = false, Revenue = 0, QuarterForecast = currentQuarter };
                var obj2 = new RoomAllocation() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Weekday = true, Segment = "Small Business", RoomsAllocated = 1530, ActualDemand = 0, RoomsSold = 0, Confirmed = false, Revenue = 0, QuarterForecast = currentQuarter };
                var obj3 = new RoomAllocation() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Weekday = true, Segment = "Corporate contract", RoomsAllocated = 1717, ActualDemand = 0, RoomsSold = 0, Confirmed = false, Revenue = 0, QuarterForecast = currentQuarter };
                var obj4 = new RoomAllocation() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Weekday = true, Segment = "Families", RoomsAllocated = 357, ActualDemand = 0, RoomsSold = 0, Confirmed = false, Revenue = 0, QuarterForecast = currentQuarter };
                var obj5 = new RoomAllocation() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Weekday = true, Segment = "Afluent Mature Travelers", RoomsAllocated = 544, ActualDemand = 0, RoomsSold = 0, Confirmed = false, Revenue = 0, QuarterForecast = currentQuarter };
                var obj6 = new RoomAllocation() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Weekday = true, Segment = "International leisure travelers", RoomsAllocated = 901, ActualDemand = 0, RoomsSold = 0, Confirmed = false, Revenue = 0, QuarterForecast = currentQuarter };
                var obj7 = new RoomAllocation() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Weekday = true, Segment = "Corporate/Business Meetings", RoomsAllocated = 1445, ActualDemand = 0, RoomsSold = 0, Confirmed = false, Revenue = 0, QuarterForecast = currentQuarter };
                var obj8 = new RoomAllocation() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Weekday = true, Segment = "Association Meetings", RoomsAllocated = 357, ActualDemand = 0, RoomsSold = 0, Confirmed = false, Revenue = 0, QuarterForecast = currentQuarter };
                var obj9 = new RoomAllocation() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Weekday = false, Segment = "Business", RoomsAllocated = 180, ActualDemand = 0, RoomsSold = 0, Confirmed = false, Revenue = 0, QuarterForecast = currentQuarter };
                var obj10 = new RoomAllocation() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Weekday = false, Segment = "Small Business", RoomsAllocated = 276, ActualDemand = 0, RoomsSold = 0, Confirmed = false, Revenue = 0, QuarterForecast = currentQuarter };
                var obj11 = new RoomAllocation() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Weekday = false, Segment = "Corporate contract", RoomsAllocated = 96, ActualDemand = 0, RoomsSold = 0, Confirmed = false, Revenue = 0, QuarterForecast = currentQuarter };
                var obj12 = new RoomAllocation() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Weekday = false, Segment = "Families", RoomsAllocated = 1452, ActualDemand = 0, RoomsSold = 0, Confirmed = false, Revenue = 0, QuarterForecast = currentQuarter };
                var obj13 = new RoomAllocation() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Weekday = false, Segment = "Afluent Mature Travelers", RoomsAllocated = 1272, ActualDemand = 0, RoomsSold = 0, Confirmed = false, Revenue = 0, QuarterForecast = currentQuarter };
                var obj14 = new RoomAllocation() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Weekday = false, Segment = "International leisure travelers", RoomsAllocated = 912, ActualDemand = 0, RoomsSold = 0, Confirmed = false, Revenue = 0, QuarterForecast = currentQuarter };
                var obj15 = new RoomAllocation() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Weekday = false, Segment = "Corporate/Business Meetings", RoomsAllocated = 360, ActualDemand = 0, RoomsSold = 0, Confirmed = false, Revenue = 0, QuarterForecast = currentQuarter };
                var obj16 = new RoomAllocation() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Weekday = false, Segment = "Association Meetings", RoomsAllocated = 1452, ActualDemand = 0, RoomsSold = 0, Confirmed = false, Revenue = 0, QuarterForecast = currentQuarter };
                context.RoomAllocation.Add(obj1);
                context.RoomAllocation.Add(obj2);
                context.RoomAllocation.Add(obj3);
                context.RoomAllocation.Add(obj4);
                context.RoomAllocation.Add(obj5);
                context.RoomAllocation.Add(obj6);
                context.RoomAllocation.Add(obj7);
                context.RoomAllocation.Add(obj8);
                context.RoomAllocation.Add(obj9);
                context.RoomAllocation.Add(obj10);
                context.RoomAllocation.Add(obj11);
                context.RoomAllocation.Add(obj12);
                context.RoomAllocation.Add(obj13);
                context.RoomAllocation.Add(obj14);
                context.RoomAllocation.Add(obj15);
                context.RoomAllocation.Add(obj16);

                int status = context.SaveChanges();
                index++;
            }
            return 1;
        }
        */
        public int CreateRoomAllocation(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels)
        {
            // Pending For RoomAllocation Value 

            int index = 1;
            int groupID = index;
            List<SegmentDto> lstSegment = GetSegment(context);
            for (int i = 1; i <= noOfHotels; i++)
            {
                foreach (var item in lstSegment)
                {

                    RoomAllocationPriceList RmPList = GetRoomAllocationPriceList(item.SegmentName.Trim());
                    var obj1 = new RoomAllocation()
                    {
                        MonthID = monthID,
                        QuarterNo = currentQuarter + 1,
                        GroupID = i,
                        Weekday = true,
                        Segment = item.SegmentName,
                        RoomsAllocated = RmPList.RoomsAllocated,
                        ActualDemand = RmPList.ActualDemand,
                        RoomsSold = RmPList.RoomsSold,
                        Confirmed = false,
                        Revenue = RmPList.Revenue,
                        QuarterForecast = currentQuarter
                    };
                    context.RoomAllocation.Add(obj1);
                }
                int status = context.SaveChanges();

                index++;
            }
            return 1;
        }

        public int CreateCustomerRawRating(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels)
        {


            List<SegmentDto> lstSegment = GetSegment(context);
            List<AttributeDto> lstAttribute = GetAttribute(context);
            for (int i = 1; i <= noOfHotels; i++)
            {
                foreach (var attributeRow in lstAttribute)
                {
                    foreach (var segmentRow in lstSegment)
                    {

                        IQueryable<CustomerRawRating> query = context.CustomerRawRating;
                        var obj1 = new CustomerRawRating() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = i, Attribute = attributeRow.AttributeName, Segment = segmentRow.SegmentName, RawRating = 0 };
                        context.CustomerRawRating.Add(obj1);
                        int status = context.SaveChanges();
                    }
                }

            }



            return 1;
        }

        public int CreateWeightedAttributeRating(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels)
        {

            List<SegmentDto> lstSegment = GetSegment(context);
            // List<AttributeDto> lstAttribute = GetAttribute(context);
            int index = 1;
            int groupID = index;
            while (index < noOfHotels + 1)
            {
                foreach (var segmentRow in lstSegment)
                {


                    var obj1 = new WeightedAttributeRating() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = groupID, Segment = segmentRow.SegmentName, CustomerRating = 0, ActualDemand = 0 };
                    context.WeightedAttributeRating.Add(obj1);
                    int status = context.SaveChanges();
                }
                index++;
            }
            return 1;
        }

        public int CreateIncomeState(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels)
        {


            for (int i = 1; i <= noOfHotels; i++)
            {

                var obj1 = new IncomeState()
                {
                    MonthID = monthID,
                    QuarterNo = currentQuarter + 1,
                    GroupID = i,
                    Room1 = 814530,
                    FoodB = 485585,
                    FoodB1 = 1000000,
                    FoodB2 = 1000000,
                    FoodB3 = 1000000,
                    FoodB4 = 1000000,
                    FoodB5 = 1000000,
                    Other = 1000000,
                    Other1 = 1000000,
                    Other2 = 1000000,
                    Other3 = 1000000,
                    Other4 = 1000000,
                    Other5 = 1000000,
                    Other6 = 1000000,
                    Rent = 1000000,
                    TotReven = 1000000,
                    Room = 1000000,
                    Food2B = 1000000,
                    Other7 = 1000000,
                    TotDeptIncom = 1000000,
                    UndisExpens1 = 1000000,
                    UndisExpens2 = 1000000,
                    UndisExpens3 = 1000000,
                    UndisExpens4 = 1000000,
                    UndisExpens5 = 1000000,
                    UndisExpens6 = 1000000,
                    GrossProfit = 1000000,
                    MgtFee = 1000000,
                    IncomBfCharg = 1000000,
                    Property = 1000000,
                    Insurance = 1000000,
                    PropDepreciationerty = 1000000,
                    TotCharg = 1000000,
                    NetIncomBfTAX = 1000000,
                    Replace = 1000000,
                    AjstNetIncom = 1000000,
                    IncomTAX = 1000000,
                    NetIncom = 1000000
                };
                context.IncomeState.Add(obj1);
                int status = context.SaveChanges();


            }
            return 1;
        }

        public int CreateGoal(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels)
        {


            int index = 1;
            int groupID = index;
            while (index < noOfHotels + 1)
            {
                IQueryable<Goal> query = context.Goal;
                var obj1 = new Goal()
                {
                    MonthID = monthID,
                    QuarterNo = currentQuarter + 1,
                    GroupID = groupID,
                    OccupancyM = 0,
                    OccupancyY = 0,
                    RoomRevenM = 0,
                    RoomRevenY = 0,
                    TotalRevenM = 0,
                    TotalRevenY = 0,
                    ShareRevenM = 0,
                    ShareRevenY = 0,
                    ShareRoomM = 0,
                    ShareRoomY = 0,
                    RevparM = 0,
                    RevparY = 0,
                    ADRM = 0,
                    ADRY = 0,
                    YieldMgtM = 0,
                    YieldMgtY = 0,
                    MgtEfficiencyM = 0,
                    MgtEfficiencyY = 0,
                    ProfitMarginM = 0,
                    ProfitMarginY = 0
                };
                context.Goal.Add(obj1);
                int status = context.SaveChanges();

                index++;
            }
            return 1;
        }

        public int CreateSoldRoomByChannel(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels)
        {
            int index = 1;
            int groupID = index;

            int i = 1;
            string segmentName = null;
            string channelName = null;
            bool weekdayIndicator = true;

            while (i < noOfHotels + 1)
            {
                for (int j = 1; j <= 8; j++)
                {
                    for (int k = 1; k <= 4; k++)
                    {
                        for (int w = 0; w <= 1; w++)
                        {
                            if (j == 1)
                                segmentName = "Business";
                            if (j == 2)
                                segmentName = "Small Business";
                            if (j == 3)
                                segmentName = "Corporate contract";
                            if (j == 4)
                                segmentName = "Families";
                            if (j == 5)
                                segmentName = "Afluent Mature Travelers";
                            if (j == 6)
                                segmentName = "International leisure travelers";
                            if (j == 7)
                                segmentName = "Corporate/Business Meetings";
                            if (j == 8)
                                segmentName = "Association Meetings";
                            if (k == 1)
                                channelName = "Direct";
                            if (k == 2)
                                channelName = "Travel Agent";
                            if (k == 3)
                                channelName = "Online Travel Agent";
                            if (k == 4)
                                channelName = "Opaque";
                            if (w == 0)
                                weekdayIndicator = false;
                            if (w == 1)
                                weekdayIndicator = true;

                            var obj1 = new SoldRoomByChannel()
                            {
                                MonthID = monthID,
                                QuarterNo = currentQuarter + 1,
                                GroupID = groupID,
                                Segment = segmentName,
                                Channel = channelName,
                                Weekday = weekdayIndicator,
                                SoldRoom = 0,
                                Revenue = 0,
                                Cost = 0

                            };
                            context.SoldRoomByChannel.Add(obj1);
                            int status = context.SaveChanges();

                        }
                    }
                }
                ////Go to next group
                i++;
            }
            return 1;
        }

        public int CreateBalanceSheet(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels)
        {


            int index = 1;
            int groupID = index;
            while (index < noOfHotels + 1)
            {


                var obj1 = new BalanceSheet()
                {
                    MonthID = monthID,
                    QuarterNo = currentQuarter,
                    GroupID = groupID,
                    Cash = 1000000,
                    AcctReceivable = 400000,
                    Inventories = 500000,
                    TotCurrentAsset = 1888000,
                    NetPrptyEquip = 45335000,
                    TotAsset = 52223000,
                    TotCurrentLiab = 0,
                    LongDebt = 40000000,
                    LongDebtPay = 0,
                    ShortDebt = 0,
                    ShortDebtPay = 0,
                    TotLiab = 40896010,
                    RetainedEarn = 1326990
                };
                var obj2 = new BalanceSheet()
                {
                    MonthID = monthID,
                    QuarterNo = currentQuarter + 1,
                    GroupID = groupID,
                    Cash = 1000000,
                    AcctReceivable = 400000,
                    Inventories = 500000,
                    TotCurrentAsset = 1888000,
                    NetPrptyEquip = 45335000,
                    TotAsset = 52223000,
                    TotCurrentLiab = 0,
                    LongDebt = 40000000,
                    LongDebtPay = 0,
                    ShortDebt = 0,
                    ShortDebtPay = 0,
                    TotLiab = 40896010,
                    RetainedEarn = 1326990
                };
                context.BalanceSheet.Add(obj1);
                context.BalanceSheet.Add(obj2);
                int status = context.SaveChanges();

                index++;
            }
            return 1;
        }

        public int UpdateClassQuarter(HotelDbContext context, int classID, int currentQuarter)
        {
            ClassSession clsSess = context.ClassSessions.Where(x => x.ClassId == classID).First();
            clsSess.CurrentQuater = currentQuarter + 1;
            clsSess.Status = ClassStatus.I;
            context.ClassSessions.Add(clsSess);
            context.Entry(clsSess).State = EntityState.Modified;
            context.SaveChanges();

            return 1;
        }
        public List<AttributeDto> GetAttribute(HotelDbContext context)
        {
            IQueryable<Attribute> query = context.Attribute;

            var result = query.Select(x => new AttributeDto
            {

                ID = x.ID,
                AttributeName = x.AttributeName

            }).ToList();

            return result;

        }
        public List<SegmentDto> GetSegment(HotelDbContext context)
        {
            IQueryable<Segment> query = context.Segment;
            ;

            var result = query.Select(x => new SegmentDto
            {

                ID = x.ID,
                SegmentName = x.SegmentName

            }).ToList();

            return result;

        }
        public List<MarketingTechniquesDto> GetMarketingTechniques(HotelDbContext context)
        {
            IQueryable<MarketingTechniques> query = context.MarketingTechniques;

            var result = query.Select(x => new MarketingTechniquesDto
            {

                ID = x.ID,
                Techniques = x.Techniques

            }).ToList();

            return result;

        }
        public List<DistributionChannelsDto> GetDistributionChannels(HotelDbContext context)
        {
            IQueryable<DistributionChannels> query = context.DistributionChannels;

            var result = query.Select(x => new DistributionChannelsDto
            {

                ID = x.ID,
                Channel = x.Channel

            }).ToList();

            return result;

        }
        public decimal ScalarQueryInitialCapitalInvestAttributeConfig(HotelDbContext context, int monthID, int currentQuarter, string AttributeName)
        {
            string newAttr = AttributeName.Trim();
            var data = (from a in context.AttributeMaxCapitalOperationConfig
                        join m in context.Months on a.ConfigID equals m.ConfigId // x.MonthId == monthID)
                        join c in context.ClassSessions on m.ClassId equals c.ClassId
                        where m.MonthId == monthID && c.CurrentQuater == currentQuarter && a.Attribute == newAttr
                        select new
                        {
                            initialCapital = a.InitialCapital,
                            //MonthID = m.MonthId,
                            //CurrentQuarter = c.CurrentQuater,
                            //AttributeName = a.Attribute
                        }

                     ).ToList();
            decimal sindleRow = 0;
            if (data != null)
            {
                sindleRow = data[0].initialCapital;//data.Where(x => x.MonthID == monthID && x.CurrentQuarter == currentQuarter && x.AttributeName.Contains(newAttr));
            }


            return Convert.ToDecimal(sindleRow);
        }
        public MarketingDecisionDto GetDataBySingleRowMarketingDecision(HotelDbContext context, int groupID, int monthID, int currentQuarter, string marketingTechniques, string segment)
        {
            var mkt = (from md in context.MarketingDecision
                       where md.GroupID == groupID && md.MonthID == monthID && md.QuarterNo == currentQuarter
                       && md.MarketingTechniques == marketingTechniques && md.Segment == segment
                       select new
                       {
                           ActualDemand = md.ActualDemand,
                           Confirmed = md.Confirmed,
                           GroupID = md.GroupID,
                           LaborSpending = md.LaborSpending,
                           MarketingTechniques = md.MarketingTechniques,
                           QuarterNo = md.QuarterNo,
                           Segment = md.Segment,
                           MonthID = md.MonthID,
                           Spending = md.Spending
                       }).ToList();
            MarketingDecisionDto obj = new MarketingDecisionDto();
            if (obj != null)
            {
                obj.ActualDemand = mkt[0].ActualDemand;
                obj.Confirmed = mkt[0].Confirmed;
                obj.GroupID = mkt[0].GroupID;
                obj.LaborSpending = mkt[0].LaborSpending;
                obj.MarketingTechniques = mkt[0].MarketingTechniques;
                obj.QuarterNo = mkt[0].QuarterNo;
                obj.Segment = mkt[0].Segment;
                obj.MonthID = mkt[0].MonthID;
                obj.Spending = mkt[0].Spending;
            }
            return obj;
        }
        public AttributeDecisionDto GetDataBySingleRowAttributeDecision(HotelDbContext context, int groupID, int monthID, int currentQuarter, string attributeName)
        {
            var mkt = (from md in context.AttributeDecision
                       where md.GroupID == groupID && md.MonthID == monthID && md.QuarterNo == currentQuarter
                       && md.Attribute == attributeName.Trim()
                       select new
                       {
                           AccumulatedCapital = md.AccumulatedCapital,
                           Attribute = md.Attribute,
                           Confirmed = md.Confirmed,
                           GroupID = md.GroupID,
                           LaborBudget = md.LaborBudget,
                           NewCapital = md.NewCapital,
                           OperationBudget = md.OperationBudget,
                           QuarterForecast = md.QuarterForecast,
                           QuarterNo = md.QuarterNo,
                           MonthID = md.MonthID
                       }).ToList();
            AttributeDecisionDto obj = new AttributeDecisionDto();
            if (mkt.Count > 0)
            {

                obj.AccumulatedCapital = mkt[0].AccumulatedCapital;
                obj.Attribute = mkt[0].Attribute;
                obj.GroupID = mkt[0].GroupID;
                obj.LaborBudget = mkt[0].LaborBudget;
                obj.NewCapital = mkt[0].NewCapital;
                obj.QuarterNo = mkt[0].QuarterNo;
                obj.OperationBudget = mkt[0].OperationBudget;
                obj.MonthID = mkt[0].MonthID;
                obj.QuarterForecast = mkt[0].QuarterForecast;
            }
            return obj;
        }
        public decimal ScalarDepreciRateMonthlyAttributeConfig(HotelDbContext context, int monthID, int currentQuarter, string AttributeName)
        {
            var attrConf = (from m in context.Months
                            join c in context.ClassSessions on m.ClassId equals c.ClassId
                            join a in context.AttributeMaxCapitalOperationConfig on m.ConfigId equals a.ConfigID
                            where m.MonthId == monthID && c.CurrentQuater == currentQuarter && a.Attribute == AttributeName
                            select new
                            {
                                Expr1 = a.DepreciationYearly / 12
                            }).ToList();

            decimal Expr1 = attrConf[0].Expr1;
            return Expr1;
        }
        public MarketDecisionPriceList GetMarketDecisionPriceList(string marketingTech, string segment)
        {
            PriceListCreated objPC = new PriceListCreated();
            var plist = objPC.MarketDecisionPriceList().Where(x => x.MarketingTechniques == marketingTech.Trim() && x.Segment == segment.Trim()).ToList();

            MarketDecisionPriceList obj = new MarketDecisionPriceList();
            obj.ActualDemand = plist[0].ActualDemand;
            obj.LaborSpending = plist[0].LaborSpending;
            obj.Spending = plist[0].Spending;



            return obj;
        }

        public PriceDecisionPriceList GetPriceDecisionPriceList(string distributionChannel, string segment, int index)
        {
            PriceListCreated objPC = new PriceListCreated();
            var plist = objPC.PriceDecisionPriceList().Where(x => x.DistributionChannel == distributionChannel.Trim() && x.Segment == segment.Trim()).ToList();

            PriceDecisionPriceList obj = new PriceDecisionPriceList();
            if (index < 32)
            {
                obj.ActualDemand = plist[0].ActualDemand;
                obj.Price = plist[0].Price;
            }
            else
            {
                obj.ActualDemand = plist[1].ActualDemand;
                obj.Price = plist[1].Price;
            }
            return obj;
        }

        public AttributeDecisionPriceList GetAttributeDecisionPriceList(string AttributeName)
        {
            PriceListCreated objPC = new PriceListCreated();
            var plist = objPC.AttributeDecisionPriceList().Where(x => x.Attribute == AttributeName.Trim()).ToList();

            AttributeDecisionPriceList obj = new AttributeDecisionPriceList();

            obj.AccumulatedCapital = plist[0].AccumulatedCapital;
            obj.NewCapital = plist[0].NewCapital;

            obj.OperationBudget = plist[0].OperationBudget;
            obj.LaborBudget = plist[0].LaborBudget;
            obj.QuarterForecast = plist[0].QuarterForecast;

            return obj;
        }
        public RoomAllocationPriceList GetRoomAllocationPriceList(string SegmentName)
        {
            PriceListCreated objPC = new PriceListCreated();
            var plist = objPC.RoomAllocationPriceList().Where(x => x.Segment == SegmentName.Trim()).ToList();

            RoomAllocationPriceList obj = new RoomAllocationPriceList();

            obj.RoomsAllocated = plist[0].RoomsAllocated;
            obj.ActualDemand = plist[0].ActualDemand;

            obj.RoomsSold = plist[1].RoomsSold;
            obj.Revenue = plist[1].Revenue;
            obj.QuarterForecast = plist[1].QuarterForecast;

            return obj;

        }
        public IncomeState GetDataBySingleRowIncomeState(HotelDbContext context, int MonthID, int groupID, int currentQuarter)
        {
            var data = context.IncomeState.Where(x => x.MonthID == MonthID && x.GroupID == groupID && x.QuarterNo == currentQuarter)
                    .ToList();
            IncomeState obj = new IncomeState();
            obj.TotReven = data[0].TotReven;
            return obj;
        }
        public bool UpdateMonthCompletedStatus(HotelDbContext context, int classID, int currentQuarter, bool iscompleted)
        {
            bool result = false;
            try
            {
                Month clsMonth = context.Months.Where(x => x.ClassId == classID && x.Sequence == currentQuarter).First();
                clsMonth.IsComplete = iscompleted;
                context.Months.Add(clsMonth);
                context.Entry(clsMonth).State = EntityState.Modified;
                context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
        public bool UpdateClassStatus(HotelDbContext context, int classID, string status)
        {
            bool result = false;
            try
            {
                ClassSession clsSess = context.ClassSessions.Where(x => x.ClassId == classID).First();
                switch (status)
                {
                    case "I":
                        clsSess.Status = ClassStatus.I;
                        break;
                    case "A":
                        clsSess.Status = ClassStatus.A;
                        break;
                    case "S":
                        clsSess.Status = ClassStatus.S;
                        break;
                    case "T":
                        clsSess.Status = ClassStatus.T;
                        break;

                }

                context.ClassSessions.Add(clsSess);
                context.Entry(clsSess).State = EntityState.Modified;
                context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
