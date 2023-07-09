using Database;
using Database.Migrations;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdNet;

namespace Service
{
    public class FunCalculation
    {
        public List<MarketingDecisionDto> GetDataByQuarterMarketDecision(HotelDbContext context, int monthId, int quarterNo)
        {
            List<MarketingDecisionDto> listMD = context.MarketingDecision.Where(x => x.QuarterNo == quarterNo && x.MonthID == monthId).
                Select(x => new MarketingDecisionDto
                {
                    ID=x.ID,
                    GroupID = x.GroupID,
                    QuarterNo = x.QuarterNo,
                    MonthID = x.MonthID,
                    ActualDemand = x.ActualDemand,
                    Segment = x.Segment,
                    MarketingTechniques = x.MarketingTechniques,
                    LaborSpending = x.LaborSpending,
                    Spending = x.Spending,
                    Confirmed = x.Confirmed,
                }).ToList();

            return listMD;
        }
        public double ScalarQueryIndustrialNormPercentMarketingDecision(HotelDbContext context, int monthId, int currentQuarter, string segment, string marketTech)
        {
            double Proportion = 0;

            var list = (from m in context.Months
                        join sc in context.SegmentConfig on m.ConfigId equals sc.ConfigID
                        join msc in context.MarketingVSsegmentConfig on sc.Segment equals msc.Segment
                        select new
                        {
                            Proportion = sc.Percentage * msc.Percentage,
                            ClassId = m.ClassId,
                            ConfigId = m.ConfigId,
                            Segment = sc.Segment,
                            QuarterNo = m.Sequence,
                            MarketTechniques = msc.MarketingTechniques,
                            SCPercentage = sc.Percentage,
                            MSCPercentage = msc.Percentage,
                            MonthID = m.MonthId
                        })
                        .Where(x => x.MonthID == monthId && x.QuarterNo == currentQuarter
                                  && x.Segment.Trim() == segment.Trim() && x.MarketTechniques.Trim() == marketTech.Trim()
                               )

                       .ToList();
            if (list.Count > 0)
            {
                Proportion = list[0].Proportion;
            }
            else
            {
                Proportion = 0;
            }



            return Proportion;
        }

        public double ScalarQueryPastSpendingMarketingDecision(HotelDbContext context, int monthId, int quarterNo, int groupID, string marketingTechniques, string segment)
        {
            var lst = (from m in context.MarketingDecision.Where(x => x.MonthID == monthId && x.QuarterNo == quarterNo && x.GroupID == groupID && x.MarketingTechniques == marketingTechniques.Trim() && x.Segment == segment.Trim())
                       select new
                       {
                           PastSpending = m.Spending
                       }).ToList();

            double PastSpending = 0;
            if (lst.Count > 0)
            {
                PastSpending = lst[0].PastSpending;
            }
            return PastSpending;
        }

        public double ScalarQueryPastLaborSpendingMarketingDecision(HotelDbContext context, int monthId, int quarterNo, int groupID, string marketingTechniques, string segment)
        {
            var lst = (from m in context.MarketingDecision.Where(x => x.MonthID == monthId && x.QuarterNo == quarterNo && x.GroupID == groupID && x.MarketingTechniques == marketingTechniques.Trim() && x.Segment == segment.Trim())
                       select new
                       {
                           LaborSpending = m.LaborSpending
                       }).ToList();

            double LaborSpending = 0;
            if (lst.Count > 0)
            {
                LaborSpending = lst[0].LaborSpending;
            }
            return LaborSpending;

        }

