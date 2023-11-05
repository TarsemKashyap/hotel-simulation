using Common.Dto;
using Database;
using Database.Migrations;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mysqlx.Resultset;
using MySqlX.XDevAPI.Relational;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Service
{
    public interface ICalculationServices
    {
        IEnumerable<MonthDto> monthList();
        Task<ResponseData> Create(MonthDto month);
        Task<ResponseData> List(MonthDto month);
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
        private async Task<List<int>> Months(int classId)
        {
            return await _context.Months
                            .Where(x => x.ClassId == classId)
                            .OrderBy(x => x.MonthId)
                            .Select(x => x.MonthId).ToListAsync();

        }
        public async Task<ResponseData> Calculation(MonthDto month)
        {
            ResponseData resObj = new ResponseData();
            var prevMonths = await Months(month.ClassId);
            Func<int, int, int> getLastMonth = (int monthId, int index) =>
            {
                int prevMonthIndex = prevMonths.FindIndex(p => p == monthId);
                return prevMonths[prevMonthIndex - index];
            };

            using (var transaction = _context.Database.BeginTransaction())
            {

                try
                {

                    List<MonthDto> listMonth = await GetMonthListByClassId(month.ClassId);

                    for (int i = 0; i < listMonth.Count; i++)
                    {
                        FunMonth obj = new FunMonth();
                        FunCalculation objCalculation = new FunCalculation();
                        obj.UpdateClassStatus(_context, month.ClassId, "C");
                        ClassSessionDto objclassSess = obj.GetClassDetailsById(month.ClassId, _context);
                        int currentQuarter = objclassSess.CurrentQuater;
                        int hotelsCount = objclassSess.HotelsCount;
                        int monthId = listMonth[i].MonthId;


                        #region MarketingDecision


                        List<MarketingDecisionDto> objListMd = objCalculation.GetDataByQuarterMarketDecision(_context, monthId, currentQuarter);
                        decimal ratio = 0;
                        decimal fourpercentOfRevenue = 0;
                        decimal spending = 0;
                        decimal laborSpending = 0;
                        decimal Qminus1 = 0;
                        decimal Qminus2 = 0;
                        decimal Qminus3 = 0;
                        decimal Qminus4 = 0;
                        decimal LQminus1 = 0;
                        decimal LQminus2 = 0;
                        decimal LQminus3 = 0;
                        decimal LQminus4 = 0;
                        decimal industryNorm = 0;
                        if (objListMd.Count > 0)
                        {
                            foreach (MarketingDecisionDto mDRow in objListMd)
                            {
                                int groupId = mDRow.GroupID;

                                if (currentQuarter == 1)
                                {
                                    fourpercentOfRevenue = Convert.ToDecimal(251645.01 / 3);
                                }
                                else
                                {
                                    var currentMonth = _context.Months.FirstOrDefault(x => x.MonthId == monthId);
                                    var previousMonth = _context.Months
                                         .Where(x => x.ClassId == currentMonth.ClassId && x.Sequence < currentMonth.Sequence)
                                         .OrderByDescending(x => x.Sequence)
                                         .FirstOrDefault();
                                    decimal totalRevTemp = obj.GetDataBySingleRowIncomeState(_context, previousMonth.MonthId, groupId, previousMonth.Sequence);
                                    fourpercentOfRevenue = Convert.ToDecimal(totalRevTemp * Convert.ToDecimal(0.04));
                                }
                                if (mDRow.QuarterNo == 1)
                                {
                                    Qminus1 = Convert.ToDecimal(251645.01 / 6) * Convert.ToDecimal(objCalculation.ScalarQueryIndustrialNormPercentMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo, mDRow.Segment.Trim(), mDRow.MarketingTechniques.Trim()));
                                    Qminus2 = Qminus1;
                                    Qminus3 = Qminus1;
                                    Qminus4 = Qminus1;
                                    LQminus1 = Convert.ToDecimal(251645.01 / 6) * Convert.ToDecimal(objCalculation.ScalarQueryIndustrialNormPercentMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo, mDRow.Segment.Trim(), mDRow.MarketingTechniques.Trim()));
                                    LQminus2 = LQminus1;
                                    LQminus3 = LQminus1;
                                    LQminus4 = LQminus1;
                                }
                                if (mDRow.QuarterNo == 2)
                                {
                                    Qminus1 = Convert.ToDecimal(objCalculation.ScalarQueryPastSpendingMarketingDecision(_context, getLastMonth(mDRow.MonthID, 1), mDRow.QuarterNo - 1, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                                    Qminus2 = Convert.ToDecimal(251645.01 / 6) * Convert.ToDecimal(objCalculation.ScalarQueryIndustrialNormPercentMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo, mDRow.Segment, mDRow.MarketingTechniques));
                                    Qminus3 = Qminus2;
                                    Qminus4 = Qminus2;
                                    LQminus1 = Convert.ToDecimal(objCalculation.ScalarQueryPastLaborSpendingMarketingDecision(_context, getLastMonth(mDRow.MonthID, 1), mDRow.QuarterNo - 1, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                                    LQminus2 = Convert.ToDecimal(251645.01 / 6) * Convert.ToDecimal(objCalculation.ScalarQueryIndustrialNormPercentMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo, mDRow.Segment, mDRow.MarketingTechniques));
                                    LQminus3 = LQminus2;
                                    LQminus4 = LQminus2;

                                }
                                if (mDRow.QuarterNo == 3)
                                {
                                    Qminus1 = Convert.ToDecimal(objCalculation.ScalarQueryPastLaborSpendingMarketingDecision(_context, getLastMonth(mDRow.MonthID, 1), mDRow.QuarterNo - 1, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                                    Qminus2 = Convert.ToDecimal(objCalculation.ScalarQueryPastSpendingMarketingDecision(_context, getLastMonth(mDRow.MonthID, 2), mDRow.QuarterNo - 2, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                                    Qminus3 = Convert.ToDecimal(251645.01 / 6) * Convert.ToDecimal(objCalculation.ScalarQueryIndustrialNormPercentMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo, mDRow.Segment, mDRow.MarketingTechniques));
                                    Qminus4 = Qminus3;
                                    LQminus1 = Convert.ToDecimal(objCalculation.ScalarQueryPastLaborSpendingMarketingDecision(_context, getLastMonth(mDRow.MonthID, 1), mDRow.QuarterNo - 1, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                                    LQminus2 = Convert.ToDecimal(objCalculation.ScalarQueryPastLaborSpendingMarketingDecision(_context, getLastMonth(mDRow.MonthID, 2), mDRow.QuarterNo - 2, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                                    LQminus3 = Convert.ToDecimal(251645.01 / 6) * (objCalculation.ScalarQueryIndustrialNormPercentMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo, mDRow.Segment, mDRow.MarketingTechniques));
                                    LQminus4 = LQminus3;
                                }
                                if (mDRow.QuarterNo == 4)
                                {
                                    Qminus1 = (objCalculation.ScalarQueryPastSpendingMarketingDecision(_context, getLastMonth(mDRow.MonthID, 1), mDRow.QuarterNo - 1, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                                    Qminus2 = (objCalculation.ScalarQueryPastSpendingMarketingDecision(_context, getLastMonth(mDRow.MonthID, 2), mDRow.QuarterNo - 2, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                                    Qminus3 = (objCalculation.ScalarQueryPastSpendingMarketingDecision(_context, getLastMonth(mDRow.MonthID, 3), mDRow.QuarterNo - 3, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                                    Qminus4 = Convert.ToDecimal(251645.01 / 6) * (objCalculation.ScalarQueryIndustrialNormPercentMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo, mDRow.Segment, mDRow.MarketingTechniques));
                                    LQminus1 = (objCalculation.ScalarQueryPastLaborSpendingMarketingDecision(_context, getLastMonth(mDRow.MonthID, 1), mDRow.QuarterNo - 1, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                                    LQminus2 = (objCalculation.ScalarQueryPastLaborSpendingMarketingDecision(_context, getLastMonth(mDRow.MonthID, 2), mDRow.QuarterNo - 2, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                                    LQminus3 = (objCalculation.ScalarQueryPastLaborSpendingMarketingDecision(_context, getLastMonth(mDRow.MonthID, 3), mDRow.QuarterNo - 3, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                                    LQminus4 = Convert.ToDecimal(251645.01 / 6) * (objCalculation.ScalarQueryIndustrialNormPercentMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo, mDRow.Segment, mDRow.MarketingTechniques));

                                }
                                if (mDRow.QuarterNo > 4)
                                {
                                    Qminus1 = (objCalculation.ScalarQueryPastSpendingMarketingDecision(_context, getLastMonth(mDRow.MonthID, 1), mDRow.QuarterNo - 1, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                                    Qminus2 = (objCalculation.ScalarQueryPastSpendingMarketingDecision(_context, getLastMonth(mDRow.MonthID, 2), mDRow.QuarterNo - 2, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                                    Qminus3 = (objCalculation.ScalarQueryPastSpendingMarketingDecision(_context, getLastMonth(mDRow.MonthID, 3), mDRow.QuarterNo - 3, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                                    Qminus4 = (objCalculation.ScalarQueryPastSpendingMarketingDecision(_context, getLastMonth(mDRow.MonthID, 4), mDRow.QuarterNo - 4, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                                    LQminus1 = (objCalculation.ScalarQueryPastLaborSpendingMarketingDecision(_context, getLastMonth(mDRow.MonthID, 1), mDRow.QuarterNo - 1, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                                    LQminus2 = (objCalculation.ScalarQueryPastLaborSpendingMarketingDecision(_context, getLastMonth(mDRow.MonthID, 2), mDRow.QuarterNo - 2, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                                    LQminus3 = (objCalculation.ScalarQueryPastLaborSpendingMarketingDecision(_context, getLastMonth(mDRow.MonthID, 3), mDRow.QuarterNo - 3, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                                    LQminus4 = (objCalculation.ScalarQueryPastLaborSpendingMarketingDecision(_context, getLastMonth(mDRow.MonthID, 4), mDRow.QuarterNo - 4, mDRow.GroupID, mDRow.MarketingTechniques, mDRow.Segment));
                                }
                                if (mDRow.MarketingTechniques == "Advertising")
                                {
                                    spending = Convert.ToDecimal(0.75) * Qminus1 + Convert.ToDecimal(0.25) * Qminus2 + Convert.ToDecimal(0.15) * Qminus3;
                                    laborSpending = Convert.ToDecimal(0.75) * LQminus1 + Convert.ToDecimal(0.25) * LQminus2 + Convert.ToDecimal(0.15) * LQminus3;
                                }
                                if (mDRow.MarketingTechniques == "Sales Force")
                                {
                                    spending = Convert.ToDecimal(0.5) * Qminus2 + Convert.ToDecimal(0.35) * Qminus3 + Convert.ToDecimal(0.25) * Qminus4;
                                    laborSpending = Convert.ToDecimal(0.5) * LQminus2 + Convert.ToDecimal(0.35) * LQminus3 + Convert.ToDecimal(0.25) * LQminus4;
                                }
                                if (mDRow.MarketingTechniques == "Promotions")
                                {
                                    spending = Convert.ToDecimal(0.8) * Qminus1 + Convert.ToDecimal(0.15) * Qminus2 + Convert.ToDecimal(0.05) * Qminus3;
                                    laborSpending = Convert.ToDecimal(0.8) * LQminus1 + Convert.ToDecimal(0.15) * LQminus2 + Convert.ToDecimal(0.05) * LQminus3;
                                }
                                if (mDRow.MarketingTechniques == "Public Relations")
                                {
                                    spending = Convert.ToDecimal(0.6) * Qminus1 + Convert.ToDecimal(0.35) * Qminus2 + Convert.ToDecimal(0.25) * Qminus3;
                                    laborSpending = Convert.ToDecimal(0.6) * LQminus1 + Convert.ToDecimal(0.35) * LQminus2 + Convert.ToDecimal(0.25) * LQminus3;
                                }
                                industryNorm = fourpercentOfRevenue * (objCalculation.ScalarQueryIndustrialNormPercentMarketingDecision(_context, mDRow.MonthID, mDRow.QuarterNo, mDRow.Segment, mDRow.MarketingTechniques));
                                decimal laborPercent = (objCalculation.ScalarQueryLaborPercentMarketingDecision(_context, mDRow.MonthID, mDRow.MarketingTechniques, mDRow.Segment, mDRow.QuarterNo));
                                decimal weightedSpending = laborSpending * laborPercent + spending * (1 - laborPercent);

                                if (weightedSpending > fourpercentOfRevenue * 7 / 2)
                                {
                                    weightedSpending = fourpercentOfRevenue * 7 / 2;
                                }
                                decimal fairmarket = (objCalculation.ScalarQueryFairMarketMarketingDecision(_context, mDRow.Segment, mDRow.MarketingTechniques, mDRow.MonthID, mDRow.QuarterNo));
                                decimal AverageSpendingMarktingDecision = objCalculation.ScalarQueryAverageSpendingMarktingDecision(_context, mDRow.Segment, mDRow.MarketingTechniques, mDRow.MonthID, mDRow.QuarterNo);
                                if (AverageSpendingMarktingDecision > 0)
                                    ratio = (((weightedSpending * weightedSpending) / AverageSpendingMarktingDecision) / industryNorm);
                                else
                                    ratio = 0;

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

                                if (mDRow.ActualDemand < 0)
                                {
                                    mDRow.ActualDemand = 0;
                                }
                                MarketingDecisionUpdate(mDRow);
                            }



                        }
                        #endregion
                        #region Price Decision
                        {

                            PriceDecision objPriceDecision = new PriceDecision();
                            ratio = 0;
                            decimal avergePrice;
                            decimal expectedPrice;
                            decimal fairMarketPD = 0;
                            decimal actualDemand;

                            var data = await _context.PriceDecision.Where(x => x.MonthID == monthId && x.QuarterNo == currentQuarter).ToListAsync();

                            foreach (PriceDecision priceDecisionRow in data)
                            {
                                avergePrice = Convert.ToDecimal(ScalarQueryAvgPricePriceDecision(monthId, currentQuarter, priceDecisionRow.Weekday, priceDecisionRow.DistributionChannel.Trim(), priceDecisionRow.Segment.Trim()));
                                expectedPrice = Convert.ToDecimal(ScalarQueryPriceExpectationPriceDecision(monthId, currentQuarter, priceDecisionRow.Segment, priceDecisionRow.Weekday));
                                expectedPrice = expectedPrice - 25;
                                if (avergePrice != 0)
                                {
                                    ratio = (priceDecisionRow.Price * priceDecisionRow.Price / avergePrice / expectedPrice);
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
                                    actualDemand = Convert.ToDecimal((1 / (ratio - 2) + 2)) * Convert.ToDecimal(fairMarketPD);
                                }

                                if (actualDemand < 0)
                                {
                                    actualDemand = 0;
                                }
                                priceDecisionRow.ActualDemand = Math.Round(actualDemand);
                                _context.Update(priceDecisionRow);


                            }
                            _context.SaveChanges();
                        }
                        #endregion
                        #region IncomeState
                        var dataIncomTableAfter = await _context.IncomeState.Where(x => x.MonthID == monthId && x.QuarterNo == currentQuarter).ToListAsync();
                        foreach (IncomeState row in dataIncomTableAfter)
                        {
                            if (currentQuarter <= 1)
                            {
                                row.TotReven = 3162981;
                            }
                            else
                            {
                                //Puneet
                                IncomeState objinsDto = await GetDataBySingleRowIncomeState(getLastMonth(monthId, 1), currentQuarter - 1, row.GroupID);
                                row.TotReven = objinsDto.TotReven;
                            }

                            if (row.TotReven == 0)
                            {
                                row.TotReven = 3585548;
                            }
                            _context.Update(row);

                        }
                        _context.SaveChanges();
                        #endregion

                        #region CustomerRawRating
                        var datarawRatingTable = await _context.CustomerRawRating.Where(x => x.MonthID == monthId && x.QuarterNo == currentQuarter).ToListAsync();
                        foreach (CustomerRawRating row in datarawRatingTable)
                        {
                            AttributeDecisionDto attriDeRow = await GetDataBySingleRowAttributeDecision(monthId, currentQuarter, row.GroupID, row.Attribute.Trim());
                            if (attriDeRow.OperationBudget == 0 || attriDeRow.AccumulatedCapital == 0 || attriDeRow.LaborBudget == 0)
                            {
                                row.RawRating = 0;
                            }
                            else
                            {
                                decimal ideal = ScalarAttriSegIdealRating(row.MonthID, row.QuarterNo, row.Attribute, row.Segment);
                                row.RawRating = Math.Round(ScalarQueryCustomerRawRating(row.MonthID, row.QuarterNo, row.GroupID, row.Attribute, row.Segment), 4);
                                if (row.RawRating > ideal)
                                {
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

                            _context.Update(row);

                        }
                        _context.SaveChanges();
                        #endregion

                        #region WeightedAttributeRating customerRating
                        var dataoverallRatingTable = await _context.WeightedAttributeRating.Where(x => x.MonthID == monthId && x.QuarterNo == currentQuarter).ToListAsync();

                        decimal averageRating = 0;
                        decimal fairMarket = 0;
                        foreach (WeightedAttributeRating row in dataoverallRatingTable)
                        {
                            row.CustomerRating = ScalarQueryRatingBySegmentCustomerRawRatting(row.MonthID, row.QuarterNo, row.GroupID, row.Segment);
                            _context.Add(row);
                            _context.Update(row);

                        }
                        _context.SaveChanges();
                        #endregion
                        #region weightedAttributeRatting Actualdemand
                        var dataoverallRatingTable1 = await _context.WeightedAttributeRating.Where(x => x.MonthID == monthId && x.QuarterNo == currentQuarter).ToListAsync();

                        foreach (WeightedAttributeRating row in dataoverallRatingTable1)
                        {

                            averageRating = Convert.ToDecimal(ScalarQueryGetAvageRatingWeightAttributeRating(row.MonthID, row.QuarterNo, row.Segment));
                            fairMarket = Convert.ToDecimal(ScalarQueryFairMarketWeightAttributeRating(row.Segment, row.MonthID, row.QuarterNo));
                            if (row.CustomerRating == 0 || averageRating == 0 || fairMarket == 0)
                            { row.ActualDemand = 0; }
                            else
                            {
                                row.ActualDemand = Convert.ToInt32(row.CustomerRating / averageRating * fairMarket);
                            }
                            _context.Add(row);
                            _context.Update(row);

                        }
                        _context.SaveChanges();
                        #endregion

                        #region room Allowcation 
                        var datatable = await _context.RoomAllocation.Where(x => x.MonthID == monthId && x.QuarterNo == currentQuarter).ToListAsync();
                        foreach (RoomAllocation row in datatable)
                        {
                            if (row.Weekday == true)
                            {
                                row.ActualDemand = Convert.ToInt32(4 * (Convert.ToInt32(ScalarQueryMarketingDemandBySegment(monthId, currentQuarter, row.GroupID, row.Segment)) + Convert.ToInt32(ScalarQueryAttributeDemandBySegment(monthId, currentQuarter, row.GroupID, row.Segment))) / 7) + Convert.ToInt32(ScalarQueryPriceDemandBySegment(monthId, currentQuarter, row.GroupID, row.Segment, row.Weekday));
                            }
                            if (row.Weekday == false)
                            {
                                row.ActualDemand = Convert.ToInt32(3 * (Convert.ToInt32(ScalarQueryMarketingDemandBySegment(monthId, currentQuarter, row.GroupID, row.Segment)) + Convert.ToInt32(ScalarQueryAttributeDemandBySegment(monthId, currentQuarter, row.GroupID, row.Segment))) / 7) + Convert.ToInt32(ScalarQueryPriceDemandBySegment(monthId, currentQuarter, row.GroupID, row.Segment, row.Weekday));
                            }
                            if (row.RoomsAllocated < row.ActualDemand)
                            {
                                string Value = "Wrong Value";
                            }


                            _context.Update(row);

                        }
                        _context.SaveChanges();
                        #endregion
                        #region RoomAllocation


                        int maxGroup = Convert.ToInt32(ScalarQueryMaxGroupNoRommAllocation(monthId, currentQuarter));
                        int groupID = 1;
                        int roomPool = 0;
                        while (groupID < maxGroup + 1)
                        {
                            /////////Do the sold room and Room Pools each group by each group for weekday
                            //table = await GetDataByGroupWeekdayRoomAllocation(monthId, currentQuarter, groupID, true);
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
                            ///
                            #region RoomAllocation WeekDay true
                            var datatableWeektrue = await _context.RoomAllocation.Where(x => x.MonthID == monthId && x.QuarterNo == currentQuarter && x.GroupID == groupID && x.Weekday == true).ToListAsync();

                            foreach (RoomAllocation row in datatableWeektrue)
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
                            }
                            if (roomPool > 0)
                            {
                                foreach (RoomAllocation row in datatableWeektrue)
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
                                    //    RoomAllocationUpdate(row);
                                    //}
                                    _context.Update(row);
                                }
                                _context.SaveChanges();
                            }



                            #endregion
                            #region RoomAllocation weekDay False
                            roomPool = 0;
                            var datatableWeekfalse = await _context.RoomAllocation.Where(x => x.MonthID == monthId && x.QuarterNo == currentQuarter && x.GroupID == groupID && x.Weekday == false).ToListAsync();

                            foreach (RoomAllocation row in datatableWeekfalse)
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
                            }
                            //    RoomAllocationUpdate(row);
                            //}
                            ////////////////Re-allocate the rooms that are free in such an order
                            ////////////////Business, Corporate contract, Small Business,Afluent Mature Travelers,International leisure travelers,Families,Corporate/Business Meetings,Association Meetings  
                            if (roomPool > 0)
                            {
                                foreach (RoomAllocation row in datatableWeekfalse)
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
                                    //  RoomAllocationUpdate(row);
                                    //}
                                    //////}
                                    //if (roomPool > 0)
                                    //{
                                    //foreach (RoomAllocationDto row in table)
                                    //{
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

                                    _context.Update(row);
                                }

                                _context.SaveChanges();

                            }

                            #endregion

                            groupID++;
                        }
                        #endregion
                        {
                            #region SoldRoomByChannel

                            var datasoldChanTable = await _context.SoldRoomByChannel.Where(x => x.MonthID == monthId && x.QuarterNo == currentQuarter).ToListAsync();

                            decimal directSold;
                            decimal travelSold;
                            decimal onlineSold;
                            decimal opaqueSold;
                            decimal thisSold;
                            decimal roomAlloSold;

                            decimal sum;

                            foreach (SoldRoomByChannel row in datasoldChanTable)
                            {
                                directSold = GetDataBySingleRowPriceDecision(row.MonthID, row.QuarterNo, row.GroupID, row.Weekday, "Direct", row.Segment);
                                travelSold = GetDataBySingleRowPriceDecision(row.MonthID, row.QuarterNo, row.GroupID, row.Weekday, "Travel Agent", row.Segment);
                                onlineSold = GetDataBySingleRowPriceDecision(row.MonthID, row.QuarterNo, row.GroupID, row.Weekday, "Online Travel Agent", row.Segment);
                                opaqueSold = GetDataBySingleRowPriceDecision(row.MonthID, row.QuarterNo, row.GroupID, row.Weekday, "Opaque", row.Segment);
                                thisSold = GetDataBySingleRowPriceDecision(row.MonthID, row.QuarterNo, row.GroupID, row.Weekday, row.Channel, row.Segment);
                                roomAlloSold = GetDataByEachDecisionRoomAllocation(row.MonthID, row.QuarterNo, row.GroupID, row.Weekday, row.Segment);

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
                                _context.Update(row);

                            }
                            _context.SaveChanges();

                            #endregion
                            #region SoldRoombyChannel 
                            var datasoldChanTable1 = await _context.SoldRoomByChannel.Where(x => x.MonthID == monthId && x.QuarterNo == currentQuarter).ToListAsync();

                            foreach (SoldRoomByChannel row in datasoldChanTable1)
                            {
                                directSold = GetDataBySingleRowPriceDecision(row.MonthID, row.QuarterNo, row.GroupID, row.Weekday, "Direct", row.Segment);
                                travelSold = GetDataBySingleRowPriceDecision(row.MonthID, row.QuarterNo, row.GroupID, row.Weekday, "Travel Agent", row.Segment);
                                onlineSold = GetDataBySingleRowPriceDecision(row.MonthID, row.QuarterNo, row.GroupID, row.Weekday, "Online Travel Agent", row.Segment);
                                opaqueSold = GetDataBySingleRowPriceDecision(row.MonthID, row.QuarterNo, row.GroupID, row.Weekday, "Opaque", row.Segment);
                                thisSold = GetDataBySingleRowPriceDecision(row.MonthID, row.QuarterNo, row.GroupID, row.Weekday, row.Channel, row.Segment);
                                roomAlloSold = GetDataByEachDecisionRoomAllocation(row.MonthID, row.QuarterNo, row.GroupID, row.Weekday, row.Segment);

                                sum = directSold + travelSold + onlineSold + opaqueSold;

                                if (sum == 0)
                                {
                                    row.SoldRoom = 0;
                                    row.Revenue = 0;
                                    row.Cost = 0;
                                }
                                else
                                {
                                    row.Revenue = ScalarSingleRevenueSoldRoomByChannel(row.MonthID, row.QuarterNo, row.GroupID, row.Segment, row.Channel, row.Weekday);
                                    row.Cost = ScalarSingleCostSoldRoomByChannel(row.MonthID, row.QuarterNo, row.GroupID, row.Segment, row.Channel, row.Weekday);

                                }


                                _context.Update(row);

                            }
                            _context.SaveChanges();
                            #endregion

                        }

                        {

                            BalanceSheetDto balanTableRow;
                            #region   IncomeState
                            var dataIncoTable = await _context.IncomeState.Where(x => x.MonthID == monthId && x.QuarterNo == currentQuarter).ToListAsync();
                            int groupNo = Convert.ToInt32(ScalarQueryFindNoOfHotels(month.ClassId));
                            if (dataIncoTable.Count > 0)
                            {
                                for (int c = 1; c < groupNo + 1; c++)
                                {
                                    IncomeState incomStaRow = await GetDataBySingleRowIncomeState(monthId, currentQuarter, c);

                                    decimal roomRevenue = ScalarGroupRoomRevenueByMonthSoldRoomByChannel(monthId, currentQuarter, incomStaRow.GroupID);
                                    incomStaRow.Room1 = roomRevenue;
                                    incomStaRow.FoodB = Convert.ToDecimal(31 * roomRevenue / 52);
                                    decimal restaurantScore = Convert.ToDecimal(ScalarAttributeRevenueScoreRoomAllocation(monthId, currentQuarter, incomStaRow.GroupID, "Resturants"));
                                    decimal BarScore = Convert.ToDecimal(ScalarAttributeRevenueScoreRoomAllocation(monthId, currentQuarter, incomStaRow.GroupID, "Bars"));
                                    decimal roomServiceScore = Convert.ToDecimal(ScalarAttributeRevenueScoreRoomAllocation(monthId, currentQuarter, incomStaRow.GroupID, "Room Service"));
                                    decimal banquetScore = Convert.ToDecimal(ScalarAttributeRevenueScoreRoomAllocation(monthId, currentQuarter, incomStaRow.GroupID, "Banquet & Catering"));
                                    decimal meetRoomScore = Convert.ToDecimal(ScalarAttributeRevenueScoreRoomAllocation(monthId, currentQuarter, incomStaRow.GroupID, "Meeting Rooms"));
                                    decimal foodBTotalScore = restaurantScore + BarScore + roomServiceScore + banquetScore + meetRoomScore;

                                    if (foodBTotalScore == 0)
                                    {
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
                                        incomStaRow.Other1 = incomStaRow.Other * golfScore / otherOperatedTotal;
                                        incomStaRow.Other2 = incomStaRow.Other * spaScore / otherOperatedTotal;
                                        incomStaRow.Other3 = incomStaRow.Other * fitnessScore / otherOperatedTotal;
                                        incomStaRow.Other4 = incomStaRow.Other * businessScore / otherOperatedTotal;
                                        incomStaRow.Other5 = incomStaRow.Other * otherRecreScore / otherOperatedTotal;
                                        incomStaRow.Other6 = incomStaRow.Other * entertainScore / otherOperatedTotal;
                                    }

                                    incomStaRow.Rent = 5 * roomRevenue / 52;
                                    incomStaRow.TotReven = 100 * roomRevenue / 52;
                                    AttributeDecisionDto attriDecisionRow = await GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Guest Rooms");
                                    incomStaRow.Room = attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                                    attriDecisionRow = await GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Reservations");
                                    incomStaRow.Room = incomStaRow.Room + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                                    attriDecisionRow = await GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Guest Check in/Guest Check out");
                                    incomStaRow.Room = incomStaRow.Room + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                                    attriDecisionRow = await GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Concierge");
                                    incomStaRow.Room = incomStaRow.Room + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                                    attriDecisionRow = await GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Housekeeping");
                                    incomStaRow.Room = incomStaRow.Room + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                                    attriDecisionRow = await GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Resturants");
                                    incomStaRow.Food2B = attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                                    attriDecisionRow = await GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Bars");
                                    incomStaRow.Food2B = incomStaRow.Food2B + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                                    attriDecisionRow = await GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Room Service");
                                    incomStaRow.Food2B = incomStaRow.Food2B + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                                    attriDecisionRow = await GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Banquet & Catering");
                                    incomStaRow.Food2B = incomStaRow.Food2B + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                                    attriDecisionRow = await GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Meeting Rooms");
                                    incomStaRow.Food2B = incomStaRow.Food2B + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                                    attriDecisionRow = await GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Spa");
                                    incomStaRow.Other7 = attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                                    attriDecisionRow = await GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Fitness Center");
                                    incomStaRow.Other7 = incomStaRow.Other7 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                                    attriDecisionRow = await GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Business Center");
                                    incomStaRow.Other7 = incomStaRow.Other7 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                                    attriDecisionRow = await GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Golf Course");
                                    incomStaRow.Other7 = incomStaRow.Other7 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                                    attriDecisionRow = await GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Other Recreation Facilities - Pools, game rooms, tennis courts, ect");
                                    incomStaRow.Other7 = incomStaRow.Other7 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                                    attriDecisionRow = await GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Entertainment");
                                    incomStaRow.Other7 = incomStaRow.Other7 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                                    incomStaRow.TotExpen = incomStaRow.Room + incomStaRow.Food2B + incomStaRow.Other7;
                                    incomStaRow.TotDeptIncom = 100 * roomRevenue / 52 - incomStaRow.TotExpen;
                                    attriDecisionRow = await GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Courtesy(FB)");
                                    incomStaRow.UndisExpens1 = 2 * roomRevenue / 13 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                                    attriDecisionRow = await GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Management/Sales Attention");
                                    incomStaRow.UndisExpens1 = incomStaRow.UndisExpens1 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                                    incomStaRow.UndisExpens2 = ScalarQueryTotalSpendingMarketingDecision(monthId, currentQuarter, incomStaRow.GroupID);
                                    incomStaRow.UndisExpens3 = Convert.ToDecimal(ScalarGroupDistriCostByMonthSoldByChannel(monthId, currentQuarter, incomStaRow.GroupID));
                                    incomStaRow.UndisExpens4 = 11 * roomRevenue / 104;
                                    attriDecisionRow = await GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Maintanence and security");
                                    incomStaRow.UndisExpens4 = incomStaRow.UndisExpens4 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                                    attriDecisionRow = await GetDataBySingleRowAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID, "Courtesy (Rooms)");
                                    incomStaRow.UndisExpens4 = incomStaRow.UndisExpens4 + attriDecisionRow.LaborBudget + attriDecisionRow.OperationBudget;
                                    incomStaRow.UndisExpens5 = 11 * roomRevenue / 130;
                                    incomStaRow.UndisExpens6 = incomStaRow.UndisExpens1 + incomStaRow.UndisExpens2 + incomStaRow.UndisExpens3 + incomStaRow.UndisExpens4 + incomStaRow.UndisExpens5;
                                    incomStaRow.GrossProfit = incomStaRow.TotDeptIncom - incomStaRow.UndisExpens6;
                                    incomStaRow.MgtFee = 5 * roomRevenue / 104;
                                    incomStaRow.IncomBfCharg = incomStaRow.GrossProfit - incomStaRow.MgtFee;
                                    incomStaRow.Property = 4 * roomRevenue / 65;
                                    incomStaRow.Insurance = 3 * roomRevenue / 104;
                                    balanTableRow = GetDataBySingleRowBallanceSheet(incomStaRow.MonthID, incomStaRow.QuarterNo, incomStaRow.GroupID);
                                    incomStaRow.Interest = balanTableRow.LongDebt * 7 / 1000 + balanTableRow.ShortDebt * 3 / 100;
                                    incomStaRow.PropDepreciationerty = ScalarMonthDepreciationTotalAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID);
                                    incomStaRow.TotCharg = incomStaRow.Property + incomStaRow.Insurance + incomStaRow.Interest + incomStaRow.PropDepreciationerty;
                                    incomStaRow.NetIncomBfTAX = incomStaRow.IncomBfCharg - incomStaRow.TotCharg;
                                    incomStaRow.Replace = Convert.ToDecimal(ScalarMonthlyTotalNewCapitalAttributeDecision(monthId, currentQuarter, incomStaRow.GroupID));
                                    incomStaRow.AjstNetIncom = incomStaRow.NetIncomBfTAX - incomStaRow.Replace;
                                    if (incomStaRow.NetIncomBfTAX < 0)
                                    {
                                        incomStaRow.IncomTAX = 0;
                                    }
                                    else
                                    {
                                        incomStaRow.IncomTAX = incomStaRow.NetIncomBfTAX / 4;
                                    }
                                    incomStaRow.NetIncom = incomStaRow.NetIncomBfTAX - incomStaRow.IncomTAX;
                                    incomStaRow.TotReven = 100 * roomRevenue / 52;
                                    _context.Update(incomStaRow);
                                }
                                _context.SaveChanges();
                            }

                            if (currentQuarter > 1)
                            {
                                decimal totalRevThisMonth;
                                decimal totalRevPrevMonth;
                                var dataincome1Table = await _context.IncomeState.Where(x => x.MonthID == monthId && x.QuarterNo == currentQuarter).ToListAsync();
                                foreach (IncomeState incom1StaRow in dataincome1Table)
                                {
                                    totalRevThisMonth = Convert.ToDecimal(ScalarGetTotalRevenueIncomeState(monthId, incom1StaRow.GroupID, currentQuarter));
                                    totalRevPrevMonth = Convert.ToDecimal(ScalarGetTotalRevenueIncomeState(getLastMonth(monthId, 1), incom1StaRow.GroupID, currentQuarter - 1));
                                    if (totalRevPrevMonth == totalRevThisMonth)
                                    {
                                        decimal roomRevenue = Convert.ToDecimal(ScalarGroupRoomRevenueByMonthSoldRoomByChannel(monthId, currentQuarter, incom1StaRow.GroupID));
                                        incom1StaRow.TotReven = 100 * roomRevenue / 52;
                                        _context.Update(incom1StaRow);

                                    }
                                }
                                _context.SaveChanges();
                            }
                            #endregion

                        }
                        #region BalanceSheet Update 

                        {
                            var dataBalanceSheet = await _context.BalanceSheet.Where(x => x.MonthID == monthId && x.QuarterNo == currentQuarter).ToListAsync();
                            foreach (BalanceSheet row in dataBalanceSheet)
                            {

                                IncomeState incoRow = await GetDataBySingleRowIncomeState(row.MonthID, row.QuarterNo, row.GroupID);
                                row.LongDebt = row.LongDebt - row.LongDebtPay;
                                row.ShortDebt = row.ShortDebt - row.ShortDebtPay;
                                row.AcctReceivable = incoRow.FoodB1 / 20 + incoRow.FoodB2 / 20 + incoRow.FoodB3 / 20 + 3 * incoRow.FoodB4 / 20 + 3 * incoRow.FoodB5 / 20 + 3 * incoRow.Other1 / 20 + 7 * incoRow.Other2 / 100 + 7 * incoRow.Other3 / 100 + 7 * (incoRow.Other4 + incoRow.Other5 + incoRow.Other6) / 100 + incoRow.Rent / 10;
                                row.Inventories = await ScalarTotalOtherExpenAttributeDecision(row.MonthID, row.QuarterNo, row.GroupID);
                                decimal payable = await ScalarTotalOtherExpenAttributeDecision(monthId, row.QuarterNo, row.GroupID) / 5;
                                decimal advanceDeposite = Convert.ToDecimal(ScalarGroupRoomRevenueByMonthSoldRoomByChannel(monthId, row.QuarterNo, row.GroupID)) / 20;
                                row.TotCurrentLiab = payable + advanceDeposite + incoRow.IncomTAX;
                                row.TotLiab = row.TotCurrentLiab + row.LongDebt + row.ShortDebt;
                                row.NetPrptyEquip = await ScalarTotalAccumuCapitalInAMonthAttributeDecision(row.MonthID, row.QuarterNo, row.GroupID);
                                {
                                    BalanceSheetDto balanceRowPrevious = GetDataBySingleRowBallanceSheet(row.MonthID, row.QuarterNo - 1, row.GroupID);
                                    decimal previousCash = balanceRowPrevious.Cash;
                                    decimal netIncome = incoRow.NetIncom;
                                    decimal changeInNetReceivableInventory = (row.AcctReceivable - balanceRowPrevious.AcctReceivable) * 97 / 100 + row.Inventories - balanceRowPrevious.Inventories;
                                    decimal changeInTotalLiabi = row.TotLiab - balanceRowPrevious.TotLiab;
                                    decimal changeInPropertyEquip = Convert.ToDecimal(ScalarMonthlyTotalNewCapitalAttributeDecision(row.MonthID, row.QuarterNo, row.GroupID));
                                    row.Cash = previousCash + netIncome - changeInNetReceivableInventory + changeInTotalLiabi + incoRow.PropDepreciationerty - changeInPropertyEquip;
                                }
                                if (row.Cash < 0)
                                {
                                    row.ShortDebt = row.ShortDebt - row.Cash;
                                    row.Cash = 0;
                                    row.TotLiab = row.TotCurrentLiab + row.LongDebt + row.ShortDebt;
                                }
                                row.TotCurrentAsset = 97 * row.AcctReceivable / 100 + row.Inventories + row.Cash;
                                row.TotAsset = row.TotCurrentAsset + row.NetPrptyEquip + 5000000;
                                row.RetainedEarn = row.TotAsset - 10000000 - row.TotLiab;
                                _context.Update(row);
                            }
                            _context.SaveChanges();

                        }


                        #endregion
                        #region RoomAllocation
                        var dataroTab = await _context.RoomAllocation.Where(x => x.MonthID == monthId && x.QuarterNo == currentQuarter).ToListAsync();
                        foreach (RoomAllocation roRw in dataroTab)
                        {
                            roRw.Revenue = Convert.ToDecimal(ScalarQueryRevenueByWeekSegmentRoomAllocation(roRw.MonthID, roRw.GroupID, roRw.QuarterNo, roRw.Segment, roRw.Weekday));
                            _context.Update(roRw);

                        }
                        _context.SaveChanges();
                        #endregion
                        #region class status update 
                        obj.UpdateClassStatus(_context, month.ClassId, "T");
                        #endregion

                        #region Ranking
                        if (currentQuarter > 1)
                        {
                            int maxGroupRA = Convert.ToInt32(ScalarQueryMaxGroupNoRommAllocation(monthId, currentQuarter));
                            int groupIDRA = 1;
                            string schoolName = Convert.ToString(ScalarSchoolName(monthId));
                            string groupName = null;

                            decimal a;
                            decimal b;
                            decimal profiM;
                            while (groupIDRA < maxGroupRA + 1)
                            {
                                groupName = Convert.ToString(ScalarGroupName(monthId, groupIDRA));
                                //////Profit Margin
                                ///
                                IncomeState incomeRowCurrent = await GetDataBySingleRowIncomeState(monthId, currentQuarter, groupIDRA);
                                a = incomeRowCurrent.NetIncom;
                                b = incomeRowCurrent.TotReven;

                                if (b == 0)
                                {
                                    profiM = 0;
                                }
                                else
                                {
                                    profiM = a / b;
                                }
                                if (groupName == "Null")
                                {
                                    groupName = "Group " + groupIDRA.ToString();
                                }
                                Rankings ranksR = GetDataBySingleRowRanking(monthId, "Profit Margin", groupIDRA);
                                if (ranksR.ID == 0)
                                {
                                    InsertRank(monthId, currentQuarter, groupIDRA, groupName, schoolName, "Profit Margin", profiM, DateTime.Now);
                                }
                                else
                                {
                                    ranksR.Performance = profiM;
                                    _context.ChangeTracker.Clear();
                                    _context.Update(ranksR);
                                    _context.SaveChanges();

                                }
                                groupIDRA++;
                            }
                        }
                        #endregion

                    }
                    await transaction.CommitAsync();
                    resObj.Message = "Calculation Success";
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    String exp = ex.ToString();
                    resObj.Message = ex.ToString();

                }
            }

            resObj.StatusCode = 200;
            string strjson = "{ ClassId:" + month.ClassId + "}";
            var jobj = JsonConvert.DeserializeObject<MonthDto>(strjson)!;
            resObj.Data = jobj;
            return resObj;
        }

        public Task<ResponseData> Create(MonthDto month)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData> List(MonthDto month)
        {
            FunMonth obj = new FunMonth();
            ClassSessionDto objclassSess = obj.GetClassDetailsById(month.ClassId, _context);
            int currentQuarter = objclassSess.CurrentQuater;
            month.Sequence = currentQuarter;
            ResponseData resObj = new ResponseData();
            var list = (from c in _context.ClassSessions
                        join m in _context.Months on c.ClassId equals m.ClassId
                        where (c.ClassId == month.ClassId)
                        select new
                        {
                            MonthId = m.MonthId,
                            Status = c.Status,// MonthStatus(m.Sequence, month.Sequence, c.Status)
                            IsMonthCompleted = m.IsComplete
                        }
                      ).ToList();

            List<CalculationResponse> listnew = new List<CalculationResponse>();
            foreach (var item in list)
            {
                CalculationResponse cr = new CalculationResponse();
                //item.Status = MonthStatus(item.MonthId, currentQuarter, item.Status);
                cr.MonthId = item.MonthId;
                cr.Status = MonthStatus(item.MonthId, currentQuarter, item.Status, item.IsMonthCompleted);
                listnew.Add(cr);


            }

            resObj.Message = "Calculation Success";
            resObj.StatusCode = 200;
            // string strjson = "{ monthID:" + month.MonthId + "}";
            var jobj = listnew;
            resObj.Data = jobj;
            return resObj;

        }
        protected string MonthStatus(int quartarno, int currentQuartarNo, ClassStatus status, bool isMonthCompleted)
        {
            string returnStatus = "";
            //if (currentQuartarNo < quartarno)
            //{
            //    returnStatus = "Calculated";
            //}
            //else
            if (isMonthCompleted == true)
            {
                returnStatus = "Calculated";
            }
            else
            {

                if (status == ClassStatus.T)
                {
                    returnStatus = "Calculated";
                }
                else if (status == ClassStatus.S)
                {
                    returnStatus = "This Month Has Been Just Created.";
                }
                else if (status == ClassStatus.A)
                {
                    returnStatus = "This Month Has Been Finalized. Please Go Ahead and Create A New Month.";
                }
                else if (status == ClassStatus.C)
                {
                    returnStatus = "Calculation is not finished yet, please wait...";
                }
                else
                {
                    returnStatus = "Ready For Calculation";
                }
            }
            return returnStatus;
        }
        public IEnumerable<MonthDto> monthList()
        {
            throw new NotImplementedException();
        }


        public async Task<List<PriceDecisionDto>> GetDataByQuarterPriceDecision(int monthId, int quartorNo)
        {
            var data = await _context.PriceDecision.AsNoTracking().Where(x => x.MonthID == monthId && x.QuarterNo == quartorNo).ToListAsync();
            if (data == null)
            {
                throw new ValidationException("data not found ");
            }
            return data.Adapt<List<PriceDecisionDto>>();

        }
        private decimal ScalarQueryAvgPricePriceDecision(int monthId, int quarterNo, bool weekday, string distributionChannel, string segment)
        {


            var list = _context.PriceDecision.Where(x => x.MonthID == monthId
                        && x.QuarterNo == quarterNo
                        && x.Weekday == weekday
                        && x.DistributionChannel == distributionChannel.Trim()
                        && x.Segment == segment.Trim())
                        .Select(a => a.Price).ToList();
            return list.Average(a => a);

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

        private async Task<List<IncomeStateDto>> GetDataByMonthIncomeState(int monthId, int quarterNo)
        {
            var data = await _context.IncomeState.Where(x => x.MonthID == monthId && x.QuarterNo == quarterNo).ToListAsync();
            if (data == null)
            {
                throw new ValidationException("data not found ");
            }
            return data.Adapt<List<IncomeStateDto>>();


        }
        private async Task<IncomeState> GetDataBySingleRowIncomeState(int monthId, int quarterNo, int groupId)
        {
            var data = await _context.IncomeState.SingleOrDefaultAsync(x => x.MonthID == monthId && x.QuarterNo == quarterNo && x.GroupID == groupId);
            if (data == null)
            {
                throw new ValidationException("data not found ");
            }
            return data;

        }



        private async Task<List<CustomerRawRatingDto>> GetDataByQuarterCustomerRowRatting(int monthId, int quarterNo)
        {
            var data = await _context.CustomerRawRating.Where(x => x.MonthID == monthId && x.QuarterNo == quarterNo).ToListAsync();
            if (data == null)
            {
                throw new ValidationException("data not found ");
            }
            return data.Adapt<List<CustomerRawRatingDto>>();

        }
        private async Task<AttributeDecisionDto> GetDataBySingleRowAttributeDecision(int monthId, int quarterNo, int groupId, string attribute)
        {
            var data = await _context.AttributeDecision.SingleOrDefaultAsync(x => x.MonthID == monthId && x.QuarterNo == quarterNo && x.GroupID == groupId && x.Attribute.Trim() == attribute.Trim());
            if (data == null)
            {
                throw new ValidationException("data not found ");
            }
            return data.Adapt<AttributeDecisionDto>();

        }

        private decimal ScalarAttriSegIdealRating(int monthID, int quarterNo, string attribute, string segment)
        {

            decimal ideal = 0;
            var list = (from irawc in _context.IdealRatingAttributeWeightConfig
                        join m in _context.Months on irawc.ConfigID equals m.ConfigId
                        where (irawc.Attribute == attribute.Trim() && irawc.Segment == segment.Trim() && m.Sequence == quarterNo && m.MonthId == monthID)
                        select new
                        {
                            Ideal = irawc.IdealRating,

                        }).ToList();

            if (list.Count > 0)
            {
                ideal = list.Average(x => x.Ideal);
            }
            return ideal;
        }

        private decimal ScalarQueryCustomerRawRating(int monthID, int quarterNo, int groupId, string attribute, string segment)
        {
            try
            {
                decimal ideal = 0;
                var list = (from crr in _context.CustomerRawRating
                            join irawc in _context.IdealRatingAttributeWeightConfig on crr.Segment equals irawc.Segment
                            where (crr.Attribute == irawc.Attribute)
                            join amcoc in _context.AttributeMaxCapitalOperationConfig on crr.Attribute equals amcoc.Attribute
                            where (irawc.ConfigID == amcoc.ConfigID)
                            join m in _context.Months on crr.MonthID equals m.MonthId
                            join ad in _context.AttributeDecision on crr.MonthID equals ad.MonthID
                            where (crr.QuarterNo == ad.QuarterNo && crr.GroupID == ad.GroupID && crr.Attribute == ad.Attribute)
                            join ins in _context.IncomeState on crr.MonthID equals ins.MonthID
                            where (m.MonthId == ins.MonthID && m.Sequence == ins.QuarterNo && crr.GroupID == ins.GroupID && crr.MonthID == monthID
                            && crr.QuarterNo == quarterNo && crr.GroupID == groupId && crr.Attribute == attribute && crr.Segment == segment)
                            select new
                            {
                                RawRating = (decimal?)(((ad.AccumulatedCapital + ad.NewCapital) / amcoc.MaxNewCapital)
                                * irawc.IdealRating * amcoc.NewCapitalPortion + ad.OperationBudget
                                / amcoc.MaxOperation
                          * irawc.IdealRating * amcoc.OperationPortion + ad.LaborBudget
                          / ins.TotReven
                          / amcoc.PreLaborPercent * irawc.IdealRating * amcoc.LaborPortion) ?? 0,

                            }).ToList();

                if (list.Count > 0)
                {
                    ideal = list[0].RawRating;
                }
                return ideal;
            }
            catch (Exception ex)
            {
                string exp = ex.ToString();
                return 0;


            }

        }


        private async Task<List<WeightedAttributeRatingDto>> GetDataByQuarterWeightAttributeRating(int monthId, int quarterNo)
        {
            var data = await _context.WeightedAttributeRating.Where(x => x.MonthID == monthId && x.QuarterNo == quarterNo).ToListAsync();
            if (data == null)
            {
                throw new ValidationException("data not found ");
            }
            return data.Adapt<List<WeightedAttributeRatingDto>>();


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
                            RawRating = crr.RawRating,
                            Weight = irawc.Weight,

                        }).ToList();

            //}).ToList();

            if (list.Count > 0)
            {
                WeightedRating = list.Sum(a => (a.RawRating * a.Weight));
            }
            return WeightedRating;

        }
        private decimal ScalarQueryGetAvageRatingWeightAttributeRating(int monthID, int quarterNo, string segment)
        {


            var list = (from w in _context.WeightedAttributeRating
                        .Where(x => x.MonthID == monthID && x.QuarterNo == quarterNo && x.Segment == segment)
                        select new
                        {
                            CustomerRating = w.CustomerRating
                        }).ToList();



            decimal AverageRating = 0;
            if (list.Count > 0)
            {
                AverageRating = list.Average(x => x.CustomerRating);
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


        private List<RoomAllocationDto> GetDataByQuarterRoomAllocation(int monthId, int quarterNo)
        {

            var list = _context.RoomAllocation.Where(x => x.MonthID == monthId && x.QuarterNo == quarterNo).
                Select(x => new RoomAllocationDto
                {
                    ID = x.ID,
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
            _context.ChangeTracker.Clear();
            return list;
        }
        private decimal ScalarQueryMarketingDemandBySegment(int monthId, int quarterNo, int groupID, string segment)
        {

            decimal MarketingDemand = 0;
            var list = (from md in _context.MarketingDecision
                        where (md.MonthID == monthId && md.Segment == segment && md.QuarterNo == quarterNo && md.GroupID == groupID)
                        select new
                        {
                            MarketingDemand = md.ActualDemand

                        }).ToList();


            if (list.Count > 0)
            {
                MarketingDemand = list.Sum(x => x.MarketingDemand);
            }
            return MarketingDemand;

        }

        private decimal ScalarQueryAttributeDemandBySegment(int monthId, int quarterNo, int groupID, string segment)
        {
            decimal AttributeDemand = 0;
            var list = (from md in _context.WeightedAttributeRating
                        where (md.MonthID == monthId && md.Segment == segment && md.QuarterNo == quarterNo && md.GroupID == groupID)
                        select new
                        {
                            AttributeDemand = md.ActualDemand,

                        }).ToList();


            if (list.Count > 0)
            {
                AttributeDemand = list.Sum(x => x.AttributeDemand);
            }
            return AttributeDemand;

        }
        private decimal ScalarQueryPriceDemandBySegment(int monthId, int quarterNo, int groupID, string segment, bool weekday)
        {
            decimal PriceDemand = 0;
            var list = (from md in _context.PriceDecision
                        where (md.MonthID == monthId && md.Segment == segment && md.QuarterNo == quarterNo && Convert.ToInt16(md.GroupID) == groupID
                        && md.Weekday == weekday)

                        select new
                        {
                            PriceDemand = md.ActualDemand,

                        }).ToList();

            if (list.Count > 0)
            {
                PriceDemand = list.Sum(x => x.PriceDemand);
            }
            return PriceDemand;

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
                _context.MarketingDecision.Attach(objMd);
                _context.Entry(objMd).Property(x => x.ActualDemand).IsModified = true;
                _context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;

        }

        private bool IncomeStateUpdate(IncomeStateDto pObj)
        {
            bool result = false;
            try
            {
                IncomeState objPd = new IncomeState
                {
                    ID = pObj.ID,
                    GroupID = pObj.GroupID,
                    MonthID = pObj.MonthID,
                    QuarterNo = pObj.QuarterNo,
                    Replace = (int)pObj.Replace,
                    AjstNetIncom = (int)pObj.AjstNetIncom,
                    IncomTAX = (int)pObj.IncomTAX,
                    NetIncom = (int)pObj.NetIncom,
                    Room1 = (int)pObj.Room1,
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
                    PropDepreciationerty = (int)pObj.PropDepreciationerty,
                    TotCharg = (int)pObj.TotCharg,
                    NetIncomBfTAX = (int)pObj.NetIncomBfTAX,
                };
                _context.IncomeState.Add(objPd);
                _context.Entry(objPd).Property(x => x.Room1).IsModified = true;
                _context.Entry(objPd).Property(x => x.FoodB1).IsModified = true;
                _context.Entry(objPd).Property(x => x.FoodB2).IsModified = true;
                _context.Entry(objPd).Property(x => x.FoodB3).IsModified = true;
                _context.Entry(objPd).Property(x => x.FoodB4).IsModified = true;
                _context.Entry(objPd).Property(x => x.FoodB5).IsModified = true;
                _context.Entry(objPd).Property(x => x.Other).IsModified = true;
                _context.Entry(objPd).Property(x => x.Other1).IsModified = true;
                _context.Entry(objPd).Property(x => x.Other2).IsModified = true;
                _context.Entry(objPd).Property(x => x.Other3).IsModified = true;
                _context.Entry(objPd).Property(x => x.Other4).IsModified = true;
                _context.Entry(objPd).Property(x => x.Other5).IsModified = true;
                _context.Entry(objPd).Property(x => x.Other6).IsModified = true;
                _context.Entry(objPd).Property(x => x.Rent).IsModified = true;
                _context.Entry(objPd).Property(x => x.TotReven).IsModified = true;
                _context.Entry(objPd).Property(x => x.TotExpen).IsModified = true;
                _context.Entry(objPd).Property(x => x.TotDeptIncom).IsModified = true;
                _context.Entry(objPd).Property(x => x.UndisExpens1).IsModified = true;
                _context.Entry(objPd).Property(x => x.UndisExpens2).IsModified = true;
                _context.Entry(objPd).Property(x => x.UndisExpens3).IsModified = true;
                _context.Entry(objPd).Property(x => x.UndisExpens4).IsModified = true;
                _context.Entry(objPd).Property(x => x.UndisExpens5).IsModified = true;
                _context.Entry(objPd).Property(x => x.UndisExpens6).IsModified = true;
                _context.Entry(objPd).Property(x => x.GrossProfit).IsModified = true;
                _context.Entry(objPd).Property(x => x.MgtFee).IsModified = true;
                _context.Entry(objPd).Property(x => x.IncomBfCharg).IsModified = true;
                _context.Entry(objPd).Property(x => x.Property).IsModified = true;
                _context.Entry(objPd).Property(x => x.Insurance).IsModified = true;
                _context.Entry(objPd).Property(x => x.Interest).IsModified = true;
                _context.Entry(objPd).Property(x => x.PropDepreciationerty).IsModified = true;
                _context.Entry(objPd).Property(x => x.TotCharg).IsModified = true;
                _context.Entry(objPd).Property(x => x.NetIncomBfTAX).IsModified = true;
                _context.Entry(objPd).Property(x => x.Replace).IsModified = true;
                _context.Entry(objPd).Property(x => x.AjstNetIncom).IsModified = true;
                _context.Entry(objPd).Property(x => x.IncomTAX).IsModified = true;
                _context.Entry(objPd).Property(x => x.NetIncom).IsModified = true;

                _context.UpdateRange(objPd);
                _context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;

        }
        private bool IncomeStateTotalRevenUpdate(IncomeStateDto pObj)
        {
            bool result = false;
            try
            {
                IncomeState objPd = new IncomeState
                {
                    ID = pObj.ID,
                    MonthID = pObj.MonthID,
                    QuarterNo = pObj.QuarterNo,
                    GroupID = pObj.GroupID,

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
                    PropDepreciationerty = (int)pObj.PropDepreciationerty,
                    TotCharg = (int)pObj.TotCharg,
                    NetIncomBfTAX = (int)pObj.NetIncomBfTAX,

                };
                _context.ChangeTracker.Clear();
                _context.IncomeState.Add(objPd);
                _context.Entry(objPd).Property(x => x.TotReven).IsModified = true;
                _context.Update(objPd);
                _context.SaveChanges();


                result = true;
            }
            catch
            {
                result = false;
            }
            return result;

        }
        private bool CustomerRowRatingUpdate(CustomerRawRatingDto pObj)
        {
            bool result = false;
            try
            {
                CustomerRawRating objPd = new CustomerRawRating
                {
                    ID = pObj.ID,
                    MonthID = pObj.MonthID,
                    QuarterNo = pObj.QuarterNo,
                    GroupID = pObj.GroupID,
                    Attribute = pObj.Attribute,
                    Segment = pObj.Segment,
                    RawRating = (int)pObj.RawRating
                };
                _context.CustomerRawRating.Attach(objPd);
                _context.Entry(objPd).Property(x => x.RawRating).IsModified = true;
                _context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                string exp = ex.ToString();
                result = false;
            }
            return result;

        }
        private bool RoomAllocationActualDemandUpdate(RoomAllocationDto pObj)
        {
            bool result = false;
            try
            {
                RoomAllocation objPd = new RoomAllocation
                {
                    ID = pObj.ID,
                    MonthID = pObj.MonthID,
                    QuarterNo = pObj.QuarterNo,
                    GroupID = pObj.GroupID,
                    Weekday = pObj.Weekday,
                    Segment = pObj.Segment,
                    RoomsAllocated = pObj.RoomsAllocated,
                    ActualDemand = pObj.ActualDemand,
                    RoomsSold = pObj.RoomsSold,
                    Confirmed = pObj.Confirmed,
                    Revenue = (int)pObj.Revenue,
                    QuarterForecast = pObj.QuarterForecast,
                };
                _context.RoomAllocation.Add(objPd);
                _context.Entry(objPd).Property(x => x.ActualDemand).IsModified = true;
                _context.Update(objPd);
                _context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;

        }
        private bool RoomAllocationRoomSoldUpdate(RoomAllocationDto pObj)
        {
            bool result = false;
            try
            {
                RoomAllocation objPd = new RoomAllocation
                {
                    ID = pObj.ID,
                    MonthID = pObj.MonthID,
                    QuarterNo = pObj.QuarterNo,
                    GroupID = pObj.GroupID,
                    Weekday = pObj.Weekday,
                    Segment = pObj.Segment,
                    RoomsAllocated = pObj.RoomsAllocated,
                    ActualDemand = pObj.ActualDemand,
                    RoomsSold = pObj.RoomsSold,
                    Confirmed = pObj.Confirmed,
                    Revenue = (int)pObj.Revenue,
                    QuarterForecast = pObj.QuarterForecast,
                };
                _context.RoomAllocation.Add(objPd);
                _context.Entry(objPd).Property(x => x.RoomsSold).IsModified = true;
                _context.Update(objPd);
                _context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;

        }
        private bool RoomAllocationRevenueUpdate(RoomAllocationDto pObj)
        {
            bool result = false;
            try
            {
                RoomAllocation objPd = new RoomAllocation
                {
                    ID = pObj.ID,
                    MonthID = pObj.MonthID,
                    QuarterNo = pObj.QuarterNo,
                    GroupID = pObj.GroupID,
                    Weekday = pObj.Weekday,
                    Segment = pObj.Segment,
                    RoomsAllocated = pObj.RoomsAllocated,
                    ActualDemand = pObj.ActualDemand,
                    RoomsSold = pObj.RoomsSold,
                    Confirmed = pObj.Confirmed,
                    Revenue = (int)pObj.Revenue,
                    QuarterForecast = pObj.QuarterForecast,
                };
                _context.RoomAllocation.Add(objPd);
                _context.Entry(objPd).Property(x => x.Revenue).IsModified = true;
                _context.Update(objPd);
                _context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;

        }
        private bool RoomAllocationUpdate(RoomAllocationDto pObj)
        {
            bool result = false;
            try
            {
                RoomAllocation objPd = new RoomAllocation
                {
                    ID = pObj.ID,
                    MonthID = pObj.MonthID,
                    QuarterNo = pObj.QuarterNo,
                    GroupID = pObj.GroupID,
                    Weekday = pObj.Weekday,
                    Segment = pObj.Segment,
                    RoomsAllocated = pObj.RoomsAllocated,
                    ActualDemand = pObj.ActualDemand,
                    RoomsSold = pObj.RoomsSold,
                    Confirmed = pObj.Confirmed,
                    Revenue = (int)pObj.Revenue,
                    QuarterForecast = pObj.QuarterForecast,
                };
                _context.RoomAllocation.Add(objPd);
                _context.Entry(objPd).Property(x => x.RoomsSold).IsModified = true;
                _context.Update(objPd);
                _context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;

        }
        private bool WeightedAttributeRatingUpdate(WeightedAttributeRatingDto pObj)
        {
            bool result = false;
            try
            {
                WeightedAttributeRating objPd = new WeightedAttributeRating
                {
                    ID = pObj.ID,
                    MonthID = pObj.MonthID,
                    QuarterNo = pObj.QuarterNo,
                    GroupID = pObj.GroupID,
                    CustomerRating = Convert.ToInt16(pObj.CustomerRating),
                    Segment = pObj.Segment,
                    ActualDemand = (int)pObj.ActualDemand
                };
                _context.WeightedAttributeRating.Attach(objPd);
                _context.Entry(objPd).Property(x => x.ActualDemand).IsModified = true;
                _context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;

        }
        private bool SoldRoomByChannelUpdate(SoldRoomByChannelDto pObj)
        {
            bool result = false;
            try
            {
                SoldRoomByChannel objPd = new SoldRoomByChannel
                {
                    ID = pObj.ID,
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
                _context.SoldRoomByChannel.Attach(objPd);
                _context.Entry(objPd).Property(x => x.Revenue).IsModified = true;
                _context.Entry(objPd).Property(x => x.Cost).IsModified = true;
                _context.Entry(objPd).Property(x => x.SoldRoom).IsModified = true;
                _context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;

        }

        public int InsertRank(int monthId, int currentQuarter, int groupID, string groupName, string schoolName, string indicator, decimal profiM, DateTime time)
        {
            IQueryable<Rankings> query = _context.Rankings;
            var obj = new Rankings()
            {
                MonthID = monthId,
                Month = currentQuarter,
                Indicator = indicator,
                Institution = schoolName,
                Performance = profiM,
                TeamName = groupName,
                TeamNo = groupID,
                Time = time
            };
            _context.Rankings.Add(obj);
            int status = _context.SaveChanges();
            int id = obj.ID;
            _context.ChangeTracker.Clear();

            return id;
        }
        private bool RankingUpdate(RankingsDto pObj)
        {
            bool result = false;
            try
            {
                Rankings objPd = new Rankings
                {
                    ID = pObj.ID,
                    MonthID = pObj.MonthID,
                    Month = pObj.Month,
                    TeamNo = pObj.TeamNo,
                    TeamName = pObj.TeamName,
                    Indicator = pObj.Indicator,
                    Institution = pObj.Institution,
                    Performance = pObj.Performance,
                    Time = pObj.Time
                };
                _context.Rankings.Add(objPd);
                _context.Entry(objPd).State = EntityState.Modified;
                _context.Update(objPd);
                _context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;

        }
        private int ScalarQueryMaxGroupNoRommAllocation(int monthID, int quarterNo)
        {
            int MaxGroup = 0;
            var list = (from r in _context.RoomAllocation
                        where (r.MonthID == monthID && r.QuarterNo == quarterNo)
                        select new
                        {
                            MaxGroup = r.GroupID

                        }).ToList();
            //group r by r.GroupID into g
            //select new
            //{
            //    MaxGroup = g.Max(x => x.GroupID)

            //}).ToList();


            if (list.Count > 0)
            {
                MaxGroup = list.Max(x => x.MaxGroup);
            }
            return MaxGroup;

        }


        private async Task<List<RoomAllocationDto>> GetDataByGroupWeekdayRoomAllocation(int monthId, int quarterNo, int groupId, bool weekday)
        {
            var data = await _context.RoomAllocation.Where(x => x.MonthID == monthId && x.QuarterNo == quarterNo && x.GroupID == groupId && x.Weekday == weekday).ToListAsync();
            if (data == null)
            {
                throw new ValidationException("data not found ");
            }
            return data.Adapt<List<RoomAllocationDto>>();

        }

        private async Task<List<SoldRoomByChannelDto>> GetDataByMonthSoldRoomByChannel(int monthId, int quarterNo)
        {
            var data = await _context.SoldRoomByChannel.Where(x => x.MonthID == monthId && x.QuarterNo == quarterNo).ToListAsync();
            if (data == null)
            {
                throw new ValidationException("data not found ");
            }
            return data.Adapt<List<SoldRoomByChannelDto>>();

        }
        private decimal GetDataBySingleRowPriceDecision(int monthId, int quarterNo, int groupId, bool weekday, string distributionChannel, string segment)
        {
            var data = (from m in _context.PriceDecision
                        where (m.MonthID == monthId && m.QuarterNo == quarterNo && m.GroupID == groupId
                        && m.DistributionChannel == distributionChannel && m.Segment == segment && m.Weekday == weekday)
                        select new { ActualDemand = m.ActualDemand }).ToList();
            decimal ActualDemand = 0;
            if (data.Count > 0)
            {
                ActualDemand = data[0].ActualDemand;
            }
            return ActualDemand;

        }



        private decimal GetDataByEachDecisionRoomAllocation(int monthId, int quarterNo, int groupId, bool weekday, string segment)
        {
            var data = (from m in _context.RoomAllocation
                        where (m.MonthID == monthId && m.QuarterNo == quarterNo && m.GroupID == groupId
                        && m.Segment == segment && m.Weekday == weekday)
                        select new { RoomsSold = m.RoomsSold }).ToList();
            decimal RoomsSold = 0;
            if (data.Count > 0)
            {
                RoomsSold = data[0].RoomsSold;
            }
            return RoomsSold;

        }

        private decimal ScalarSingleRevenueSoldRoomByChannel(int monthId, int quarterNo, int groupID, string segment, string channel, bool weekday)
        {

            decimal Revenue = 0;
            var list = (from dcvsc in _context.DistributionChannelVSsegmentConfig
                        join m in _context.Months on dcvsc.ConfigID equals m.ConfigId
                        join pd in _context.PriceDecision on m.MonthId equals pd.MonthID
                        where (m.Sequence == pd.QuarterNo)
                        join sbr in _context.SoldRoomByChannel on pd.MonthID equals sbr.MonthID
                        where (pd.QuarterNo == sbr.QuarterNo && pd.GroupID == sbr.GroupID && pd.DistributionChannel == sbr.Channel
                        && pd.Segment == sbr.Segment && pd.Weekday == sbr.Weekday && dcvsc.Segment == sbr.Segment && dcvsc.DistributionChannel == sbr.Channel
                       && sbr.MonthID == monthId && sbr.GroupID == groupID && sbr.QuarterNo == quarterNo && sbr.Segment == segment
                       && sbr.Channel == channel && sbr.Weekday == weekday)
                        select new
                        {
                            Price = pd.Price,
                            SoldRoom = sbr.SoldRoom,
                            CostPercentage = dcvsc.CostPercent
                        }).ToList();


            if (list.Count > 0)
            {

                var listReven = list.GroupBy(x => new { x.Price, x.CostPercentage, x.SoldRoom }).Select(g => new { Revenue = g.Key.Price * g.Key.SoldRoom }).ToList();
                Revenue = listReven[0].Revenue;
            }
            return Revenue;

        }

        private decimal ScalarSingleCostSoldRoomByChannel(int monthId, int quarterNo, int groupID, string segment, string channel, bool weekday)
        {

            decimal COST = 0;
            var list = (from dcvsc in _context.DistributionChannelVSsegmentConfig
                        join m in _context.Months on dcvsc.ConfigID equals m.ConfigId
                        join pd in _context.PriceDecision on m.MonthId equals pd.MonthID
                        where (m.Sequence == pd.QuarterNo)
                        join sbr in _context.SoldRoomByChannel on pd.MonthID equals sbr.MonthID
                        where (pd.QuarterNo == sbr.QuarterNo && pd.GroupID == sbr.GroupID && pd.DistributionChannel == sbr.Channel
                        && pd.Segment == sbr.Segment && pd.Weekday == sbr.Weekday && dcvsc.Segment == sbr.Segment && dcvsc.DistributionChannel == sbr.Channel
                       && sbr.MonthID == monthId && sbr.GroupID == groupID && sbr.QuarterNo == quarterNo && sbr.Segment == segment
                       && sbr.Channel == channel && sbr.Weekday == weekday)

                        group new { pd, dcvsc, sbr } by new { pd.Price, dcvsc.CostPercent, sbr.SoldRoom } into dps

                        select new
                        {
                            // priceDecision.price * distributionChannelVSsegmentConfig.costPercent * soldRoomByChannel.soldRoom
                            COST = dps.Sum((x => x.pd.Price * x.dcvsc.CostPercent * x.sbr.SoldRoom)),

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
                            noOfHotels = c.HotelsCount

                        }).ToList();

            if (list.Count > 0)
            {
                noOfHotels = list[0].noOfHotels;
            }
            return noOfHotels;

        }

        private decimal ScalarGroupRoomRevenueByMonthSoldRoomByChannel(int monthId, int quarterNo, int groupId)
        {

            decimal groupRevenue = 0;
            var list = (from c in _context.SoldRoomByChannel
                        where (c.MonthID == monthId && c.QuarterNo == quarterNo && c.GroupID == groupId)
                        select new { Revenue = c.Revenue }).ToList();

            if (list.Count > 0)
            {
                groupRevenue = list.Sum(x => x.Revenue);
            }
            return groupRevenue;

        }
        private decimal ScalarAttributeRevenueScoreRoomAllocation(int monthId, int quarterNo, int groupId, string attribute)
        {
            decimal RevenueScore = 0;
            var list = (from r in _context.RoomAllocation
                        join m in _context.Months on r.MonthID equals m.MonthId
                        where (r.QuarterNo == m.Sequence)
                        join irawc in _context.IdealRatingAttributeWeightConfig on m.ConfigId equals irawc.ConfigID
                        where (r.Segment == irawc.Segment && r.MonthID == monthId && r.QuarterNo == quarterNo && r.GroupID == groupId && irawc.Attribute == attribute)

                        select new { RevenueScore = irawc.Weight * r.RoomsSold }).ToList();

            if (list.Count > 0)
            {
                RevenueScore = list.Sum(x => x.RevenueScore);
            }
            return RevenueScore;

        }

        private decimal ScalarQueryTotalSpendingMarketingDecision(int monthId, int quarterNo, int groupId)
        {

            decimal totalSpending = 0;
            var list = (from r in _context.MarketingDecision

                        where (r.MonthID == monthId && r.QuarterNo == quarterNo
                        && r.GroupID == groupId)
                        select new
                        {
                            totalSpending = r.Spending + r.LaborSpending
                        }
                        ).ToList();


            if (list.Count > 0)
            {
                totalSpending = list.Sum(x => x.totalSpending);
            }
            return totalSpending;

        }

        private decimal ScalarGroupDistriCostByMonthSoldByChannel(int monthId, int quarterNo, int groupId)
        {

            decimal groupCost = 0;
            var list = (from r in _context.SoldRoomByChannel

                        where (r.MonthID == monthId && r.QuarterNo == quarterNo
                        && r.GroupID == groupId)
                        select new { groupCost = r.Cost }).ToList();

            if (list.Count > 0)
            {
                groupCost = list.Sum(x => x.groupCost);
            }
            return groupCost;

        }
        private BalanceSheetDto GetDataBySingleRowBallanceSheet(int monthId, int quarterNo, int groupId)
        {
            var data = _context.BalanceSheet.
                SingleOrDefault(x => x.MonthID == monthId && x.QuarterNo == quarterNo && x.GroupID == groupId
                );
            if (data == null)
            {
                // throw new ValidationException("data not found ");
                return new BalanceSheetDto();
            }
            return data.Adapt<BalanceSheetDto>();


        }

        private decimal ScalarMonthDepreciationTotalAttributeDecision(int monthId, int quarter, int groupID)
        {

            decimal TotalDepreciation = 0;
            var list = (from ad in _context.AttributeDecision
                        join m in _context.Months on ad.MonthID equals m.MonthId
                        where (ad.QuarterNo == m.Sequence)
                        join amcoc in _context.AttributeMaxCapitalOperationConfig on m.ConfigId equals amcoc.ConfigID
                        where (ad.Attribute == amcoc.Attribute && ad.MonthID == monthId && ad.QuarterNo == quarter && ad.GroupID == groupID)
                        select new
                        {

                            AccumulatedCapital = ad.AccumulatedCapital,
                            NewCapital = ad.NewCapital,
                            DepreciationYearly = amcoc.DepreciationYearly

                        }).ToList();

            if (list.Count > 0)
            {
                TotalDepreciation = list.Sum(x => ((x.AccumulatedCapital + x.NewCapital) * x.DepreciationYearly / 12));
            }
            return TotalDepreciation;
        }

        private decimal ScalarMonthlyTotalNewCapitalAttributeDecision(int monthId, int quarterNo, int groupId)
        {

            decimal TotalNewCapital = 0;
            var list = (from r in _context.AttributeDecision

                        where (r.MonthID == monthId && r.QuarterNo == quarterNo
                        && r.GroupID == groupId)
                        select new { TotalNewCapital = r.NewCapital }).ToList();

            if (list.Count > 0)
            {
                TotalNewCapital = list.Sum(x => x.TotalNewCapital);
            }
            return TotalNewCapital;

        }

        private decimal ScalarGetTotalRevenueIncomeState(int monthId, int groupId, int quarterNo)
        {

            decimal totalRevenue = 0;
            var list = (from r in _context.IncomeState

                        where (r.MonthID == monthId && r.QuarterNo == quarterNo
                        && r.GroupID == groupId)
                        select new
                        {
                            TotalRevenue = r.TotReven

                        }).ToList();


            if (list.Count > 0)
            {
                totalRevenue = list.Sum(x => x.TotalRevenue);
            }
            return totalRevenue;

        }

        private decimal ScalarQueryRevenueByWeekSegmentRoomAllocation(int monthId, int groupId, int quarterNo, string segment, bool weekday)
        {
            decimal roomAllocation = 0;
            var list = (from r in _context.RoomAllocation
                        join m in _context.Months on r.MonthID equals m.MonthId
                        where (r.QuarterNo == m.Sequence)
                        join dcvsc in _context.DistributionChannelVSsegmentConfig on r.Segment equals dcvsc.Segment
                        where (m.ConfigId == dcvsc.ConfigID)
                        join pd in _context.PriceDecision on m.MonthId equals pd.MonthID
                        where (m.Sequence == pd.QuarterNo && r.GroupID == Convert.ToInt16(pd.GroupID) && dcvsc.DistributionChannel == pd.DistributionChannel
                        && r.Segment == pd.Segment && r.Weekday == pd.Weekday && r.MonthID == monthId && r.QuarterNo == quarterNo
                        && r.GroupID == groupId && r.Segment == segment && r.Weekday == weekday)
                        select new { roomAllocation = r.RoomsSold * dcvsc.Percentage * pd.Price }).ToList();

            if (list.Count > 0)
            {
                roomAllocation = list.Sum(x => x.roomAllocation);
            }
            return roomAllocation;

        }

        private string ScalarSchoolName(int monthId)
        {
            return "HardCord ";
        }

        private string ScalarGroupName(int monthId, int groupId)
        {
            return "HardCode";
        }
        private Rankings GetDataBySingleRowRanking(int monthId, string indicator, int teamno)
        {

            var data = _context.Rankings.SingleOrDefault(x => x.MonthID == monthId && x.Indicator == indicator && x.TeamNo == teamno);
            if (data == null)
            {
                return new Rankings();
            }
            return data.Adapt<Rankings>();

        }


        public async Task<List<MonthDto>> GetMonthListByClassId(int classID)
        {
            var data = await _context.Months.Where(x => x.ClassId == classID && x.IsComplete == false).ToListAsync();
            if (data == null)
            {
                throw new ValidationException("data not found ");
            }
            return data.Adapt<List<MonthDto>>();

        }

        public async Task<decimal> ScalarTotalOtherExpenAttributeDecision(int monthId, int quarterNo, int groupId)
        {
            var data = await (from a in _context.AttributeDecision.
                         Where(x => x.MonthID == monthId && x.QuarterNo == quarterNo && x.GroupID == groupId)
                              select new { TotalOtherExpens = a.OperationBudget }).ToListAsync();
            decimal TotalOtherExpens = 0;
            if (data.Count > 0)
            {
                TotalOtherExpens = data.Sum(x => x.TotalOtherExpens);

            }


            return TotalOtherExpens;
        }
        public async Task<decimal> ScalarTotalAccumuCapitalInAMonthAttributeDecision(int monthId, int quarterNo, int groupId)
        {
            var data = await (from a in _context.AttributeDecision.
                         Where(x => x.MonthID == monthId && x.QuarterNo == quarterNo && x.GroupID == groupId)
                              select new { TotalAccumuCapital = a.AccumulatedCapital }).ToListAsync();
            decimal TotalAccumuCapital = 0;
            if (data.Count > 0)
            {
                TotalAccumuCapital = data.Sum(x => x.TotalAccumuCapital);
            }

            return TotalAccumuCapital;
        }
    }
    public class CalculationResponse

    {
        public int MonthId { get; set; }
        public string Status { get; set; }
    }
}
