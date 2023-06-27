using Database;
using Database.Migrations;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
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
                    Replace = pObj.Replace,
                    AjstNetIncom = pObj.AjstNetIncom,
                    IncomTAX = pObj.IncomTAX,
                    NetIncom = pObj.NetIncom,
                    FoodB = pObj.FoodB,
                    FoodB1 = pObj.FoodB1,
                    FoodB2 = pObj.FoodB2,
                    FoodB3 = pObj.FoodB3,
                    FoodB4 = pObj.FoodB4,
                    FoodB5 = pObj.FoodB5,
                    Other = pObj.Other,
                    Other1 = pObj.Other1,
                    Other2 = pObj.Other2,
                    Other3 = pObj.Other3,
                    Other4 = pObj.Other4,
                    Other5 = pObj.Other5,
                    Other6 = pObj.Other6,
                    Other7 = pObj.Other7,
                    Rent = pObj.Rent,
                    TotReven = pObj.TotReven,
                    Room = pObj.Room,

                    TotExpen = pObj.TotExpen,
                    TotDeptIncom = pObj.TotDeptIncom,
                    UndisExpens1 = pObj.UndisExpens1,
                    UndisExpens2 = pObj.UndisExpens2,
                    UndisExpens3 = pObj.UndisExpens3,
                    UndisExpens4 = pObj.UndisExpens4,
                    UndisExpens5 = pObj.UndisExpens5,
                    UndisExpens6 = pObj.UndisExpens6,
                    GrossProfit = pObj.GrossProfit,
                    MgtFee = pObj.MgtFee,
                    IncomBfCharg = pObj.IncomBfCharg,
                    Insurance = pObj.Insurance,
                    Interest = pObj.Interest,
                    PropDepreciationerty = pObj.PropDepreciationerty,
                    TotCharg = pObj.TotCharg,
                    NetIncomBfTAX = pObj.NetIncomBfTAX,
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
        private decimal ScalarQueryPriceDemandBySegment(int monthId, int quarterNo, int groupID, string segment,bool weekday)
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
    }
}