        public double ScalarQueryLaborPercentMarketingDecision(HotelDbContext context, int monthId, string marketingTechniques, string segment, int quarterNo)
        {


            var lst = (from m in context.Months
                       join msc in context.MarketingVSsegmentConfig on m.ConfigId equals msc.ConfigID

                       select new
                       {
                           MonthID = m.MonthId,
                           Quarter = m.Sequence,
                           MarketingTechniques = msc.MarketingTechniques,
                           Segment = msc.Segment,
                           laborPercent = msc.LaborPercent
                       })
                       .Where(x => x.MonthID == monthId && x.Quarter == quarterNo
                       && x.MarketingTechniques == marketingTechniques.Trim() && x.Segment == segment.Trim()
                       ).ToList();


            //.GroupBy(x => x.laborPercent).ToListAsync();
            //
            //.ToList();
            // lst = lst[0].Where(x => x.MonthID == monthId && x.Quarter == quarterNo && x.MarketingTechniques == marketingTechniques.Trim() && x.Segment == segment)
            double LaborPercent = 0;
            if (lst.Count > 0)
            {
                LaborPercent = lst[0].laborPercent;
            }
            return LaborPercent;

        }
        public decimal ScalarQueryFairMarketMarketingDecision(HotelDbContext context, string segment, string marketingTechniques, int MonthID, int QuarterNo)
        {
            /*SELECT DISTINCT 
                      marketingVSsegmentConfig.percentage * priceMarketingAttributeSegmentConfig.percentage * quarterlyMarket.totalMarket * segmentConfig.percentage / classSession.noOfHotels
                       AS fairMarket
FROM         quarterlyMarket 
            INNER JOIN
                      marketingVSsegmentConfig ON quarterlyMarket.configID = marketingVSsegmentConfig.configID INNER JOIN
                      marketingDecision ON quarterlyMarket.sessionID = marketingDecision.sessionID 
            AND quarterlyMarket.quarterNo = marketingDecision.quarterNo 
            AND 
                      marketingVSsegmentConfig.marketingTechniques = marketingDecision.marketingTechniques AND 
                      marketingVSsegmentConfig.segment = marketingDecision.segment
            INNER JOIN
                      priceMarketingAttributeSegmentConfig ON quarterlyMarket.configID = priceMarketingAttributeSegmentConfig.configID AND 
                      marketingVSsegmentConfig.segment = priceMarketingAttributeSegmentConfig.segment 
            INNER JOIN
                      segmentConfig ON quarterlyMarket.configID = segmentConfig.configID AND marketingDecision.segment = segmentConfig.segment 
            INNER JOIN
                      classSession ON quarterlyMarket.sessionID = classSession.classSessionID
WHERE     (priceMarketingAttributeSegmentConfig.PMA = N'Marketing') AND (marketingDecision.segment = @segment) AND 
                      (marketingDecision.marketingTechniques = @marketingTechniques) AND (quarterlyMarket.sessionID = @sessionID) AND 
                      (quarterlyMarket.quarterNo = @quarterNo)*/

            var lst = (from m in context.Months
                       join msc in context.MarketingVSsegmentConfig on m.ConfigId equals msc.ConfigID
                       join md in context.MarketingDecision on m.MonthId equals md.MonthID
                       where (m.Sequence == md.QuarterNo && msc.MarketingTechniques == md.MarketingTechniques && msc.Segment == md.Segment)
                       join pmasc in context.PriceMarketingAttributeSegmentConfig on m.ConfigId equals pmasc.ConfigID
                       where (msc.Segment == pmasc.Segment)
                       join sc in context.SegmentConfig on m.ConfigId equals sc.ConfigID
                       where (md.Segment == sc.Segment)
                       join c in context.ClassSessions on m.ClassId equals c.ClassId
                       where (pmasc.PMA == "Marketing" && md.Segment == segment.Trim()
                       && md.MarketingTechniques == marketingTechniques && m.MonthId == MonthID && m.Sequence == QuarterNo)


                       select new
                       {

                           FairMarket = Convert.ToDecimal(msc.Percentage) * pmasc.Percentage * m.TotalMarket * Convert.ToDecimal(sc.Percentage) / c.HotelsCount

                       }
                       ).ToList();


            decimal FairMarket = 0;
            if (lst.Count > 0)
            {
                FairMarket = lst[0].FairMarket;
            }
            return FairMarket;

        }

        public double ScalarQueryAverageSpendingMarktingDecision(HotelDbContext context, string segment, string marketingTechniques, int monthID, int quarterNo)
        {

            /*SELECT              AVG(marketingDecision.spending * (1 - marketingVSsegmentConfig.laborPercent) + marketingDecision.laborSpending * marketingVSsegmentConfig.laborPercent) 
                                AS averageSpending
FROM                  marketingDecision INNER JOIN
                                quarterlyMarket ON marketingDecision.sessionID = quarterlyMarket.sessionID 
            AND marketingDecision.quarterNo = quarterlyMarket.quarterNo 
            INNER JOIN
                                marketingVSsegmentConfig ON quarterlyMarket.configID = marketingVSsegmentConfig.configID 
            AND 
                                marketingDecision.marketingTechniques = marketingVSsegmentConfig.marketingTechniques 
            AND marketingDecision.segment = marketingVSsegmentConfig.segment
WHERE              (marketingDecision.segment = @segment) AND (marketingDecision.marketingTechniques = @marketingTechniques)
            AND (marketingDecision.sessionID = @sessionID) AND 
                                (marketingDecision.quarterNo = @quarterNo)
GROUP BY       marketingDecision.segment, marketingDecision.marketingTechniques, marketingDecision.sessionID, marketingDecision.quarterNo*/
            double AverageSpending = 0;
            var list = (from md in context.MarketingDecision
                        join m in context.Months on md.MonthID equals m.MonthId
                        where (md.QuarterNo == m.Sequence)
                        join mvsc in context.MarketingVSsegmentConfig on m.ConfigId equals mvsc.ConfigID
                        where (md.MarketingTechniques == mvsc.MarketingTechniques && md.Segment == mvsc.Segment
                        && md.Segment == segment && md.MarketingTechniques == marketingTechniques && md.MonthID == monthID && md.QuarterNo == quarterNo
                        )
                        group new { md, mvsc } by new { md.Segment, md.MarketingTechniques, md.MonthID, md.QuarterNo, mvsc.LaborPercent } into gp
                        select new
                        {
                            /*AVG(marketingDecision.spending * (1 - marketingVSsegmentConfig.laborPercent) + marketingDecision.laborSpending * marketingVSsegmentConfig.laborPercent) 
                                */
                            AverageSpending = gp.Average(x => (x.md.Spending * (1 - x.mvsc.LaborPercent) + x.md.LaborSpending * x.mvsc.LaborPercent))

                        }

                      ).ToList();
            if (list.Count > 0)
            {
                AverageSpending = list[0].AverageSpending;
            }

            return AverageSpending;
        }
    }
}
