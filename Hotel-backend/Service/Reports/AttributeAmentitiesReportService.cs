using Common.ReportDto;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Reports
{

    public interface IAttributeAmentitiesReportService
    {
        Task<AttributeAmentitiesReportDto> ReportAsync(ReportParams p);
    }

    public class AttributeAmentitiesReportService : IAttributeAmentitiesReportService
    {
        private readonly HotelDbContext _context;
        private List<AttributeDecision> _attributeDecision;

        public AttributeAmentitiesReportService(HotelDbContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<AttributeAmentitiesReportDto> ReportAsync(ReportParams p)
        {

            _attributeDecision = await _context.AttributeDecision.Where(x => x.MonthID == p.MonthId && x.QuarterNo == p.CurrentQuarter && x.GroupID == p.GroupId).ToListAsync();

            string[] atrNames = new string[] { "Spa", "Fitness Center", "Business Center", "Golf Course", "Other Recreation Facilities - Pools, game rooms, tennis courts, ect", "Management/Sales Attention", "Resturants", "Bars", "Room Service", "Banquet & Catering", "Meeting Rooms", "Entertainment", "Courtesy(FB)", "Guest Rooms", "Reservations", "Guest Check in/Guest Check out", "Concierge", "Housekeeping", "Maintanence and security", "Courtesy (Rooms)" };
            List<AttributeAmentiDto> attrubtes = atrNames.Select(x => GetAttribute(x)).ToList();


            decimal totalAccumu = _attributeDecision.Where(x => atrNames.Contains(x.Attribute)).Sum(x => x.AccumulatedCapital);
            decimal totalN = _attributeDecision.Where(x => atrNames.Contains(x.Attribute)).Sum(x => x.NewCapital);
            decimal accoumlator = totalN + _attributeDecision.Where(x => atrNames.Contains(x.Attribute)).Sum(x => x.OperationBudget);
            decimal totalBudget = accoumlator - totalN;
            accoumlator += _attributeDecision.Where(x => atrNames.Contains(x.Attribute)).Sum(x => x.LaborBudget);
            decimal totalLabor = accoumlator - totalN - totalBudget;

            AttributeAmentiDto total = new AttributeAmentiDto()
            {
                Label = "TOTAL",
                AccumulatedCapital = totalAccumu,
                LaborBudget = totalLabor,
                NewCaptial = totalN,
                OperationBudget = totalBudget,

            };
            attrubtes.Add(total);
            return new AttributeAmentitiesReportDto
            {
                TotalExpense = accoumlator,
                Attributes = attrubtes
            };
        }

        private AttributeAmentiDto GetAttribute(string label)
        {
            var attr = _attributeDecision.FirstOrDefault(x => x.Attribute.Trim().Equals(label, StringComparison.OrdinalIgnoreCase));
            return new AttributeAmentiDto { Label = label, AccumulatedCapital = attr.AccumulatedCapital, LaborBudget = attr.LaborBudget, NewCaptial = attr.NewCapital, OperationBudget = attr.OperationBudget };
        }
    }
}
