using Database;
using Database.Migrations;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Mysqlx.Resultset;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Cms;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZstdNet;

namespace Service
{
    public interface ICalculationServices
    {
        IEnumerable<MonthDto> monthList();
        Task<ResponseData> Create(MonthDto month);
        IEnumerable<MonthDto> List(string monthidId = null);
        Task<ResponseData> Calculation(MonthDto month);

    }
    public class CalculationServices : ICalculationServices
    {
        private readonly IMapper _mapper;
        private readonly HotelDbContext _context;

        public CalculationServices(IMapper mapper, HotelDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ResponseData> Calculation(MonthDto month)
        {
            ResponseData resObj = new ResponseData();
            try
            {

                FunMonth obj = new FunMonth();
                FunCalculation objCalculation = new FunCalculation();
                // obj.UpdateClassStatus(_context, month.ClassId, "C");
                ClassSessionDto objclassSess = obj.GetClassDetailsById(month.ClassId, _context);
                int currentQuarter = objclassSess.CurrentQuater;
                int hotelsCount = objclassSess.HotelsCount;
                int monthId = month.MonthId;
                List<MarketingDecisionDto> objListMd = objCalculation.GetDataByQuarterMarketDecision(_context, monthId, currentQuarter);
                double ratio = 0;
                double fourpercentOfRevenue;

                double spending = 0;
                double laborSpending = 0;
                double Qminus1 = 0;
                double Qminus2 = 0;
                double Qminus3 = 0;
                double Qminus4 = 0;
                double LQminus1 = 0;
                double LQminus2 = 0;
                double LQminus3 = 0;
                double LQminus4 = 0;
                double industryNorm = 0;
                if (objListMd.Count > 0)
                {
                    foreach (MarketingDecisionDto mDRow in objListMd)
                    {
                        int groupId = mDRow.GroupID;
                        ////set the four percent of reveune when first quarter
                        if (currentQuarter == 1)
                        {
                            fourpercentOfRevenue = 251645.01 / 3;
                        }
                        else
                        {

                            fourpercentOfRevenue = Convert.ToDouble(obj.GetDataBySingleRowIncomeState(_context, monthId, groupId, currentQuarter).TotReven) * 0.04;
                        }
                        if (mDRow.QuarterNo == 1)
                        {
                            Qminus1 = 251645.01 / 6 * Convert.ToDouble(objCalculation.ScalarQueryIndustrialNormPercentMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo, mDRow.Segment.Trim(), mDRow.MarketingTechniques.Trim()));
                            Qminus2 = Qminus1;
                            Qminus3 = Qminus1;
                            Qminus4 = Qminus1;
                            LQminus1 = 251645.01 / 6 * Convert.ToDouble(objCalculation.ScalarQueryIndustrialNormPercentMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo, mDRow.Segment.Trim(), mDRow.MarketingTechniques.Trim()));
                            LQminus2 = LQminus1;
                            LQminus3 = LQminus1;
                            LQminus4 = LQminus1;
                        }
                        if (mDRow.QuarterNo == 2)
                        {
                            Qminus1 = Convert.ToDouble(objCalculation.ScalarQueryPastSpendingMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo - 1, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                            Qminus2 = 251645.01 / 6 * Convert.ToDouble(objCalculation.ScalarQueryIndustrialNormPercentMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo, mDRow.Segment, mDRow.MarketingTechniques));
                            Qminus3 = Qminus2;
                            Qminus4 = Qminus2;
                            LQminus1 = Convert.ToDouble(objCalculation.ScalarQueryPastLaborSpendingMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo - 1, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                            LQminus2 = 251645.01 / 6 * Convert.ToDouble(objCalculation.ScalarQueryIndustrialNormPercentMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo, mDRow.Segment, mDRow.MarketingTechniques));
                            LQminus3 = LQminus2;
                            LQminus4 = LQminus2;

                        }
                        if (mDRow.QuarterNo == 3)
                        {
                            Qminus1 = Convert.ToDouble(objCalculation.ScalarQueryPastLaborSpendingMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo - 1, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                            Qminus2 = Convert.ToDouble(objCalculation.ScalarQueryPastSpendingMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo - 2, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                            Qminus3 = 251645.01 / 6 * Convert.ToDouble(objCalculation.ScalarQueryIndustrialNormPercentMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo, mDRow.Segment, mDRow.MarketingTechniques));
                            Qminus4 = Qminus3;
                            LQminus1 = Convert.ToDouble(objCalculation.ScalarQueryPastLaborSpendingMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo - 1, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                            LQminus2 = Convert.ToDouble(objCalculation.ScalarQueryPastLaborSpendingMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo - 2, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                            LQminus3 = 251645.01 / 6 * Convert.ToDouble(objCalculation.ScalarQueryIndustrialNormPercentMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo, mDRow.Segment, mDRow.MarketingTechniques));
                            LQminus4 = LQminus3;
                        }
                        if (mDRow.QuarterNo == 4)
                        {
                            Qminus1 = Convert.ToDouble(objCalculation.ScalarQueryPastSpendingMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo - 1, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                            Qminus2 = Convert.ToDouble(objCalculation.ScalarQueryPastSpendingMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo - 2, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                            Qminus3 = Convert.ToDouble(objCalculation.ScalarQueryPastSpendingMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo - 3, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                            Qminus4 = 251645.01 / 6 * Convert.ToDouble(objCalculation.ScalarQueryIndustrialNormPercentMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo, mDRow.Segment, mDRow.MarketingTechniques));
                            LQminus1 = Convert.ToDouble(objCalculation.ScalarQueryPastLaborSpendingMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo - 1, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                            LQminus2 = Convert.ToDouble(objCalculation.ScalarQueryPastLaborSpendingMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo - 2, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                            LQminus3 = Convert.ToDouble(objCalculation.ScalarQueryPastLaborSpendingMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo - 3, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                            LQminus4 = 251645.01 / 6 * Convert.ToDouble(objCalculation.ScalarQueryIndustrialNormPercentMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo, mDRow.Segment, mDRow.MarketingTechniques));

                        }
                        if (mDRow.QuarterNo > 4)
                        {
                            Qminus1 = Convert.ToDouble(objCalculation.ScalarQueryPastSpendingMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo - 1, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                            Qminus2 = Convert.ToDouble(objCalculation.ScalarQueryPastSpendingMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo - 2, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                            Qminus3 = Convert.ToDouble(objCalculation.ScalarQueryPastSpendingMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo - 3, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                            Qminus4 = Convert.ToDouble(objCalculation.ScalarQueryPastSpendingMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo - 4, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                            LQminus1 = Convert.ToDouble(objCalculation.ScalarQueryPastLaborSpendingMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo - 1, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                            LQminus2 = Convert.ToDouble(objCalculation.ScalarQueryPastLaborSpendingMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo - 2, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                            LQminus3 = Convert.ToDouble(objCalculation.ScalarQueryPastLaborSpendingMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo - 3, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                            LQminus4 = Convert.ToDouble(objCalculation.ScalarQueryPastLaborSpendingMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo - 4, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                        }
                        if (mDRow.MarketingTechniques == "Advertising")
                        {
                            spending = 0.75 * Qminus1 + 0.25 * Qminus2 + 0.15 * Qminus3;
                            laborSpending = 0.75 * LQminus1 + 0.25 * LQminus2 + 0.15 * LQminus3;
                        }
                        if (mDRow.MarketingTechniques == "Sales Force")
                        {
                            spending = 0.5 * Qminus2 + 0.35 * Qminus3 + 0.25 * Qminus4;
                            laborSpending = 0.5 * LQminus2 + 0.35 * LQminus3 + 0.25 * LQminus4;
                        }
                        if (mDRow.MarketingTechniques == "Promotions")
                        {
                            spending = 0.8 * Qminus1 + 0.15 * Qminus2 + 0.05 * Qminus3;
                            laborSpending = 0.8 * LQminus1 + 0.15 * LQminus2 + 0.05 * LQminus3;
                        }
                        if (mDRow.MarketingTechniques == "Public Relations")
                        {
                            spending = 0.6 * Qminus1 + 0.35 * Qminus2 + 0.25 * Qminus3;
                            laborSpending = 0.6 * LQminus1 + 0.35 * LQminus2 + 0.25 * LQminus3;
                        }
                        industryNorm = fourpercentOfRevenue * Convert.ToDouble(objCalculation.ScalarQueryIndustrialNormPercentMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo, mDRow.Segment, mDRow.MarketingTechniques));
                        double laborPercent = Convert.ToDouble(objCalculation.ScalarQueryLaborPercentMarketingDecision(_context, mDRow.MonthID, mDRow.MarketingTechniques, mDRow.Segment, mDRow.QuarterNo));
                        double weightedSpending = laborSpending * laborPercent + spending * (1 - laborPercent);
                        //////If weighted spending is greater than 14% of the total revenue from last month
                        //////We force it cut it down to 14% of total revenue
                        if (weightedSpending > fourpercentOfRevenue * 7 / 2)
                        {
                            weightedSpending = fourpercentOfRevenue * 7 / 2;
                        }
                        double fairmarket = Convert.ToDouble(objCalculation.ScalarQueryFairMarketMarketingDecision(_context, mDRow.Segment, mDRow.MarketingTechniques, mDRow.MonthID, mDRow.QuarterNo));
                        ratio = Convert.ToDouble(weightedSpending * weightedSpending / Convert.ToDouble(objCalculation.ScalarQueryAverageSpendingMarktingDecision(_context, mDRow.Segment, mDRow.MarketingTechniques, mDRow.MonthID, mDRow.QuarterNo)) / industryNorm);


                        ////////To avoid ratio = and fairmarket=0 exceptions
                        if (ratio == 0 || fairmarket == 0)
                        {
                            mDRow.ActualDemand = 0;
                        }
                        else if (ratio < 0)
                        {
                            mDRow.ActualDemand = 0;
                        }
                        else
                        {
                            try
                            {
                                mDRow.ActualDemand = Convert.ToInt32((-1 / ratio + 2) * fairmarket);
                            }
                            catch
                            {
                                Trace.Write(mDRow.GroupID.ToString() + "Ratio" + ratio.ToString() + "/n" + "Fairmarket" + fairmarket.ToString());
                            }

                        }
                        //To avoid actual demand is less zero, we just set the less than zero value to be zero
                        if (mDRow.ActualDemand < 0)
                        {
                            mDRow.ActualDemand = 0;
                        }
                        MarketingDecisionUpdate(mDRow);
                    }

                    ////Slow down the calucation to give database more time to process, wait 1/10 second
                    System.Threading.Thread.Sleep(10);
                }

                {
                    //Price Decision
                    PriceDecision objPriceDecision = new PriceDecision();
                    ratio = 0;
                    decimal avergePrice;
                    decimal expectedPrice;
                    decimal fairMarketPD = 0;
                    int actualDemand;
                    //This get data need to change.
                    List<PriceDecisionDto> listPd = GetDataByQuarterPriceDecision(monthId, currentQuarter);
                    foreach (PriceDecisionDto priceDecisionRow in listPd)
                    {
                        avergePrice = Convert.ToDecimal(ScalarQueryAvgPricePriceDecision(monthId, currentQuarter, priceDecisionRow.Weekday, priceDecisionRow.DistributionChannel.Trim(), priceDecisionRow.Segment.Trim()));
                        expectedPrice = Convert.ToDecimal(ScalarQueryPriceExpectationPriceDecision(monthId, currentQuarter, priceDecisionRow.Segment, priceDecisionRow.Weekday));
                        //////Lower expected price by $25 dollars, this is a change made on 3/18/2012, in version 4.5.5
                        expectedPrice = expectedPrice - 25;
                        if (avergePrice != 0)
                        {
                            ratio = Convert.ToDouble(priceDecisionRow.Price * priceDecisionRow.Price / avergePrice / expectedPrice);
                        }
                        else
                        {
                            ratio = 2;
                        }

                        fairMarketPD = Convert.ToDecimal(ScalarQueryFairMarketPriceDecision(priceDecisionRow.Segment, priceDecisionRow.Weekday, priceDecisionRow.DistributionChannel, monthId, currentQuarter));
                        if (ratio == 2 || fairMarketPD == 0)
                        { actualDemand = 0; }
                        else if (ratio > 2)
                        {
                            actualDemand = 0;
                        }
                        else
                        {
                            actualDemand = Convert.ToInt32((1 / (ratio - 2) + 2) * Convert.ToDouble(fairMarketPD));
                        }

                        if (actualDemand < 0)
                        {
                            actualDemand = 0;
                        }
                        priceDecisionRow.ActualDemand = actualDemand;
                        PriceDecisionUpdate(priceDecisionRow);

                    }

                    ////Slow down the calucation to give database more time to process, wait 1/10 second
                    System.Threading.Thread.Sleep(10);
                }

                List<IncomeStateDto> incomTableAfter = GetDataByMonthIncomeState(monthId, currentQuarter);
                foreach (IncomeStateDto row in incomTableAfter)
                {
                    if (currentQuarter <= 1)
                    {
                        row.TotReven = 3162981;
                    }
                    else
                    {
                        row.TotReven = GetDataBySingleRowIncomeState(monthId, currentQuarter - 1, row.GroupID).TotReven;
                    }

                    if (row.TotReven == 0)
                    {
                        row.TotReven = 3585548;
                    }
                    IncomeStateUpdate(row);
                }

                ////Slow down the calucation to give database more time to process, wait 1/10 second
                System.Threading.Thread.Sleep(10);


                List<CustomerRawRatingDto> rawRatingTable = GetDataByQuarterCustomerRowRatting(monthId, currentQuarter);
                //attributeDecisionTableAdapter attriDeAdapter = new attributeDecisionTableAdapter();
                //idealRatingAttributeWeightConfigTableAdapter idealAdpt = new idealRatingAttributeWeightConfigTableAdapter();

                foreach (CustomerRawRatingDto row in rawRatingTable)
                {
                    AttributeDecisionDto attriDeRow = GetDataBySingleRowAttributeDecision(monthId, currentQuarter, row.GroupID, row.Attribute);
                    //In order to avoid divided by zero exception, if any of these spending is zero, 
                    //we will skip the quary and set the rating as zero.
                    if (attriDeRow.OperationBudget == 0 || attriDeRow.AccumulatedCapital == 0 || attriDeRow.LaborBudget == 0)
                    {
                        row.RawRating = 0;
                    }
                    else
                    {
                        decimal ideal = ScalarAttriSegIdealRating(row.MonthID, row.QuarterNo, row.Attribute, row.Segment);
                        row.RawRating = Convert.ToDecimal(ScalarQueryCustomerRawRating(row.MonthID, row.QuarterNo, row.GroupID, row.Attribute, row.Segment));
                        if (row.RawRating > ideal)
                        {
                            //////This one is tricky!
                            ///////This line I made changes to correct the captial over time effects
                            row.RawRating = ideal + Convert.ToDecimal(Math.Log(Convert.ToDouble(row.RawRating - ideal + 1), 2));
                        }
                    }
                    if (row.RawRating < 0)
                    {
                        row.RawRating = 0;
                    }
                    if (row.RawRating >= 10)
                    {
                        row.RawRating = Convert.ToDecimal(9.99);
                    }
                    CustomerRowRatingUpdate(row);
                }
                // rawRatingAdapter.Update(rawRatingTable);
                ////Slow down the calucation to give database more time to process, wait 1/10 second
                System.Threading.Thread.Sleep(10);
                // hotelSimulator.weightedAttributeRatingDataTable overallRatingTable;
                List<WeightedAttributeRatingDto> overallRatingTable = GetDataByQuarterWeightAttributeRating(monthId, currentQuarter);
                decimal averageRating = 0;
                decimal fairMarket = 0;
                foreach (WeightedAttributeRatingDto row in overallRatingTable)
                {
                    row.CustomerRating = ScalarQueryRatingBySegmentCustomerRawRatting(row.MonthID, row.QuarterNo, row.GroupID, row.Segment);
                    averageRating = Convert.ToDecimal(ScalarQueryGetAvageRatingWeightAttributeRating(row.MonthID, row.QuarterNo, row.Segment));
                    fairMarket = Convert.ToDecimal(ScalarQueryFairMarketWeightAttributeRating(row.Segment, row.MonthID, row.QuarterNo));
                    if (row.CustomerRating == 0 || averageRating == 0 || fairMarket == 0)
                    { row.ActualDemand = 0; }
                    else
                    {
                        row.ActualDemand = Convert.ToInt32(row.CustomerRating / averageRating * fairMarket);
                    }
                    WeightedAttributeRatingUpdate(row);
                }

                ////Slow down the calucation to give database more time to process, wait 1/10 second
                System.Threading.Thread.Sleep(10);


                // roomAllocationTableAdapter adapter = new roomAllocationTableAdapter();
                List<RoomAllocationDto> table = GetDataByQuarterRoomAllocation(monthId, currentQuarter);

                //Sum customer demand and set into database
                foreach (RoomAllocationDto row in table)
                {
                    if (row.Weekday == true)
                    {
                        row.ActualDemand = Convert.ToInt32(4 * (
                            Convert.ToInt32(ScalarQueryMarketingDemandBySegment(monthId, currentQuarter, row.GroupID, row.Segment)) +
                            Convert.ToInt32(ScalarQueryAttributeDemandBySegment(monthId, currentQuarter, row.GroupID, row.Segment))) / 7) +
                            Convert.ToInt32(ScalarQueryPriceDemandBySegment(monthId, currentQuarter, row.GroupID, row.Segment, row.Weekday));
                    }
                    if (row.Weekday == false)
                    {
                        row.ActualDemand = Convert.ToInt32(3 * (
                            Convert.ToInt32(ScalarQueryMarketingDemandBySegment(monthId, currentQuarter, row.GroupID, row.Segment)) +
                            Convert.ToInt32(ScalarQueryAttributeDemandBySegment(monthId, currentQuarter, row.GroupID, row.Segment))) / 7) +
                            Convert.ToInt32(ScalarQueryPriceDemandBySegment(monthId, currentQuarter, row.GroupID, row.Segment, row.Weekday));
                    }
                    RoomAllocationUpdate(row);
                }

                ////Slow down the calucation to give database more time to process, wait 1/10 second
                System.Threading.Thread.Sleep(10);

                int maxGroup = Convert.ToInt32(ScalarQueryMaxGroupNoRommAllocation(monthId, currentQuarter));
                int groupID = 1;
                int roomPool = 0;

                /////////Do the sold room and Room Pools each group by each group
                while (groupID < maxGroup + 1)
                {
                    /////////Do the sold room and Room Pools each group by each group for weekday
                    table = GetDataByGroupWeekdayRoomAllocation(monthId, currentQuarter, groupID, true);
                    //////First, we collect all the free rooms that is not used, or in another word, over-allocated.
                    /////Different Segment will have different percentage that will "go bad"
                    /////Business 60% goes bad
                    /////Small Business 60% goes bad
                    /////Corporate contract 60% goes bad
                    /////Afluent Mature Travelers 40% goes bad
                    /////International leisure travelers 40% goes bad
                    /////Families 40% goes bad
                    /////Corporate/Business Meetings 20% goes bad
                    /////Association Meetings 20% goes bad
                    foreach (RoomAllocationDto row in table)
                    {
                        if (row.RoomsAllocated > row.ActualDemand && row.Segment == "Business")
                        {
                            roomPool = (row.RoomsAllocated - row.ActualDemand) * 2 / 5 + roomPool;
                        }
                        if (row.RoomsAllocated > row.ActualDemand && row.Segment == "Small Business")
                        {
                            roomPool = (row.RoomsAllocated - row.ActualDemand) * 2 / 5 + roomPool;
                        }
                        if (row.RoomsAllocated > row.ActualDemand && row.Segment == "Corporate contract")
                        {
                            roomPool = (row.RoomsAllocated - row.ActualDemand) * 2 / 5 + roomPool;
                        }
                        if (row.RoomsAllocated > row.ActualDemand && row.Segment == "Families")
                        {
                            roomPool = (row.RoomsAllocated - row.ActualDemand) * 3 / 5 + roomPool;
                        }
                        if (row.RoomsAllocated > row.ActualDemand && row.Segment == "Afluent Mature Travelers")
                        {
                            roomPool = (row.RoomsAllocated - row.ActualDemand) * 3 / 5 + roomPool;
                        }
                        if (row.RoomsAllocated > row.ActualDemand && row.Segment == "International leisure travelers")
                        {
                            roomPool = (row.RoomsAllocated - row.ActualDemand) * 3 / 5 + roomPool;
                        }
                        if (row.RoomsAllocated > row.ActualDemand && row.Segment == "Corporate/Business Meetings")
                        {
                            roomPool = (row.RoomsAllocated - row.ActualDemand) * 4 / 5 + roomPool;
                        }
                        if (row.RoomsAllocated > row.ActualDemand && row.Segment == "Association Meetings")
                        {
                            roomPool = (row.RoomsAllocated - row.ActualDemand) * 4 / 5 + roomPool;
                        }
                        RoomAllocationUpdate(row);
                    }
                    ////////////////Re-allocate the rooms that are free in such an order
                    ////////////////Business, Corporate contract, Small Business,Afluent Mature Travelers,International leisure travelers,Families,Corporate/Business Meetings,Association Meetings  
                    if (roomPool > 0)
                    {
                        foreach (RoomAllocationDto row in table)
                        {
                            if (row.Segment == "Business")
                            {
                                if (row.RoomsAllocated > row.ActualDemand)
                                {
                                    row.RoomsSold = row.ActualDemand;
                                }
                                if (row.RoomsAllocated == row.ActualDemand)
                                {
                                    row.RoomsSold = row.ActualDemand;
                                }
                                if (row.RoomsAllocated < row.ActualDemand)
                                {
                                    if (currentQuarter <= 1)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                    }
                                    else if (row.RoomsAllocated + roomPool > row.ActualDemand)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                        roomPool = roomPool - row.ActualDemand + row.RoomsAllocated;
                                        if (roomPool < 0)
                                            roomPool = 0;
                                    }
                                    else if (row.RoomsAllocated + roomPool == row.ActualDemand)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                        roomPool = 0;
                                    }
                                    else if (row.RoomsAllocated + roomPool < row.ActualDemand)
                                    {
                                        row.RoomsSold = row.RoomsAllocated + roomPool;
                                        roomPool = 0;
                                    }
                                }
                            }
                            RoomAllocationUpdate(row);
                        }
                    }
                    if (roomPool > 0)
                    {
                        foreach (RoomAllocationDto row in table)
                        {
                            if (row.Segment == "Corporate contract")
                            {
                                if (row.RoomsAllocated > row.ActualDemand)
                                {
                                    row.RoomsSold = row.ActualDemand;
                                }
                                if (row.RoomsAllocated == row.ActualDemand)
                                {
                                    row.RoomsSold = row.ActualDemand;
                                }
                                if (row.RoomsAllocated < row.ActualDemand)
                                {
                                    if (currentQuarter <= 1)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                    }
                                    else if (row.RoomsAllocated + roomPool > row.ActualDemand)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                        roomPool = roomPool - row.ActualDemand + row.RoomsAllocated;
                                        if (roomPool < 0)
                                            roomPool = 0;
                                    }
                                    else if (row.RoomsAllocated + roomPool == row.ActualDemand)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                        roomPool = 0;
                                    }
                                    else if (row.RoomsAllocated + roomPool < row.ActualDemand)
                                    {
                                        row.RoomsSold = row.RoomsAllocated + roomPool;
                                        roomPool = 0;
                                    }
                                }
                            }
                            RoomAllocationUpdate(row);
                        }
                    }
                    if (roomPool > 0)
                    {
                        foreach (RoomAllocationDto row in table)
                        {
                            if (row.Segment == "Small Business")
                            {
                                if (row.RoomsAllocated > row.ActualDemand)
                                {
                                    row.RoomsSold = row.ActualDemand;
                                }
                                if (row.RoomsAllocated == row.ActualDemand)
                                {
                                    row.RoomsSold = row.ActualDemand;
                                }
                                if (row.RoomsAllocated < row.ActualDemand)
                                {
                                    if (currentQuarter <= 1)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                    }
                                    else if (row.RoomsAllocated + roomPool > row.ActualDemand)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                        roomPool = roomPool - row.ActualDemand + row.RoomsAllocated;
                                        if (roomPool < 0)
                                            roomPool = 0;
                                    }
                                    else if (row.RoomsAllocated + roomPool == row.ActualDemand)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                        roomPool = 0;
                                    }
                                    else if (row.RoomsAllocated + roomPool < row.ActualDemand)
                                    {
                                        row.RoomsSold = row.RoomsAllocated + roomPool;
                                        roomPool = 0;
                                    }
                                }
                            }
                            RoomAllocationUpdate(row);
                        }
                    }
                    if (roomPool > 0)
                    {
                        foreach (RoomAllocationDto row in table)
                        {
                            if (row.Segment == "Afluent Mature Travelers")
                            {
                                if (row.RoomsAllocated > row.ActualDemand)
                                {
                                    row.RoomsSold = row.ActualDemand;
                                }
                                if (row.RoomsAllocated == row.ActualDemand)
                                {
                                    row.RoomsSold = row.ActualDemand;
                                }
                                if (row.RoomsAllocated < row.ActualDemand)
                                {
                                    if (currentQuarter <= 1)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                    }
                                    else if (row.RoomsAllocated + roomPool > row.ActualDemand)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                        roomPool = roomPool - row.ActualDemand + row.RoomsAllocated;
                                        if (roomPool < 0)
                                            roomPool = 0;
                                    }
                                    else if (row.RoomsAllocated + roomPool == row.ActualDemand)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                        roomPool = 0;
                                    }
                                    else if (row.RoomsAllocated + roomPool < row.ActualDemand)
                                    {
                                        row.RoomsSold = row.RoomsAllocated + roomPool;
                                        roomPool = 0;
                                    }
                                }
                            }
                            RoomAllocationUpdate(row);
                        }
                    }
                    if (roomPool > 0)
                    {
                        foreach (RoomAllocationDto row in table)
                        {
                            if (row.Segment == "International leisure travelers")
                            {
                                if (row.RoomsAllocated > row.ActualDemand)
                                {
                                    row.RoomsSold = row.ActualDemand;
                                }
                                if (row.RoomsAllocated == row.ActualDemand)
                                {
                                    row.RoomsSold = row.ActualDemand;
                                }
                                if (row.RoomsAllocated < row.ActualDemand)
                                {
                                    if (currentQuarter <= 1)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                    }
                                    else if (row.RoomsAllocated + roomPool > row.ActualDemand)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                        roomPool = roomPool - row.ActualDemand + row.RoomsAllocated;
                                        if (roomPool < 0)
                                            roomPool = 0;
                                    }
                                    else if (row.RoomsAllocated + roomPool == row.ActualDemand)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                        roomPool = 0;
                                    }
                                    else if (row.RoomsAllocated + roomPool < row.ActualDemand)
                                    {
                                        row.RoomsSold = row.RoomsAllocated + roomPool;
                                        roomPool = 0;
                                    }
                                }
                            }
                            RoomAllocationUpdate(row);
                        }
                    }
                    if (roomPool > 0)
                    {
                        foreach (RoomAllocationDto row in table)
                        {
                            if (row.Segment == "Families")
                            {
                                if (row.RoomsAllocated > row.ActualDemand)
                                {
                                    row.RoomsSold = row.ActualDemand;
                                }
                                if (row.RoomsAllocated == row.ActualDemand)
                                {
                                    row.RoomsSold = row.ActualDemand;
                                }
                                if (row.RoomsAllocated < row.ActualDemand)
                                {
                                    if (currentQuarter <= 1)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                    }
                                    else if (row.RoomsAllocated + roomPool > row.ActualDemand)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                        roomPool = roomPool - row.ActualDemand + row.RoomsAllocated;
                                        if (roomPool < 0)
                                            roomPool = 0;
                                    }
                                    else if (row.RoomsAllocated + roomPool == row.ActualDemand)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                        roomPool = 0;
                                    }
                                    else if (row.RoomsAllocated + roomPool < row.ActualDemand)
                                    {
                                        row.RoomsSold = row.RoomsAllocated + roomPool;
                                        roomPool = 0;
                                    }
                                }
                            }
                            RoomAllocationUpdate(row);
                        }
                    }
                    if (roomPool > 0)
                    {
                        foreach (RoomAllocationDto row in table)
                        {
                            if (row.Segment == "Corporate/Business Meetings")
                            {
                                if (row.RoomsAllocated > row.ActualDemand)
                                {
                                    row.RoomsSold = row.ActualDemand;
                                }
                                if (row.RoomsAllocated == row.ActualDemand)
                                {
                                    row.RoomsSold = row.ActualDemand;
                                }
                                if (row.RoomsAllocated < row.ActualDemand)
                                {
                                    if (currentQuarter <= 1)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                    }
                                    else if (row.RoomsAllocated + roomPool > row.ActualDemand)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                        roomPool = roomPool - row.ActualDemand + row.RoomsAllocated;
                                        if (roomPool < 0)
                                            roomPool = 0;
                                    }
                                    else if (row.RoomsAllocated + roomPool == row.ActualDemand)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                        roomPool = 0;
                                    }
                                    else if (row.RoomsAllocated + roomPool < row.ActualDemand)
                                    {
                                        row.RoomsSold = row.RoomsAllocated + roomPool;
                                        roomPool = 0;
                                    }
                                }
                            }
                            RoomAllocationUpdate(row);
                        }
                    }
                    if (roomPool > 0)
                    {
                        foreach (RoomAllocationDto row in table)
                        {
                            if (row.Segment == "Association Meetings")
                            {
                                if (row.RoomsAllocated > row.ActualDemand)
                                {
                                    row.RoomsSold = row.ActualDemand;
                                }
                                if (row.RoomsAllocated == row.ActualDemand)
                                {
                                    row.RoomsSold = row.ActualDemand;
                                }
                                if (row.RoomsAllocated < row.ActualDemand)
                                {
                                    if (currentQuarter <= 1)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                    }
                                    else if (row.RoomsAllocated + roomPool > row.ActualDemand)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                        roomPool = roomPool - row.ActualDemand + row.RoomsAllocated;
                                        if (roomPool < 0)
                                            roomPool = 0;
                                    }
                                    else if (row.RoomsAllocated + roomPool == row.ActualDemand)
                                    {
                                        row.RoomsSold = row.ActualDemand;
                                        roomPool = 0;
                                    }
                                    else if (row.RoomsAllocated + roomPool < row.ActualDemand)
                                    {
                                        row.RoomsSold = row.RoomsAllocated + roomPool;
                                        roomPool = 0;
                                    }
                                }
                            }
                            RoomAllocationUpdate(row);
                        }

                    }
                    // RoomAllocationUpdate(Row);
                    ////Slow down the calucation to give database more time to process, wait 1/10 second
                    System.Threading.Thread.Sleep(10);
                }
                {

                    List<SoldRoomByChannelDto> soldChanTable = GetDataByMonthSoldRoomByChannel(monthId, currentQuarter);


                    decimal directSold;
                    decimal travelSold;
                    decimal onlineSold;
                    decimal opaqueSold;
                    decimal thisSold;
                    decimal roomAlloSold;

                    decimal sum;

                    foreach (SoldRoomByChannelDto row in soldChanTable)
                    {
                        directSold = GetDataBySingleRowPriceDecision(row.MonthID, row.QuarterNo, row.GroupID, row.Weekday, "Direct", row.Segment).ActualDemand;
                        travelSold = GetDataBySingleRowPriceDecision(row.MonthID, row.QuarterNo, row.GroupID, row.Weekday, "Travel Agent", row.Segment).ActualDemand;
                        onlineSold = GetDataBySingleRowPriceDecision(row.MonthID, row.QuarterNo, row.GroupID, row.Weekday, "Online Travel Agent", row.Segment).ActualDemand;
                        opaqueSold = GetDataBySingleRowPriceDecision(row.MonthID, row.QuarterNo, row.GroupID, row.Weekday, "Opaque", row.Segment).ActualDemand;
                        thisSold = GetDataBySingleRowPriceDecision(row.MonthID, row.QuarterNo, row.GroupID, row.Weekday, row.Channel, row.Segment).ActualDemand;
                        roomAlloSold = GetDataByEachDecisionRoomAllocation(row.MonthID, row.QuarterNo, row.GroupID, row.Weekday, row.Segment).RoomsSold;

                        sum = directSold + travelSold + onlineSold + opaqueSold;

                        if (sum == 0)
                        {
                            row.SoldRoom = 0;
                            row.Revenue = 0;
                            row.Cost = 0;
                        }
                        else
                        {
                            row.SoldRoom = Convert.ToInt32(roomAlloSold * thisSold / sum);

                        }
                        SoldRoomByChannelUpdate(row);
                    }

                    ////Slow down the calucation to give database more time to process, wait 1/10 second
                    System.Threading.Thread.Sleep(10);

                    /////////////////////////////////
                    /////////Set revenue and cost
                    //////////////////////////////
                    //List<SoldRoomByChannelDto> soldChanTable = GetDataByMonthSoldRoomByChannel(monthId, currentQuarter);
                    foreach (SoldRoomByChannelDto row in soldChanTable)
                    {
                        row.Revenue = Convert.ToInt16(ScalarSingleRevenueSoldRoomByChannel(row.MonthID, row.QuarterNo, row.GroupID, row.Segment, row.Channel, row.Weekday));
                        row.Cost = Convert.ToInt16(ScalarSingleCostSoldRoomByChannel(row.MonthID, row.QuarterNo, row.GroupID, row.Segment, row.Channel, row.Weekday));
                        SoldRoomByChannelUpdate(row);
                    }


                    ////Slow down the calucation to give database more time to process, wait 1/10 second
                    System.Threading.Thread.Sleep(10);
                }

                {


                    IncomeState incomStatAdpt = new IncomeState();
                    BalanceSheet balanTableAdpt = new BalanceSheet();
                    RoomAllocation roomAlloAdpt = new RoomAllocation();
                    SoldRoomByChannel roomChanAdpt = new SoldRoomByChannel();
                    ClassSession classAdapter = new ClassSession();

                    BalanceSheetDto balanTableRow;

                    List<IncomeStateDto> incoTable = GetDataByMonthIncomeState(monthId, currentQuarter);

                    int groupNo = Convert.ToInt32(ScalarQueryFindNoOfHotels(monthId));

                    for (int c = 1; c < groupNo + 1; c++)
                    {
                        IncomeStateDto incomStaRow = GetDataBySingleRowIncomeState(monthId, currentQuarter, c);

                        int roomRevenue = ScalarGroupRoomRevenueByMonthSoldRoomByChannel(monthId, currentQuarter, incomStaRow.GroupID);

                        ///Revenue_Room Revenue
                        incomStaRow.Room = roomRevenue;

                        ///Revenue_Food and Beverage Total
                        incomStaRow.FoodB1 = Convert.ToInt16(31 * roomRevenue / 52);

                        ////revenue by attribute under Food and Beverage Section
                        decimal restaurantScore = Convert.ToDecimal(ScalarAttributeRevenueScoreRoomAllocation(monthId, currentQuarter, incomStaRow.GroupID, "Resturants"));
                        decimal BarScore = Convert.ToDecimal(ScalarAttributeRevenueScoreRoomAllocation(monthId, currentQuarter, incomStaRow.GroupID, "Bars"));
                        decimal roomServiceScore = Convert.ToDecimal(ScalarAttributeRevenueScoreRoomAllocation(monthId, currentQuarter, incomStaRow.GroupID, "Room Service"));
                        decimal banquetScore = Convert.ToDecimal(ScalarAttributeRevenueScoreRoomAllocation(monthId, currentQuarter, incomStaRow.GroupID, "Banquet & Catering"));
                        decimal meetRoomScore = Convert.ToDecimal(ScalarAttributeRevenueScoreRoomAllocation(monthId, currentQuarter, incomStaRow.GroupID, "Meeting Rooms"));
                        decimal foodBTotalScore = restaurantScore + BarScore + roomServiceScore + banquetScore + meetRoomScore;

                        if (foodBTotalScore == 0)
                        {
                            ///////////////////////////////
                            ///////////To Avoid foodBTotal is zero, avoid divided by zero exception
                            incomStaRow.FoodB1 = 0;
                            incomStaRow.FoodB2 = 0;
                            incomStaRow.FoodB3 = 0;
                            incomStaRow.FoodB4 = 0;
                            incomStaRow.FoodB5 = 0;
                        }
                        else
                        {
                            incomStaRow.FoodB1 = incomStaRow.FoodB * restaurantScore / foodBTotalScore;
                            incomStaRow.FoodB2 = incomStaRow.FoodB * BarScore / foodBTotalScore;
                            incomStaRow.FoodB3 = incomStaRow.FoodB * roomServiceScore / foodBTotalScore;
                            incomStaRow.FoodB4 = incomStaRow.FoodB * banquetScore / foodBTotalScore;
                            incomStaRow.FoodB5 = incomStaRow.FoodB * meetRoomScore / foodBTotalScore;
                        }

                        ///Revenue_Other Operated Departments
                        incomStaRow.Other = 12 * roomRevenue / 52;

                        decimal golfScore = Convert.ToDecimal(ScalarAttributeRevenueScoreRoomAllocation(monthId, currentQuarter, incomStaRow.GroupID, "Golf Course"));
                        decimal spaScore = Convert.ToDecimal(ScalarAttributeRevenueScoreRoomAllocation(monthId, currentQuarter, incomStaRow.GroupID, "Spa"));
                        decimal fitnessScore = Convert.ToDecimal(ScalarAttributeRevenueScoreRoomAllocation(monthId, currentQuarter, incomStaRow.GroupID, "Fitness Center"));
                        decimal businessScore = Convert.ToDecimal(ScalarAttributeRevenueScoreRoomAllocation(monthId, currentQuarter, incomStaRow.GroupID, "Business Center"));
                        decimal otherRecreScore = Convert.ToDecimal(ScalarAttributeRevenueScoreRoomAllocation(monthId, currentQuarter, incomStaRow.GroupID, "Other Recreation Facilities - Pools, game rooms, tennis courts, ect"));
                        decimal entertainScore = Convert.ToDecimal(ScalarAttributeRevenueScoreRoomAllocation(monthId, currentQuarter, incomStaRow.GroupID, "Entertainment"));
                        decimal otherOperatedTotal = golfScore + spaScore + fitnessScore + businessScore + otherRecreScore + entertainScore;

                        if (otherOperatedTotal == 0)
                        {
                            incomStaRow.Other1 = 0;
                            incomStaRow.Other2 = 0;
                            incomStaRow.Other3 = 0;
                            incomStaRow.Other4 = 0;
                            incomStaRow.Other5 = 0;
                            incomStaRow.Other6 = 0;
                        }
                        else
                        {
                            ///////////////////////////////
                            ///////////To Avoid foodBTotal is zero, avoid divided by zero exception
                            ///////////////////////////////////////////////////////////
                            incomStaRow.Other1 = incomStaRow.Other * golfScore / otherOperatedTotal;
                            incomStaRow.Other2 = incomStaRow.Other * spaScore / otherOperatedTotal;
                            incomStaRow.Other3 = incomStaRow.Other * fitnessScore / otherOperatedTotal;
                            incomStaRow.Other4 = incomStaRow.Other * businessScore / otherOperatedTotal;
                            incomStaRow.Other5 = incomStaRow.Other * otherRecreScore / otherOperatedTotal;
                            incomStaRow.Other6 = incomStaRow.Other * entertainScore / otherOperatedTotal;
                        }

                        ///Revenue_Rental and other Income
                        incomStaRow.Rent = 5 * roomRevenue / 52;

                        ///Revenue_Total Revenue
                        incomStaRow.TotReven = 100 * roomRevenue / 52;

                        ///Departmental Expenses from Rooms
                        // attributeDecisionTableAdapter attriDecisionAdpt = new attributeDecisionTableAdapter();
                        //hotelSimulator.attributeDecisionRow attriDecisionRow;

                        AttributeDecisionDto attriDecisionRow = GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Guest Rooms");
                        incomStaRow.Room1 = attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                        attriDecisionRow = GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Reservations");
                        incomStaRow.Room1 = incomStaRow.Room1 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                        attriDecisionRow = GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Guest Check in/Guest Check out");
                        incomStaRow.Room1 = incomStaRow.Room1 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                        attriDecisionRow = GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Concierge");
                        incomStaRow.Room1 = incomStaRow.Room1 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                        attriDecisionRow = GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Housekeeping");
                        incomStaRow.Room1 = incomStaRow.Room1 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                        //attriDecisionRow = GetDataBySingleRow(sessionID, quarterNo, incomStaRow.groupID, "Maintanence and security")[0];
                        //incomStaRow._2Room = incomStaRow._2Room + attriDecisionRow.laborBudget + attriDecisionRow.operationBudget;
                        //attriDecisionRow = GetDataBySingleRow(sessionID, quarterNo, incomStaRow.groupID, "Courtesy (Rooms)")[0];
                        //incomStaRow._2Room = incomStaRow._2Room + attriDecisionRow.laborBudget + attriDecisionRow.operationBudget;

                        ///Departmental Expenses from Food and Beverage
                        attriDecisionRow = GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Resturants");
                        incomStaRow.FoodB2 = attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                        attriDecisionRow = GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Bars");
                        incomStaRow.FoodB2 = incomStaRow.FoodB2 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                        attriDecisionRow = GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Room Service");
                        incomStaRow.FoodB2 = incomStaRow.FoodB2 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                        attriDecisionRow = GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Banquet & Catering");
                        incomStaRow.FoodB2 = incomStaRow.FoodB2 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                        attriDecisionRow = GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Meeting Rooms");
                        incomStaRow.FoodB2 = incomStaRow.FoodB2 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                        //attriDecisionRow = GetDataBySingleRow(sessionID, quarterNo, incomStaRow.groupID, "Entertainment")[0];
                        //incomStaRow._2FoodB = incomStaRow._2FoodB + attriDecisionRow.laborBudget + attriDecisionRow.operationBudget;
                        //attriDecisionRow = GetDataBySingleRow(sessionID, quarterNo, incomStaRow.groupID, "Courtesy(FB)")[0];
                        //incomStaRow._2FoodB = incomStaRow._2FoodB + attriDecisionRow.laborBudget + attriDecisionRow.operationBudget;

                        ///Expenses from other operation department such as spa, fitness center etc.
                        attriDecisionRow = GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Spa");
                        incomStaRow.Other2 = attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                        attriDecisionRow = GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Fitness Center");
                        incomStaRow.Other2 = incomStaRow.Other2 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                        attriDecisionRow = GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Business Center");
                        incomStaRow.Other2 = incomStaRow.Other2 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                        attriDecisionRow = GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Golf Course");
                        incomStaRow.Other2 = incomStaRow.Other2 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                        attriDecisionRow = GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Other Recreation Facilities - Pools, game rooms, tennis courts, ect");
                        incomStaRow.Other2 = incomStaRow.Other2 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                        attriDecisionRow = GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Entertainment");
                        incomStaRow.Other2 = incomStaRow.Other2 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;

                        ///Expenses TOTAL
                        incomStaRow.TotExpen = incomStaRow.Room1 + incomStaRow.FoodB2 + incomStaRow.Other2;

                        ///Total Departmental Income
                        ///////room revenue *100/52 is the total revenue
                        incomStaRow.TotDeptIncom = 100 * roomRevenue / 52 - incomStaRow.TotExpen;

                        ///Undistributed Expenses from Administrative and General or mangement sales attention
                        ///8% of the total revenue.
                        //////Revision: according to Gursoy's email Monday, April 27, 2015 3:58 PM, 
                        //////courtesy and management/sales attention should be listed under administrative and general (which is undisExpens1)
                        //////incomStaRow._4UndisExpens1 = 2 * roomRevenue / 13;
                        attriDecisionRow = GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Courtesy(FB)");
                        incomStaRow.UndisExpens1 = 2 * roomRevenue / 13 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                        attriDecisionRow = GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Management/Sales Attention");
                        incomStaRow.UndisExpens1 = incomStaRow.UndisExpens1 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;

                        ///Undistributed Expenses from Salses and Marketing
                        incomStaRow.UndisExpens2 = ScalarQueryTotalSpendingMarketingDecision(monthId, currentQuarter, incomStaRow.GroupID);

                        ///Undistributed Expenses from Distributional Channels
                        incomStaRow.UndisExpens3 = Convert.ToDecimal(ScalarGroupDistriCostByMonthSoldByChannel(monthId, currentQuarter, incomStaRow.GroupID));

                        ///Undistributed Expenses Property Operation and Maintenance (5.5 % of total revenue plus the labor and other from mantainance and security, building)
                        incomStaRow.UndisExpens4 = 11 * roomRevenue / 104;
                        attriDecisionRow = GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Maintanence and security");
                        incomStaRow.UndisExpens4 = incomStaRow.UndisExpens4 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                        attriDecisionRow = GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Courtesy (Rooms)");
                        incomStaRow.UndisExpens4 = incomStaRow.UndisExpens4 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;


                        ///Undistributed Expenses from untilities
                        incomStaRow.UndisExpens5 = 11 * roomRevenue / 130;

                        ///Undistributed Expenses TOTAL
                        incomStaRow.UndisExpens6 = incomStaRow.UndisExpens1 + incomStaRow.UndisExpens2 + incomStaRow.UndisExpens3 + incomStaRow.UndisExpens4 + incomStaRow.UndisExpens5;

                        ///5.Gross operating profit
                        incomStaRow.GrossProfit = incomStaRow.TotDeptIncom - incomStaRow.UndisExpens6;

                        ///6.Managment Fee
                        incomStaRow.MgtFee = 5 * roomRevenue / 104;

                        ///7.Income before fixed charges
                        incomStaRow.IncomBfCharg = incomStaRow.GrossProfit - incomStaRow.MgtFee;

                        ///8.1 Property and other taxes
                        incomStaRow.Property = 4 * roomRevenue / 65;

                        ///8.2 Insurance
                        incomStaRow.Insurance = 3 * roomRevenue / 104;

                        ///8.3 Insterests 
                        ///Right now is fake value, the real value should come from (currentquarter -1) balance sheet 
                        balanTableRow = GetDataBySingleRowBallanceSheet(incomStaRow.MonthID, incomStaRow.QuarterNo, incomStaRow.GroupID);
                        incomStaRow.Interest = balanTableRow.LongDebt * 7 / 1000 + balanTableRow.ShortDebt * 3 / 100;

                        ///8.4 Depriciation
                        incomStaRow.PropDepreciationerty = ScalarMonthDepreciationTotalAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID);

                        ///8 Fixed Charge Total
                        incomStaRow.TotCharg = incomStaRow.Property + incomStaRow.Insurance + incomStaRow.Interest + incomStaRow.PropDepreciationerty;

                        ///9. Net Operating Income (before TAX)
                        incomStaRow.NetIncomBfTAX = incomStaRow.IncomBfCharg - incomStaRow.TotCharg;

                        ///10.LESS: replacement reserves
                        incomStaRow.Replace = Convert.ToDecimal(ScalarMonthlyTotalNewCapitalAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID));

                        ///11. Adjusted.......
                        incomStaRow.AjstNetIncom = incomStaRow.NetIncomBfTAX - incomStaRow.Replace;

                        ///income TAX
                        if (incomStaRow.NetIncomBfTAX < 0)
                        {
                            incomStaRow.IncomTAX = 0;
                        }
                        else
                        {
                            incomStaRow.IncomTAX = incomStaRow.NetIncomBfTAX / 4;
                        }

                        ///NET INCOME
                        incomStaRow.NetIncom = incomStaRow.NetIncomBfTAX - incomStaRow.IncomTAX;


                        /////Repeat one more time to input the total Revenue
                        incomStaRow.TotReven = 100 * roomRevenue / 52;

                        ////////Update the income statement row by row rather than the entire table, to avoide the same total income issue. 
                        IncomeStateUpdate(incomStaRow);
                        ////Slow down the calucation to give database more time to process, wait 1/10 second
                        System.Threading.Thread.Sleep(10);

                    }
                    //////////////////
                    ///////Update Database///
                    /////////incomStatAdpt.Update(incoTable);

                    ////////////////////////////////////////
                    ////The following code is just to ensure the Total Revenue is input correctly
                    /////This check only makes sense when current month is greater than 1
                    ////////////////////////////////////////

                    if (currentQuarter > 1)
                    {
                        //incomeState1TableAdapter income1Adpt = new incomeState1TableAdapter();
                        decimal totalRevThisMonth;
                        decimal totalRevPrevMonth;

                        List<IncomeStateDto> income1Table = GetDataByMonthIncomeState(monthId, currentQuarter);
                        foreach (IncomeStateDto incom1StaRow in income1Table)
                        {
                            totalRevThisMonth = Convert.ToDecimal(ScalarGetTotalRevenueIncomeState(monthId, incom1StaRow.GroupID, currentQuarter));
                            totalRevPrevMonth = Convert.ToDecimal(ScalarGetTotalRevenueIncomeState(monthId, incom1StaRow.GroupID, currentQuarter - 1));
                            if (totalRevPrevMonth == totalRevThisMonth)
                            {
                                decimal roomRevenue = Convert.ToDecimal(ScalarGroupRoomRevenueByMonthSoldRoomByChannel(monthId, currentQuarter, incom1StaRow.GroupID));
                                incom1StaRow.TotReven = 100 * roomRevenue / 52;
                                IncomeStateUpdate(incom1StaRow);
                                ////Slow down the calucation to give database more time to process, wait 1/10 second
                                System.Threading.Thread.Sleep(10);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                String exp = ex.ToString();

            }
            resObj.Message = "Calculation Success";
            resObj.StatusCode = 200;
            string strjson = "{ monthID:" + month.MonthId + "}";
            var jobj = JsonConvert.DeserializeObject<MonthDto>(strjson)!;
            resObj.Data = jobj;
            return resObj;
        }

        public Task<ResponseData> Create(MonthDto month)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MonthDto> List(string monthidId = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MonthDto> monthList()
        {
            throw new NotImplementedException();
        }

        private bool MarketingDecisionUpdate(MarketingDecisionDto pObj)
        {
            bool result = false;
            try
            {
                MarketingDecision objMd = new MarketingDecision
                {
                    ID = pObj.ID,
                    MonthID = pObj.MonthID,
                    QuarterNo = pObj.QuarterNo,
                    GroupID = pObj.GroupID,
                    MarketingTechniques = pObj.MarketingTechniques,
                    Segment = pObj.Segment,
                    Spending = pObj.Spending,
                    LaborSpending = pObj.LaborSpending,
                    ActualDemand = pObj.ActualDemand,
                    Confirmed = pObj.Confirmed,
                };
                _context.MarketingDecision.Add(objMd);
                _context.Entry(objMd).State = EntityState.Modified;
                _context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;

        }
        public List<PriceDecisionDto> GetDataByQuarterPriceDecision(int monthId, int quartorNo)
        {


            List<PriceDecisionDto> objlist = _context.PriceDecision.Where(x => x.MonthID == monthId && x.QuarterNo == quartorNo).
                Select(x => new PriceDecisionDto
                {

                    MonthID = x.MonthID,
                    QuarterNo = x.QuarterNo,
                    GroupID = x.GroupID,
                    Weekday = x.Weekday,
                    DistributionChannel = x.DistributionChannel,
                    Segment = x.Segment,
                    Price = x.Price,
                    ActualDemand = x.ActualDemand,
                    Confirmed = x.Confirmed,
                }
                ).ToList();
            return objlist;

        }
        private decimal ScalarQueryAvgPricePriceDecision(int monthId, int quarterNo, bool weekday, string distributionChannel, string segment)
        {

            var list = (from m in _context.PriceDecision.Where(x => x.MonthID == monthId
                        && x.QuarterNo == quarterNo
                        && x.Weekday == weekday
                        && x.DistributionChannel == distributionChannel.Trim()
                        && x.Segment == segment.Trim())
                        select new { averagePrice = m.Price }).ToList();
            decimal averagePrice = 0;
            if (list.Count > 0)
            {
                averagePrice = list[0].averagePrice;
            }
            return averagePrice;
        }

        private decimal ScalarQueryPriceExpectationPriceDecision(int monthId, int quarterNo, string segment, bool weekday)
        {

            decimal PriceExpectation = 0;
            var list = (from m in _context.Months
                        join wsc in _context.WeekdayVSsegmentConfig on m.ConfigId equals wsc.ConfigID
                        select new
                        {
                            MonthId = m.MonthId,
                            QuarterNo = m.Sequence,
                            Segment = wsc.Segment,
                            Weekday = wsc.WeekDay,
                            PriceExpect = wsc.PriceExpectation
                        }).Where(x => x.MonthId == monthId && x.QuarterNo == quarterNo && x.Segment == segment && x.Weekday == weekday).ToList();

            if (list.Count > 0)
            {
                PriceExpectation = list[0].PriceExpect;
            }
            return PriceExpectation;
        }

        private decimal ScalarQueryFairMarketPriceDecision(string segment, bool weekday, string distributionChannel, int monthId, int quarterNo)
        {

            decimal FairMarket = 0;
            var list = (from pd in _context.PriceDecision
                        join m in _context.Months on pd.MonthID equals m.MonthId
                        where (pd.QuarterNo == m.Sequence)
                        join pmasc in _context.PriceMarketingAttributeSegmentConfig on m.ConfigId equals pmasc.ConfigID
                        where (pd.Segment == pmasc.Segment)
                        join sc in _context.SegmentConfig on pmasc.Segment equals sc.Segment
                        join wvsc in _context.WeekdayVSsegmentConfig on pd.Weekday equals wvsc.WeekDay
                        where (pd.Segment == wvsc.Segment && m.ConfigId == wvsc.ConfigID)
                        join dcvsc in _context.DistributionChannelVSsegmentConfig on m.ConfigId equals dcvsc.ConfigID
                        where (pd.Segment == dcvsc.Segment && pd.DistributionChannel == dcvsc.DistributionChannel)
                        join c in _context.ClassSessions on m.ClassId equals c.ClassId
                        where (pmasc.Segment == segment && pmasc.PMA == "Price" && pd.Weekday == weekday && pd.DistributionChannel == distributionChannel
                        && pd.QuarterNo == quarterNo && pd.MonthID == monthId)
                        select new
                        {
                            FairMarket = m.TotalMarket * Convert.ToDecimal(sc.Percentage) * wvsc.Percentage * pmasc.Percentage * dcvsc.Percentage / c.HotelsCount,

                        }).ToList();

            if (list.Count > 0)
            {
                FairMarket = list[0].FairMarket;
            }
            return FairMarket;
        }

        private bool PriceDecisionUpdate(PriceDecisionDto pObj)
        {
            bool result = false;
            try
            {
                PriceDecision objPd = new PriceDecision
                {
                    ID = pObj.ID,
                    MonthID = pObj.MonthID,
                    QuarterNo = pObj.QuarterNo,
                    GroupID = pObj.GroupID,
                    Weekday = pObj.Weekday,
                    Segment = pObj.Segment,
                    DistributionChannel = pObj.DistributionChannel,
                    Price = pObj.Price,
                    ActualDemand = pObj.ActualDemand,
                    Confirmed = pObj.Confirmed,
                };
                _context.PriceDecision.Add(objPd);
                _context.Entry(objPd).State = EntityState.Modified;
                _context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;

        }
        private List<IncomeStateDto> GetDataByMonthIncomeState(int monthId, int quarterNo)
        {
            List<IncomeStateDto> list = _context.IncomeState.Where(x => x.MonthID == monthId && x.QuarterNo == quarterNo)
                       .Select(x => new IncomeStateDto
                       {
                           MonthID = x.MonthID,
                           QuarterNo = x.QuarterNo,
                           GroupID = x.GroupID,
                           Room1 = x.Room1,
                           FoodB = x.FoodB,
                           FoodB1 = x.FoodB1,
                           Food2B = x.FoodB2,
                           FoodB3 = x.FoodB3,
                           FoodB4 = x.FoodB4,
                           FoodB5 = x.FoodB5,
                           Other = x.Other,
                           Other1 = x.Other1,
                           Other2 = x.Other2,
                           Other3 = x.Other3,
                           Other4 = x.Other4,
                           Other5 = x.Other5,
                           Other6 = x.Other6,
                           Other7 = x.Other7,
                           Rent = x.Rent,
                           TotReven = x.TotReven,
                           Room = x.Room,
                           FoodB2 = x.FoodB2,
                           TotExpen = x.TotExpen,
                           TotDeptIncom = x.TotDeptIncom,
                           UndisExpens1 = x.UndisExpens1,
                           UndisExpens2 = x.UndisExpens2,
                           UndisExpens3 = x.UndisExpens3,
                           UndisExpens4 = x.UndisExpens4,
                           UndisExpens5 = x.UndisExpens5,
                           UndisExpens6 = x.UndisExpens6,
                           GrossProfit = x.GrossProfit,
                           MgtFee = x.MgtFee,
                           IncomBfCharg = x.IncomBfCharg,
                           Insurance = x.Insurance,
                           Interest = x.Interest,
                           PropDepreciationerty = x.PropDepreciationerty,
                           TotCharg = x.TotCharg,
                           NetIncomBfTAX = x.NetIncomBfTAX,
                           Replace = x.Replace,
                           AjstNetIncom = x.AjstNetIncom,
                           IncomTAX = x.IncomTAX,
                           NetIncom = x.NetIncom

                       }
                       ).ToList();

            return list;

        }
        private IncomeStateDto GetDataBySingleRowIncomeState(int monthId, int quarterNo, int groupId)
        {

            var list = _context.IncomeState.Where(x => x.MonthID == monthId && x.QuarterNo == quarterNo && x.GroupID == groupId).
                Select(x => new IncomeStateDto
                {
                    Replace = x.Replace,
                    AjstNetIncom = x.AjstNetIncom,
                    IncomTAX = x.IncomTAX,
                    NetIncom = x.NetIncom,
                    FoodB = x.FoodB,
                    FoodB1 = x.FoodB1,
                    FoodB2 = x.FoodB2,
                    FoodB3 = x.FoodB3,
                    FoodB4 = x.FoodB4,
                    FoodB5 = x.FoodB5,
                    Other = x.Other,
                    Other1 = x.Other1,
                    Other2 = x.Other2,
                    Other3 = x.Other3,
                    Other4 = x.Other4,
                    Other5 = x.Other5,
                    Other6 = x.Other6,
                    Other7 = x.Other7,
                    Rent = x.Rent,
                    TotReven = x.TotReven,
                    Room = x.Room,

                    TotExpen = x.TotExpen,
                    TotDeptIncom = x.TotDeptIncom,
                    UndisExpens1 = x.UndisExpens1,
                    UndisExpens2 = x.UndisExpens2,
                    UndisExpens3 = x.UndisExpens3,
                    UndisExpens4 = x.UndisExpens4,
                    UndisExpens5 = x.UndisExpens5,
                    UndisExpens6 = x.UndisExpens6,
                    GrossProfit = x.GrossProfit,
                    MgtFee = x.MgtFee,
                    IncomBfCharg = x.IncomBfCharg,
                    Insurance = x.Insurance,
                    Interest = x.Interest,
                    PropDepreciationerty = x.PropDepreciationerty,
                    TotCharg = x.TotCharg,
                    NetIncomBfTAX = x.NetIncomBfTAX,


                }).ToList();

            return list[0];
        }


        private bool IncomeStateUpdate(IncomeStateDto pObj)
        {
            bool result = false;
            try
            {
                IncomeState objPd = new IncomeState
                {
                    Replace = (int)pObj.Replace,
                    AjstNetIncom = (int)pObj.AjstNetIncom,
                    IncomTAX = (int)pObj.IncomTAX,
                    NetIncom = (int)pObj.NetIncom,
                    FoodB = (int)pObj.FoodB,
                    FoodB1 = (int)pObj.FoodB1,
                    FoodB2 = (int)pObj.FoodB2,
                    FoodB3 = (int)pObj.FoodB3,
                    FoodB4 = (int)pObj.FoodB4,
                    FoodB5 = (int)pObj.FoodB5,
                    Other = (int)pObj.Other,
                    Other1 = (int)pObj.Other1,
                    Other2 = (int)pObj.Other2,
                    Other3 = (int)pObj.Other3,
                    Other4 = (int)pObj.Other4,
                    Other5 = (int)pObj.Other5,
                    Other6 = (int)pObj.Other6,
                    Other7 = pObj.Other7,
                    Rent = pObj.Rent,
                    TotReven = (int)pObj.TotReven,
                    Room = pObj.Room,

                    TotExpen = (int)pObj.TotExpen,
                    TotDeptIncom = (int)pObj.TotDeptIncom,
                    UndisExpens1 = (int)pObj.UndisExpens1,
                    UndisExpens2 = (int)pObj.UndisExpens2,
                    UndisExpens3 = (int)pObj.UndisExpens3,
                    UndisExpens4 = (int)pObj.UndisExpens4,
                    UndisExpens5 = (int)pObj.UndisExpens5,
                    UndisExpens6 = (int)pObj.UndisExpens6,
                    GrossProfit = (int)pObj.GrossProfit,
                    MgtFee = pObj.MgtFee,
                    IncomBfCharg = (int)pObj.IncomBfCharg,
                    Insurance = pObj.Insurance,
                    Interest = pObj.Interest,
                    PropDepreciationerty = pObj.PropDepreciationerty,
                    TotCharg = pObj.TotCharg,
                    NetIncomBfTAX = (int)pObj.NetIncomBfTAX,
                };
                _context.IncomeState.Add(objPd);
                _context.Entry(objPd).State = EntityState.Modified;
                _context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;

        }

        private List<CustomerRawRatingDto> GetDataByQuarterCustomerRowRatting(int monthId, int quarterNo)
        {
            List<CustomerRawRatingDto> list = _context.CustomerRawRating.Where(x => x.MonthID == monthId && x.QuarterNo == quarterNo)
                       .Select(x => new CustomerRawRatingDto
                       {
                           MonthID = x.MonthID,
                           QuarterNo = x.QuarterNo,
                           GroupID = x.GroupID,
                           Attribute = x.Attribute,
                           RawRating = x.RawRating,
                           Segment = x.Segment


                       }
                       ).ToList();

            return list;

        }
        private AttributeDecisionDto GetDataBySingleRowAttributeDecision(int monthId, int quarterNo, int groupId, string attribute)
        {

            var list = _context.AttributeDecision.Where(x => x.MonthID == monthId && x.QuarterNo == quarterNo && x.GroupID == groupId && x.Attribute == attribute).
                Select(x => new AttributeDecisionDto
                {
                    QuarterNo = x.QuarterNo,
                    GroupID = x.GroupID,
                    Attribute = x.Attribute,
                    AccumulatedCapital = x.AccumulatedCapital,
                    NewCapital = x.NewCapital,
                    OperationBudget = x.OperationBudget,
                    LaborBudget = x.LaborBudget,
                    Confirmed = x.Confirmed,
                    QuarterForecast = x.QuarterForecast,
                    MonthID = x.MonthID,


                }).ToList();

            return list[0];
        }

        private decimal ScalarAttriSegIdealRating(int monthID, int quarterNo, string attribute, string segment)
        {

            decimal ideal = 0;
            var list = (from irawc in _context.IdealRatingAttributeWeightConfig
                        join m in _context.Months on irawc.ConfigID equals m.ConfigId
                        where (irawc.Attribute == attribute && irawc.Segment == segment && m.Sequence == quarterNo && m.MonthId == monthID)
                        select new
                        {
                            Ideal = irawc.IdealRating,

                        }).ToList();

            if (list.Count > 0)
            {
                ideal = list[0].Ideal;
            }
            return ideal;
        }

        //ScalarQueryRawRating

        private decimal ScalarQueryCustomerRawRating(int monthID, int quarterNo, int groupId, string attribute, string segment)
        {

            decimal ideal = 0;
            var list = (from crr in _context.CustomerRawRating
                        join irawc in _context.IdealRatingAttributeWeightConfig on crr.Segment equals irawc.Segment
                        where (crr.Attribute == irawc.Attribute)
                        join amcoc in _context.AttributeMaxCapitalOperationConfig on crr.Attribute equals amcoc.Attribute
                        where (irawc.ConfigID == amcoc.ConfigID)
                        join m in _context.Months on crr.MonthID equals m.MonthId
                        join ad in _context.AttributeDecision on crr.MonthID equals ad.MonthID
                        where (crr.QuarterNo == ad.MonthID && crr.GroupID == ad.GroupID && crr.Attribute == ad.Attribute)
                        join ins in _context.IncomeState on crr.MonthID equals ins.MonthID
                        where (m.MonthId == ins.MonthID && m.Sequence == ins.QuarterNo && crr.GroupID == ins.GroupID && crr.MonthID == monthID
                        && crr.QuarterNo == quarterNo && crr.GroupID == groupId && crr.Attribute == attribute && crr.Segment == segment)
                        select new
                        {
                            RawRating = (ad.AccumulatedCapital + ad.NewCapital / ((amcoc.MaxNewCapital))
                            * irawc.IdealRating * amcoc.NewCapitalPortion + ad.OperationBudget)
                            / amcoc.MaxOperation
                      * irawc.IdealRating * amcoc.OperationPortion + ad.LaborBudget
                      / ins.TotReven
                      / amcoc.PreLaborPercent * irawc.IdealRating * amcoc.LaborPortion,

                        }).ToList();

            if (list.Count > 0)
            {
                ideal = list[0].RawRating;
            }
            return ideal;
        }
        private bool CustomerRowRatingUpdate(CustomerRawRatingDto pObj)
        {
            bool result = false;
            try
            {
                CustomerRawRating objPd = new CustomerRawRating
                {
                    MonthID = pObj.MonthID,
                    QuarterNo = pObj.QuarterNo,
                    GroupID = pObj.GroupID,
                    Attribute = pObj.Attribute,
                    Segment = pObj.Segment,
                    RawRating = (int)pObj.RawRating
                };
                _context.CustomerRawRating.Add(objPd);
                _context.Entry(objPd).State = EntityState.Modified;
                _context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;

        }

        private List<WeightedAttributeRatingDto> GetDataByQuarterWeightAttributeRating(int monthId, int quarterNo)
        {

            var list = _context.WeightedAttributeRating.Where(x => x.MonthID == monthId && x.QuarterNo == quarterNo).
                Select(x => new WeightedAttributeRatingDto
                {
                    QuarterNo = x.QuarterNo,
                    GroupID = x.GroupID,
                    MonthID = x.MonthID,
                    ActualDemand = x.ActualDemand,
                    CustomerRating = x.CustomerRating,
                    Segment = x.Segment


                }).ToList();

            return list;
        }
        private decimal ScalarQueryRatingBySegmentCustomerRawRatting(int monthID, int quarterNo, int groupID, string segment)
        {

            decimal WeightedRating = 0;
            var list = (from irawc in _context.IdealRatingAttributeWeightConfig
                        join m in _context.Months on irawc.ConfigID equals m.ConfigId
                        join crr in _context.CustomerRawRating on irawc.Attribute equals crr.Attribute
                        where (irawc.Segment == crr.Segment && m.MonthId == crr.MonthID && m.Sequence == crr.QuarterNo
                        && crr.MonthID == monthID && crr.QuarterNo == quarterNo && crr.GroupID == groupID && crr.Segment == segment
                        )
                        select new
                        {
                            WeightedRating = (crr.RawRating * irawc.Weight),

                        }).ToList();

            if (list.Count > 0)
            {
                WeightedRating = list[0].WeightedRating;
            }
            return WeightedRating;

        }
        private decimal ScalarQueryGetAvageRatingWeightAttributeRating(int monthID, int quarterNo, string segment)
        {


            var list = (from w in _context.WeightedAttributeRating.Where(x => x.MonthID == monthID && x.QuarterNo == quarterNo && x.Segment == segment)
                        select new { AverageRating = w.CustomerRating }).ToList();



            decimal AverageRating = 0;
            if (list.Count > 0)
            {
                AverageRating = list[0].AverageRating;
            }
            return AverageRating;

        }
        private decimal ScalarQueryFairMarketWeightAttributeRating(string segment, int monthID, int quarterNo)
        {

            decimal FairMarket = 0;
            var list = (from m in _context.Months
                        join sc in _context.SegmentConfig on m.ConfigId equals sc.ConfigID
                        join pmasc in _context.PriceMarketingAttributeSegmentConfig on m.ConfigId equals pmasc.ConfigID
                        where (sc.Segment == pmasc.Segment)
                        join c in _context.ClassSessions on m.ClassId equals c.ClassId
                        where (pmasc.PMA == "Attributes" && sc.Segment == segment && m.MonthId == monthID && m.Sequence == quarterNo)

                        select new
                        {
                            FairMarket = Convert.ToDecimal(sc.Percentage) * pmasc.Percentage * m.TotalMarket / c.HotelsCount,

                        }).ToList();

            if (list.Count > 0)
            {
                FairMarket = list[0].FairMarket;
            }
            return FairMarket;

        }
        private bool WeightedAttributeRatingUpdate(WeightedAttributeRatingDto pObj)
        {
            bool result = false;
            try
            {
                WeightedAttributeRating objPd = new WeightedAttributeRating
                {
                    MonthID = pObj.MonthID,
                    QuarterNo = pObj.QuarterNo,
                    GroupID = pObj.GroupID,
                    CustomerRating = Convert.ToInt16(pObj.CustomerRating),
                    Segment = pObj.Segment,
                    ActualDemand = (int)pObj.ActualDemand
                };
                _context.WeightedAttributeRating.Add(objPd);
                _context.Entry(objPd).State = EntityState.Modified;
                _context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;

        }

        private List<RoomAllocationDto> GetDataByQuarterRoomAllocation(int monthId, int quarterNo)
        {

            var list = _context.RoomAllocation.Where(x => x.MonthID == monthId && x.QuarterNo == quarterNo).
                Select(x => new RoomAllocationDto
                {
                    MonthID = x.MonthID,
                    QuarterNo = x.QuarterNo,
                    GroupID = x.GroupID,
                    Weekday = x.Weekday,
                    Segment = x.Segment,
                    RoomsAllocated = x.RoomsAllocated,
                    ActualDemand = x.ActualDemand,
                    RoomsSold = x.RoomsSold,
                    Confirmed = x.Confirmed,
                    Revenue = x.Revenue,
                    QuarterForecast = x.QuarterForecast,


                }).ToList();

            return list;
        }
        //ScalarQueryMarketingDemandBySegment
        private decimal ScalarQueryMarketingDemandBySegment(int monthId, int quarterNo, int groupID, string segment)
        {
            /*SELECT     SUM(actualDemand) AS MarketingDemand
FROM         marketingDecision
WHERE     (sessionID = @sessionID) AND (quarterNo = @quarterNo) AND (groupID = @groupID) AND (segment = @segment)*/
            decimal MarketingDemand = 0;
            var list = (from md in _context.MarketingDecision
                        where (md.MonthID == monthId && md.Segment == segment && md.MonthID == monthId && md.GroupID == groupID)

                        select new
                        {
                            MarketingDemand = md.ActualDemand,

                        }).ToList();

            if (list.Count > 0)
            {
                MarketingDemand = list[0].MarketingDemand;
            }
            return MarketingDemand;

        }

        private decimal ScalarQueryAttributeDemandBySegment(int monthId, int quarterNo, int groupID, string segment)
        {
            /*SELECT     SUM(actualDemand) AS AttributeDemand
FROM         weightedAttributeRating
WHERE     (sessionID = @sessionID) AND (quarterNo = @quarterNo) AND (groupID = @groupID) AND (segment = @segment)*/
            decimal AttributeDemand = 0;
            var list = (from md in _context.WeightedAttributeRating
                        where (md.MonthID == monthId && md.Segment == segment && md.QuarterNo == quarterNo && md.GroupID == groupID)

                        select new
                        {
                            AttributeDemand = md.ActualDemand,

                        }).ToList();

            if (list.Count > 0)
            {
                AttributeDemand = list[0].AttributeDemand;
            }
            return AttributeDemand;

        }
        //ScalarQueryPriceDemandBySegment
        private decimal ScalarQueryPriceDemandBySegment(int monthId, int quarterNo, int groupID, string segment, bool weekday)
        {
            /*SELECT     SUM(actualDemand) AS PriceDemand
FROM         priceDecision
WHERE     (sessionID = @sessionID) AND (quarterNo = @quarterNo) AND (groupID = @groupID) AND (segment = @segment) AND (weekday = @weekday)*/
            decimal PriceDemand = 0;
            var list = (from md in _context.PriceDecision
                        where (md.MonthID == monthId && md.Segment == segment && md.QuarterNo == quarterNo && Convert.ToInt16(md.GroupID) == groupID)

                        select new
                        {
                            PriceDemand = md.ActualDemand,

                        }).ToList();

            if (list.Count > 0)
            {
                PriceDemand = list[0].PriceDemand;
            }
            return PriceDemand;

        }

        private bool RoomAllocationUpdate(RoomAllocationDto pObj)
        {
            bool result = false;
            try
            {
                RoomAllocation objPd = new RoomAllocation
                {
                    MonthID = pObj.MonthID,
                    QuarterNo = pObj.QuarterNo,
                    GroupID = pObj.GroupID,
                    Weekday = pObj.Weekday,
                    Segment = pObj.Segment,
                    RoomsAllocated = pObj.RoomsAllocated,
                    ActualDemand = pObj.ActualDemand,
                    RoomsSold = pObj.RoomsSold,
                    Confirmed = pObj.Confirmed,
                    Revenue = pObj.Revenue,
                    QuarterForecast = pObj.QuarterForecast,
                };
                _context.RoomAllocation.Add(objPd);
                _context.Entry(objPd).State = EntityState.Modified;
                _context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;

        }
        //ScalarQueryMaxGroupNo

        private int ScalarQueryMaxGroupNoRommAllocation(int monthID, int quarterNo)
        {
            /*SELECT     COUNT(DISTINCT groupID) AS MaxGroup
FROM         roomAllocation
WHERE     (sessionID = @sessionID) AND (quarterNo = @quarterNo)*/
            int MaxGroup = 0;
            var list = (from r in _context.RoomAllocation
                        where (r.MonthID == monthID && r.QuarterNo == quarterNo)
                        group r by r.GroupID into g
                        select new
                        {
                            MaxGroup = g.Max(x => x.GroupID)

                        }).ToList();

            if (list.Count > 0)
            {
                MaxGroup = list[0].MaxGroup;
            }
            return MaxGroup;

        }

        //GetDataByGroupWeekday
        private List<RoomAllocationDto> GetDataByGroupWeekdayRoomAllocation(int monthId, int quarterNo, int groupId, bool weekday)
        {

            var list = _context.RoomAllocation.Where(x => x.MonthID == monthId && x.QuarterNo == quarterNo && x.GroupID == groupId && x.Weekday == weekday).
                Select(x => new RoomAllocationDto
                {
                    MonthID = x.MonthID,
                    QuarterNo = x.QuarterNo,
                    GroupID = x.GroupID,
                    Weekday = x.Weekday,
                    Segment = x.Segment,
                    RoomsAllocated = x.RoomsAllocated,
                    ActualDemand = x.ActualDemand,
                    RoomsSold = x.RoomsSold,
                    Confirmed = x.Confirmed,
                    Revenue = x.Revenue,
                    QuarterForecast = x.QuarterForecast,


                }).ToList();

            return list;
        }
        //GetDataByMonth
        private List<SoldRoomByChannelDto> GetDataByMonthSoldRoomByChannel(int monthId, int quarterNo)
        {

            var list = _context.SoldRoomByChannel.Where(x => x.MonthID == monthId && x.QuarterNo == quarterNo).
                Select(x => new SoldRoomByChannelDto
                {
                    MonthID = x.MonthID,
                    QuarterNo = x.QuarterNo,
                    GroupID = x.GroupID,
                    Weekday = x.Weekday,
                    Segment = x.Segment,
                    Revenue = x.Revenue,
                    Channel = x.Channel,
                    Cost = x.Cost,
                    SoldRoom = x.SoldRoom,
                }).ToList();

            return list;
        }
        private PriceDecisionDto GetDataBySingleRowPriceDecision(int monthId, int quarterNo, int groupId, bool weekday, string distributionChannel, string segment)
        {
            /*SELECT actualDemand, confirmed, distributionChannel, groupID, price, quarterNo, segment, sessionID, weekday FROM priceDecision 
             * WHERE (sessionID = @sessionID) AND (quarterNo = @quarterNo) AND (groupID = @groupID) AND (weekday = @weekday) AND (distributionChannel = @distributionChannel) AND (segment = @segment)*/
            var list = _context.PriceDecision.
                Where(x => x.MonthID == monthId && x.QuarterNo == quarterNo && x.GroupID == groupId.ToString()
                && x.DistributionChannel == distributionChannel && x.Segment == segment && x.Weekday == weekday).
                Select(x => new PriceDecisionDto
                {
                    QuarterNo = x.QuarterNo,
                    GroupID = x.GroupID,
                    MonthID = x.MonthID,
                    ActualDemand = x.ActualDemand,
                    Confirmed = x.Confirmed,
                    DistributionChannel = x.DistributionChannel,
                    Price = x.Price,
                    Segment = x.Segment,
                    Weekday = x.Weekday


                }).ToList();

            return list[0];
        }



        private RoomAllocationDto GetDataByEachDecisionRoomAllocation(int monthId, int quarterNo, int groupId, bool weekday, string segment)
        {
            /*SELECT actualDemand, confirmed, groupID, quarterForecast, quarterNo, revenue, roomsAllocated, 
             * roomsSold, segment, sessionID, weekday FROM roomAllocation 
             * WHERE (sessionID = @sessionID) 
             * AND (quarterNo = @quarterNo) AND (groupID = @groupID) AND (weekday = @weekday) AND (segment = @segment)*/

            var list = _context.RoomAllocation.
                Where(x => x.MonthID == monthId && x.QuarterNo == quarterNo && x.GroupID == groupId
                && x.Segment == segment && x.Weekday == weekday).
                Select(x => new RoomAllocationDto
                {
                    QuarterNo = x.QuarterNo,
                    GroupID = x.GroupID,
                    MonthID = x.MonthID,
                    ActualDemand = x.ActualDemand,
                    Confirmed = x.Confirmed,
                    Segment = x.Segment,
                    Weekday = x.Weekday,
                    QuarterForecast = x.QuarterForecast,
                    Revenue = x.Revenue,
                    RoomsAllocated = x.RoomsAllocated,
                    RoomsSold = x.RoomsSold


                }).ToList();

            return list[0];
        }
        private bool SoldRoomByChannelUpdate(SoldRoomByChannelDto pObj)
        {
            bool result = false;
            try
            {
                SoldRoomByChannel objPd = new SoldRoomByChannel
                {
                    MonthID = pObj.MonthID,
                    QuarterNo = pObj.QuarterNo,
                    GroupID = pObj.GroupID,
                    Weekday = pObj.Weekday,
                    Segment = pObj.Segment,
                    Revenue = pObj.Revenue,
                    Channel = pObj.Channel,
                    Cost = pObj.Cost,
                    SoldRoom = pObj.SoldRoom

                };
                _context.SoldRoomByChannel.Add(objPd);
                _context.Entry(objPd).State = EntityState.Modified;
                _context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;

        }


        //ScalarSingleRevenue

        private decimal ScalarSingleRevenueSoldRoomByChannel(int monthId, int quarterNo, int groupID, string segment, string channel, bool weekday)
        {

            decimal Revenue = 0;
            var list = (from dcvsc in _context.DistributionChannelVSsegmentConfig
                        join m in _context.Months on dcvsc.ConfigID equals m.ConfigId
                        join pd in _context.PriceDecision on m.MonthId equals pd.MonthID
                        where (m.Sequence == pd.QuarterNo)
                        join sbr in _context.SoldRoomByChannel on pd.MonthID equals sbr.MonthID
                        where (pd.QuarterNo == sbr.QuarterNo && pd.GroupID == sbr.GroupID.ToString() && pd.DistributionChannel == sbr.Channel
                        && pd.Segment == sbr.Segment && pd.Weekday == sbr.Weekday && dcvsc.Segment == sbr.Segment && dcvsc.DistributionChannel == sbr.Channel
                       && sbr.MonthID == monthId && sbr.GroupID == groupID && sbr.QuarterNo == quarterNo && sbr.Segment == segment
                       && sbr.Channel == channel && sbr.Weekday == weekday)

                        group new { pd, dcvsc, sbr } by new { pd.Price, dcvsc.CostPercent, sbr.SoldRoom } into dps
                        let firstgroup = dps.FirstOrDefault()
                        let priceDecision = firstgroup.pd
                        let distChanConfig = firstgroup.dcvsc
                        let soldChanel = firstgroup.sbr
                        select new
                        {
                            Revenue = priceDecision.Price * soldChanel.SoldRoom,

                        }).ToList();

            if (list.Count > 0)
            {
                Revenue = list[0].Revenue;
            }
            return Revenue;

        }
        //ScalarSingleCost
        private decimal ScalarSingleCostSoldRoomByChannel(int monthId, int quarterNo, int groupID, string segment, string channel, bool weekday)
        {

            decimal COST = 0;
            var list = (from dcvsc in _context.DistributionChannelVSsegmentConfig
                        join m in _context.Months on dcvsc.ConfigID equals m.ConfigId
                        join pd in _context.PriceDecision on m.MonthId equals pd.MonthID
                        where (m.Sequence == pd.QuarterNo)
                        join sbr in _context.SoldRoomByChannel on pd.MonthID equals sbr.MonthID
                        where (pd.QuarterNo == sbr.QuarterNo && pd.GroupID == sbr.GroupID.ToString() && pd.DistributionChannel == sbr.Channel
                        && pd.Segment == sbr.Segment && pd.Weekday == sbr.Weekday && dcvsc.Segment == sbr.Segment && dcvsc.DistributionChannel == sbr.Channel
                       && sbr.MonthID == monthId && sbr.GroupID == groupID && sbr.QuarterNo == quarterNo && sbr.Segment == segment
                       && sbr.Channel == channel && sbr.Weekday == weekday)

                        group new { pd, dcvsc, sbr } by new { pd.Price, dcvsc.CostPercent, sbr.SoldRoom } into dps
                        let firstgroup = dps.FirstOrDefault()
                        let priceDecision = firstgroup.pd
                        let distChanConfig = firstgroup.dcvsc
                        let soldChanel = firstgroup.sbr
                        select new
                        {
                            // priceDecision.price * distributionChannelVSsegmentConfig.costPercent * soldRoomByChannel.soldRoom
                            COST = priceDecision.Price * distChanConfig.CostPercent * soldChanel.SoldRoom,

                        }).ToList();

            if (list.Count > 0)
            {
                COST = list[0].COST;
            }
            return COST;

        }


        private int ScalarQueryFindNoOfHotels(int classId)
        {

            int noOfHotels = 0;
            var list = (from c in _context.ClassSessions
                        where (c.ClassId == classId)
                        select new
                        {
                            // priceDecision.price * distributionChannelVSsegmentConfig.costPercent * soldRoomByChannel.soldRoom
                            noOfHotels = c.HotelsCount

                        }).ToList();

            if (list.Count > 0)
            {
                noOfHotels = list[0].noOfHotels;
            }
            return noOfHotels;

        }

        private int ScalarGroupRoomRevenueByMonthSoldRoomByChannel(int monthId, int quarterNo, int groupId)
        {

            int groupRevenue = 0;
            var list = (from c in _context.SoldRoomByChannel
                        where (c.MonthID == monthId && c.QuarterNo == quarterNo && c.GroupID == groupId)
                        group c by c.Revenue into gpc
                        select new
                        {
                            groupRevenue = gpc.Sum(x => x.Revenue)

                        }).ToList();

            if (list.Count > 0)
            {
                groupRevenue = list[0].groupRevenue;
            }
            return groupRevenue;

        }
        private int ScalarAttributeRevenueScoreRoomAllocation(int monthId, int quarterNo, int groupId, string attribute)
        {

            int groupRevenue = 0;
            var list = (from c in _context.SoldRoomByChannel
                        where (c.MonthID == monthId && c.QuarterNo == quarterNo && c.GroupID == groupId)
                        group c by c.Revenue into gpc
                        select new
                        {
                            groupRevenue = gpc.Sum(x => x.Revenue)

                        }).ToList();

            if (list.Count > 0)
            {
                groupRevenue = list[0].groupRevenue;
            }
            return groupRevenue;

        }

        private decimal ScalarQueryTotalSpendingMarketingDecision(int monthId, int quarterNo, int groupId)
        {

            decimal totalSpending = 0;
            var list = (from r in _context.MarketingDecision

                        where (r.MonthID == monthId && r.QuarterNo == quarterNo
                        && r.GroupID == groupId)
                        group r by new { r.Spending, r.LaborSpending } into gpc

                        select new
                        {
                            totalSpending = gpc.Sum(x => x.Spending + x.LaborSpending)

                        }).ToList();

            if (list.Count > 0)
            {
                totalSpending = list[0].totalSpending;
            }
            return totalSpending;

        }

        //ScalarGroupDistriCostByMonth
        /*SELECT              SUM(cost) AS groupCost
FROM                  soldRoomByChannel
WHERE              (sessionID = @sessionID) AND (quarterNo = @quarter) AND (groupID = @groupID)*/

        private decimal ScalarGroupDistriCostByMonthSoldByChannel(int monthId, int quarterNo, int groupId)
        {

            decimal groupCost = 0;
            var list = (from r in _context.SoldRoomByChannel

                        where (r.MonthID == monthId && r.QuarterNo == quarterNo
                        && r.GroupID == groupId)
                        group r by new { r.Cost } into gpc

                        select new
                        {
                            groupCost = gpc.Sum(x => x.Cost)

                        }).ToList();

            if (list.Count > 0)
            {
                groupCost = list[0].groupCost;
            }
            return groupCost;

        }

        //GetDataBySingleRow

        private BalanceSheetDto GetDataBySingleRowBallanceSheet(int monthId, int quarterNo, int groupId)
        {

            var list = _context.BalanceSheet.
                Where(x => x.MonthID == monthId && x.QuarterNo == quarterNo && x.GroupID == groupId
                ).
                Select(x => new BalanceSheetDto
                {
                    QuarterNo = x.QuarterNo,
                    GroupID = x.GroupID,
                    MonthID = x.MonthID,
                    Cash = x.Cash,
                    AcctReceivable = x.AcctReceivable,
                    Inventories = x.Inventories,
                    TotCurrentAsset = x.TotCurrentLiab,
                    NetPrptyEquip = x.NetPrptyEquip,
                    TotAsset = x.TotAsset,
                    TotCurrentLiab = x.TotCurrentLiab,
                    LongDebt = x.LongDebt,
                    LongDebtPay = x.LongDebtPay,
                    ShortDebt = x.ShortDebtPay,
                    ShortDebtPay = x.ShortDebtPay,
                    TotLiab = x.TotLiab,
                    RetainedEarn = x.RetainedEarn,


                }).ToList();

            return list[0];
        }

        private int ScalarMonthDepreciationTotalAttributeDecision(int monthId, int quarter, int groupID)
        {
            int TotalDepreciation = 0;
            var list = (from ad in _context.AttributeDecision
                        join m in _context.Months on ad.MonthID equals m.MonthId
                        where (ad.QuarterNo == m.Sequence)
                        join amcoc in _context.AttributeMaxCapitalOperationConfig on m.ConfigId equals amcoc.ConfigID
                        where (ad.Attribute == amcoc.Attribute && ad.MonthID == monthId && ad.QuarterNo == quarter && ad.GroupID == groupID)
                        group new { ad, amcoc } by new { ad.AccumulatedCapital, ad.NewCapital, amcoc.DepreciationYearly } into gp
                        select new
                        {
                            TotalDepreciation = gp.Sum((x => (x.ad.AccumulatedCapital + x.ad.NewCapital) * x.amcoc.DepreciationYearly))

                        }

                      ).ToList();
            if (list.Count > 0)
            {
                TotalDepreciation = Convert.ToInt16(list[0].TotalDepreciation);
            }
            return 0;
        }

        /*SELECT              SUM(newCapital) AS totalNewCapital
FROM                  attributeDecision
WHERE              (sessionID = @sessionID) AND (quarterNo = @quarterNo) AND (groupID = @groupID)*/

        private decimal ScalarMonthlyTotalNewCapitalAttributeDecision(int monthId, int quarterNo, int groupId)
        {

            decimal totalNewCapital = 0;
            var list = (from r in _context.AttributeDecision

                        where (r.MonthID == monthId && r.QuarterNo == quarterNo
                        && r.GroupID == groupId)
                        group r by new { r.NewCapital } into gpc

                        select new
                        {
                            totalNewCapital = gpc.Sum(x => x.NewCapital)

                        }).ToList();

            if (list.Count > 0)
            {
                totalNewCapital = list[0].totalNewCapital;
            }
            return totalNewCapital;

        }

        /*SELECT     [1TotReven] AS totalRevenue
FROM         incomeState
WHERE     (sessionID = @session) AND (groupID = @group) AND (quarterNo = @quarter)
GROUP BY [1TotReven]*/

        private decimal ScalarGetTotalRevenueIncomeState(int monthId, int groupId, int quarterNo)
        {

            decimal totalRevenue = 0;
            var list = (from r in _context.IncomeState

                        where (r.MonthID == monthId && r.QuarterNo == quarterNo
                        && r.GroupID == groupId)
                        group r by new { r.TotReven } into gpc

                        select new
                        {
                            TotalRevenue = gpc.Sum(x => x.TotReven)

                        }).ToList();

            if (list.Count > 0)
            {
                totalRevenue = list[0].TotalRevenue;
            }
            return totalRevenue;

        }
      



//        private decimal ScalarGroupRoomRevenueByMonthSoldRoomByChannel(int monthId, int groupId, int quarterNo)
//        {
//            /*SELECT              SUM(revenue) AS groupRevenue
//FROM                  soldRoomByChannel
//WHERE              (sessionID = @sessionID) AND (quarterNo = @quarter) AND (groupID = @groupID)*/

//            decimal groupRevenue = 0;
//            var list = (from r in _context.SoldRoomByChannel

//                        where (r.MonthID == monthId && r.QuarterNo == quarterNo
//                        && r.GroupID == groupId)
//                        group r by new { r.Revenue } into gpc

//                        select new
//                        {
//                            groupRevenue = gpc.Sum(x => x.Revenue)

//                        }).ToList();

//            if (list.Count > 0)
//            {
//                groupRevenue = list[0].groupRevenue;
//            }
//            return groupRevenue;

//        }
    }

}
