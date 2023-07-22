namespace Common.ReportDto
{
    public class IncomeReportDto
    {
        public Revenue Revenue { get; set; }
        public DepartmentalExpenses DepartmentalExpenses { get; set; }
        public ReportAttribute TotalDepartIncome => new ReportAttribute { Label = "TOTAL DEPARTMENTAL INCOME" };
        public UndistOperatingExpenses UndistOperatingExpenses { get; set; }
        public ReportAttribute GrossOperatingProfit => new ReportAttribute { Label = "GROSS OPERATING PROFIT" };
        public ReportAttribute ManagmentFees => new ReportAttribute { Label = "GROSS OPERATING PROFIT" };
        public ReportAttribute IncomeBeforeFixedCharges => new ReportAttribute { Label = "INCOME BEFORE FIXED CHARGES" };
        public FixedCharges FixedCharges { get; set; }
        public ReportAttribute NetOperatingIncome => new ReportAttribute { Label = "NET OPERATING INCOME (BEFORE TAX)" };
        public ReportAttribute IncomeTax => new ReportAttribute { Label = "Income Tax" };
        public ReportAttribute NetIncome => new ReportAttribute { Label = "NET INCOME" };



    }

    public class Revenue
    {
        public ReportAttribute Rooms { get; set; }
        public FoodBeverage FoodBeverage { get; set; }
        public OtherOperatedDocs OtherOperatedDocs { get; set; }
        public ReportAttribute RentelOtherIncome { get; set; }
        public ReportAttribute TotalRevenue { get; set; }


    }
    public class ReportAttribute
    {
        public string Label { get; set; }
        public AbstractDecimal Value { get; set; }
        public Dictionary<string, AbstractDecimal> ChildAttribute { get; set; } = new Dictionary<string, AbstractDecimal>();
        protected void AddMoney(string label, decimal? money)
        {
            ChildAttribute.Add(label, money.HasValue ? new Currency(money.Value) : null);
        }

        public decimal Sum => ChildAttribute.Values.Sum(x => x.Value);
    }

    public class DepartmentalExpenses : ReportAttribute
    {
        public DepartmentalExpenses()
        {
            this.Label = "DEPARTMENTAL EXPENSES";
        }
        public ReportAttribute TotalDepartmentalIncome => new ReportAttribute() { Label = "TOTAL DEPARTMENTAL INCOME", Value = new Currency(Sum) };
        public void Rooms(decimal? money) => AddMoney("Rooms", money);
        public void FoodAndBeverage(decimal? money) => AddMoney("Food and Beverage", money);
        public void OtherOperatedDepartment(decimal? money) => AddMoney("Other Operated Departments", money);
        public void TotalDepartmentalExpenses(decimal? money) => AddMoney("TOTAL DEPARTMENTAL EXPENSES", money);
    }

    public class FoodBeverage : ReportAttribute
    {
        public FoodBeverage()
        {
            Label = "Food and Beverage";
        }
        public void Restaurants(decimal? money) => AddMoney("Restaurants", money);
        public void Bars(decimal? money) => AddMoney("Bars", money);

        public void RoomService(decimal? money) => AddMoney("Room Service", money);

        public void BanquetCartering(decimal? money) => AddMoney("Banquet & Cartering", money);
        public void MeetingRooms(decimal? money) => AddMoney("Meeting Rooms", money);
    }

    public class OtherOperatedDocs : ReportAttribute
    {
        public void GolfCourse(decimal? money) => AddMoney("Golf Course", money);
        public void Spa(decimal? money) => AddMoney("Spa", money);
        public void FitnessCenter(decimal? money) => AddMoney("Fitness Center", money);
        public void BusinessCenter(decimal? money) => AddMoney("Business Center", money);
        public void OtherRecreationFacilities(decimal? money) => AddMoney("Other Recreation Facilities", money);
        public void Entertainment(decimal? money) => AddMoney("Entertainment", money);

    }

    public class FixedCharges : ReportAttribute
    {
        public FixedCharges()
        {
            Label = "FIXED CHARGES";
        }

        public void PropertyAndOtherTax(decimal? value) => AddMoney("Property and Other Taxes", value);
        public void Insurance(decimal? value) => AddMoney("Insurance", value);
        public void Interest(decimal? value) => AddMoney("Interest", value);
        public void DepreciationAmortization(decimal? value) => AddMoney("Depreciation & amortization", value);
        public void TotalFixedCharges(decimal? value) => AddMoney("Total Fixed Charges", value);
    }

    public class UndistOperatingExpenses : ReportAttribute
    {
        public UndistOperatingExpenses()
        {
            this.Label = "UNDISTRIBUTED OPERATING EXPENSES";
        }

        public void Adminstrative(decimal? value) => AddMoney("Administrative and General", value);
        public void SalesAndMarketing(decimal? value) => AddMoney("Sales and Marketing", value);
        public void CostOfUtilizingChannels(decimal? value) => AddMoney("Cost of utilizing distribution channels", value);
        public void TotalSaleAndMarketing(decimal? value) => AddMoney("TOTAL Sales and Marketing", value);
        public void PropertyOperation(decimal? value) => AddMoney("Property Operation and Maintenance", value);
        public void Utilities(decimal? value) => AddMoney("Utilities", value);
        public void TotalUnderdistributed(decimal? value) => AddMoney("TOTAL UNDISTRIBUTED OPERATING EXPENSES", value);
    }

}
