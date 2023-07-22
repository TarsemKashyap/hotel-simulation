using Common;
using Common.ReportDto;
using Database;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Tls;
using System;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace Service;

public interface IIncomeReportService
{
    Task<IncomeReportDto> Report(ReportParams reportParams);
}

public class IncomeReportService : AbstractReportService, IIncomeReportService
{
    private readonly HotelDbContext _context;

    public IncomeReportService(HotelDbContext context)
    {
        _context = context;
    }

    public async Task<IncomeReportDto> Report(ReportParams reportParams)
    {
        int quater = reportParams.CurrentQuarter, groupId = reportParams.GroupId;
        IncomeState incomeState = await _context.IncomeState.AsNoTracking().Where(x => x.QuarterNo == quater && x.GroupID == groupId).FirstOrDefaultAsync();
        IncomeReportDto report = new IncomeReportDto();
        report.Revenue = new Revenue()
        {
            Rooms = Money("Rooms", incomeState.Room1),
            RentelOtherIncome = Money("Rental and Other Income", incomeState.Rent),
            FoodBeverage = GetFoodBeverage(incomeState),
            OtherOperatedDocs = GetOperateDept(incomeState),
            TotalRevenue = GetTotalReveue(incomeState)
        };

        report.DepartmentalExpenses = DepartmentalExpesense(incomeState);

        report.UndistOperatingExpenses = UndistOperatingExpense(incomeState);
        //gross profit
        report.GrossOperatingProfit.Value = Money(incomeState.GrossProfit);

        //mgmt fee
        report.ManagmentFees.Value = Money(incomeState.IncomBfCharg);
        // income before fixed charge
        report.IncomeBeforeFixedCharges.Value = Money(incomeState.MgtFee);
        //Fixed Charges


        report.FixedCharges = FixedCharges(incomeState);
        report.NetOperatingIncome.Value = Money(incomeState.NetIncomBfTAX);
        report.IncomeTax.Value = Money(incomeState.IncomTAX);
        report.NetIncome.Value = Money(incomeState.NetIncom);

        return report;


    }

    private static FixedCharges FixedCharges(IncomeState incomeState)
    {
        FixedCharges fixedCharges = new FixedCharges();
        fixedCharges.PropertyAndOtherTax(incomeState.Property);
        fixedCharges.Insurance(incomeState.Insurance);
        fixedCharges.Interest(incomeState.Interest);
        fixedCharges.DepreciationAmortization(incomeState.PropDepreciationerty);
        fixedCharges.TotalFixedCharges(incomeState.TotCharg);
        return fixedCharges;
    }

    private static UndistOperatingExpenses UndistOperatingExpense(IncomeState incomeState)
    {
        UndistOperatingExpenses undist = new UndistOperatingExpenses();
        undist.Adminstrative(incomeState.UndisExpens1);
        undist.SalesAndMarketing(incomeState.UndisExpens2);
        undist.CostOfUtilizingChannels(incomeState.UndisExpens3);
        undist.TotalSaleAndMarketing((incomeState.UndisExpens2 + incomeState.UndisExpens3));
        undist.PropertyOperation(incomeState.UndisExpens4);
        undist.Utilities(incomeState.UndisExpens5);
        undist.TotalUnderdistributed(incomeState.UndisExpens6);
        return undist;
    }

    private DepartmentalExpenses DepartmentalExpesense(IncomeState incomeState)
    {
        DepartmentalExpenses expenses = new DepartmentalExpenses();
        expenses.Rooms(incomeState.Room);
        expenses.FoodAndBeverage(incomeState.Food2B);
        expenses.OtherOperatedDepartment(incomeState.Other7);
        expenses.TotalDepartmentalExpenses(incomeState.TotExpen);
        expenses.TotalDepartmentalIncome.Value = Money(incomeState.TotDeptIncom);
        return expenses;
    }

    private ReportAttribute GetTotalReveue(IncomeState incomeState)
    {
        decimal totalRevnew = incomeState.Room1 + incomeState.FoodB1 + incomeState.Other1 + incomeState.Rent;
        return new ReportAttribute { Label = "TOTAL REVENUE", Value = new Currency(totalRevnew) };
    }

    private OtherOperatedDocs GetOperateDept(IncomeState incomeState)
    {
        OtherOperatedDocs OtherOperatedDocs = new OtherOperatedDocs() { Label = "Other Operated Departments", Value = new Currency(incomeState.Other) };
        OtherOperatedDocs.GolfCourse(incomeState.Other1);
        OtherOperatedDocs.Spa(incomeState.Other2);
        OtherOperatedDocs.FitnessCenter(incomeState.Other3);
        OtherOperatedDocs.BusinessCenter(incomeState.Other4);
        OtherOperatedDocs.OtherRecreationFacilities(incomeState.Other5);
        OtherOperatedDocs.Entertainment(incomeState.Other6);
        return OtherOperatedDocs;
    }

    private FoodBeverage GetFoodBeverage(IncomeState incomeState)
    {
        var foodBeverage = new FoodBeverage
        {
            Value = new Currency(incomeState.FoodB),
        };
        foodBeverage.Restaurants(incomeState.FoodB1);
        foodBeverage.Bars(incomeState.FoodB2);
        foodBeverage.RoomService(incomeState.FoodB3);
        foodBeverage.BanquetCartering(incomeState.FoodB4);
        foodBeverage.MeetingRooms(incomeState.FoodB5);

        return foodBeverage;
    }
}





