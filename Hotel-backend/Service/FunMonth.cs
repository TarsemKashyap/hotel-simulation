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
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Net.Http;
using System.Threading.Channels;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
//using EFCore.BulkExtensions;

namespace Service
{
    public class FunMonth
    {
        public ClassSessionDto GetClassDetailsById(int classID, HotelDbContext context)
        {

            var data = context.ClassSessions.SingleOrDefault(x => x.ClassId == classID);
            if (data == null)
            // throw new ValidationException("data not found ");
            {
                return new ClassSessionDto();
            }
            return data.Adapt<ClassSessionDto>();

            //IQueryable<ClassSession> query = context.ClassSessions;
            //if (classID > 0)
            //{
            //    query = query.Where(x => x.ClassId == classID);
            //}
            //var result = query.Select(x => new ClassSessionDto
            //{

            //    ClassId = x.ClassId,
            //    CurrentQuater = x.CurrentQuater,
            //    HotelsCount = x.HotelsCount
            //}).ToList();
            //ClassSessionDto obj = new ClassSessionDto();
            //obj = result[0];
            //return obj;

        }
        public int CreateMonth(HotelDbContext context, int classID, int currentQuarter, int totalMarket, bool isCompleted)
        {
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
        public async Task<int> CreateMarketingDecision(HotelDbContext context, int newMonth, int currentQuarter, int lastMonth)
        {
            //SELECT actualDemand, confirmed, groupID, laborSpending, marketingTechniques, quarterNo, segment, sessionID, spending FROM marketingDecision WHERE (sessionID = @sessionID) AND (quarterNo = @quarterNo)
            var mktDecisions = context.MarketingDecision.Where(x => x.MonthID == lastMonth).AsEnumerable();
            foreach (var item in mktDecisions)
            {
                var decision = new MarketingDecision()
                {
                    QuarterNo = currentQuarter + 1,
                    GroupID = item.GroupID,
                    LaborSpending = item.LaborSpending,
                    Segment = item.Segment,
                    MarketingTechniques = item.MarketingTechniques,
                    Spending = item.Spending,
                    ActualDemand = 0,
                    Confirmed = false,
                    MonthID = newMonth,
                };
                context.MarketingDecision.Add(decision);
            }
            return await context.SaveChangesAsync();

        }


        public async Task<int> CreateFirstMarketingDecision(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels)
        {

            try
            {
                List<SegmentDto> lstSegment = await GetSegment(context);
                List<MarketingTechniquesDto> lstmarketingTechniques = await GetMarketingTechniques(context);
                for (int i = 1; i <= noOfHotels; i++)
                {
                    //index = i;
                    foreach (SegmentDto segment in lstSegment)
                    {
                        foreach (MarketingTechniquesDto Mkt in lstmarketingTechniques)
                        {
                            MarketDecisionPriceList obj = GetMarketDecisionPriceList(Mkt.Techniques.Trim(), segment.SegmentName.Trim());
                            var obj1 = new MarketingDecision()
                            {
                                // ID = Random.Shared.Next(100),
                                MonthID = monthID,
                                QuarterNo = currentQuarter + 1,
                                GroupID = i,
                                MarketingTechniques = Mkt.Techniques,
                                Segment = segment.SegmentName,
                                Spending = (int)obj.Spending,
                                LaborSpending = (int)obj.LaborSpending,
                                ActualDemand = (int)obj.ActualDemand,
                                Confirmed = false
                            };
                            context.MarketingDecision.Add(obj1);
                            int affected = await context.SaveChangesAsync();
                        }


                    }
                }
                //context.SaveChanges();

            }
            catch (Exception ex)
            {
                string exp = ex.ToString();
            }


            return 1;
        }

        public async Task<int> CreateFirstPriceDecision(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels, bool weekday)
        {

            // Not underStand for Old Project Run Two Time Insert 
            List<SegmentDto> lstSegment = await GetSegment(context);

            List<DistributionChannelsDto> lstChannel = await GetDistributionChannels(context);

            for (int i = 1; i <= noOfHotels; i++)
            {
                // int index = 1;
                foreach (SegmentDto segment in lstSegment)
                {
                    foreach (DistributionChannelsDto channel in lstChannel)
                    {

                        PriceDecisionPriceList obj = GetPriceDecisionPriceList(channel.Channel.Trim(), segment.SegmentName.Trim(), weekday);
                        context.PriceDecision.Add(new PriceDecision
                        {
                            MonthID = monthID,
                            QuarterNo = currentQuarter + 1,
                            GroupID = i,
                            Weekday = weekday,
                            DistributionChannel = channel.Channel,
                            Segment = segment.SegmentName,
                            Price = obj.Price,
                            ActualDemand = Math.Round(obj.ActualDemand),
                            Confirmed = false
                        });
                        // context.PriceDecision.Add(obj1);
                        int affected = await context.SaveChangesAsync();
                    }

                }

                // context.SaveChanges();

            }
            return 1;
        }

        public async Task<int> CreatePriceDecision(HotelDbContext context, int monthID, int currentQuarter, int lastMonth)
        {
            var priceDecisions = context.PriceDecision.Where(x => x.MonthID == lastMonth).AsEnumerable();
            foreach (var item in priceDecisions)
            {
                var decision = new PriceDecision
                {
                    MonthID = monthID,
                    QuarterNo = currentQuarter + 1,
                    GroupID = item.GroupID,
                    Weekday = item.Weekday,
                    DistributionChannel = item.DistributionChannel,
                    Segment = item.Segment,
                    Price = item.Price,
                    ActualDemand = item.ActualDemand,
                    Confirmed = false
                };
                context.PriceDecision.Add(decision);
            }
            return await context.SaveChangesAsync();
        }

        public async Task<int> CreateAttributeDecision(HotelDbContext context, int monthID, int currentQuarter, int lastMonthId)
        {

            var attributeDecisions = context.AttributeDecision.Where(x => x.MonthID == lastMonthId).AsEnumerable();

            var result = (from m in context.Months
                          from config in context.AttributeMaxCapitalOperationConfig
                          where m.MonthId == monthID && m.ConfigId == config.ConfigID
                          select new { config.Attribute, Deprec = config.DepreciationYearly / 12 }).ToDictionary(x => x.Attribute, x => x.Deprec);

            foreach (var item in attributeDecisions)
            {
                result.TryGetValue(item.Attribute, out decimal depre);
                var obj1 = new AttributeDecision()
                {
                    MonthID = monthID,
                    QuarterNo = currentQuarter + 1,
                    GroupID = item.GroupID,
                    Attribute = item.Attribute,
                    AccumulatedCapital = (item.AccumulatedCapital + item.NewCapital) * (1 - depre),
                    NewCapital = item.NewCapital,
                    OperationBudget = item.OperationBudget,
                    LaborBudget = item.LaborBudget,
                    Confirmed = false,
                    QuarterForecast = currentQuarter
                };
                context.AttributeDecision.Add(obj1);
            }
            return await context.SaveChangesAsync();
        }

        public async Task<int> CreateFirstAttributeDecision(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels)
        {

            List<AttributeDto> lstAttribute = await GetAttribute(context);
            Month month = await context.Months.FirstOrDefaultAsync(x => x.MonthId == monthID);
            Month lastMonth = await context.Months.Where(x => x.ClassId == month.ClassId && x.MonthId < monthID).OrderByDescending(x => x.MonthId).FirstOrDefaultAsync();
            for (int i = 1; i <= noOfHotels; i++)
            {
                foreach (var item in lstAttribute)
                {
                    int groupID = i;
                    // var datafi = GetDataBySingleRowAttributeDecision(context, groupID, monthID, currentQuarter, item.AttributeName.Trim());
                    if (currentQuarter == 0)
                    {
                        decimal accumuCapital = ScalarQueryInitialCapitalInvestAttributeConfig(context, monthID, currentQuarter, item.AttributeName);
                        AttributeDecisionPriceList AttPlist = GetAttributeDecisionPriceList(item.AttributeName.Trim());
                        var obj1 = new AttributeDecision()
                        {
                            MonthID = monthID,
                            QuarterNo = currentQuarter + 1,
                            GroupID = groupID,
                            Attribute = item.AttributeName.Trim(),
                            AccumulatedCapital = (int)accumuCapital,
                            NewCapital = AttPlist.NewCapital,
                            OperationBudget = AttPlist.OperationBudget,
                            LaborBudget = AttPlist.LaborBudget,
                            Confirmed = false,
                            QuarterForecast = currentQuarter
                        };
                        context.AttributeDecision.Add(obj1);
                    }
                    else
                    {
                        AttributeDecisionDto row = GetDataBySingleRowAttributeDecision(context, lastMonth.MonthId, currentQuarter, i, item.AttributeName);
                        decimal depreciationRate = Convert.ToDecimal(ScalarDepreciRateMonthlyAttributeConfig(context, monthID, currentQuarter, item.AttributeName));
                        decimal accumuCapital = (row.AccumulatedCapital + row.NewCapital) * (1 - depreciationRate);
                        AttributeDecisionPriceList AttPlist = GetAttributeDecisionPriceList(item.AttributeName.Trim());
                        var obj1 = new AttributeDecision()
                        {
                            MonthID = monthID,
                            QuarterNo = currentQuarter + 1,
                            GroupID = groupID,
                            Attribute = item.AttributeName,
                            AccumulatedCapital = (int)accumuCapital,
                            NewCapital = row.NewCapital,
                            OperationBudget = row.OperationBudget,
                            LaborBudget = row.LaborBudget,
                            Confirmed = false,
                            QuarterForecast = currentQuarter
                        };
                        context.AttributeDecision.Add(obj1);

                    }
                }



                int status = context.SaveChanges();
            }

            return 1;
        }

        public async Task<int> CreateFirstRoomAllocation(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels, bool weekday)
        {
            // Pending For RoomAllocation Value 
            List<SegmentDto> lstSegment = await GetSegment(context);
            for (int i = 1; i <= noOfHotels; i++)
            {
                foreach (var item in lstSegment)
                {

                    RoomAllocationPriceList RmPList = GetRoomAllocationPriceList(item.SegmentName.Trim(), weekday);
                    var obj1 = new RoomAllocation()
                    {
                        MonthID = monthID,
                        QuarterNo = currentQuarter + 1,
                        GroupID = i,
                        Weekday = weekday,
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

            }
            return 1;
        }

        public async Task<int> CreateRoomAllocation(HotelDbContext context, int monthID, int currentQuarter, int lastMonthId)
        {
            var roomAllocation = context.RoomAllocation.Where(x => x.MonthID == lastMonthId).AsEnumerable();
            var newColl = roomAllocation.Select(item =>
              {
                  return new RoomAllocation
                  {
                      MonthID = monthID,
                      QuarterNo = currentQuarter + 1,
                      GroupID = item.GroupID,
                      Weekday = item.Weekday,
                      Segment = item.Segment,
                      RoomsAllocated = item.RoomsAllocated,
                      ActualDemand = 0,
                      RoomsSold = 0,
                      Confirmed = false,
                      Revenue = 0,
                      QuarterForecast = currentQuarter
                  };
              });
            context.RoomAllocation.AddRange(newColl);
            return await context.SaveChangesAsync();
        }

        public async Task<int> CreateCustomerRawRating(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels)
        {


            List<SegmentDto> lstSegment = await GetSegment(context);
            List<AttributeDto> lstAttribute = await GetAttribute(context);
            for (int i = 1; i <= noOfHotels; i++)
            {
                foreach (var attributeRow in lstAttribute)
                {
                    foreach (var segmentRow in lstSegment)
                    {

                        var obj1 = new CustomerRawRating() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = i, Attribute = attributeRow.AttributeName, Segment = segmentRow.SegmentName, RawRating = 0 };
                        context.CustomerRawRating.Add(obj1);
                    }
                    context.SaveChanges();
                }

            }

            return 1;
        }

        public async Task<int> CreateWeightedAttributeRating(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels)
        {

            List<SegmentDto> lstSegment = await GetSegment(context);
            // List<AttributeDto> lstAttribute = GetAttribute(context);

            for (int i = 1; i <= noOfHotels; i++)
            {
                foreach (var segmentRow in lstSegment)
                {
                    var obj1 = new WeightedAttributeRating() { MonthID = monthID, QuarterNo = currentQuarter + 1, GroupID = i, Segment = segmentRow.SegmentName, CustomerRating = 0, ActualDemand = 0 };
                    context.WeightedAttributeRating.Add(obj1);
                    int status = context.SaveChanges();
                }

            }
            return 1;
        }


        public async Task<int> CreateIncomeState(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels)
        {

            if (currentQuarter == 0)
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
                        NetIncom = 1000000,
                        Interest = 1000000,
                        TotExpen = 1000000
                    };
                    context.IncomeState.Add(obj1);
                }
                int status = await context.SaveChangesAsync();
            }
            else
            {
                var prevMonths = PreviousMonthFinder(context, monthID);

                for (int i = 1; i <= noOfHotels; i++)
                {

                    decimal totalRevenBefore = GetDataBySingleRowIncomeState(context, prevMonths(1), i, currentQuarter);
                    var obj1 = new IncomeState()
                    {
                        MonthID = monthID,
                        QuarterNo = currentQuarter + 1,
                        GroupID = i,
                        Room1 = 0,
                        FoodB = 0,
                        FoodB1 = 0,
                        FoodB2 = 0,
                        FoodB3 = 0,
                        FoodB4 = 0,
                        FoodB5 = 0,
                        Other = 0,
                        Other1 = 0,
                        Other2 = 0,
                        Other3 = 0,
                        Other4 = 0,
                        Other5 = 0,
                        Other6 = 0,
                        Rent = 0,
                        TotReven = totalRevenBefore,
                        Room = 0,
                        Food2B = 0,
                        Other7 = 0,
                        TotDeptIncom = 0,
                        UndisExpens1 = 0,
                        UndisExpens2 = 0,
                        UndisExpens3 = 0,
                        UndisExpens4 = 0,
                        UndisExpens5 = 0,
                        UndisExpens6 = 0,
                        GrossProfit = 0,
                        MgtFee = 0,
                        IncomBfCharg = 0,
                        Property = 0,
                        Insurance = 0,
                        PropDepreciationerty = 0,
                        TotCharg = 0,
                        NetIncomBfTAX = 0,
                        Replace = 0,
                        AjstNetIncom = 0,
                        IncomTAX = 0,
                        NetIncom = 0,
                        Interest = 0,
                        TotExpen = 0
                    };
                    context.IncomeState.Add(obj1);

                }
                await context.SaveChangesAsync();
            }
            return 1;
        }

