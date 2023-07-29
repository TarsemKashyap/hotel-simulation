
namespace Common.ReportDto
{
    public class IncomeReportDto
    {
        public Revenue Revenue { get; set; }
        public DepartmentalExpenses DepartmentalExpenses { get; set; }
        public ReportAttribute TotalDepartIncome { get; set; } = new ReportAttribute { Label = "TOTAL DEPARTMENTAL INCOME" };
        public UndistOperatingExpenses UndistOperatingExpenses { get; set; }
        public ReportAttribute GrossOperatingProfit { get; set; } = new ReportAttribute { Label = "GROSS OPERATING PROFIT" };
        public ReportAttribute ManagmentFees { get; set; } = new ReportAttribute { Label = "GROSS OPERATING PROFIT" };
        public ReportAttribute IncomeBeforeFixedCharges { get; set; } = new ReportAttribute { Label = "INCOME BEFORE FIXED CHARGES" };
        public FixedCharges FixedCharges { get; set; }
        public ReportAttribute NetOperatingIncome { get; set; } = new ReportAttribute { Label = "NET OPERATING INCOME (BEFORE TAX)" };
        public ReportAttribute IncomeTax { get; set; } = new ReportAttribute { Label = "Income Tax" };
        public ReportAttribute NetIncome { get; set; } = new ReportAttribute { Label = "NET INCOME" };



    }

    public class Revenue
    {
        public ReportAttribute Rooms { get; set; }
        public FoodBeverage FoodBeverage { get; set; }
        public OtherOperatedDocs OtherOperatedDocs { get; set; }
        public ReportAttribute RentelOtherIncome { get; set; }
        public ReportAttribute TotalRevenue { get; set; }


    }

    public class DepartmentalExpenses : ReportAttribute
    {
        public DepartmentalExpenses()
        {
            this.Label = "DEPARTMENTAL EXPENSES";
        }
        public ReportAttribute TotalDepartmentalIncome => new ReportAttribute() { Label = "TOTAL DEPARTMENTAL INCOME" };
        public void Rooms(decimal? money) => AddChild("Rooms", money);
        public void FoodAndBeverage(decimal? money) => AddChild("Food and Beverage", money);
        public void OtherOperatedDepartment(decimal? money) => AddChild("Other Operated Departments", money);
        public void TotalDepartmentalExpenses(decimal? money) => AddChild("TOTAL DEPARTMENTAL EXPENSES", money);
    }

    public class FoodBeverage : ReportAttribute
    {
        public FoodBeverage()
        {
            Label = "Food and Beverage";
        }
        public void Restaurants(decimal? money) => AddChild("Restaurants", money);
        public void Bars(decimal? money) => AddChild("Bars", money);

        public void RoomService(decimal? money) => AddChild("Room Service", money);

        public void BanquetCartering(decimal? money) => AddChild("Banquet & Cartering", money);
        public void MeetingRooms(decimal? money) => AddChild("Meeting Rooms", money);
    }

    public class OtherOperatedDocs : ReportAttribute
    {
        public void GolfCourse(decimal? money) => AddChild("Golf Course", money);
        public void Spa(decimal? money) => AddChild("Spa", money);
        public void FitnessCenter(decimal? money) => AddChild("Fitness Center", money);
        public void BusinessCenter(decimal? money) => AddChild("Business Center", money);
        public void OtherRecreationFacilities(decimal? money) => AddChild("Other Recreation Facilities", money);
        public void Entertainment(decimal? money) => AddChild("Entertainment", money);

    }

    public class FixedCharges : ReportAttribute
    {
        public FixedCharges()
        {
            Label = "FIXED CHARGES";
        }

        public void PropertyAndOtherTax(decimal? value) => AddChild("Property and Other Taxes", value);
        public void Insurance(decimal? value) => AddChild("Insurance", value);
        public void Interest(decimal? value) => AddChild("Interest", value);
        public void DepreciationAmortization(decimal? value) => AddChild("Depreciation & amortization", value);
        public void TotalFixedCharges(decimal? value) => AddChild("Total Fixed Charges", value);
    }

    public class UndistOperatingExpenses : ReportAttribute
    {
        public UndistOperatingExpenses()
        {
            this.Label = "UNDISTRIBUTED OPERATING EXPENSES";
        }

        public void Adminstrative(decimal? value) => AddChild("Administrative and General", value);
        public void SalesAndMarketing(decimal? value) => AddChild("Sales and Marketing", value);
        public void CostOfUtilizingChannels(decimal? value) => AddChild("Cost of utilizing distribution channels", value);
        public void TotalSaleAndMarketing(decimal? value) => AddChild("TOTAL Sales and Marketing", value);
        public void PropertyOperation(decimal? value) => AddChild("Property Operation and Maintenance", value);
        public void Utilities(decimal? value) => AddChild("Utilities", value);
        public void TotalUnderdistributed(decimal? value) => AddChild("TOTAL UNDISTRIBUTED OPERATING EXPENSES", value);
    }

  
}