        private Func<int, int> PreviousMonthFinder(HotelDbContext context, int monthID)
        {
            var prevMonths = context.Months
                            .Include(x => x.Class)
                            .ThenInclude(x => x.Months)
                            .Where(x => x.MonthId == monthID)
                            .SelectMany(x => x.Class.Months)
                            .OrderBy(x => x.MonthId)
                           .Select(x => x.MonthId)
                            .ToList();
            return (int index) =>
            {
                int prevMonthIndex = prevMonths.FindIndex(p => p == monthID);
                return prevMonths[prevMonthIndex - index];
            };
        }

        public async Task<int> CreateGoal(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels)
        {


            //while (index < noOfHotels + 1)
            for (int i = 1; i <= noOfHotels; i++)
            {
                var obj1 = new Goal()
                {
                    MonthID = monthID,
                    QuarterNo = currentQuarter + 1,
                    GroupID = i,
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
            }
            return await context.SaveChangesAsync();
        }

        public async Task<int> CreateSoldRoomByChannel(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels)
        {

            string segmentName = null;
            string channelName = null;
            bool weekdayIndicator = true;

            // while (i < noOfHotels + 1)
            for (int i = 1; i <= noOfHotels; i++)
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
                                GroupID = i,
                                Segment = segmentName,
                                Channel = channelName,
                                Weekday = weekdayIndicator,
                                SoldRoom = 0,
                                Revenue = 0,
                                Cost = 0

                            };
                            context.SoldRoomByChannel.Add(obj1);

                        }
                    }
                    await context.SaveChangesAsync();
                }
                ////Go to next group

            }
            return 1;
        }

        public async Task<int> CreateBalanceSheet(HotelDbContext context, int monthID, int currentQuarter, int noOfHotels)
        {



            // while (index < noOfHotels + 1)
            var lastMonthid = await context.Months.LastMonthId(monthID);

            for (int i = 1; i <= noOfHotels; i++)
            {
                if (currentQuarter == 0)
                {

                    var obj1 = new BalanceSheet()
                    {
                        MonthID = monthID,
                        QuarterNo = currentQuarter,
                        GroupID = i,
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
                        GroupID = i,
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
                }
                else
                {

                    BalanceSheet balanceRow = await GetDataBySingleRowBalanceSheet(context, lastMonthid, currentQuarter, i);

                    var obj1 = new BalanceSheet()
                    {
                        MonthID = monthID,
                        QuarterNo = currentQuarter + 1,
                        GroupID = i,
                        Cash = balanceRow.Cash,
                        AcctReceivable = 0,
                        Inventories = 0,
                        TotCurrentAsset = 0,
                        NetPrptyEquip = 0,
                        TotAsset = 0,
                        TotCurrentLiab = 0,
                        LongDebt = balanceRow.LongDebt,
                        LongDebtPay = 0,
                        ShortDebt = balanceRow.ShortDebt,
                        ShortDebtPay = 0,
                        TotLiab = 0,
                        RetainedEarn = 0
                    };

                    context.BalanceSheet.Add(obj1);

                }
            }
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateClassQuarter(HotelDbContext context, int classID, int currentQuarter)
        {
            ClassSession clsSess = context.ClassSessions.Where(x => x.ClassId == classID).First();
            clsSess.CurrentQuater = currentQuarter + 1;
            clsSess.Status = ClassStatus.I;
            context.ClassSessions.Add(clsSess);
            context.Entry(clsSess).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return 1;
        }
        public async Task<List<AttributeDto>> GetAttribute(HotelDbContext context)
        {

            var data = await context.Attribute.ToListAsync();
            if (data == null)
                throw new ValidationException("data not found ");
            return data.Adapt<List<AttributeDto>>();
            //IQueryable<Attribute> query = context.Attribute;

            //var result = query.Select(x => new AttributeDto
            //{

            //    ID = x.ID,
            //    AttributeName = x.AttributeName

            //}).ToList();

            //return result;

        }
        public async Task<List<SegmentDto>> GetSegment(HotelDbContext context)
        {
            //var data = context.Segment;
            //if (data == null)
            //    throw new ValidationException("data not found ");
            //context.ChangeTracker.Clear();
            //return data.Adapt<List<SegmentDto>>();


            var data = await context.Segment.ToListAsync();
            if (data == null)
                throw new ValidationException("data not found ");
            // context.ChangeTracker.Clear();
            return data.Adapt<List<SegmentDto>>();


            //IQueryable<Segment> query = context.Segment;
            //;

            //var result = query.Select(x => new SegmentDto
            //{

            //    ID = x.ID,
            //    SegmentName = x.SegmentName

            //}).ToList();

            //return result;

        }
        public async Task<List<MarketingTechniquesDto>> GetMarketingTechniques(HotelDbContext context)
        {
            var data = await context.MarketingTechniques.ToListAsync();
            if (data == null)
                throw new ValidationException("data not found ");
            //context.ChangeTracker.Clear();
            return data.Adapt<List<MarketingTechniquesDto>>();


            //IQueryable<MarketingTechniques> query = context.MarketingTechniques;

            //var result = query.Select(x => new MarketingTechniquesDto
            //{

            //    ID = x.ID,
            //    Techniques = x.Techniques

            //}).ToList();

            //return result;

        }
        public async Task<List<DistributionChannelsDto>> GetDistributionChannels(HotelDbContext context)
        {

            var data = await context.DistributionChannels.ToListAsync();
            if (data == null)
                throw new ValidationException("data not found ");
            return data.Adapt<List<DistributionChannelsDto>>();

            //IQueryable<DistributionChannels> query = context.DistributionChannels;

            //var result = query.Select(x => new DistributionChannelsDto
            //{

            //    ID = x.ID,
            //    Channel = x.Channel

            //}).ToList();

            //return result;

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
            var data = context.MarketingDecision.SingleOrDefault(x => (x.GroupID == groupID && x.MonthID == monthID && x.QuarterNo == currentQuarter
                       && x.MarketingTechniques == marketingTechniques && x.Segment == segment));
            if (data == null)
                throw new ValidationException("data not found ");
            return data.Adapt<MarketingDecisionDto>();
            //var mkt = (from md in context.MarketingDecision
            //           where md.GroupID == groupID && md.MonthID == monthID && md.QuarterNo == currentQuarter
            //           && md.MarketingTechniques == marketingTechniques && md.Segment == segment
            //           select new
            //           {
            //               ActualDemand = md.ActualDemand,
            //               Confirmed = md.Confirmed,
            //               GroupID = md.GroupID,
            //               LaborSpending = md.LaborSpending,
            //               MarketingTechniques = md.MarketingTechniques,
            //               QuarterNo = md.QuarterNo,
            //               Segment = md.Segment,
            //               MonthID = md.MonthID,
            //               Spending = md.Spending
            //           }).ToList();
            //MarketingDecisionDto obj = new MarketingDecisionDto();
            //if (obj != null)
            //{
            //    obj.ActualDemand = mkt[0].ActualDemand;
            //    obj.Confirmed = mkt[0].Confirmed;
            //    obj.GroupID = mkt[0].GroupID;
            //    obj.LaborSpending = mkt[0].LaborSpending;
            //    obj.MarketingTechniques = mkt[0].MarketingTechniques;
            //    obj.QuarterNo = mkt[0].QuarterNo;
            //    obj.Segment = mkt[0].Segment;
            //    obj.MonthID = mkt[0].MonthID;
            //    obj.Spending = mkt[0].Spending;
            //}
            //return obj;
        }
        public AttributeDecisionDto GetDataBySingleRowAttributeDecision(HotelDbContext context, int monthID, int currentQuarter, int groupID, string attributeName)
        {

            var data = context.AttributeDecision.SingleOrDefault(x => (x.GroupID == groupID && x.MonthID == monthID && x.QuarterNo == currentQuarter
                     && x.Attribute.Trim() == attributeName.Trim()));
            if (data == null)
                throw new ValidationException("data not found ");
            return data.Adapt<AttributeDecisionDto>();

            //var mkt = (from md in context.AttributeDecision
            //           where md.GroupID == groupID && md.MonthID == monthID && md.QuarterNo == currentQuarter
            //           && md.Attribute == attributeName.Trim()
            //           select new
            //           {
            //               AccumulatedCapital = md.AccumulatedCapital,
            //               Attribute = md.Attribute,
            //               Confirmed = md.Confirmed,
            //               GroupID = md.GroupID,
            //               LaborBudget = md.LaborBudget,
            //               NewCapital = md.NewCapital,
            //               OperationBudget = md.OperationBudget,
            //               QuarterForecast = md.QuarterForecast,
            //               QuarterNo = md.QuarterNo,
            //               MonthID = md.MonthID
            //           }).ToList();
            //AttributeDecisionDto obj = new AttributeDecisionDto();
            //if (mkt.Count > 0)
            //{

            //    obj.AccumulatedCapital = mkt[0].AccumulatedCapital;
            //    obj.Attribute = mkt[0].Attribute;
            //    obj.GroupID = mkt[0].GroupID;
            //    obj.LaborBudget = mkt[0].LaborBudget;
            //    obj.NewCapital = mkt[0].NewCapital;
            //    obj.QuarterNo = mkt[0].QuarterNo;
            //    obj.OperationBudget = mkt[0].OperationBudget;
            //    obj.MonthID = mkt[0].MonthID;
            //    obj.QuarterForecast = mkt[0].QuarterForecast;
            //}
            //return obj;
        }
        public decimal ScalarDepreciRateMonthlyAttributeConfig(HotelDbContext context, int monthID, int currentQuarter, string AttributeName)
        {
            var attrConf = (from m in context.Months
                            join c in context.ClassSessions on m.ClassId equals c.ClassId
                            join a in context.AttributeMaxCapitalOperationConfig on m.ConfigId equals a.ConfigID
                            where m.MonthId == monthID && c.CurrentQuater == currentQuarter && a.Attribute.Trim() == AttributeName.Trim()
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
            var data = objPC.MarketDecisionPriceList().SingleOrDefault(x => x.MarketingTechniques == marketingTech.Trim() && x.Segment == segment.Trim());

            if (data == null)
                throw new ValidationException("data not found ");
            return data.Adapt<MarketDecisionPriceList>();


        }

        public async Task<BalanceSheet> GetDataBySingleRowBalanceSheet(HotelDbContext context, int monthId, int quarterNo, int groupId)
        {
            /*SELECT              sessionID, quarterNo, groupID, cash, acctReceivable, inventories, totCurrentAsset, netPrptyEquip, totAsset, totCurrentLiab, longDebt, longDebtPay, shortDebt, shortDebtPay, totLiab, 
                                    retainedEarn
    FROM                  balanceSheet
    WHERE              (sessionID = @sessionID) AND (quarterNo = @quarterNo) AND (groupID = @groupID)*/
            return await context.BalanceSheet.Where(x => x.MonthID == monthId && x.QuarterNo == quarterNo && x.GroupID == groupId).FirstOrDefaultAsync();
        }
        public PriceDecisionPriceList GetPriceDecisionPriceList(string distributionChannel, string segment, bool weekday)
        {

            PriceListCreated objPC = new PriceListCreated();
            var data = objPC.PriceDecisionPriceList().SingleOrDefault(x => x.DistributionChannel == distributionChannel.Trim()
            && x.Segment == segment.Trim() && x.WeekDay == weekday);
            if (data == null)
                throw new ValidationException("data not found ");
            return data.Adapt<PriceDecisionPriceList>();

            //PriceListCreated objPC = new PriceListCreated();
            //var plist = objPC.PriceDecisionPriceList().Where(x => x.DistributionChannel == distributionChannel.Trim()
            //&& x.Segment == segment.Trim() && x.WeekDay == weekday).ToList();

            //PriceDecisionPriceList obj = new PriceDecisionPriceList();

            //obj.ActualDemand = plist[0].ActualDemand;
            //obj.Price = plist[0].Price;



            //return obj;
        }

        public AttributeDecisionPriceList GetAttributeDecisionPriceList(string AttributeName)
        {

            PriceListCreated objPC = new PriceListCreated();
            var data = objPC.AttributeDecisionPriceList().SingleOrDefault(x => x.Attribute == AttributeName.Trim());
            if (data == null)
                throw new ValidationException("data not found ");
            return data.Adapt<AttributeDecisionPriceList>();

            //PriceListCreated objPC = new PriceListCreated();
            //var plist = objPC.AttributeDecisionPriceList().Where(x => x.Attribute == AttributeName.Trim()).ToList();

            //AttributeDecisionPriceList obj = new AttributeDecisionPriceList();

            //obj.AccumulatedCapital = plist[0].AccumulatedCapital;
            //obj.NewCapital = plist[0].NewCapital;

            //obj.OperationBudget = plist[0].OperationBudget;
            //obj.LaborBudget = plist[0].LaborBudget;
            //obj.QuarterForecast = plist[0].QuarterForecast;

            //return obj;
        }
        public RoomAllocationPriceList GetRoomAllocationPriceList(string SegmentName, bool weekday)
        {

            PriceListCreated objPC = new PriceListCreated();
            var data = objPC.RoomAllocationPriceList().SingleOrDefault(x => x.Segment == SegmentName.Trim() && x.WeekDay == weekday);
            if (data == null)
                throw new ValidationException("data not found ");
            return data.Adapt<RoomAllocationPriceList>();


            //PriceListCreated objPC = new PriceListCreated();
            //var plist = objPC.RoomAllocationPriceList().Where(x => x.Segment == SegmentName.Trim() && x.WeekDay == weekday).ToList();

            //RoomAllocationPriceList obj = new RoomAllocationPriceList();

            //obj.RoomsAllocated = plist[0].RoomsAllocated;
            //obj.ActualDemand = plist[0].ActualDemand;

            //obj.RoomsSold = plist[0].RoomsSold;
            //obj.Revenue = plist[0].Revenue;
            //obj.QuarterForecast = plist[0].QuarterForecast;

            //return obj;

        }
        public decimal GetDataBySingleRowIncomeState(HotelDbContext context, int MonthID, int groupID, int currentQuarter)
        {

            decimal TotReven = 0;
            var data = context.IncomeState.FirstOrDefault(x => x.MonthID == MonthID && x.GroupID == groupID && x.QuarterNo == currentQuarter);
            if (data == null)
                TotReven = 0;
            else
                TotReven = data.TotReven;
            return TotReven;

            //var data = context.IncomeState.Where(x => x.MonthID == MonthID && x.GroupID == groupID && x.QuarterNo == currentQuarter)
            //        .ToList();
            //IncomeState obj = new IncomeState();
            //obj.TotReven = data[0].TotReven;
            //return obj;
        }
        public bool UpdateMonthCompletedStatus(HotelDbContext context, int classID, int currentQuarter)
        {
            bool result = false;
            try
            {
                List<Month> lstMonth = context.Months.Where(x => x.ClassId == classID && x.Sequence == currentQuarter && x.IsComplete == false).ToList();
                context.ChangeTracker.Clear();
                if (lstMonth.Count > 0)
                {
                    Month clsMonth = new Month();
                    clsMonth.MonthId = lstMonth[0].MonthId;
                    clsMonth.ClassId = lstMonth[0].ClassId;
                    clsMonth.Sequence = lstMonth[0].Sequence;
                    clsMonth.TotalMarket = lstMonth[0].TotalMarket;
                    clsMonth.ConfigId = lstMonth[0].ConfigId;

                    clsMonth.IsComplete = true;
                    context.Months.Add(clsMonth);
                    context.Entry(clsMonth).Property(x => x.IsComplete).IsModified = true;
                    context.Update(clsMonth);
                    context.SaveChanges();
                }

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
                    case "C": // For Calculation
                        clsSess.Status = ClassStatus.C;
                        break;
                }

                context.ClassSessions.Add(clsSess);
                // context.Entry(clsSess).State = EntityState.Modified;
                context.Entry(clsSess).Property(c => c.Status).IsModified = true;
                context.Update(clsSess);
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
